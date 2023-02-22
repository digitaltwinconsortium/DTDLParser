# Resolve external references asynchronously

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial illustrates how to deal with models that are not self-contained.
A [synchronous version](./Tutorial11_ResolveExternalReferences.md) of this tutorial is also available.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial10Async_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model that references another model

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an asynchronous enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model contains an external reference.

```C# Snippet:DtdlParserTutorial10Async_ObtainDtdlText
string jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""extends"": ""dtmi:example:anotherInterface;1"",
  ""contents"": [
    {
      ""@type"": ""Property"",
      ""name"": ""expectedDistance"",
      ""schema"": ""double""
    }
  ]
}";
```

The Interface's "extends" property has value "dtmi:example:anotherInterface;1", which is an identifier that is not defined in the model.
The parser is unable to fully validate the model without a definition for this referenced Interface.

## Submit the JSON text to the ModelParser

The main asynchronous method on the `ModelParser` is `ParseAsync()`.
One argument is required, which can be either a string or an asynchronous enumeration of strings containing the JSON text to parse as DTDL.

```C# Snippet:DtdlParserTutorial10Async_CallParseAsync
var parseTask = modelParser.ParseAsync(jsonText);
```

The return value is a `Task`, whose completion must be awaited before proceeding.
If the submitted model is invalid or incomplete, an exception will be thrown, wrapped in an `AggregateException` by the `System.Threading.Tasks` framework.
If the submitted model is invalid, the wrapped exception will be a `ParsingException`.
If the submitted model is referentially incomplete, the wrapped exception will be a `ResolutionException`.
If no exception is thrown, the model is valid.

```C# Snippet:DtdlParserTutorial10Async_CallWait
try
{
    parseTask.Wait();
    Console.WriteLine($"DTDL model is valid!");
}
catch (AggregateException ex)
{
    if (ex.InnerException is ResolutionException)
    {
        Console.WriteLine($"DTDL model is referentially incomplete: {ex.InnerException}");
    }
    else if (ex.InnerException is ParsingException)
    {
        Console.WriteLine($"DTDL model is invalid: {ex.InnerException}");
    }
    else
    {
        throw;
    }
}
```

## Observe resolution exception

For the JSON text above, the code snippet above will display:

```Console
DTDL model is referentially incomplete: No DtmiResolverAsync provided to resolve requisite reference(s): dtmi:example:anotherInterface;1 (referenced in 1 place)
```

This exception indicates two things:
First, the submitted model contains a reference to dtmi:example:anotherInterface;1 that requires a definition.
Second, the caller has not registered a `DtmiResolverAsync` callback to provide definitions for external references.

The asynchronous `ParseAsync()` method expects a `DtmiResolverAsync` callback for resolving undefined identifiers to an asynchronous enumeration of JSON text strings.

## Obtain the JSON text of referenced DTDL model

We will store the JSON text of the referenced model in a dictionary keyed on the identifier.

```C# Snippet:DtdlParserTutorial10Async_ObtainReferencedDtdlText
var otherJsonTexts = new Dictionary<Dtmi, string>();

otherJsonTexts[new Dtmi("dtmi:example:anotherInterface;1")] =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anotherInterface;1"",
  ""@type"": ""Interface"",
  ""contents"": [
    {
      ""@type"": ""Telemetry"",
      ""name"": ""currentDistance"",
      ""schema"": ""double""
    }
  ]
}";
```

## Register a DtmiResolverAsync with the ModelParser

A `DtmiResolverAsync` is a delegate that the `ModelParser` calls whenever it encounters an external reference to an identifier that requires a definition.
We can write a simple resolver and register it with the parser in the constructor, using a local function to generate an asynchronous enumeration:

```C# Snippet:DtdlParserTutorial10Async_NewParserRegisterDtmiResolverAsync
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
async IAsyncEnumerable<string> GetJsonTexts(IReadOnlyCollection<Dtmi> dtmis, Dictionary<Dtmi, string> jsonTexts)
{
    foreach (Dtmi dtmi in dtmis)
    {
        if (jsonTexts.TryGetValue(dtmi, out string refJsonText))
        {
            yield return refJsonText;
        }
    }
}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

modelParser = new ModelParser(new ParsingOptions { DtmiResolverAsync = (IReadOnlyCollection<Dtmi> dtmis, CancellationToken _) =>
{
    return GetJsonTexts(dtmis, otherJsonTexts);
}});
```

## Resubmit the original JSON text to the ModelParser

With the `DtmiResolverAsync` registered, when we call `ParseAsync()` again:

```C# Snippet:DtdlParserTutorial10Async_RepeatCallParseAsync
parseTask = modelParser.ParseAsync(jsonText);
```

[repeat]: # (Snippet:DtdlParserTutorial10Async_CallWait)

And then wait on the `Task` per the code snippet above, it displays:

```Console
DTDL model is valid!
```
