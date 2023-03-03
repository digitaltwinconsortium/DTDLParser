// @ts-check
import { GetTermOrUri } from './modelParser.js'
export const InterfaceInfo = (
    /** @type import("./DtdlOM").DtdlObjectModel */om, 
    /** @type {String} */ dtmi) => {

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
    Object.keys(root.properties).forEach(k => { properties.push(/** @type {import("./DtdlOM").PropertyInfo} */(om[root.properties[k]])) })
    Object.keys(root.telemetries).forEach(k => { telemetries.push(/** @type {import("./DtdlOM").TelemetryInfo} */(om[root.telemetries[k]])) })
    Object.keys(root.commands).forEach(k => { commands.push(/** @type {import("./DtdlOM").CommandInfo} */(om[root.commands[k]])) })
    Object.keys(root.components).forEach(k => { components.push(/** @type {import("./DtdlOM").ComponentInfo} */(om[root.components[k]])) })
    Object.keys(root.relationships).forEach(k => { relationships.push(/** @type {import("./DtdlOM").RelationshipInfo} */(om[root.relationships[k]])) })
 
    const print = (outFn) => {
        if (!(outFn instanceof Function)) outFn = console.log
        telemetries.forEach(t => outFn(` [T] ${t.name} [${GetTermOrUri(t.schema)}]`))
        properties.forEach(p => outFn(` [P] ${p.name} [${GetTermOrUri(p.schema)}]`))
        commands.forEach(c => outFn(` [C] ${c.name} req: ${c.request} resp: ${c.response}`))
    }

    return { telemetries, properties, commands, components, relationships, print }
}