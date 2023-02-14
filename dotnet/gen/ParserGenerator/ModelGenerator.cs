﻿namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>Model</c> class.
    /// </summary>
    public class ModelGenerator : ITypeGenerator
    {
        private readonly string baseClassName;
        private readonly string baseEnumName;
        private readonly string baseEnumPropertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        public ModelGenerator(string baseName)
        {
            this.baseClassName = NameFormatter.FormatNameAsClass(baseName);
            this.baseEnumName = NameFormatter.FormatNameAsEnum(baseName);
            this.baseEnumPropertyName = NameFormatter.FormatNameAsEnumProperty(baseName);
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
            method.Body
                .Line($"{this.baseClassName} element = this.Dict.ContainsKey(referencedElementId) ? this.Dict[referencedElementId] : new {referenceClassName}(0, referencedElementId, null, null, null);")
                .Line($"element.{ParserGeneratorValues.ParentReferencesName}.Add(new ParentReference() {{ ParentId = elementId, PropertyName = propertyName }});")
                .Line("return this.Dict[elementId].TrySetObjectProperty(propertyName, layer, propProp, element, keyProp, keyValue, parsingErrorCollection);");
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
                    .Line("typeRestriction: targetSibling.EntityKind.ToString(),")
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
                    .Line("typeRestriction: targetSibling.EntityKind.ToString(),")
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
