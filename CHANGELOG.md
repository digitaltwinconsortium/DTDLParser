# DTDLParser Release History

## 1.0.37-preview (2023-03-02)

- Multi-target netstandard2.0 & netstandard2.1 #50
- Adds ExtendsBy #59
- Adds breakout properties #47
- Validates MaxDtdlVersion #55
- Adds Overriding extension #71

### Supported extension contexts

The chart below itemizes the extension contexts supported by this version.

| Extension | Context specifier | DTDL versions |
| --- | --- | --- |
| [Iotcentral v2](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v2/DTDL.iotcentral.v2.md) | `dtmi:iotcentral:context;2` | [2](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v2/DTDL.v2.md) |
| [QuantitativeTypes v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.quantitativeTypes.v1.md) | `dtmi:dtdl:extension:quantitativeTypes;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |
| [Historization v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.historization.v1.md) | `dtmi:dtdl:extension:historization;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |
| [Streaming v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.streaming.v1.md) | `dtmi:dtdl:extension:streaming;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |
| [Annotation v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.annotation.v1.md) | `dtmi:dtdl:extension:annotation;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |
| [Overriding v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.overriding.v1.md) | `dtmi:dtdl:extension:overriding;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |

## 1.0.12-preview (2023-02-24)

- Adds `ParseToJson` APIs to support JavaScript scenarios
- Adds Tutorials (generated) and Samples (manual)
- Adds WASM Sample to show how to consume the parser from JavaScript Browser
- Updates Links in error messages to use aka.ms links

## 1.0.4-preview (2023-02-14)

- First public preview of the new DTDLParser
- Supports DTDL V2 
- Supports DTDL V3 preview

### Supported extension contexts

The chart below itemizes the extension contexts supported by this version.

| Extension | Context specifier | DTDL versions |
| --- | --- | --- |
| [Iotcentral v2](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v2/DTDL.iotcentral.v2.md) | `dtmi:iotcentral:context;2` | [2](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v2/DTDL.v2.md) |
| [QuantitativeTypes v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.quantitativeTypes.v1.md) | `dtmi:dtdl:extension:quantitativeTypes;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |
| [Historization v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.historization.v1.md) | `dtmi:dtdl:extension:historization;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |
| [Streaming v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.streaming.v1.md) | `dtmi:dtdl:extension:streaming;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |
| [Annotation v1](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.annotation.v1.md) | `dtmi:dtdl:extension:annotation;1` | [3](https://github.com/Azure/opendigitaltwins-dtdl/blob/master/DTDL/v3/DTDL.v3.md) |