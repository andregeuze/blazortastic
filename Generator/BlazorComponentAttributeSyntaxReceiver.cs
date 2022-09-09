using GeneratorHelper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator
{
    public class BlazorComponentAttributeSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> ClassesWithBlazorComponentAttribute { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.ClassDeclaration))
            {
                var classDeclarationSyntax = syntaxNode as ClassDeclarationSyntax;

                if (classDeclarationSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(x => x.Name.ToString() == nameof(BlazorComponentAttribute)))
                {
                    ClassesWithBlazorComponentAttribute.Add(classDeclarationSyntax);
                }
            }
        }
    }
}
