namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a transformation that imports property values from the relevant descendants of a class.
    /// </summary>
    public class DescendantControlImportProperties : IDescendantControl
    {
        private int dtdlVersion;
        private string rootClass;
        private string definingClass;
        private List<string> propertyNames;
        private bool isNarrow;
        private List<string> importProperties;
        private Dictionary<string, int> maxDepth;
        private string getTransitivePropertiesMethodName;
        private Dictionary<string, string> importPropertyMethodNames;
        private Dictionary<string, string> fieldNames;
        private string propertiesDesc;
        private string maxDepthName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DescendantControlImportProperties"/> class.
        /// </summary>
        /// <param name="dtdlVersion">The version of DTDL that defines the control.</param>
        /// <param name="rootClass">The name of the concete class the control pertains to.</param>
        /// <param name="definingClass">The name of the class in which the the control is defined.</param>
        /// <param name="propertyNames">The names of the properties for which this control is relevant.</param>
        /// <param name="isNarrow">Indicates whether the descendant hierarchy should be scanned only along relevant properties.</param>
        /// <param name="importProperties">A list of names of properties whose values should be imported from the relevant descendants.</param>
        /// <param name="maxDepth">The maximum allowed count of relevant properties in a hierarchical chain.</param>
        public DescendantControlImportProperties(int dtdlVersion, string rootClass, string definingClass, List<string> propertyNames, bool isNarrow, List<string> importProperties, Dictionary<string, int> maxDepth)
        {
            this.dtdlVersion = dtdlVersion;
            this.rootClass = rootClass;
            this.definingClass = definingClass;
            this.propertyNames = propertyNames;
            this.isNarrow = isNarrow;
            this.importProperties = importProperties;
            this.maxDepth = maxDepth;

            string propertyNameDisjunction = string.Join("Or", this.propertyNames.Select(p => NameFormatter.FormatNameAsProperty(p)));
            this.getTransitivePropertiesMethodName = $"GetTransitive{propertyNameDisjunction}{(this.isNarrow ? "Narrow" : string.Empty)}";

            this.importPropertyMethodNames = new Dictionary<string, string>();
            this.fieldNames = new Dictionary<string, string>();
            foreach (string importProperty in this.importProperties)
            {
                string coreName = NameFormatter.FormatNameAsProperty(importProperty);
                this.importPropertyMethodNames[importProperty] = $"Import{coreName}";
                this.fieldNames[importProperty] = $"{ParserGeneratorValues.ShadowPropertyPrefix}{coreName}";
            }

            this.propertiesDesc = string.Join(" or ", this.propertyNames.Select(p => $"'{p}'"));
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
            this.AddGetTransitivePropertiesMethod(obverseClass, classIsBase, classIsAbstract, materialProperties);

            if (this.definingClass == typeName)
            {
                foreach (string importProperty in this.importProperties)
                {
                    this.AddImportPropertyMethods(obverseClass, materialProperties, importProperty);
                    this.AddField(obverseClass, materialProperties, importProperty);
                }
            }
        }

        /// <inheritdoc/>
        public void AddRestriction(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName)
        {
        }

        /// <inheritdoc/>
        public void AddTransformation(CsScope applyTransformationsMethodBody, int dtdlVersion, string typeName, List<MaterialProperty> materialProperties)
        {
            if (this.dtdlVersion == dtdlVersion && this.rootClass == typeName)
            {
                foreach (string importProperty in this.importProperties)
                {
                    MaterialProperty materialProperty = materialProperties.Where(mp => mp.PropertyName == importProperty).FirstOrDefault();

                    string importPropName = NameFormatter.FormatNameAsProperty(importProperty);

                    applyTransformationsMethodBody.If($"this.{ParserGeneratorValues.ShadowPropertyPrefix}{importPropName} == null")
                        .Line($"this.{ParserGeneratorValues.ShadowPropertyPrefix}{importPropName} = new {materialProperty.ConcretePropertyType}(({materialProperty.ConcretePropertyType})this.{importPropName});");
                }

                ValueLimiter.DefineLimitVariable(applyTransformationsMethodBody, this.maxDepth, this.maxDepthName, $"this.{ParserGeneratorValues.LimitSpecifierPropertyName}", nullable: false);

                applyTransformationsMethodBody.Line($"HashSet<Dtmi> sources = this.{this.getTransitivePropertiesMethodName}(0, {this.maxDepthName}, out Dtmi tooDeepElementId, out Dictionary<string, JsonLdElement> tooDeepElts, parsingErrorCollection);");
                applyTransformationsMethodBody.Break();

                CsIf ifSourcesNotNull = applyTransformationsMethodBody.If("sources != null");

                CsForEach forEachDtmi = ifSourcesNotNull.ForEach("Dtmi dtmi in sources");

                foreach (string importProperty in this.importProperties)
                {
                    MaterialProperty materialProperty = materialProperties.Where(mp => mp.PropertyName == importProperty).FirstOrDefault();

                    string importPropName = NameFormatter.FormatNameAsProperty(importProperty);

                    forEachDtmi.Line($"(({NameFormatter.FormatNameAsClass(this.definingClass)})model.Dict[dtmi]).Import{importPropName}(this.{ParserGeneratorValues.IdentifierName}, \"{this.propertyNames.First()}\", \"{this.propertiesDesc}\", this.{importPropName}, this.JsonLdElements, parsingErrorCollection);");
                }

                ifSourcesNotNull.ElseIf("tooDeepElementId != null")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line(this.isNarrow ? "\"excessiveDepthNarrow\"," : "\"excessiveDepthWide\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyDisjunction: \"{this.propertiesDesc}\",")
                        .Line($"referenceId: tooDeepElementId,")
                        .Line($"observedCount: {this.maxDepthName} + 1,")
                        .Line($"expectedCount: {this.maxDepthName},")
                        .Line("ancestorElement: this.JsonLdElements.First().Value,")
                        .Line("descendantElement: tooDeepElts.First().Value);");
            }
        }

        private void AddGetTransitivePropertiesMethod(CsClass obverseClass, bool classIsBase, bool classIsAbstract, List<MaterialProperty> materialProperties)
        {
            if (obverseClass.HasMethod(this.getTransitivePropertiesMethodName))
            {
                return;
            }

            if (classIsBase)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Abstract, $"HashSet<{ParserGeneratorValues.IdentifierType}>", this.getTransitivePropertiesMethodName);
                baseClassMethod.Summary($"Get the transitive closure of the IDs of all descendant {string.Join(" or ", this.propertyNames)} properties, and also check the nesting depth.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depth", "The depth from the root to this element.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depthLimit", "The allowed limit on the depth.");
                baseClassMethod.Param(ParserGeneratorValues.IdentifierType, "tooDeepElementId", "An out parameter for the ID of the first element that exceeds the depth.", passage: Passage.Out);
                baseClassMethod.Param("Dictionary<string, JsonLdElement>", "tooDeepElts", "An out parameter providing a dictionary that maps from layer name to the <see cref=\"JsonLdElement\"/> that defines the layer of the first element that exceeds the depth.", passage: Passage.Out);
                baseClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
                baseClassMethod.Returns("A <c>HashSet</c> of the IDs, or null if the depth exceeds the limit.");
            }
            else if (!classIsAbstract)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, "HashSet<Dtmi>", this.getTransitivePropertiesMethodName);
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depth");
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeInteger, "depthLimit");
                concreteClassMethod.Param(ParserGeneratorValues.IdentifierType, "tooDeepElementId", passage: Passage.Out);
                concreteClassMethod.Param("Dictionary<string, JsonLdElement>", "tooDeepElts", passage: Passage.Out);
                concreteClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                foreach (MaterialProperty materialProperty in materialProperties)
                {
                    if (materialProperty.PropertyKind == PropertyKind.Object && this.propertyNames.Contains(materialProperty.PropertyName))
                    {
                        string varName = "item";
                        materialProperty.One(concreteClassMethod.Body, ref varName)
                            .If("depth == depthLimit")
                                .Line($"tooDeepElementId = {varName}.{ParserGeneratorValues.IdentifierName};")
                                .Line($"tooDeepElts = {varName}.JsonLdElements;")
                                .Line("return null;");
                    }
                }

                concreteClassMethod.Body.Line("HashSet<Dtmi> closure = new HashSet<Dtmi>();");
                concreteClassMethod.Body.Break();

                foreach (MaterialProperty materialProperty in materialProperties)
                {
                    bool isRelevantProperty = this.propertyNames.Contains(materialProperty.PropertyName);
                    string conditionalIncrement = isRelevantProperty ? " + 1" : string.Empty;

                    if (materialProperty.PropertyKind == PropertyKind.Object && (isRelevantProperty || !this.isNarrow))
                    {
                        string varName = "item";
                        CsScope iterationScope = materialProperty.Iterate(concreteClassMethod.Body, ref varName);

                        iterationScope.Line($"HashSet<Dtmi> others = {varName}.{this.getTransitivePropertiesMethodName}(depth{conditionalIncrement}, depthLimit, out tooDeepElementId, out tooDeepElts, parsingErrorCollection);");

                        CsIf ifOthersNotNull = iterationScope.If("others != null");

                        if (isRelevantProperty)
                        {
                            ifOthersNotNull.Line($"closure.Add({varName}.{ParserGeneratorValues.IdentifierName});");
                        }

                        ifOthersNotNull.Line("closure.UnionWith(others);");

                        CsElse othersIsNull = ifOthersNotNull.Else();

                        CsIf ifTooDeepElementIsThis = othersIsNull.If($"tooDeepElementId == this.{ParserGeneratorValues.IdentifierName}");

                        ifTooDeepElementIsThis
                            .MultiLine("parsingErrorCollection.Notify(")
                                .Line(this.isNarrow ? "\"recursiveStructureNarrow\"," : "\"recursiveStructureWide\",")
                                .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                                .Line($"propertyDisjunction: \"{this.propertiesDesc}\",")
                                .Line("element: this.JsonLdElements.First().Value);");

                        ifTooDeepElementIsThis.Line("tooDeepElementId = null;");

                        othersIsNull.Line("return null;");
                    }
                }

                concreteClassMethod.Body
                    .Line("tooDeepElementId = null;")
                    .Line("tooDeepElts = null;")
                    .Line("return closure;");
            }
        }

        private void AddImportPropertyMethods(CsClass obverseClass, List<MaterialProperty> materialProperties, string importProperty)
        {
            string methodName = this.importPropertyMethodNames[importProperty];
            if (obverseClass.HasMethod(methodName))
            {
                return;
            }

            string fieldName = this.fieldNames[importProperty];
            string paramName = NameFormatter.FormatNameAsParameter(importProperty);

            MaterialProperty materialProperty = materialProperties.Where(mp => mp.PropertyName == importProperty).FirstOrDefault();

            CsMethod method = obverseClass.Method(Access.Internal, Novelty.Normal, "void", methodName);
            method.Summary($"Copy the values of this object's {NameFormatter.FormatNameAsProperty(importProperty)} property into <paramref name=\"{paramName}\"/>.");

            method.Param(ParserGeneratorValues.IdentifierType, "ancestorId", "The identifier of the ancestor element whose obverse class invokes the method.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "importPropertyName", "The name of the property responsible for the importing.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "importPropertyDesc", "A description of the property responsible for the importing.");
            method.Param(materialProperty.PropertyType, paramName, "The destination for the copied values.");
            method.Param("Dictionary<string, JsonLdElement>", "importerElements", "A dictionary that maps from layer name to the <see cref=\"JsonLdElement\"/> that defines the layer of the importing element.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");

            if (materialProperty.Representation == PropertyRepresentation.List)
            {
                method.Body.Line($"{paramName}.AddRange(this.{NameFormatter.FormatNameAsProperty(importProperty)});");
            }
            else if (materialProperty.Representation == PropertyRepresentation.Dictionary)
            {
                CsForEach forEachKvp = method.Body.ForEach($"var kvp in this.{fieldName} ?? this.{NameFormatter.FormatNameAsProperty(materialProperty.PropertyName)}");

                CsIf ifGetExtantValue = forEachKvp.If($"{paramName}.TryGetValue(kvp.Key, out var extantValue)");
                CsIf ifIdMismatch = ifGetExtantValue.If($"extantValue.{ParserGeneratorValues.IdentifierName} != kvp.Value.{ParserGeneratorValues.IdentifierName}");

                ifIdMismatch
                    .Line($"JsonLdProperty keyPropProp = kvp.Value.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{materialProperty.KeyProperty}\")).Value?.Properties?.First(p => p.Name == \"{materialProperty.KeyProperty}\");")
                    .Line($"JsonLdProperty dupKeyProp = extantValue.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{materialProperty.KeyProperty}\")).Value?.Properties?.First(p => p.Name == \"{materialProperty.KeyProperty}\");")
                    .Break();

                ifIdMismatch
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"nonUniqueImportedPropertyValue\",")
                        .Line("elementId: ancestorId,")
                        .Line($"referenceId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyName: \"{importProperty}\",")
                        .Line("refPropertyName: importPropertyDesc,")
                        .Line($"nestedName: \"{materialProperty.KeyProperty}\",")
                        .Line("nestedValue: kvp.Key,")
                        .Line("incidentProperty: dupKeyProp,")
                        .Line("extantProperty: keyPropProp);");

                ifGetExtantValue.Else()
                    .Line($"(({materialProperty.ConcretePropertyType}){paramName})[kvp.Key] = kvp.Value;")
                    .Line($"kvp.Value.{ParserGeneratorValues.ParentReferencesName}.Add(new ParentReference() {{ ParentId = ancestorId, PropertyName = \"{materialProperty.PropertyName}\" }});");
            }
        }

        private void AddField(CsClass obverseClass, List<MaterialProperty> materialProperties, string importProperty)
        {
            string fieldName = this.fieldNames[importProperty];
            if (obverseClass.HasField(fieldName))
            {
                return;
            }

            MaterialProperty materialProperty = materialProperties.Where(mp => mp.PropertyName == importProperty).FirstOrDefault();

            obverseClass.Field(Access.Private, materialProperty.PropertyType, fieldName, "null");
        }
    }
}
