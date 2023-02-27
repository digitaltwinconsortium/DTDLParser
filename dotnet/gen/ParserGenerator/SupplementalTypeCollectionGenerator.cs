namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Code generator for <c>SupplementalTypeCollection</c> partial class.
    /// </summary>
    public class SupplementalTypeCollectionGenerator : ITypeGenerator
    {
        private string kindEnum;

        private List<SupplementalType> supplementalTypes;

        private Dictionary<string, string> contextIdVariables;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalTypeCollectionGenerator"/> class.
        /// </summary>
        /// <param name="supplementalTypes">A dictionary that maps from type URI to a <see cref="SupplementalTypeDigest"/> object providing details about the identified supplemental type.</param>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        /// <param name="extensibleMaterialClasses">A dictionary that maps from DTDL version to a list of strings that each indicate a material class that is extensible.</param>
        /// <param name="baseName">The base name for the parser's object model.</param>
        public SupplementalTypeCollectionGenerator(
            Dictionary<string, SupplementalTypeDigest> supplementalTypes,
            Dictionary<string, Dictionary<string, string>> contexts,
            Dictionary<int, List<string>> extensibleMaterialClasses,
            string baseName)
        {
            this.kindEnum = NameFormatter.FormatNameAsEnum(baseName);

            this.supplementalTypes = new List<SupplementalType>();

            foreach (KeyValuePair<int, List<string>> kvp in extensibleMaterialClasses)
            {
                Dictionary<string, string> dtdlContext = contexts[ParserGeneratorValues.GetDtdlContextIdString(kvp.Key)];
                foreach (string extensibleMaterialClass in kvp.Value)
                {
                    this.supplementalTypes.Add(new SupplementalType(dtdlContext[extensibleMaterialClass]));
                }
            }

            foreach (KeyValuePair<string, SupplementalTypeDigest> kvp in supplementalTypes)
            {
                this.supplementalTypes.Add(new SupplementalTypeExtension(kvp.Key, kvp.Value, this.kindEnum));
            }

            this.supplementalTypes.Sort((x, y) => x.TypeUri.CompareTo(y.TypeUri));

            this.contextIdVariables = contexts.ToDictionary(e => e.Key, e => GetContextIdVariableName(e.Key));
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass collectionClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "SupplementalTypeCollection", completeness: Completeness.Partial);
            collectionClass.Summary("A collection of DTDL types that are not materialized as C# classes.");

            this.GenerateConstructor(collectionClass);
            this.GenerateTrySetAllowedCotypesMethod(collectionClass);
            this.GenerateTryGetKindsAndVersionsMethod(collectionClass);
        }

        private static string GetContextIdVariableName(string contextId)
        {
            int versionStart = contextId.IndexOf(';') + 1;
            int pathStart = contextId.IndexOf(':') + 1;
            int pathLength = versionStart - pathStart - 1;
            string rootName = string.Concat(contextId.Substring(pathStart, pathLength).Split(new char[] { ':' }).Where(s => s != "context").Select((s, i) => i == 0 ? s : char.ToUpperInvariant(s[0]) + s.Substring(1)));
            string suffix = contextId.Substring(versionStart).Replace('.', '_');
            return $"{rootName}ContextIdV{suffix}";
        }

        private void GenerateConstructor(CsClass collectionClass)
        {
            CsConstructor constructor = collectionClass.Constructor(Access.Implicit, Multiplicity.Static);

            constructor.Body.Line($"EndogenousSupplementalTypes = new Dictionary<{ParserGeneratorValues.IdentifierType}, DTSupplementalTypeInfo>();");

            constructor.Body.Break();
            foreach (KeyValuePair<string, string> kvp in this.contextIdVariables)
            {
                constructor.Body.Line($"{ParserGeneratorValues.IdentifierType} {kvp.Value} = new {ParserGeneratorValues.IdentifierType}(\"{kvp.Key}\");");
            }

            constructor.Body.Break();
            foreach (SupplementalType supplementalType in this.supplementalTypes)
            {
                supplementalType.DefineIdVariable(constructor.Body);
            }

            foreach (SupplementalType supplementalType in this.supplementalTypes)
            {
                constructor.Body.Break();
                supplementalType.DefineInfoVariable(constructor.Body, this.contextIdVariables);
            }

            constructor.Body.Break();
            foreach (SupplementalType supplementalType in this.supplementalTypes)
            {
                supplementalType.AssignInfoVariable(constructor.Body, "EndogenousSupplementalTypes");
            }

            constructor.Body.Break();

            constructor.Body.Break();
            constructor.Body.Line("ConnectEndogenousPropertySetters();");
        }

        private void GenerateTrySetAllowedCotypesMethod(CsClass collectionClass)
        {
            CsMethod method = collectionClass.Method(Access.Private, Novelty.Normal, "bool", "TrySetAllowedCotypes", Multiplicity.Static);
            method.Param(ParserGeneratorValues.IdentifierType, "extensionId");
            method.Param(ParserGeneratorValues.IdentifierType, "typeId");
            method.Param("List<JsonLdValue>", "allowedCotypes");
            method.Param("DTSupplementalTypeInfo", "typeInfo");
            method.Param("DTSupplementalTypeInfo", "parentTypeInfo");
            method.Param("AggregateContext", "aggregateContext");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");

            method.Body.If("!TryGetClassConstraints(allowedCotypes, \"sh:or\", typeInfo, aggregateContext, out HashSet<Dtmi> cotypeIds, parsingErrorCollection)")
                .Line("return false;");

            CsIf ifCotypeIdsNotNull = method.Body.If("cotypeIds != null");

            ifCotypeIdsNotNull
                .If($"!TryGetKindsAndVersions(extensionId, typeId, cotypeIds, allowedCotypes, out HashSet<{this.kindEnum}> kinds, out HashSet<int> versions, parsingErrorCollection)")
                    .Line("return false;");

            ifCotypeIdsNotNull
                .Line("typeInfo.AllowedCotypeKinds = kinds;")
                .Line("typeInfo.AllowedCotypeVersions = versions;")
                .If("parentTypeInfo != null")
                    .Line("typeInfo.AllowedCotypeKinds.IntersectWith(parentTypeInfo.AllowedCotypeKinds);")
                    .Line("typeInfo.AllowedCotypeVersions.IntersectWith(parentTypeInfo.AllowedCotypeVersions);");

            ifCotypeIdsNotNull.ElseIf("parentTypeInfo != null")
                .Line("typeInfo.AllowedCotypeKinds = parentTypeInfo.AllowedCotypeKinds;")
                .Line("typeInfo.AllowedCotypeVersions = parentTypeInfo.AllowedCotypeVersions;");

            method.Body.Line("return true;");
        }

        private void GenerateTryGetKindsAndVersionsMethod(CsClass collectionClass)
        {
            CsMethod method = collectionClass.Method(Access.Private, Novelty.Normal, "bool", "TryGetKindsAndVersions", Multiplicity.Static);
            method.Param(ParserGeneratorValues.IdentifierType, "extensionId");
            method.Param(ParserGeneratorValues.IdentifierType, "typeId");
            method.Param($"HashSet<{ParserGeneratorValues.IdentifierType}>", "cotypeIds");
            method.Param("List<JsonLdValue>", "allowedCotypes");
            method.Param($"HashSet<{this.kindEnum}>", "kinds", passage: Passage.Out);
            method.Param("HashSet<int>", "versions", passage: Passage.Out);
            method.Param("ParsingErrorCollection", "parsingErrorCollection");

            method.Body.Line($"kinds = new HashSet<{this.kindEnum}>();");
            method.Body.Line("versions = new HashSet<int>();");
            method.Body.Break();

            CsForEach forEachCotypeId = method.Body.ForEach($"{ParserGeneratorValues.IdentifierType} cotypeId in cotypeIds");

            forEachCotypeId.Line($"{this.kindEnum} kind;");
            CsIf ifTryParse = forEachCotypeId.If("!Enum.TryParse(ContextCollection.GetTermOrUri(cotypeId), out kind)");
            ifTryParse
                .Line("string cotypeIdTerm = ContextCollection.GetTermOrUri(cotypeId);")
                .Line("JsonLdValue allowedCotype = allowedCotypes.First(c => c.StringValue == cotypeIdTerm || c.StringValue == cotypeId.OriginalString);")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"cotypeNotConcreteMaterial\",")
                    .Line("contextId: extensionId,")
                    .Line("elementId: typeId,")
                    .Line("cotype: allowedCotype.StringValue,")
                    .Line("incidentValue: allowedCotype);");

            ifTryParse
                .Break()
                .Line("return false;");

            forEachCotypeId.Line("kinds.Add(kind);");
            forEachCotypeId.Line("versions.Add(cotypeId.MajorVersion);");

            method.Body.Line("return true;");
        }
    }
}
