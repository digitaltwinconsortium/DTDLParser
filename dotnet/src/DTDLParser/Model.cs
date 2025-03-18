namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A DTDL this.
    /// </summary>
    internal partial class Model
    {
        /// <summary>
        /// Set the object properties from <paramref name="objectPropertyInfoList"/> on elements in this <see cref="Model"/>.
        /// </summary>
        /// <param name="objectPropertyInfoList">Object property information for the this.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="standardElementCollection">A <see cref="StandardElementCollection"/> for resolving element aliases as needed.</param>
        /// <param name="preserveElementAliases">True if the method should avoid resolving any aliases before adding the element and referenced elements.</param>
        internal void SetObjectProperties(List<ParsedObjectPropertyInfo> objectPropertyInfoList, ParsingErrorCollection parsingErrorCollection, StandardElementCollection standardElementCollection, bool preserveElementAliases)
        {
            foreach (ParsedObjectPropertyInfo objectPropertyInfo in objectPropertyInfoList)
            {
                Dtmi resolvedReference = preserveElementAliases ? objectPropertyInfo.ReferencedElementId : standardElementCollection.ResolveAnyAliases(objectPropertyInfo.ReferencedElementId);

                if (!this.Dict[resolvedReference].IsPartition)
                {
                    Dtmi sourcePartition = this.Dict[objectPropertyInfo.ElementId].IsPartition || this.Dict[objectPropertyInfo.ElementId].DefinedIn == null ? objectPropertyInfo.ElementId : this.Dict[objectPropertyInfo.ElementId].DefinedIn;
                    Dtmi targetPartition = this.Dict[resolvedReference].DefinedIn;
                    if (targetPartition != null && targetPartition != sourcePartition)
                    {
                        JsonLdProperty propertyProp = this.Dict[objectPropertyInfo.ElementId].JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == objectPropertyInfo.PropertyName)).Value?.Properties?.First(p => p.Name == objectPropertyInfo.PropertyName);
                        JsonLdElement refElt = this.Dict[resolvedReference].JsonLdElements.First().Value;

                        parsingErrorCollection.Notify(
                            "crossPartitionReference",
                            elementId: objectPropertyInfo.ElementId,
                            propertyName: objectPropertyInfo.PropertyName,
                            referenceId: objectPropertyInfo.ReferencedElementId,
                            refPartition: targetPartition.ToString(),
                            partition: sourcePartition.ToString(),
                            incidentProperty: propertyProp,
                            element: refElt);
                    }
                }

                if (objectPropertyInfo.ExpectedKinds != null && !this.IsKindInSet(resolvedReference, objectPropertyInfo.ExpectedKinds))
                {
                    JsonLdProperty propertyProp = this.Dict[objectPropertyInfo.ElementId].JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == objectPropertyInfo.PropertyName)).Value?.Properties?.First(p => p.Name == objectPropertyInfo.PropertyName);
                    JsonLdValue propertyValue = propertyProp.Values.Values.FirstOrDefault(v => v.StringValue == objectPropertyInfo.ReferencedIdString);

                    if (propertyProp != null && propertyProp.TryGetSourceLocation(out string sourceName1, out int sourceLine1))
                    {
                        string sourceName2 = null;
                        int startLine2 = 0;
                        int endLine2 = 0;
                        propertyValue?.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2);
                        parsingErrorCollection.Add(
                            new Uri("dtmi:dtdl:parsingError:badType"),
                            objectPropertyInfo.BadTypeLocatedCauseFormat,
                            objectPropertyInfo.BadTypeActionFormat,
                            primaryId: objectPropertyInfo.ElementId,
                            property: objectPropertyInfo.PropertyName,
                            secondaryId: objectPropertyInfo.ReferencedElementId,
                            value: this.GetKindString(resolvedReference),
                            sourceName1: sourceName1,
                            startLine1: sourceLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        parsingErrorCollection.Add(
                            new Uri("dtmi:dtdl:parsingError:badType"),
                            objectPropertyInfo.BadTypeCauseFormat,
                            objectPropertyInfo.BadTypeActionFormat,
                            primaryId: objectPropertyInfo.ElementId,
                            property: objectPropertyInfo.PropertyName,
                            secondaryId: objectPropertyInfo.ReferencedElementId,
                            value: this.GetKindString(resolvedReference));
                    }

                    continue;
                }

                if (objectPropertyInfo.AllowedVersions != null && !objectPropertyInfo.AllowedVersions.Contains(this.Dict[resolvedReference].DtdlVersion))
                {
                    string propName = Dtmi.TryCreateDtmi(objectPropertyInfo.PropertyName, out Dtmi dtmi) ? ContextCollection.GetTermOrUri(dtmi) : objectPropertyInfo.PropertyName;
                    JsonLdProperty propertyProp = this.Dict[objectPropertyInfo.ElementId].JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == propName)).Value?.Properties?.First(p => p.Name == propName);
                    JsonLdElement refElt = this.Dict[resolvedReference].JsonLdElements.First().Value;

                    parsingErrorCollection.Notify(
                        "disallowedVersionReference",
                        elementId: objectPropertyInfo.ElementId,
                        propertyName: objectPropertyInfo.PropertyName,
                        referenceId: objectPropertyInfo.ReferencedElementId,
                        version: this.Dict[resolvedReference].DtdlVersion.ToString(),
                        versionRestriction: string.Join(" ,", objectPropertyInfo.AllowedVersions),
                        incidentProperty: propertyProp,
                        element: refElt);
                }

                if (objectPropertyInfo.ChildOf != null && this.Dict[resolvedReference].ChildOf != objectPropertyInfo.ChildOf)
                {
                    string propName = Dtmi.TryCreateDtmi(objectPropertyInfo.PropertyName, out Dtmi dtmi) ? ContextCollection.GetTermOrUri(dtmi) : objectPropertyInfo.PropertyName;
                    JsonLdProperty propertyProp = this.Dict[objectPropertyInfo.ElementId].JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == propName)).Value?.Properties?.First(p => p.Name == propName);

                    parsingErrorCollection.Notify(
                        "wrongParent",
                        elementId: objectPropertyInfo.ElementId,
                        propertyName: objectPropertyInfo.PropertyName,
                        referenceId: objectPropertyInfo.ReferencedElementId,
                        relation: this.Dict[resolvedReference].ChildOf != null ? $"child of {this.Dict[resolvedReference].ChildOf}" : "top-level element",
                        relationRestriction: objectPropertyInfo.ChildOf.ToString(),
                        incidentProperty: propertyProp);
                }

                string dictKey = null;
                if (objectPropertyInfo.KeyProperty != null)
                {
                    if (!this.Dict[resolvedReference].StringProperties.TryGetValue(objectPropertyInfo.KeyProperty, out dictKey) || dictKey == null)
                    {
                        if (resolvedReference.Tail == string.Empty)
                        {
                            string propName = Dtmi.TryCreateDtmi(objectPropertyInfo.PropertyName, out Dtmi dtmi) ? ContextCollection.GetTermOrUri(dtmi) : objectPropertyInfo.PropertyName;
                            JsonLdProperty propertyProp = this.Dict[objectPropertyInfo.ElementId].JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == propName)).Value?.Properties?.First(p => p.Name == propName);
                            JsonLdElement refElt = this.Dict[resolvedReference].JsonLdElements.First().Value;

                            parsingErrorCollection.Notify(
                                "missingDictKeyPropertyValue",
                                elementId: objectPropertyInfo.ElementId,
                                propertyName: objectPropertyInfo.PropertyName,
                                referenceId: objectPropertyInfo.ReferencedElementId,
                                nestedName: objectPropertyInfo.KeyProperty,
                                incidentProperty: propertyProp,
                                element: refElt);
                        }
                    }
                }

                this.TrySetObjectProperty(objectPropertyInfo.ElementId, objectPropertyInfo.Layer, objectPropertyInfo.PropertyName, objectPropertyInfo.JsonLdProperty, resolvedReference, objectPropertyInfo.KeyProperty, dictKey, parsingErrorCollection);
            }
        }

        /// <summary>
        /// Check to ensure any required properties are present.
        /// </summary>
        /// <param name="elementPropertyConstraints">A list of <see cref="ElementPropertyConstraint"/> to check against the elements in the model.</param>
        /// <param name="supplementalTypeCollection">A <see cref="SupplementalTypeCollection"/> containing information about supplemental types that may affect the constraints on element properties.</param>
        /// <param name="standardElementCollection">A <see cref="StandardElementCollection"/> for resolving aliases if <paramref name="preserveElementAliases"/> is false.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="preserveElementAliases">True if element aliases are being preserved in the model.</param>
        internal void CheckElementPropertyConstraints(List<ElementPropertyConstraint> elementPropertyConstraints, SupplementalTypeCollection supplementalTypeCollection, StandardElementCollection standardElementCollection, ParsingErrorCollection parsingErrorCollection, bool preserveElementAliases)
        {
            foreach (ElementPropertyConstraint elementPropertyConstraint in elementPropertyConstraints)
            {
                Dtmi elementId = preserveElementAliases ? elementPropertyConstraint.ElementId : standardElementCollection.ResolveAnyAliases(elementPropertyConstraint.ElementId);

                ITypeChecker typeChecker = this.Dict[elementId];
                if (elementPropertyConstraint.ValueConstraint.RequiredTypes != null && !elementPropertyConstraint.ValueConstraint.RequiredTypes.Any(t => typeChecker.DoesHaveType(t)))
                {
                    string propName = Dtmi.TryCreateDtmi(elementPropertyConstraint.PropertyName, out Dtmi dtmi) ? ContextCollection.GetTermOrUri(dtmi) : elementPropertyConstraint.PropertyName;
                    JsonLdProperty propertyProp = this.Dict[elementPropertyConstraint.ParentId].JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == propName)).Value?.Properties?.First(p => p.Name == propName);

                    parsingErrorCollection.Notify(
                        "notRequiredType",
                        elementId: elementPropertyConstraint.ParentId,
                        propertyName: elementPropertyConstraint.PropertyName,
                        referenceId: elementId,
                        typeRestriction: elementPropertyConstraint.ValueConstraint.RequiredTypesString,
                        incidentProperty: propertyProp);
                }

                if (elementPropertyConstraint.ValueConstraint.RequiredValues != null && !elementPropertyConstraint.ValueConstraint.RequiredValues.Contains(elementId))
                {
                    string propName = Dtmi.TryCreateDtmi(elementPropertyConstraint.PropertyName, out Dtmi dtmi) ? ContextCollection.GetTermOrUri(dtmi) : elementPropertyConstraint.PropertyName;
                    JsonLdProperty propertyProp = this.Dict[elementPropertyConstraint.ParentId].JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == propName)).Value?.Properties?.First(p => p.Name == propName);

                    parsingErrorCollection.Notify(
                        "notRequiredValue",
                        elementId: elementPropertyConstraint.ParentId,
                        propertyName: elementPropertyConstraint.PropertyName,
                        referenceId: elementId,
                        valueRestriction: elementPropertyConstraint.ValueConstraint.RequiredValuesString,
                        incidentProperty: propertyProp);
                }
            }
        }

        /// <summary>
        /// Apply any transformations that can only be performed after object properties have been assigned.
        /// </summary>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        internal void ApplyTransformations(ParsingErrorCollection parsingErrorCollection)
        {
            foreach (var element in this.Dict.Values)
            {
                element.ApplyTransformations(this, parsingErrorCollection);
            }
        }

        /// <summary>
        /// Check whether the element obeys restrictions that can only be assessed after object properties have been assigned.
        /// This includes any constraints imposed by sibling elements.
        /// </summary>
        /// <param name="elementPropertyConstraints">A list of <see cref="ElementPropertyConstraint"/> to check against the elements in the model.</param>
        /// <param name="supplementalTypeCollection">A <see cref="SupplementalTypeCollection"/> containing information about supplemental types that may affect the constraints on element properties.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        internal void CheckRestrictionsAndSiblingConstraints(List<ElementPropertyConstraint> elementPropertyConstraints, SupplementalTypeCollection supplementalTypeCollection, ParsingErrorCollection parsingErrorCollection)
        {
            Dictionary<(string, string, string, string), Dictionary<string, JsonLdElement>> referenceCounts = new Dictionary<(string, string, string, string), Dictionary<string, JsonLdElement>>();
            foreach (var element in this.Dict.Values)
            {
                element.CheckRestrictions(parsingErrorCollection);
                this.CheckParentConstraints(element, supplementalTypeCollection, parsingErrorCollection);
                this.CheckSiblingConstraints(element, supplementalTypeCollection, parsingErrorCollection, referenceCounts);
                this.CheckSupplementalPropertyConstraints(element, supplementalTypeCollection, parsingErrorCollection);
            }

            foreach (ElementPropertyConstraint elementPropertyConstraint in elementPropertyConstraints)
            {
                if (elementPropertyConstraint.ValueConstraint.SiblingClassOfPropertyId != null || elementPropertyConstraint.ValueConstraint.SiblingParentOfPropertyId != null)
                {
                    this.CheckValueConstraintOnSibling(elementPropertyConstraint, supplementalTypeCollection, parsingErrorCollection);
                }
            }
        }

        private class DtmiEqualityComparer : IEqualityComparer<Dtmi>
        {
            bool IEqualityComparer<Dtmi>.Equals(Dtmi id1, Dtmi id2)
            {
                return id1.AbsoluteUri == id2.AbsoluteUri;
            }

            int IEqualityComparer<Dtmi>.GetHashCode(Dtmi id)
            {
                return id.AbsoluteUri.GetHashCode();
            }
        }
    }
}
