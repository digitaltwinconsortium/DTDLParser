// @ts-check
const contentKind = {
    Telemetry: 'Telemetry',
    Property: 'Property',
    Command: 'Command',
    Component: 'Component',
    Relationship: 'Relationship'
}

export const InterfaceInfo = (
    /** @type import("./DtdlOM").DtdlObjectModel */om, 
    /** @type {String} */ dtmi) => {

    const getTermOrUri = (/** @type {String} */ dtmi) => om[dtmi].displayName.en

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
    
    const root = /** @type import("./DtdlOM").InterfaceInfo*/(om[dtmi])
    Object.keys(root.contents).forEach(k => {
        const elDtmi = root.contents[k]
        const el = om[elDtmi]
        switch(el.EntityKind) {
            case contentKind.Property:
                properties.push(/** @type {import("./DtdlOM").PropertyInfo} */(el))
                break
            case contentKind.Telemetry:
                telemetries.push(/** @type {import("./DtdlOM").TelemetryInfo} */(el))
                break
            case contentKind.Command:
                commands.push(/** @type {import("./DtdlOM").CommandInfo} */(el))
                break
            case contentKind.Component:
                components.push(/** @type {import("./DtdlOM").ComponentInfo} */(el))
                break
            case contentKind.Relationship:
                relationships.push(/** @type {import("./DtdlOM").RelationshipInfo} */(el))
                break
        }
    })
 
    const print = (outFn) => {
        if (!(outFn instanceof Function)) outFn = console.log
        telemetries.forEach(t => outFn(` [T] ${t.name} [${getTermOrUri(t.schema)}]`))
        properties.forEach(p => outFn(` [P] ${p.name} [${getTermOrUri(p.schema)}]`))
        commands.forEach(c => outFn(` [C] ${c.name} req: ${c.request} resp: ${c.response}`))
    }

    return { telemetries, properties, commands, components, relationships, print }
}