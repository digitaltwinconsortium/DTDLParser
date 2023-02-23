# DTDLParserJSInteropSample

This sample uses .NET 7 JSInterop to invoke the parser from JavaScript running in a browser.

To run this sample you need the `wasm-tools` workload, install it with:

```bash
dotnet workload install wasm-tools
```

## JS Interop Layer

The [ModelParserInterop.cs](./DTDLParserJSInteropSample/ModelParserInterop.cs) exports the `ParseToJson` to JavaScript:

```cs
[JSExport]
public static string Parse(string dtdl) => new ModelParser().ParseToJson(dtdl);
```

To be called using `dotnet.js`

```js
import { dotnet } from './dotnet.js'
const { getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const config = getConfig()
const assemblyExports = await getAssemblyExports(config.mainAssemblyName)
await dotnet.run();
assemblyExports.DtdlParserJSInterop.ModelParserInterop.Parse(dtdl)
```

## DTDL Object Model Typings

The result of Parse is a JavaScript `Map<dtmi,entityInfo>`, equivalent to the .NET dictionary returned by ModelParser.Parse. To consume this structure we provide typings (`.d.ts` files) that can be used from modern code editors such as VSCode or VisualStudio by decorating the variables. These files are available in the [javascript folder](../javascript/) (and also copied to the sample project).

To provide an object model to enumerate the elements included in DTDL interface, the function `InterfaceInfo.js` provides an API to query the Telemetry, Properties, Commands, Components and Relationships.

```js
/** @type {Array<import("./DtdlOM").TelemetryInfo>} */
const telemetries = []

/** @type {Array<import("./DtdlOM").PropertyInfo>} */
const properties = []

/** @type {Array<import("./DtdlOM").CommandInfo>} */
const commands = []

/** @type {Array<import("./DtdlOM").ComponentInfo>} */
const components = []

/** @type {Array<import("./DtdlOM").RelationshipInfo>} */
const relationships = []
```

That can be used as:

```js
const parseResult = JSON.parse(assemblyExports.DtdlParserJSInterop.ModelParserInterop.Parse(el.value))
const dtdlOm = InterfaceInfo(parseResult, 'dtmi:com:example;1')
dtdlOm.telemetries.forEach(t => console.log(t))
```