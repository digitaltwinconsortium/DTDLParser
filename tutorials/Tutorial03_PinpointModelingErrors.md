# Pinpoint errors in an invalid DTDL model

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial walks through the second of these uses: how to identify and fix modeling errors.
This tutorial follows the same flow as [Fix an invalid DTDL model](./Tutorial02_FixInvalidDtdlModel.md) but illustrates use of the `DtdlParseLocator` to provide pinpoint accuracy to identified modeling errors.
An [asynchronous version](./Tutorial03_PinpointModelingErrorsAsync.md) of this tutorial is also available.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial03_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model contains several errors.

```C# Snippet:DtdlParserTutorial03_ObtainInvalidDtdlText
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

## Create a DtdlParseLocator

Without support from the calling code, the `ModelParser` reports the location of an error with reference to a DTDL element with an `@id` property, as illustrated in tutorial [Fix an invalid DTDL model](Tutorial02_FixInvalidDtdlModel.md).
If an error is found in an element that lacks an `@id` property, the location is reported by indirect reference to the closest element with an `@id` value above in the hierarchy.
If the modeling hierarchy is deep, this can require excessive effort for a user to trace.

In many cases, the `ModelParser` can identify which of the submitted JSON text strings contains the error, as well as the line number or range within the text string.
The calling code can provide a callback delegate that converts this internal location into a location that is understandable by the user, which can greatly improve the clarity of the error description.

An example of such a delegate is the following:

```C# Snippet:DtdlParserTutorial03_CreateDtdlParseLocator
DtdlParseLocator locator = (
    int parseIndex,
    int parseLine,
    out string sourceName,
    out int sourceLine) =>
{
    sourceName = "string variable 'jsonText'";
    sourceLine = parseLine;
    return true;
};
```

When the `ModelParser` encounters an error, it calls the delegate with a `parseIndex` indicating the index of a submitted text string and a `parseLine` indicating a line number within this string.
If the delegate is able to convert these values into a user-meaningful location, it should set the out-parameter `sourceName` to the name of the appropriate source file, URL, etc.; set the out-parameter `sourceLine` to the corresponding line number within this source; and return a value of true.
If it is not able to perform the conversion, it should return a value of false.

Because the present tutorial submits only a single JSON text string, the information provided by the delegate above is minimal, but it illustrates the usage.

## Submit the JSON text to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
An optional second argument is a `DtdlParseLocator`, such as the one defined above.
If the submitted model is invalid, a `ParsingException` will be thrown.

```C# Snippet:DtdlParserTutorial03_CallParse
try
{
    var objectModel = modelParser.Parse(jsonText, locator);
    Console.WriteLine($"DTDL model is valid!");
}
catch (ResolutionException ex)
{
    Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
}
catch (ParsingException ex)
{
    Console.WriteLine("DTDL model is invalid:");
    foreach (ParsingError err in ex.Errors)
    {
        Console.WriteLine(err);
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
In string variable 'jsonText', property 'content' on line 5 is an undefined term. Replace property 'content' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.
```

## Fix first parsing error and resubmit

Looking at the JSON text, we see that the `@type` on line 4 has value `Interface` and the property on line 5 has name `content`, which is not a valid property for an `Interface`.
The property should be named `contents`.
This is corrected as follows:

```C# Snippet:DtdlParserTutorial03_CorrectPropertyName
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

[repeat]: # (Snippet:DtdlParserTutorial03_CallParse)

When this JSON text is submitted via the code snippet above, it displays:

```Console
DTDL model is invalid:
In string variable 'jsonText', element has @type on line 7 that specifies type Telemtry that is an undefined term. Remove @type Telemtry or replace with an appropriate DTDL type -- see aka.ms/dtdl.
```

## Fix second parsing error and resubmit

Previously, when the property named `contents` was misnamed `content`, the above error was hidden, because this error reflects a restriction on the allowed values of `contents`.
Looking at the JSON text above, we see that the `contents` property on line 5 has a value that is a JSON object that spans lines 6-9.
This object has a `@type` property on line 7 with value `"Telemtry"`, which is a misspelling of `"Telemetry"`.
This is corrected as follows:

```C# Snippet:DtdlParserTutorial03_CorrectTypeName
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

[repeat]: # (Snippet:DtdlParserTutorial03_CallParse)

When this JSON text is submitted via the code snippet above, it displays:

```Console
DTDL model is invalid:
In string variable 'jsonText', element on lines 6-9 requires property 'schema'; however, this property is not present. Add a property 'schema' to the element.
```

## Fix third parsing error and resubmit

Previously, when the `@type` of the `contents` value was misspelled as `Telemtry`, the above error was hidden, because this error reflects a restriction on the allowed values of elements that have type `Telemetry`.
The missing `schema` property can be corrected as follows:

```C# Snippet:DtdlParserTutorial03_AddRequiredProperty
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

[repeat]: # (Snippet:DtdlParserTutorial03_CallParse)

The JSON text above is valid DTDL, so the code snippet above will display:

```Console
DTDL model is valid!
```
