namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a restriction that limits the count of specified descendants of a class.
    /// </summary>
    public class DescendantControlMaxCount : IDescendantControl
    {
        private int dtdlVersion;
        private string rootClass;
        private List<string> propertyNames;
        private bool isNarrow;
        private Dictionary<string, int> maxCount;
        private string coreName;
        private string methodName;
        private string propertiesDesc;
        private string maxCountName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DescendantControlMaxCount"/> class.
        /// </summary>
        /// <param name="dtdlVersion">The version of DTDL that defines the control.</param>
        /// <param name="rootClass">The name of the concete class the control pertains to.</param>
        /// <param name="propertyNames">The names of the properties for which this control is relevant.</param>
        /// <param name="isNarrow">Indicates whether the descendant hierarchy should be scanned only along relevant properties.</param>
        /// <param name="maxCount">The maximum allowed count of relevant properties in a hierarchy, according to a limit spec.</param>
        public DescendantControlMaxCount(int dtdlVersion, string rootClass, List<string> propertyNames, bool isNarrow, Dictionary<string, int> maxCount)
        {
            this.dtdlVersion = dtdlVersion;
            this.rootClass = rootClass;
            this.propertyNames = propertyNames;
            this.isNarrow = isNarrow;
            this.maxCount = maxCount;

            string propertyNameDisjunction = string.Join("Or", this.propertyNames.Select(p => NameFormatter.FormatNameAsProperty(p)));
            this.coreName = $"{propertyNameDisjunction}{(this.isNarrow ? "Narrow" : string.Empty)}";
            this.methodName = $"GetCountOf{this.coreName}";
            this.propertiesDesc = string.Join(" or ", this.propertyNames.Select(p => $"'{p}'"));
            this.maxCountName = $"maxCountOf{propertyNameDisjunction}";
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
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Abstract, ParserGeneratorValues.ObverseTypeInteger, this.methodName);
                baseClassMethod.Summary($"Get the count of all descendant {string.Join(" or ", this.propertyNames)} properties.");
                baseClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
                baseClassMethod.Returns("The count of relevant property values.");
            }
            else if (!classIsAbstract)
            {
                string statusFieldName = $"countOf{this.coreName}Status";
                obverseClass.Field(Access.Private, "TraversalStatus", statusFieldName, "TraversalStatus.NotStarted");

                string valueFieldName = $"countOf{this.coreName}Value";
                obverseClass.Field(Access.Private, ParserGeneratorValues.ObverseTypeInteger, valueFieldName, "0");

                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, ParserGeneratorValues.ObverseTypeInteger, this.methodName);
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                concreteClassMethod.Body.If($"this.{statusFieldName} == TraversalStatus.Complete")
                    .Line($"return this.{valueFieldName};");

                CsIf ifInProgress = concreteClassMethod.Body.If($"this.{statusFieldName} == TraversalStatus.InProgress");

                ifInProgress
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line(this.isNarrow ? "\"recursiveStructureNarrow\"," : "\"recursiveStructureWide\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyDisjunction: \"{this.propertiesDesc}\",")
                        .Line("element: this.JsonLdElements.First().Value);");

                ifInProgress.Line("return 0;");

                concreteClassMethod.Body.Line($"this.{statusFieldName} = TraversalStatus.InProgress;");

                foreach (MaterialProperty materialProperty in materialProperties)
                {
                    bool isRelevantProperty = this.propertyNames.Contains(materialProperty.PropertyName);
                    string conditionalIncrement = isRelevantProperty ? " + 1" : string.Empty;

                    if (materialProperty.PropertyKind == PropertyKind.Object && (isRelevantProperty || !this.isNarrow))
                    {
                        string varName = "item";
                        materialProperty.Iterate(concreteClassMethod.Body, ref varName)
                            .Line($"this.{valueFieldName} += {varName}.{this.methodName}(parsingErrorCollection){conditionalIncrement};");
                    }
                }

                concreteClassMethod.Body.Line($"this.{statusFieldName} = TraversalStatus.Complete;");
                concreteClassMethod.Body.Line($"return this.{valueFieldName};");
            }
        }

        /// <inheritdoc/>
        public void AddRestriction(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName)
        {
            if (this.dtdlVersion == dtdlVersion && this.rootClass == typeName)
            {
                ValueLimiter.DefineLimitVariable(checkRestrictionsMethodBody, this.maxCount, this.maxCountName, $"this.{ParserGeneratorValues.LimitSpecifierPropertyName}", nullable: false);

                checkRestrictionsMethodBody
                    .Line($"{ParserGeneratorValues.ObverseTypeInteger} num{this.coreName}Values = this.{this.methodName}(parsingErrorCollection);")
                    .If($"num{this.coreName}Values > {this.maxCountName}")
                        .MultiLine("parsingErrorCollection.Notify(")
                            .Line("\"excessiveCount\",")
                            .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                            .Line($"propertyDisjunction: \"{this.propertiesDesc}\",")
                            .Line($"observedCount: num{this.coreName}Values,")
                            .Line($"expectedCount: {this.maxCountName},")
                            .Line("element: this.JsonLdElements.First().Value);");
            }
        }

        /// <inheritdoc/>
        public void AddTransformation(CsScope applyTransformationsMethodBody, int dtdlVersion, string typeName, List<MaterialProperty> materialProperties)
        {
        }
    }
}
