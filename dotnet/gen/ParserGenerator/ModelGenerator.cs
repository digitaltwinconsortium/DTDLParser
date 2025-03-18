namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Code generator for <c>Model</c> class.
    /// </summary>
    public class ModelGenerator : ITypeGenerator
    {
        private readonly string baseClassName;
        private readonly string baseEnumName;
        private readonly string baseEnumPropertyName;
        private readonly HashSet<string> reservedIdPrefixes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        /// <param name="reservedIdPrefixes">A dicictionary that maps from context ID to a list of identifier prefixes that are reserved for this context.</param>
        public ModelGenerator(string baseName, Dictionary<string, List<string>> reservedIdPrefixes)
        {
            this.baseClassName = NameFormatter.FormatNameAsClass(baseName);
            this.baseEnumName = NameFormatter.FormatNameAsEnum(baseName);
            this.baseEnumPropertyName = NameFormatter.FormatNameAsEnumProperty(baseName);

            this.reservedIdPrefixes = new HashSet<string>();
            foreach (KeyValuePair<string, List<string>> kvp in reservedIdPrefixes)
            {
                foreach (string reservedIdPrefix in kvp.Value)
                {
                    this.reservedIdPrefixes.Add(reservedIdPrefix);
                }
            }
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass modelClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "Model", completeness: Completeness.Partial);
            modelClass.Summary("A DTDL model.");

            this.GenerateConstructor(modelClass);
            this.GenerateDictProperty(modelClass);
            this.GenerateIsPartitionMethod(modelClass);
            this.GenerateTrySetObjectPropertyMethod(modelClass);
            this.GenerateIsKindInSetMethod(modelClass);
            this.GenerateGetKindStringMethod(modelClass);
            this.GenerateCheckValueConstraintOnSiblingMethod(modelClass);
            this.GenerateCheckParentConstraintsMethod(modelClass);
            this.GenerateCheckSupplementalPropertyConstraintsMethod(modelClass);
            this.GenerateCheckSiblingConstraintsMethod(modelClass);
        }

        private void GenerateConstructor(CsClass modelClass)
        {
            CsConstructor constructor = modelClass.Constructor(Access.Internal);
            constructor.Body.Line($"this.Dict = new Dictionary<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}>(new DtmiEqualityComparer());");
        }

        private void GenerateDictProperty(CsClass modelClass)
        {
            CsProperty property = modelClass.Property(Access.Internal, Novelty.Normal, $"Dictionary<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}>", "Dict");
            property.Summary("Gets a dictionary that maps DTMIs to their obverse objects.");
            property.Get();
        }

        private void GenerateIsPartitionMethod(CsClass modelClass)
        {
            CsMethod method = modelClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "IsPartition");
            method.Summary("Is the element in the dictionary a partition.");
            method.Param(ParserGeneratorValues.IdentifierType, "elementId", "The ID of the element.");
            method.Returns("True if the element is a partition.");
            method.Body.Line($"return this.Dict[elementId].{ParserGeneratorValues.IsPartitionPropertyName};");
        }

        private void GenerateTrySetObjectPropertyMethod(CsClass modelClass)
        {
            string referenceClassName = NameFormatter.FormatNameAsClass(ParserGeneratorValues.ReferenceObverseName);

            CsMethod method = modelClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "TrySetObjectProperty");
            method.Summary("Try to set an object property with a given <paramref name=\"propertyName\"/>.");
            method.Param(ParserGeneratorValues.IdentifierType, "elementId", "Identifier of the element.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "Name of the variable that holds the layer of the property.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propertyName", "The name of the property whose value to set if the property is recognized.");
            method.Param("JsonLdProperty", "propProp", "The <see cref=\"JsonLdProperty\"/> representing the source of the property by which the parent refers to this element.");
            method.Param(ParserGeneratorValues.IdentifierType, "referencedElementId", "The element ID of the object to set as the value.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "keyProp", "The key property for dictionary properties.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "keyValue", "The key value for dictionary properties.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Returns("True if the property name is recognized.");

            method.Body.Line($"{this.baseClassName} element = this.Dict.ContainsKey(referencedElementId) ? this.Dict[referencedElementId] : new {referenceClassName}(0, referencedElementId, null, null, null);");

            method.Body.If(string.Join(" && ", this.reservedIdPrefixes.Select(p => $"!referencedElementId.AbsoluteUri.StartsWith(\"{p}\")")))
                .Line($"element.{ParserGeneratorValues.ParentReferencesName}.Add(new ParentReference() {{ ParentId = elementId, PropertyName = propertyName }});");

            method.Body.Line("return this.Dict[elementId].TrySetObjectProperty(propertyName, layer, propProp, element, keyProp, keyValue, parsingErrorCollection);");
        }

        private void GenerateIsKindInSetMethod(CsClass modelClass)
        {
            CsMethod method = modelClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "IsKindInSet");
            method.Summary($"Determines whether the <c>{this.baseEnumPropertyName}</c> of the element is in the given <paramref name=\"kindSet\"/>.");
            method.Param(ParserGeneratorValues.IdentifierType, "elementId", "Identifier of the element.");
            method.Param($"HashSet<{this.baseEnumName}>", "kindSet", $"Set of <c>{this.baseEnumName}</c>.");
            method.Returns("True if the element's kind is in the set.");
            method.Body.Line($"return kindSet.Contains(this.Dict[elementId].{this.baseEnumPropertyName});");
        }

        private void GenerateGetKindStringMethod(CsClass modelClass)
        {
            CsMethod method = modelClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeString, "GetKindString");
            method.Summary($"Gets a string representing the <c>{this.baseEnumPropertyName}</c> of an element.");
            method.Param(ParserGeneratorValues.IdentifierType, "elementId", "Identifier of the element.");
            method.Returns($"String representation of the element's <c>{this.baseEnumPropertyName}</c>.");
            method.Body.Line($"return this.Dict[elementId].{this.baseEnumPropertyName}.ToString();");
        }

        private void GenerateCheckValueConstraintOnSiblingMethod(CsClass parserClass)
        {
            CsMethod method = parserClass.Method(Access.Private, Novelty.Normal, "void", "CheckValueConstraintOnSibling");
            method.Param("ElementPropertyConstraint", "elementPropertyConstraint");
            method.Param("SupplementalTypeCollection", "supplementalTypeCollection");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");

            method.Body.Line($"{this.baseClassName} currentSibling = this.Dict[elementPropertyConstraint.ParentId];");

            CsForEach forEachParentRef = method.Body.ForEach($"ParentReference parentReference in currentSibling.{ParserGeneratorValues.ParentReferencesName}");

            this.GenerateTryGetTargetSibling(forEachParentRef, "elementPropertyConstraint.ValueConstraint.SiblingKeyPropertyName", "elementPropertyConstraint.ValueConstraint.SiblingKeyrefPropertyId", "elementPropertyConstraint.ParentId");

            this.GenerateCheckForMismatch(forEachParentRef, "SiblingClassOfPropertyId", "Type", "classOfPropertyId");

            this.GenerateCheckForMismatch(forEachParentRef, "SiblingParentOfPropertyId", "ChildOf", "parentOfPropertyId");
        }

        private void GenerateCheckParentConstraintsMethod(CsClass parserClass)
        {
            CsMethod method = parserClass.Method(Access.Private, Novelty.Normal, "void", "CheckParentConstraints");
            method.Param(this.baseClassName, "currentSibling");
            method.Param("SupplementalTypeCollection", "supplementalTypeCollection");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");

            CsIf ifFoundParent = method.Body.ForEach($"{ParserGeneratorValues.IdentifierType} supplementalTypeId in currentSibling.SupplementalTypes")
                .If("supplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo)")
                    .ForEach("ParentConstraint parentConstraint in supplementalTypeInfo.ParentConstraints")
                        .ForEach("ParentReference parentReference in currentSibling.ParentReferences")
                            .If("parentReference.PropertyName == parentConstraint.ParentPropertyName");

            ifFoundParent
                .Line($"{this.baseClassName} commonParent = this.Dict[parentReference.ParentId];")
                .If("parentConstraint.RequiredParentCotype != null && !commonParent.SupplementalTypes.Contains(parentConstraint.RequiredParentCotype)")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"parentMissingCotype\",")
                        .Line("elementId: currentSibling.Id,")
                        .Line("propertyName: parentReference.PropertyName,")
                        .Line("elementType: ContextCollection.GetTermOrUri(supplementalTypeInfo.Type),")
                        .Line("referenceType: ContextCollection.GetTermOrUri(parentConstraint.RequiredParentCotype));");

            ifFoundParent
                .If("parentConstraint.AdjunctTypeIsUnique")
                .ForEach($"{this.baseClassName} otherSibling in commonParent.GetChildren(parentReference.PropertyName)")
                    .If("!ReferenceEquals(otherSibling, currentSibling) && otherSibling.SupplementalTypes.Contains(supplementalTypeInfo.Type)")
                        .Line("Dtmi firstSiblingId = string.Compare(currentSibling.Id.AbsoluteUri, otherSibling.Id.AbsoluteUri) < 0 ? currentSibling.Id : otherSibling.Id;")
                        .Line("Dtmi secondSiblingId = string.Compare(currentSibling.Id.AbsoluteUri, otherSibling.Id.AbsoluteUri) < 0 ? otherSibling.Id : currentSibling.Id;")
                        .MultiLine("parsingErrorCollection.Notify(")
                            .Line("\"nonUniqueAdjunctType\",")
                            .Line("elementId: firstSiblingId,")
                            .Line("referenceId: secondSiblingId,")
                            .Line("propertyName: parentReference.PropertyName,")
                            .Line("elementType: ContextCollection.GetTermOrUri(supplementalTypeInfo.Type));");
        }

        private void GenerateCheckSupplementalPropertyConstraintsMethod(CsClass parserClass)
        {
            CsMethod method = parserClass.Method(Access.Private, Novelty.Normal, "void", "CheckSupplementalPropertyConstraints");
            method.Param(this.baseClassName, "currentSibling");
            method.Param("SupplementalTypeCollection", "supplementalTypeCollection");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");

            method.Body.ForEach($"{ParserGeneratorValues.IdentifierType} supplementalTypeId in currentSibling.SupplementalTypes")
                .If("supplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo)")
                    .ForEach("KeyValuePair<string, DTSupplementalPropertyInfo> propInfo in supplementalTypeInfo.Properties")
                        .If("propInfo.Value.HasUniqueValue && currentSibling.SupplementalProperties.TryGetValue(propInfo.Key, out object currentPropValue)")
                            .ForEach("ParentReference parentReference in currentSibling.ParentReferences")
                                .Line($"{this.baseClassName} commonParent = this.Dict[parentReference.ParentId];")
                                .Line("string commonPropName = parentReference.PropertyName;")
                                .Break()
                                .ForEach($"{this.baseClassName} otherSibling in commonParent.GetChildren(commonPropName)")
                                    .If("!ReferenceEquals(otherSibling, currentSibling) && otherSibling.SupplementalProperties.TryGetValue(propInfo.Key, out object siblingPropValue) && siblingPropValue.Equals(currentPropValue)")
                                        .MultiLine("parsingErrorCollection.Notify(")
                                            .Line("\"nonUniquePropertyValue\",")
                                            .Line("elementId: parentReference.ParentId,")
                                            .Line("propertyName: commonPropName,")
                                            .Line("nestedName: propInfo.Key,")
                                            .Line("nestedValue: currentPropValue.ToString());");
        }

        private void GenerateCheckSiblingConstraintsMethod(CsClass parserClass)
        {
            CsMethod method = parserClass.Method(Access.Private, Novelty.Normal, "void", "CheckSiblingConstraints");
            method.Param(this.baseClassName, "currentSibling");
            method.Param("SupplementalTypeCollection", "supplementalTypeCollection");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");
            method.Param("Dictionary<(string, string, string, string), Dictionary<string, JsonLdElement>>", "referenceElts");

            CsIf ifCanCheck = method.Body.If($"currentSibling.SiblingConstraints != null && currentSibling.{ParserGeneratorValues.IdentifierName}.Tail == string.Empty");

            CsForEach forEachParentRef = ifCanCheck.ForEach($"ParentReference parentReference in currentSibling.{ParserGeneratorValues.ParentReferencesName}");

            CsForEach forEachConstraint = forEachParentRef.ForEach("SiblingConstraint siblingConstraint in currentSibling.SiblingConstraints");

            CsIf ifKeyrefNotNull = forEachConstraint.If("siblingConstraint.KeyrefPropertyId != null");

            this.GenerateTryGetTargetSibling(ifKeyrefNotNull, "siblingConstraint.KeyPropertyName", "siblingConstraint.KeyrefPropertyId", "currentSibling.Id");

            CsIf ifUniqueRefNotNull = ifKeyrefNotNull.If("siblingConstraint.UniqueReference != null");
            ifUniqueRefNotNull
                .Line("Dtmi pathVal = (Dtmi)currentSibling.SupplementalProperties[siblingConstraint.UniqueReference.ToString()];")
                .Line("var reference = (parentReference.ParentId.ToString(), parentReference.PropertyName, keyVal, pathVal.ToString());")
                .Line("string uniqueRefName = ContextCollection.GetTermOrUri(siblingConstraint.UniqueReference);")
                .Line("string pathName = ContextCollection.GetTermOrUri(pathVal);");

            CsIf ifTryGetRef = ifUniqueRefNotNull.If("referenceElts.TryGetValue(reference, out Dictionary<string, JsonLdElement> extantElts)");

            ifTryGetRef
                .Line("JsonLdElement extantElt = extantElts.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value;")
                .Line("JsonLdElement currentElt = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value;");

            ifTryGetRef
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"multipleReference\",")
                    .Line("elementId: parentReference.ParentId,")
                    .Line("propertyName: parentReference.PropertyName,")
                    .Line("referenceId: siblingConstraint.UniqueReference,")
                    .Line("propertyConjunction: $\"'{keyrefProp}' and '{uniqueRefName}'\",")
                    .Line("valueConjunction: $\"'{keyVal}' and '{pathName}'\",")
                    .Line("element: currentElt,")
                    .Line("siblingElement: extantElt);");

            ifTryGetRef.Else().Line("referenceElts[reference] = currentSibling.JsonLdElements;");

            CsIf ifNotHasRequiredType = ifKeyrefNotNull.If("siblingConstraint.RequiredTypes != null && !siblingConstraint.RequiredTypes.Any(t => ((ITypeChecker)targetSibling).DoesHaveType(t))");

            ifNotHasRequiredType
                .Line("JsonLdProperty refProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value?.Properties?.First(p => p.Name == keyrefProp);")
                .Line("JsonLdElement targetElt = targetSibling.JsonLdElements.First().Value;")
                .Break();

            ifNotHasRequiredType
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"notSiblingRequiredType\",")
                    .Line("elementId: currentSibling.Id,")
                    .Line("propertyName: keyrefProp,")
                    .Line("referenceId: targetSibling.Id,")
                    .Line("refPropertyName: keyProp,")
                    .Line("refValue: keyVal,")
                    .Line("typeRestriction: siblingConstraint.RequiredTypesString,")
                    .Line("refProperty: refProp,")
                    .Line("siblingElement: targetElt);");

            CsIf ifDisallowedType = ifKeyrefNotNull.If("siblingConstraint.DisallowedType != null && ((ITypeChecker)targetSibling).DoesHaveType(siblingConstraint.DisallowedType)");

            ifDisallowedType
                .Line("JsonLdProperty refProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value?.Properties?.First(p => p.Name == keyrefProp);")
                .Line("JsonLdElement targetElt = targetSibling.JsonLdElements.FirstOrDefault(e => e.Value.Types.Contains(siblingConstraint.DisallowedTypeName)).Value;")
                .Break();

            ifDisallowedType
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"disallowedType\",")
                    .Line("elementId: currentSibling.Id,")
                    .Line("propertyName: keyrefProp,")
                    .Line("referenceId: targetSibling.Id,")
                    .Line("refPropertyName: keyProp,")
                    .Line("refValue: keyVal,")
                    .Line("typeRestriction: siblingConstraint.DisallowedTypeName,")
                    .Line("refProperty: refProp,")
                    .Line("siblingElement: targetElt);");

            CsIf ifSupplementalPropertyPathNotNull = ifKeyrefNotNull.If("siblingConstraint.SupplementalPropertyPath != null");

            ifSupplementalPropertyPathNotNull.Line("Dtmi supplementalPropertyId = (Dtmi)currentSibling.SupplementalProperties[siblingConstraint.SupplementalPropertyPath.ToString()];");

            CsIf ifNoMatchingSupplementalProperty = ifSupplementalPropertyPathNotNull.If("!supplementalTypeCollection.DoesAnySupplementalTypeHaveProperty(targetSibling.SupplementalTypes, supplementalPropertyId.ToString())");

            ifNoMatchingSupplementalProperty
                .Line("string propertyPath = ContextCollection.GetTermOrUri(siblingConstraint.SupplementalPropertyPath);")
                .Line("JsonLdProperty refProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value?.Properties?.First(p => p.Name == keyrefProp);")
                .Line("JsonLdElement targetElt = targetSibling.JsonLdElements.First().Value;")
                .Line("string supplementalPropTerm = ContextCollection.GetTermOrUri(supplementalPropertyId);")
                .Break();

            CsIf ifMatchNotSupplemental = ifNoMatchingSupplementalProperty.If("targetSibling.MaterialProperties.Contains(supplementalPropTerm)");

            ifMatchNotSupplemental
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"matchingPropertyNotSupplemental\",")
                    .Line("elementId: currentSibling.Id,")
                    .Line("propertyName: keyrefProp,")
                    .Line("referenceId: targetSibling.Id,")
                    .Line("refPropertyName: keyProp,")
                    .Line("refValue: keyVal,")
                    .Line("nestedName: supplementalPropTerm,")
                    .Line("literalPropertyName: propertyPath,")
                    .Line($"typeRestriction: targetSibling.{this.baseEnumPropertyName}.ToString(),")
                    .Line("refProperty: refProp,")
                    .Line("siblingElement: targetElt);");

            ifMatchNotSupplemental.Else()
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"noMatchingSupplementalProperty\",")
                    .Line("elementId: currentSibling.Id,")
                    .Line("propertyName: keyrefProp,")
                    .Line("referenceId: targetSibling.Id,")
                    .Line("refPropertyName: keyProp,")
                    .Line("refValue: keyVal,")
                    .Line("nestedName: supplementalPropTerm,")
                    .Line("literalPropertyName: propertyPath,")
                    .Line($"typeRestriction: targetSibling.{this.baseEnumPropertyName}.ToString(),")
                    .Line("refProperty: refProp,")
                    .Line("siblingElement: targetElt);");
        }

        private void GenerateTryGetTargetSibling(CsScope scope, string keyVar, string keyrefVar, string currentIdVar)
        {
            scope
                .Line($"string keyVal = (string)currentSibling.SupplementalProperties[{keyrefVar}.ToString()];")
                .Line($"{this.baseClassName} commonParent = this.Dict[parentReference.ParentId];")
                .Line("string commonPropName = parentReference.PropertyName;")
                .Line($"string keyProp = {keyVar};")
                .Line($"string keyrefProp = ContextCollection.GetTermOrUri({keyrefVar});")
                .If($"!commonParent.TryGetChild(commonPropName, {keyVar}, keyVal, out {this.baseClassName} targetSibling)")
                    .Line("continue;");
        }

        private void GenerateCheckForMismatch(CsScope scope, string valueConstraintField, string supplementalPropertyField, string constraintValueVar)
        {
            CsIf ifConstraintFieldNotNull = scope.If($"elementPropertyConstraint.ValueConstraint.{valueConstraintField} != null");
            ifConstraintFieldNotNull.Line($"{ParserGeneratorValues.IdentifierType} {constraintValueVar} = ({ParserGeneratorValues.IdentifierType})currentSibling.SupplementalProperties[elementPropertyConstraint.ValueConstraint.{valueConstraintField}.ToString()];");

            CsForEach forEachTargetSupplementalTypeId = ifConstraintFieldNotNull.ForEach($"{ParserGeneratorValues.IdentifierType} targetSupplementalTypeId in targetSibling.SupplementalTypes");
            CsIf ifTryGetSupplementalPropertyInfo = forEachTargetSupplementalTypeId.If($"supplementalTypeCollection.TryGetSupplementalTypeInfo(targetSupplementalTypeId, out DTSupplementalTypeInfo targetSupplementalTypeInfo) && targetSupplementalTypeInfo.Properties.TryGetValue({constraintValueVar}.ToString(), out DTSupplementalPropertyInfo supplementalPropertyInfo)");

            CsIf ifPropertyMismatch = ifTryGetSupplementalPropertyInfo.If($"elementPropertyConstraint.ElementId != supplementalPropertyInfo.{supplementalPropertyField}");

            ifPropertyMismatch
                .Line("string actualValue = ContextCollection.GetTermOrUri(elementPropertyConstraint.ElementId);")
                .Line($"string expectedValue = ContextCollection.GetTermOrUri(supplementalPropertyInfo.{supplementalPropertyField});")
                .Line("JsonLdProperty propProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == elementPropertyConstraint.PropertyName)).Value?.Properties?.First(p => p.Name == elementPropertyConstraint.PropertyName);")
                .Break();

            ifPropertyMismatch
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"siblingPropertyMismatch\",")
                    .Line("elementId: elementPropertyConstraint.ParentId,")
                    .Line("propertyName: elementPropertyConstraint.PropertyName,")
                    .Line("refValue: actualValue,")
                    .Line("valueRestriction: expectedValue,")
                    .Line("incidentProperty: propProp);");
        }
    }
}
