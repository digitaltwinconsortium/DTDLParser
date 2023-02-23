export const ErrorInfo = /** @type {import('./DtdlErr').ModelingException */ err => {
    /** @type {import('./DtdlErr').ParsingException} */
    let parsingError

    /** @type {import('./DtdlErr').ResolutionException} */
    let resolutionException

    if (err.ExceptionKind === 'Parsing') {
        parsingError = err
    }

    if (err.ExceptionKind === 'Resolution') {
        resolutionException = err
    }

    const print = (outFn) => {
        if (!(outFn instanceof Function)) outFn = console.log
        if (parsingError) {
            outFn('Parsing Exception\n')
            parsingError.Errors.forEach(e => outFn(`${e.PrimaryID} \n Cause: ${e.Cause} \n Action: ${e.Action} \n\n`))
        }
        if (resolutionException) {
            outFn('Resolution Exception\n')
            resolutionException.UndefinedIdentifiers.forEach(i => outFn(`Cannnot resolve ${i} \n`))
        }
    }
    return { print }
}