
import { dotnet } from './dotnet.js'

let dotnetExports = null

async function createRuntimeAndGetExports() {
    const { getAssemblyExports, getConfig } = await dotnet.create()
    const config = getConfig()
    return getAssemblyExports(config.mainAssemblyName)
}

export async function parseAsync(jsonTexts) {
    /* Start dotnet runtime if not already started */
    dotnetExports ??= await createRuntimeAndGetExports()

    jsonTexts = typeof jsonTexts === 'string' ? [jsonTexts] : jsonTexts

    let objectModelJson
    try {
        objectModelJson = await dotnetExports.ModelParserInterop.ParseToJsonAsync(Array.from(jsonTexts))
    } catch (modelParsingError) {
        try {
            /**
             * Error info was marshalled accross the interop boundary using the error message.
             * Parse the error info JSON into a more usable form in JS.
             */
            modelParsingError.exceptionInfo = JSON.parse(error.message)
        } catch (errorMessageParsingError) { /* ignore JSON parsing error and keep original error as-is */ }
        throw modelParsingError
    }

    const objectModel = JSON.parse(objectModelJson)

    /* Turn DTMI references in 'contents' fields into hydrated objects */
    for (const entity of Object.values(objectModel)) {
        for (const [name, id] of Object.entries(entity.contents ?? {})) {
            entity.contents[name] = objectModel[id] ? objectModel[id] : id
        }
    }
    return objectModel
}