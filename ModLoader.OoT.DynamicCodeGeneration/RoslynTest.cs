using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;
using NUnit.Framework;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ModLoader.OoT.DynamicCodeGeneration;

public class RoslynTest
{

    public static void test()
    {
        var c = ClassDeclaration("Fuck").AddModifiers(Token(SyntaxKind.PublicKeyword));
        var ns = NamespaceDeclaration(IdentifierName("ModLoader.OoT.API")).AddMembers(c);
        var cu = CompilationUnit().AddMembers(ns);
        var formattedNode = Formatter.Format(cu, new AdhocWorkspace());
        var sb = new StringBuilder();
        using (var writer = new StringWriter(sb))
        {
            formattedNode.WriteTo(writer);
        }
        var text = sb.ToString();
        Console.WriteLine(text);
    }

}
