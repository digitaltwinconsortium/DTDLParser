namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a restriction that enforces a matching datatype on specified descendants of a class.
    /// </summary>
    public class DescendantControlDatatypeProperty : IDescendantControl
    {
        private int dtdlVersion;
        private string rootClass;
        private List<string> propertyNames;
        private bool isNarrow;
        private string datatypeProperty;
        private string coreName;
        private string checkMethodName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DescendantControlDatatypeProperty"/> class.
        /// </summary>
        /// <param name="dtdlVersion">The version of DTDL that defines the control.</param>
        /// <param name="rootClass">The name of the concete class the control pertains to.</param>
        /// <param name="propertyNames">The names of the properties for which this control is relevant.</param>
        /// <param name="isNarrow">Indicates whether the descendant hierarchy should be scanned only along relevant properties.</param>
        /// <param name="datatypeProperty">The property whose value determines the required datatype of the relevant properties.</param>
        public DescendantControlDatatypeProperty(int dtdlVersion, string rootClass, List<string> propertyNames, bool isNarrow, string datatypeProperty)
        {
            this.dtdlVersion = dtdlVersion;
            this.rootClass = rootClass;
            this.propertyNames = propertyNames;
            this.isNarrow = isNarrow;
            this.datatypeProperty = datatypeProperty;

            string propertyNameDisjunction = string.Join("Or", this.propertyNames.Select(p => NameFormatter.FormatNameAsProperty(p)));
            this.coreName = $"{propertyNameDisjunction}{(this.isNarrow ? "Narrow" : string.Empty)}";
            this.checkMethodName = $"CheckDescendant{this.coreName}Datatype";
        }

        /// <inheritdoc/>
        public bool AppliesToType(string typeName)
        {
            return this.rootClass == typeName;
        }

        /// <inheritdoc/>
        public void AddMembers(CsClass obverseClass, string typeName, bool classIsBase, bool classIsAbstract, List<MaterialProperty> materialProperties)
        {
            this.AddCheckMethod(obverseClass, classIsBase, classIsAbstract, materialProperties);
        }

        /// <inheritdoc/>
        public void AddRestriction(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName)
        {
            if (this.dtdlVersion == dtdlVersion && this.rootClass == typeName)
            {
                CsSwitch switchOnUri = checkRestrictionsMethodBody.Switch($"Dtmi.TryCreateDtmi(this.{NameFormatter.FormatNameAsProperty(this.datatypeProperty)}.{ParserGeneratorValues.IdentifierName}.OriginalString, out Dtmi valueSchema) ? valueSchema.Versionless : string.Empty");

                switchOnUri.Case($"\"dtmi:dtdl:instance:Schema:integer\"");
                switchOnUri.Line($"this.{this.checkMethodName}(this.{ParserGeneratorValues.IdentifierName}, this.JsonLdElements, typeof({ParserGeneratorValues.ObverseTypeInteger}), parsingErrorCollection);");
                switchOnUri.Line("break;");

                switchOnUri.Case($"\"dtmi:dtdl:instance:Schema:string\"");
                switchOnUri.Line($"this.{this.checkMethodName}(this.{ParserGeneratorValues.IdentifierName}, this.JsonLdElements, typeof({ParserGeneratorValues.ObverseTypeString}), parsingErrorCollection);");
                switchOnUri.Line("break;");

                switchOnUri.Case($"\"dtmi:dtdl:instance:Schema:boolean\"");
                switchOnUri.Line($"this.{this.checkMethodName}(this.{ParserGeneratorValues.IdentifierName}, this.JsonLdElements, typeof({ParserGeneratorValues.ObverseTypeBoolean}), parsingErrorCollection);");
                switchOnUri.Line("break;");
            }
        }

        /// <inheritdoc/>
        public void AddTransformation(CsScope applyTransformationsMethodBody, int dtdlVersion, string typeName, List<MaterialProperty> materialProperties)
        {
        }

        private void AddCheckMethod(CsClass obverseClass, bool classIsBase, bool classIsAbstract, List<MaterialProperty> materialProperties)
        {
            if (obverseClass.HasMethod(this.checkMethodName))
            {
                return;
            }

            if (classIsBase)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Abstract, "void", this.checkMethodName);
                baseClassMethod.Summary($"Check that the datatype of every descendant {string.Join(" or ", this.propertyNames)} property matches <paramref name=\"datatype\"/>.");
                baseClassMethod.Param(ParserGeneratorValues.IdentifierType, "ancestorId", "The identifier of the ancestor element that specifies the required datatype.");
                baseClassMethod.Param("Dictionary<string, JsonLdElement>", "ancestorElts", "An out parameter providing a dictionary that maps from layer name to the <see cref=\"JsonLdElement\"/> that defines the layer of the ancestor element.");
                baseClassMethod.Param("Type", "datatype", "The required datatype.");
                baseClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            }
            else if (!classIsAbstract)
            {
                string checkedFieldName = $"checkedDescendant{this.coreName}Datatype";
                obverseClass.Field(Access.Private, "Type", checkedFieldName, "null");

                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, "void", this.checkMethodName);
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param(ParserGeneratorValues.IdentifierType, "ancestorId");
                concreteClassMethod.Param("Dictionary<string, JsonLdElement>", "ancestorElts");
                concreteClassMethod.Param("Type", "datatype");
                concreteClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                CsIf ifNonMatch = concreteClassMethod.Body.If($"this.{checkedFieldName} != datatype");

                ifNonMatch.Line($"this.{checkedFieldName} = datatype;");

                foreach (MaterialProperty materialProperty in materialProperties)
                {
                    if (materialProperty.PropertyKind == PropertyKind.Object && !this.isNarrow)
                    {
                        string varName = "item";
                        materialProperty.Iterate(ifNonMatch, ref varName)
                            .Line($"{varName}.{this.checkMethodName}(ancestorId, ancestorElts, datatype, parsingErrorCollection);");
                    }

                    if (materialProperty.PropertyKind == PropertyKind.Literal && this.propertyNames.Contains(materialProperty.PropertyName))
                    {
                        string varName = "item";
                        CsIf ifNonConformant = materialProperty.Iterate(ifNonMatch, ref varName).If($"{varName}.GetType() != datatype");

                        ifNonConformant
                            .Line($"JsonLdProperty mismatchedProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{materialProperty.PropertyName}\")).Value?.Properties?.First(p => p.Name == \"{materialProperty.PropertyName}\");")
                            .Line($"JsonLdProperty requirementProp = ancestorElts.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{this.datatypeProperty}\")).Value?.Properties?.First(p => p.Name == \"{this.datatypeProperty}\");")
                            .Break();

                        ifNonConformant
                            .MultiLine("parsingErrorCollection.Notify(")
                                .Line("\"nonConformantDatatype\",")
                                .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                                .Line($"propertyName: \"{materialProperty.PropertyName}\",")
                                .Line("governingId: ancestorId,")
                                .Line($"governingPropertyName: \"{this.datatypeProperty}\",")
                                .Line($"propertyValue: {varName}.ToString(),")
                                .Line($"datatype: Helpers.GetDatytypeString(datatype),")
                                .Line("incidentProperty: mismatchedProp,")
                                .Line("governingProperty: requirementProp);");
                    }
                }
            }
        }
    }
}
