namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a concrete subclass of a class that is materialized in the parser object model.
    /// </summary>
    public class ExtensibleMaterialClass
    {
        private int dtdlVersion;
        private string typeName;
        private string className;
        private string kindEnum;
        private string kindValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensibleMaterialClass"/> class.
        /// </summary>
        /// <param name="dtdlVersion">The version of DTDL that defines the class and subclass.</param>
        /// <param name="typeName">The type name (DTDL term) of the class.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        public ExtensibleMaterialClass(int dtdlVersion, string typeName, string kindEnum)
        {
            this.dtdlVersion = dtdlVersion;
            this.typeName = typeName;
            this.className = NameFormatter.FormatNameAsClass(typeName);
            this.kindEnum = kindEnum;
            this.kindValue = NameFormatter.FormatNameAsEnumValue(typeName);
        }

        /// <summary>
        /// Add a case for this subclass to the swtich statement in the obverse class method that parses a type string.
        /// </summary>
        /// <param name="switchOnExtensionKind">A <see cref="CsSwitch"/> object to which to add the code.</param>
        /// <param name="extensibleMaterialSubtypes">A list of strings representing the extensible material types that are subtypes of the class.</param>
        /// <param name="elementInfoVar">Name of the variable that holds the new element info object created in this case.</param>
        /// <param name="propNameVar">>Name of the variable that holds the property name.</param>
        /// <param name="propPropVar">Name of the variable that holds the <c>JsonLdProperty</c> representing the source of the property.</param>
        /// <param name="elementIdVar">Name of the variable that holds the identifier of the element.</param>
        /// <param name="typeStringVar">>Name of the variable that holds the type string.</param>
        /// <param name="elementLayerVar">Name of the variable that holds the layer of the element.</param>
        /// <param name="jsonLdEltVar">Name of the variable that holds the <c>JsonLdElement</c> being parsed.</param>
        /// <param name="parentIdVar">Name of the variable that holds the identifier of the parent of the element.</param>
        /// <param name="myPropNameVar">Name of the variable that holds the name of the property by which the parent refers to the element.</param>
        /// <param name="definedInVar">Name of the variable that holds the identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="typeIdVar">Name of the variable that holds a supplemental type identifier.</param>
        /// <param name="typeInfoVar">Name of the variable that holds a supplemental <c>DTSupplementalTypeInfo</c> object.</param>
        /// <param name="errorVar">Name of the variable that holds a <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public void AddCaseToParseTypeStringSwitch(CsSwitch switchOnExtensionKind, List<string> extensibleMaterialSubtypes, string elementInfoVar, string propNameVar, string propPropVar, string elementIdVar, string typeStringVar, string elementLayerVar, string jsonLdEltVar, string parentIdVar, string myPropNameVar, string definedInVar, string typeIdVar, string typeInfoVar, string errorVar, bool isLayeringSupported)
        {
            switchOnExtensionKind.Case($"DTExtensionKind.{this.kindValue}");

            if (extensibleMaterialSubtypes.Contains(this.typeName))
            {
                switchOnExtensionKind.If($"{elementInfoVar} == null")
                    .Line($"{elementInfoVar} = new {this.className}({this.dtdlVersion}, {elementIdVar}, {parentIdVar}, {myPropNameVar}, {definedInVar}, {typeIdVar});");

                if (isLayeringSupported)
                {
                    switchOnExtensionKind.Line($"{elementInfoVar}.SetLayerDefinedWhere({elementLayerVar}, {jsonLdEltVar}, {parentIdVar}, {definedInVar}, {errorVar});");
                }
                else
                {
                    switchOnExtensionKind.Line($"{elementInfoVar}.{ParserGeneratorValues.JsonLdElementsPropertyName}[string.Empty] = {jsonLdEltVar};");
                }

                switchOnExtensionKind.Line($"{elementInfoVar}.AddType({typeIdVar}, {typeInfoVar}, {errorVar});");
                switchOnExtensionKind.Line($"materialKinds.Add({this.kindEnum}.{this.kindValue});");
                switchOnExtensionKind.Line("return true;");
            }
            else
            {
                CsScope badTypeScope = switchOnExtensionKind.Scope(null);

                badTypeScope
                    .Line("string sourceName1 = null;")
                    .Line("int sourceLine1 = 0;");

                CsIf ifGotSourceLoc = badTypeScope.If($"{propPropVar} != null && {propPropVar}.TryGetSourceLocation(out sourceName1, out sourceLine1) && {jsonLdEltVar}.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2)");

                ifGotSourceLoc
                    .Line($"{jsonLdEltVar}.TryGetSourceLocationForType(out _, out int sourceLine3);")
                    .MultiLine("parsingErrorCollection.Add(")
                        .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                        .Line($"BadTypeLocatedCauseFormat[{this.dtdlVersion}],")
                        .Line($"BadTypeActionFormat[{this.dtdlVersion}],")
                        .Line($"primaryId: {parentIdVar},")
                        .Line($"property: {propNameVar},")
                        .Line($"secondaryId: {elementIdVar},")
                        .Line($"value: {typeStringVar},")
                        .Line($"layer: {elementLayerVar},")
                        .Line("sourceName1: sourceName1,")
                        .Line("startLine1: sourceLine1,")
                        .Line("sourceName2: sourceName2,")
                        .Line("startLine2: startLine2,")
                        .Line("endLine2: endLine2,")
                        .Line("startLine3: sourceLine3);");

                ifGotSourceLoc.Else().MultiLine("parsingErrorCollection.Add(")
                    .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                    .Line($"BadTypeCauseFormat[{this.dtdlVersion}],")
                    .Line($"BadTypeActionFormat[{this.dtdlVersion}],")
                    .Line($"primaryId: {parentIdVar},")
                    .Line($"property: {propNameVar},")
                    .Line($"secondaryId: {elementIdVar},")
                    .Line($"value: {typeStringVar},")
                    .Line($"layer: {elementLayerVar});");

                switchOnExtensionKind.Line("return false;");
            }
        }
    }
}
