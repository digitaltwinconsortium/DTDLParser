import * as parser from './modelParser.js'
import { InterfaceInfo } from './interfaceInfo.js'
import { ErrorInfo } from './errorInfo.js'

const parserVersionDiv = document.getElementById('parserVersion')
const el = document.getElementById('dtdl-text')
const out = document.getElementById('out')
const applog = (...args) => out.innerHTML += args.concat()

parserVersionDiv.innerText = parser.ParserVersion()

const validate = () => {
    out.innerHTML = '.. parsing ..'
    out.style.color = 'grey'
    let parseResult = ''
    try {
        parseResult = JSON.parse(parser.Parse(el.value))
        console.log(parseResult)
        out.style.color = 'black'
        out.style.whiteSpace = 'break-spaces'
        out.innerHTML = ''
        const dtdlOm = InterfaceInfo(parseResult, 'dtmi:com:example;1')
        applog('Root dtmi:com:example;1 \n')
        dtdlOm.print(applog)
        dtdlOm.components.forEach(co => {
            applog(`[Co] ${co.name} ${co.schema} \n`)
            const coInfo = InterfaceInfo(parseResult, co.schema)
            coInfo.print(applog)
        })
    }
    catch (err) {
        out.style.color = 'red'
        out.style.whiteSpace = 'auto'
        out.innerHTML = ''
        const errorInfo = ErrorInfo(JSON.parse(err.message))
        errorInfo.print(applog)
    }
}
el.onchange = validate
validate()

