namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// A delegate to be provided by code that uses the <see cref="ModelParser"/> for converting a parse index and line number into a source name and line number.
    /// </summary>
    /// <param name="parseIndex">An index into an enumeration of JSON text strings passed to the parser.</param>
    /// <param name="parseLine">A line number within the JSON text string indicated by the parse index.</param>
    /// <param name="sourceName">The source name corresponding to the parse index and line number.</param>
    /// <param name="sourceLine">The soure line number corresponding to the parse index and line number.</param>
    /// <returns>True if the source information could be obtained.</returns>
    /// <remarks>
    /// This delegate is used to enhance the reporting of errors in DTDL models.
    /// A <c>DtdlParseLocator</c> can be defined and passed into either the <see cref="ModelParser.Parse(IEnumerable{string}, DtdlParseLocator)"/> or <see cref="ModelParser.ParseAsync(IAsyncEnumerable{string}, DtdlParseLocator, CancellationToken)"/> methods of a <see cref="ModelParser"/> instance.
    /// When the <c>ModelParser</c> encounters an error in a model, it calls this delegate with a <paramref name="parseIndex"/> indicating the index of a submitted text string and a <paramref name="parseLine"/> indicating a line number within this string.
    /// If the delegate is able to convert these values into a user-meaningful location, it should set the out-parameter <paramref name="sourceName"/> to the name of the appropriate source file, URL, etc.; set the out-parameter <paramref name="sourceLine"/> to the corresponding line number within this source; and return a value of true.
    /// If it is not able to perform the conversion, it should return a value of false.
    /// </remarks>
    public delegate bool DtdlParseLocator(
        int parseIndex,
        int parseLine,
        out string sourceName,
        out int sourceLine);
}
