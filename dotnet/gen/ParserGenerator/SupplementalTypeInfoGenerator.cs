namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>DTSupplementalTypeInfo</c> partial class.
    /// </summary>
    public class SupplementalTypeInfoGenerator : ITypeGenerator
    {
        private readonly string baseEnumName;
        private readonly List<string> extensionKinds;
        private readonly Dictionary<string, MaterialClassDigest> materialClasses;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalTypeInfoGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        /// <param name="extensionKinds">A list of strings that indicate the extension points defined in the metamodel digest.</param>
        /// <param name="materialClasses">A a dictionary that maps from class name to a <see cref="MaterialClassDigest"/> object providing details about the named material class.</param>
        public SupplementalTypeInfoGenerator(string baseName, List<string> extensionKinds, Dictionary<string, MaterialClassDigest> materialClasses)
        {
            this.baseEnumName = NameFormatter.FormatNameAsEnum(baseName);
            this.extensionKinds = extensionKinds;
            this.materialClasses = materialClasses;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass infoClass = parserLibrary.Class(Access.Public, Novelty.Normal, "DTSupplementalTypeInfo", completeness: Completeness.Partial, exports: "IEquatable<DTSupplementalTypeInfo>, ITypeChecker", subNamespace: ParserGeneratorValues.ElementSubNamespace);
            infoClass.Summary("Class that provides information about a type that is not materialized as a C# class.");

            infoClass.Constructor(Access.Private).Body.Line($"this.AllowedCotypeKinds = new HashSet<{this.baseEnumName}>();");

            CsProperty allowedCotypeKindsProp = infoClass.Property(Access.Internal, Novelty.Normal, $"HashSet<{this.baseEnumName}>", "AllowedCotypeKinds");
            allowedCotypeKindsProp.Summary("Gets or sets a collection of kinds of allowed cotypes for this supplemental type.");
            allowedCotypeKindsProp.Get().Set();

            this.GenerateTryParseExtensionElementMethod(infoClass);
        }

        private void GenerateTryParseExtensionElementMethod(CsClass infoClass)
        {
            CsMethod method = infoClass.Method(Access.Private, Novelty.Normal, "bool", "TryParseExtensionElement", Multiplicity.Static);
            method.Param("DTExtensionKind", "extensionKind");
            method.Param("Model", "model");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints");
            method.Param("AggregateContext", "aggregateContext");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");
            method.Param("JsonLdElement", "elt");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer");
            method.Param(ParserGeneratorValues.IdentifierType, "parentId");
            method.Param(ParserGeneratorValues.IdentifierType, "definedIn");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propName");
            method.Param("JsonLdProperty", "propProp");
            method.Param(ParserGeneratorValues.ObverseTypeString, "dtmiSeg");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "idRequired");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "typeRequired");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "globalize");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowReservedIds");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "tolerateSolecisms");
            method.Param(ParserGeneratorValues.ObverseTypeString, "inferredType");

            CsSwitch kindSwitch = method.Body.Switch("extensionKind");

            foreach (string extensionKind in this.extensionKinds)
            {
                if (this.materialClasses.ContainsKey(extensionKind))
                {
                    kindSwitch.Case($"{NameFormatter.FormatNameAsEnum("Extension")}.{NameFormatter.FormatNameAsEnumValue(extensionKind)}");
                    kindSwitch.Line($"return {NameFormatter.FormatNameAsClass(extensionKind)}.TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, null, aggregateContext, parsingErrorCollection, elt, layer, parentId, definedIn, propName, propProp, dtmiSeg, null, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, null, inferredType);");
                }
            }

            kindSwitch.Default();
            kindSwitch.Line("return false;");
        }
    }
}
