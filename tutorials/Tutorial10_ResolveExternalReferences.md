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

```C# Snippet:DtdlParserTutorial10_DeclareObjectModelVar
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = null;
```

```C# Snippet:DtdlParserTutorial10_CallParse
try
{
    objectModel = modelParser.Parse(jsonText);
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

## Inspect the Interface hierarchy via DTDL properties

An individual element can be looked up in the object model by its identifier:

```C# Snippet:DtdlParserTutorial10_GetInterfacesById
var anInterface = (DTInterfaceInfo)objectModel[new Dtmi("dtmi:example:anInterface;1")];
var anotherInterface = (DTInterfaceInfo)objectModel[new Dtmi("dtmi:example:anotherInterface;1")];
```

The .NET property `Extends` on .NET class `DTInterfaceInfo` is a direct analogue of the DTDL property 'extends' on DTDL type Interface.
Inspecting the `Extends` property enables bottom-up traversal of the Interface hierarchy by showing the other Interfaces that each Interface extends, as shown in the following code snippet.

```C# Snippet:DtdlParserTutorial10_DisplayExtendingInterfaces
if (anInterface.Extends.Any())
{
    Console.WriteLine($"anInterface extends:");
    foreach (DTInterfaceInfo extendedInterface in anInterface.Extends)
    {
        Console.WriteLine($"  {extendedInterface.Id}");
    }
}

if (anotherInterface.Extends.Any())
{
    Console.WriteLine($"anotherInterface extends:");
    foreach (DTInterfaceInfo extendedInterface in anotherInterface.Extends)
    {
        Console.WriteLine($"  {extendedInterface.Id}");
    }
}
```

This code snippet displays:

```Console
anInterface extends:
  dtmi:example:anotherInterface;1
```

## Inspect the Interface hierarchy via synthetic properties

The object model exposed via the `ModelParser` attaches properties that are not directly represented in the DTDL language but rather are synthesized from DTDL properties.
Specifically, values of the 'extends' property are also expressed in a reversed form via the synthetic property `ExtendedBy`, which enables top-down traversal of the Interface hierarchy by showing the other Interfaces that each Interface is extended by.
This is shown in the following code snippet.

```C# Snippet:DtdlParserTutorial10_DisplayExtendedInterfaces
if (anInterface.ExtendedBy.Any())
{
    Console.WriteLine($"anInterface is extended by:");
    foreach (DTInterfaceInfo extendedInterface in anInterface.ExtendedBy)
    {
        Console.WriteLine($"  {extendedInterface.Id}");
    }
}

if (anotherInterface.ExtendedBy.Any())
{
    Console.WriteLine($"anotherInterface is extended by:");
    foreach (DTInterfaceInfo extendedInterface in anotherInterface.ExtendedBy)
    {
        Console.WriteLine($"  {extendedInterface.Id}");
    }
}
```

This code snippet displays:

```Console
anotherInterface is extended by:
  dtmi:example:anInterface;1
```
