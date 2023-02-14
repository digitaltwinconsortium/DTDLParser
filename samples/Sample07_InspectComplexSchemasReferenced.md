# Inspect complex schemas referenced by contents

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This sample walks through an aspect of the third use: how to inspect complex schema elements in the object model.
This sample specifically inspects complex schemas referenced by contents.
Two related samples inspect [complex schemas embedded in contents](./Sample06_InspectComplexSchemasEmbedded.md) and [standard complex schemas referenced by contents](./Sample08_InspectComplexSchemasStandard.md).

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserSample07_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model containing a referenced complex schema

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.

```C# Snippet:DtdlParserSample07_ObtainDtdlTextContainingMap
string jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""schemas"": [
    {
      ""@id"": ""dtmi:example:allotment;1"",
      ""@type"": ""Map"",
      ""mapKey"": {
        ""name"": ""item"",
        ""schema"": ""string""
      },
      ""mapValue"": {
        ""name"": ""count"",
        ""schema"": ""integer""
      }
    }
  ],
  ""contents"": [
    {
      ""@type"": ""Property"",
      ""name"": ""expectedAllotment"",
      ""schema"": ""dtmi:example:allotment;1""
    },
    {
      ""@type"": ""Telemetry"",
      ""name"": ""runningAllotment"",
      ""schema"": ""dtmi:example:allotment;1""
    }
  ]
}";
```

## Submit the JSON text to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is complete and valid, no exception will be thrown.
Proper code should catch and process exceptions as shown in other samples such as [this one](Sample02_FixInvalidDtdlModel.md), but for simplicity the present sample omits exception handling.

```C# Snippet:DtdlParserSample07_CallParse
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
```

## Retrieve Interface and contents elements from object model

The Interface element can be looked up in the object model by its identifier:

```C# Snippet:DtdlParserSample07_GetInterfaceById
var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
```

Each of the content values can be accessed by name via the `Contents` property on `DTInterfaceInfo`:

```C# Snippet:DtdlParserSample07_GetContentsByName
string expectedAllotmentName = "expectedAllotment";
var expectedAllotment = (DTPropertyInfo)anInterface.Contents[expectedAllotmentName];

string runningAllotmentName = "runningAllotment";
var runningAllotment = (DTTelemetryInfo)anInterface.Contents[runningAllotmentName];
```

## Display complex schema

The Property "expectedAllotment" and the Telemetry "runningAllotment" both have the same schema value.
This can be seen in the JSON text above, and it can also be accessed via the `Schema` property on each object:

```C# Snippet:DtdlParserSample07_DisplayContentsSchema
Console.WriteLine($"expectedAllotment schema is {expectedAllotment.Schema.Id}");
Console.WriteLine($"runningAllotment schema is {runningAllotment.Schema.Id}");
```

This snippet displays:

```Console
expectedAllotment schema is dtmi:example:allotment;1
runningAllotment schema is dtmi:example:allotment;1
```

As can be seen in the JSON text above, a complex schema element with identifier "dtmi:example:allotment;1" is defined in the "schemas" property of "dtmi:example:anInterface;1".
To obtain information about this complex schema element, we can look in the object model.

## Drill down on complex schema

The DTDL type of each element is expressed via the property `EntityKind` on the `DTEntityInfo` base class, which has type `enum DTEntityKind`.
We can use this property to determine the complex schema type.
We can access the complex schema element as `expectedAllotment.Schema` or as `runningAllotment.Schema`, since they both point to the same element.
However, for this sample, will illustrate accessing the complex schema element by identifier:

```C# Snippet:DtdlParserSample07_DisplayAllotmentKind
var allotmentId = new Dtmi("dtmi:example:allotment;1");
var allotment = objectModel[allotmentId];
Console.WriteLine($"allotment type is {allotment.EntityKind}");
```

This snippet displays:

```Console
allotment type is Map
```

This is the same output we would see from either of the following lines:

```C# Snippet:DtdlParserSample07_DisplayAllotmentKindIndirect
Console.WriteLine($"allotment type is {expectedAllotment.Schema.EntityKind}");
Console.WriteLine($"allotment type is {runningAllotment.Schema.EntityKind}");
```

Which display:

```Console
allotment type is Map
allotment type is Map
```

By casting the schema element to a DTMapInfo, we can inspect its properties:

```C# Snippet:DtdlParserSample07_DisplayAllotmentMapProperties
var allotmentMap = (DTMapInfo)allotment;

Console.WriteLine($"map key name is {allotmentMap.MapKey.Name}");
Console.WriteLine($"map key schema is {allotmentMap.MapKey.Schema.Id}");
Console.WriteLine($"map value name is {allotmentMap.MapValue.Name}");
Console.WriteLine($"map value schema is {allotmentMap.MapValue.Schema.Id}");
```

This snippet displays:

```Console
map key name is item
map key schema is dtmi:dtdl:instance:Schema:string;2
map value name is count
map value schema is dtmi:dtdl:instance:Schema:integer;2
```

The identifiers dtmi:dtdl:instance:Schema:string;2 and dtmi:dtdl:instance:Schema:integer;2 represent elements in the DTDL language model for the schemas 'string' and 'integer', respectively.

If we care to, we can map these identifiers back to the terms used in the JSON text of the DTDL model by using the `ModelParser.GetTermOrUri()` static method:

```C# Snippet:DtdlParserSample07_DisplayAllotmentMapPropertySchemaTerms
Console.WriteLine($"map key schema term is {ModelParser.GetTermOrUri(allotmentMap.MapKey.Schema.Id)}");
Console.WriteLine($"map value schema term is {ModelParser.GetTermOrUri(allotmentMap.MapValue.Schema.Id)}");
```

This snippet displays:

```Console
map key schema term is string
map value schema term is integer
```
