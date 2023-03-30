namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Class <c>ParsingOptions</c> defines properties that can be passed into the <see cref="ModelParser"/> constructor to configure its behavior.
    /// </summary>
    public partial class ParsingOptions
    {
        /// <summary>
        /// Gets or sets an optional <c>DtmiResolver</c> callback that will be called to resolve <c>@context</c> references or undefined identifiers during execution of the <see cref="ModelParser.Parse(IEnumerable{string}, DtdlParseLocator)"/> API.
        /// </summary>
        public DtmiResolver DtmiResolver { get; set; }

        /// <summary>
        /// Gets or sets an optional <c>DtmiResolverAsync</c> callback that will be called to resolve <c>@context</c> references or undefined identifiers during execution of the <see cref="ModelParser.ParseAsync(IAsyncEnumerable{string}, DtdlParseLocator, CancellationToken)"/> API.
        /// </summary>
        public DtmiResolverAsync DtmiResolverAsync { get; set; }

        /// <summary>
        /// Gets or sets an optional <c>DtdlResolveLocator</c> delegate that will be called to convert a resolve DTMI and line number into a source name and line number.
        /// </summary>
        public DtdlResolveLocator DtdlResolveLocator { get; set; }
    }
}
