namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a restriction that limits the depth of specified descendants of a class.
    /// </summary>
    public class DescendantControlMaxDepth : IDescendantControl
    {
        private int dtdlVersion;
        private string rootClass;
        private List<string> propertyNames;
        private bool isNarrow;
        private Dictionary<string, int> maxDepth;
        private bool allowSelf;
        private string methodName;
        private string propertiesDesc;
        private string elementIdsName;
        private string jsonLdEltsName;
        private string maxDepthName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DescendantControlMaxDepth"/> class.
        /// </summary>
        /// <param name="dtdlVersion">The version of DTDL that defines the control.</param>
        /// <param name="rootClass">The name of the concete class the control pertains to.</param>
        /// <param name="propertyNames">The names of the properties for which this control is relevant.</param>
        /// <param name="isNarrow">Indicates whether the descendant hierarchy should be scanned only along relevant properties.</param>
        /// <param name="maxDepth">The maximum allowed count of relevant properties in a hierarchical chain, according to a limit spec.</param>
        /// <param name="allowSelf">True if descendants are permitted to refer to the object at the root of the hierarchy.</param>
        public DescendantControlMaxDepth(int dtdlVersion, string rootClass, List<string> propertyNames, bool isNarrow, Dictionary<string, int> maxDepth, bool allowSelf)
        {
            this.dtdlVersion = dtdlVersion;
            this.rootClass = rootClass;
            this.propertyNames = propertyNames;
            this.isNarrow = isNarrow;
            this.maxDepth = maxDepth;
            this.allowSelf = allowSelf;

            string propertyNameDisjunction = string.Join("Or", this.propertyNames.Select(p => NameFormatter.FormatNameAsProperty(p)));
            this.methodName = $"CheckDepthOf{propertyNameDisjunction}{(this.isNarrow ? "Narrow" : string.Empty)}";
            this.propertiesDesc = string.Join(" or ", this.propertyNames.Select(p => $"'{p}'"));
            this.elementIdsName = $"tooDeep{propertyNameDisjunction}ElementIds";
            this.jsonLdEltsName = $"tooDeep{propertyNameDisjunction}Elts";
            this.maxDepthName = $"maxDepthOf{propertyNameDisjunction}";
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
                baseClassMethod.Summary($"Check the nesting depth of all descendant {string.Join(" or ", this.propertyNames)} properties.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depth", "The depth from the root to this element.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depthLimit", "The allowed limit on the depth.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowSelf", "True if descendants are permitted to refer to the object at the root of the hierarchy.");
                baseClassMethod.Param($"List<{ParserGeneratorValues.IdentifierType}>", "tooDeepElementIds", "A logica out parameter for the IDs of the chain of elements that exceed the depth.");
                baseClassMethod.Param("Dictionary<string, JsonLdElement>", "tooDeepElts", "An out parameter providing a dictionary that maps from layer name to the <see cref=\"JsonLdElement\"/> that defines the layer of the first element that exceeds the depth.", passage: Passage.Out);
                baseClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
                baseClassMethod.Returns("True if the depth is within the limit.");
            }
            else if (!classIsAbstract)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, this.methodName);
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depth");
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depthLimit");
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowSelf");
                concreteClassMethod.Param($"List<{ParserGeneratorValues.IdentifierType}>", "tooDeepElementIds");
                concreteClassMethod.Param("Dictionary<string, JsonLdElement>", "tooDeepElts", passage: Passage.Out);
                concreteClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                foreach (MaterialProperty materialProperty in materialProperties)
                {
                    if (materialProperty.PropertyKind == PropertyKind.Object && this.propertyNames.Contains(materialProperty.PropertyName))
                    {
                        materialProperty.CheckPresence(concreteClassMethod.Body)
                            .If("depth == depthLimit")
                                .Line($"tooDeepElementIds.Add(this.{ParserGeneratorValues.IdentifierName});")
                                .Line($"tooDeepElts = this.JsonLdElements;")
                                .Line("return false;");
                    }
                }

                foreach (MaterialProperty materialProperty in materialProperties)
                {
                    bool isRelevantProperty = this.propertyNames.Contains(materialProperty.PropertyName);
                    string conditionalIncrement = isRelevantProperty ? " + 1" : string.Empty;

                    if (materialProperty.PropertyKind == PropertyKind.Object && (isRelevantProperty || !this.isNarrow))
                    {
                        string varName = "item";
                        CsScope iterationScope = materialProperty.Iterate(concreteClassMethod.Body, ref varName);

                        CsIf ifTooDeep = iterationScope.If($"!{varName}.{this.methodName}(depth{conditionalIncrement}, depthLimit, allowSelf, tooDeepElementIds, out tooDeepElts, parsingErrorCollection)");

                        CsIf ifTooDeepElementIsThis = ifTooDeep.If($"tooDeepElementIds.Contains(this.{ParserGeneratorValues.IdentifierName})");

                        ifTooDeepElementIsThis
                            .If("!allowSelf")
                                .MultiLine("parsingErrorCollection.Notify(")
                                    .Line(this.isNarrow ? "\"recursiveStructureNarrow\"," : "\"recursiveStructureWide\",")
                                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                                    .Line($"propertyDisjunction: \"{this.propertiesDesc}\",")
                                    .Line("element: this.JsonLdElements.First().Value);");

                        ifTooDeepElementIsThis
                            .Line("tooDeepElementIds.Clear();")
                            .Line("return true;");

                        ifTooDeep
                            .Line("tooDeepElementIds.Add(this.Id);")
                            .Line("return false;");
                    }
                }

                concreteClassMethod.Body
                    .Line($"tooDeepElts = null;")
                    .Line("return true;");
            }
        }

        /// <inheritdoc/>
        public void AddRestriction(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName)
        {
            if (this.dtdlVersion == dtdlVersion && this.rootClass == typeName)
            {
                ValueLimiter.DefineLimitVariable(checkRestrictionsMethodBody, this.maxDepth, this.maxDepthName, $"this.{ParserGeneratorValues.LimitSpecifierPropertyName}", nullable: false);

                checkRestrictionsMethodBody.Line($"List<{ParserGeneratorValues.IdentifierType}> {this.elementIdsName} = new List<{ParserGeneratorValues.IdentifierType}>();");
                checkRestrictionsMethodBody.If($"!this.{this.methodName}(0, {this.maxDepthName}, {ParserGeneratorValues.GetBooleanLiteral(this.allowSelf)}, {this.elementIdsName}, out Dictionary<string, JsonLdElement> {this.jsonLdEltsName}, parsingErrorCollection) && {this.elementIdsName} != null")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line(this.isNarrow ? "\"excessiveDepthNarrow\"," : "\"excessiveDepthWide\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyDisjunction: \"{this.propertiesDesc}\",")
                        .Line($"referenceId: {this.elementIdsName}.First(),")
                        .Line($"observedCount: {this.maxDepthName} + 1,")
                        .Line($"expectedCount: {this.maxDepthName},")
                        .Line("ancestorElement: this.JsonLdElements.First().Value,")
                        .Line($"descendantElement: {this.jsonLdEltsName}.First().Value);");
            }
        }

        /// <inheritdoc/>
        public void AddTransformation(CsScope applyTransformationsMethodBody, int dtdlVersion, string typeName, List<MaterialProperty> materialProperties)
        {
        }
    }
}
