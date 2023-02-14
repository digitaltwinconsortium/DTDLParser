namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for class that stores DTDL context term definitions.
    /// </summary>
    public class AggregateContextGenerator : ITypeGenerator
    {
        private readonly string baseEnumName;
        private readonly string baseEnumParam;
        private readonly bool areDynamicExtensionsSupported;
        private Dictionary<string, HashSet<string>> contextMergeableTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateContextGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        /// <param name="areDynamicExtensionsSupported">True if dynamic extensions are supported by any recognized language version.</param>
        /// <param name="supplementalTypes">A dictionary that maps from type URI to a <see cref="SupplementalTypeDigest"/> object providing details about the identified supplemental type.</param>
        public AggregateContextGenerator(string baseName, bool areDynamicExtensionsSupported, Dictionary<string, SupplementalTypeDigest> supplementalTypes)
        {
            this.baseEnumName = NameFormatter.FormatNameAsEnum(baseName);
            this.baseEnumParam = NameFormatter.FormatNameAsEnumParameter(baseName);
            this.areDynamicExtensionsSupported = areDynamicExtensionsSupported;

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
            CsClass contextClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "AggregateContext", Multiplicity.Instance, Completeness.Partial);
            contextClass.Summary("Class for parsing and storing information from JSON-LD context blocks.");

            if (this.areDynamicExtensionsSupported)
            {
                this.GenerateAreDynamicExtensionsAllowedProperty(contextClass);
            }

            this.GenerateGetMergeableCotypesForKindMethod(contextClass);
        }

        private void GenerateAreDynamicExtensionsAllowedProperty(CsClass contextClass)
        {
            CsProperty prop = contextClass.Property(Access.Internal, Novelty.Normal, "bool", "AreDynamicExtensionsAllowed");
            prop.Summary("Gets a value indicating whether dynamic extensions to the active version of the DTDL language are allowed.");

            prop.Body().Get().Line("return this.contextCollection.DoesDtdlVersionAllowDynamicExtensions(this.DtdlVersion);");
        }

        private void GenerateGetMergeableCotypesForKindMethod(CsClass contextClass)
        {
            CsMethod method = contextClass.Method(Access.Internal, Novelty.Normal, "List<string>", "GetMergeableCotypesForKind");
            method.Summary("Get a list of mergeable cotypes that can be applied to an element of a given kind.");
            method.Param(this.baseEnumName, this.baseEnumParam, "The kind of element.");
            method.Returns("A list of strings, each of which is a term that maps to an appropriate mergeable cotype.");

            method.Body.Line("List<string> mergeableCotypes = new List<string>();");

            method.Body.ForEach("Dtmi cotypeId in this.activeDtdlContext.MergeableTypeIds")
                .If($"this.SupplementalTypeCollection.TryGetSupplementalTypeInfo(cotypeId, out DTSupplementalTypeInfo supplementalTypeInfo) && supplementalTypeInfo.AllowedCotypeKinds.Contains({this.baseEnumParam})")
                    .If("this.activeDtdlContext.TryGetTerm(cotypeId, out string term)")
                        .Line("mergeableCotypes.Add(term);");

            method.Body.ForEach("VersionedContext affiliateContext in this.activeAffiliateContexts.Values")
                .ForEach("Dtmi cotypeId in affiliateContext.MergeableTypeIds")
                    .If($"this.SupplementalTypeCollection.TryGetSupplementalTypeInfo(cotypeId, out DTSupplementalTypeInfo supplementalTypeInfo) && supplementalTypeInfo.AllowedCotypeKinds.Contains({this.baseEnumParam})")
                        .If("affiliateContext.TryGetTerm(cotypeId, out string term)")
                            .Line("mergeableCotypes.Add(term);");

            method.Body.Line("return mergeableCotypes;");
        }
    }
}
