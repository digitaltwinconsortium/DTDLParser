namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a restriction that excludes a specific type from specified descendants of a class.
    /// </summary>
    public class DescendantControlExcludeType : IDescendantControl
    {
        private int dtdlVersion;
        private string rootClass;
        private List<string> propertyNames;
        private bool isNarrow;
        private string excludeType;
        private string kindProperty;
        private string excludeKind;
        private string coreName;
        private string methodName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DescendantControlExcludeType"/> class.
        /// </summary>
        /// <param name="dtdlVersion">The version of DTDL that defines the control.</param>
        /// <param name="rootClass">The name of the concete class the control pertains to.</param>
        /// <param name="propertyNames">The names of the properties for which this control is relevant.</param>
        /// <param name="isNarrow">Indicates whether the descendant hierarchy should be scanned only along relevant properties.</param>
        /// <param name="excludeType">The type that is to be excluded from the relevant properties.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        /// <param name="kindProperty">The property on the DTDL base obverse class that indicates the kind of DTDL element.</param>
        public DescendantControlExcludeType(int dtdlVersion, string rootClass, List<string> propertyNames, bool isNarrow, string excludeType, string kindEnum, string kindProperty)
        {
            this.dtdlVersion = dtdlVersion;
            this.rootClass = rootClass;
            this.propertyNames = propertyNames;
            this.isNarrow = isNarrow;
            this.excludeType = excludeType;
            this.kindProperty = kindProperty;
            this.excludeKind = $"{kindEnum}.{NameFormatter.FormatNameAsEnumValue(excludeType)}";

            string propertyNameDisjunction = string.Join("Or", this.propertyNames.Select(p => NameFormatter.FormatNameAsProperty(p)));
            this.coreName = $"{propertyNameDisjunction}{this.excludeType}{(this.isNarrow ? "Narrow" : string.Empty)}";
            this.methodName = $"TryGetDescendant{this.coreName}";
        }

        /// <inheritdoc/>
        public bool AppliesToType(string typeName)
        {
            return this.rootClass == typeName;
        }

        /// <inheritdoc/>
        public void AddMembers(CsClass obverseClass, string typeName, bool classIsBase, bool classIsAbstract, List<MaterialProperty> materialProperties)
        {
            if (obverseClass.HasMethod(this.methodName))
            {
                return;
            }

            if (classIsBase)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Abstract, ParserGeneratorValues.ObverseTypeBoolean, this.methodName);
                baseClassMethod.Summary($"Get the ID of any {this.excludeType} element of a descendant {string.Join(" or ", this.propertyNames)} property, if such exists.");
                baseClassMethod.Param(ParserGeneratorValues.IdentifierType, "elementId", "An out parameter providing the element ID.", passage: Passage.Out);
                baseClassMethod.Param("Dictionary<string, JsonLdElement>", "excludedElts", "An out parameter providing a dictionary that maps from layer name to the <see cref=\"JsonLdElement\"/> that defines the layer of the element.", passage: Passage.Out);
                baseClassMethod.Returns("True if the type is found among the relevant descendant properties.");
            }
            else if (!classIsAbstract)
            {
                string checkedForFieldName = $"checkedForDescendant{this.coreName}";
                obverseClass.Field(Access.Private, ParserGeneratorValues.ObverseTypeBoolean, checkedForFieldName, ParserGeneratorValues.ObverseValueFalse);

                string idOfFieldName = $"idOfDescendant{this.coreName}";
                string eltsOfFieldName = $"eltsOfDescendant{this.coreName}";
                obverseClass.Field(Access.Private, ParserGeneratorValues.IdentifierType, idOfFieldName, "null");
                obverseClass.Field(Access.Private, "Dictionary<string, JsonLdElement>", eltsOfFieldName, "null");

                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, this.methodName);
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param(ParserGeneratorValues.IdentifierType, "elementId", passage: Passage.Out);
                concreteClassMethod.Param("Dictionary<string, JsonLdElement>", "excludedElts", passage: Passage.Out);

                concreteClassMethod.Body.If($"this.{checkedForFieldName}")
                    .Line($"elementId = this.{idOfFieldName};")
                    .Line($"excludedElts = this.{eltsOfFieldName};")
                    .Line($"return this.{idOfFieldName} != null;");

                concreteClassMethod.Body.Line($"this.{checkedForFieldName} = true;");
                concreteClassMethod.Body.Break();

                foreach (MaterialProperty materialProperty in materialProperties)
                {
                    bool isRelevantProperty = this.propertyNames.Contains(materialProperty.PropertyName);

                    if (materialProperty.PropertyKind == PropertyKind.Object && (isRelevantProperty || !this.isNarrow))
                    {
                        string varName = "item";
                        CsScope iterationScope = materialProperty.Iterate(concreteClassMethod.Body, ref varName);

                        if (isRelevantProperty)
                        {
                            iterationScope.If($"{varName}.{this.kindProperty} == {this.excludeKind}")
                                .Line($"elementId = {varName}.{ParserGeneratorValues.IdentifierName};")
                                .Line($"excludedElts = {varName}.JsonLdElements;")
                                .Line($"this.{idOfFieldName} = elementId;")
                                .Line($"this.{eltsOfFieldName} = excludedElts;")
                                .Line("return true;");
                        }

                        iterationScope.If($"{varName}.{this.methodName}(out elementId, out excludedElts)")
                            .Line($"this.{idOfFieldName} = elementId;")
                            .Line($"this.{eltsOfFieldName} = excludedElts;")
                            .Line("return true;");
                    }
                }

                concreteClassMethod.Body.Line("elementId = null;");
                concreteClassMethod.Body.Line("excludedElts = null;");
                concreteClassMethod.Body.Line("return false;");
            }
        }

        /// <inheritdoc/>
        public void AddRestriction(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName)
        {
            if (this.dtdlVersion == dtdlVersion && this.rootClass == typeName)
            {
                string propertiesDesc = string.Join(" or ", this.propertyNames.Select(p => $"'{p}'"));
                string elementIdName = $"excluded{this.excludeType}ElementId";
                string excludedEltsName = $"excluded{this.excludeType}Elts";

                CsIf ifGotRestrictedType = checkRestrictionsMethodBody.If($"this.{this.methodName}(out Dtmi {elementIdName}, out Dictionary<string, JsonLdElement> {excludedEltsName})");

                string.Join(" || ", this.propertyNames.Select(p => $"p.Name == \"{p}\""));

                ifGotRestrictedType
                    .Line($"JsonLdElement ancestorElt = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => {string.Join(" || ", this.propertyNames.Select(p => $"p.Name == \"{p}\""))})).Value;")
                    .Line($"JsonLdElement descendantElt = {excludedEltsName}.FirstOrDefault(e => e.Value.Types.Contains(\"{this.excludeType}\")).Value;")
                    .Break();

                ifGotRestrictedType
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"excludedType\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyDisjunction: \"{propertiesDesc}\",")
                        .Line($"referenceId: {elementIdName},")
                        .Line($"referenceType: \"{this.excludeType}\",")
                        .Line($"elementType: \"{typeName}\",")
                        .Line("ancestorElement: ancestorElt,")
                        .Line("descendantElement: descendantElt);");
            }
        }

        /// <inheritdoc/>
        public void AddTransformation(CsScope applyTransformationsMethodBody, int dtdlVersion, string typeName, List<MaterialProperty> materialProperties)
        {
        }
    }
}
