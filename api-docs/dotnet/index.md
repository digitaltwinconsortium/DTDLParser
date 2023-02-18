# DTDLParser for .NET

The Digital Twins Definition Language ([DTDL][language_docs]) is a language for describing models and interfaces for IoT digital twins.
Digital twins are models of entities from the physical environment such as shipping containers, rooms, factory floors, or logical entities that participate in IoT solutions.
Using DTDL to describe a digital twin's capabilities enables any IoT platform to leverage the semantics of the entity.

The DTDLParser is a library that can be used to determine whether one or more JSON documents are valid DTDL [v2](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v2/DTDL.v2.md) or [v3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) models, to identify specific modeling errors, and to enable inspection of model contents.

Details of the DTDLParser API can be found in the [API documentation](api/DTDLParser.html).
A collection of [sample tutorials][samples_readme] describes how to use the API across a broad set of use cases, ranging from basic to advanced.

The NuGet package for the DTDLParser library is available via its [NuGet feed][package], and the library source code can be found in its [GitHub repo][parser_repo].

<!-- LINKS -->
[package]: https://www.nuget.org/packages/DTDLParser/
[language_docs]: https://github.com/Azure/opendigitaltwins-dtdl
[parser_repo]: https://github.com/digitaltwinconsortium/DTDLParser
[samples_readme]: https://github.com/digitaltwinconsortium/DTDLParser/blob/main/samples/README.md
