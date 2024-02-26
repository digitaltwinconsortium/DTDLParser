namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using DTDLParser.Models;

    /// <summary>
    /// A collection of DTDL types that are not materialized as C# classes.
    /// </summary>
    internal partial class SupplementalTypeCollection
    {
        private static readonly Dictionary<Dtmi, DTSupplementalTypeInfo> EndogenousSupplementalTypes;

        private Dictionary<Dtmi, DTSupplementalTypeInfo> exogenousSupplementalTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalTypeCollection"/> class.
        /// </summary>
        internal SupplementalTypeCollection()
        {
            this.exogenousSupplementalTypes = new Dictionary<Dtmi, DTSupplementalTypeInfo>();
        }

        /// <summary>
        /// Add model-level elements from a DTDL extension to the collection.
        /// </summary>
        /// <param name="extensionId">The identifier of the extension.</param>
        /// <param name="metamodelGraphElements">A list of <see cref="JsonLdElement"/> of DTDL metamodel elements.</param>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        internal void AddExtensionMetamodel(Dtmi extensionId, List<JsonLdElement> metamodelGraphElements, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            if (metamodelGraphElements == null)
            {
                return;
            }

            foreach (JsonLdElement metamodelGraphElement in metamodelGraphElements)
            {
                DtdlExtensionMetamodelElement metamodelElt = new DtdlExtensionMetamodelElement(aggregateContext, metamodelGraphElement, extensionId, parsingErrorCollection);
                parsingErrorCollection.ThrowIfAny();

                if (!this.TryGetParentTypeInfo(extensionId, metamodelElt, aggregateContext, parsingErrorCollection, out Dtmi parentType, out DTSupplementalTypeInfo parentTypeInfo, out DTExtensionKind extensionKind))
                {
                    continue;
                }

                DTSupplementalTypeInfo typeInfo = new DTSupplementalTypeInfo(extensionKind, extensionId, metamodelElt.Id, metamodelElt.IsAbstract, metamodelElt.IsMergeable, parentType);

                if (!TrySetAllowedCotypes(extensionId, metamodelElt.Id, metamodelElt.AllowedCotypes, typeInfo, parentTypeInfo, aggregateContext, parsingErrorCollection) ||
                    !TrySetRequiredCocotypes(metamodelElt.RequiredCocotypes, typeInfo, parentTypeInfo, aggregateContext, parsingErrorCollection) ||
                    !TrySetDisallowedCocotypes(metamodelElt.DisallowedCocotypes, typeInfo, parentTypeInfo, aggregateContext, parsingErrorCollection) ||
                    !TryAddProperties(metamodelElt.Properties, typeInfo, aggregateContext, parentTypeInfo, parsingErrorCollection))
                {
                    continue;
                }

                typeInfo.ParentSupplementalType = parentTypeInfo;

                this.exogenousSupplementalTypes[metamodelElt.Id] = typeInfo;
            }

            parsingErrorCollection.ThrowIfAny();
        }

        /// <summary>
        /// Gets a dictionary that maps from the DTMI of a type to a <c>DTSupplementalTypeInfo</c> object.
        /// </summary>
        /// <returns>The supplemental type dictionary.</returns>
        internal IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> GetSupplementalTypes()
        {
            return EndogenousSupplementalTypes.ExpandWith(this.exogenousSupplementalTypes);
        }

        /// <summary>
        /// Get a <see cref="DTSupplementalTypeInfo"/> object corresponding to the <paramref name="supplementalTypeId"/>, if present in the collection.
        /// </summary>
        /// <param name="supplementalTypeId">The ID of the supplemental type to get.</param>
        /// <param name="supplementalTypeInfo">Out parameter to receive the <see cref="DTSupplementalTypeInfo"/> object for the type.</param>
        /// <returns>True if the collection contains the indicated type.</returns>
        internal bool TryGetSupplementalTypeInfo(Dtmi supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo)
        {
            return EndogenousSupplementalTypes.TryGetValue(supplementalTypeId, out supplementalTypeInfo)
                || this.exogenousSupplementalTypes.TryGetValue(supplementalTypeId, out supplementalTypeInfo);
        }

        /// <summary>
        /// Indicate whether any supplemental type in a collection has a given property.
        /// </summary>
        /// <param name="supplementalTypeIds">A <c>ReadOnlyCollection</c> of supplemental type IDs.</param>
        /// <param name="propertyId">The ID of the property to check for.</param>
        /// <returns>True if some supplemental type in the collection has the indicated propety ID.</returns>
        internal bool DoesAnySupplementalTypeHaveProperty(IReadOnlyCollection<Dtmi> supplementalTypeIds, string propertyId)
        {
            return supplementalTypeIds.Any(t =>
                (EndogenousSupplementalTypes.TryGetValue(t, out DTSupplementalTypeInfo endoTypeInfo) && endoTypeInfo.Properties.ContainsKey(propertyId)) ||
                (this.exogenousSupplementalTypes.TryGetValue(t, out DTSupplementalTypeInfo exoTypeInfo) && exoTypeInfo.Properties.ContainsKey(propertyId)));
        }

        private static void ConnectEndogenousPropertySetters()
        {
            foreach (KeyValuePair<Dtmi, DTSupplementalTypeInfo> supplementalType in EndogenousSupplementalTypes)
            {
                if (supplementalType.Value.ParentType != null && EndogenousSupplementalTypes.ContainsKey(supplementalType.Value.ParentType))
                {
                    supplementalType.Value.ParentSupplementalType = EndogenousSupplementalTypes[supplementalType.Value.ParentType];
                }
            }
        }

        private static bool TryGetClassConstraints(List<JsonLdValue> typeValues, string constraintSpecifier, DTSupplementalTypeInfo typeInfo, AggregateContext aggregateContext, out HashSet<Dtmi> classIds, ParsingErrorCollection parsingErrorCollection)
        {
            if (typeValues != null)
            {
                classIds = new HashSet<Dtmi>();
                foreach (JsonLdValue typeValue in typeValues)
                {
                    if (typeValue?.StringValue == null)
                    {
                        parsingErrorCollection.Notify(
                            "classNotString",
                            contextId: typeInfo.ContextId,
                            elementId: typeInfo.Type,
                            constraintName: constraintSpecifier,
                            incidentValue: typeValue);
                        return false;
                    }

                    if (!aggregateContext.TryCreateDtmi(typeValue.StringValue, out Dtmi classId))
                    {
                        parsingErrorCollection.Notify(
                            "classNotRecognized",
                            contextId: typeInfo.ContextId,
                            elementId: typeInfo.Type,
                            constraintName: constraintSpecifier,
                            constraintValue: typeValue.StringValue,
                            incidentValue: typeValue);
                        return false;
                    }

                    classIds.Add(classId);
                }
            }
            else
            {
                classIds = null;
            }

            return true;
        }

        private static bool TrySetRequiredCocotypes(List<JsonLdValue> requiredCocotypes, DTSupplementalTypeInfo typeInfo, DTSupplementalTypeInfo parentTypeInfo, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            if (!TryGetClassConstraints(requiredCocotypes, "sh:and", typeInfo, aggregateContext, out HashSet<Dtmi> cocotypeIds, parsingErrorCollection))
            {
                return false;
            }

            if (cocotypeIds != null)
            {
                typeInfo.RequiredCocotypes = cocotypeIds;
                if (parentTypeInfo != null)
                {
                    typeInfo.RequiredCocotypes.UnionWith(parentTypeInfo.RequiredCocotypes);
                }
            }
            else if (parentTypeInfo != null)
            {
                typeInfo.RequiredCocotypes = parentTypeInfo.RequiredCocotypes;
            }

            return true;
        }

        private static bool TrySetDisallowedCocotypes(List<JsonLdValue> disallowedCocotypes, DTSupplementalTypeInfo typeInfo, DTSupplementalTypeInfo parentTypeInfo, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            if (!TryGetClassConstraints(disallowedCocotypes, "sh:not", typeInfo, aggregateContext, out HashSet<Dtmi> discotypeIds, parsingErrorCollection))
            {
                return false;
            }

            if (discotypeIds != null)
            {
                typeInfo.DisallowedCocotypes = discotypeIds;
                if (parentTypeInfo != null)
                {
                    typeInfo.DisallowedCocotypes.UnionWith(parentTypeInfo.DisallowedCocotypes);
                }
            }
            else if (parentTypeInfo != null)
            {
                typeInfo.DisallowedCocotypes = parentTypeInfo.DisallowedCocotypes;
            }

            return true;
        }

        private static bool TryAddProperties(Dictionary<string, DtdlExtensionProperty> extensionProperties, DTSupplementalTypeInfo typeInfo, AggregateContext aggregateContext, DTSupplementalTypeInfo parentTypeInfo, ParsingErrorCollection parsingErrorCollection)
        {
            IReadOnlyDictionary<string, DTSupplementalPropertyInfo> parentPropertiesInfo = parentTypeInfo?.Properties ?? new Dictionary<string, DTSupplementalPropertyInfo>();

            foreach (DtdlExtensionProperty extensionProperty in extensionProperties.Values)
            {
                if (extensionProperty.Path == null)
                {
                    parsingErrorCollection.Notify(
                        "pathConstraintNoValue",
                        contextId: typeInfo.ContextId,
                        typeId: typeInfo.Type,
                        extensionProperty: extensionProperty);
                    return false;
                }

                if (!aggregateContext.TryCreateDtmi(extensionProperty.Path, out Dtmi pathId))
                {
                    parsingErrorCollection.Notify(
                        "pathNotRecognized",
                        contextId: typeInfo.ContextId,
                        typeId: typeInfo.Type,
                        constraintValue: extensionProperty.Path,
                        extensionProperty: extensionProperty);
                    return false;
                }

                parentPropertiesInfo.TryGetValue(pathId.AbsoluteUri, out DTSupplementalPropertyInfo parentPropertyInfo);

                if (!TryGetPropertyType(extensionProperty, parentPropertyInfo, typeInfo, aggregateContext, parentPropertiesInfo, out Uri propertyTypeUri, parsingErrorCollection) ||
                    !TryGetRegex(extensionProperty, parentPropertyInfo, typeInfo, aggregateContext, parentPropertiesInfo, out Regex regex, parsingErrorCollection) ||
                    !TryGetChildOfId(extensionProperty, parentPropertyInfo, typeInfo, aggregateContext, parentPropertiesInfo, out Dtmi childOfId, parsingErrorCollection) ||
                    !TryGetMinCountAndIsOptional(extensionProperty, parentPropertyInfo, typeInfo, aggregateContext, parentPropertiesInfo, out int? minCount, out bool isOptional, parsingErrorCollection) ||
                    !TryGetMaxCountAndIsPlural(extensionProperty, parentPropertyInfo, typeInfo, aggregateContext, parentPropertiesInfo, out int? maxCount, out bool isPlural, parsingErrorCollection) ||
                    !TryValidateMinAndMaxCounts(extensionProperty, typeInfo, minCount, maxCount, parsingErrorCollection))
                {
                    return false;
                }

                typeInfo.AddProperty(pathId.AbsoluteUri, propertyTypeUri, maxCount: maxCount, minCount: minCount, maxInclusive: null, minInclusive: null, maxLength: null, regex: regex, hasUniqueValue: false, isPlural: isPlural, isOptional: isOptional, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: true, typeRequired: true, childOf: childOfId, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            }

            return true;
        }

        private static bool TryGetPropertyType(DtdlExtensionProperty extensionProperty, DTSupplementalPropertyInfo parentPropertyInfo, DTSupplementalTypeInfo typeInfo, AggregateContext aggregateContext, IReadOnlyDictionary<string, DTSupplementalPropertyInfo> parentPropertiesInfo, out Uri propertyTypeUri, ParsingErrorCollection parsingErrorCollection)
        {
            propertyTypeUri = null;
            if (extensionProperty.Class != null)
            {
                if (!aggregateContext.TryCreateDtmi(extensionProperty.Class, out Dtmi typeId))
                {
                    parsingErrorCollection.Notify(
                        "propertyClassNotRecognized",
                        contextId: typeInfo.ContextId,
                        elementId: typeInfo.Type,
                        propertyName: extensionProperty.Path,
                        propertyValue: extensionProperty.Class,
                        extensionProperty: extensionProperty);
                    return false;
                }

                propertyTypeUri = typeId;
            }
            else if (parentPropertyInfo != null)
            {
                propertyTypeUri = parentPropertyInfo.Type;
            }

            return true;
        }

        private static bool TryGetRegex(DtdlExtensionProperty extensionProperty, DTSupplementalPropertyInfo parentPropertyInfo, DTSupplementalTypeInfo typeInfo, AggregateContext aggregateContext, IReadOnlyDictionary<string, DTSupplementalPropertyInfo> parentPropertiesInfo, out Regex regex, ParsingErrorCollection parsingErrorCollection)
        {
            regex = null;
            if (extensionProperty.Pattern != null)
            {
                try
                {
                    regex = new Regex(extensionProperty.Pattern);
                }
                catch (ArgumentException)
                {
                    parsingErrorCollection.Notify(
                        "patternInvalid",
                        contextId: typeInfo.ContextId,
                        elementId: typeInfo.Type,
                        propertyName: extensionProperty.Path,
                        propertyValue: extensionProperty.Pattern,
                        extensionProperty: extensionProperty);
                    return false;
                }
            }
            else if (parentPropertyInfo != null)
            {
                regex = parentPropertyInfo.Regex;
            }

            return true;
        }

        private static bool TryGetChildOfId(DtdlExtensionProperty extensionProperty, DTSupplementalPropertyInfo parentPropertyInfo, DTSupplementalTypeInfo typeInfo, AggregateContext aggregateContext, IReadOnlyDictionary<string, DTSupplementalPropertyInfo> parentPropertiesInfo, out Dtmi childOfId, ParsingErrorCollection parsingErrorCollection)
        {
            childOfId = null;
            if (extensionProperty.ChildOf != null)
            {
                if (!aggregateContext.TryCreateDtmi(extensionProperty.ChildOf, out childOfId))
                {
                    parsingErrorCollection.Notify(
                        "childOfNotRecognized",
                        contextId: typeInfo.ContextId,
                        elementId: typeInfo.Type,
                        constraintName: extensionProperty.Path,
                        constraintValue: extensionProperty.ChildOf,
                        extensionProperty: extensionProperty);
                    return false;
                }
            }
            else if (parentPropertyInfo != null)
            {
                childOfId = parentPropertyInfo.ChildOf;
            }

            return true;
        }

        private static bool TryGetMinCountAndIsOptional(DtdlExtensionProperty extensionProperty, DTSupplementalPropertyInfo parentPropertyInfo, DTSupplementalTypeInfo typeInfo, AggregateContext aggregateContext, IReadOnlyDictionary<string, DTSupplementalPropertyInfo> parentPropertiesInfo, out int? minCount, out bool isOptional, ParsingErrorCollection parsingErrorCollection)
        {
            minCount = extensionProperty.MinCount;
            isOptional = minCount == null || minCount < 1;

            if (minCount != null)
            {
                if (minCount < 0)
                {
                    parsingErrorCollection.Notify(
                        "minCountNegative",
                        contextId: typeInfo.ContextId,
                        elementId: typeInfo.Type,
                        propertyName: extensionProperty.Path,
                        propertyValue: minCount.ToString(),
                        extensionProperty: extensionProperty);
                    return false;
                }
            }
            else if (parentPropertyInfo != null)
            {
                minCount = parentPropertyInfo.MinCount;
                isOptional = parentPropertyInfo.IsOptional;
            }

            return true;
        }

        private static bool TryGetMaxCountAndIsPlural(DtdlExtensionProperty extensionProperty, DTSupplementalPropertyInfo parentPropertyInfo, DTSupplementalTypeInfo typeInfo, AggregateContext aggregateContext, IReadOnlyDictionary<string, DTSupplementalPropertyInfo> parentPropertiesInfo, out int? maxCount, out bool isPlural, ParsingErrorCollection parsingErrorCollection)
        {
            maxCount = extensionProperty.MaxCount;
            isPlural = maxCount == null || maxCount > 1;

            if (maxCount != null)
            {
                if (maxCount < 1)
                {
                    parsingErrorCollection.Notify(
                        "maxCountNotPositive",
                        contextId: typeInfo.ContextId,
                        elementId: typeInfo.Type,
                        propertyName: extensionProperty.Path,
                        propertyValue: maxCount.ToString(),
                        extensionProperty: extensionProperty);
                    return false;
                }
            }
            else if (parentPropertyInfo != null)
            {
                maxCount = parentPropertyInfo.MaxCount;
                isPlural = parentPropertyInfo.IsPlural;
            }

            return true;
        }

        private static bool TryValidateMinAndMaxCounts(DtdlExtensionProperty extensionProperty, DTSupplementalTypeInfo typeInfo, int? minCount, int? maxCount, ParsingErrorCollection parsingErrorCollection)
        {
            if (minCount != null && maxCount != null && minCount > maxCount)
            {
                parsingErrorCollection.Notify(
                    "minCountExceedsMaxCount",
                    contextId: typeInfo.ContextId,
                    elementId: typeInfo.Type,
                    propertyName: extensionProperty.Path,
                    propertyValue: minCount.ToString(),
                    refValue: maxCount.ToString(),
                    extensionProperty: extensionProperty);
                return false;
            }

            return true;
        }

        private bool TryGetParentTypeInfo(Dtmi extensionId, DtdlExtensionMetamodelElement metamodelElt, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, out Dtmi parentType, out DTSupplementalTypeInfo parentTypeInfo, out DTExtensionKind extensionKind)
        {
            parentType = null;
            parentTypeInfo = null;
            extensionKind = DTExtensionKind.None;

            if (metamodelElt.SubClassOf?.StringValue == null)
            {
                parsingErrorCollection.Notify(
                    "missingSubClassOf",
                    contextId: extensionId,
                    elementId: metamodelElt.Id,
                    incidentValue: metamodelElt.SubClassOf);
                return false;
            }

            if (!aggregateContext.TryCreateDtmi(metamodelElt.SubClassOf.StringValue, out parentType))
            {
                parsingErrorCollection.Notify(
                    "subClassOfNotRecognized",
                    contextId: extensionId,
                    elementId: metamodelElt.Id,
                    propertyValue: metamodelElt.SubClassOf.StringValue,
                    incidentValue: metamodelElt.SubClassOf);
                return false;
            }

            bool gotParentTypeInfo = this.TryGetSupplementalTypeInfo(parentType, out parentTypeInfo);
            if (!Enum.TryParse(ContextCollection.GetTermOrUri(parentType), out extensionKind) && gotParentTypeInfo)
            {
                extensionKind = parentTypeInfo.ExtensionKind;
            }

            if (extensionKind == DTExtensionKind.None)
            {
                parsingErrorCollection.Notify(
                    "subClassOfNotExtensible",
                    contextId: extensionId,
                    elementId: metamodelElt.Id,
                    propertyValue: metamodelElt.SubClassOf.StringValue,
                    incidentValue: metamodelElt.SubClassOf);
                return false;
            }

            return true;
        }
    }
}
