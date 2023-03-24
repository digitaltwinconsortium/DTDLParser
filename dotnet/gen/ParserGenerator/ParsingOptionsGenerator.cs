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
        private readonly List<int> dtdlVersions;
        private readonly List<int> dtdlVersionsAllowingUndefinedExtensionsByDefault;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingOptionsGenerator"/> class.
        /// </summary>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        /// <param name="dtdlVersions">A list of DTDL (major) version numbers defined in the metamodel digest.</param>
        /// <param name="dtdlVersionsAllowingUndefinedExtensionsByDefault">A list of DTDL versions that by default allow undefined extension contexts to be specified in models.</param>
        public ParsingOptionsGenerator(bool isLayeringSupported, List<int> dtdlVersions, List<int> dtdlVersionsAllowingUndefinedExtensionsByDefault)
        {
            this.isLayeringSupported = isLayeringSupported;
            this.dtdlVersions = dtdlVersions;
            this.dtdlVersionsAllowingUndefinedExtensionsByDefault = dtdlVersionsAllowingUndefinedExtensionsByDefault;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            int minKnownDtdlVersion = this.dtdlVersions.Min();
            int maxKnownDtdlVersion = this.dtdlVersions.Max();

            CsClass optionsClass = parserLibrary.Class(Access.Public, Novelty.Normal, "ParsingOptions", completeness: Completeness.Partial);
            optionsClass.Summary("Class <c>ParsingOptions</c> defines properties that can be passed into the <see cref=\"ModelParser\"/> constructor to configure its behavior.");

            optionsClass.Constant(Access.Public, "int", "MinKnownDtdlVersion", minKnownDtdlVersion.ToString(), "The lowest version of DTDL understood by this version of <see cref=\"ModelParser\"/>.");
            optionsClass.Constant(Access.Public, "int", "MaxKnownDtdlVersion", maxKnownDtdlVersion.ToString(), "The highest version of DTDL understood by this version of <see cref=\"ModelParser\"/>.");

            CsProperty maxDtdlVersionProperty = optionsClass.Property(Access.Public, Novelty.Normal, "int", "MaxDtdlVersion");
            maxDtdlVersionProperty.Summary("Gets or sets an integer value that restricts the highest DTDL version the parser should accept; if a higher version model is submitted, a <see cref=\"ParsingException\"/> will be thrown with a <see cref=\"ParsingError\"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:disallowedContextVersion.");
            maxDtdlVersionProperty.Remarks($"The default value is {maxKnownDtdlVersion}, because this is the highest version of DTDL understood by this version of <see cref=\"ModelParser\"/>.");
            maxDtdlVersionProperty.Get().Set().Default("MaxKnownDtdlVersion");

            CsProperty allowUndefinedExtensionsProperty = optionsClass.Property(Access.Public, Novelty.Normal, "WhenToAllow", "AllowUndefinedExtensions");
            allowUndefinedExtensionsProperty.Summary("Gets or sets a value indicating whether and when the parser should continue parsing if it encounters a reference to an extension that cannot be resolved.");
            allowUndefinedExtensionsProperty.Summary("If this property is <see cref=\"WhenToAllow.Never\"/>, an undefined extension context in a model will result in a <see cref=\"ParsingException\"/> with a <see cref=\"ParsingError\"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:unresolvableContextSpecifier or dtmi:dtdl:parsingError:unresolvableContextVersion.");
            allowUndefinedExtensionsProperty.Summary("If this property is <see cref=\"WhenToAllow.Always\"/>, an undefined extension context in a model will not interrupt parsing, and furthermore the presence of this undefined context will allow the model to use undefined co-types and to use undefined properties in elements that have undefined co-types.");
            allowUndefinedExtensionsProperty.Summary("If this property is not set or is set to <see cref=\"WhenToAllow.PerDefault\"/>, the parsing behavior is determined according to the version of the DTDL context specified by the model.");
            allowUndefinedExtensionsProperty.Get().Set().Default("WhenToAllow.PerDefault");
            foreach (int dtdlVersion in this.dtdlVersions)
            {
                string defaultBehavior = this.dtdlVersionsAllowingUndefinedExtensionsByDefault.Contains(dtdlVersion) ? "allow" : "disallow";
                allowUndefinedExtensionsProperty.Remarks($"For DTDL v{dtdlVersion}, the default behavior is to {defaultBehavior} undefined extensions.");
            }

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
