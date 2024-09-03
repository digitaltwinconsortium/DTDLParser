namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class for holding information from JSON-LD context blocks that are currently known.
    /// </summary>
    internal partial class ContextCollection
    {
        private const string DtdlContextPrefix = "dtmi:dtdl:context;";

        private static readonly Regex TermRegex = new Regex(@"^[A-Za-z0-9\-\._~!\$&'\(\)\*\+,;=][@A-Za-z0-9\-\._~!\$&'\(\)\*\+,;=]*$", RegexOptions.Compiled);

        private static readonly ContextHistory DtdlContextHistory;
        private static readonly Dictionary<string, ContextHistory> EndogenousAffiliateContextHistories;

        private static readonly HashSet<int> DtdlVersionsAllowingUndefinedExtensionsByDefault;
        private static readonly HashSet<int> DtdlVersionsAllowingLocalTerms;
        private static readonly HashSet<int> DtdlVersionsRestrictingKeywords;
        private static readonly HashSet<int> DtdlVersionsAllowingCustomLimits;
        private static readonly Dictionary<string, int> EndogenousAffiliateContextsImplicitDtdlVersions;

        private List<Dtmi> acceptableLimitContexts;
        private Dictionary<string, ContextHistory> exogenousAffiliateContextHistories;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextCollection"/> class.
        /// </summary>
        /// <param name="acceptableLimitContexts">A list of <c>Dtmi</c> context specifiers of limit extensions that are acceptable.</param>
        internal ContextCollection(List<Dtmi> acceptableLimitContexts)
        {
            this.acceptableLimitContexts = acceptableLimitContexts;
            this.exogenousAffiliateContextHistories = new Dictionary<string, ContextHistory>();
        }

        /// <summary>
        /// Get a term from a URI if defined in the context. If not, return the URI as a string.
        /// </summary>
        /// <param name="uri">The URI for which to get the term.</param>
        /// <returns>The value of the term or the URI string.</returns>
        internal static string GetTermOrUri(Uri uri)
        {
            string term;

            if (DtdlContextHistory.TryGetTerm(uri, out term))
            {
                return term;
            }

            foreach (ContextHistory contextHistory in EndogenousAffiliateContextHistories.Values)
            {
                if (contextHistory.TryGetTerm(uri, out term))
                {
                    return term;
                }
            }

            return uri.ToString();
        }

        /// <summary>
        /// Gets a value indicating whether an identifier is restricted from being used in a model.
        /// </summary>
        /// <param name="id">The identifier to assess.</param>
        /// <returns>True if the identifier is restricted.</returns>
        internal static bool IsIdentifierReserved(string id)
        {
            return DtdlContextHistory.IsIdentifierReserved(id) || EndogenousAffiliateContextHistories.Any(kvp => kvp.Value.IsIdentifierReserved(id));
        }

        /// <summary>
        /// Add term definitions from a <see cref="JsonLdContextComponent"/> as a new extension context.
        /// </summary>
        /// <param name="extensionId">The identifier of the extension.</param>
        /// <param name="contextComponents">Array of <see cref="JsonLdContextComponent"/> containing the included context specifiers of the extension and the term definitions to add.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        internal void AddExtensionContext(Dtmi extensionId, JsonLdContextComponent[] contextComponents, ParsingErrorCollection parsingErrorCollection)
        {
            if (contextComponents.Length < 1 ||
                contextComponents[0].IsLocal ||
                !contextComponents[0].RemoteId.StartsWith(DtdlContextPrefix) ||
                !Dtmi.TryCreateDtmi(contextComponents[0].RemoteId, out Dtmi dtdlContextDtmi) ||
                dtdlContextDtmi.Fragment != string.Empty)
            {
                parsingErrorCollection.Quit("missingDtdlContext", contextComponent: contextComponents[0]);
                return;
            }

            VersionedContext versionedContext = new VersionedContext(extensionId.AbsoluteUri, extensionId.MajorVersion, extensionId.MinorVersion, 0, null);

            if (contextComponents.Last().IsLocal)
            {
                JsonLdContextComponent localContextComponent = contextComponents.Last();
                foreach (KeyValuePair<string, string> kvp in localContextComponent.Terms)
                {
                    this.ValidateTerm(kvp.Key, localContextComponent, parsingErrorCollection, extensionId);

                    if (this.TryGetTermDefinition(kvp.Key, kvp.Value, localContextComponent, out Uri termDefinition, out string prefixDefinition, dtdlContextDtmi.MajorVersion, string.Empty, parsingErrorCollection, extensionId))
                    {
                        if (termDefinition != null)
                        {
                            versionedContext.AddTermDefinition(kvp.Key, termDefinition);
                        }
                        else if (prefixDefinition != null)
                        {
                            versionedContext.AddPrefixDefinition(kvp.Key, prefixDefinition);
                        }
                    }
                }
            }

            string affiliateName = extensionId.Versionless;
            if (this.exogenousAffiliateContextHistories.TryGetValue(affiliateName, out ContextHistory contextHistory))
            {
                contextHistory.AddVersion(versionedContext);
            }
            else
            {
                this.exogenousAffiliateContextHistories[affiliateName] = new ContextHistory(new List<VersionedContext>() { versionedContext });
            }
        }

        /// <summary>
        /// Indicates whether a given DTDL version by default allows undefined extension contexts to be specified in models.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version number to check.</param>
        /// <returns>True if undefined extension contexts are permitted.</returns>
        internal bool DoesDtdlVersionAllowUndefinedExtensionsByDefault(int dtdlVersion)
        {
            return DtdlVersionsAllowingUndefinedExtensionsByDefault.Contains(dtdlVersion);
        }

        /// <summary>
        /// Indicates whether a given DTDL version allows local term definitions.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version number to check.</param>
        /// <returns>True if local term definitions are permitted.</returns>
        internal bool DoesDtdlVersionAllowLocalTerms(int dtdlVersion)
        {
            return DtdlVersionsAllowingLocalTerms.Contains(dtdlVersion);
        }

        /// <summary>
        /// Indicates whether a given DTDL version restricts the use of unsupported JSON-LD keywords.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version number to check.</param>
        /// <returns>True if unsupported keywords are restricted.</returns>
        internal bool DoesDtdlVersionRestrictKeywords(int dtdlVersion)
        {
            return DtdlVersionsRestrictingKeywords.Contains(dtdlVersion);
        }

        /// <summary>
        /// Indicates whether a given DTDL version allows custom limits to be specified via a limit context.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version number to check.</param>
        /// <returns>True if custom limits are allowed.</returns>
        internal bool DoesDtdlVersionAllowCustomLimits(int dtdlVersion)
        {
            return DtdlVersionsAllowingCustomLimits.Contains(dtdlVersion);
        }

        /// <summary>
        /// Get the implicit DTDL version for an affiliate context if one is specified by the affiliate extension.
        /// </summary>
        /// <param name="affiliateContextSpecifier">The context specifier for the affiliate extension.</param>
        /// <param name="implicitDtdlVersion">Out parameter to receive the implicit DTDL version for the extension.</param>
        /// <returns>True if the affiliate extension specifies an implicit DTDL context version.</returns>
        internal bool TryGetAffiliateImplicitDtdlVersion(string affiliateContextSpecifier, out int implicitDtdlVersion)
        {
            return EndogenousAffiliateContextsImplicitDtdlVersions.TryGetValue(affiliateContextSpecifier, out implicitDtdlVersion);
        }

        /// <summary>
        /// Get a <see cref="VersionedContext"/> object corresponding to the DTDL context specified by <paramref name="contextComponent"/>.
        /// </summary>
        /// <param name="contextComponent">A <see cref="JsonLdContextComponent"/> specifying which DTDL context to retrieve.</param>
        /// <param name="dtdlContext">The <see cref="VersionedContext"/> returned.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="maxDtdlVersion">An optional integer value that restricts the highest DTDL version the parser should accept.</param>
        /// <param name="specifiesLimits">An out parameter indicating whether the context specifies modeling limits.</param>
        internal void GetDtdlContextFromContextComponent(JsonLdContextComponent contextComponent, ref VersionedContext dtdlContext, ParsingErrorCollection parsingErrorCollection, int? maxDtdlVersion, out bool specifiesLimits)
        {
            if (!Dtmi.TryCreateDtmi(contextComponent.RemoteId, out Dtmi dtdlContextDtmi))
            {
                parsingErrorCollection.Quit(
                    "invalidContextSpecifier",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (dtdlContextDtmi.Fragment != string.Empty && dtdlContextDtmi.Fragment != "#limitless" && dtdlContextDtmi.Fragment != "#limits")
            {
                parsingErrorCollection.Quit(
                    "invalidContextFragment",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (!this.DoesDtdlVersionAllowCustomLimits(dtdlContextDtmi.MajorVersion) && dtdlContextDtmi.Fragment != string.Empty)
            {
                parsingErrorCollection.Quit(
                    "disallowedContextFragment",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId,
                    version: dtdlContextDtmi.MajorVersion.ToString());
            }

            if (dtdlContextDtmi.MajorVersion == 0)
            {
                parsingErrorCollection.Quit(
                    "missingContextVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (dtdlContextDtmi.Fragment == "#limits")
            {
                parsingErrorCollection.Quit(
                    "misplacedLimitContext",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (!DtdlContextHistory.TryGetMatchingContext(dtdlContextDtmi.MajorVersion, dtdlContextDtmi.MinorVersion, out dtdlContext))
            {
                parsingErrorCollection.Quit(
                    "unrecognizedContextVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId,
                    versionRestriction: DtdlContextHistory.AvailableVersions);
            }

            if (maxDtdlVersion != null && dtdlContextDtmi.MajorVersion > maxDtdlVersion)
            {
                parsingErrorCollection.Quit(
                    "disallowedContextVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId,
                    version: maxDtdlVersion.ToString());
            }

            if (!IdValidator.IsIdReferenceValid(dtdlContextDtmi.Fragmentless.ToString(), dtdlContextDtmi.MajorVersion, string.Empty))
            {
                parsingErrorCollection.Quit(
                    "invalidContextSpecifierForVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId,
                    version: dtdlContextDtmi.MajorVersion.ToString());
            }

            specifiesLimits = dtdlContextDtmi.Fragment != "#limitless";
        }

        /// <summary>
        /// Get a <see cref="VersionedContext"/> object corresponding to the affiliate context specified by <paramref name="contextComponent"/>.
        /// </summary>
        /// <param name="contextComponent">A <see cref="JsonLdContextComponent"/> that should contain a remote context specifier indicating which affiliate context to retrieve.</param>
        /// <param name="affiliateName">The name of the affiliate context returned.</param>
        /// <param name="affiliateContext">The <see cref="VersionedContext"/> returned.</param>
        /// <param name="dtdlVersion">The version of DTDL whose identifier syntax applies.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="allowUndefinedExtensions">True if parsing should continue when encountering a reference to an extension that cannot be resolved.</param>
        /// <returns>True if a matching affiliate context is found.</returns>
        internal bool TryGetAffiliateContextFromContextComponent(JsonLdContextComponent contextComponent, out string affiliateName, out VersionedContext affiliateContext, int dtdlVersion, ParsingErrorCollection parsingErrorCollection, bool allowUndefinedExtensions)
        {
            if (contextComponent.IsLocal)
            {
                parsingErrorCollection.Quit("localContextNotLast", contextComponent: contextComponent);
            }

            if (contextComponent.RemoteId.StartsWith(DtdlContextPrefix))
            {
                parsingErrorCollection.Quit(
                    "dtdlContextFollowsAffiliate",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (!contextComponent.RemoteId.StartsWith("dtmi:"))
            {
                parsingErrorCollection.Quit(
                    "nonDtmiContextSpecifier",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (!Dtmi.TryCreateDtmi(contextComponent.RemoteId, out Dtmi affiliateContextDtmi) || affiliateContextDtmi.Fragment != string.Empty)
            {
                parsingErrorCollection.Quit(
                    "invalidContextSpecifier",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (!IdValidator.IsIdReferenceValid(contextComponent.RemoteId, dtdlVersion, string.Empty))
            {
                parsingErrorCollection.Quit(
                    "invalidContextSpecifierForVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId,
                    version: dtdlVersion.ToString());
            }

            if (affiliateContextDtmi.MajorVersion == 0)
            {
                parsingErrorCollection.Quit(
                    "missingContextVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            affiliateName = affiliateContextDtmi.Versionless;

            if (!EndogenousAffiliateContextHistories.TryGetValue(affiliateName, out ContextHistory affiliateContextHistory) &&
                !this.exogenousAffiliateContextHistories.TryGetValue(affiliateName, out affiliateContextHistory))
            {
                if (!allowUndefinedExtensions)
                {
                    parsingErrorCollection.Quit(
                        "unresolvableContextSpecifier",
                        contextComponent: contextComponent,
                        contextValue: contextComponent.RemoteId);
                }

                affiliateContext = null;
                return false;
            }

            if (!affiliateContextHistory.TryGetMatchingContext(affiliateContextDtmi.MajorVersion, affiliateContextDtmi.MinorVersion, out affiliateContext))
            {
                if (!allowUndefinedExtensions)
                {
                    parsingErrorCollection.Quit(
                        "unresolvableContextVersion",
                        contextComponent: contextComponent,
                        contextValue: contextComponent.RemoteId,
                        versionRestriction: affiliateContextHistory.AvailableVersions);
                }

                affiliateContext = null;
                return false;
            }

            if (affiliateContext.LimitSpec != null)
            {
                parsingErrorCollection.Quit(
                    "misplacedLimitContext",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            return true;
        }

        /// <summary>
        /// Get a limit specifier if the context specified by <paramref name="contextComponent"/> specifies limits.
        /// </summary>
        /// <param name="contextComponent">A <see cref="JsonLdContextComponent"/> that should contain a remote context specifier.</param>
        /// <param name="limitSpecifier">A string representing the limit specifier if the context specifies limits.</param>
        /// <param name="dtdlVersion">The version of DTDL requiring limits.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <returns>True if a matching limit context is found.</returns>
        internal bool TryGetLimitSpecifierFromContextComponent(JsonLdContextComponent contextComponent, out string limitSpecifier, int dtdlVersion, ParsingErrorCollection parsingErrorCollection)
        {
            if (contextComponent.IsLocal)
            {
                limitSpecifier = null;
                return false;
            }

            if (!contextComponent.RemoteId.StartsWith("dtmi:"))
            {
                parsingErrorCollection.Quit(
                    "nonDtmiContextSpecifier",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (!Dtmi.TryCreateDtmi(contextComponent.RemoteId, out Dtmi contextDtmi))
            {
                parsingErrorCollection.Quit(
                    "invalidContextSpecifier",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (contextDtmi.MajorVersion == 0)
            {
                parsingErrorCollection.Quit(
                    "missingContextVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId);
            }

            if (!IdValidator.IsIdReferenceValid(contextDtmi.Fragmentless.ToString(), dtdlVersion, string.Empty))
            {
                parsingErrorCollection.Quit(
                    "invalidContextSpecifierForVersion",
                    contextComponent: contextComponent,
                    contextValue: contextComponent.RemoteId,
                    version: dtdlVersion.ToString());
            }

            if (contextComponent.RemoteId.StartsWith(DtdlContextPrefix))
            {
                if (contextDtmi.Fragment != string.Empty && contextDtmi.Fragment != "#limitless" && contextDtmi.Fragment != "#limits")
                {
                    parsingErrorCollection.Quit(
                        "invalidContextFragment",
                        contextComponent: contextComponent,
                        contextValue: contextComponent.RemoteId);
                }

                if (contextDtmi.Fragment != "#limitless" && contextDtmi.MajorVersion == dtdlVersion)
                {
                    limitSpecifier = string.Empty;
                    return true;
                }
            }
            else
            {
                if (contextDtmi.Fragment != string.Empty)
                {
                    parsingErrorCollection.Quit(
                        "invalidContextSpecifier",
                        contextComponent: contextComponent,
                        contextValue: contextComponent.RemoteId);
                }

                string affiliateName = contextDtmi.Versionless;

                if (EndogenousAffiliateContextHistories.TryGetValue(affiliateName, out ContextHistory affiliateContextHistory) ||
                    this.exogenousAffiliateContextHistories.TryGetValue(affiliateName, out affiliateContextHistory))
                {
                    if (affiliateContextHistory.TryGetMatchingContext(contextDtmi.MajorVersion, contextDtmi.MinorVersion, out VersionedContext affiliateContext) &&
                        affiliateContext.LimitsDtdlVersion == dtdlVersion)
                    {
                        foreach (Dtmi acceptableContext in this.acceptableLimitContexts)
                        {
                            if (acceptableContext.Versionless == contextDtmi.Versionless)
                            {
                                if (acceptableContext.MajorVersion == 0 ||
                                    (acceptableContext.MajorVersion == contextDtmi.MajorVersion && acceptableContext.MinorVersion <= contextDtmi.MinorVersion))
                                {
                                    limitSpecifier = affiliateContext.LimitSpec;
                                    return true;
                                }
                            }
                        }

                        parsingErrorCollection.Quit(
                            "disallowedLimitContext",
                            contextComponent: contextComponent,
                            contextValue: contextComponent.RemoteId,
                            valueRestriction: $"\"{DtdlContextPrefix}{dtdlVersion}#limits\"" + string.Concat(this.acceptableLimitContexts.Select(c => $" or \"{c}\"")));
                    }
                }
            }

            limitSpecifier = null;
            return false;
        }

        /// <summary>
        /// Get a set of term definitions and a set of prefix definitions from the <paramref name="contextComponent"/>.
        /// </summary>
        /// <param name="contextComponent">The <see cref="JsonLdContextComponent"/> from which to obtain the definitions.</param>
        /// <param name="termDefinitions">A dictionary mapping terms to DTMIs.</param>
        /// <param name="prefixDefinitions">A dictionary mapping prefixes to prefix definitions.</param>
        /// <param name="parentTermDefinitions">The term definitions defined prior to parsing the <paramref name="contextComponent"/>.</param>
        /// <param name="parentPrefixDefinitions">The prefix definitions defined prior to parsing the <paramref name="contextComponent"/>.</param>
        /// <param name="dtdlVersion">The version of DTDL whose identifier syntax applies.</param>
        /// <param name="limitSpecifier">A limit specifier to indicate a customized limit.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        internal void GetChildDefinitionsfromContextComponent(JsonLdContextComponent contextComponent, out Dictionary<string, Uri> termDefinitions, out Dictionary<string, string> prefixDefinitions, Dictionary<string, Uri> parentTermDefinitions, Dictionary<string, string> parentPrefixDefinitions, int dtdlVersion, string limitSpecifier, ParsingErrorCollection parsingErrorCollection)
        {
            termDefinitions = new Dictionary<string, Uri>(parentTermDefinitions);
            prefixDefinitions = new Dictionary<string, string>(parentPrefixDefinitions);

            foreach (KeyValuePair<string, string> kvp in contextComponent.Terms)
            {
                this.ValidateTerm(kvp.Key, contextComponent, parsingErrorCollection);

                termDefinitions.Remove(kvp.Key);
                prefixDefinitions.Remove(kvp.Key);

                if (this.TryGetTermDefinition(kvp.Key, kvp.Value, contextComponent, out Uri termDefinition, out string prefixDefinition, dtdlVersion, limitSpecifier, parsingErrorCollection))
                {
                    if (termDefinition != null)
                    {
                        termDefinitions[kvp.Key] = termDefinition;
                    }
                    else if (prefixDefinition != null)
                    {
                        prefixDefinitions[kvp.Key] = prefixDefinition;
                    }
                }
            }
        }

        private void ValidateTerm(string term, JsonLdContextComponent contextComponent, ParsingErrorCollection parsingErrorCollection, Dtmi extensionId = null)
        {
            string validationPrefix = extensionId == null ? "local" : "extension";

            if (term == string.Empty)
            {
                parsingErrorCollection.Quit(
                    $"{validationPrefix}TermEmpty",
                    contextId: extensionId,
                    contextComponent: contextComponent,
                    term: term);
            }

            if (term == "dtmi")
            {
                parsingErrorCollection.Quit(
                    $"{validationPrefix}TermSchemePrefix",
                    contextId: extensionId,
                    contextComponent: contextComponent,
                    term: term);
            }

            if (!TermRegex.IsMatch(term))
            {
                parsingErrorCollection.Quit(
                    $"{validationPrefix}TermInvalid",
                    contextId: extensionId,
                    contextComponent: contextComponent,
                    term: term);
            }

            if (DtdlContextHistory.IsTermInContext(term))
            {
                parsingErrorCollection.Quit(
                    $"{validationPrefix}TermReserved",
                    contextId: extensionId,
                    contextComponent: contextComponent,
                    term: term);
            }
        }

        private bool TryGetTermDefinition(string term, string definition, JsonLdContextComponent contextComponent, out Uri termDefinition, out string prefixDefinition, int dtdlVersion, string limitSpecifier, ParsingErrorCollection parsingErrorCollection, Dtmi extensionId = null)
        {
            if (definition == null)
            {
                termDefinition = null;
                prefixDefinition = null;
                return false;
            }

            if (definition.EndsWith(":") || definition.EndsWith("/"))
            {
                termDefinition = null;
                prefixDefinition = definition;
            }
            else
            {
                prefixDefinition = null;

                string validationPrefix = extensionId == null ? "local" : "extension";

                if (definition.StartsWith("dtmi:"))
                {
                    if (!IdValidator.IsIdReferenceValid(definition, dtdlVersion, limitSpecifier))
                    {
                        parsingErrorCollection.Quit(
                            $"{validationPrefix}TermDefinitionInvalidDtmi",
                            contextId: extensionId,
                            contextComponent: contextComponent,
                            term: term,
                            identifier: definition,
                            version: dtdlVersion.ToString());
                    }

                    termDefinition = new Dtmi(definition, skipValidation: true);
                }
                else
                {
                    if (!Uri.TryCreate(definition, UriKind.Absolute, out termDefinition))
                    {
                        parsingErrorCollection.Quit(
                            $"{validationPrefix}TermDefinitionInvalidUri",
                            contextId: extensionId,
                            contextComponent: contextComponent,
                            term: term,
                            identifier: definition);
                    }
                }
            }

            return true;
        }
    }
}
