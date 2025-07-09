using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace task11;

public class Calculator
{
    public static dynamic test()
    {
        string Code = @"
                public class Calculator
                {
                    public int Add(int a, int b) => a + b;
                    public int Minus(int a, int b) => a - b;
                    public int Mul(int a, int b) => a * b;
                    public int Div(int a, int b) => a / b;
                }";

        var compilation = CSharpCompilation.Create(
            "DynamicCalculator",
            [CSharpSyntaxTree.ParseText(Code)],
            [
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Console).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        var emitResult = compilation.Emit(ms);

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        Type? calculatorType = assembly.GetType("Calculator");

        var a = Activator.CreateInstance(calculatorType!);

        return a!;
    }
}
