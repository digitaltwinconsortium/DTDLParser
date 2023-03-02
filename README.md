# DTDLParser for .NET
> .NET Standard 2.0 (C#) library

[![CI](https://github.com/digitaltwinconsortium/DTDLParser/actions/workflows/ci.yml/badge.svg)](https://github.com/digitaltwinconsortium/DTDLParser/actions/workflows/ci.yml)
[![CD](https://github.com/digitaltwinconsortium/DTDLParser/actions/workflows/cd.yml/badge.svg)](https://github.com/digitaltwinconsortium/DTDLParser/actions/workflows/cd.yml)
[![Nuget](https://img.shields.io/nuget/v/DTDLParser?label=DTDLParser&style=plastic)](https://www.nuget.org/packages/DTDLParser)

The Digital Twins Definition Language ([DTDL][language_docs]) is a language for describing models and interfaces for IoT digital twins. Digital twins are models of entities from the physical environment such as shipping containers, rooms, factory floors, or logical entities that participate in IoT solutions. Using DTDL to describe a digital twin's capabilities enables any IoT platform to leverage the semantics of the entity.

The DTDLParser is a library that can be used to determine whether one or more models are valid according to the language [v2](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v2/DTDL.v2.md) or [v3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) specifications, to identify specific modeling errors, and to enable inspection of model contents.

[Source code][source_root] | [Package (NuGet)][package] | [DTDL language documentation][language_docs] | [DTDLParser API docs][parser_api_docs] | [Tutorials][source_tutorials] | [Samples](./samples)

## :point_right: Getting started

### :package: Install the package

Install the DTDL Parser library for .NET from [NuGet.org](https://www.nuget.org/packages/DTDLParser) with:

```bash
dotnet add package DTDLParser --prerelease
```

#### Preview Feed

[![Push Dev Package](https://github.com/digitaltwinconsortium/DTDLParser/actions/workflows/cd-dev.yml/badge.svg)](https://github.com/digitaltwinconsortium/DTDLParser/actions/workflows/cd-dev.yml)

There is also a [preview NuGet feed](https://dev.azure.com/azure-dtdl/DTDLParser/_artifacts/feed/DTDLParser-prerelease) where all commits to `main` are published as preview packages.

 ```bash
 dotnet add package DTDLParser --prerelease --source https://pkgs.dev.azure.com/azure-dtdl/DTDLParser/_packaging/DTDLParser-prerelease/nuget/v3/index.json
 ```

### :white_check_mark: Parse and validate a DTDL interface

Given the following DTDL interface:

```json
{
    "@context" : "dtmi:dtdl:context;3",
    "@type": "Interface",
    "@id": "dtmi:com:example:Thermostat",
    "contents": [
      {
        "@type": "Telemetry",
        "name": "temperature",
        "schema": "double"
      }
    ]
}
```

It can be parsed and validated with:

```cs
var parser = new ModelParser();
var result = parser.Parse(dtdl);
foreach (var i in result.Values) Console.WriteLine(i.Id);
```

The output of this program will show all the element Ids defined, or referenced in the DTDL interface:

```text
dtmi:com:example:Thermostat:_contents:__temperature
dtmi:com:example:Thermostat
dtmi:dtdl:instance:Schema:double;2
```

Check out the [Tutorials][source_tutorials] and [Samples][source_samples] for more advanced use cases.

## :key: Key concepts

### :boom: Parse and navigate the object model

The [Parse](https://digitaltwinconsortium.github.io/DTDLParser/api/DTDLParser.ModelParser.html#DTDLParser_ModelParser_Parse_System_String_DTDLParser_DtdlParseLocator_) and [ParseAsync](https://digitaltwinconsortium.github.io/DTDLParser/api/DTDLParser.ModelParser.html#DTDLParser_ModelParser_ParseAsync_System_Collections_Generic_IAsyncEnumerable_System_String__DTDLParser_DtdlParseLocator_System_Threading_CancellationToken_) methods return a `IReadOnlyDictionary<Dtmi, DTEntityInfo>` structure, to navigate a given interface cast to `DTInterfaceInfo` to access the `Components`, `Properties`, `Telemetries`, `Commands` and `Relationships`.

```cs
var parser = new ModelParser();
var result = parser.Parse(dtdl);
var thermostat = (DTInterfaceInfo)result[new Dtmi("dtmi:com:example:Thermostat")];
foreach (var t in thermostat.Telemetries) Console.WriteLine(t.Value.Name);
```

> Note: `ParseAsync` requires [Microsoft.Bcl.AsyncInterfaces](https://www.nuget.org/packages/Microsoft.Bcl.AsyncInterfaces)

### :id: Digital Twins Model Identifier (DTMI)

Every element in a DTDL model has an identifer known as a [Digital Twins Modeling Identifier (DTMI)][dtmi_spec], which is a subtype of [Universal Resource Identifier (URI)][uri_rfc].

The `Dtmi` class encapsulates all identifiers returned by the parser, including those defined in models submitted to the parser and those defined by the the DTDL language itself.

Parsing returns a dictionary whose keys are instances of the `Dtmi` class. The values in these dictionaries are instances of `DTEntityInfo` subclasses.

### :nut_and_bolt: Context identifieres

The interface above has a `@context` value of `dtmi:dtdl:context;3`, indicating that the model is written in DTDL version 3.
Every model must have at least a DTDL context specifier, and it may also have one or more *extension* context specifiers, which are itemized in in [Supported extension contexts](dotnet/src/DTDLParser/generated/SupportedExtensions.g.md).

### :eyeglasses: DtmiResolver and DtmiResolverAsync

For a DTDL model to be fully parsed, validated, and returned as an object model, the model must be complete.
If the collection of models is not self-contained but instead depends on references to identifiers without definitions, the `ModelParser` will attempt to obtain definitions by calling a registered `DtmiResolver` delegate (when executing the `Parse()` method) or a registered `DtmiResolverAsync` delegate (when executing the `ParseAsync()` method):

These delegates are registered with `ParsingOptions` when creating the instance of the `ModelParser` object.

### :warning: ParsingException and ParsingError

If an invalid model is found, a `ParsingException` will be thrown.
The `ParsingException` has a property named `Errors` that is a collection of `ParsingError` objects, each of which provides details about one error in the submitted model:

### :grey_question: DtdlParseLocator and DtdlResolveLocator

An error in a submitted model will throw a `ParsingException` containing a `ParsingError` that characterizes the error.
To enhance the parser's error reporting, the caller can provide an optional second argument, a `DtdlParseLocator` delegate, which enables the `ParsingError` to indicate the location of an error more readily.

### :triangular_flag_on_post: ValidateInstance

When a model that contains or refers to schema definitions is parsed, elements in the returned object model can be used to validate instance data for conformance with the defined schema.

For example, if a Telemetry has schema "string", or a Property has schema "integer", or a Command request has a schema that is a Map from "string" to Array of "boolean", their object-model elements can be used to validated that a Telemetry message payload contains a JSON string, that a Property value in a twin is an integer, and that the body of a Command request is a JSON object whose values are all JSON arrays whose elements are all booleans.

This validation is performed via `ValidateInstance(string)` or `ValidateInstance(JsonElement)` method of the `DTEntityInfo` class.

## :next_track_button: Next steps

For further details, see the [tutorials README][source_tutorials] and the [samples README][source_samples]].

## :woman_judge: License

This project is licensed under the MIT license. See [LICENSE](LICENSE) for more information.

## :construction_worker: Contributing

See the [Contributing][contrib] guide for details on building, testing, and contributing to this library.

This project welcomes contributions and suggestions.
Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us the rights to use your contribution.


<!-- LINKS -->
[source_root]: ./dotnet/src/DTDLParser
[source_tutorials]: ./tutorials
[language_docs]: https://github.com/Azure/opendigitaltwins-dtdl
[dtmi_spec]: https://github.com/Azure/opendigitaltwins-dtdl/tree/master/DTMI
[uri_rfc]: https://datatracker.ietf.org/doc/html/rfc3986/
[package]: https://www.nuget.org/packages/DTDLParser/
[contrib]: ./CONTRIBUTING.md
[parser_api_docs]: https://digitaltwinconsortium.github.io/DTDLParser/api/DTDLParser.html
[source_samples]: ./samples
