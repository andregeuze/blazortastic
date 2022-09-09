using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator
{
    internal class CreateBuilder : Builder
    {
        internal override string Type => "Create";

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
                .AppendLine($"using Microsoft.AspNetCore.Components;").AppendLine("using Microsoft.AspNetCore.Components.Forms;").AppendLine()
                .AppendLine($"namespace {namespaceDeclarationSyntax.Name};").AppendLine()
                .AppendLine($"public partial class {identifierOfClass}Component_{Type}_g")
                .AppendLine("{");
            using (builder.Indent())
            {
                builder.AppendLine($"[Inject] ICrudService<{identifierOfClass}> Service {{ get; set; }}");

                builder.AppendLine($"{identifierOfClass} model = new {identifierOfClass}();");
                builder.AppendLine("bool success;");

                builder.AppendLine("private async Task OnValidSubmit(EditContext context)")
                    .AppendLine("{");

                using (builder.Indent())
                {
                    builder.AppendLine("success = true;");
                    builder.AppendLine("await Service.Post(model);");
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
                .AppendLine($"@page \"/create{identifierOfClass.ToString().ToLowerInvariant()}\"")
                .AppendLine($"@using {namespaceDeclarationSyntax.Name}")
                .AppendLine($"@using GeneratorHelper")
                .AppendLine()
                .AppendLine($"<PageTitle>Create {identifierOfClass}</PageTitle>")
                .AppendLine("<EditForm Model=\"@model\" OnValidSubmit=\"OnValidSubmit\">");
            using (builder.Indent())
            {

                builder
                    .AppendLine("<MudGrid>");

                using (builder.Indent())
                {
                    builder.AppendLine("<MudItem xs=\"12\" sm=\"7\">");
                    using (builder.Indent())
                    {
                        builder.AppendLine("<MudCard>");

                        using (builder.Indent())
                        {
                            builder.AppendLine("<MudCardContent>");

                            using (builder.Indent())
                            {
                                var properties = classDeclaration.Members
                                    .Where(x => x.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.PropertyDeclaration))
                                    .Cast<PropertyDeclarationSyntax>()
                                    .ToList();

                                foreach (var prop in properties)
                                {
                                    builder.AppendLine($"<MudTextField T=\"string\" Label=\"{prop.Identifier.Text}\" @bind-Value=\"model.{prop.Identifier.Text}\" />");
                                }
                            }

                            builder.AppendLine("</MudCardContent>");

                            builder.AppendLine("<MudCardActions>");

                            using (builder.Indent())
                            {
                                builder.AppendLine("<MudButton ButtonType=\"ButtonType.Submit\" Variant=\"Variant.Filled\" Color=\"Color.Primary\" Class=\"ml-auto\">Register</MudButton>");
                            }

                            builder.AppendLine("</MudCardActions>");
                        }

                        builder.AppendLine("</MudCard>");
                    }
                    builder.AppendLine("</MudItem>");
                    builder.AppendLine("<MudItem xs=\"12\" sm=\"5\">");

                    using (builder.Indent())
                    {
                        builder.AppendLine("<MudPaper Class=\"pa-4 mud-height-full\">");

                        using (builder.Indent())
                        {
                            builder.AppendLine("@if (success)").AppendLine("{");

                            using (builder.Indent())
                            {
                                builder.AppendLine("<MudText Color=\"Color.Success\">Success</MudText>");
                            }

                            builder.AppendLine("}");
                        }

                        builder.AppendLine("</MudPaper>");
                    }

                    builder.AppendLine("</MudItem>");
                }

                builder
                    .AppendLine("</MudGrid>");
            }
            builder.AppendLine("</EditForm>");
        }
    }
}
