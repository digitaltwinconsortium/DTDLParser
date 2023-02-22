// @ts-check

const showProp = (/** @type import("./DtdlOM").PropertyInfo */ prop) => {
    console.log(prop.name, prop.schema, prop.writable)
}

const showTel = (/** @type import("./DtdlOM").TelemetryInfo */ tel) => {
    console.log(tel.name, tel.schema)
}

const showCmd = (/** @type import("./DtdlOM").CommandInfo */ cmd) => {

    const showCmdReq = (/** @type import("./DtdlOM").CommandRequestInfo */ req) => {
        console.log(req.name, req.schema)
    }
    
    const showCmdRes = (/** @type import("./DtdlOM").CommandResponseInfo */ res) => {
        console.log(res.name, res.schema)
    }

    console.log(cmd.name, cmd.request, cmd.response)
    const req = Object.entries(fullModel).filter(e => e[1].Id === cmd.request)[0]
    showCmdReq(/** @type import("./DtdlOM").CommandRequestInfo */(req[1]))
    const res = Object.entries(fullModel).filter(e => e[1].Id === cmd.response)[0]
    showCmdRes(/** @type import("./DtdlOM").CommandResponseInfo */(res[1]))
}


/** @type import("./DtdlOM").DtdlObjectModel */
let fullModel

/** @param {import("./DtdlOm").DtdlObjectModel} model*/
export const render = model  => {
    fullModel = model
    const root = Object.entries(model).filter(e => e[1].EntityKind === 'Interface' && e[1].ChildOf === null)
    console.log('root', root)

    console.log('Properties:')
    const props = Object.entries(model).filter(e => e[1].EntityKind == "Property")
    props.forEach(p => showProp(/** @type import("./DtdlOM").PropertyInfo */ (p[1])))

    console.log('Telemetries:')
    const tels = Object.entries(model).filter(e => e[1].EntityKind == "Telemetry")
    tels.forEach(t => showTel(/** @type import("./DtdlOM").TelemetryInfo */(t[1])))

    console.log('Commands:')
    const cmds = Object.entries(model).filter(e => e[1].EntityKind == "Command")
    cmds.forEach(c => showCmd(/** @type import("./DtdlOM").CommandInfo */(c[1])))
}
