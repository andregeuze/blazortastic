using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;

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
            GenerateRazorComponent();
        }

        void GenerateRazorComponent()
        {
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

        }
    }
}
