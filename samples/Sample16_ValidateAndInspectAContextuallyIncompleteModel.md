# Validate and inspect a contextually incomplete model

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This sample illustrates working with a model that is *contextually incomplete*, meaning that it specifies an extension context that has no available definition.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserSample16_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a model that includes an unknown context

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model specifies a context for DTDL version 3 and an extension context that is not recognized by the `ModelParser` because it is not one of the contexts  itemized in [Supported extension contexts](../dotnet/src/Parser/generated/SupportedExtensions.g.md).

```C# Snippet:DtdlParserSample16_ObtainDtdlText
string jsonText =
@"{
  ""@context"": [ ""dtmi:dtdl:context;3"", ""dtmi:hypothetical:example:schemaEncoding;1"" ],
  ""@id"": ""dtmi:ex:anInterface;1"",
  ""@type"": ""Interface"",
  ""contents"": [
    {
      ""@type"": [ ""Telemetry"", ""EncodedBoolean"" ],
      ""name"": ""intAsBool"",
      ""schema"": ""integer"",
      ""falseValue"": 0,
      ""trueValue"": 1
    },
    {
      ""@type"": [ ""Telemetry"", ""EncodedBoolean"" ],
      ""name"": ""stringAsBool"",
      ""schema"": ""string"",
      ""falseValue"": ""FALSE"",
      ""trueValue"": ""TRUE""
    }
  ]
}";
```

The hypothetical example extension allows a model to designate that a Property or Telemetry with a non-boolean schema should be interpreted as a boolean, with specific values indicating false and true.
The "intAsBool" Telemetry in this model represents false with an integer value of 0 and true with an integer value of 1.
The "stringAsBool" Telemetry represents false with a string value of "FALSE" and true with a string value of "TRUE".

## Submit the JSON text to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is invalid, a `ParsingException` will be thrown.

```C# Snippet:DtdlParserSample16_DeclareObjectModelVar
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = null;
```

```C# Snippet:DtdlParserSample16_CallParse
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
    Console.WriteLine("DTDL model is invalid:");
    foreach (ParsingError err in ex.Errors)
    {
        Console.WriteLine(err);
    }
}
```

## Observe parsing errors

For the JSON text above, the code snippet above will display a single error:

```Console
DTDL model is invalid:
@context specifier has value 'dtmi:hypothetical:example:schemaEncoding;1', which is unrecognized. Remove 'dtmi:hypothetical:example:schemaEncoding;1' @context specifier.
```

Note that the `ModelParser` did not throw a `ResolutionException`, as it would have if the model contained an external reference to another DTDL model, as illustrated in the [Resolve external references](./Sample10_ResolveExternalReferences.md) sample.
A model with an external reference is *referentially incomplete*, whereas the above model is *contextually incomplete* since it includes an unrecognized context specifier.
At present, the `ModelParser` is unable to dynamically load DTDL language extensions via the `DtmiResolver` callback, so no `ResolutionException` exception is thrown.

## Set the AllowUndefinedExtensions option and resubmit the JSON text strings

The extension context dtmi:hypothetical:example:schemaEncoding;1 might refer to a legitimate DTDL language extension that happens to be unknown to the version of the `ModelParser` that is being used.
Alternatively, this context may refer to an expected future language extension that has not yet been fully defined or released.
In either case, a service or tool may benefit from being able to perform a best-effort validation of the understood portions of the model and to inspect the opaque portions.

The `ModelParser` constructor takes an optional parameter of type `ParsingOptions` that controls the parsing of DTDL models.
The following snippet instantiates a new parser with a `ParsingOptions` instance whose `AllowUndefinedExtensions` property is `true`.

```C# Snippet:DtdlParserSample16_NewParserSetAllowUndefinedExtensions
modelParser = new ModelParser(new ParsingOptions() { AllowUndefinedExtensions = true });
```

[repeat]: # (Snippet:DtdlParserSample16_CallParse)

With this option set, if the code snippet above that calls `modelParser.Parse()` on the JSON texts is re-executed, the output will be:

```Console
DTDL model is valid!
```

This indicates that the submitted model is valid to the extent that the `ModelParser` is able to determine without knowing the definition of the dtmi:hypothetical:example:schemaEncoding;1 extension.
If the extension happens to be legitimate but unknown, its definition might include rules that the model violates, such as:

* It might not define a supplemental type named `EncodedBoolean`.
* It might define the `EncodedBoolean` supplemental type but disallow it from co-typing a Telemetry.
* It might not associate the property `falseValue` and/or `trueValue` with the `EncodedBoolean` type.
* It might not allow an integer value for the property `falseValue` or the property `trueValue`.
* It might not allow a string value for the property `falseValue` or the property `trueValue`.

It is also possible for the extension to include rules that the above model does not violate.
For example, it might require the literal type of the properties `falseValue` and `trueValue` to match the type specified by the `schema` property.

However, even though the `ModelParser` is unable to validate whether the model conforms to the unknown extension, it does validate whether the model contains any other errors.
For example, if one of the Telemetries lacked a `schema` property, this would throw a `ParsingException` irrespective of whether the `AllowUndefinedExtensions` option is set.

## Display elements in object model

The object model is a collection of objects in a class hierarchy rooted at `DTEntityInfo`.
All DTDL elements derive from the DTDL abstract type Entity, and each DTDL type has a corresponding C# class whose name has a prefix of "DT" (for Digital Twins) and a suffix of "Info".
The elements in the object model are indexed by their identifiers, which have type `Dtmi`.  The following snippet displays the identifiers of all elements in the object model:

```C# Snippet:DtdlParserSample16_DisplayElements
Console.WriteLine($"{objectModel.Count} elements in model:");
foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
{
    Console.WriteLine(modelElement.Key);
}
```

For the JSON text above, this snippet displays:

```Console
5 elements in model:
dtmi:ex:anInterface:_contents:__intAsBool;1
dtmi:ex:anInterface:_contents:__stringAsBool;1
dtmi:ex:anInterface;1
dtmi:dtdl:instance:Schema:integer;2
dtmi:dtdl:instance:Schema:string;2
```

Of these five identifiers, only dtmi:ex:anInterface;1 is present in the DTDL source model.
The identifiers for the contents named "intAsBool" and "stringAsBool" are auto-generated by the `ModelParser` following rules that guarantee their uniqueness.
The identifiers for the terms "integer" and "string" are defined by the DTDL language model.

## Drill down to elements with undefined types

An individual element can be looked up in the object model by its identifier:

```C# Snippet:DtdlParserSample16_GetInterfaceById
var anInterfaceId = new Dtmi("dtmi:ex:anInterface;1");
var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
```

The Interface in the DTDL model above has two 'contents' values, which are Telemetries that we will access by name via the `Contents` property on `DTInterfaceInfo`:

```C# Snippet:DtdlParserSample16_GetTelemetriesByName
string intAsBoolName = "intAsBool";
string stringAsBoolName = "stringAsBool";

var intAsBool = (DTTelemetryInfo)anInterface.Contents[intAsBoolName];
var stringAsBool = (DTTelemetryInfo)anInterface.Contents[stringAsBoolName];
```

## Inspect model element types

We can observe the primary type of these element from their `EntityKind` property values.

```C# Snippet:DtdlParserSample16_DisplayTelemetryPrimaryTypes
Console.WriteLine($"intAsBool primary type = {intAsBool.EntityKind}");
Console.WriteLine($"stringAsBool primary type = {stringAsBool.EntityKind}");
```

This snippet displays:

```Console
intAsBool primary type = Telemetry
stringAsBool primary type = Telemetry
```

We can see in the DTDL model text that these two Telemetries each appear to have a supplemental type of `EncodedBoolean`, which we might expect to observe when inspecting the `SupplementalTypes` property.

```C# Snippet:DtdlParserSample16_DisplayTelemetrySupplementalTypes
Console.WriteLine("intAsBool supplemental types = " + string.Join(", ", intAsBool.SupplementalTypes.Select(t => t.ToString())));
Console.WriteLine("stringAsBool supplemental types = " + string.Join(", ", stringAsBool.SupplementalTypes.Select(t => t.ToString())));
```

This snippet displays:

```Console
intAsBool supplemental types = 
stringAsBool supplemental types = 
```

Perhaps surprisingly, the Telemetries have no supplemental types.
This is because there is no known definition for these types, since the extension is unknown.

The object model provides a different property, `UndefinedTypes`, to inspect any undefined types on an element.

```C# Snippet:DtdlParserSample16_DisplayTelemetryUndefinedTypes
Console.WriteLine("intAsBool undefined types = " + string.Join(", ", intAsBool.UndefinedTypes));
Console.WriteLine("stringAsBool undefined types = " + string.Join(", ", stringAsBool.UndefinedTypes));
```

This snippet displays:

```Console
intAsBool undefined types = EncodedBoolean
stringAsBool undefined types = EncodedBoolean
```

Unlike supplemental types, which are reported by their DTMI values, undefined types are reported by the string values used in the DTDL model, because the absence of known context definitions prevents translating the putative terms to DTMIs.

## Inspect model element properties

Just as the object model exposes no supplemental types for the Telemetries, it also exposes no supplemental properties:

```C# Snippet:DtdlParserSample16_DisplayTelemetrySupplementalProperties
Console.WriteLine("intAsBool supplemental properties = " + string.Join(", ", intAsBool.SupplementalProperties.Select(t => t.Key)));
Console.WriteLine("stringAsBool supplemental properties = " + string.Join(", ", stringAsBool.SupplementalProperties.Select(t => t.Key)));
```

This snippet displays:

```Console
intAsBool supplemental properties = 
stringAsBool supplemental properties = 
```

As with the undefined types, the undefined properties can be accessed directly in the raw JSON form in which they are present in the model text.

```C# Snippet:DtdlParserSample16_DisplayTelemetryUndefinedProperties
Console.WriteLine($"intAsBool undefined properties:");
foreach (KeyValuePair<string, JsonElement> kvp in intAsBool.UndefinedProperties)
{
    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
}

Console.WriteLine($"stringAsBool undefined properties:");
foreach (KeyValuePair<string, JsonElement> kvp in stringAsBool.UndefinedProperties)
{
    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
}
```

This snippet displays:

```Console
intAsBool undefined properties:
  falseValue: 0
  trueValue: 1
stringAsBool undefined properties:
  falseValue: FALSE
  trueValue: TRUE
```
