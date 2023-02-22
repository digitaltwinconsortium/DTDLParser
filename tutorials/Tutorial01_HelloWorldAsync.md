# Determine whether a DTDL model is valid asynchronously

This tutorial demonstrates basic interaction with this library's core class, `ModelParser`.
This class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial walks through the first of these uses: determining whether a model is valid.
A [synchronous version](./Tutorial01_HelloWorld.md) of this tutorial is also available.
More detailed uses are covered in further tutorials.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial01Async_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model

The DTDL language is syntactically JSON.
The `ModelParser` expects a string value, which is JSON text of a DTDL model.

```C# Snippet:DtdlParserTutorial01Async_ObtainOneDtdlText
string jsonText1 =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
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

## Submit the JSON text to the ModelParser

The main asynchronous method on the `ModelParser` is `ParseAsync()`.
One argument is required: a string containing the JSON text to parse as DTDL.

```C# Snippet:DtdlParserTutorial01Async_CallParseAsyncOnJsonObject
var parseTask = modelParser.ParseAsync(jsonText1);
```

The return value is a `Task`, whose completion must be awaited before proceeding.
If the submitted model is invalid or incomplete, an exception will be thrown, wrapped in an `AggregateException` by the `System.Threading.Tasks` framework.
If the submitted model is invalid, the wrapped exception will be a `ParsingException`.
If the submitted model is referentially incomplete, the wrapped exception will be a `ResolutionException`.
If no exception is thrown, the model is valid.

```C# Snippet:DtdlParserTutorial01Async_CallWait
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

The JSON text above is valid DTDL, so the code snippet above will display:

```Console
DTDL model is valid!
```

## Obtain the JSON text of a DTDL model containing multiple Interface definitions

The JSON text of a DTDL model can contain a JSON array with multiple Interface definitions as elements of the array.

```C# Snippet:DtdlParserTutorial01Async_ObtainAnotherDtdlText
string jsonText2 =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anotherInterface;1"",
  ""@type"": ""Interface"",
  ""contents"": [
    {
      ""@type"": ""Property"",
      ""name"": ""expectedDistance"",
      ""schema"": ""double"",
      ""writable"": true
    }
  ]
}";

string jsonText = $"[ {jsonText1}, {jsonText2} ]";
```

## Submit the combined JSON text to the ModelParser

```C# Snippet:DtdlParserTutorial01Async_CallParseAsyncOnJsonArray
parseTask = modelParser.ParseAsync(jsonText);
```

[repeat]: # (Snippet:DtdlParserTutorial01Async_CallWait)

The JSON text above, which contains two Interface definitions, is valid DTDL.
After waiting on the `Task` per the code snippet above, it displays:

```Console
DTDL model is valid!
```

## Submit an asynchronous enumeration of JSON text strings

The `ModelParser` also expects an asynchronous enumeration of strings, each of which is JSON text of a DTDL model.
We can generate an asynchronous enumeration using a local function.

```C# Snippet:DtdlParserTutorial01Async_ConstructAsyncEnumeration
string[] jsonTexts = { jsonText1, jsonText2 };

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
async IAsyncEnumerable<string> GetJsonTexts(string[] jsonTexts)
{
    foreach (string jsonText in jsonTexts)
    {
        yield return jsonText;
    }
}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
```

The asynchronous `ParseAsync()` method on the `ModelParser`, in addition to accepting a single string, also accepts an asynchronous enumeration of string values containing the JSON text to parse as DTDL.

```C# Snippet:DtdlParserTutorial01Async_CallParseAsyncWithEnumeration
parseTask = modelParser.ParseAsync(GetJsonTexts(jsonTexts));
```

[repeat]: # (Snippet:DtdlParserTutorial01Async_CallWait)

The enumeration of JSON texts above is valid DTDL.
After waiting on the `Task` per the code snippet above, it displays:

```Console
DTDL model is valid!
```
