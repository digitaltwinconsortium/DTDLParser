# Fix an invalid DTDL model asynchronously

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial walks through the second of these uses: how to identify and fix modeling errors.
A [synchronous version](./Tutorial02_FixInvalidDtdlModel.md) of this tutorial is also available.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial02Async_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an asynchronous enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model contains several errors.

```C# Snippet:DtdlParserTutorial02Async_ObtainInvalidDtdlText
string jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""content"": [
    {
      ""@type"": ""Telemtry"",
      ""name"": ""currentDistance""
    }
  ]
}";
```

## Submit the JSON text to the ModelParser

The main asynchronous method on the `ModelParser` is `ParseAsync()`.
One argument is required, which can be either a string or an asynchronous enumeration of strings containing the JSON text to parse as DTDL.

```C# Snippet:DtdlParserTutorial02Async_CallParseAsync
var parseTask = modelParser.ParseAsync(jsonText);
```

The return value is a `Task`, whose completion must be awaited before proceeding.
If the submitted model is invalid or incomplete, an exception will be thrown, wrapped in an `AggregateException` by the `System.Threading.Tasks` framework.
If the submitted model is invalid, the wrapped exception will be a `ParsingException`.

```C# Snippet:DtdlParserTutorial02Async_CallWait
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
    else if (ex.InnerException is ParsingException pe)
    {
        Console.WriteLine("DTDL model is invalid:");
        foreach (ParsingError err in pe.Errors)
        {
            Console.WriteLine(err);
        }
    }
    else
    {
        throw;
    }
}
```

## Observe parsing errors

The `ParsingException` has a property named `Errors` that is a collection of `ParsingError` objects, each of which provides details about one error in the submitted model.
The `ParsingError` class has several properties that can be programatically inspected to obtain details of the error.
This class also overrides the `ToString()` method to provide a human-readable description of the error.

The `Errors` property is a collection because the `ModelParser` report as many errors as it can identify, instead of stopping the parse at the first error.
However, certain errors will prevent other errors from being observed, as this tutorial will illustrate.

For the JSON text above, the code snippet above will display a single error:

```Console
DTDL model is invalid:
dtmi:example:anInterface;1's property 'content' is an undefined term. Replace property 'content' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.
```

## Fix first parsing error and resubmit

Looking at the JSON text, we see that element whose `@id` is `dtmi:example:anInterface;1` has a property named `content`, which is not a valid property for an `Interface`.
The property should be named `contents`.
This is corrected as follows:

```C# Snippet:DtdlParserTutorial02Async_CorrectPropertyName
jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""contents"": [
    {
      ""@type"": ""Telemtry"",
      ""name"": ""currentDistance""
    }
  ]
}";
```

When we call `ParseAsync()` with this updated JSON text:

```C# Snippet:DtdlParserTutorial02Async_RepeatCallParseAsync
parseTask = modelParser.ParseAsync(jsonText);
```

[repeat]: # (Snippet:DtdlParserTutorial02Async_CallWait)

And then wait on the `Task` per the code snippet above, it displays:

```Console
DTDL model is invalid:
dtmi:example:anInterface;1 has 'contents' value with name 'currentDistance' which has @type that specifies type Telemtry that is an undefined term. Remove @type Telemtry or replace with an appropriate DTDL type -- see https://github.com/Azure/opendigitaltwins-dtdl/tree/master/DTDL.
```

## Fix second parsing error and resubmit

Previously, when the property named `contents` was misnamed `content`, the above error was hidden, because this error reflects a restriction on the allowed values of `contents`.
Looking at the JSON text above, we see that the value of `contents` is a JSON object whose `@type` is `"Telemtry"`, which is a misspelling of `"Telemetry"`.
This is corrected as follows:

```C# Snippet:DtdlParserTutorial02Async_CorrectTypeName
jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""contents"": [
    {
      ""@type"": ""Telemetry"",
      ""name"": ""currentDistance""
    }
  ]
}";
```

[repeat]: # (Snippet:DtdlParserTutorial02Async_RepeatCallParseAsync)

[repeat]: # (Snippet:DtdlParserTutorial02Async_CallWait)

When this JSON text is submitted to the code snippets above, it displays:

```Console
DTDL model is invalid:
dtmi:example:anInterface;1 has 'contents' value with name 'currentDistance' which requires property 'schema'; however, this property is not present. Add a property 'schema' to the element.
```

## Fix third parsing error and resubmit

Previously, when the `@type` of the `contents` value was misspelled as `Telemtry`, the above error was hidden, because this error reflects a restriction on the allowed values of elements that have type `Telemetry`.
The missing `schema` property can be corrected as follows:

```C# Snippet:DtdlParserTutorial02Async_AddRequiredProperty
jsonText =
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

[repeat]: # (Snippet:DtdlParserTutorial02Async_RepeatCallParseAsync)

[repeat]: # (Snippet:DtdlParserTutorial02Async_CallWait)

The JSON text above is valid DTDL, so the code snippet above will display:

```Console
DTDL model is valid!
```
