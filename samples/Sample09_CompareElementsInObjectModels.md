# Compare elements in object models

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This sample walks through a specific case of inspecting model contents: comparing elements for equivalence.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserSample09_CreateModelParser
var modelParser = new ModelParser();
```

## Obtain the JSON text of a DTDL model

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.

```C# Snippet:DtdlParserSample09_ObtainFirstDtdlText
string jsonText1 =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""schemas"": [
    {
      ""@id"": ""dtmi:example:numericArray;1"",
      ""@type"": ""Array"",
      ""elementSchema"": ""double""
    }
  ],
  ""contents"": [
    {
      ""@type"": ""Telemetry"",
      ""name"": ""distanceLog"",
      ""schema"": ""dtmi:example:numericArray;1"",
      ""displayName"": ""distance log""
    },
    {
      ""@type"": ""Property"",
      ""name"": ""expectedDistance"",
      ""schema"": ""double"",
      ""displayName"": ""expected distance"",
      ""writable"": true
    },
    {
      ""@type"": ""Command"",
      ""name"": ""setDistance"",
      ""request"": {
        ""name"": ""desiredDistance"",
        ""schema"": ""double"",
        ""displayName"": ""desired distance""
      },
      ""response"": {
        ""name"": ""reportedDistance"",
        ""schema"": ""double"",
        ""displayName"": ""reported distance""
      }
    },
    {
      ""@type"": ""Relationship"",
      ""name"": ""proximity"",
      ""displayName"": {
        ""en-US"": ""proximity""
      }
    }
  ]
}";
```

## Obtain the JSON text of another DTDL model

Within a single object model, there is only one element for any given identifier, and every element is always equal to itself.
To provide more complex scenarios for illustration, this sample performs comparisons across two different object models.

```C# Snippet:DtdlParserSample09_ObtainSecondDtdlText
string jsonText2 =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""schemas"": [
    {
      ""@id"": ""dtmi:example:numericArray;1"",
      ""@type"": ""Array"",
      ""elementSchema"": ""integer""
    }
  ],
  ""contents"": [
    {
      ""@type"": ""Telemetry"",
      ""name"": ""distanceLog"",
      ""schema"": ""dtmi:example:numericArray;1"",
      ""displayName"": ""distance log""
    },
    {
      ""@type"": ""Property"",
      ""writable"": true,
      ""schema"": ""double"",
      ""name"": ""expectedDistance"",
      ""displayName"": ""expected distance""
    },
    {
      ""@type"": ""Command"",
      ""name"": ""setDistance"",
      ""request"": {
        ""@id"": ""dtmi:example:desiredDistance;1"",
        ""name"": ""desiredDistance"",
        ""schema"": ""double"",
        ""displayName"": ""desired distance""
      },
      ""response"": {
        ""name"": ""reportedDistance"",
        ""schema"": ""double"",
        ""displayName"": {
          ""en"": ""reported distance""
        }
      }
    },
    {
      ""@type"": ""Relationship"",
      ""name"": ""proximity"",
      ""displayName"": {
        ""en-US"": ""proximity"",
        ""es-ES"": ""proximidad""
      }
    }
  ]
}";
```

## Submit the JSON texts to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is complete and valid, no exception will be thrown.
Proper code should catch and process exceptions as shown in other samples such as [this one](Sample02_FixInvalidDtdlModel.md), but for simplicity the present sample omits exception handling.

```C# Snippet:DtdlParserSample09_CallParse
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel1 = modelParser.Parse(jsonText1);
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel2 = modelParser.Parse(jsonText2);
```

## Retrieve Interface elements from object models

The Interface element can be looked up in each object model by its identifier:

```C# Snippet:DtdlParserSample09_GetInterfacesById
var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
var anInterface1 = (DTInterfaceInfo)objectModel1[anInterfaceId];
var anInterface2 = (DTInterfaceInfo)objectModel2[anInterfaceId];
```

## Compare Interface elements

We can take a shallow look at the two `DTInterfaceInfo` objects:

```C# Snippet:DtdlParserSample09_DisplayInterfaceProperties
Console.WriteLine($"anInterface1 type = {anInterface1.EntityKind}");
Console.WriteLine($"anInterface1 schemas = {string.Join(", ", anInterface1.Schemas.Select(s => s.Id.ToString()))}");
Console.WriteLine($"anInterface1 contents = {string.Join(", ", anInterface1.Contents.Select(c => c.Value.Name))}");

Console.WriteLine($"anInterface2 type = {anInterface2.EntityKind}");
Console.WriteLine($"anInterface2 schemas = {string.Join(", ", anInterface2.Schemas.Select(s => s.Id.ToString()))}");
Console.WriteLine($"anInterface2 contents = {string.Join(", ", anInterface2.Contents.Select(c => c.Value.Name))}");
```

This snippet displays:

```Console
anInterface1 type = Interface
anInterface1 schemas = dtmi:example:numericArray;1
anInterface1 contents = distanceLog, expectedDistance, setDistance, proximity
anInterface2 type = Interface
anInterface2 schemas = dtmi:example:numericArray;1
anInterface2 contents = distanceLog, expectedDistance, setDistance, proximity
```

The two objects have the same type, the same single schema identifer in their schemas property, the same set of content names in their contents property, and no other properties.
Consequently, the `==` operator will show them as equal:

```C# Snippet:DtdlParserSample09_ShallowCompareInterfaces
Console.WriteLine($"Interface anInterface1 == anInterface2 = {anInterface1 == anInterface2}");
```

This snippet displays:

```Console
Interface anInterface1 == anInterface2 = True
```

We can do a deep comparison by using the `DeepEquals()` method:

```C# Snippet:DtdlParserSample09_DeepCompareInterfaces
Console.WriteLine($"Interface anInterface1.DeepEquals(anInterface2) = {anInterface1.DeepEquals(anInterface2)}");
```

This snippet displays:

```Console
Interface anInterface1.DeepEquals(anInterface2) = False
```

This is expected because there are a number of differences in the full object hierarchies under the two Interfaces, as we shall see below.

## Compare elements with same identifier but different property values

The two Arrays in the schemas of each Interface have the same identifier, dtmi:example:numericArray;1.

```C# Snippet:DtdlParserSample09_GetArraysById
var numericArrayId = new Dtmi("dtmi:example:numericArray;1");
var numericArray1 = (DTArrayInfo)objectModel1[numericArrayId];
var numericArray2 = (DTArrayInfo)objectModel2[numericArrayId];
```

However, as can be seen in the JSON text above, these Arrays have different values for property elementSchema.
The first has value double, and the second has value integer.
Therefore, the two Arrays are not equal, as the `==` operator and the `DeepEquals()` method both show:

```C# Snippet:DtdlParserSample09_CompareArrays
Console.WriteLine($"Array numericArray1 == numericArray2 = {numericArray1 == numericArray2}");
Console.WriteLine($"Array numericArray1.DeepEquals(numericArray2) = {numericArray1.DeepEquals(numericArray2)}");
```

This snippet displays:

```Console
Array numericArray1 == numericArray2 = False
Array numericArray1.DeepEquals(numericArray2) = False
```

## Compare elements with same shallow property values but different deep values

The situation for the two Telemetries is analogous to the situation for the Interfaces.
We can retrieve the `DTTelemetryInfo` objects using `Linq` to extract each "contents" value by name:

```C# Snippet:DtdlParserSample09_QueryTelemetriesByName
var distanceLog1 = (DTTelemetryInfo)anInterface1.Contents.Values.First(c => c.Name == "distanceLog");
var distanceLog2 = (DTTelemetryInfo)anInterface2.Contents.Values.First(c => c.Name == "distanceLog");
```

Looking at the JSON text above, the Telemetries appear identical:

```JSON
{
  "@type": "Telemetry",
  "name": "distanceLog",
  "schema": "dtmi:example:numericArray;1",
  "displayName": "distance log"
}
```

However, the definition of dtmi:example:numericArray;1 is different in the two object models, as we saw above.
In the first object model, this identifer is defined as Array of double; in the second, it is defined as an Array of integer.

Therefore, the two Telemetries are shallowly equal but not deeply equal:

```C# Snippet:DtdlParserSample09_CompareTelemetries
Console.WriteLine($"Telemetry distanceLog1 == distanceLog2 = {distanceLog1 == distanceLog2}");
Console.WriteLine($"Telemetry distanceLog1.DeepEquals(distanceLog2) = {distanceLog1.DeepEquals(distanceLog2)}");
```

This snippet displays:

```Console
Telemetry distanceLog1 == distanceLog2 = True
Telemetry distanceLog1.DeepEquals(distanceLog2) = False
```

## Compare elements with properties in different order

Looking at the JSON text above, the Properties have the same property values, but they are in a different order.
Order is irrelevant to the equality comparison, so the two Properties are equal, as the `==` operator and the `DeepEquals()` method both show:

```C# Snippet:DtdlParserSample09_QueryAndCompareProperties
var expectedDistance1 = (DTPropertyInfo)anInterface1.Contents.Values.First(c => c.Name == "expectedDistance");
var expectedDistance2 = (DTPropertyInfo)anInterface2.Contents.Values.First(c => c.Name == "expectedDistance");

Console.WriteLine($"Property expectedDistance1 == expectedDistance2 = {expectedDistance1 == expectedDistance2}");
Console.WriteLine($"Property expectedDistance1.DeepEquals(expectedDistance2) = {expectedDistance1.DeepEquals(expectedDistance2)}");
```

This snippet displays:

```Console
Property expectedDistance1 == expectedDistance2 = True
Property expectedDistance1.DeepEquals(expectedDistance2) = True
```

## Compare elements with different shallow property values but same deep values

The situation for the two Commands is opposite the situation for the two Telemetries.
As we will see below, the two Command Requests compare as equal both shallowly and deeply, and the two Command Responses compare as equal both shallowly and deeply.
Since the Commands also have the same name, same type, and no additional properties, we might expect that they would compare as equal.
However, things appear different when we take a shallow look at the two `DTCommandInfo` objects:

```C# Snippet:DtdlParserSample09_DisplayCommandProperties
var setDistance1 = (DTCommandInfo)anInterface1.Contents.Values.First(c => c.Name == "setDistance");
var setDistance2 = (DTCommandInfo)anInterface2.Contents.Values.First(c => c.Name == "setDistance");

Console.WriteLine($"Command setDistance1 name = {setDistance1.Name}");
Console.WriteLine($"Command setDistance1 type = {setDistance1.EntityKind}");
Console.WriteLine($"Command setDistance1 request = {setDistance1.Request.Id}");
Console.WriteLine($"Command setDistance1 response = {setDistance1.Response.Id}");

Console.WriteLine($"Command setDistance2 name = {setDistance2.Name}");
Console.WriteLine($"Command setDistance2 type = {setDistance2.EntityKind}");
Console.WriteLine($"Command setDistance2 request = {setDistance2.Request.Id}");
Console.WriteLine($"Command setDistance2 response = {setDistance2.Response.Id}");
```

This snippet displays:

```Console
Command setDistance1 name = setDistance
Command setDistance1 type = Command
Command setDistance1 request = dtmi:example:anInterface:_contents:__setDistance:_request;1
Command setDistance1 response = dtmi:example:anInterface:_contents:__setDistance:_response;1
Command setDistance2 name = setDistance
Command setDistance2 type = Command
Command setDistance2 request = dtmi:example:desiredDistance;1
Command setDistance2 response = dtmi:example:anInterface:_contents:__setDistance:_response;1
```

The name, type, and response values are the same, but the requests have different identifiers.
One identifier is generated automatically by the parser, and the other is assigned in the model.
Therefore, a shallow comparison (which does not look deeper than the identifiers of the property values) shows the Commands as unequal, but a deep comparison shows them as equal:


```C# Snippet:DtdlParserSample09_CompareCommands
Console.WriteLine($"Command setDistance1 == setDistance2 = {setDistance1 == setDistance2}");
Console.WriteLine($"Command setDistance1.DeepEquals(setDistance2) = {setDistance1.DeepEquals(setDistance2)}");
```

This snippet displays:

```Console
Command setDistance1 == setDistance2 = False
Command setDistance1.DeepEquals(setDistance2) = True
```

## Compare elements that are identical but have different identifiers

In the above section on comparing Commands, we noted that although the two Request values have different identifiers, the Requests compare as equal.
This is because the identifier of an element is not relevant to the equality comparison.
For this reason, even within a single object model it is possible for two different elements (which necessarily have different identifiers) to compare as equal.
We can demonstrate this using the `==` operator and the `DeepEquals()` method to compare the Requests:

```C# Snippet:DtdlParserSample09_CompareRequests
var desiredDistance1 = (DTCommandPayloadInfo)setDistance1.Request;
var desiredDistance2 = (DTCommandPayloadInfo)setDistance2.Request;

Console.WriteLine($"Request desiredDistance1 == desiredDistance2 = {desiredDistance1 == desiredDistance2}");
Console.WriteLine($"Request desiredDistance1.DeepEquals(desiredDistance2) = {desiredDistance1.DeepEquals(desiredDistance2)}");
```

This snippet displays:

```Console
Request desiredDistance1 == desiredDistance2 = True
Request desiredDistance1.DeepEquals(desiredDistance2) = True
```

## Compare elements with different representations of language-tagged strings

Looking at the JSON text above, the first Command Request has a displayName with value "desired distance", with no explicit language tag; therefore, in the object model it is tagged with the default language of English.
The second Command Request has a displayName with the same value "desired distance" and an explicit language tag of "en-US" for English.
In the object model, these are the same:

```C# Snippet:DtdlParserSample09_DisplayResponseDisplayNames
var reportedDistance1 = (DTCommandPayloadInfo)setDistance1.Response;
var reportedDistance2 = (DTCommandPayloadInfo)setDistance2.Response;

Console.WriteLine($"Response reportedDistance1 displayName = {{ {string.Join(", ", reportedDistance1.DisplayName.Select(kvp => $"\"{kvp.Key}\": \"{kvp.Value}\""))} }}");
Console.WriteLine($"Response reportedDistance2 displayName = {{ {string.Join(", ", reportedDistance2.DisplayName.Select(kvp => $"\"{kvp.Key}\": \"{kvp.Value}\""))} }}");
```

This snippet displays:

```Console
Response reportedDistance1 displayName = { "en": "reported distance" }
Response reportedDistance2 displayName = { "en": "reported distance" }
```

Therefore, the Responses compare as equal:

```C# Snippet:DtdlParserSample09_CompareResponses
Console.WriteLine($"Response reportedDistance1 == reportedDistance2 = {reportedDistance1 == reportedDistance2}");
Console.WriteLine($"Response reportedDistance1.DeepEquals(reportedDistance2) = {reportedDistance1.DeepEquals(reportedDistance2)}");
```

This snippet displays:

```Console
Response reportedDistance1 == reportedDistance2 = True
Response reportedDistance1.DeepEquals(reportedDistance2) = True
```

## Compare elements with different language tags

A contrast with the Responses above can be seen in the different Relationships.
Looking at the JSON text above, the first Relationship has a displayName only in English, whereas the second Relationship has a displayName in both English and Spanish.
This is a sufficient difference to cause the Relationships to be unequal:

```C# Snippet:DtdlParserSample09_QueryAndCompareRelationships
var proximity1 = (DTRelationshipInfo)anInterface1.Contents.Values.First(c => c.Name == "proximity");
var proximity2 = (DTRelationshipInfo)anInterface2.Contents.Values.First(c => c.Name == "proximity");

Console.WriteLine($"Relationship proximity1 == proximity2 = {proximity1 == proximity2}");
Console.WriteLine($"Relationship proximity1.DeepEquals(proximity2) = {proximity1.DeepEquals(proximity2)}");
```

This snippet displays:

```Console
Relationship proximity1 == proximity2 = False
Relationship proximity1.DeepEquals(proximity2) = False
```
