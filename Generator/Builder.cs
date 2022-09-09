using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;

namespace Generator
{
    internal abstract class Builder
    {
        internal (KeyValuePair<string, string> page, KeyValuePair<string, string> code) GetCode(ClassDeclarationSyntax classDeclarationSyntax)
        {
            var pageBuilder = new IndentedStringBuilder();
            var codeBuilder = new IndentedStringBuilder();

            var path = Path.GetDirectoryName(classDeclarationSyntax.GetLocation().SourceTree.FilePath);
            var identifierOfClass = classDeclarationSyntax.Identifier.Value;

            GenerateCodeBehind(classDeclarationSyntax, codeBuilder);
            GenerateRazor(classDeclarationSyntax, pageBuilder);

            return
            (
                new KeyValuePair<string, string>(Path.Combine(path, $"{identifierOfClass}Component.{Type}.g.razor.cs"), codeBuilder.ToString()),
                new KeyValuePair<string, string>(Path.Combine(path, $"{identifierOfClass}Component.{Type}.g.razor"), pageBuilder.ToString())
            );
        }

        internal abstract void GenerateCodeBehind(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder);

        internal abstract void GenerateRazor(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder);

        internal abstract string Type { get; }
    }
}