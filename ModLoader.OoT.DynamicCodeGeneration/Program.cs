using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;

namespace ModLoader.OoT.DynamicCodeGeneration;

public class EnumDataStore
{
    public string name;
    public string comment = "";

    public EnumDataStore(string name)
    {
        this.name = name.Replace(",", "");
        if (this.name.Contains("//")){
            this.comment = this.name.Split("//")[1].Trim();
            this.name = this.name.Split("//")[0].Trim();
        }else if (this.name.Contains(" "))
        {
            // If there is whitespace there was probably a comment block that got mangled.
            this.comment = this.name.Split(" ")[0];
            this.name = this.name.Split(" ")[1];
        }
    }
}

public class StructDataStore
{
    public string name;
    public string type;
    public string comment = "";
    public bool isArray;
    public string arrayCount = "0";
    public string offset = "0x0";
    public string size = "0x0";

    public StructDataStore(string name, string type, string offset)
    {
        this.name = name.Replace(";", "");
        this.type = type.Replace(";", "");
        this.offset = offset.Replace(";", "");

        if (this.name.Contains("//"))
        {
            this.comment = this.name.Split("//")[1].Trim();
            this.name = this.name.Split("//")[0].Trim();
        }

        if (this.name.Contains("[") && this.name.Contains("]"))
        {
            this.isArray = true;
            var temp = this.name.Replace("]", "").Split("[")[1].Trim();
            arrayCount = temp;
            this.name = this.name.Replace("]", "").Split("[")[0].Trim();
        }
        else
        {
            this.isArray = false;
        }
    }
}

public static class Program
{

    private static string[] primitiveTypes = new string[] { "u8", "u16", "u32", "s8", "s16", "s32", "f16", "f32", "char", "@char" };

    public static void Main(string[] args)
    {
        //ConvertSaveStructEnums();
        //ConvertSaveStruct_Structs();

        WrapperGen.GenerateWrapperFromFile();

        //RoslynTest.test();
    }

    private static void ConvertSaveStruct_Structs()
    {
        var decomp_path = Path.GetFullPath("../../../../oot");
        var include_path = Path.GetFullPath(Path.Join(decomp_path, "include"));
        var z64save_h = Path.GetFullPath(Path.Join(include_path, "z64save.h"));

        var lines = File.ReadAllLines(z64save_h);
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            if (line.Contains("typedef struct"))
            {
                Console.WriteLine("Found a struct");
                CodeCompileUnit compileUnit = new CodeCompileUnit();
                CodeNamespace samples = new CodeNamespace("ModLoader.OoT.API");
                compileUnit.Namespaces.Add(samples);
                CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration();
                typeDeclaration.Attributes = MemberAttributes.Public;
                typeDeclaration.IsStruct = true;
                CodeAttributeDeclaration attr = new CodeAttributeDeclaration(new CodeTypeReference(typeof(StructLayoutAttribute)));
                attr.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(2)));
                attr.Arguments.Add(new CodeAttributeArgument("Pack", new CodePrimitiveExpression(1)));
                typeDeclaration.CustomAttributes.Add(attr);
                samples.Types.Add(typeDeclaration);
                List<StructDataStore> data = new List<StructDataStore>();

                List<string> knownTypes = new List<string>();

                for (int j = i + 1; j < lines.Length; j++)
                {
                    var lookahead = lines[j];
                    // This logic fails on unions or structs declarations inside other structs.
                    if (!lookahead.Contains("}") && lookahead.Contains(";"))
                    {
                        // 0 = Offset, 1 = declaration
                        var parseOutComment = lookahead.Replace("/*", "").Split("*/");
                        var offset = parseOutComment[0].Trim();
                        var dec = parseOutComment[1].Trim();
                        // 0 = type, 1 = name and maybe comment.
                        Console.WriteLine(dec);
                        var split = dec.Split(" ");
                        data.Add(new StructDataStore(split[1], split[0], offset));
                    }
                    else
                    {
                        var parseOutSyntax = lookahead.Replace(";", "").Replace("}", "").Split("//")[0].Trim();
                        typeDeclaration.Name = parseOutSyntax;
                        foreach (var entry in data)
                        {
                            knownTypes.Add(entry.type);
                            var member = new CodeMemberField(entry.type, entry.name);
                            member.Attributes = MemberAttributes.Public;
                            CodeAttributeDeclaration offset_attr = new CodeAttributeDeclaration(new CodeTypeReference(typeof(FieldOffsetAttribute)));
                            offset_attr.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(Convert.ToInt32(entry.offset, 16))));
                            member.CustomAttributes.Add(offset_attr);
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
                        }

                        CSharpCodeProvider provider = new CSharpCodeProvider();
                        string sourceFile = $"../../../../ModLoader.OoT.API/Structs/{parseOutSyntax}.cs";

                        using (StreamWriter sw = new StreamWriter(sourceFile, false))
                        {
                            IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

                            provider.GenerateCodeFromCompileUnit(compileUnit, tw,
                                new CodeGeneratorOptions());

                            tw.Close();

                            var postprocesslines = File.ReadAllLines(sourceFile);
                            for (int q = 0; q < postprocesslines.Length; q++)
                            {
                                var postprocessline = postprocesslines[q];
                                if (postprocessline.Contains("public struct"))
                                {
                                    postprocessline = postprocessline.Replace("public struct", "public unsafe struct");
                                    postprocesslines[q] = postprocessline;
                                }
                                else if (postprocessline.Contains("#ARRCOUNT"))
                                {
                                    int z = q + 2;
                                    var zline = postprocesslines[z];
                                    Console.WriteLine("found a fucking arrcount");
                                    var zsplit = postprocessline.Trim().Split(" ")[2].Trim();
                                    var typesplit = zline.Trim().Split(" ")[1].Trim();
                                    zline = zline.Replace("public", $"[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1, SizeConst = {zsplit})] public");
                                    zline = zline.Replace(typesplit, $"{typesplit}[]");
                                    postprocesslines[z] = zline;
                                }
                                else if (postprocessline.Contains("[System.Runtime.InteropServices.StructLayoutAttribute(2"))
                                {
                                    postprocessline = postprocessline.Replace("[System.Runtime.InteropServices.StructLayoutAttribute(2", "[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Explicit");
                                    postprocesslines[q] = postprocessline;
                                }

                                if (postprocessline.Contains("RESPAWN_MODE_MAX"))
                                {
                                    postprocessline = postprocessline.Replace("RESPAWN_MODE_MAX", "(int)RespawnMode.RESPAWN_MODE_MAX");
                                    postprocesslines[q] = postprocessline;
                                }else if (postprocessline.Contains("TIMER_ID_MAX"))
                                {
                                    postprocessline = postprocessline.Replace("TIMER_ID_MAX", "(int)TimerId.TIMER_ID_MAX");
                                    postprocesslines[q] = postprocessline;
                                }
                            }
                            File.WriteAllLines(sourceFile, postprocesslines);
                        }

                        break;
                    }
                }
            }
        }
    }

    private static void ConvertSaveStructEnums() {
        var decomp_path = Path.GetFullPath("../../../../oot");
        var include_path = Path.GetFullPath(Path.Join(decomp_path, "include"));
        var z64save_h = Path.GetFullPath(Path.Join(include_path, "z64save.h"));

        var lines = File.ReadAllLines(z64save_h);
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            //Console.WriteLine(i);
            if (line.Contains("typedef enum"))
            {
                Console.WriteLine("Found an enum");
                CodeCompileUnit compileUnit = new CodeCompileUnit();
                CodeNamespace samples = new CodeNamespace("ModLoader.OoT.API");
                compileUnit.Namespaces.Add(samples);
                CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration();
                typeDeclaration.Attributes = MemberAttributes.Public;
                typeDeclaration.IsEnum = true;
                samples.Types.Add(typeDeclaration);
                List<EnumDataStore> data = new List<EnumDataStore>();

                for (int j = i + 1; j < lines.Length; j++)
                {
                    var lookahead = lines[j];
                    if (!lookahead.Contains(";"))
                    {
                        //Console.WriteLine(lookahead);
                        // 0 = The enum value, 1 = Name
                        Console.WriteLine(lookahead);
                        var parseOutComment = lookahead.Replace("/*", "").Split("*/");
                        data.Add(new EnumDataStore(parseOutComment[1]));
                    }
                    else
                    {
                        var parseOutSyntax = lookahead.Replace(";", "").Replace("}", "").Trim();
                        typeDeclaration.Name = parseOutSyntax;
                        foreach (var entry in data)
                        {
                            var member = new CodeMemberField(parseOutSyntax, entry.name);
                            member.Attributes = MemberAttributes.Public;
                            if (entry.comment != string.Empty)
                            {
                                member.Comments.Add(new CodeCommentStatement(entry.comment));
                            }
                            
                            typeDeclaration.Members.Add(member);
                        }

                        CSharpCodeProvider provider = new CSharpCodeProvider();
                        string sourceFile = $"../../../../ModLoader.OoT.API/Enums/{parseOutSyntax}.cs";

                        using (StreamWriter sw = new StreamWriter(sourceFile, false))
                        {
                            IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");

                            provider.GenerateCodeFromCompileUnit(compileUnit, tw,
                                new CodeGeneratorOptions());

                            tw.Close();
                        }

                        break;
                    }
                }
            }
        }
    }
}