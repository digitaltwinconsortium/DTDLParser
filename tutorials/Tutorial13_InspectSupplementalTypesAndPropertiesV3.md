# Inspect supplemental types and properties in a DTDL v3 model

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial walks through an advanced aspect of the last of these uses: how to access supplemental types and properties on elements in the object model.
A [variation](./Tutorial12_InspectSupplementalTypesAndPropertiesV2.md) on this tutorial that works with DTDL v2 is also available.
These two tutorials mainly use different code; however, the same code is used in the [final section](#observe-unit-value-independent-of-dtdl-version) of each tutorial, illustrating howe common code can access a supplemental property and value across substantial internal changes to DTDL versions.

## Create a ModelParser

To parse a DTDL model, you need to instantiate a `ModelParser`.
No arguments are required.

```C# Snippet:DtdlParserTutorial13_CreateModelParser
var modelParser = new ModelParser();
```

## Inspect available supplemental types

Before even parsing a model, we can inspect the supplemental types that are available in the `ModelParser` object.
The `GetSupplementalTypes()` method returns a dictionary that maps from supplemental type identifier to a `DTSupplementalTypeInfo` object containing information about the type.

```C# Snippet:DtdlParserTutorial13_GetSupplementalTypes
IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> supplementalTypes = modelParser.GetSupplementalTypes();
```

The model in this tutorial uses the supplemental type Distance, which is defined in the Quantitative Types extension, so we will begin by inspecting the Distance type's properties.
If we happen to know the identifier of the Distance type, we can use it as a key into the `supplemantalTypes` dictionary.
If we do not know the identifier, we can use `Linq` to find a key/value pair whose key has term "Distance" and whose value indicates a type defined by the Quantitative Types context.

```C# Snippet:DtdlParserTutorial13_GetDistanceTypeInfo
Dtmi quantitativeTypesContext = new Dtmi("dtmi:dtdl:extension:quantitativeTypes;1");
DTSupplementalTypeInfo distanceTypeInfo = supplementalTypes.First(t => ModelParser.GetTermOrUri(t.Key) == "Distance" && t.Value.ContextId == quantitativeTypesContext).Value;
```

The `DTSupplementalTypeInfo` object contains information about the type and its properties.

```C# Snippet:DtdlParserTutorial13_DisplayDistanceTypeAndProperties
Console.WriteLine($"Distance type = {distanceTypeInfo.Type}");

Console.WriteLine($"Distance properties:");

foreach (KeyValuePair<string, DTSupplementalPropertyInfo> distanceProp in distanceTypeInfo.Properties)
{
    string optional = distanceProp.Value.IsOptional ? "optional" : "required";
    string plural = distanceProp.Value.IsPlural ? "plural" : "singular";
    string distanceType = ModelParser.GetTermOrUri(distanceProp.Value.Type);
    Console.WriteLine($"  {distanceProp.Key} => {distanceType}, {optional}, {plural}");
    Console.WriteLine($"    child of: {distanceProp.Value.ChildOf}");
}
```

This snippet displays:

```Console
Distance type = dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance
Distance properties:
  dtmi:dtdl:extension:quantitativeTypes:v1:property:unit => EnumValue, required, singular
    child of: dtmi:dtdl:extension:quantitativeTypes:v1:enum:LengthUnit
```

The only property has an identifier of dtmi:dtdl:extension:quantitativeTypes:v1:property:unit and a type of EnumValue.
This property is required on all elements that have a Distance co-type, and the property has a singular value.

In the QuantitativeTypes extension, all units are defined as instances of EnumValue.
Each quantitative type's "unit" property further restricts its values to those defined as children of some specific Enum instance.
In the case of the Distance type, this Enum instance is dtmi:dtdl:extension:quantitativeTypes:v1:enum:LengthUnit.

In the Quantitative Types extension, the property identifier dtmi:dtdl:extension:quantitativeTypes:v1:property:unit corresponds to the JSON property name "unit", as can be seen by using the `ModelParser.GetTermOrUri()` static method:

```C# Snippet:DtdlParserTutorial13_DisplayUnitTerm
Console.WriteLine(ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit")));
```

This snippet displays:

```Console
unit
```

## Inspect available unit values

Still without needing to parse a model, we can inspect the elements defined by DTDL, by the standard extensions, and by any partner or feature extension that is directly supported by the `ModelParser`.

The `GetImplicitElements()` method returns an object model of elements that are implicitly available for reference by any model that could be submitted to the `Parse()` or `ParseAsync()` methods.
The type of the returned object model matches the type returned by the parsing methods.

```C# Snippet:DtdlParserTutorial13_GetImplicitElement
IReadOnlyDictionary<Dtmi, DTEntityInfo> implicitElements = modelParser.GetImplicitElements();
```

The output above showed that the Distance type has a "unit" property whose type is EnumValue and whose values are children of the Enum dtmi:dtdl:extension:quantitativeTypes:v1:enum:LengthUnit.

We can display the terms for all EnumValue elements defined under the LengthUnit Enum.

```C# Snippet:DtdlParserTutorial13_DisplayLengthUnits
Dtmi lengthUnitId = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:LengthUnit");
DTEnumInfo lengthUnitEnum = (DTEnumInfo)implicitElements[lengthUnitId];
foreach (DTEnumValueInfo enumVal in lengthUnitEnum.EnumValues)
{
    Console.WriteLine(ModelParser.GetTermOrUri(enumVal.Id));
}
```

This snippet displays:

```Console
metre
centimetre
millimetre
micrometre
nanometre
kilometre
foot
inch
mile
nauticalMile
astronomicalUnit
```

These are the permitted values for the "unit" property of an element that has co-type Distance.
The model presented in the next section has one of these values (kilometre) for its "unit" property.

## Obtain the JSON text of a DTDL model that employs a supplemental type

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.

```C# Snippet:DtdlParserTutorial13_ObtainDtdlText
string jsonText =
@"{
  ""@context"": [
    ""dtmi:dtdl:context;3"",
    ""dtmi:dtdl:extension:quantitativeTypes;1""
  ],
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
Proper code should catch and process exceptions as shown in other tutorials such as [this one](Tutorial02_FixInvalidDtdlModel.md), but for simplicity the present tutorial omits exception handling.

```C# Snippet:DtdlParserTutorial13_CallParse
IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
```

## Display elements in object model

The object model is a collection of objects in a class hierarchy rooted at `DTEntityInfo`.
All DTDL elements derive from the DTDL abstract type Entity, and each DTDL type has a corresponding C# class whose name has a prefix of "DT" (for Digital Twins) and a suffix of "Info".
The elements in the object model are indexed by their identifiers, which have type `Dtmi`.  The following snippet displays the identifiers of all elements in the object model:

```C# Snippet:DtdlParserTutorial13_DisplayElements
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
dtmi:dtdl:extension:quantitativeTypes:v1:unit:kilometre
dtmi:dtdl:extension:quantitativeTypes:v1:unit:metre
dtmi:dtdl:extension:quantitativeTypes:v1:unitprefix:kilo
```

Of these six identifiers, only dtmi:example:anInterface;1 is present in the DTDL source model.
The identifier for the content named "currentDistance" is auto-generated by the `ModelParser` following rules that guarantee its uniqueness.
The identifier for the term "double" is defined by the DTDL language model.
The last three identifiers represent elements in the Quantitative Types extension.

## Drill down to element with supplemental type

An individual element can be looked up in the object model by its identifier:

```C# Snippet:DtdlParserTutorial13_GetInterfaceById
var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
```

The Interface in the DTDL model above has one 'contents' value, a Telemetry that we will access by name via the `Contents` property on `DTInterfaceInfo`:

```C# Snippet:DtdlParserTutorial13_GetTelemetryByName
string currentDistanceName = "currentDistance";
var currentDistance = (DTTelemetryInfo)anInterface.Contents[currentDistanceName];
```

## Inspect model element types

We can observe the primary type of the `currentDistance` element from its `EntityKind` property.
To observe the supplemental types, we inspect the `SupplementalTypes` property.

```C# Snippet:DtdlParserTutorial13_DisplayTelemetryTypes
Console.WriteLine($"currentDistace primary type = {currentDistance.EntityKind}");
Console.WriteLine("currentDistance supplemental types = " + string.Join(", ", currentDistance.SupplementalTypes.Select(t => t.ToString())));
```

This snippet displays:

```Console
currentDistace primary type = Telemetry
currentDistance supplemental types = dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance
```

These correspond to the two values of "@type" seen in the JSON text above.

If we care to, we can map the supplemental type identifier back to the term used in the JSON text of the DTDL model by using the `ModelParser.GetTermOrUri()` static method:

```C# Snippet:DtdlParserTutorial13_DisplayDistanceTerm
Console.WriteLine($"term for dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance is {ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance"))}");
```

This snippet displays:

```Console
term for dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance is Distance
```

## Inspect model element properties

Native properties of the `currentDistance` element, such as "schema", can be observed directly.
Supplemental properties can be observed via the `SupplementalProperties` property.
We know from our inspection of the Distance type above that its one supplemental property has identifier dtmi:dtdl:extension:quantitativeTypes:v1:property:unit.

```C# Snippet:DtdlParserTutorial13_DisplayTelemetryProperties
Console.WriteLine($"currentDistance schema = {currentDistance.Schema.Id}");

var currentDistanceUnit = (DTEnumValueInfo)currentDistance.SupplementalProperties["dtmi:dtdl:extension:quantitativeTypes:v1:property:unit"];
Console.WriteLine($"currentDistance unit = {currentDistanceUnit.Id}");
```

This snippet displays:

```Console
currentDistance schema = dtmi:dtdl:instance:Schema:double;2
currentDistance unit = dtmi:dtdl:extension:quantitativeTypes:v1:unit:kilometre
```

This is all the information included in the DTDL model submitted to `Parse()`.
However, we can dig further into the element referenced from the model.

## Inspect referenced element types

The value of the unit property is an element with identifier dtmi:dtdl:extension:quantitativeTypes:v1:unit:kilometre.
We can observe the primary type of this element from its `EntityKind` property.
To observe the supplemental types, we inspect the `SupplementalTypes` property.

```C# Snippet:DtdlParserTutorial13_DisplayKilometreTypes
Console.WriteLine($"kilometre primary type = {currentDistanceUnit.EntityKind}");
Console.WriteLine("kilometre supplemental types:");
foreach (Dtmi kilometreSupplementalType in currentDistanceUnit.SupplementalTypes)
{
    Console.WriteLine($"  {kilometreSupplementalType}");
}
```

This snippet displays:

```Console
kilometre primary type = EnumValue
kilometre supplemental types:
  dtmi:dtdl:extension:quantitativeTypes:v1:class:SymbolicUnit
  dtmi:dtdl:extension:quantitativeTypes:v1:class:DecimalUnit
```

In the QuantitativeTypes extension, all units have type EnumValue, which is materialized in the object model using class `DTEnumValueInfo`.

The kilometre unit is also a SymbolicUnit, meaning that it has the semantic meaning of a unit and that it has an associated symbol.
It is also a DecimalUnit, meaning that it is a decimal multiple of a base unit.

We can learn more about these two aspects by inspecting the element's properties.

## Inspect referenced element properties

Native properties of the unit element, such as "name", can be observed directly.
Supplemental properties can be observed via the `SupplementalProperties` property.

```C# Snippet:DtdlParserTutorial13_DisplayKilometreProperties
Console.WriteLine($"kilometre properties:");
Console.WriteLine($"  name: {currentDistanceUnit.Name}");
Console.WriteLine($"  displayName: {currentDistanceUnit.DisplayName["en"]}");
foreach (KeyValuePair<string, object> kvp in currentDistanceUnit.SupplementalProperties)
{
    Console.WriteLine($"  {kvp.Key}: {(kvp.Value is DTEntityInfo entity ? entity.Id : kvp.Value)}");
}
```

This snippet displays:

```Console
kilometre properties:
  name: kilometre
  displayName: kilometre
  dtmi:dtdl:property:symbol;3: km
  dtmi:dtdl:extension:quantitativeTypes:v1:property:baseUnit: dtmi:dtdl:extension:quantitativeTypes:v1:unit:metre
  dtmi:dtdl:extension:quantitativeTypes:v1:property:prefix: dtmi:dtdl:extension:quantitativeTypes:v1:unitprefix:kilo
```

One supplemental property, symbol, is bestowed by the SymbolicUnit co-type.
Two other supplemental properties are bestowed by the DecimalUnit co-type.
These two properties, baseUnit and prefix, indicate how the kilometre unit is composed from the base unit metre and the prefix kilo.
With further inspection not shown in this tutorial, it is possible to see properties of these referenced elements, which convey additional information such as the decimal exponent implied by the kilo prefix.

## Observe unit value independent of DTDL version

The tutorial code above is specific to the Quantitative Types extension and DTDL versions above v2.
It is possible to write code that identifies the value of the unit property independently from the DTDL version, like so:

```C# Snippet:DtdlParserTutorial13_DisplayUnitValueIndepentOfVersion
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
