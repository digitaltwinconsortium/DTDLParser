import { dotnet } from './dotnet.js'
import { InterfaceInfo } from "./interfaceInfo.js"

const { getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const config = getConfig()
const assemblyExports = await getAssemblyExports(config.mainAssemblyName)
await dotnet.run();

const parserVersionDiv = document.getElementById('parserVersion')
const el = document.getElementById('dtdl-text')
const out = document.getElementById('out')
const applog = (...args) => out.innerHTML += args.concat() + '\n'

parserVersionDiv.innerText = assemblyExports.DtdlParserJSInterop.ModelParserInterop.ParserVersion()

const validate = () => {
    out.innerHTML = '.. parsing ..'
    out.style.color = 'grey'
    let parseResult = ''
    try {
        parseResult = JSON.parse(assemblyExports.DtdlParserJSInterop.ModelParserInterop.Parse(el.value))
        console.log(parseResult)
        out.innerHTML = ''
        const dtdlOm = InterfaceInfo(parseResult, 'dtmi:com:example;1')
        applog('Root dtmi:com:example;1')
        dtdlOm.print(applog)
        dtdlOm.components.forEach(co => {
            applog(`[Co] ${co.name} ${co.schema}`)
            const coInfo = InterfaceInfo(parseResult, co.schema)
            coInfo.print(applog)
        })
        //out.innerHTML = JSON.stringify(parseResult, null, 2)
        out.style.color = 'black'
    }
    catch (err) {
        console.error(err)
        out.innerHTML = JSON.stringify(JSON.parse(err.message), null, 2)
        out.style.color = 'red'
    }
}
el.onchange = validate
validate()

