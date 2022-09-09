using Microsoft.CodeAnalysis;
using System;

namespace Generator
{
    [Generator]
    public class RazorBlazorGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("Test.g.cs", "namespace Test { public class Test { } }");
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            
        }
    }
}
