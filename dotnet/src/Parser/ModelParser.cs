namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using DTDLParser.Models;

    /// <summary>
    /// Instances of the <c>ModelParser</c> class parse models written in the DTDL language.
    /// This class can be used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
    /// </summary>
    /// <remarks>
    /// The primary methods on this class are <see cref="ModelParser.Parse(IEnumerable{string}, DtdlParseLocator)"/> and <see cref="ModelParser.ParseAsync(IAsyncEnumerable{string}, DtdlParseLocator, CancellationToken)"/>.
    /// </remarks>
    public partial class ModelParser
    {
        private ReaderWriterAsyncLock readerWriterAsyncLock;

        private ContextCollection contextCollection;
        private SupplementalTypeCollection supplementalTypeCollection;
        private StandardElementCollection standardElementCollection;

        private DtmiResolver dtmiResolver;
        private DtmiResolverAsync dtmiResolverAsync;
        private DtdlResolveLocator dtdlResolveLocator;

        private JsonLdReader jsonLdResolvingReader;
        private ModelParsingQuirk quirks;

        /// <summary>
        /// Gets an integer value that restricts the highest DTDL version the parser should accept; if a higher version model is submitted, a <see cref="ParsingException"/> will be thrown with a <see cref="ParsingError"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:disallowedContextVersion.
        /// </summary>
        public int MaxDtdlVersion { get; }

        /// <summary>
        /// Gets a value indicating whether the parser will continue parsing if it encounters a reference to an extension that cannot be resolved.
        /// When this property is true, the parser will not throw a <see cref="ParsingException"/> with a <see cref="ParsingError"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:unresolvableContextSpecifier or dtmi:dtdl:parsingError:unresolvableContextVersion.
        /// </summary>
        public bool AllowUndefinedExtensions { get; }

        /// <summary>
        /// Get a term from a URI if defined in the context. If not, return the URI as a string.
        /// </summary>
        /// <param name="uri">The URI for which to get the term.</param>
        /// <returns>The value of the term or the URI string.</returns>
        public static string GetTermOrUri(Uri uri)
        {
            return ContextCollection.GetTermOrUri(uri);
        }

        /// <summary>
        /// Returns a collection of <c>DTSupplementalTypeInfo</c> objects, each of which provides information about a type known to the parser that is not materialized as a C# class.
        /// </summary>
        /// <returns>
        /// A dictionary that maps each <c>Dtmi</c> to a <c>DTSupplementalTypeInfo</c> object that describes the type with the given identifier.
        /// </returns>
        public IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> GetSupplementalTypes()
        {
            IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> supplementalTypes;

            this.readerWriterAsyncLock?.EnterReadLock();
            try
            {
                supplementalTypes = this.supplementalTypeCollection.GetSupplementalTypes();
            }
            finally
            {
                this.readerWriterAsyncLock?.ExitReadLock();
            }

            return supplementalTypes;
        }

        private void ParseJsonLdValues(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, ParsingErrorCollection parsingErrorCollection, JsonLdValue jsonLdValue, int dtdlVersion, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames)
        {
            if (jsonLdValue.ValueType != JsonLdValueType.Element)
            {
                parsingErrorCollection.Quit("notJsonObject", incidentValue: jsonLdValue);
            }

            AggregateContext aggregateContext = new AggregateContext(
                this.contextCollection,
                this.supplementalTypeCollection,
                allowUndefinedExtensions: this.AllowUndefinedExtensions,
                suppressDefinitionMerging: false,
                maxDtdlVersion: this.MaxDtdlVersion)
                .GetChildContext(jsonLdValue.ElementValue, parsingErrorCollection);

            if (jsonLdValue.ElementValue.Graph != null)
            {
                parsingErrorCollection.Quit("topLevelGraphDisallowed", element: jsonLdValue.ElementValue);
            }

            if (jsonLdValue.ElementValue.Id == null)
            {
                parsingErrorCollection.Quit("missingTopLevelId", element: jsonLdValue.ElementValue);
            }

            if (!RootableTypeCollection.IncludesRootableType(jsonLdValue.ElementValue.Types, aggregateContext.DtdlVersion))
            {
                parsingErrorCollection.Quit(
                    "typeNotRootable",
                    identifier: jsonLdValue.ElementValue.Id,
                    typeRestriction: RootableTypeCollection.RootableTypeDescriptions[aggregateContext.DtdlVersion],
                    element: jsonLdValue.ElementValue);
            }

            if (aggregateContext.RestrictKeywords)
            {
                if (jsonLdValue.ElementValue.Language != null)
                {
                    parsingErrorCollection.Quit("topLevelKeywordDisallowed", keyword: "@language", element: jsonLdValue.ElementValue);
                }

                if (jsonLdValue.ElementValue.Value != null)
                {
                    parsingErrorCollection.Quit("topLevelKeywordDisallowed", keyword: "@value", element: jsonLdValue.ElementValue);
                }

                foreach (string keyword in jsonLdValue.ElementValue.Keywords)
                {
                    parsingErrorCollection.Quit("topLevelKeywordDisallowed", keyword: keyword, element: jsonLdValue.ElementValue);
                }
            }

            ParseElement(model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, jsonLdValue.ElementValue, globalize: false, allowReservedIds: allowReservedIds, allowIdReferenceSyntax: allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames: ignoreElementsWithAutoIDsAndDuplicateNames);
        }

        private IEnumerable<string> StringToEnumerable(string jsonText)
        {
            yield return jsonText;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async IAsyncEnumerable<string> StringToAsyncEnumerable(string jsonText)
        {
            yield return jsonText;
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
