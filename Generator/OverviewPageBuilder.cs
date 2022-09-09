using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace Generator
{
    internal class OverviewPageBuilder : Builder
    {
        internal override string Type => "Overview";

        internal override void GenerateRazor(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder)
        {
            var identifierOfClass = classDeclaration.Identifier.Value;
            builder
                .AppendLine("namespace Generated;")
                .AppendLine($"public partial class {identifierOfClass}Component")
                .AppendLine("{")
                .AppendLine("}");
        }

        internal override void GenerateCodeBehind(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder)
        {
            builder.AppendLine("<p>Hello Source Generated World!</p>");
        }
    }
}
