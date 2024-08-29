namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for class that stores DTDL context term definitions.
    /// </summary>
    public class ContextCollectionGenerator : ITypeGenerator
    {
        private readonly Dictionary<string, Dictionary<string, string>> contexts;
        private readonly List<int> dtdlVersionsAllowingUndefinedExtensionsByDefault;
        private readonly List<int> dtdlVersionsAllowingLocalTerms;
        private readonly List<int> dtdlVersionsRestrictingKeywords;
        private readonly List<int> dtdlVersionsAllowingCustomLimits;
        private readonly List<int> dtdlVersionsAllowingDynamicExtensions;
        private readonly List<string> contextsMergeDefinitions;
        private readonly Dictionary<string, List<string>> reservedIdPrefixes;
        private readonly bool areDynamicExtensionsSupported;
        private Dictionary<string, int> affiliateContextsImplicitDtdlVersions;
        private Dictionary<string, LimitContextDigest> limitContexts;
        private Dictionary<string, HashSet<string>> contextMergeableTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextCollectionGenerator"/> class.
        /// </summary>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        /// <param name="supplementalTypes">A dictionary that maps from type URI to a <see cref="SupplementalTypeDigest"/> object providing details about the identified supplemental type.</param>
        /// <param name="dtdlVersionsAllowingUndefinedExtensionsByDefault">A list of DTDL versions that by default allow undefined extension contexts to be specified in models.</param>
        /// <param name="dtdlVersionsAllowingLocalTerms">A list of DTDL versions that allow local term definitions in context blocks.</param>
        /// <param name="dtdlVersionsAllowingDynamicExtensions">A list of DTDL versions that allow language extensions to be added dynamically.</param>
        /// <param name="dtdlVersionsRestrictingKeywords">A list of DTDL versions that restrict the use of JSON-LD keywords.</param>
        /// <param name="dtdlVersionsAllowingCustomLimits">A list of DTDL versions that allow extensions to customize the language's modeling limits.</param>
        /// <param name="contextsMergeDefinitions">A list of context specifiers for which definitions whose identifiers contain IRI fragments should be merged.</param>
        /// <param name="reservedIdPrefixes">A dicictionary that maps from context ID to a list of identifier prefixes that are reserved for this context.</param>
        /// <param name="affiliateContextsImplicitDtdlVersions">A dictionary that maps from affiliate context specifiers to implicit DTDL versions, for those affiliate contexts that are permitted to precede a DTDL context for back-compat reasons.</param>
        /// <param name="limitContexts">A dictionary that maps from affiliate context specifier to a <see cref="LimitContextDigest"/> object.</param>
        /// <param name="areDynamicExtensionsSupported">True if dynamic extensions are supported by any recognized language version.</param>
        public ContextCollectionGenerator(
            Dictionary<string, Dictionary<string, string>> contexts,
            Dictionary<string, SupplementalTypeDigest> supplementalTypes,
            List<int> dtdlVersionsAllowingUndefinedExtensionsByDefault,
            List<int> dtdlVersionsAllowingLocalTerms,
            List<int> dtdlVersionsAllowingDynamicExtensions,
            List<int> dtdlVersionsRestrictingKeywords,
            List<int> dtdlVersionsAllowingCustomLimits,
            List<string> contextsMergeDefinitions,
            Dictionary<string, List<string>> reservedIdPrefixes,
            Dictionary<string, int> affiliateContextsImplicitDtdlVersions,
            Dictionary<string, LimitContextDigest> limitContexts,
            bool areDynamicExtensionsSupported)
        {
            this.contexts = contexts;
            this.dtdlVersionsAllowingUndefinedExtensionsByDefault = dtdlVersionsAllowingUndefinedExtensionsByDefault;
            this.dtdlVersionsAllowingLocalTerms = dtdlVersionsAllowingLocalTerms;
            this.dtdlVersionsAllowingDynamicExtensions = dtdlVersionsAllowingDynamicExtensions;
            this.dtdlVersionsRestrictingKeywords = dtdlVersionsRestrictingKeywords;
            this.dtdlVersionsAllowingCustomLimits = dtdlVersionsAllowingCustomLimits;
            this.contextsMergeDefinitions = contextsMergeDefinitions;
            this.reservedIdPrefixes = reservedIdPrefixes;
            this.affiliateContextsImplicitDtdlVersions = affiliateContextsImplicitDtdlVersions;
            this.areDynamicExtensionsSupported = areDynamicExtensionsSupported;
            this.limitContexts = limitContexts;

            this.contextMergeableTypes = new Dictionary<string, HashSet<string>>();
            foreach (KeyValuePair<string, SupplementalTypeDigest> kvp in supplementalTypes)
            {
                if (kvp.Value.IsMergeable)
                {
                    if (!this.contextMergeableTypes.TryGetValue(kvp.Value.ExtensionContext, out HashSet<string> mergeableTypes))
                    {
                        mergeableTypes = new HashSet<string>();
                        this.contextMergeableTypes[kvp.Value.ExtensionContext] = mergeableTypes;
                    }

                    mergeableTypes.Add(kvp.Key);
                }
            }
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass contextClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "ContextCollection", Multiplicity.Instance, Completeness.Partial);
            contextClass.Summary("Class for parsing and storing information from JSON-LD context blocks.");

            this.GenerateStaticFields(contextClass);
            this.GenerateStaticConstructor(contextClass);

            if (this.areDynamicExtensionsSupported)
            {
                this.GenerateDoesDtdlVersionAllowDynamicExtensionsMethod(contextClass);
            }
        }

        private void GenerateStaticFields(CsClass contextClass)
        {
            if (this.areDynamicExtensionsSupported)
            {
                contextClass.Field(Access.Private, "HashSet<int>", "DtdlVersionsAllowingDynamicExtensions", null, Multiplicity.Static, Mutability.ReadOnly);
            }
        }

        private void GenerateStaticConstructor(CsClass contextClass)
        {
            CsConstructor constructor = contextClass.Constructor(Access.Implicit, Multiplicity.Static);

            constructor.Body.Line($"DtdlVersionsAllowingUndefinedExtensionsByDefault = new HashSet<int>() {{ {string.Join(", ", this.dtdlVersionsAllowingUndefinedExtensionsByDefault)} }};");
            constructor.Body.Break();

            constructor.Body.Line($"DtdlVersionsAllowingLocalTerms = new HashSet<int>() {{ {string.Join(", ", this.dtdlVersionsAllowingLocalTerms)} }};");
            constructor.Body.Break();

            if (this.areDynamicExtensionsSupported)
            {
                constructor.Body.Line($"DtdlVersionsAllowingDynamicExtensions = new HashSet<int>() {{ {string.Join(", ", this.dtdlVersionsAllowingDynamicExtensions)} }};");
                constructor.Body.Break();
            }

            constructor.Body.Line($"DtdlVersionsRestrictingKeywords = new HashSet<int>() {{ {string.Join(", ", this.dtdlVersionsRestrictingKeywords)} }};");
            constructor.Body.Break();

            constructor.Body.Line($"DtdlVersionsAllowingCustomLimits = new HashSet<int>() {{ {string.Join(", ", this.dtdlVersionsAllowingCustomLimits)} }};");
            constructor.Body.Break();

            constructor.Body.Line("EndogenousAffiliateContextsImplicitDtdlVersions = new Dictionary<string, int>();");
            foreach (KeyValuePair<string, int> kvp in this.affiliateContextsImplicitDtdlVersions)
            {
                constructor.Body.Line($"EndogenousAffiliateContextsImplicitDtdlVersions[\"{kvp.Key}\"] = {kvp.Value};");
            }

            constructor.Body.Break();

            constructor.Body
                .Line("DtdlContextHistory = GetDtdlContextHistory();").Break()
                .Line("EndogenousAffiliateContextHistories = new Dictionary<string, ContextHistory>();").Break();

            CsMethod dtdlContextMethod = contextClass.Method(Access.Private, Novelty.Normal, "ContextHistory", $"GetDtdlContextHistory", Multiplicity.Static);
            dtdlContextMethod.Body.Line("List<VersionedContext> versionedContexts = new List<VersionedContext>();").Break();

            int affiliateCount = 0;
            Dictionary<string, int> affiliateIndices = new Dictionary<string, int>();
            Dictionary<int, CsMethod> affiliateContextMethods = new Dictionary<int, CsMethod>();

            foreach (KeyValuePair<string, Dictionary<string, string>> contextPair in this.contexts)
            {
                string contextSpecifier = contextPair.Key;
                if (contextSpecifier.StartsWith(ParserGeneratorValues.DtdlContextPrefix))
                {
                    this.AddContextVersion(dtdlContextMethod.Body, contextSpecifier, contextPair.Value, 0, null);
                }
                else
                {
                    string affiliateName = contextSpecifier.Substring(0, contextSpecifier.IndexOf(";"));
                    if (!affiliateIndices.TryGetValue(affiliateName, out int affiliateIndex))
                    {
                        affiliateIndex = affiliateCount++;
                        affiliateIndices[affiliateName] = affiliateIndex;

                        CsMethod affiliateContextMethod = contextClass.Method(Access.Private, Novelty.Normal, "ContextHistory", $"GetAffiliate{affiliateIndex}ContextHistory", Multiplicity.Static);
                        affiliateContextMethod.Body.Line("List<VersionedContext> versionedContexts = new List<VersionedContext>();").Break();
                        affiliateContextMethods[affiliateIndex] = affiliateContextMethod;
                    }

                    if (!this.limitContexts.TryGetValue(contextSpecifier, out LimitContextDigest limitContext))
                    {
                        limitContext = new LimitContextDigest();
                    }

                    this.AddContextVersion(affiliateContextMethods[affiliateIndex].Body, contextSpecifier, contextPair.Value, limitContext.DtdlVersion, limitContext.LimitSpec);
                }
            }

            dtdlContextMethod.Body.Line("return new ContextHistory(versionedContexts);");

            foreach (KeyValuePair<int, CsMethod> affiliateContextMethod in affiliateContextMethods)
            {
                affiliateContextMethod.Value.Body.Line("return new ContextHistory(versionedContexts);");
            }

            foreach (KeyValuePair<string, int> affiliateIndex in affiliateIndices)
            {
                constructor.Body.Line($"EndogenousAffiliateContextHistories[\"{affiliateIndex.Key}\"] = GetAffiliate{affiliateIndex.Value}ContextHistory();");
            }
        }

        private void GenerateDoesDtdlVersionAllowDynamicExtensionsMethod(CsClass contextClass)
        {
            CsMethod method = contextClass.Method(Access.Internal, Novelty.Normal, "bool", "DoesDtdlVersionAllowDynamicExtensions");
            method.Summary("Indicates whether a given DTDL version allows adding dynamic extensions to the DTDL language.");
            method.Param("int", "dtdlVersion", "The DTDL version number to check.");
            method.Returns("True if dynamic extensions are permitted.");

            method.Body.Line("return DtdlVersionsAllowingDynamicExtensions.Contains(dtdlVersion);");
        }

        private void AddContextVersion(CsScope contextMethodBody, string contextSpecifer, Dictionary<string, string> termDefinitions, int limitsDtdlVersion, string limitSpec)
        {
            string versionString = contextSpecifer.Substring(contextSpecifer.IndexOf(';') + 1);
            int dotIx = versionString.IndexOf('.');
            int majorVersion = int.Parse(versionString);
            int minorVersion = dotIx < 0 ? 0 : int.Parse(versionString.Substring(dotIx + 1));

            string mergeDefinitions = this.contextsMergeDefinitions.Contains(contextSpecifer) ? "true" : "false";
            string limitSpecString = limitSpec == null ? "null" : $"\"{limitSpec}\"";

            if (!this.contextMergeableTypes.TryGetValue(contextSpecifer, out HashSet<string> mergeableTypes))
            {
                mergeableTypes = new HashSet<string>();
            }

            string contextVar = $"context{majorVersion}_{minorVersion}";

            contextMethodBody.Line($"VersionedContext {contextVar} = new VersionedContext(\"{contextSpecifer}\", {majorVersion}, {minorVersion}, {limitsDtdlVersion}, {limitSpecString}, mergeDefinitions: {mergeDefinitions});");

            if (this.reservedIdPrefixes.TryGetValue(contextSpecifer, out List<string> reservedIdPrefixes))
            {
                foreach (string reservedIdPrefix in reservedIdPrefixes)
                {
                    contextMethodBody.Line($"{contextVar}.ReserveIdDefinitionPrefix(\"{reservedIdPrefix}\");");
                }
            }

            foreach (KeyValuePair<string, string> kvp in termDefinitions)
            {
                if (kvp.Value.EndsWith(":") || kvp.Value.EndsWith("/"))
                {
                    contextMethodBody.Line($"{contextVar}.AddPrefixDefinition(\"{kvp.Key}\", \"{kvp.Value}\");");
                }
                else
                {
                    string isMergeableTypeString = $"isMergeableType: {ParserGeneratorValues.GetBooleanLiteral(mergeableTypes.Contains(kvp.Value))}";
                    contextMethodBody.Line($"{contextVar}.AddTermDefinition(\"{kvp.Key}\", new {ParserGeneratorValues.IdentifierType}(\"{kvp.Value}\"), {isMergeableTypeString});");
                }
            }

            contextMethodBody.Line($"versionedContexts.Add({contextVar});").Break();
        }
    }
}
