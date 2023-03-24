namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for adding parsing code to material classes.
    /// </summary>
    public static class MaterialClassParser
    {
        private const string UndefinedTypeStringsPropertyName = "UndefinedTypeStrings";
        private const string UndefinedPropertyDictionaryPropertyName = "UndefinedPropertyDictionary";

        /// <summary>
        /// Generate code for the constructor of the material class.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        public static void GenerateConstructorCode(CsScope scope, bool classIsBase)
        {
            if (classIsBase)
            {
                scope.Break();
                scope.Line($"this.{UndefinedTypeStringsPropertyName} = new HashSet<string>();");
                scope.Line($"this.{UndefinedPropertyDictionaryPropertyName} = new Dictionary<string, JsonElement>();");
                scope.Line($"this.{ParserGeneratorValues.JsonLdElementsPropertyName} = new Dictionary<string, JsonLdElement>();");
                scope.Line("this.SiblingConstraints = null;");
            }
        }

        /// <summary>
        /// Generate appropriate members for the material class.
        /// </summary>
        /// <param name="dtdlVersions">A list of DTDL major version numbers to generate members for.</param>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="baseClassName">The C# name of the DTDL base class.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        /// <param name="kindProperty">The property on the DTDL base obverse class that indicates the kind of DTDL element.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        /// <param name="classIsAbstract">True if the material class is abstract.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        /// <param name="classIsPartition">True if the material class is a partition.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        /// <param name="concreteSubclasses">A map from DTDL version to a list of <see cref="ConcreteSubclass"/> objects.</param>
        /// <param name="specializableSubclasses">A map from DTDL version to a list of <see cref="ConcreteSubclass"/> objects that lack standard elements.</param>
        /// <param name="extensibleMaterialClasses">A map from DTDL version to a list of <see cref="ExtensibleMaterialClass"/> objects.</param>
        /// <param name="extensibleMaterialSubtypes">A map from DTDL version to a list of strings, each representing an extensible material subtype of the material class.</param>
        /// <param name="properties">A list of <see cref="MaterialProperty"/> objects for the properties of the material class.</param>
        /// <param name="dtdlVersionsWithApocryphalPropertyCotypeDependency">A list of DTDL versions for which apocryphal properties are dependent on an apocryphal cotype.</param>
        /// <param name="apocryphaDigests">A dictionary that maps from DTDL version to an <see cref="ApocryphaDigest"/> object that describes the conditions for allowing apocryphal terms and IRIs.</param>
        public static void AddMembers(
            List<int> dtdlVersions,
            CsClass obverseClass,
            string typeName,
            string baseClassName,
            string kindEnum,
            string kindProperty,
            bool classIsBase,
            bool classIsAbstract,
            bool classIsAugmentable,
            bool classIsPartition,
            bool isLayeringSupported,
            Dictionary<int, List<ConcreteSubclass>> concreteSubclasses,
            Dictionary<int, List<ConcreteSubclass>> specializableSubclasses,
            Dictionary<int, List<ExtensibleMaterialClass>> extensibleMaterialClasses,
            Dictionary<int, List<string>> extensibleMaterialSubtypes,
            List<MaterialProperty> properties,
            List<int> dtdlVersionsWithApocryphalPropertyCotypeDependency,
            Dictionary<int, ApocryphaDigest> apocryphaDigests)
        {
            GenerateUndefinedTypeAndPropertiesProperties(obverseClass, classIsBase);
            GenerateJsonLdElementsProperty(obverseClass, classIsBase);
            GenerateSetLayerDefinedWhereMethod(obverseClass, classIsBase, isLayeringSupported);
            GenerateDoesHaveTypeMethod(obverseClass, classIsAugmentable);
            GenerateTryGetChildMethod(obverseClass, baseClassName, classIsBase, classIsAbstract, properties);
            GenerateDoesPropertyDictContainKeyMethod(obverseClass, classIsBase, properties);
            GenerateAddSiblingConstraintMethod(obverseClass, classIsBase, classIsAugmentable, properties);
            GenerateAddConstraintMethod(obverseClass, classIsAugmentable, properties);
            GenerateAddInstancePropertyMethod(obverseClass, classIsAugmentable, properties);
            GenerateParseValueCollectionMethod(obverseClass, typeName, classIsBase, classIsAbstract);
            GenerateTryParseIdStringMethod(obverseClass, typeName, classIsBase, classIsAbstract);
            GenerateTryParseElementMethod(dtdlVersions, obverseClass, typeName, baseClassName, kindProperty, classIsBase, classIsAbstract, isLayeringSupported);
            GenerateTryParseTypeStringsMethod(dtdlVersions, obverseClass, typeName, kindEnum, kindProperty, classIsBase);

            foreach (int dtdlVersion in dtdlVersions)
            {
                GenerateTryParseTypeStringMethod(
                    dtdlVersion,
                    obverseClass,
                    typeName,
                    kindEnum,
                    classIsBase,
                    classIsBase ? concreteSubclasses[dtdlVersion] : specializableSubclasses[dtdlVersion],
                    extensibleMaterialClasses[dtdlVersion],
                    extensibleMaterialSubtypes[dtdlVersion],
                    apocryphaDigests[dtdlVersion],
                    isLayeringSupported);
                GenerateParsePropertiesMethod(
                    dtdlVersion,
                    obverseClass,
                    typeName,
                    classIsBase,
                    classIsAugmentable,
                    classIsPartition,
                    properties,
                    apocryphaDigests[dtdlVersion],
                    apocryphalPropertyNeedsCotype: dtdlVersionsWithApocryphalPropertyCotypeDependency.Contains(dtdlVersion));
            }
        }

        private static void GenerateUndefinedTypeAndPropertiesProperties(CsClass obverseClass, bool classIsBase)
        {
            if (classIsBase)
            {
                CsProperty undefinedTypeStringsProperty = obverseClass.Property(Access.Internal, Novelty.Normal, "HashSet<string>", UndefinedTypeStringsPropertyName);
                undefinedTypeStringsProperty.Summary("Gets or sets a list of undefined type strings applied to the element.");
                undefinedTypeStringsProperty.Get().Set();

                CsProperty undefinedPropertyDictionaryProperty = obverseClass.Property(Access.Internal, Novelty.Normal, "Dictionary<string, JsonElement>", UndefinedPropertyDictionaryPropertyName);
                undefinedPropertyDictionaryProperty.Summary("Gets or sets a dictionary that maps from an undefined property name to a <c>JsonElement</c> representation of the property value.");
                undefinedPropertyDictionaryProperty.Get().Set();

                CsProperty undefinedTypesAccessor = obverseClass.Property(Access.Public, Novelty.Normal, $"IReadOnlyCollection<string>", ParserGeneratorValues.UndefinedTypesPropertyName);
                undefinedTypesAccessor.Summary("Gets a collection of strings, each of which is an undefined type that applies to the DTDL element that corresponds to this object.");
                undefinedTypesAccessor.Value("A collection of undefined type strings.");
                undefinedTypesAccessor.Remarks("An undefined type is any JSON-LD term used as a value of \"@type\" that is not defined in the DTDL language and not defined by any DTDL extension included via the \"@context\" property.");
                undefinedTypesAccessor.Remarks("The DTDL language permits undefined types to be co-typed on any DTDL element regardless of its primary type.");
                undefinedTypesAccessor.Body().Get()
                    .Line($"return this.{UndefinedTypeStringsPropertyName};");

                CsProperty undefinedPropertiesAccessor = obverseClass.Property(Access.Public, Novelty.Normal, "IDictionary<string, JsonElement>", ParserGeneratorValues.UndefinedPropertiesPropertyName);
                undefinedPropertiesAccessor.Summary("Gets any undefined properties of the DTDL element that corresponds to this object.");
                undefinedPropertiesAccessor.Value("A dictionary that maps each string-valued property name to an object that holds the value of the property with the given name.");
                undefinedPropertiesAccessor.Remarks("An undefined property is any JSON-LD term used as a property name that is not defined in the DTDL language and not defined by any DTDL extension included via the \"@context\" property.");
                undefinedTypesAccessor.Remarks("The DTDL language permits undefined propertyes to be added to any DTDL element that has an undefined co-type.");
                undefinedPropertiesAccessor.Body().Get()
                    .Line($"return this.{UndefinedPropertyDictionaryPropertyName};");
            }
        }

        private static void GenerateJsonLdElementsProperty(CsClass obverseClass, bool classIsBase)
        {
            if (classIsBase)
            {
                CsProperty jsonLdElementsProperty = obverseClass.Property(Access.Internal, Novelty.Normal, "Dictionary<string, JsonLdElement>", ParserGeneratorValues.JsonLdElementsPropertyName);
                jsonLdElementsProperty.Summary("Gets a dictionary that maps from layer name to the <see cref=\"JsonLdElement\"/> that defines the layer of the element.");
                jsonLdElementsProperty.Get().Set(Access.Private);
            }
        }

        private static void GenerateSetLayerDefinedWhereMethod(CsClass obverseClass, bool classIsBase, bool isLayeringSupported)
        {
            if (classIsBase && isLayeringSupported)
            {
                string definingParentParamName = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningParentName);
                string definingPartitionParamName = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningPartitionName);

                CsMethod method = obverseClass.Method(Access.Internal, Novelty.Normal, "void", "SetLayerDefinedWhere");
                method.Summary("Add the layer name to the element, and check that the defining parent and partition are consistent with the initialized values.");
                method.Param(ParserGeneratorValues.ObverseTypeString, "layerName", "The name of the layer.");
                method.Param("JsonLdElement", "elt", "The <see cref=\"JsonLdElement\"/> that defines this DTDL element.");
                method.Param(ParserGeneratorValues.IdentifierType, definingParentParamName, "Identifier of the parent element in which this element is defined.");
                method.Param(ParserGeneratorValues.IdentifierType, definingPartitionParamName, "Identifier of the partition in which this element is defined.");
                method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");

                CsIf ifInconsistent = method.Body.If($"!this.{ParserGeneratorValues.IsPartitionPropertyName} && (this.{ParserGeneratorValues.DefiningParentName} != {definingParentParamName} || this.{ParserGeneratorValues.DefiningPartitionName} != {definingPartitionParamName})");
                ifInconsistent
                    .Line("string newLayer = layerName == string.Empty ? \"the base layer\" : $\"layer '{layerName}'\";")
                    .Line($"string extantLayers = string.Join(\" and \", this.{ParserGeneratorValues.LayersPropertyName}.Select(l => l == string.Empty ? \"the base layer\" : $\"layer '{{l}}'\"));");

                CsIf ifInconsistentParent = ifInconsistent.If($"this.{ParserGeneratorValues.DefiningParentName} != {definingParentParamName}");

                ifInconsistentParent
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"inconsistentParent\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("layer: layerName);");

                ifInconsistentParent.Else()
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"inconsistentPartition\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("layer: layerName);");

                ifInconsistent.Else()
                    .Line($"((List<{ParserGeneratorValues.ObverseTypeString}>)this.{ParserGeneratorValues.LayersPropertyName}).Add(layerName);")
                    .Line($"this.{ParserGeneratorValues.JsonLdElementsPropertyName}[layerName] = elt;");
            }
        }

        private static void GenerateDoesHaveTypeMethod(CsClass obverseClass, bool classIsAugmentable)
        {
            CsMethod method = obverseClass.Method(Access.Implicit, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "ITypeChecker.DoesHaveType");
            method.InheritDoc();
            method.Param(ParserGeneratorValues.IdentifierType, "typeId");

            CsMultiLine returnLine = method.Body.MultiLine("return VersionlessTypes.Contains(typeId.Versionless)");

            MaterialClassAugmentor.AddTypeCheckLine(returnLine, classIsAugmentable);

            returnLine.Line(";");
        }

        private static void GenerateTryGetChildMethod(CsClass obverseClass, string baseClassName, bool classIsBase, bool classIsAbstract, List<MaterialProperty> properties)
        {
            if (classIsBase)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Abstract, ParserGeneratorValues.ObverseTypeBoolean, "TryGetChild");
                baseClassMethod.Summary("From the property given by <paramref name=\"childrenPropertyName\"/>, get the element with a property given by <paramref name=\"keyPropertyName\"/> whose value is given by <paramref name=\"keyPropertyValue\"/>.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeString, "childrenPropertyName", "The name of the plural object property that contains the child to get.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeString, "keyPropertyName", "The name of a string property on the child element that uniquely identifies the child.");
                baseClassMethod.Param(ParserGeneratorValues.ObverseTypeString, "keyPropertyValue", "The value of the unique string property on the child element that indicates which child to get.");
                baseClassMethod.Param(baseClassName, "child", "The identified child.", passage: Passage.Out);
                baseClassMethod.Returns("True if the indicated child is found.");
            }
            else if (!classIsAbstract)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, "TryGetChild");
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeString, "childrenPropertyName");
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeString, "keyPropertyName");
                concreteClassMethod.Param(ParserGeneratorValues.ObverseTypeString, "keyPropertyValue");
                concreteClassMethod.Param(baseClassName, "child", passage: Passage.Out);

                CsSwitch switchOnChildrenPropertyName = concreteClassMethod.Body.Switch("childrenPropertyName");

                bool anyCases = false;
                foreach (MaterialProperty materialProperty in properties)
                {
                    anyCases |= materialProperty.TryAddCaseToGetChildSwitch(switchOnChildrenPropertyName, "keyPropertyName", "keyPropertyValue", "child");
                }

                if (!anyCases)
                {
                    switchOnChildrenPropertyName.Default().Line("break;");
                }

                concreteClassMethod.Body.Line("child = null;").Line("return false;");
            }
        }

        private static void GenerateDoesPropertyDictContainKeyMethod(CsClass obverseClass, bool classIsBase, List<MaterialProperty> properties)
        {
            CsMethod method = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Virtual : Novelty.Override, ParserGeneratorValues.ObverseTypeBoolean, "DoesPropertyDictContainKey");
            method.Summary("Indicate whether the property with a given <paramref name=\"propertyName\"/> is a dictionary that holds a given <paramref name=\"key\"/>.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propertyName", "The name of the property to check.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "key", "The key for the dictionary property.");
            method.Returns("True if the property is present, is a dictionary, and holds the given key.");

            if (!classIsBase)
            {
                method.Body.If("base.DoesPropertyDictContainKey(propertyName, key)")
                    .Line("return true;");
            }

            CsSwitch switchOnPropertyName = method.Body.Switch("propertyName");

            foreach (MaterialProperty materialProperty in properties)
            {
                materialProperty.AddCaseToDictionaryKeySwitch(switchOnPropertyName);
            }

            switchOnPropertyName.Default()
                .Line("return false;");
        }

        private static void GenerateAddConstraintMethod(CsClass obverseClass, bool classIsAugmentable, List<MaterialProperty> properties)
        {
            if (classIsAugmentable)
            {
                CsMethod method = obverseClass.Method(Access.Implicit, Novelty.Normal, "void", "IConstrainer.AddPropertyValueConstraint");
                method.InheritDoc();
                method.Param(ParserGeneratorValues.ObverseTypeString, "propertyName");
                method.Param("ValueConstraint", "valueConstraint");

                if (classIsAugmentable)
                {
                    CsSwitch switchOnPropertyName = method.Body.Switch("propertyName");

                    foreach (MaterialProperty materialProperty in properties)
                    {
                        materialProperty.AddCaseForValueConstraintSwitch(switchOnPropertyName, "valueConstraint");
                    }
                }
            }
        }

        private static void GenerateAddSiblingConstraintMethod(CsClass obverseClass, bool classIsBase, bool classIsAugmentable, List<MaterialProperty> properties)
        {
            if (classIsBase)
            {
                obverseClass.Property(Access.Internal, Novelty.Normal, "List<SiblingConstraint>", "SiblingConstraints").Get().Set();
            }

            if (classIsAugmentable)
            {
                CsMethod method = obverseClass.Method(Access.Implicit, Novelty.Normal, "void", "IConstrainer.AddSiblingConstraint");
                method.InheritDoc();
                method.Param("SiblingConstraint", "siblingConstraint");

                method.Body.If("this.SiblingConstraints == null")
                    .Line("this.SiblingConstraints = new List<SiblingConstraint>();");

                method.Body.Line("this.SiblingConstraints.Add(siblingConstraint);");
            }
        }

        private static void GenerateAddInstancePropertyMethod(CsClass obverseClass, bool classIsAugmentable, List<MaterialProperty> properties)
        {
            if (classIsAugmentable && properties.Any(p => p.PropertyKind == PropertyKind.Object))
            {
                CsMethod method = obverseClass.Method(Access.Implicit, Novelty.Normal, "void", "IPropertyInstanceBinder.AddInstanceProperty");
                method.InheritDoc();
                method.Param(ParserGeneratorValues.ObverseTypeString, "propertyName");
                method.Param(ParserGeneratorValues.ObverseTypeString, "instancePropertyName");

                CsSwitch switchOnPropertyName = method.Body.Switch("propertyName");

                foreach (MaterialProperty materialProperty in properties)
                {
                    materialProperty.AddCaseForInstancePropertySwitch(switchOnPropertyName, "instancePropertyName");
                }
            }
        }

        private static void GenerateParseValueCollectionMethod(CsClass obverseClass, string typeName, bool classIsBase, bool classIsAbstract)
        {
            CsMethod method = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Normal : Novelty.New, "void", "ParseValueCollection", Multiplicity.Static);
            method.Summary($"Parse elements encoded in a <see cref=\"JsonLdValueCollection\"/> into objects of {(classIsAbstract ? string.Empty : "subclasses of ")}type <c>{NameFormatter.FormatNameAsClass(typeName)}</c>.");
            method.Param("Model", "model", "The model to add the element to.");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList", "A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints", "List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.");
            method.Param("List<ValueConstraint>", "valueConstraints", "List of <see cref=\"ValueConstraint\"/> to be added to <paramref name=\"elementPropertyConstraints\"/>.");
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Param("JsonLdProperty", "valueCollectionProp", "The <see cref=\"JsonLdProperty\"/> holding the <see cref=\"JsonLdValueCollection\"/> to parse.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "Name of the layer currently being parsed.");
            method.Param(ParserGeneratorValues.IdentifierType, "parentId", "The identifier of the parent of the element.");
            method.Param(ParserGeneratorValues.IdentifierType, "definedIn", "Identifier of the partition or top-level element under which this element is defined.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propName", "The name of the property by which the parent refers to this element, used for auto ID generation.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "dtmiSeg", "A DTMI segment identifier, used for auto ID generation.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "keyProp", "A property used for the key if the parent exposes a collection of these elements as a dictionary.");
            method.Param(ParserGeneratorValues.ObverseTypeInteger, "minCount", "The minimum count of element values required.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "isPlural", "A boolean value indicating whether the property allows multiple values.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "idRequired", "A boolean value indicating whether an @id must be present.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "typeRequired", "A boolean value indicating whether a @type must be present.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "globalize", "Treat all nested definitions as though defined globally.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowReservedIds", "Allow elements to define IDs that have reserved prefixes.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "tolerateSolecisms", "Tolerate specific minor invalidities to support backward compatibility.");
            method.Param("HashSet<int>", "allowedVersions", "A set of allowed versions for the element.");

            method.Body
                .Line("int valueCount = 0;")
                .Break();

            CsForEach forEachValue = method.Body.ForEach("JsonLdValue value in valueCollectionProp.Values.Values");

            CsIf ifMultipleWrong = forEachValue.If("!isPlural && valueCount > 0");

            ifMultipleWrong
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"objectMultipleValues\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("incidentValues: valueCollectionProp.Values,")
                    .Line("layer: layer);");

            ifMultipleWrong.Line("return;");

            CsSwitch switchOnTokenType = forEachValue.Switch("value.ValueType");

            switchOnTokenType.Case("JsonLdValueType.String");

            CsIf ifTryParseIdString = switchOnTokenType
                .If("parentId != null")
                    .If("TryParseIdString(objectPropertyInfoList, elementPropertyConstraints, valueConstraints, aggregateContext, parsingErrorCollection, value.StringValue, layer, parentId, propName, valueCollectionProp, keyProp, allowedVersions)");

            ifTryParseIdString.Line("++valueCount;");

            ifTryParseIdString.Else()
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"badDtmiOrTerm\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("propertyValue: value.StringValue,")
                    .Line("incidentProperty: valueCollectionProp,")
                    .Line("incidentValue: value,")
                    .Line("layer: layer);");

            switchOnTokenType.Line("break;");

            switchOnTokenType.Case("JsonLdValueType.Element");

            switchOnTokenType.If($"TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, valueConstraints, aggregateContext, parsingErrorCollection, value.ElementValue, layer, parentId, definedIn, propName, valueCollectionProp, dtmiSeg, keyProp, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, allowedVersions, \"{typeName}\")")
                .Line("++valueCount;");
            switchOnTokenType.Line("break;");

            switchOnTokenType.Default();

            switchOnTokenType
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"refNotStringOrObject\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("incidentProperty: valueCollectionProp,")
                    .Line("incidentValue: value,")
                    .Line("layer: layer);");

            switchOnTokenType.Line("break;");

            method.Body.If("valueCount < minCount")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"objectCountBelowMin\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("observedCount: valueCount,")
                    .Line("expectedCount: minCount,")
                    .Line("incidentProperty: valueCollectionProp,")
                    .Line("layer: layer);");
        }

        private static void GenerateTryParseIdStringMethod(CsClass obverseClass, string typeName, bool classIsBase, bool classIsAbstract)
        {
            CsMethod method = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Normal : Novelty.New, "bool", "TryParseIdString", Multiplicity.Static);
            method.Summary($"Parse a string property value as an identifier for {(classIsAbstract ? "a subclass of " : string.Empty)}type <c>{NameFormatter.FormatNameAsClass(typeName)}</c>.");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList", "A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints", "List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.");
            method.Param("List<ValueConstraint>", "valueConstraints", "List of <see cref=\"ValueConstraint\"/> to be added to <paramref name=\"elementPropertyConstraints\"/>.");
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "idString", "The identifier string to parse.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "Name of the layer currently being parsed.");
            method.Param(ParserGeneratorValues.IdentifierType, "parentId", "The identifier of the parent of the element.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propName", "The name of the property by which the parent refers to this element, used for auto ID generation.");
            method.Param("JsonLdProperty", "propProp", "The <see cref=\"JsonLdProperty\"/> representing the source of the property by which the parent refers to this element.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "keyProp", "A property used for the key if the parent exposes a collection of these elements as a dictionary.");
            method.Param("HashSet<int>", "allowedVersions", "A set of allowed versions for the element.");
            method.Returns("True if the string parses correctly as an identifier.");

            method.Body.If("!aggregateContext.TryCreateDtmi(idString, out Dtmi elementId)")
                .Line("return false;");

            method.Body.Line("objectPropertyInfoList.Add(new ParsedObjectPropertyInfo() { ElementId = parentId, PropertyName = propName, Layer = layer, JsonLdProperty = propProp, ReferencedIdString = idString, ReferencedElementId = elementId, KeyProperty = keyProp, ExpectedKinds = ConcreteKinds[aggregateContext.DtdlVersion], AllowedVersions = allowedVersions, ChildOf = null, BadTypeCauseFormat = BadTypeCauseFormat[aggregateContext.DtdlVersion], BadTypeLocatedCauseFormat = BadTypeLocatedCauseFormat[aggregateContext.DtdlVersion], BadTypeActionFormat = BadTypeActionFormat[aggregateContext.DtdlVersion] });");
            method.Body.Break();

            method.Body.If("valueConstraints != null && elementPropertyConstraints != null")
                .ForEach("ValueConstraint valueConstraint in valueConstraints")
                    .Line("elementPropertyConstraints.Add(new ElementPropertyConstraint() { ParentId = parentId, PropertyName = propName, ElementId = elementId, ValueConstraint = valueConstraint });");

            method.Body.Line("return true;");
        }

        private static void GenerateTryParseElementMethod(List<int> dtdlVersions, CsClass obverseClass, string typeName, string baseClassName, string kindProperty, bool classIsBase, bool classIsAbstract, bool isLayeringSupported)
        {
            CsMethod method = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Normal : Novelty.New, "bool", "TryParseElement", Multiplicity.Static);
            method.Summary($"Parse an element encoded in a <see cref=\"JsonLdElement\"/> into an object {(classIsAbstract ? string.Empty : "that is a subclass ")}of type <c>{NameFormatter.FormatNameAsClass(typeName)}</c>.");
            method.Param("Model", "model", "The model to add the element to.");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList", "A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints", "List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.");
            method.Param("List<ValueConstraint>", "valueConstraints", "List of <see cref=\"ValueConstraint\"/> to be added to <paramref name=\"elementPropertyConstraints\"/>.");
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Param("JsonLdElement", "elt", "The <see cref=\"JsonLdElement\"/> to parse.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "Name of the layer currently being parsed.");
            method.Param(ParserGeneratorValues.IdentifierType, "parentId", "The identifier of the parent of the element.");
            method.Param(ParserGeneratorValues.IdentifierType, "definedIn", "Identifier of the partition or top-level element under which this element is defined.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propName", "The name of the property by which the parent refers to this element, used for auto ID generation.");
            method.Param("JsonLdProperty", "propProp", "The <see cref=\"JsonLdProperty\"/> representing the source of the property by which the parent refers to this element.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "dtmiSeg", "A DTMI segment identifier, used for auto ID generation.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "keyProp", "A property used for the key if the parent exposes a collection of these elements as a dictionary.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "idRequired", "A boolean value indicating whether an @id must be present.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "typeRequired", "A boolean value indicating whether a @type must be present.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "globalize", "Treat all nested definitions as though defined globally.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowReservedIds", "Allow elements to define IDs that have reserved prefixes.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "tolerateSolecisms", "Tolerate specific minor invalidities to support backward compatibility.");
            method.Param("HashSet<int>", "allowedVersions", "A set of allowed versions for the element.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "inferredType", "The type name to infer if no @type specified on element.");
            method.Returns("True if the <see cref=\"JsonLdElement\"/> parses correctly as an appropriate element.");

            method.Body.Line("AggregateContext childAggregateContext = aggregateContext.GetChildContext(elt, parsingErrorCollection);").Break();

            method.Body.Line("bool allowIdReferenceSyntax = tolerateSolecisms && aggregateContext.DtdlVersion < 3;");
            method.Body.Break();

            CsIf ifIdReference = method.Body.If("elt.PropertyCount == 1 && elt.Id != null");

            CsIf ifAllowed = ifIdReference.If("allowIdReferenceSyntax");

            CsIf ifTryParseIdString = ifAllowed.If("TryParseIdString(objectPropertyInfoList, elementPropertyConstraints, valueConstraints, childAggregateContext, parsingErrorCollection, elt.Id, layer, parentId, propName, propProp, keyProp, allowedVersions)");

            ifTryParseIdString.Line("return true;");

            CsElse elseNotTryParseIdString = ifTryParseIdString.Else();

            elseNotTryParseIdString
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"idRefBadDtmiOrTerm\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("propertyValue: elt.Id,")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            elseNotTryParseIdString.Line("return false;");

            CsElse elseNotAllowed = ifAllowed.Else();

            elseNotAllowed
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"idReference\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("identifier: elt.Id,")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            elseNotAllowed.Line("return false;");

            method.Body.If("elt.Graph != null")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"graphDisallowed\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("element: elt);");

            CsIf ifRestrictKeywords = method.Body.If("childAggregateContext.RestrictKeywords");

            ifRestrictKeywords.If("elt.Language != null")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"keywordDisallowed\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("keyword: \"@language\",")
                    .Line("element: elt);");

            ifRestrictKeywords.If("elt.Value != null")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"keywordDisallowed\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("keyword: \"@value\",")
                    .Line("element: elt);");

            ifRestrictKeywords.ForEach("string keyword in elt.Keywords")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"keywordDisallowed\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("keyword: keyword,")
                    .Line("element: elt);");

            CsIf ifDisallowedVersion = method.Body.If($"allowedVersions != null && !allowedVersions.Contains(childAggregateContext.DtdlVersion)");

            ifDisallowedVersion
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"disallowedVersionDefinition\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("version: childAggregateContext.DtdlVersion.ToString(),")
                    .Line("versionRestriction: string.Join(\" ,\", allowedVersions),")
                    .Line("contextComponent: childAggregateContext.DtdlContextComponent,")
                    .Line("layer: layer);");

            method.Body.Line("bool tolerateReservedIds = tolerateSolecisms && aggregateContext.DtdlVersion < 3;");
            method.Body.Break();

            method.Body.Line("Dtmi elementId = IdValidator.ParseIdProperty(childAggregateContext, elt, childAggregateContext.MergeDefinitions ? layer : null, parentId, propName, dtmiSeg, idRequired, allowReservedIds || tolerateReservedIds, parsingErrorCollection);");
            method.Body.Break();

            method.Body.Line("Dtmi baseElementId = childAggregateContext.MergeDefinitions || elementId.Tail == string.Empty ? elementId.Fragmentless : elementId;");
            method.Body.Line("string elementLayer = childAggregateContext.MergeDefinitions ? elementId.Tail : string.Empty;");
            method.Body.Break();

            method.Body.Line("bool ignoreElementsWithAutoIDsAndDuplicateNames = tolerateSolecisms && aggregateContext.DtdlVersion < 3;");
            method.Body.Break();

            CsIf ifTryGetElement = method.Body.If($"model.Dict.TryGetValue(baseElementId, out {baseClassName} baseElement)");

            if (isLayeringSupported)
            {
                CsIf ifIncompatibleType = ifTryGetElement.If($"!baseElement.{ParserGeneratorValues.LayersPropertyName}.Contains(elementLayer) && !ConcreteKinds[aggregateContext.DtdlVersion].Contains(baseElement.{kindProperty})");

                ifIncompatibleType
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"incompatibleType\",")
                        .Line("elementId: parentId,")
                        .Line("propertyName: propName,")
                        .Line("referenceId: baseElementId,")
                        .Line($"elementType: baseElement.{kindProperty}.ToString(),")
                        .Line("typeRestriction: string.Join(\" or \", ConcreteKinds[aggregateContext.DtdlVersion].Select(k => k.ToString())),")
                        .Line("layer: elementLayer);");
                ifIncompatibleType.Line("return false;");
            }

            CsIf ifContainsLayer = ifTryGetElement.If($"baseElement.{ParserGeneratorValues.JsonLdElementsPropertyName}.TryGetValue(elementLayer, out JsonLdElement duplicateElt)");

            CsIf ifNotReserved = ifContainsLayer.If("!baseElementId.IsReserved");

            ifNotReserved
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"duplicateDefinition\",")
                    .Line("elementId: baseElementId,")
                    .Line("element: elt,")
                    .Line("extantElement: duplicateElt,")
                    .Line("layer: layer);");

            CsElseIf elseIfdtmiSeg = ifNotReserved.ElseIf("!ignoreElementsWithAutoIDsAndDuplicateNames && dtmiSeg != null");

            elseIfdtmiSeg
                .Line("JsonLdProperty dtmiSegProp = elt.Properties.First(p => p.Name == dtmiSeg || (Dtmi.TryCreateDtmi(p.Name, out Dtmi d) && ContextCollection.GetTermOrUri(d) == dtmiSeg));")
                .Line("JsonLdProperty dupSegProp = duplicateElt.Properties.First(p => p.Name == dtmiSeg || (Dtmi.TryCreateDtmi(p.Name, out Dtmi d) && ContextCollection.GetTermOrUri(d) == dtmiSeg));")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"nonUniquePropertyValue\",")
                    .Line("elementId: parentId,")
                    .Line("propertyName: propName,")
                    .Line("nestedName: dtmiSeg,")
                    .Line("nestedValue: dtmiSegProp.Values.Values.First().StringValue,")
                    .Line("incidentProperty: dupSegProp,")
                    .Line("extantProperty: dtmiSegProp);");

            ifContainsLayer.Line("return false;");

            CsIf ifInconsistentContext = ifTryGetElement.If($"baseElement.{ParserGeneratorValues.DtdlVersionPropertyName} != childAggregateContext.DtdlVersion");

            ifInconsistentContext
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"inconsistentContext\",")
                    .Line("elementId: baseElementId,")
                    .Line("version: childAggregateContext.DtdlVersion.ToString(),")
                    .Line($"versionRestriction: baseElement.{ParserGeneratorValues.DtdlVersionPropertyName}.ToString(),")
                    .Line("layer: elementLayer);");
            ifInconsistentContext.Line("return false;");

            CsIf ifMissingRequiredType = method.Body.If("typeRequired && elt.Types == null");

            ifMissingRequiredType
                .Line("string sourceName1 = null;")
                .Line("int sourceLine1 = 0;");

            string getPropLocString = classIsBase ? string.Empty : "propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && ";

            CsIf ifGotSourceLoc = ifMissingRequiredType.If($"{getPropLocString}elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2)");

            ifGotSourceLoc.MultiLine("parsingErrorCollection.Add(")
                .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                .Line("BadTypeLocatedCauseFormat[childAggregateContext.DtdlVersion],")
                .Line("BadTypeActionFormat[childAggregateContext.DtdlVersion],")
                .Line("primaryId: parentId,")
                .Line("property: propName,")
                .Line("secondaryId: baseElementId,")
                .Line("layer: layer,")
                .Line("sourceName1: sourceName1,")
                .Line("startLine1: sourceLine1,")
                .Line("sourceName2: sourceName2,")
                .Line("startLine2: startLine2,")
                .Line("endLine2: endLine2);");

            ifGotSourceLoc.Else().MultiLine("parsingErrorCollection.Add(")
                .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                .Line("BadTypeCauseFormat[childAggregateContext.DtdlVersion],")
                .Line("BadTypeActionFormat[childAggregateContext.DtdlVersion],")
                .Line("primaryId: parentId,")
                .Line("property: propName,")
                .Line("secondaryId: baseElementId,")
                .Line("layer: layer);");

            ifMissingRequiredType.Line("return false;");

            method.Body.Line($"{NameFormatter.FormatNameAsClass(typeName)} elementInfo = ({NameFormatter.FormatNameAsClass(typeName)})baseElement;");
            method.Body.Line("HashSet<Dtmi> immediateSupplementalTypeIds;");
            method.Body.Line("List<string> immediateUndefinedTypes;");
            method.Body.Line("bool typeArrayParsed;");
            method.Body
                .If("elt.Types == null")
                    .Line($"typeArrayParsed = TryParseTypeStrings(new List<string>() {{ inferredType }}, baseElementId, elementLayer, elt, parentId, definedIn, propName, propProp, ref elementInfo, childAggregateContext, out immediateSupplementalTypeIds, out immediateUndefinedTypes, parsingErrorCollection);")
                .Else()
                    .Line("typeArrayParsed = TryParseTypeStrings(elt.Types, baseElementId, elementLayer, elt, parentId, definedIn, propName, propProp, ref elementInfo, childAggregateContext, out immediateSupplementalTypeIds, out immediateUndefinedTypes, parsingErrorCollection);");

            method.Body.If("!typeArrayParsed || ReferenceEquals(null, elementInfo)")
                .Line("return false;");

            method.Body.Break();

            if (dtdlVersions.Any())
            {
                CsSwitch switchOnDtdlVersion = method.Body.Switch("childAggregateContext.DtdlVersion");

                foreach (int dtdlVersion in dtdlVersions)
                {
                    switchOnDtdlVersion.Case(dtdlVersion.ToString());
                    switchOnDtdlVersion.Line($"elementInfo.ParsePropertiesV{dtdlVersion}(model, objectPropertyInfoList, elementPropertyConstraints, childAggregateContext, immediateSupplementalTypeIds, immediateUndefinedTypes, parsingErrorCollection, elt, elementLayer, definedIn, globalize, allowReservedIds, tolerateSolecisms);");
                    switchOnDtdlVersion.Line("break;");
                }
            }

            method.Body.Line("elementInfo.RecordSourceAsAppropriate(elementLayer, elt, childAggregateContext, parsingErrorCollection, atRoot: parentId == null, globalized: globalize);");
            method.Body.Break();

            method.Body.Line("model.Dict[baseElementId] = elementInfo;");
            method.Body.Break();

            CsIf ifParentIdNotNull = method.Body.If("parentId != null");
            ifParentIdNotNull.Line("objectPropertyInfoList.Add(new ParsedObjectPropertyInfo() { ElementId = parentId, PropertyName = propName, Layer = layer, JsonLdProperty = propProp, ReferencedElementId = baseElementId, KeyProperty = keyProp, ExpectedKinds = null, AllowedVersions = null, ChildOf = null, BadTypeCauseFormat = null, BadTypeLocatedCauseFormat = null, BadTypeActionFormat = null });");
            ifParentIdNotNull.Break();
            ifParentIdNotNull.If("valueConstraints != null && elementPropertyConstraints != null")
                .ForEach("ValueConstraint valueConstraint in valueConstraints")
                .Line("elementPropertyConstraints.Add(new ElementPropertyConstraint() { ParentId = parentId, PropertyName = propName, ElementId = baseElementId, ValueConstraint = valueConstraint });");

            method.Body.Line("return true;");
        }

        private static void GenerateTryParseTypeStringsMethod(List<int> dtdlVersions, CsClass obverseClass, string typeName, string kindEnum, string kindProperty, bool classIsBase)
        {
            CsMethod method = obverseClass.Method(Access.Internal, Novelty.Normal, "bool", "TryParseTypeStrings", Multiplicity.Static);
            method.Summary("Parse a list of @type values containing material or supplemental types.");
            method.Param("List<string>", "typeStrings", "The list of strings to parse.");
            method.Param(ParserGeneratorValues.IdentifierType, "elementId", "The identifier of the element of this type to create.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "Name of the layer currently being parsed.");
            method.Param("JsonLdElement", "elt", "The <see cref=\"JsonLdElement\"/> currently being parsed.");
            method.Param(ParserGeneratorValues.IdentifierType, "parentId", "The identifier of the parent of the element.");
            method.Param(ParserGeneratorValues.IdentifierType, "definedIn", "Identifier of the partition or top-level element under which this element is defined.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propName", "The name of the property by which the parent refers to this element, used for auto ID generation.");
            method.Param("JsonLdProperty", "propProp", "The <see cref=\"JsonLdProperty\"/> representing the source of the property by which the parent refers to this element.");
            method.Param(NameFormatter.FormatNameAsClass(typeName), "elementInfo", $"The <see cref=\"{NameFormatter.FormatNameAsClass(typeName)}\"/> created.", passage: Passage.Reference);
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("HashSet<Dtmi>", "immediateSupplementalTypeIds", "A set of supplemental type IDs from the type array.", passage: Passage.Out);
            method.Param("List<string>", "immediateUndefinedTypes", "A list of undefind type strings from the type array.", passage: Passage.Out);
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Returns("True if no errors detected in type array.");

            method.Body.Line("immediateSupplementalTypeIds = new HashSet<Dtmi>();");
            method.Body.Line("immediateUndefinedTypes = new List<string>();");
            method.Body.Break();

            method.Body.Line($"HashSet<{kindEnum}> materialKinds = new HashSet<{kindEnum}>();");
            method.Body.Break();

            method.Body.Line("bool anyFailures = false;");

            CsForEach forEachTypeString = method.Body.ForEach("string typeString in typeStrings");

            if (dtdlVersions.Any())
            {
                CsSwitch switchOnDtdlVersion = forEachTypeString.Switch("aggregateContext.DtdlVersion");

                foreach (int dtdlVersion in dtdlVersions)
                {
                    switchOnDtdlVersion.Case(dtdlVersion.ToString());
                    switchOnDtdlVersion.If($"!TryParseTypeStringV{dtdlVersion}(typeString, elementId, layer, elt, parentId, definedIn, propName, propProp, materialKinds, immediateSupplementalTypeIds, ref elementInfo, ref immediateUndefinedTypes, aggregateContext, parsingErrorCollection)")
                        .Line("anyFailures = true;");
                    switchOnDtdlVersion.Line("break;");
                }
            }

            method.Body.If("anyFailures")
                .Line("return false;");

            CsIf ifInfoIsNull = method.Body.If("ReferenceEquals(null, elementInfo)");

            ifInfoIsNull
                .Line("string sourceName1 = null;")
                .Line("int sourceLine1 = 0;");

            string getPropLocString = classIsBase ? string.Empty : "propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && ";

            CsIf ifGotSourceLoc = ifInfoIsNull.If($"{getPropLocString}elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2)");

            ifGotSourceLoc
                .Line("elt.TryGetSourceLocationForType(out _, out int sourceLine3);")
                .MultiLine("parsingErrorCollection.Add(")
                    .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                    .Line("BadTypeLocatedCauseFormat[aggregateContext.DtdlVersion],")
                    .Line("BadTypeActionFormat[aggregateContext.DtdlVersion],")
                    .Line("primaryId: parentId,")
                    .Line("property: propName,")
                    .Line("secondaryId: elementId,")
                    .Line("layer: layer,")
                    .Line("sourceName1: sourceName1,")
                    .Line("startLine1: sourceLine1,")
                    .Line("sourceName2: sourceName2,")
                    .Line("startLine2: startLine2,")
                    .Line("endLine2: endLine2,")
                    .Line("startLine3: sourceLine3);");

            ifGotSourceLoc.Else().MultiLine("parsingErrorCollection.Add(")
                .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                .Line("BadTypeCauseFormat[aggregateContext.DtdlVersion],")
                .Line("BadTypeActionFormat[aggregateContext.DtdlVersion],")
                .Line("primaryId: parentId,")
                .Line("property: propName,")
                .Line("secondaryId: elementId,")
                .Line("layer: layer);");

            ifInfoIsNull.Line("return false;");

            CsIf ifNoMaterialKinds = method.Body.If("!materialKinds.Any()");

            ifNoMaterialKinds
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"layerMissingMaterialType\",")
                    .Line("elementId: elementId,")
                    .Line($"elementType: elementInfo.{kindProperty}.ToString(),")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            ifNoMaterialKinds.Line("return false;");

            method.Body.Line($"materialKinds.Add(elementInfo.{kindProperty});");

            CsIf ifMultipleMaterialKinds = method.Body.If("materialKinds.Count() > 1");

            ifMultipleMaterialKinds
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"multipleMaterialTypes\",")
                    .Line("elementId: elementId,")
                    .Line("valueConjunction: string.Join(\" and \", materialKinds),")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            ifMultipleMaterialKinds.Line("return false;");

            method.Body.Line($"elementInfo.{UndefinedTypeStringsPropertyName}.UnionWith(immediateUndefinedTypes);");
            method.Body.Break();

            method.Body.Line("bool requiresMergeability = layer != string.Empty && !elementId.IsReserved;");
            method.Body.Line("bool isMergeable = false;");

            CsForEach forEachSupplementalType = method.Body.ForEach("Dtmi supplementalTypeId in immediateSupplementalTypeIds");

            CsIf ifTryGetSupplementalTypeInfo = forEachSupplementalType.If("aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo)");

            CsIf ifKindNotAllowed = ifTryGetSupplementalTypeInfo.If($"!supplementalTypeInfo.AllowedCotypeKinds.Contains(elementInfo.{kindProperty})");

            ifKindNotAllowed
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"invalidCotype\",")
                    .Line("elementId: elementId,")
                    .Line("cotype: ContextCollection.GetTermOrUri(supplementalTypeId),")
                    .Line("typeRestriction: string.Join(\" or \", supplementalTypeInfo.AllowedCotypeKinds),")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            ifKindNotAllowed.ElseIf($"!supplementalTypeInfo.AllowedCotypeVersions.Contains(elementInfo.{ParserGeneratorValues.DtdlVersionPropertyName})")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"invalidCotypeVersion\",")
                    .Line("elementId: elementId,")
                    .Line("cotype: ContextCollection.GetTermOrUri(supplementalTypeId),")
                    .Line("versionRestriction: string.Join(\" or \", supplementalTypeInfo.AllowedCotypeVersions),")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            CsElse elseKindAllowed = ifKindNotAllowed.Else();

            elseKindAllowed.Line("elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);");

            elseKindAllowed
                .ForEach("Dtmi requiredCocotype in supplementalTypeInfo.RequiredCocotypes")
                    .If("!immediateSupplementalTypeIds.Contains(requiredCocotype)")
                        .Line("string requiredCocotypeTerm = ContextCollection.GetTermOrUri(requiredCocotype);")
                        .MultiLine("parsingErrorCollection.Notify(")
                            .Line("\"missingCocotype\",")
                            .Line("elementId: elementId,")
                            .Line("elementType: ContextCollection.GetTermOrUri(supplementalTypeId),")
                            .Line("cotype: requiredCocotypeTerm,")
                            .Line("element: elt);");

            ifTryGetSupplementalTypeInfo.Line("isMergeable = isMergeable || supplementalTypeInfo.IsMergeable;");

            method.Body.If("requiresMergeability && !isMergeable")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"disallowedIdFragment\",")
                    .Line("elementId: elementId,")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            method.Body.Line("return true;");
        }

        private static void GenerateTryParseTypeStringMethod(int dtdlVersion, CsClass obverseClass, string typeName, string kindEnum, bool classIsBase, List<ConcreteSubclass> concreteSubclasses, List<ExtensibleMaterialClass> extensibleMaterialClasses, List<string> extensibleMaterialSubtypes, ApocryphaDigest apocryphaDigest, bool isLayeringSupported)
        {
            CsMethod method = obverseClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, $"TryParseTypeStringV{dtdlVersion}", Multiplicity.Static);
            method.Summary("Parse a string @type value, whether material or supplemental.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "typeString", "The string value to parse.");
            method.Param(ParserGeneratorValues.IdentifierType, "elementId", "The identifier of the element of this type to create.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "Name of the layer currently being parsed.");
            method.Param("JsonLdElement", "elt", "The <see cref=\"JsonLdElement\"/> currently being parsed.");
            method.Param(ParserGeneratorValues.IdentifierType, "parentId", "The identifier of the parent of the element.");
            method.Param(ParserGeneratorValues.IdentifierType, "definedIn", "Identifier of the partition or top-level element under which this element is defined.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propName", "The name of the property by which the parent refers to this element.");
            method.Param("JsonLdProperty", "propProp", "The <see cref=\"JsonLdProperty\"/> representing the source of the property by which the parent refers to this element.");
            method.Param($"HashSet<{kindEnum}>", "materialKinds", "A set of material kinds to update with the material kind of the type, if any.");
            method.Param("HashSet<Dtmi>", "supplementalTypeIds", "A set of supplemental type IDs to update with the supplemental type, if any.");
            method.Param(NameFormatter.FormatNameAsClass(typeName), "elementInfo", "The element created if the type is material.", passage: Passage.Reference);
            method.Param("List<string>", "undefinedTypes", "A list of string values of any undefined supplemental types.", passage: Passage.Reference);
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Returns("True if the parse is sucessful.");

            if (concreteSubclasses.Any())
            {
                CsSwitch switchOnTypeString = method.Body.Switch("typeString");

                foreach (ConcreteSubclass concreteSubclass in concreteSubclasses)
                {
                    concreteSubclass.AddCaseToParseTypeStringSwitch(switchOnTypeString, "elementInfo", "elementId", "layer", "elt", "parentId", "propName", "definedIn", "parsingErrorCollection", isLayeringSupported);
                }
            }

            string getPropLocString = classIsBase ? string.Empty : "propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && ";

            CsIf ifMaterialType = method.Body.If("MaterialTypeNameCollection.IsMaterialType(typeString)");

            ifMaterialType
                .Line("string sourceName1 = null;")
                .Line("int sourceLine1 = 0;");

            CsIf ifGotSourceLocMaterial = ifMaterialType.If($"{getPropLocString}elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2)");

            ifGotSourceLocMaterial
                .Line("elt.TryGetSourceLocationForType(out _, out int sourceLine3);")
                .MultiLine("parsingErrorCollection.Add(")
                    .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                    .Line($"BadTypeLocatedCauseFormat[{dtdlVersion}],")
                    .Line($"BadTypeActionFormat[{dtdlVersion}],")
                    .Line("primaryId: parentId,")
                    .Line("property: propName,")
                    .Line("secondaryId: elementId,")
                    .Line("value: typeString,")
                    .Line("layer: layer,")
                    .Line("sourceName1: sourceName1,")
                    .Line("startLine1: sourceLine1,")
                    .Line("sourceName2: sourceName2,")
                    .Line("startLine2: startLine2,")
                    .Line("endLine2: endLine2,")
                    .Line("startLine3: sourceLine3);");

            ifGotSourceLocMaterial.Else().MultiLine("parsingErrorCollection.Add(")
                .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                .Line($"BadTypeCauseFormat[{dtdlVersion}],")
                .Line($"BadTypeActionFormat[{dtdlVersion}],")
                .Line("primaryId: parentId,")
                .Line("property: propName,")
                .Line("secondaryId: elementId,")
                .Line("value: typeString,")
                .Line("layer: layer);");

            ifMaterialType.Line("return false;");

            CsIf ifNotDtmi = method.Body.If("!aggregateContext.TryCreateDtmi(typeString, out Dtmi supplementalTypeId)");

            ConditionalAlternativeSeries apocryphaSeries = new ConditionalAlternativeSeries(ifNotDtmi);

            if (apocryphaSeries.TryGetScope(apocryphaDigest.InvalidDtmiAsCotype != ApocryphaTolerance.Valid, "typeString.StartsWith(\"dtmi:\")", "Invalid DTMI always valid as cotype", out CsScope dtmiPrefixScope))
            {
                CsScope invalidDtmiScope = apocryphaDigest.InvalidDtmiAsCotype == ApocryphaTolerance.Tenable ? dtmiPrefixScope.If("aggregateContext.IsComplete") : dtmiPrefixScope;
                invalidDtmiScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"typeInvalidDtmi\",")
                        .Line("elementId: elementId,")
                        .Line("cotype: typeString,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                invalidDtmiScope.Line("return false;");
            }

            if (apocryphaSeries.TryGetScope(apocryphaDigest.NotDtmiNorTermAsCotype != ApocryphaTolerance.Valid, "typeString.Contains(\":\")", "Non-DTMI IRI always valid as cotype", out CsScope isIriScope))
            {
                CsScope notDtmiNorTermScope = apocryphaDigest.NotDtmiNorTermAsCotype == ApocryphaTolerance.Tenable ? isIriScope.If("aggregateContext.IsComplete") : isIriScope;
                notDtmiNorTermScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"typeNotDtmiNorTerm\",")
                        .Line("elementId: elementId,")
                        .Line("cotype: typeString,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                notDtmiNorTermScope.Line("return false;");
            }

            if (apocryphaSeries.TryGetScope(apocryphaDigest.UndefinedTermAsCotype != ApocryphaTolerance.Valid, null, "Undefined term always valid as cotype", out CsScope isTermScope))
            {
                CsScope undefinedTermScope = apocryphaDigest.UndefinedTermAsCotype == ApocryphaTolerance.Tenable ? isTermScope.If("aggregateContext.IsComplete") : isTermScope;
                undefinedTermScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"typeUndefinedTerm\",")
                        .Line("elementId: elementId,")
                        .Line("cotype: typeString,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                undefinedTermScope.Line("return false;");
            }

            ifNotDtmi
                .Line("undefinedTypes.Add(typeString);")
                .Line("return true;");

            CsIf ifInvalidSupplementalType = method.Body.If("!aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo)");

            if (apocryphaDigest.IrrelevantDtmiOrTermAsCotype != ApocryphaTolerance.Valid)
            {
                CsScope irrelevantDtmiOrTermScope = apocryphaDigest.IrrelevantDtmiOrTermAsCotype == ApocryphaTolerance.Tenable ? ifInvalidSupplementalType.If("aggregateContext.IsComplete") : ifInvalidSupplementalType;
                irrelevantDtmiOrTermScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"typeIrrelevantDtmiOrTerm\",")
                        .Line("elementId: elementId,")
                        .Line("cotype: typeString,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                irrelevantDtmiOrTermScope.Line("return false;");
            }
            else
            {
                ifInvalidSupplementalType.Line("/* Irrelevant DTMI or term always valid as cotype */");
                ifInvalidSupplementalType.Break();
            }

            if (apocryphaDigest.IrrelevantDtmiOrTermAsCotype != ApocryphaTolerance.Invalid)
            {
                ifInvalidSupplementalType
                    .Line("undefinedTypes.Add(typeString);")
                    .Line("return true;");
            }

            CsIf ifAbstractSupplementalType = method.Body.If("supplementalTypeInfo.IsAbstract");

            ifAbstractSupplementalType
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"abstractSupplementalType\",")
                    .Line("elementId: elementId,")
                    .Line("cotype: ContextCollection.GetTermOrUri(supplementalTypeId),")
                    .Line("element: elt,")
                    .Line("layer: layer);");

            ifAbstractSupplementalType.Line("return false;");

            if (extensibleMaterialClasses.Any())
            {
                CsSwitch switchOnExtensionKind = method.Body.Switch("supplementalTypeInfo.ExtensionKind");

                foreach (ExtensibleMaterialClass extensibleMaterialClass in extensibleMaterialClasses)
                {
                    extensibleMaterialClass.AddCaseToParseTypeStringSwitch(switchOnExtensionKind, extensibleMaterialSubtypes, "elementInfo", "propName", "propProp", "elementId", "typeString", "layer", "elt", "parentId", "propName", "definedIn", "supplementalTypeId", "supplementalTypeInfo", "parsingErrorCollection", isLayeringSupported);
                }
            }

            method.Body.Line("supplementalTypeIds.Add(supplementalTypeId);");
            method.Body.Break();
            method.Body.Line("return true;");
        }

        private static void GenerateParsePropertiesMethod(int dtdlVersion, CsClass obverseClass, string typeName, bool classIsBase, bool classIsAugmentable, bool classIsPartition, List<MaterialProperty> properties, ApocryphaDigest apocryphaDigest, bool apocryphalPropertyNeedsCotype)
        {
            CsMethod method = obverseClass.Method(Access.Internal, classIsBase ? Novelty.Virtual : Novelty.Override, "void", $"ParsePropertiesV{dtdlVersion}");
            method.Summary($"Parse the properties in a JSON {typeName} object.");
            method.Param("Model", "model", "Model to which to add object properties.");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList", "List of object info structs for deferred assignments.");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints", "List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.");
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("HashSet<Dtmi>", "immediateSupplementalTypeIds", "A set of supplemental type IDs.");
            method.Param("List<string>", "immediateUndefinedTypes", "A list of undefind type strings.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Param("JsonLdElement", "elt", "The <see cref=\"JsonLdElement\"/> to parse.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "Name of the layer currently being parsed.");
            method.Param(ParserGeneratorValues.IdentifierType, "definedIn", "Identifier of the partition or top-level element under which this element is defined.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "globalize", "Treat all nested definitions as though defined globally.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowReservedIds", "Allow elements to define IDs that have reserved prefixes.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "tolerateSolecisms", "Tolerate specific minor invalidities to support backward compatibility.");

            foreach (MaterialProperty materialProperty in properties)
            {
                materialProperty.SetValue(dtdlVersion, method.Body, "this");
            }

            method.Body.Break();

            foreach (MaterialProperty materialProperty in properties)
            {
                materialProperty.AddPreludeToParseSwitch(dtdlVersion, method.Body);
            }

            method.Body.Line("Dictionary<string, JsonLdProperty> supplementalJsonLdProperties = new Dictionary<string, JsonLdProperty>();");
            method.Body.Break();

            CsForEach forEachProp = method.Body.ForEach("JsonLdProperty prop in elt.Properties");

            if (properties.Any(p => p.IsParseable(dtdlVersion)))
            {
                CsSwitch switchOnKey = forEachProp.Switch("prop.Name");

                foreach (MaterialProperty materialProperty in properties)
                {
                    materialProperty.AddCaseToParseSwitch(dtdlVersion, switchOnKey, "prop", classIsAugmentable, classIsPartition, "layer", "valueCount", "definedIn", "aggregateContext");
                }
            }

            MaterialClassAugmentor.AddTryParseSupplementalProperty(forEachProp, classIsAugmentable, "immediateSupplementalTypeIds", "supplementalJsonLdProperties");

            ConditionalAlternativeSeries apocryphaSeries = new ConditionalAlternativeSeries(forEachProp);

            if (apocryphaSeries.TryGetScope(apocryphaDigest.IrrelevantDtmiOrTermAsProperty != ApocryphaTolerance.Valid, "aggregateContext.TryCreateDtmi(prop.Name, out Dtmi _)", "Irrelevant DTMI or term always valid as property", out CsScope validDtmiScope))
            {
                CsScope irrelevantDtmiOrTermScope = apocryphaDigest.IrrelevantDtmiOrTermAsProperty == ApocryphaTolerance.Tenable ? validDtmiScope.If("aggregateContext.IsComplete") : validDtmiScope;
                irrelevantDtmiOrTermScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"propertyIrrelevantDtmiOrTerm\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("propertyName: prop.Name,")
                        .Line("incidentProperty: prop,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                irrelevantDtmiOrTermScope.Line("continue;");
            }

            if (apocryphaSeries.TryGetScope(apocryphaDigest.InvalidDtmiAsProperty != ApocryphaTolerance.Valid, "prop.Name.StartsWith(\"dtmi:\")", "Invalid DTMI always valid as property", out CsScope dtmiPrefixScope))
            {
                CsScope invalidDtmiScope = apocryphaDigest.InvalidDtmiAsProperty == ApocryphaTolerance.Tenable ? dtmiPrefixScope.If("aggregateContext.IsComplete") : dtmiPrefixScope;
                invalidDtmiScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"propertyInvalidDtmi\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("propertyName: prop.Name,")
                        .Line("incidentProperty: prop,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                invalidDtmiScope.Line("continue;");
            }

            if (apocryphaSeries.TryGetScope(apocryphaDigest.NotDtmiNorTermAsProperty != ApocryphaTolerance.Valid, "prop.Name.Contains(\":\")", "Non-DTMI IRI always valid as property", out CsScope isIriScope))
            {
                CsScope notDtmiNorTermScope = apocryphaDigest.NotDtmiNorTermAsProperty == ApocryphaTolerance.Tenable ? isIriScope.If("aggregateContext.IsComplete") : isIriScope;
                notDtmiNorTermScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"propertyNotDtmiNorTerm\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("propertyName: prop.Name,")
                        .Line("incidentProperty: prop,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                notDtmiNorTermScope.Line("continue;");
            }

            if (apocryphaSeries.TryGetScope(apocryphaDigest.UndefinedTermAsProperty != ApocryphaTolerance.Valid, null, "Undefined term always valid as property", out CsScope isTermScope))
            {
                CsScope undefinedTermScope = apocryphaDigest.UndefinedTermAsProperty == ApocryphaTolerance.Tenable ? isTermScope.If("aggregateContext.IsComplete") : isTermScope;
                undefinedTermScope
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"propertyUndefinedTerm\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("propertyName: prop.Name,")
                        .Line("incidentProperty: prop,")
                        .Line("element: elt,")
                        .Line("layer: layer);");
                undefinedTermScope.Line("continue;");
            }

            if (apocryphalPropertyNeedsCotype)
            {
                CsIf ifNoUndefinedType = forEachProp.If($"!immediateUndefinedTypes.Any()");
                CsIf ifExplicitTypes = ifNoUndefinedType.If("elt.Types != null");

                ifExplicitTypes
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"noTypeThatAllows\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("propertyName: prop.Name,")
                        .Line("incidentProperty: prop,")
                        .Line("element: elt,")
                        .Line("layer: layer);");

                ifExplicitTypes.Else()
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"inferredTypeDoesNotAllow\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"referenceType: \"{typeName}\",")
                        .Line("propertyName: prop.Name,")
                        .Line("incidentProperty: prop,")
                        .Line("element: elt,")
                        .Line("layer: layer);");

                ifNoUndefinedType.Line("continue;");
            }

            forEachProp.Using("JsonDocument propDoc = JsonDocument.Parse(prop.Values.GetJsonText())")
                .Line($"this.{UndefinedPropertyDictionaryPropertyName}[prop.Name] = propDoc.RootElement.Clone();");

            method.Body.Break();

            foreach (MaterialProperty materialProperty in properties)
            {
                materialProperty.AddCheckForRequiredProperty(dtdlVersion, method.Body, "elt");
            }

            MaterialClassAugmentor.AddChecksForRequiredProperties(method.Body, classIsAugmentable);
        }
    }
}
