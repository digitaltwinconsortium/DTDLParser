namespace DTDLParser
{
    /// <summary>
    /// Class for adding code to material classes that are marked as augmentable.
    /// </summary>
    public static class MaterialClassAugmentor
    {
        /// <summary>
        /// Generate code for the constructor of the material class.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsOvert">True if the DTDL type is permitted to be used in a DTDL model.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        /// <param name="implicitTypeVar">Name of the variable that holds an implicit supplemental type identifier.</param>
        public static void GenerateConstructorCode(CsScope scope, bool classIsOvert, bool classIsAugmentable, string implicitTypeVar)
        {
            if (classIsAugmentable)
            {
                scope.Break();
                scope.Line($"this.supplementalTypeIds = new HashSet<{ParserGeneratorValues.IdentifierType}>();");
                scope.Line("this.supplementalProperties = new Dictionary<string, object>();");
                scope.Line("this.supplementalTypes = new List<DTSupplementalTypeInfo>();");

                if (!classIsOvert)
                {
                    scope.Line($"this.implicitSupplementalTypeId = {implicitTypeVar};");
                }
            }
        }

        /// <summary>
        /// Generate appropriate members for the material class.
        /// </summary>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="classIsOvert">True if the DTDL type is permitted to be used in a DTDL model.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        /// <param name="anyObjectProperties">True if the material class as any object properties.</param>
        public static void AddMembers(CsClass obverseClass, string typeName, bool classIsOvert, bool classIsAugmentable, bool classIsBase, bool anyObjectProperties)
        {
            CsMethod addTypeMethod = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Virtual : Novelty.Override, "void", "AddType");
            addTypeMethod.Summary("Add a supplemental type.");
            addTypeMethod.Param(ParserGeneratorValues.IdentifierType, NameFormatter.FormatNameAsParameter(ParserGeneratorValues.IdentifierName), "Identifier for the supplemental type to add.");
            addTypeMethod.Param("DTSupplementalTypeInfo", "supplementalType", "<c>DTSupplementalTypeInfo</c> for the supplemental type.");
            addTypeMethod.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            if (classIsAugmentable)
            {
                CsIf ifTypeAdded = addTypeMethod.Body.If($"this.supplementalTypeIds.Add({NameFormatter.FormatNameAsParameter(ParserGeneratorValues.IdentifierName)})");
                ifTypeAdded.Line("this.supplementalTypes.Add(supplementalType);");
                if (anyObjectProperties)
                {
                    ifTypeAdded.Line("supplementalType.AttachConstraints(this);");
                    ifTypeAdded.Line("supplementalType.BindInstanceProperties(this);");

                    CsIf ifConflictingType = ifTypeAdded.If("!supplementalType.TryRecordPropertySources(this.supplementalPropertySources, out string conflictingProperty, out Dtmi conflictingType)");

                    ifConflictingType
                        .Line($"string typeString1 = ContextCollection.GetTermOrUri(conflictingType);")
                        .Line($"string typeString2 = ContextCollection.GetTermOrUri({NameFormatter.FormatNameAsParameter(ParserGeneratorValues.IdentifierName)});")
                        .Line($"string propName = ContextCollection.GetTermOrUri(new {ParserGeneratorValues.IdentifierType}(conflictingProperty));")
                        .Line("JsonLdElement elt = this.JsonLdElements.FirstOrDefault(e => e.Value.Types.Contains(typeString1) && e.Value.Types.Contains(typeString2)).Value;");

                    ifConflictingType
                        .MultiLine("parsingErrorCollection.Notify(")
                            .Line("\"conflictingSupplementalTypes\",")
                            .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                            .Line("elementType: typeString1,")
                            .Line("referenceType: typeString2,")
                            .Line("propertyName: propName,")
                            .Line("element: elt);");
                }
            }
            else
            {
                addTypeMethod.Body.Line($"throw new Exception($\"attempt to add type {{{NameFormatter.FormatNameAsParameter(ParserGeneratorValues.IdentifierName)}}} to non-augmentable type {NameFormatter.FormatNameAsClass(typeName)}\");");
            }

            CsProperty supplementalTypesAccessor = obverseClass.Property(Access.Public, classIsBase ? Novelty.Virtual : Novelty.Override, $"IReadOnlyCollection<{ParserGeneratorValues.IdentifierType}>", ParserGeneratorValues.SupplementalTypesPropertyName);
            supplementalTypesAccessor.Summary("Gets a collection of identifiers, each of which is a <c>Dtmi</c> that indicates a supplemental type that applies to the DTDL element that corresponds to this object.");
            supplementalTypesAccessor.Value("A collection of DTMIs indicating the supplemental types that apply to the DTDL element.");
            if (classIsAugmentable)
            {
                supplementalTypesAccessor.Body().Get()
                    .Line("return this.supplementalTypeIds;");
            }
            else
            {
                supplementalTypesAccessor.Body().Get()
                    .Line($"return new List<{ParserGeneratorValues.IdentifierType}>();");
            }

            CsProperty supplementalPropertiesAccessor = obverseClass.Property(Access.Public, classIsBase ? Novelty.Virtual : Novelty.Override, "IDictionary<string, object>", ParserGeneratorValues.SupplementalPropertiesPropertyName);
            supplementalPropertiesAccessor.Summary("Gets the supplemantal properties of the DTDL element that corresponds to this object.");
            supplementalPropertiesAccessor.Value("A dictionary that maps each string-valued property name to an object that holds the value of the property with the given name.");
            supplementalPropertiesAccessor.Remarks("If the property is a literal in the DTDL model, the object holds a literal value.");
            supplementalPropertiesAccessor.Remarks("If the property is another DTDL element in the model, the object is the C# object that corresponds to this element.");
            if (classIsAugmentable)
            {
                supplementalPropertiesAccessor.Body().Get()
                    .Line("return this.supplementalProperties;");
            }
            else
            {
                supplementalPropertiesAccessor.Body().Get()
                    .Line("return new Dictionary<string, object>();");
            }

            if (classIsAugmentable)
            {
                obverseClass.Field(Access.Private, $"HashSet<{ParserGeneratorValues.IdentifierType}>", "supplementalTypeIds");
                obverseClass.Field(Access.Private, "Dictionary<string, object>", "supplementalProperties");
                obverseClass.Field(Access.Private, "List<DTSupplementalTypeInfo>", "supplementalTypes");
                obverseClass.Field(Access.Private, $"Dictionary<string, {ParserGeneratorValues.IdentifierType}>", "supplementalPropertySources", $"new Dictionary<string, {ParserGeneratorValues.IdentifierType}>()");
                obverseClass.Field(Access.Private, "Dictionary<string, string>", "supplementalSingularPropertyLayers", "new Dictionary<string, string>()");

                if (!classIsOvert)
                {
                    obverseClass.Field(Access.Private, ParserGeneratorValues.IdentifierType, "implicitSupplementalTypeId", mutability: Mutability.ReadOnly);
                }

                CsMethod tryParseSupplementalPropertyMethod = obverseClass.Method(Access.Private, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "TryParseSupplementalProperty");
                tryParseSupplementalPropertyMethod.Param("Model", "model");
                tryParseSupplementalPropertyMethod.Param("HashSet<Dtmi>", "immediateSupplementalTypeIds");
                tryParseSupplementalPropertyMethod.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList");
                tryParseSupplementalPropertyMethod.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints");
                tryParseSupplementalPropertyMethod.Param("AggregateContext", "aggregateContext");
                tryParseSupplementalPropertyMethod.Param(ParserGeneratorValues.ObverseTypeString, "layer");
                tryParseSupplementalPropertyMethod.Param(ParserGeneratorValues.IdentifierType, "definedIn");
                tryParseSupplementalPropertyMethod.Param("ParsingErrorCollection", "parsingErrorCollection");
                tryParseSupplementalPropertyMethod.Param(ParserGeneratorValues.ObverseTypeString, "propName");
                tryParseSupplementalPropertyMethod.Param(ParserGeneratorValues.ObverseTypeBoolean, "globalize");
                tryParseSupplementalPropertyMethod.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowReservedIds");
                tryParseSupplementalPropertyMethod.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowIdReferenceSyntax");
                tryParseSupplementalPropertyMethod.Param(ParserGeneratorValues.ObverseTypeBoolean, "ignoreElementsWithAutoIDsAndDuplicateNames");
                tryParseSupplementalPropertyMethod.Param("JsonLdProperty", "valueCollectionProp");
                tryParseSupplementalPropertyMethod.Param("Dictionary<string, JsonLdProperty>", "supplementalJsonLdProperties");
                tryParseSupplementalPropertyMethod.Body.If("!aggregateContext.TryCreateDtmi(propName, out Dtmi propDtmi)")
                    .Line("return false;");

                if (!classIsOvert)
                {
                    tryParseSupplementalPropertyMethod.Body.If($"aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(implicitSupplementalTypeId, out DTSupplementalTypeInfo implicitSupplementalTypeInfo) && implicitSupplementalTypeInfo.TryParseProperty(model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, layer, this.Id, definedIn, propDtmi.ToString(), globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, valueCollectionProp, ref this.supplementalProperties, supplementalJsonLdProperties, this.supplementalSingularPropertyLayers, this.JsonLdElements)")
                        .Line("return true;");
                }

                tryParseSupplementalPropertyMethod.Body.ForEach("Dtmi supplementalTypeId in immediateSupplementalTypeIds")
                    .If($"aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo) && supplementalTypeInfo.TryParseProperty(model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, layer, this.Id, definedIn, propDtmi.ToString(), globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, valueCollectionProp, ref this.supplementalProperties, supplementalJsonLdProperties, this.supplementalSingularPropertyLayers, this.JsonLdElements)")
                    .Line("return true;");
                tryParseSupplementalPropertyMethod.Body.Line("return false;");
            }
        }

        /// <summary>
        /// Generate code for the Equals method of the material class.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code line.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddEqualsLines(CsSorted sorted, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                sorted.Line("&& this.supplementalTypeIds.SetEquals(other.supplementalTypeIds)");
                sorted.Line("&& Helpers.AreDictionariesIdOrLiteralEqual(this.supplementalProperties, other.supplementalProperties)");
            }
        }

        /// <summary>
        /// Generate code for the DeepEquals method of the material class.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code line.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddDeepEqualsLines(CsSorted sorted, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                sorted.Line("&& this.supplementalTypeIds.SetEquals(other.supplementalTypeIds)");
                sorted.Line("&& Helpers.AreDictionariesDeepOrLiteralEqual(this.supplementalProperties, other.supplementalProperties)");
            }
        }

        /// <summary>
        /// Generate code for the GetHashCode method of the material class.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code line.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddHashLines(CsSorted sorted, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                sorted.Line("hashCode = (hashCode * 131) + Helpers.GetSetLiteralHashCode(this.supplementalTypeIds);");
                sorted.Line("hashCode = (hashCode * 131) + Helpers.GetDictionaryIdOrLiteralHashCode(this.supplementalProperties);");
            }
        }

        /// <summary>
        /// Generate code for the DoesHaveType method of the material class.
        /// </summary>
        /// <param name="multiLine">A <see cref="CsMultiLine"/> object to which to add the code line.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddTypeCheckLine(CsMultiLine multiLine, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                multiLine.Line("|| this.supplementalTypes.Any(ps => ((ITypeChecker)ps).DoesHaveType(typeId))");
            }
        }

        /// <summary>
        /// Generate code to call TryParseSupplementalProperty method.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        /// <param name="supplementalTypesVar">Name of the variable that holds a set of supplemental type IDs.</param>
        /// <param name="jsonLdPropsVar">Name of the variable that holds a dictionary that maps from property name to a <c>JsonLdProperty</c> that defines the property.</param>
        public static void AddTryParseSupplementalProperty(CsScope scope, bool classIsAugmentable, string supplementalTypesVar, string jsonLdPropsVar)
        {
            if (classIsAugmentable)
            {
                scope.If($"this.TryParseSupplementalProperty(model, {supplementalTypesVar}, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, layer, definedIn, parsingErrorCollection, prop.Name, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, prop, {jsonLdPropsVar})")
                    .Line("continue;");
            }
        }

        /// <summary>
        /// Generate code to check for required properties.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddChecksForRequiredProperties(CsScope scope, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                scope.ForEach("Dtmi supplementalTypeId in immediateSupplementalTypeIds")
                    .If("aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo)")
                        .Line($"supplementalTypeInfo.CheckForRequiredProperties(parsingErrorCollection, this.{ParserGeneratorValues.IdentifierName}, elt, supplementalJsonLdProperties);");
            }
        }

        /// <summary>
        /// Generate code to check for required properties.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddChecksForRestrictions(CsScope scope, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                scope
                    .Line("this.CheckForDisallowedCocotypes(this.supplementalTypeIds, this.supplementalTypes, parsingErrorCollection);")
                    .Break()
                    .ForEach("DTSupplementalTypeInfo supplementalType in this.supplementalTypes")
                    .Line($"supplementalType.CheckRestrictions(this.supplementalProperties, parsingErrorCollection, this.{ParserGeneratorValues.IdentifierName}, this.JsonLdElements);");
            }
        }

        /// <summary>
        /// Generate code to add supplemental object properties to a <c>JArray</c> of object properties.
        /// </summary>
        /// <param name="scope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="arrayVariable">Name of a <c>JArray</c> variable to add the property to.</param>
        /// <param name="referencesVariable">Name of a variable to add any object references to.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddSupplementalObjectPropertiesToArray(CsScope scope, string arrayVariable, string referencesVariable, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                scope.ForEach("KeyValuePair<string, object> kvp in this.supplementalProperties")
                    .Line($"this.AddSupplementalObjectPropertyToJArray(kvp.Key, kvp.Value, ref {arrayVariable}, ref {referencesVariable});");
            }
        }

        /// <summary>
        /// Generate code to add supplemental literal properties to a <c>JArray</c> of literal properties.
        /// </summary>
        /// <param name="scope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="arrayVariable">Name of a <c>JArray</c> variable to add the property to.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddSupplementalLiteralPropertiesToArray(CsScope scope, string arrayVariable, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                scope.ForEach("KeyValuePair<string, object> kvp in this.supplementalProperties")
                    .Line($"this.AddSupplementalLiteralPropertyToJArray(kvp.Key, kvp.Value, ref {arrayVariable});");
            }
        }

        /// <summary>
        /// Generate code to try-set supplemental object properties.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="propVar">Name of the variable that holds the name of the property.</param>
        /// <param name="valueVar">Name of the variable that holds the value to set.</param>
        /// <param name="keyVar">Name of the variable that holds the value of a dictionary key.</param>
        /// <param name="layerVar">Name of the variable that holds the name of the layer being parsed.</param>
        /// <param name="propPropVar">Name of the variable that holds the <c>JsonLdProperty</c> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public static void AddTrySetObjectProperties(CsScope scope, string propVar, string valueVar, string keyVar, string layerVar, string propPropVar, bool classIsAugmentable)
        {
            if (classIsAugmentable)
            {
                scope.ForEach("DTSupplementalTypeInfo supplementalType in this.supplementalTypes")
                    .If($"supplementalType.TrySetObjectProperty(this.{ParserGeneratorValues.IdentifierName}, {propVar}, {valueVar}, {keyVar}, {layerVar}, {propPropVar}, ref this.supplementalProperties, this.supplementalSingularPropertyLayers, this.JsonLdElements, parsingErrorCollection)")
                        .Line("return true;");
            }
        }
    }
}
