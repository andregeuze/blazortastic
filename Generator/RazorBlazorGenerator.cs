using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;

namespace Generator
{
    [Generator]
    public class RazorBlazorGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Generating C# works:
            //context.AddSource("Test.g.cs", "namespace Test { public class Test { } }");

            // Now let's try Razor:
            //context.AddSource("TestComponent.razor", "<p>Hello Source Generated World!</p>");
            // failed..

            // But what if we write to a file DIRECTLY
            var receiver = (BlazorComponentAttributeSyntaxReceiver)context.SyntaxReceiver;

            GenerateRazorComponent(receiver);
        }

        void GenerateRazorComponent(BlazorComponentAttributeSyntaxReceiver syntaxReceiver)
        {
            var classesWithAttribute = syntaxReceiver.ClassesWithBlazorComponentAttribute;
            
            var directory = "Generated";
            //Directory.CreateDirectory(directory); // Doesn't work, surprise surprise.
            File.WriteAllText($"{directory}/TestComponent.razor", "<p>Hello Source Generated World!</p>");

            // Generate the Code behind of the component
            var sourceText = new IndentedStringBuilder();
            sourceText
                .AppendLine($"namespace {directory};").AppendLine()
                .AppendLine("public partial class TestComponent {").AppendLine()
                .AppendLine("}");

            File.WriteAllText($"{directory}/TestComponent.razor.cs", sourceText.ToString());

        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new BlazorComponentAttributeSyntaxReceiver());
        }
    }
}
