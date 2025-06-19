/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using DTDLParser.Models;

    /// <summary>
    /// A DTDL model.
    /// </summary>
    internal partial class Model
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        internal Model()
        {
            this.Dict = new Dictionary<Dtmi, DTEntityInfo>(new DtmiEqualityComparer());
        }

        /// <summary>
        /// Gets a dictionary that maps DTMIs to their obverse objects.
        /// </summary>
        internal Dictionary<Dtmi, DTEntityInfo> Dict { get; }

        /// <summary>
        /// Determines whether the <c>EntityKind</c> of the element is in the given <paramref name="kindSet"/>.
        /// </summary>
        /// <param name="elementId">Identifier of the element.</param>
        /// <param name="kindSet">Set of <c>DTEntityKind</c>.</param>
        /// <returns>True if the element's kind is in the set.</returns>
        internal bool IsKindInSet(Dtmi elementId, HashSet<DTEntityKind> kindSet)
        {
            return kindSet.Contains(this.Dict[elementId].EntityKind);
        }

        /// <summary>
        /// Is the element in the dictionary a partition.
        /// </summary>
        /// <param name="elementId">The ID of the element.</param>
        /// <returns>True if the element is a partition.</returns>
        internal bool IsPartition(Dtmi elementId)
        {
            return this.Dict[elementId].IsPartition;
        }

        /// <summary>
        /// Try to set an object property with a given <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="elementId">Identifier of the element.</param>
        /// <param name="layer">Name of the variable that holds the layer of the property.</param>
        /// <param name="propertyName">The name of the property whose value to set if the property is recognized.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="referencedElementId">The element ID of the object to set as the value.</param>
        /// <param name="keyProp">The key property for dictionary properties.</param>
        /// <param name="keyValue">The key value for dictionary properties.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if the property name is recognized.</returns>
        internal bool TrySetObjectProperty(Dtmi elementId, string layer, string propertyName, JsonLdProperty propProp, Dtmi referencedElementId, string keyProp, string keyValue, ParsingErrorCollection parsingErrorCollection)
        {
            DTEntityInfo element = this.Dict.ContainsKey(referencedElementId) ? this.Dict[referencedElementId] : new DTReferenceInfo(0, referencedElementId, null, null, null);
            if (!referencedElementId.AbsoluteUri.StartsWith("dtmi:dtdl:") && !referencedElementId.AbsoluteUri.StartsWith("dtmi:standard:") && !referencedElementId.AbsoluteUri.StartsWith("dtmi:iotcentral:") && !referencedElementId.AbsoluteUri.StartsWith("dtmi:iotoperations:"))
            {
                element.ParentReferences.Add(new ParentReference() { ParentId = elementId, PropertyName = propertyName });
            }

            return this.Dict[elementId].TrySetObjectProperty(propertyName, layer, propProp, element, keyProp, keyValue, parsingErrorCollection);
        }

        /// <summary>
        /// Gets a string representing the <c>EntityKind</c> of an element.
        /// </summary>
        /// <param name="elementId">Identifier of the element.</param>
        /// <returns>String representation of the element's <c>EntityKind</c>.</returns>
        internal string GetKindString(Dtmi elementId)
        {
            return this.Dict[elementId].EntityKind.ToString();
        }

        private void CheckParentConstraints(DTEntityInfo currentSibling, SupplementalTypeCollection supplementalTypeCollection, ParsingErrorCollection parsingErrorCollection)
        {
            foreach (Dtmi supplementalTypeId in currentSibling.SupplementalTypes)
            {
                if (supplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
                {
                    foreach (ParentConstraint parentConstraint in supplementalTypeInfo.ParentConstraints)
                    {
                        foreach (ParentReference parentReference in currentSibling.ParentReferences)
                        {
                            if (parentReference.PropertyName == parentConstraint.ParentPropertyName)
                            {
                                DTEntityInfo commonParent = this.Dict[parentReference.ParentId];
                                if (parentConstraint.RequiredParentCotype != null && !commonParent.SupplementalTypes.Contains(parentConstraint.RequiredParentCotype))
                                {
                                    parsingErrorCollection.Notify(
                                        "parentMissingCotype",
                                        elementId: currentSibling.Id,
                                        propertyName: parentReference.PropertyName,
                                        elementType: ContextCollection.GetTermOrUri(supplementalTypeInfo.Type),
                                        referenceType: ContextCollection.GetTermOrUri(parentConstraint.RequiredParentCotype));
                                }

                                if (parentConstraint.AdjunctTypeIsUnique)
                                {
                                    foreach (DTEntityInfo otherSibling in commonParent.GetChildren(parentReference.PropertyName))
                                    {
                                        if (!ReferenceEquals(otherSibling, currentSibling) && otherSibling.SupplementalTypes.Contains(supplementalTypeInfo.Type))
                                        {
                                            Dtmi firstSiblingId = string.Compare(currentSibling.Id.AbsoluteUri, otherSibling.Id.AbsoluteUri) < 0 ? currentSibling.Id : otherSibling.Id;
                                            Dtmi secondSiblingId = string.Compare(currentSibling.Id.AbsoluteUri, otherSibling.Id.AbsoluteUri) < 0 ? otherSibling.Id : currentSibling.Id;
                                            parsingErrorCollection.Notify(
                                                "nonUniqueAdjunctType",
                                                elementId: firstSiblingId,
                                                referenceId: secondSiblingId,
                                                propertyName: parentReference.PropertyName,
                                                elementType: ContextCollection.GetTermOrUri(supplementalTypeInfo.Type));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CheckSiblingConstraints(DTEntityInfo currentSibling, SupplementalTypeCollection supplementalTypeCollection, ParsingErrorCollection parsingErrorCollection, Dictionary<(string, string, string, string), Dictionary<string, JsonLdElement>> referenceElts)
        {
            if (currentSibling.SiblingConstraints != null && currentSibling.Id.Tail == string.Empty)
            {
                foreach (ParentReference parentReference in currentSibling.ParentReferences)
                {
                    foreach (SiblingConstraint siblingConstraint in currentSibling.SiblingConstraints)
                    {
                        if (siblingConstraint.KeyrefPropertyId != null)
                        {
                            string keyVal = (string)currentSibling.SupplementalProperties[siblingConstraint.KeyrefPropertyId.ToString()];
                            DTEntityInfo commonParent = this.Dict[parentReference.ParentId];
                            string commonPropName = parentReference.PropertyName;
                            string keyProp = siblingConstraint.KeyPropertyName;
                            string keyrefProp = ContextCollection.GetTermOrUri(siblingConstraint.KeyrefPropertyId);
                            if (!commonParent.TryGetChild(commonPropName, siblingConstraint.KeyPropertyName, keyVal, out DTEntityInfo targetSibling))
                            {
                                continue;
                            }

                            if (siblingConstraint.UniqueReference != null)
                            {
                                Dtmi pathVal = (Dtmi)currentSibling.SupplementalProperties[siblingConstraint.UniqueReference.ToString()];
                                var reference = (parentReference.ParentId.ToString(), parentReference.PropertyName, keyVal, pathVal.ToString());
                                string uniqueRefName = ContextCollection.GetTermOrUri(siblingConstraint.UniqueReference);
                                string pathName = ContextCollection.GetTermOrUri(pathVal);
                                if (referenceElts.TryGetValue(reference, out Dictionary<string, JsonLdElement> extantElts))
                                {
                                    JsonLdElement extantElt = extantElts.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value;
                                    JsonLdElement currentElt = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value;
                                    parsingErrorCollection.Notify(
                                        "multipleReference",
                                        elementId: parentReference.ParentId,
                                        propertyName: parentReference.PropertyName,
                                        referenceId: siblingConstraint.UniqueReference,
                                        propertyConjunction: $"'{keyrefProp}' and '{uniqueRefName}'",
                                        valueConjunction: $"'{keyVal}' and '{pathName}'",
                                        element: currentElt,
                                        siblingElement: extantElt);
                                }
                                else
                                {
                                    referenceElts[reference] = currentSibling.JsonLdElements;
                                }
                            }

                            if (siblingConstraint.RequiredTypes != null && !siblingConstraint.RequiredTypes.Any(t => ((ITypeChecker)targetSibling).DoesHaveType(t)))
                            {
                                JsonLdProperty refProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value?.Properties?.First(p => p.Name == keyrefProp);
                                JsonLdElement targetElt = targetSibling.JsonLdElements.First().Value;

                                parsingErrorCollection.Notify(
                                    "notSiblingRequiredType",
                                    elementId: currentSibling.Id,
                                    propertyName: keyrefProp,
                                    referenceId: targetSibling.Id,
                                    refPropertyName: keyProp,
                                    refValue: keyVal,
                                    typeRestriction: siblingConstraint.RequiredTypesString,
                                    refProperty: refProp,
                                    siblingElement: targetElt);
                            }

                            if (siblingConstraint.DisallowedType != null && ((ITypeChecker)targetSibling).DoesHaveType(siblingConstraint.DisallowedType))
                            {
                                JsonLdProperty refProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value?.Properties?.First(p => p.Name == keyrefProp);
                                JsonLdElement targetElt = targetSibling.JsonLdElements.FirstOrDefault(e => e.Value.Types.Contains(siblingConstraint.DisallowedTypeName)).Value;

                                parsingErrorCollection.Notify(
                                    "disallowedType",
                                    elementId: currentSibling.Id,
                                    propertyName: keyrefProp,
                                    referenceId: targetSibling.Id,
                                    refPropertyName: keyProp,
                                    refValue: keyVal,
                                    typeRestriction: siblingConstraint.DisallowedTypeName,
                                    refProperty: refProp,
                                    siblingElement: targetElt);
                            }

                            if (siblingConstraint.SupplementalPropertyPath != null)
                            {
                                Dtmi supplementalPropertyId = (Dtmi)currentSibling.SupplementalProperties[siblingConstraint.SupplementalPropertyPath.ToString()];
                                if (!supplementalTypeCollection.DoesAnySupplementalTypeHaveProperty(targetSibling.SupplementalTypes, supplementalPropertyId.ToString()))
                                {
                                    string propertyPath = ContextCollection.GetTermOrUri(siblingConstraint.SupplementalPropertyPath);
                                    JsonLdProperty refProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == keyrefProp)).Value?.Properties?.First(p => p.Name == keyrefProp);
                                    JsonLdElement targetElt = targetSibling.JsonLdElements.First().Value;
                                    string supplementalPropTerm = ContextCollection.GetTermOrUri(supplementalPropertyId);

                                    if (targetSibling.MaterialProperties.Contains(supplementalPropTerm))
                                    {
                                        parsingErrorCollection.Notify(
                                            "matchingPropertyNotSupplemental",
                                            elementId: currentSibling.Id,
                                            propertyName: keyrefProp,
                                            referenceId: targetSibling.Id,
                                            refPropertyName: keyProp,
                                            refValue: keyVal,
                                            nestedName: supplementalPropTerm,
                                            literalPropertyName: propertyPath,
                                            typeRestriction: targetSibling.EntityKind.ToString(),
                                            refProperty: refProp,
                                            siblingElement: targetElt);
                                    }
                                    else
                                    {
                                        parsingErrorCollection.Notify(
                                            "noMatchingSupplementalProperty",
                                            elementId: currentSibling.Id,
                                            propertyName: keyrefProp,
                                            referenceId: targetSibling.Id,
                                            refPropertyName: keyProp,
                                            refValue: keyVal,
                                            nestedName: supplementalPropTerm,
                                            literalPropertyName: propertyPath,
                                            typeRestriction: targetSibling.EntityKind.ToString(),
                                            refProperty: refProp,
                                            siblingElement: targetElt);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CheckSupplementalPropertyConstraints(DTEntityInfo currentSibling, SupplementalTypeCollection supplementalTypeCollection, ParsingErrorCollection parsingErrorCollection)
        {
            foreach (Dtmi supplementalTypeId in currentSibling.SupplementalTypes)
            {
                if (supplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
                {
                    foreach (KeyValuePair<string, DTSupplementalPropertyInfo> propInfo in supplementalTypeInfo.Properties)
                    {
                        if (propInfo.Value.HasUniqueValue && currentSibling.SupplementalProperties.TryGetValue(propInfo.Key, out object currentPropValue))
                        {
                            foreach (ParentReference parentReference in currentSibling.ParentReferences)
                            {
                                DTEntityInfo commonParent = this.Dict[parentReference.ParentId];
                                string commonPropName = parentReference.PropertyName;

                                foreach (DTEntityInfo otherSibling in commonParent.GetChildren(commonPropName))
                                {
                                    if (!ReferenceEquals(otherSibling, currentSibling) && otherSibling.SupplementalProperties.TryGetValue(propInfo.Key, out object siblingPropValue) && siblingPropValue.Equals(currentPropValue))
                                    {
                                        parsingErrorCollection.Notify(
                                            "nonUniquePropertyValue",
                                            elementId: parentReference.ParentId,
                                            propertyName: commonPropName,
                                            nestedName: propInfo.Key,
                                            nestedValue: currentPropValue.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CheckValueConstraintOnSibling(ElementPropertyConstraint elementPropertyConstraint, SupplementalTypeCollection supplementalTypeCollection, ParsingErrorCollection parsingErrorCollection)
        {
            DTEntityInfo currentSibling = this.Dict[elementPropertyConstraint.ParentId];
            foreach (ParentReference parentReference in currentSibling.ParentReferences)
            {
                string keyVal = (string)currentSibling.SupplementalProperties[elementPropertyConstraint.ValueConstraint.SiblingKeyrefPropertyId.ToString()];
                DTEntityInfo commonParent = this.Dict[parentReference.ParentId];
                string commonPropName = parentReference.PropertyName;
                string keyProp = elementPropertyConstraint.ValueConstraint.SiblingKeyPropertyName;
                string keyrefProp = ContextCollection.GetTermOrUri(elementPropertyConstraint.ValueConstraint.SiblingKeyrefPropertyId);
                if (!commonParent.TryGetChild(commonPropName, elementPropertyConstraint.ValueConstraint.SiblingKeyPropertyName, keyVal, out DTEntityInfo targetSibling))
                {
                    continue;
                }

                if (elementPropertyConstraint.ValueConstraint.SiblingClassOfPropertyId != null)
                {
                    Dtmi classOfPropertyId = (Dtmi)currentSibling.SupplementalProperties[elementPropertyConstraint.ValueConstraint.SiblingClassOfPropertyId.ToString()];
                    foreach (Dtmi targetSupplementalTypeId in targetSibling.SupplementalTypes)
                    {
                        if (supplementalTypeCollection.TryGetSupplementalTypeInfo(targetSupplementalTypeId, out DTSupplementalTypeInfo targetSupplementalTypeInfo) && targetSupplementalTypeInfo.Properties.TryGetValue(classOfPropertyId.ToString(), out DTSupplementalPropertyInfo supplementalPropertyInfo))
                        {
                            if (elementPropertyConstraint.ElementId != supplementalPropertyInfo.Type)
                            {
                                string actualValue = ContextCollection.GetTermOrUri(elementPropertyConstraint.ElementId);
                                string expectedValue = ContextCollection.GetTermOrUri(supplementalPropertyInfo.Type);
                                JsonLdProperty propProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == elementPropertyConstraint.PropertyName)).Value?.Properties?.First(p => p.Name == elementPropertyConstraint.PropertyName);

                                parsingErrorCollection.Notify(
                                    "siblingPropertyMismatch",
                                    elementId: elementPropertyConstraint.ParentId,
                                    propertyName: elementPropertyConstraint.PropertyName,
                                    refValue: actualValue,
                                    valueRestriction: expectedValue,
                                    incidentProperty: propProp);
                            }
                        }
                    }
                }

                if (elementPropertyConstraint.ValueConstraint.SiblingParentOfPropertyId != null)
                {
                    Dtmi parentOfPropertyId = (Dtmi)currentSibling.SupplementalProperties[elementPropertyConstraint.ValueConstraint.SiblingParentOfPropertyId.ToString()];
                    foreach (Dtmi targetSupplementalTypeId in targetSibling.SupplementalTypes)
                    {
                        if (supplementalTypeCollection.TryGetSupplementalTypeInfo(targetSupplementalTypeId, out DTSupplementalTypeInfo targetSupplementalTypeInfo) && targetSupplementalTypeInfo.Properties.TryGetValue(parentOfPropertyId.ToString(), out DTSupplementalPropertyInfo supplementalPropertyInfo))
                        {
                            if (elementPropertyConstraint.ElementId != supplementalPropertyInfo.ChildOf)
                            {
                                string actualValue = ContextCollection.GetTermOrUri(elementPropertyConstraint.ElementId);
                                string expectedValue = ContextCollection.GetTermOrUri(supplementalPropertyInfo.ChildOf);
                                JsonLdProperty propProp = currentSibling.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == elementPropertyConstraint.PropertyName)).Value?.Properties?.First(p => p.Name == elementPropertyConstraint.PropertyName);

                                parsingErrorCollection.Notify(
                                    "siblingPropertyMismatch",
                                    elementId: elementPropertyConstraint.ParentId,
                                    propertyName: elementPropertyConstraint.PropertyName,
                                    refValue: actualValue,
                                    valueRestriction: expectedValue,
                                    incidentProperty: propProp);
                            }
                        }
                    }
                }
            }
        }
    }
}
