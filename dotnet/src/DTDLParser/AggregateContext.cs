namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for parsing and storing information from JSON-LD context blocks.
    /// </summary>
    internal partial class AggregateContext
    {
        private const string DtdlContextPrefix = "dtmi:dtdl:context;";

        private readonly ContextCollection contextCollection;

        private readonly bool allowUndefinedExtensions;
        private readonly bool suppressDefinitionMerging;
        private readonly int? maxDtdlVersion;

        private readonly VersionedContext activeDtdlContext;
        private readonly Dictionary<string, VersionedContext> activeAffiliateContexts;

        private readonly HashSet<string> unrecognizedContexts;

        private readonly Dictionary<string, Uri> localTermDefinitions;
        private readonly Dictionary<string, string> localPrefixDefinitions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateContext"/> class.
        /// </summary>
        /// <param name="contextCollection">A <see cref="ContextCollection"/> object that holds information from context blocks that are currently known.</param>
        /// <param name="supplementalTypeCollection">A collection of DTDL types that are not materialized as C# classes.</param>
        /// <param name="allowUndefinedExtensions">True if parsing should continue when encountering a reference to an extension that cannot be resolved.</param>
        /// <param name="suppressDefinitionMerging">True if parsing should not merge the definitions of elements whose identifiers contain IRI fragments.</param>
        /// <param name="maxDtdlVersion">An optional integer value that restricts the highest DTDL version the parser should accept.</param>
        internal AggregateContext(ContextCollection contextCollection, SupplementalTypeCollection supplementalTypeCollection, bool allowUndefinedExtensions, bool suppressDefinitionMerging, int? maxDtdlVersion = null)
        {
            this.contextCollection = contextCollection;
            this.allowUndefinedExtensions = allowUndefinedExtensions;
            this.suppressDefinitionMerging = suppressDefinitionMerging;
            this.maxDtdlVersion = maxDtdlVersion;
            this.activeDtdlContext = null;
            this.activeAffiliateContexts = new Dictionary<string, VersionedContext>();
            this.unrecognizedContexts = new HashSet<string>();
            this.localTermDefinitions = new Dictionary<string, Uri>();
            this.localPrefixDefinitions = new Dictionary<string, string>();
            this.DtdlContextComponent = null;
            this.MergeDefinitions = false;
            this.SupplementalTypeCollection = supplementalTypeCollection;
        }

        private AggregateContext(
            ContextCollection contextCollection,
            SupplementalTypeCollection supplementalTypeCollection,
            bool allowUndefinedExtensions,
            bool suppressDefinitionMerging,
            int? maxDtdlVersion,
            VersionedContext activeDtdlContext,
            Dictionary<string, VersionedContext> activeAffiliateContexts,
            HashSet<string> unrecognizedContexts,
            Dictionary<string, Uri> localTermDefinitions,
            Dictionary<string, string> localPrefixDefinitions,
            JsonLdContextComponent dtdlContextComponent)
        {
            this.contextCollection = contextCollection;
            this.allowUndefinedExtensions = allowUndefinedExtensions;
            this.suppressDefinitionMerging = suppressDefinitionMerging;
            this.maxDtdlVersion = maxDtdlVersion;
            this.activeDtdlContext = activeDtdlContext;
            this.activeAffiliateContexts = activeAffiliateContexts;
            this.unrecognizedContexts = unrecognizedContexts;
            this.localTermDefinitions = localTermDefinitions;
            this.localPrefixDefinitions = localPrefixDefinitions;
            this.DtdlContextComponent = dtdlContextComponent;
            this.MergeDefinitions = !suppressDefinitionMerging && (activeDtdlContext.MergeDefinitions || activeAffiliateContexts.Any(kvp => kvp.Value.MergeDefinitions));
            this.SupplementalTypeCollection = supplementalTypeCollection;
        }

        /// <summary>
        /// Gets the DTDL version indicated by the active context, or zero if there is no active context yet.
        /// </summary>
        internal int DtdlVersion { get => this.activeDtdlContext?.MajorVersion ?? 0; }

        /// <summary>
        /// Gets the JSON-LD context component that defines the DTDL version in use.
        /// </summary>
        internal JsonLdContextComponent DtdlContextComponent { get; }

        /// <summary>
        /// Gets a value indicating whether definitions whose identifiers contain IRI fragments should be merged.
        /// </summary>
        internal bool MergeDefinitions { get; }

        /// <summary>
        /// Gets a value indicating whether use of unsupported JSON-LD keywords is restricted.
        /// </summary>
        internal bool RestrictKeywords { get => this.contextCollection.DoesDtdlVersionRestrictKeywords(this.DtdlVersion); }

        /// <summary>
        /// Gets a value indicating whether all context specifiers in the aggregate context are recognized.
        /// </summary>
        internal bool IsComplete { get => !this.unrecognizedContexts.Any(); }

        /// <summary>
        /// Gets a <c>SupplementalTypeCollection</c>.
        /// </summary>
        internal SupplementalTypeCollection SupplementalTypeCollection { get; }

        /// <summary>
        /// Gets a value indicating whether an identifier is restricted from being used in a model.
        /// </summary>
        /// <param name="id">The identifier to assess.</param>
        /// <returns>True if the identifier is restricted.</returns>
        internal bool IsIdentifierReserved(string id)
        {
            return this.activeDtdlContext.IdDefinitionReservedPrefixes.Any(p => id.StartsWith(p)) || this.activeAffiliateContexts.Any(kvp => kvp.Value.IdDefinitionReservedPrefixes.Any(p => id.StartsWith(p)));
        }

        /// <summary>
        /// Gets a string that lists the identifier prefixes that are reserved.
        /// </summary>
        /// <returns>The prefixes as a conjoined string.</returns>
        internal string GetReservedPrefixesString()
        {
            List<string> reservedPrefixes = new List<string>();

            reservedPrefixes.AddRange(this.activeDtdlContext.IdDefinitionReservedPrefixes);

            foreach (VersionedContext affiliateContext in this.activeAffiliateContexts.Values)
            {
                reservedPrefixes.AddRange(affiliateContext.IdDefinitionReservedPrefixes);
            }

            return string.Join(", ", reservedPrefixes.Select(p => $"'{p}'"));
        }

        /// <summary>
        /// Get a string that represents the aggregate context in JSON-LD text form.
        /// </summary>
        /// <returns>The aggregate context as a JSON-LD text string.</returns>
        internal string GetJsonLdText()
        {
            List<string> contextComponents = new List<string>();

            contextComponents.Add($"\"{this.activeDtdlContext.ContextSpecifier}\"");
            foreach (VersionedContext activeAffiliateContext in this.activeAffiliateContexts.Values)
            {
                contextComponents.Add($"\"{activeAffiliateContext.ContextSpecifier}\"");
            }

            foreach (string unrecognizedContext in this.unrecognizedContexts)
            {
                contextComponents.Add($"\"{unrecognizedContext}\"");
            }

            if (this.localTermDefinitions.Any() || this.localPrefixDefinitions.Any())
            {
                List<string> termDefinitions = new List<string>();

                foreach (KeyValuePair<string, Uri> kvp in this.localTermDefinitions)
                {
                    termDefinitions.Add($"\"{kvp.Key}\": \"{kvp.Value}\"");
                }

                foreach (KeyValuePair<string, string> kvp in this.localPrefixDefinitions)
                {
                    termDefinitions.Add($"\"{kvp.Key}\": \"{kvp.Value}\"");
                }

                contextComponents.Add("{ " + string.Join(", ", termDefinitions) + " }");
            }

            return "[ " + string.Join(", ", contextComponents) + " ]";
        }

        /// <summary>
        /// Get an <see cref="AggregateContext"/> object that is either the current one or a new one derived from the current one and a JSON-LD context block.
        /// </summary>
        /// <param name="elt">The <see cref="JsonLdElement"/> in which to look for a JSON-LD context block.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <returns>A reference to the curret object or to a newly created child object.</returns>
        internal AggregateContext GetChildContext(JsonLdElement elt, ParsingErrorCollection parsingErrorCollection)
        {
            if (elt.Context == null)
            {
                if (this.activeDtdlContext == null)
                {
                    parsingErrorCollection.Quit("missingContext", element: elt);
                }

                return this;
            }

            if (!elt.Context.Any())
            {
                if (this.activeDtdlContext == null)
                {
                    parsingErrorCollection.Quit("emptyContext", element: elt);
                }
                else
                {
                    return this;
                }
            }

            return this.GetChildContextFromContextArray(elt.Context, parsingErrorCollection, forceAllowLocalContext: false);
        }

        /// <summary>
        /// Create a <c>Dtmi</c> object given a string that is either a URI or a term defined in the context.
        /// </summary>
        /// <param name="uriOrTerm">A URI or a term defined in the context.</param>
        /// <param name="dtmi">Out param to receive the constructed <c>Dtmi</c>.</param>
        /// <returns>True if a <c>Dtmi</c> was successfully created.</returns>
        internal bool TryCreateDtmi(string uriOrTerm, out Dtmi dtmi)
        {
            if (this.TryCreateUri(uriOrTerm, out Uri uri, requireDtmi: true))
            {
                dtmi = (Dtmi)uri;
                return true;
            }
            else
            {
                dtmi = null;
                return false;
            }
        }

        /// <summary>
        /// Create a <c>Uri</c> object given a string that is either a URI or a term defined in the context.
        /// </summary>
        /// <param name="uriOrTerm">A URI or a term defined in the context.</param>
        /// <param name="uri">Out param to receive the constructed <c>Uri</c>.</param>
        /// <returns>True if a <c>Uri</c> was successfully created.</returns>
        /// <param name="requireDtmi">True if the URI must be a DTMI.</param>
        internal bool TryCreateUri(string uriOrTerm, out Uri uri, bool requireDtmi)
        {
            uri = null;

            if (uriOrTerm.StartsWith("dtmi:"))
            {
                return IdValidator.TryCreateIdReference(uriOrTerm, this.activeDtdlContext.MajorVersion, out uri);
            }

            int colonPos = uriOrTerm.IndexOf(':');
            if (colonPos < 0)
            {
                return (this.activeDtdlContext.TryGetUri(uriOrTerm, out uri) || this.localTermDefinitions.TryGetValue(uriOrTerm, out uri) || this.TryGetAffiliateUri(uriOrTerm, out uri))
                    && (!requireDtmi || uri is Dtmi);
            }

            string prefix = uriOrTerm.Substring(0, colonPos);
            if (this.activeDtdlContext.TryGetPrefix(prefix, out string definition) ||
                this.localPrefixDefinitions.TryGetValue(prefix, out definition) ||
                this.TryGetAffiliatePrefix(prefix, out definition))
            {
                string conjunction = $"{definition}{uriOrTerm.Substring(colonPos + 1)}";
                if (conjunction.StartsWith("dtmi:"))
                {
                    return IdValidator.TryCreateIdReference(conjunction, this.activeDtdlContext.MajorVersion, out uri);
                }
                else
                {
                    return !requireDtmi && Uri.TryCreate(conjunction, UriKind.Absolute, out uri);
                }
            }
            else
            {
                return !requireDtmi && Uri.TryCreate(uriOrTerm, UriKind.Absolute, out uri);
            }
        }

        /// <summary>
        /// Get an <see cref="AggregateContext"/> object that is either the current one or a new one derived from the current one and <c>JArray</c> of context elements.
        /// </summary>
        /// <param name="contextComponents">A list of <see cref="JsonLdContextComponent"/> representing the elements in a JSON-LD context block.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="forceAllowLocalContext">Force local contexts to be allowed, irrespective of DTDL version.</param>
        /// <returns>A reference to the curret object or to a newly created child object.</returns>
        internal AggregateContext GetChildContextFromContextArray(JsonLdContextComponent[] contextComponents, ParsingErrorCollection parsingErrorCollection, bool forceAllowLocalContext)
        {
            JsonLdContextComponent dtdlContextComponent = this.DtdlContextComponent;
            VersionedContext childDtdlContext = this.activeDtdlContext;
            int startIndex = 0;

            Dictionary<string, VersionedContext> prefaceAffiliateContexts = new Dictionary<string, VersionedContext>();
            while (startIndex < contextComponents.Count() && !contextComponents[startIndex].IsLocal && this.contextCollection.TryGetAffiliateImplicitDtdlVersion(contextComponents[startIndex].RemoteId, out int implicitDtdlVersion))
            {
                if (this.contextCollection.TryGetAffiliateContextFromContextComponent(contextComponents[startIndex], out string affiliateName, out VersionedContext affiliateContext, implicitDtdlVersion, parsingErrorCollection, allowUndefinedExtensions: this.allowUndefinedExtensions))
                {
                    prefaceAffiliateContexts[affiliateName] = affiliateContext;
                }

                ++startIndex;
            }

            while (startIndex < contextComponents.Count() && !contextComponents[startIndex].IsLocal && contextComponents[startIndex].RemoteId.StartsWith(DtdlContextPrefix))
            {
                dtdlContextComponent = contextComponents[startIndex];
                this.contextCollection.GetDtdlContextFromContextComponent(dtdlContextComponent, ref childDtdlContext, parsingErrorCollection, this.maxDtdlVersion);
                ++startIndex;
            }

            if (childDtdlContext == null)
            {
                parsingErrorCollection.Quit("missingDtdlContext", contextComponent: contextComponents[0]);
            }

            Dictionary<string, Uri> childTermDefinitions = this.localTermDefinitions;
            Dictionary<string, string> childPrefixDefinitions = this.localPrefixDefinitions;
            int endIndex = contextComponents.Count() - 1;
            if (contextComponents[endIndex].IsLocal)
            {
                if (!forceAllowLocalContext && !this.contextCollection.DoesDtdlVersionAllowLocalTerms(childDtdlContext.MajorVersion))
                {
                    parsingErrorCollection.Quit(
                        "disallowedLocalContext",
                        contextComponent: contextComponents[endIndex],
                        version: childDtdlContext.MajorVersion.ToString());
                }

                this.contextCollection.GetChildDefinitionsfromContextComponent(contextComponents[endIndex], out childTermDefinitions, out childPrefixDefinitions, this.localTermDefinitions, this.localPrefixDefinitions, childDtdlContext.MajorVersion, parsingErrorCollection);
                --endIndex;
            }

            Dictionary<string, VersionedContext> childAffiliateContexts = this.activeAffiliateContexts;
            HashSet<string> childUnrecognizedContexts = this.unrecognizedContexts;
            if (startIndex <= endIndex || prefaceAffiliateContexts.Any())
            {
                childAffiliateContexts = new Dictionary<string, VersionedContext>(this.activeAffiliateContexts);
                prefaceAffiliateContexts.ToList().ForEach(ac => childAffiliateContexts[ac.Key] = ac.Value);

                childUnrecognizedContexts = new HashSet<string>(this.unrecognizedContexts);

                for (int index = startIndex; index <= endIndex; ++index)
                {
                    if (this.contextCollection.TryGetAffiliateContextFromContextComponent(contextComponents[index], out string affiliateName, out VersionedContext affiliateContext, childDtdlContext.MajorVersion, parsingErrorCollection, allowUndefinedExtensions: this.allowUndefinedExtensions))
                    {
                        childAffiliateContexts[affiliateName] = affiliateContext;
                    }
                    else
                    {
                        childUnrecognizedContexts.Add(contextComponents[index].RemoteId);
                    }
                }
            }

            return new AggregateContext(
                this.contextCollection,
                this.SupplementalTypeCollection,
                this.allowUndefinedExtensions,
                this.suppressDefinitionMerging,
                this.maxDtdlVersion,
                childDtdlContext,
                childAffiliateContexts,
                childUnrecognizedContexts,
                childTermDefinitions,
                childPrefixDefinitions,
                dtdlContextComponent);
        }

        private bool TryGetAffiliateUri(string uriOrTerm, out Uri uri)
        {
            foreach (VersionedContext affiliateContext in this.activeAffiliateContexts.Values)
            {
                if (affiliateContext.TryGetUri(uriOrTerm, out uri))
                {
                    return true;
                }
            }

            uri = null;
            return false;
        }

        private bool TryGetAffiliatePrefix(string uriOrTerm, out string definition)
        {
            foreach (VersionedContext affiliateContext in this.activeAffiliateContexts.Values)
            {
                if (affiliateContext.TryGetPrefix(uriOrTerm, out definition))
                {
                    return true;
                }
            }

            definition = null;
            return false;
        }
    }
}
