# CodeGenerationExample

This is the simplest 'Hello World' example of a code generator for csharp

## Running

Run the example with:

```csharp
 dotnet run --project .\Consumer\Consumer.csproj
```

Expected Output:
```
B is:Blah.Hello
```

## Understanding Code

There are 2 csproj files.

The Generator uses code gen to create the `Hello` class in the `Blah` namespace

The consumer references the Generator csproj file and can `new` the dynamically generated class.

## Important Gotchas
Getting analyzers to work properly involves many gotchas:

- Generator must target `netstandard2.0`
- Generator must have `<LangVersion>Latest</LangVersion>`
- Generator must have `<EnforceExtendedAnalyzerRules>true</EnforceExtendedAnalyzerRules>`
- Consumer project reference must have the additional XML attribute `OutputItemType="Analyzer"`
