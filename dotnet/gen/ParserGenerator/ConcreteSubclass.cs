namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a concrete subclass of a class that is materialized in the parser object model.
    /// </summary>
    public class ConcreteSubclass
    {
        private const string RegexPatternFieldSuffix = "IdentifierRegexPatternV";

        private int dtdlVersion;
        private string subclassType;
        private string kindValue;
        private string subclassTypeUri;
        private Dictionary<string, int> maxIdLength;
        private string pattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteSubclass"/> class.
        /// </summary>
        /// <param name="dtdlVersion">The version of DTDL that defines the class and subclass.</param>
        /// <param name="subclassType">The type name (DTDL term) of the subclass.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        /// <param name="classIdentifierDefinitionRestrictions">A dictionary that maps from class name to a dictionary that maps from DTDL version to a <see cref="StringRestriction"/> object that describes restrictions on identifiers used in specific classes of definitions.</param>
        public ConcreteSubclass(int dtdlVersion, string subclassType, string kindEnum, Dictionary<string, Dictionary<string, string>> contexts, Dictionary<string, Dictionary<int, StringRestriction>> classIdentifierDefinitionRestrictions)
        {
            this.SubclassName = NameFormatter.FormatNameAsClass(subclassType);

            this.dtdlVersion = dtdlVersion;
            this.subclassType = subclassType;
            this.kindValue = $"{kindEnum}.{NameFormatter.FormatNameAsEnumValue(subclassType)}";
            this.subclassTypeUri = contexts[ParserGeneratorValues.GetDtdlContextIdString(dtdlVersion)][subclassType];

            if (classIdentifierDefinitionRestrictions.TryGetValue(subclassType, out Dictionary<int, StringRestriction> idRestriction) &&
                idRestriction.TryGetValue(dtdlVersion, out StringRestriction restriction))
            {
                this.maxIdLength = restriction.MaxLength;
                this.pattern = restriction.Pattern;
            }
            else
            {
                this.maxIdLength = null;
                this.pattern = null;
            }
        }

        /// <summary>
        /// Gets the name of the concrete subclass.
        /// </summary>
        public string SubclassName { get; }

        /// <summary>
        /// Generate appropriate members for the material class that has this concrete subclass.
        /// </summary>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="superclassType">The name of the superclass to which the members are being added.</param>
        public void AddMembers(CsClass obverseClass, string superclassType)
        {
            if (this.pattern != null && superclassType == this.subclassType)
            {
                obverseClass.Field(Access.Internal, "Regex", $"{this.subclassType}{RegexPatternFieldSuffix}{this.dtdlVersion}", $"new Regex(@\"{this.pattern}\", RegexOptions.Compiled)", Multiplicity.Static, Mutability.ReadOnly, $"Regular expression pattern for identifiers for class '{this.SubclassName}' for DTDLv{this.dtdlVersion}.");
            }
        }

        /// <summary>
        /// Add the enum kind for this subclass to an enum variable.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code line.</param>
        /// <param name="varName">Name of the variable to which to add the enum value.</param>
        public void AddEnumValue(CsSorted sorted, string varName)
        {
            sorted.Line($"{varName}.Add({this.kindValue});");
        }

        /// <summary>
        /// Add a case for this subclass to the swtich statement in the obverse class method that parses a type string.
        /// </summary>
        /// <param name="switchOnTypeString">A <see cref="CsSwitch"/> object to which to add the code.</param>
        /// <param name="elementInfoVar">Name of the variable that holds the new element info object created in this case.</param>
        /// <param name="elementIdVar">Name of the variable that holds the identifier of the element.</param>
        /// <param name="elementLayerVar">Name of the variable that holds the layer of the element.</param>
        /// <param name="jsonLdEltVar">Name of the variable that holds the <c>JsonLdElement</c> being parsed.</param>
        /// <param name="parentIdVar">Name of the variable that holds the identifier of the parent of the element.</param>
        /// <param name="myPropNameVar">Name of the variable that holds the name of the property by which the parent refers to the element.</param>
        /// <param name="definedInVar">Name of the variable that holds the identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="errorVar">Name of the variable that holds a <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public void AddCaseToParseTypeStringSwitch(CsSwitch switchOnTypeString, string elementInfoVar, string elementIdVar, string elementLayerVar, string jsonLdEltVar, string parentIdVar, string myPropNameVar, string definedInVar, string errorVar, bool isLayeringSupported)
        {
            switchOnTypeString.Case($"\"{this.subclassType}\"");
            switchOnTypeString.Case($"\"{this.subclassTypeUri}\"");

            if (this.maxIdLength != null)
            {
                string limitVar = $"{NameFormatter.FormatNameAsVariable(this.subclassType)}MaxIdLength";
                ValueLimiter.DefineLimitVariable(switchOnTypeString, this.maxIdLength, limitVar, "aggregateContext.LimitSpecifier", nullable: false);
                switchOnTypeString.If($"elementId.AbsoluteUri.Length > {limitVar}")
                    .MultiLine($"{errorVar}.Notify(")
                        .Line("\"idTooLongForType\",")
                        .Line($"identifier: {elementIdVar}.ToString(),")
                        .Line($"elementType: \"{this.subclassType}\",")
                        .Line($"expectedCount: {limitVar},")
                        .Line($"element: {jsonLdEltVar});");
            }

            if (this.pattern != null)
            {
                switchOnTypeString.If($"!{this.SubclassName}.{this.subclassType}{RegexPatternFieldSuffix}{this.dtdlVersion}.IsMatch(elementId.AbsoluteUri)")
                    .MultiLine($"{errorVar}.Notify(")
                        .Line("\"idTooLongForType\",")
                        .Line($"identifier: {elementIdVar}.ToString(),")
                        .Line($"elementType: \"{this.subclassType}\",")
                        .Line($"expectedCount: {this.maxIdLength},")
                        .Line($"element: {jsonLdEltVar});");
            }

            switchOnTypeString.If($"{elementInfoVar} == null")
                .Line($"{elementInfoVar} = new {this.SubclassName}({this.dtdlVersion}, {elementIdVar}, {parentIdVar}, {myPropNameVar}, {definedInVar});");

            if (isLayeringSupported)
            {
                switchOnTypeString.Line($"{elementInfoVar}.SetLayerDefinedWhere({elementLayerVar}, {jsonLdEltVar}, {parentIdVar}, {definedInVar}, {errorVar});");
            }
            else
            {
                switchOnTypeString.Line($"{elementInfoVar}.{ParserGeneratorValues.JsonLdElementsPropertyName}[string.Empty] = {jsonLdEltVar};");
            }

            switchOnTypeString.Line($"materialKinds.Add({this.kindValue});");
            switchOnTypeString.Line("return true;");
        }
    }
}
