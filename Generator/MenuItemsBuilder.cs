using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator
{
    internal class MenuItemsBuilder : Builder
    {
        internal override string Type => "MenuItems";

        internal override void GenerateCodeBehind(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder)
        {
            var identifierOfClass = classDeclaration.Identifier.Value.ToString();

            builder.AppendLine($"<MudNavLink Href=\"{identifierOfClass.ToLowerInvariant()}\" Match=\"NavLinkMatch.Prefix\" Icon=\"@Icons.Material.Filled.List\">{identifierOfClass}</MudNavLink>");
            builder.AppendLine($"<MudNavLink Href=\"create{identifierOfClass.ToLowerInvariant()}\" Match=\"NavLinkMatch.Prefix\" Icon=\"@Icons.Material.Filled.List\">Create {identifierOfClass}</MudNavLink>");
        }

        internal override void GenerateRazor(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder)
        {
            // We do not need a code behind
        }
    }
}
