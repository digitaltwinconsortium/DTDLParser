namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Code generator for <c>ParsingOptions</c> partial class.
    /// </summary>
    public class ParsingOptionsGenerator : ITypeGenerator
    {
        private readonly bool isLayeringSupported;
        private readonly int minKnownDtdlVersion;
        private readonly int maxKnownDtdlVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingOptionsGenerator"/> class.
        /// </summary>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        /// <param name="dtdlVersions">A list of DTDL (major) version numbers defined in the metamodel digest.</param>
        public ParsingOptionsGenerator(bool isLayeringSupported, List<int> dtdlVersions)
        {
            this.isLayeringSupported = isLayeringSupported;
            this.minKnownDtdlVersion = dtdlVersions.Min();
            this.maxKnownDtdlVersion = dtdlVersions.Max();
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass optionsClass = parserLibrary.Class(Access.Public, Novelty.Normal, "ParsingOptions", completeness: Completeness.Partial);
            optionsClass.Summary("Class <c>ParsingOptions</c> defines properties that can be passed into the <see cref=\"ModelParser\"/> constructor to configure its behavior.");

            optionsClass.Constant(Access.Public, "int", "MinKnownDtdlVersion", this.minKnownDtdlVersion.ToString(), "The lowest version of DTDL understood by this version of <see cref=\"ModelParser\"/>.");
            optionsClass.Constant(Access.Public, "int", "MaxKnownDtdlVersion", this.maxKnownDtdlVersion.ToString(), "The highest version of DTDL understood by this version of <see cref=\"ModelParser\"/>.");

            CsProperty maxDtdlVersionProperty = optionsClass.Property(Access.Public, Novelty.Normal, "int", "MaxDtdlVersion");
            maxDtdlVersionProperty.Summary("Gets or sets an integer value that restricts the highest DTDL version the parser should accept; if a higher version model is submitted, a <see cref=\"ParsingException\"/> will be thrown with a <see cref=\"ParsingError\"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:disallowedContextVersion.");
            maxDtdlVersionProperty.Remarks($"The default value is {this.maxKnownDtdlVersion}, because this is the highest version of DTDL understood by this version of <see cref=\"ModelParser\"/>.");
            maxDtdlVersionProperty.Get().Set().Default("MaxKnownDtdlVersion");

            if (this.isLayeringSupported)
            {
                CsProperty dtmiPartialResolverProperty = optionsClass.Property(Access.Public, Novelty.Normal, "DtmiPartialResolver", "DtmiPartialResolver");
                dtmiPartialResolverProperty.Summary("Gets or sets an optional <c>DtmiPartialResolver</c> callback that will be called to retrieve layer definitions for elements that may be only partially defined in the model during execution of the <see cref=\"ModelParser.Parse(IEnumerable{string}, DtdlParseLocator)\"/> API.");
                dtmiPartialResolverProperty.Get().Set();

                CsProperty dtmiPartialResolverAsyncProperty = optionsClass.Property(Access.Public, Novelty.Normal, "DtmiPartialResolverAsync", "DtmiPartialResolverAsync");
                dtmiPartialResolverAsyncProperty.Summary("Gets or sets an optional <c>DtmiPartialResolverAsync</c> callback that will be called to retrieve layer definitions for elements that may be only partially defined in the model during execution of the <see cref=\"ModelParser.ParseAsync(IAsyncEnumerable{string}, DtdlParseLocator, CancellationToken)\"/> API.");
                dtmiPartialResolverAsyncProperty.Get().Set();
            }
        }
    }
}
