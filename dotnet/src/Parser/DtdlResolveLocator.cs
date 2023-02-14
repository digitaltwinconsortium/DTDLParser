namespace DTDLParser
{
    /// <summary>
    /// A delegate to be provided by code that uses the <see cref="ModelParser"/> for converting a resolve DTMI and line number into a source name and line number.
    /// </summary>
    /// <param name="resolveDtmi">The identifier whose defininition was requested through the <see cref="DtmiResolver"/> or <see cref="DtmiResolverAsync"/>.</param>
    /// <param name="resolveLine">A line number within the JSON text definition string indicated by the resolve DTMI.</param>
    /// <param name="sourceName">The source name corresponding to the resolve DTMI and line number.</param>
    /// <param name="sourceLine">The soure line number corresponding to the resolve DTMI and line number.</param>
    /// <returns>True if the source information could be obtained.</returns>
    /// <remarks>
    /// This delegate is used to enhance the reporting of errors in DTDL models.
    /// A <c>DtdlResolveLocator</c> can be defined and passed into the constructor of a <see cref="ModelParser"/> instance.
    /// When the <c>ModelParser</c> encounters an error in a definition returned by the <see cref="DtmiResolver"/> or <see cref="DtmiResolverAsync"/>, it calls the delegate with a <paramref name="resolveDtmi"/> indicating the identifer that was resolved with the relevant definition and a <paramref name="resolveLine"/> indicating a line number within this definition.
    /// If the delegate is able to convert these values into a user-meaningful location, it should set the out-parameter <paramref name="sourceName"/> to the name of the appropriate source file, URL, etc.; set the out-parameter <paramref name="sourceLine"/> to the corresponding line number within this source; and return a value of true.
    /// If it is not able to perform the conversion, it should return a value of false.
    /// </remarks>
    public delegate bool DtdlResolveLocator(
        Dtmi resolveDtmi,
        int resolveLine,
        out string sourceName,
        out int sourceLine);
}
