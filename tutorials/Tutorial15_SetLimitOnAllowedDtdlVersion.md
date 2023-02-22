# Set limit on allowed DTDL version

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial illustrates using the `MaxDtdlVersion` property of optional constructor parameter `parsingOptions` to set a limit on which versions of DTDL are acceptable.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial15_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL v2 model

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model specifies a context for DTDL version 2.

```C# Snippet:DtdlParserTutorial15_DtdlV2Text
string jsonTextV2 =
@"{
  ""@context"": ""dtmi:dtdl:context;2"",
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

## Obtain the JSON text of a DTDL v3 model

The following model specifies a context for DTDL version 3.

```C# Snippet:DtdlParserTutorial15_DtdlV3Text
string jsonTextV3 =
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

## Obtain the JSON text of a DTDL v99 model

The following model specifies a context for DTDL version 99, which has not been defined.

```C# Snippet:DtdlParserTutorial15_DtdlV99Text
string jsonTextV99 =
@"{
  ""@context"": ""dtmi:dtdl:context;99"",
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

## Submit the JSON text strings to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is invalid, a `ParsingException` will be thrown.
If the submitted model is referentially incomplete, a `ResolutionException` will be thrown.
If no exception is thrown, the model is valid.

```C# Snippet:DtdlParserTutorial15_CallParse
try
{
    Console.Write("Parsing DTDL v2 JSON text... ");
    modelParser.Parse(jsonTextV2);
    Console.WriteLine("model is valid!");

    Console.Write("Parsing DTDL v3 JSON text... ");
    modelParser.Parse(jsonTextV3);
    Console.WriteLine("model is valid!");

    Console.Write("Parsing DTDL v99 JSON text... ");
    modelParser.Parse(jsonTextV99);
    Console.WriteLine("model is valid!");
}
catch (ResolutionException ex)
{
    Console.WriteLine($"model is incomplete: {ex}");
}
catch (ParsingException ex)
{
    Console.WriteLine("model is invalid:");
    foreach (ParsingError err in ex.Errors)
    {
        Console.WriteLine($"Validation ID = {err.ValidationID}");
        Console.WriteLine($"Cause of error: {err.Cause}");
        Console.WriteLine($"Action to fix: {err.Action}");
    }
}
```

The `ParsingException` has a property named `Errors` that is a collection of `ParsingError` objects, each of which provides details about one error in the submitted model.
The `ParsingError` class has several properties that can be programmatically inspected to obtain details of the error, including:

- The `ValidationID` is an identifier associated with the particular error condition.
- The `Cause` is a human-readable string explaining what caused the error.
- The `Action` is a human-readable string explaining how to fix the error.

This class also overrides the `ToString()` method to return a concatenation of `Cause` and `Action`.

## Observe Parse() results for the three models

The code snippet above displays:

```Console
Parsing DTDL v2 JSON text... model is valid!
Parsing DTDL v3 JSON text... model is valid!
Parsing DTDL v99 JSON text... model is invalid:
Validation ID = dtmi:dtdl:parsingError:unrecognizedContextVersion
Cause of error: @context specifier has value 'dtmi:dtdl:context;99', which specifies a DTDL version that is not recognized.
Action to fix: Modify @context specifier to indicate one of the following DTDL versions: 2, 3.
```

The two models that specify DTDL versions 2 and 3 both parse as valid.
The model that specifies DTDL version 99 throws a `ParsingException` containing a `ParsingError` with `ValidationID` dtmi:dtdl:parsingError:unrecognizedContextVersion, indicating that the parser does not recognize the version of DTDL specified by the model.
The `Cause` property states this condition in English, and the `Action` property suggests which DTDL versions may be used alternatively.

## Set the maximum DTDL version and resubmit the JSON text strings

The `ModelParser` has a read-only property named `MaxDtdlVersion`, which indicates the maximum version of DTDL that is acceptable.

By default, this is the maximum version of DTDL that is understood by the parser:

```C# Snippet:DtdlParserTutorial15_DisplayParserMaxDtdlVersion
Console.WriteLine($"MaxDtdlVersion is {modelParser.MaxDtdlVersion}");
```

This code snippet displays:

```Console
MaxDtdlVersion is 3
```

The `ParsingOptions` class can be used to set options for the `ModelParser` behavior.
The following snippet instantiates an instance of the `ParsingOptions` class:

```C# Snippet:DtdlParserTutorial15_NewParsingOptions
ParsingOptions parsingOptions = new ParsingOptions();
```

The `MaxDtdlVersion` property of `ParsingOptions` indicates the maximum version of DTDL that is acceptable.
This property also defaults to the maximum version of DTDL that is understood by the parser:

```C# Snippet:DtdlParserTutorial15_DisplayOptionsMaxDtdlVersion
Console.WriteLine($"MaxDtdlVersion is {parsingOptions.MaxDtdlVersion}");
```

This code snippet displays:

```Console
MaxDtdlVersion is 3
```

The following snippet sets the value of `MaxDtdlVersion` to 2 and instantiates a new parser with the `parsingOptions` object passed into the `ModelParser` constructor.

```C# Snippet:DtdlParserTutorial15_NewParserWithClienOptions
parsingOptions.MaxDtdlVersion = 2;
modelParser = new ModelParser(parsingOptions);
```

[repeat]: # (Snippet:DtdlParserTutorial15_DisplayParserMaxDtdlVersion)

If we repeat the snippet above that displays the value of the `MaxDtdlVersion` property of the `ModelParser`, it now reflects the value passed in via `ParsingOptions`:

```Console
MaxDtdlVersion is 2
```

[repeat]: # (Snippet:DtdlParserTutorial15_CallParse)

With this limit in place, if the code snippet above that calls `modelParser.Parse()` on the JSON texts is re-executed, the output will be:

```Console
Parsing DTDL v2 JSON text... model is valid!
Parsing DTDL v3 JSON text... model is invalid:
Validation ID = dtmi:dtdl:parsingError:disallowedContextVersion
Cause of error: @context specifier has value 'dtmi:dtdl:context;3', which specifies a DTDL version that exceeds the configured max version of 2.
Action to fix: Modify @context specifier to indicate a DTDL major version no greater than 2.
```

The model that specifies DTDL version 2 parses as valid.
The model that specifies DTDL version 3 throws a `ParsingException` containing a `ParsingError` with `ValidationID` dtmi:dtdl:parsingError:disallowedContextVersion, indicating that the parser actively refuses the version of DTDL specified by the model.
The `Cause` property states this condition in English, and the `Action` property suggests the range of DTDL versions that may be used alternatively.
