using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Threading.Tasks;

namespace Generator
{
    internal class OverviewPageBuilder : Builder
    {
        internal override string Type => "Overview";
        
        internal override void GenerateCodeBehind(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder)
        {
                NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
                if (!SyntaxNodeHelper.TryGetParentSyntax(classDeclaration, out namespaceDeclarationSyntax))
                {
                    return; // or whatever you want to do in this scenario
                }

                var identifierOfClass = classDeclaration.Identifier.Value;

                builder
                    .AppendLine($"using GeneratorHelper;")
                    .AppendLine($"using Microsoft.AspNetCore.Components;").AppendLine()
                    .AppendLine($"namespace {namespaceDeclarationSyntax.Name};").AppendLine()
                    .AppendLine($"public partial class {identifierOfClass}Component_{Type}_g")
                    .AppendLine("{");

            using (builder.Indent())
            {
                builder.AppendLine($"[Inject] ICrudService<{identifierOfClass}> Service {{ get; set; }}");

                builder.AppendLine($"private {identifierOfClass}[]? {GetListName(identifierOfClass)} {{ get; set; }}");

                builder
                    .AppendLine("protected override async Task OnInitializedAsync()")
                    .AppendLine("{");

                using (builder.Indent())
                {
                    builder.AppendLine($"{GetListName(identifierOfClass)} = await Service.GetAsync();");
                }

                builder.AppendLine("}");
            }

            builder.AppendLine("}");

        }

        internal override void GenerateRazor(ClassDeclarationSyntax classDeclaration, IndentedStringBuilder builder)
        {
            NamespaceDeclarationSyntax namespaceDeclarationSyntax = null;
            if (!SyntaxNodeHelper.TryGetParentSyntax(classDeclaration, out namespaceDeclarationSyntax))
            {
                return; // or whatever you want to do in this scenario
            }
            var identifierOfClass = classDeclaration.Identifier.Value;
            builder
                .AppendLine($"@page \"/{identifierOfClass}\"")
                .AppendLine($"@using {namespaceDeclarationSyntax.Name}")
                .AppendLine($"@using GeneratorHelper")
                .AppendLine()
                .AppendLine($"<PageTitle>{identifierOfClass}</PageTitle>")
                .AppendLine($"<MudText Typo=\"Typo.h3\" GutterBottom=\"true\">{identifierOfClass}</MudText>")
                .AppendLine($"@if({GetListName(identifierOfClass)} == null)")
                .AppendLine("{");

            using (builder.Indent())
            {
                builder.AppendLine("<MudProgressCircular Color = \"Color.Default\" Indeterminate = \"true\" />");
            }

            builder
                .AppendLine("}")
                .AppendLine("else")
                .AppendLine("{");

            using (builder.Indent())
            {
                builder.AppendLine($"<MudTable Items=\"{GetListName(identifierOfClass)}\" Hover=\"true\" Elevation=\"0\">");
                using (builder.Indent())
                {
                    var properties = classDeclaration.Members
                        .Where(x => x.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.PropertyDeclaration))
                        .Cast<PropertyDeclarationSyntax>()
                        .ToList();
                    builder.AppendLine("<HeaderContent>");
                    using (builder.Indent())
                    {
                        foreach (var property in properties)
                        {
                            builder.AppendLine($"<MudTh>{property.Identifier.Text}</MudTh>");
                        }
                    }
                    builder
                        .AppendLine("</HeaderContent>")
                        .AppendLine("<RowTemplate>");

                    using (builder.Indent())
                    {
                        foreach (var property in properties)
                        {
                            builder.AppendLine($"<MudTd DataLabel=\"{property.Identifier.Text}\">@context.{property.Identifier.Text}</MudTd>");
                        }
                    }

                    builder
                        .AppendLine("</RowTemplate>");
                }
                builder.AppendLine("</MudTable>");
            }

            builder.Append("}");
        }

        private static string GetListName(object identifierOfClass)
        {
            return $"{identifierOfClass}List";
        }
    }
}
