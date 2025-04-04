
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator;

[Generator]
public class TheGenerator : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Report a diagnostic message during the initialization phase.
        var diagnostic = Diagnostic.Create(
                                            new DiagnosticDescriptor(
                                                                      "GEN001",  // Unique ID for the diagnostic
                                                                      "Generator Initialization",  // Title of the diagnostic
                                                                      "The generator was initialized",  // Message format
                                                                      "Source Generator",  // Category of the diagnostic
                                                                      DiagnosticSeverity.Info,  // Severity level
                                                                      true),  // Is this a user visible diagnostic
                                            Location.None);  // No specific location, report at global level

        var provider = context.SyntaxProvider.CreateSyntaxProvider(

                                                     predicate: static (node, _) => node is ClassDeclarationSyntax,
                                                     transform: static (ctx, _) =>
                                                                   (ClassDeclarationSyntax)ctx.Node)
               .Where(m => m is { });

        var compilation = context.CompilationProvider.Combine(provider.Collect());
        context.RegisterSourceOutput(compilation, Execute);
    }

    private void Execute(SourceProductionContext context, (Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right) arg2)
    {
        var theCode = @"
namespace Blah;

public class Hello
{
}
";

        context.AddSource("YourClass.g.cs", theCode);

        context.ReportDiagnostic(Diagnostic.Create(
                                                     new DiagnosticDescriptor(
                                                                               "GEN001",
                                                                               "Source Generator",
                                                                               "Generated code: {0}",
                                                                               "Source Generator",
                                                                               DiagnosticSeverity.Info,
                                                                               true),
                                                     Location.None, theCode));
    }
}
