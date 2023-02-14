# Inspect supplemental types and properties in a DTDL v2 model

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This sample walks through an advanced aspect of the last of these uses: how to access supplemental types and properties on elements in the object model.
A [variation](./Sample13_InspectSupplementalTypesAndPropertiesV3.md) on this sample that works with DTDL v3 and the Quantitative Types extension is also available.
These two samples mainly use different code; however, the same code is used in the [final section](#observe-unit-value-independent-of-dtdl-version) of each sample, illustrating howe common code can access a supplemental property and value across substantial internal changes to DTDL versions.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserSample12_CreateModelParser
var modelParser = new ModelParser();
```

## Inspect available supplemental types

Before even parsing a model, we can inspect the supplemental types that are available in the `ModelParser` object.
The `GetSupplementalTypes()` method returns a dictionary that maps from supplemental type identifier to a `DTSupplementalTypeInfo` object containing information about the type.

```C# Snippet:DtdlParserSample12_GetSupplementalTypes
IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> supplementalTypes = modelParser.GetSupplementalTypes();
```

The model in this sample uses the supplemental type Distance, so we will begin by inspecting the Distance type's properties.
If we happen to know the identifier of the Distance type, we can use it as a key into the `supplemantalTypes` dictionary.
If we do not know the identifier, we can use `Linq` to find a key whose term is "Distance" and whose version matches the DTDL version used for the model, which is 2.

```C# Snippet:DtdlParserSample12_GetDistanceTypeInfo
DTSupplementalTypeInfo distanceTypeInfo = supplementalTypes.First(t => ModelParser.GetTermOrUri(t.Key) == "Distance" && t.Key.MajorVersion == 2).Value;
```

The `DTSupplementalTypeInfo` object contains information about the type and its properties.

```C# Snippet:DtdlParserSample12_DisplayDistanceTypeAndProperties
Console.WriteLine($"Distance type = {distanceTypeInfo.Type}");

Console.WriteLine($"Distance properties:");

foreach (KeyValuePair<string, DTSupplementalPropertyInfo> distanceProp in distanceTypeInfo.Properties)
{
    string optional = distanceProp.Value.IsOptional ? "optional" : "required";
    string plural = distanceProp.Value.IsPlural ? "plural" : "singular";
    Dtmi distancePropType = new Dtmi(distanceProp.Value.Type);
    DTExtensionKind kind = supplementalTypes[distancePropType].ExtensionKind;
    Console.WriteLine($"  {distanceProp.Key} => {distanceProp.Value.Type}, {kind}, {optional}, {plural}");
}
```

This snippet displays:

```Console
Distance type = dtmi:standard:class:Distance;2
Distance properties:
  dtmi:dtdl:property:unit;2 => dtmi:standard:class:LengthUnit;2, Unit, required, singular
```

The only property has an identifier of dtmi:dtdl:property:unit;2 and a type of dtmi:standard:class:LengthUnit;2.
This property is required on all elements that have a Distance co-type, and the property has a singular value.

In the DTDL language model, the property identifier dtmi:dtdl:property:unit;2 corresponds to the JSON property name "unit", as can be seen by using the `ModelParser.GetTermOrUri()` static method:

```C# Snippet:DtdlParserSample12_DisplayUnitTerm
Console.WriteLine(ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:property:unit;2")));
```

This snippet displays:

```Console
unit
```

## Obtain the JSON text of a DTDL model that employs a supplemental type

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.

```C# Snippet:DtdlParserSample12_ObtainDtdlText
string jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;2"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""contents"": [
    {
      ""@type"": [ ""Telemetry"", ""Distance"" ],
      ""name"": ""currentDistance"",
      ""schema"": ""double"",
      ""unit"": ""kilometre""
    }
  ]
}";
```

## Submit the JSON text to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is complete and valid, no exception will be thrown.
Proper code should catch and process exceptions as shown in other samples such as [this one](Sample02_FixInvalidDtdlModel.md), but for simplicity the present sample omits exception handling.

```C# Snippet:DtdlParserSample12_CallParse
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
```

## Display elements in object model

The object model is a collection of objects in a class hierarchy rooted at `DTEntityInfo`.
All DTDL elements derive from the DTDL abstract type Entity, and each DTDL type has a corresponding C# class whose name has a prefix of "DT" (for Digital Twins) and a suffix of "Info".
The elements in the object model are indexed by their identifiers, which have type `Dtmi`.  The following snippet displays the identifiers of all elements in the object model:

```C# Snippet:DtdlParserSample12_DisplayElements
Console.WriteLine($"{objectModel.Count} elements in model:");
foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
{
    Console.WriteLine(modelElement.Key);
}
```

For the JSON text above, this snippet displays:

```Console
6 elements in model:
dtmi:example:anInterface:_contents:__currentDistance;1
dtmi:example:anInterface;1
dtmi:dtdl:instance:Schema:double;2
dtmi:standard:unit:kilometre;2
dtmi:standard:unit:metre;2
dtmi:standard:unitprefix:kilo;2
```

Of these six identifiers, only dtmi:example:anInterface;1 is present in the DTDL source model.
The identifier for the content named "currentDistance" is auto-generated by the `ModelParser` following rules that guarantee its uniqueness.
The last four identifiers represent elements in the DTDL language model.

## Drill down to element with supplemental type

An individual element can be looked up in the object model by its identifier:

```C# Snippet:DtdlParserSample12_GetInterfaceById
var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
```

The Interface in the DTDL model above has one 'contents' value, a Telemetry that we will access by name via the `Contents` property on `DTInterfaceInfo`:

```C# Snippet:DtdlParserSample12_GetTelemetryByName
string currentDistanceName = "currentDistance";
var currentDistance = (DTTelemetryInfo)anInterface.Contents[currentDistanceName];
```

## Inspect model element types

We can observe the primary type of the `currentDistance` element from its `EntityKind` property.
To observe the supplemental types, we inspect the `SupplementalTypes` property.

```C# Snippet:DtdlParserSample12_DisplayTelemetryTypes
Console.WriteLine($"currentDistace primary type = {currentDistance.EntityKind}");
Console.WriteLine("currentDistance supplemental types = " + string.Join(", ", currentDistance.SupplementalTypes.Select(t => t.ToString())));
```

This snippet displays:

```Console
currentDistace primary type = Telemetry
currentDistance supplemental types = dtmi:standard:class:Distance;2
```

These correspond to the two values of "@type" seen in the JSON text above.

If we care to, we can map the supplemental type identifier back to the term used in the JSON text of the DTDL model by using the `ModelParser.GetTermOrUri()` static method:

```C# Snippet:DtdlParserSample12_DisplayDistanceTerm
Console.WriteLine($"term for dtmi:standard:class:Distance;2 is {ModelParser.GetTermOrUri(new Dtmi("dtmi:standard:class:Distance;2"))}");
```

This snippet displays:

```Console
term for dtmi:standard:class:Distance;2 is Distance
```

## Inspect model element properties

Native properties of the `currentDistance` element, such as "schema", can be observed directly.
Supplemental properties can be observed via the `SupplementalProperties` property.
We know from our inspection of the Distance type above that its one supplemental property has identifier dtmi:dtdl:property:unit;2.

```C# Snippet:DtdlParserSample12_DisplayTelemetryProperties
Console.WriteLine($"currentDistance schema = {currentDistance.Schema.Id}");

var currentDistanceUnit = (DTUnitInfo)currentDistance.SupplementalProperties["dtmi:dtdl:property:unit;2"];
Console.WriteLine($"currentDistance unit = {currentDistanceUnit.Id}");
```

This snippet displays:

```Console
currentDistance schema = dtmi:dtdl:instance:Schema:double;2
currentDistance unit = dtmi:standard:unit:kilometre;2
```

This is all the information included in the DTDL model submitted to `Parse()`.
However, we can dig further into the element referenced from the model.

## Inspect referenced element types

The value of the unit property is an element with identifier dtmi:standard:unit:kilometre;2.
We can observe the primary type of this element from its `EntityKind` property.
To observe the supplemental types, we inspect the `SupplementalTypes` property.

```C# Snippet:DtdlParserSample12_DisplayKilometreTypes
Console.WriteLine($"kilometre primary type = {currentDistanceUnit.EntityKind}");
Console.WriteLine("kilometre supplemental types = " + string.Join(", ", currentDistanceUnit.SupplementalTypes.Select(t => t.ToString())));
```

This snippet displays:

```Console
kilometre primary type = Unit
kilometre supplemental types = dtmi:standard:class:LengthUnit;2, dtmi:standard:class:DecimalUnit;2
```

In DTDL v2, all unit elements have types that derive from the primary type Unit, which is materialized in the object model using class `DTUnitInfo`.
The kilometre unit is also a LengthUnit, meaning that it represents a unit of length.
It is also a DecimalUnit, meaning that it is a decimal multiple of a base unit.
We can learn more about this latter aspect by inspecting the element's properties. 

## Inspect referenced element properties

Native properties of the unit element, such as "symbol", can be observed directly.
Supplemental properties can be observed via the `SupplementalProperties` property.

```C# Snippet:DtdlParserSample12_DisplayKilometreProperties
Console.WriteLine($"kilometre properties:");
Console.WriteLine($"  displayName: {currentDistanceUnit.DisplayName["en"]}");
Console.WriteLine($"  symbol: {currentDistanceUnit.Symbol}");
foreach (KeyValuePair<string, object> kvp in currentDistanceUnit.SupplementalProperties)
{
    Console.WriteLine($"  {kvp.Key}: {((DTEntityInfo)kvp.Value).Id}");
}
```

This snippet displays:

```Console
kilometre properties:
  displayName: kilometre
  symbol: km
  dtmi:dtdl:property:baseUnit;2: dtmi:standard:unit:metre;2
  dtmi:dtdl:property:prefix;2: dtmi:standard:unitprefix:kilo;2
```

There are two supplemental properties, both of which are bestowed by the DecimalUnit co-type.
These two properties, baseUnit and prefix, indicate how the kilometre unit is composed from the base unit metre and the prefix kilo.
With further inspection not shown in this sample, it is possible to see properties of these referenced elements, which convey additional information such as the decimal exponent implied by the kilo prefix.

## Observe unit value independent of DTDL version

The sample code above is specific to the Quantitative Types extension and DTDL versions above v2.
It is possible to write code that identifies the value of the unit property independently from the DTDL version, like so:

```C# Snippet:DtdlParserSample12_DisplayUnitValueIndepentOfVersion
KeyValuePair<string, object> unitProperty = currentDistance.SupplementalProperties.FirstOrDefault(p => ModelParser.GetTermOrUri(new Dtmi(p.Key)) == "unit");
if (unitProperty.Value != null)
{
    Console.WriteLine($"currentDistance has unit {ModelParser.GetTermOrUri(((DTEntityInfo)unitProperty.Value).Id)}");
}
```

This snippet displays:

```Console
currentDistance has unit kilometre
```
