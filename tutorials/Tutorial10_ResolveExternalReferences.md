# Resolve external references

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial illustrates how to deal with models that are not self-contained.
An [asynchronous version](./Tutorial11_ResolveExternalReferencesAsync.md) of this tutorial is also available.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial10_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model that references another model

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model contains an external reference.

```C# Snippet:DtdlParserTutorial10_ObtainDtdlText
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

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is invalid, a `ParsingException` will be thrown.
If the submitted model is referentially incomplete, a `ResolutionException` will be thrown.
If no exception is thrown, the model is valid.

```C# Snippet:DtdlParserTutorial10_CallParse
try
{
    var objectModel = modelParser.Parse(jsonText);
    Console.WriteLine($"DTDL model is valid!");
}
catch (ResolutionException ex)
{
    Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
}
catch (ParsingException ex)
{
    Console.WriteLine($"DTDL model is invalid: {ex}");
}
```

## Observe resolution exception

For the JSON text above, the code snippet above will display:

```Console
DTDL model is referentially incomplete: No DtmiResolver provided to resolve requisite reference(s): dtmi:example:anotherInterface;1 (referenced in 1 place)
```

This exception indicates two things:
First, the submitted model contains a reference to dtmi:example:anotherInterface;1 that requires a definition.
Second, the caller has not registered a `DtmiResolver` callback to provide definitions for external references.

The synchronous `Parse()` method expects a `DtmiResolver` callback for resolving undefined identifiers to an enumeration of JSON text strings.

## Obtain the JSON text of referenced DTDL model

We will store the JSON text of the referenced model in a dictionary keyed on the identifier.

```C# Snippet:DtdlParserTutorial10_ObtainReferencedDtdlText
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

## Register a DtmiResolver with the ModelParser

A `DtmiResolver` is a delegate that the `ModelParser` calls whenever it encounters an external reference to an identifier that requires a definition.
We can write a simple resolver and register it with the parser in the constructor:

```C# Snippet:DtdlParserTutorial10_NewParserRegisterDtmiResolver
DtmiResolver dtmiResolver = (IReadOnlyCollection<Dtmi> dtmis) =>
{
    var refJsonTexts = new List<string>();

    foreach (Dtmi dtmi in dtmis)
    {
        if (otherJsonTexts.TryGetValue(dtmi, out string refJsonText))
        {
            refJsonTexts.Add(refJsonText);
        }
    }

    return refJsonTexts;
};

modelParser = new ModelParser(new ParsingOptions { DtmiResolver = dtmiResolver});
```

## Resubmit the original JSON text to the ModelParser

[repeat]: # (Snippet:DtdlParserTutorial10_CallParse)

With the `DtmiResolver` registered, we can resubmit the original JSON text to the `ModelParser` by re-executing the code snippet that calls `Parse()`.
The code snippet now displays:

```Console
DTDL model is valid!
```
