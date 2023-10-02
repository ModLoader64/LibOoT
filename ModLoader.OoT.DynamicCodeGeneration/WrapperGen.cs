using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace ModLoader.OoT.DynamicCodeGeneration
{
    internal class WrapperGen
    {
        public static void GenerateWrapperFromFile()
        {
            var decomp_path = Path.GetFullPath("../../../../oot");
            var include_path = Path.GetFullPath(Path.Join(decomp_path, "include"));
            var z64save_h = Path.GetFullPath(Path.Join(include_path, "z64save.h"));
            GenerateWrapperFromC(z64save_h);
        }

        private static CodeSnippetStatement GenerateGetterLoop(StructDataStore entry, string func)
        {
            var typeNoArr = entry.type.Replace("[", "").Replace("]", "");
            if (typeNoArr.Contains("char"))
            {
                typeNoArr = typeNoArr.Replace("char", "@char");
            }
            var size = Int32.Parse(entry.type.Replace("s", "").Replace("u", "").Replace("char", "8").Replace("[", "").Replace("]", "")) / 8;
            return new CodeSnippetStatement($"            {typeNoArr}[] bytes = new {typeNoArr}[{entry.arrayCount}]; for(u32 i = 0; i < {entry.arrayCount}; i++){{bytes[i] = Memory.RAM.{func}(this.pointer + {entry.offset} + (i * {size}));}} return bytes;");
        }

        private static CodeSnippetStatement GenerateSetterLoop(StructDataStore entry, string func)
        {
            var size = Int32.Parse(entry.type.Replace("s", "").Replace("u", "").Replace("char", "8").Replace("[", "").Replace("]", "")) / 8;
            return new CodeSnippetStatement($"            for(u32 i = 0; i < {entry.arrayCount}; i++){{Memory.RAM.{func}(this.pointer + {entry.offset} + (i * {size}), value[i]);}}");
        }

        private static CodeSnippetStatement GenerateGetter(StructDataStore entry, string func) {
            return new CodeSnippetStatement($"            return Memory.RAM.{func}(this.pointer + {entry.offset});");
        }

        private static CodeSnippetStatement GenerateSetter(StructDataStore entry, string func)
        {
            return new CodeSnippetStatement($"            Memory.RAM.{func}(this.pointer + {entry.offset}, value);");
        }

        private static CodeSnippetStatement GenerateSubstruct(StructDataStore entry)
        {
            if (entry.type.Contains("[")) return GenerateSubstructArray(entry);
            return new CodeSnippetStatement($"            return new Wrapper{entry.type}(this.pointer + {entry.offset});");
        }

        public static CodeSnippetStatement GenerateSubstructArray(StructDataStore entry) {
            var typeNoArr = "Wrapper" + entry.type.Replace("[", "").Replace("]", "");
            return new CodeSnippetStatement($"          {typeNoArr}[] substructs = new {typeNoArr}[{entry.arrayCount}]; for(u32 i = 0; i < {entry.arrayCount}; i++){{substructs[i] = new {typeNoArr}(this.pointer + {entry.offset} + (i * {typeNoArr}.getSize()));}}; return substructs;");
        }

        public static void GenerateWrapperFromC(string file)
        {
            var lines = File.ReadAllLines(file);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Contains("typedef struct"))
                {
                    Console.WriteLine("Found a struct");
                    Console.WriteLine(line);
                    CodeCompileUnit compileUnit = new CodeCompileUnit();
                    CodeNamespace samples = new CodeNamespace("ModLoader.OoT.API");
                    compileUnit.Namespaces.Add(samples);
                    CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration();
                    typeDeclaration.Attributes = MemberAttributes.Public;
                    var fuck = new CodeMemberField();
                    fuck.Name = "pointer";
                    fuck.Type = new CodeTypeReference("u32");
                    fuck.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(System.Text.Json.Serialization.JsonIgnoreAttribute))));
                    typeDeclaration.Members.Add(fuck);
                    samples.Types.Add(typeDeclaration);
                    CodeConstructor con = new CodeConstructor();
                    con.Attributes = MemberAttributes.Public;
                    con.Parameters.Add(new CodeParameterDeclarationExpression("u32", "pointer"));
                    con.Statements.Add(new CodeSnippetStatement("           this.pointer = pointer;"));
                    typeDeclaration.Members.Add(con);
                    List<StructDataStore> data = new List<StructDataStore>();
                    for (int j = i + 1; j < lines.Length; j++)
                    {
                        var lookahead = lines[j];
                        if (!lookahead.Contains("}") && lookahead.Contains(";"))
                        {
                            // 0 = Offset, 1 = declaration
                            var parseOutComment = lookahead.Replace("/*", "").Split("*/");
                            var offset = parseOutComment[0].Trim();
                            var dec = parseOutComment[1].Trim();
                            // 0 = type, 1 = name and maybe comment.
                            var split = dec.Split(" ");
                            data.Add(new StructDataStore(split[1], split[0], offset));
                        }
                        else
                        {
                            var parseOutSyntax = lookahead.Replace(";", "").Replace("}", "").Split("//")[0].Trim();
                            var sizecomment = lookahead.Replace(";", "").Replace("}", "").Split("//")[1].Replace("size = ", "").Trim();
                            var sizefunc = new CodeMemberMethod();
                            sizefunc.Name = "getSize";
                            sizefunc.ReturnType = new CodeTypeReference(typeof(u32));
                            sizefunc.Attributes = MemberAttributes.Public | MemberAttributes.Static;
                            sizefunc.Statements.Add(new CodeSnippetStatement($"          return {sizecomment};"));
                            typeDeclaration.Members.Add(sizefunc);
                            typeDeclaration.Name = $"Wrapper{parseOutSyntax}";
                            foreach (var entry in data)
                            {
                                var member = new CodeMemberMethod();
                                member.Attributes = MemberAttributes.Private;
                                member.Name = $"_{entry.name}";
                                if (entry.isArray)
                                {
                                    entry.type = $"{entry.type}[]";
                                }
                                member.ReturnType = new CodeTypeReference(entry.type);
                                var member2 = new CodeMemberMethod();
                                member2.Attributes = MemberAttributes.Private;
                                member2.Name = $"_{entry.name}";
                                bool isNotPrim = false;
                                var typeNameForGS = entry.type;
                                switch (entry.type)
                                {
                                    default:
                                        member.Statements.Add(GenerateSubstruct(entry));
                                        member.ReturnType = new CodeTypeReference($"Wrapper{entry.type}");
                                        isNotPrim = true;
                                        typeNameForGS = $"Wrapper{typeNameForGS}";
                                        break;
                                    case "u8":
                                        member.Statements.Add(GenerateGetter(entry, "ReadU8"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteU8"));
                                        break;
                                    case "u8[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadU8"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteU8"));
                                        break;
                                    case "u16":
                                        member.Statements.Add(GenerateGetter(entry, "ReadU16"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteU16"));
                                        break;
                                    case "u16[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadU16"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteU16"));
                                        break;
                                    case "u32":
                                        member.Statements.Add(GenerateGetter(entry, "ReadU32"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteU32"));
                                        break;
                                    case "u32[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadU32"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteU32"));
                                        break;
                                    case "s8":
                                        member.Statements.Add(GenerateGetter(entry, "ReadS8"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteS8"));
                                        break;
                                    case "s8[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadS8"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteS8"));
                                        break;
                                    case "s16":
                                        member.Statements.Add(GenerateGetter(entry, "ReadS16"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteS16"));
                                        break;
                                    case "s16[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadS16"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteS16"));
                                        break;
                                    case "s32":
                                        member.Statements.Add(GenerateGetter(entry, "ReadS32"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteS32"));
                                        break;
                                    case "s32[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadS32"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteS32"));
                                        break;
                                    case "f16":
                                        member.Statements.Add(GenerateGetter(entry, "ReadF16"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteF16"));
                                        break;
                                    case "f16[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadF16"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteF16"));
                                        break;
                                    case "f32":
                                        member.Statements.Add(GenerateGetter(entry, "ReadF32"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteF32"));
                                        break;
                                    case "f32[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadF32"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteF32"));
                                        break;
                                    case "char":
                                        member.Statements.Add(GenerateGetter(entry, "ReadU8"));
                                        member2.Statements.Add(GenerateSetter(entry, "WriteU8"));
                                        break;
                                    case "char[]":
                                        member.Statements.Add(GenerateGetterLoop(entry, "ReadU8"));
                                        member2.Statements.Add(GenerateSetterLoop(entry, "WriteU8"));
                                        break;
                                }
                                if (entry.arrayCount != "0")
                                {
                                    var comment = new CodeCommentStatement($"#ARRCOUNT {entry.arrayCount}");
                                    member.Comments.Add(comment);
                                }
                                if (entry.comment != string.Empty)
                                {
                                    member.Comments.Add(new CodeCommentStatement(entry.comment));
                                }

                                typeDeclaration.Members.Add(member);
                                if (!isNotPrim)
                                {
                                    member2.Parameters.Add(new CodeParameterDeclarationExpression(entry.type, "value"));
                                }
                                else
                                {
                                    member2.Parameters.Add(new CodeParameterDeclarationExpression(typeNameForGS, "value"));
                                }
                                typeDeclaration.Members.Add(member2);
                                var gsField = new CodeMemberField();
                                gsField.Name = $"{entry.name} {{get => this._{entry.name}(); set => this._{entry.name}(value);}}//";
                                gsField.Type = new CodeTypeReference(typeNameForGS);
                                gsField.Attributes = MemberAttributes.Public;
                                typeDeclaration.Members.Add(gsField);
                            }

                            CSharpCodeProvider provider = new CSharpCodeProvider();
                            string sourceFile = $"../../../../ModLoader.OoT.API/Wrappers/Wrapper{parseOutSyntax}.cs";

                            using (StreamWriter sw = new StreamWriter(sourceFile, false))
                            {
                                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

                                provider.GenerateCodeFromCompileUnit(compileUnit, tw,
                                    new CodeGeneratorOptions());

                                tw.Close();

                                // Specific bs patches.
                                var p = File.ReadAllLines(sourceFile);
                                for (int i2 = 0; i2 < p.Length; i2++)
                                {
                                    if (p[i2].Contains("RESPAWN_MODE_MAX"))
                                    {
                                        p[i2] = p[i2].Replace("RESPAWN_MODE_MAX", "(int)RespawnMode.RESPAWN_MODE_MAX");
                                    }else if (p[i2].Contains("TIMER_ID_MAX"))
                                    {
                                        p[i2] = p[i2].Replace("TIMER_ID_MAX", "(int)TimerId.TIMER_ID_MAX");
                                    }
                                }
                                File.WriteAllLines(sourceFile, p);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
