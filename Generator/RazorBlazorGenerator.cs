using Microsoft.CodeAnalysis;
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
            var directory = "Generated";
            //Directory.CreateDirectory(directory); // Doesn't work, surprise.
            File.WriteAllText($"{directory}/TestComponent.razor", "<p>Hello Source Generated World!</p>");
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
