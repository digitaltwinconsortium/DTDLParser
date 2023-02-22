# Pinpoint errors in resolved models

The `ModelParser` class is used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
This tutorial illustrates how to deal with models that are not self-contained and that have modeling errors.
An [asynchronous version](./Tutorial12_PinpointErrorsInResolvedModelsAsync.md) of this tutorial is also available.

## Obtain the JSON text of a DTDL model that references another model

The DTDL language is syntactically JSON.
The `ModelParser` expects a single string or an enumeration of strings.
The single string or each value in the enumeration is JSON text of a DTDL model.
The following model contains an external reference.

```C# Snippet:DtdlParserTutorial11_ObtainInvalidDtdlText
string jsonText =
@"{
  ""@context"": ""dtmi:dtdl:context;3"",
  ""@id"": ""dtmi:example:anInterface;1"",
  ""@type"": ""Interface"",
  ""extends"": ""dtmi:example:anotherInterface;1"",
  ""contents"": [
    {
      ""@type"": ""Property"",
      ""name"": ""currentDistance"",
      ""schema"": ""double""
    }
  ]
}";
```

The Interface's "extends" property has value "dtmi:example:anotherInterface;1", which is an identifier that is not defined in the model.
The parser is unable to fully validate the model without a definition for this referenced Interface.

## Obtain the JSON text of referenced DTDL model

We will store the JSON text of the referenced model in a dictionary keyed on the identifier.

```C# Snippet:DtdlParserTutorial11_ObtainReferencedDtdlText
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

## Create a ModelParser with a DtmiResolver registered

As described and illustrated in the [Resolve external references](Tutorial11_ResolveExternalReferences.md) tutorial, a `DtmiResolver` is a delegate that the `ModelParser` calls whenever it encounters an external reference to an identifier that requires a definition.
We can write a simple resolver and register it with an instance of `ModelParser` as follows:

```C# Snippet:DtdlParserTutorial11_CreateModelParserWithDtmiResolver
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

var modelParser = new ModelParser(new ParsingOptions { DtmiResolver = dtmiResolver });
```

## Submit the JSON text to the ModelParser

The main synchronous method on the `ModelParser` is `Parse()`.
One argument is required, which can be either a string or an enumeration of strings containing the JSON text to parse as DTDL.
If the submitted model is invalid, a `ParsingException` will be thrown.

```C# Snippet:DtdlParserTutorial11_CallParse
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
    Console.WriteLine("DTDL model is invalid:");
    foreach (ParsingError err in ex.Errors)
    {
        Console.WriteLine(err);
    }
}
```

## Observe parsing errors

The `ParsingException` has a property named `Errors` that is a collection of `ParsingError` objects, each of which provides details about one error in the submitted model.
The `ParsingError` class has several properties that can be programmatically inspected to obtain details of the error.
This class also overrides the `ToString()` method to provide a human-readable description of the error.

For the JSON text above, the code snippet above will display a single error:

```Console
DTDL model is invalid:
dtmi:example:anInterface;1, because it transitively 'extends' dtmi:example:anotherInterface;1, has property 'contents' that contains more than one element for which property 'name' has value 'currentDistance'. Either change the value of property 'name' to a value that is unique across all values of 'contents', or remove one or more 'extends' properties so that 'contents' will not be imported.
```

This exception indicates two things:
1. In the Interface dtmi:example:anInterface;1, property "contents" contains duplicate uses of name "currentDistance", which is invalid.
2. This duplication is a result of the fact that dtmi:example:anInterface;1 extends dtmi:example:anotherInterface;1.

Looking at the JSON texts above, we see that elements dtmi:example:anInterface;1 and dtmi:example:anotherInterface;1 both have "contents" values with name "currentDistance".
Since dtmi:example:anInterface;1 extends dtmi:example:anotherInterface;1, the contents of the latter are imported into the former, so there is a duplication among the contents values of dtmi:example:anInterface;1.

## Create a DtdlParseLocator

The error message above is quite specific, but it required some effort to track down the locations of the duplicate values, and this effort would have been greater if a larger number of models had been parsed and/or resolved.
Without support from the calling code, the `ModelParser` can only report an error's location with reference to a DTDL element with an "@id" property, which may not even be the element immediately containing the error, since this element might not have an "@id" property.

In many cases, the `ModelParser` can identify which of the submitted JSON text strings contains the error, as well as the line number or range within the text string.
As described in the [Pinpoint errors in an invalid DTDL model](Tutorial03_PinpointModelingErrors.md) tutorial, the calling code can provide a callback delegate that converts this internal location into a location that is understandable by the user, which can greatly improve the clarity of the error description.

An example of such a delegate is the following:

```C# Snippet:DtdlParserTutorial11_CreateDtdlParseLocator
DtdlParseLocator parseLocator = (
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

When the `ModelParser` encounters an error in JSON text submitted to `Parse()`, it calls the delegate with a `parseIndex` indicating the index of a submitted text string and a `parseLine` indicating a line number within this string.
If the delegate is able to convert these values into a user-meaningful location, it should set the out-parameter `sourceName` to the name of the appropriate source file, URL, etc.; set the out-parameter `sourceLine` to the corresponding line number within this source; and return a value of true.
If it is not able to perform the conversion, it should return a value of false.

## Resubmit the JSON text to the ModelParser

The `ModelParser.Parse()` method takes an optional second argument, which is a `DtdlParseLocator`, such as the one defined above.

```C# Snippet:DtdlParserTutorial11_CallParseWithLocator
try
{
    var objectModel = modelParser.Parse(jsonText, parseLocator);
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

When the original JSON text is resubmitted via the code snippet above, it displays the same output as before:

```Console
DTDL model is invalid:
dtmi:example:anInterface;1, because it transitively 'extends' dtmi:example:anotherInterface;1, has property 'contents' that contains more than one element for which property 'name' has value 'currentDistance'. Either change the value of property 'name' to a value that is unique across all values of 'contents', or remove one or more 'extends' properties so that 'contents' will not be imported.
```

There is no improvement in the description because the `ModelParser` cannot fully locate all relevant lines of the error.
The error occurred partly due to the name "currentDistance" in the submitted JSON text, but also due to the name "currentDistance" in the resolved definition.

## Register a DtdlResolveLocator with the ModelParser

To enable the parser to locate all relevant lines, we can define a `DtdlResolveLocator` and register it with the parser in the constructor.

In many cases, the `ModelParser` can identify which of the externally resolved identifiers has a definition that contains the error, as well as the line number or range within the text string returned by the resolver.
The calling code can provide a callback delegate that converts this internal location into a location that is understandable by the user, which can greatly improve the clarity of the error description.

We can write such a delegate and register it with the parser as follows:

```C# Snippet:DtdlParserTutorial11_RegisterDtdlResolveLocator
DtdlResolveLocator resolveLocator = (
    Dtmi resolveDtmi,
    int resolveLine,
    out string sourceName,
    out int sourceLine) =>
{
    sourceName = $"dictionary entry 'otherJsonTexts[new Dtmi(\"{resolveDtmi}\")]'";
    sourceLine = resolveLine;
    return true;
};

modelParser = new ModelParser(new ParsingOptions { DtmiResolver = dtmiResolver, DtdlResolveLocator = resolveLocator });
```

When the `ModelParser` encounters an error in a definition returned by the `DtmiResolver`, it calls the delegate with a `resolveDtmi` indicating the identifer that was resolved with the relevant definition and a `resolveLine` indicating a line number within this definition.
If the delegate is able to convert these values into a user-meaningful location, it should set the out-parameter `sourceName` to the name of the appropriate source file, URL, etc.; set the out-parameter `sourceLine` to the corresponding line number within this source; and return a value of true.
If it is not able to perform the conversion, it should return a value of false.

## Re-resubmit the original JSON text to the ModelParser

[repeat]: # (Snippet:DtdlParserTutorial11_CallParseWithLocator)

With the `DtdlResolveLocator` registered, we can resubmit the original JSON text to the `ModelParser` by re-executing the code snippet that calls `Parse()` with the `DtdlParseLocator`.
With both locators now available, the code snippet displays:

```Console
DTDL model is invalid:
Property 'name' has value 'currentDistance' on line 9 in string variable 'jsonText' and on line 8 in dictionary entry 'otherJsonTexts[new Dtmi("dtmi:example:anotherInterface;1")]', which is a uniqueness violation because dtmi:example:anInterface;1 transitively 'extends' dtmi:example:anotherInterface;1. Either change the value of property 'name' to a value that is unique across all values of 'contents', or remove one or more 'extends' properties so that 'contents' will not be imported.
```

Looking at the JSON texts above, we see that the text in string variable `jsonText` has a "name" property on line 9.
We also see that the `otherJsonTexts` dictionary entry for key dtmi:example:anotherInterface;1" has text with a "name" property on line 8.
Furthermore, both of these properties have the value "currentDistance".
In addition, we see that the text in string variable `jsonText` has an "extends" property on line 5.
Relative to the previous error message, the location information in this message more directly pinpoints the location of the error.

## Fix error and resubmit again

To correct this error, we can change the value "currentDistance" in the submitted JSON text to "expectedDistance":

```C# Snippet:DtdlParserTutorial11_CorrectPropertyName
jsonText =
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

[repeat]: # (Snippet:DtdlParserTutorial11_CallParseWithLocator)

When this JSON text is submitted to the code snippet above, it displays:

```Console
DTDL model is valid!
```
