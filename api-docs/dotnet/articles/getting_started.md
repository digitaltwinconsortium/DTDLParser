# DTDLParser for .NET

## Getting started

### Install the package

Install the DTDL Parser library for .NET with:

```bash
dotnet add package DTDLParser
```

### Parse and validate a DTDL interface

Following is a simple DTDL Interface:

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

This Interface can be parsed, validated, and displayed with the following code:

```cs
var parser = new ModelParser();
var result = parser.Parse(dtdl);
result.Values.ToList().ForEach(i => Console.WriteLine(i.Id));
```

The output shows the element IDs defined or referenced in the DTDL model:

```text
dtmi:com:example:Thermostat:_contents:__temperature
dtmi:com:example:Thermostat
dtmi:dtdl:instance:Schema:double;2
```

Check out the [samples][samples_readme] for more advanced and detailed use cases.

<!-- LINKS -->
[package]: https://www.nuget.org/packages/DTDLParser/
[samples_readme]: https://github.com/digitaltwinconsortium/DTDLParser/blob/main/samples/README.md