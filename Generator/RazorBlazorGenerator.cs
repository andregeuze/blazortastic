using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Generator
{
    [Generator]
    public class RazorBlazorGenerator : ISourceGenerator
    {
        private List<Builder> _builders = new List<Builder>();

        public void Execute(GeneratorExecutionContext context)
        {
            // Generating C# works:
            //context.AddSource("Test.g.cs", "namespace Test { public class Test { } }");

            // Now let's try Razor:
            //context.AddSource("TestComponent.razor", "<p>Hello Source Generated World!</p>");
            // failed.. :Only one compilation unit can have top-level statements.

            // But what if we write to a file DIRECTLY
            var receiver = (BlazorComponentAttributeSyntaxReceiver)context.SyntaxReceiver;

            GenerateRazorComponent(receiver);
        }

        void GenerateRazorComponent(BlazorComponentAttributeSyntaxReceiver syntaxReceiver)
        {
            var classesWithAttribute = syntaxReceiver.ClassesWithBlazorComponentAttribute;

            foreach (var item in classesWithAttribute)
            {
                foreach (var builder in _builders)
                {
                    var generatedCode = builder.GetCode(item);

                    File.WriteAllText(generatedCode.page.Key, generatedCode.page.Value);
                    File.WriteAllText(generatedCode.code.Key, generatedCode.code.Value);
                }
            }

            // Generate the menu items for the classes
            GenerateMenu(classesWithAttribute);
        }
        internal void GenerateMenu(List<ClassDeclarationSyntax> classDeclarationSyntax)
        {
            var path = Path.GetDirectoryName(classDeclarationSyntax.First().GetLocation().SourceTree.FilePath);

            var razorBuilder = new IndentedStringBuilder();
            var menuBuilder = new MenuItemsBuilder();
            foreach (var item in classDeclarationSyntax)
            {
                menuBuilder.GenerateCodeBehind(item, razorBuilder);
            }

            File.WriteAllText(Path.Combine(path, "MenuItems.g.razor"), razorBuilder.ToString());
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new BlazorComponentAttributeSyntaxReceiver());
            _builders.Add(new OverviewPageBuilder());
            _builders.Add(new CreateBuilder());
        }
    }
}
