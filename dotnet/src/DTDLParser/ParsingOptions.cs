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

        /// <summary>
        /// Gets or sets an optional list of <c>Dtmi</c> context specifiers of limit extensions that are acceptable; if an unacceptable context is referenced in a modedl, a <see cref="ParsingException"/> will be thrown with a <see cref="ParsingError"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:disallowedLimitContext.
        /// </summary>
        /// <remarks>
        /// An entry in the list with no version suffix accepts all major/minor versions of the indicated limit extension.
        /// (E.g., "dtmi:dtdl:limits:example" accepts "dtmi:dtdl:limits:example;1", "dtmi:dtdl:limits:example;2", etc.)
        ///
        /// An entry in the list with a version suffix accepts the indicated major version and all equal or greater minor versions.
        /// (E.g., "dtmi:dtdl:limits:example;2" accepts "dtmi:dtdl:limits:example;2", "dtmi:dtdl:limits:example;2.1", etc.)
        ///
        /// Core DTDL limits are always acceptable. This option specifies additional limit definitions that are also acceptable.
        /// </remarks>
        public List<Dtmi> ExtensionLimitContexts { get; set; } = new List<Dtmi>();
    }
}
