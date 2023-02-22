# Inspect standard complex schemas referenced by contents

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial walks through an aspect of the third use: how to inspect complex schema elements in the object model.
This tutorial specifically inspects standard complex schemas referenced by contents.
Two related tutorials inspect [complex schemas embedded in contents](./Tutorial06_InspectComplexSchemasEmbedded.md) and [complex schemas referenced by contents](./Tutorial07_InspectComplexSchemasReferenced.md).

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial08_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model that refers to a standard complex schema

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.

```C# Snippet:DtdlParserTutorial08_ObtainDtdlTextReferencingGeoPoint
string jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""contents"": [
    {
      ""@type"": ""Telemetry"",
      ""name"": ""currentLocation"",
      ""schema"": ""point""
    }
  ]
}";
```

## Submit the JSON text to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is complete and valid, no exception will be thrown.
Proper code should catch and process exceptions as shown in other tutorials such as [this one](Tutorial02_FixInvalidDtdlModel.md), but for simplicity the present tutorial omits exception handling.

```C# Snippet:DtdlParserTutorial08_CallParse
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
```

## Retrieve Interface and contents elements from object model

The Interface element can be looked up in the object model by its identifier:

```C# Snippet:DtdlParserTutorial08_GetInterfaceById
var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
```

The content value can be accessed by name via the `Contents` property on `DTInterfaceInfo`:

```C# Snippet:DtdlParserTutorial08_GetTelemetryByName
string currentLocationName = "currentLocation";
var currentLocation = (DTTelemetryInfo)anInterface.Contents[currentLocationName];
```

## Display complex schema

The JSON text above shows that the Telemetry "currentLocation" has the schema value "point".
This appears in a different form in the object model, where it can be  accessed via the `Schema` property on the Telemetry object:

```C# Snippet:DtdlParserTutorial08_DisplayTelemetrySchema
Console.WriteLine($"currentLocation schema is {currentLocation.Schema.Id}");
```

This snippet displays:

```Console
currentLocation schema is dtmi:standard:schema:geospatial:point;3
```

If we care to, we can map this identifier back to the term used in the JSON text of the DTDL model by using the `ModelParser.GetTermOrUri()` static method:

```C# Snippet:DtdlParserTutorial08_DisplayTelemetrySchemaTerm
Console.WriteLine($"currentLocation schema term is {ModelParser.GetTermOrUri(currentLocation.Schema.Id)}");
```

This snippet displays:

```Console
currentLocation schema term is point
```

An element with identifier "dtmi:standard:schema:geospatial:point;3" is defined in the DTDL v2 language model as a standard element.
Because it is referenced by an element in the submitted model, the 'point' element and all of its transitively referenced elements are included in the object model.

## Drill down on complex schema

The DTDL type of each element is expressed via the property `EntityKind` on the `DTEntityInfo` base class, which has type `enum DTEntityKind`.
We can use this property to determine the complex schema type.

We can access the complex schema element as `currentLocation.Schema`; however, will illustrate accessing it by identifier:

```C# Snippet:DtdlParserTutorial08_DisplayPointKind
var pointId = new Dtmi("dtmi:standard:schema:geospatial:point;3");
var point = objectModel[pointId];
Console.WriteLine($"point type is {point.EntityKind}");
```

This snippet displays:

```Console
point type is Object
```

This is the same output we would see from the following line:

```C# Snippet:DtdlParserTutorial08_DisplayPointKindIndirect
Console.WriteLine($"point type is {currentLocation.Schema.EntityKind}");
```

Which displays:

```Console
point type is Object
```

The object model can be inspected to determine the "fields" values of the point Object, but herein we will exploit our prior knowledge that there are two fields with names "type" and "coordinates".
By casting the schema element to a DTObjectInfo, we can extract the "fields" values using `Linq`:

```C# Snippet:DtdlParserTutorial08_QueryPointObjectFields
var pointObject = (DTObjectInfo)point;
DTFieldInfo typeField = pointObject.Fields.First(f => f.Name == "type");
DTFieldInfo coordinatesField = pointObject.Fields.First(f => f.Name == "coordinates");
```

The "type" field is an enum with a single enum value "Point", as the following snippet shows:

```C# Snippet:DtdlParserTutorial08_DisplayType
Console.WriteLine($"type field has schema type {typeField.Schema.EntityKind}");
Console.WriteLine($"type field schema has enum value {((DTEnumInfo)typeField.Schema).EnumValues[0].EnumValue}");
```

This snippet displays:

```Console
type field has schema type Enum
type field schema has enum value Point
```

The "coordinates" field is an array of double, as the following snippet shows:

```C# Snippet:DtdlParserTutorial08_DisplayCoordinates
Console.WriteLine($"coordinates field has schema type {coordinatesField.Schema.EntityKind}");
Console.WriteLine($"coordinates field schema has element schema {((DTArrayInfo)coordinatesField.Schema).ElementSchema.Id}");
```

This snippet displays:

```Console
coordinates field has schema type Array
coordinates field schema has element schema dtmi:dtdl:instance:Schema:double;2
```
