namespace DTDLParser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using DTDLParser;

    /// <summary>
    /// Class <c>DTSupplementalTypeInfo</c> provides information about a type that is not materialized as a C# class.
    /// </summary>
    public partial class DTSupplementalTypeInfo : IEquatable<DTSupplementalTypeInfo>, ITypeChecker
    {
        private readonly List<PropertyValueConstraint> propertyValueConstraints;
        private readonly List<SiblingConstraint> siblingConstraints;

        /// <summary>
        /// Initializes a new instance of the <see cref="DTSupplementalTypeInfo"/> class.
        /// </summary>
        /// <param name="extensionKind">The kind of the extension that this supplemental type inherits from.</param>
        /// <param name="contextId">The context ID of the extension in which the supplemental type is defined.</param>
        /// <param name="type">URI that represents the type.</param>
        /// <param name="isAbstract">True if the supplemental type is abstract.</param>
        /// <param name="isMergeable">Indicates whether elements with the supplemental type can have identifiers containing IRI fragments.</param>
        /// <param name="parentType">URI that represents the immediate parent in the type hierarchy.</param>
        internal DTSupplementalTypeInfo(DTExtensionKind extensionKind, Dtmi contextId, Dtmi type, bool isAbstract, bool isMergeable, Dtmi parentType)
            : this()
        {
            this.propertyValueConstraints = new List<PropertyValueConstraint>();
            this.siblingConstraints = new List<SiblingConstraint>();
            this.ExtensionKind = extensionKind;
            this.ContextId = contextId;
            this.Type = type;
            this.IsAbstract = isAbstract;
            this.ParentType = parentType;
            this.Properties = new Dictionary<string, DTSupplementalPropertyInfo>();
            this.AllowedCotypeVersions = new HashSet<int>();
            this.RequiredCocotypes = new HashSet<Dtmi>();
            this.DisallowedCocotypes = new HashSet<Dtmi>();
            this.IsMergeable = isMergeable;
        }

        /// <summary>
        /// Gets a value indicating whether the supplemental type is abstract.
        /// </summary>
        /// <value>True if the supplemental type is abstract.</value>
        public bool IsAbstract { get; }

        /// <summary>
        /// Gets the kind of extension that this supplemental type represents.
        /// </summary>
        /// <value>The kind of extension.</value>
        /// <remarks>
        /// If the supplemental type is intrinsic to the DTDL language instead of defined through an extension, the kind is <c>None</c>.
        /// </remarks>
        public DTExtensionKind ExtensionKind { get; }

        /// <summary>
        /// Gets the Dtmi of the context for the language extension in which this type is defined.
        /// </summary>
        /// <value>The context identifier for the definition.</value>
        /// <remarks>
        /// If the supplemental type is intrinsic to the DTDL language instead of defined through an extension, the ID is the DTDL languge context.
        /// </remarks>
        public Dtmi ContextId { get; }

        /// <summary>
        /// Gets the <c>Dtmi</c> that identifies the supplemental type.
        /// </summary>
        /// <value>The supplemental type identifier.</value>
        public Dtmi Type { get; }

        /// <summary>
        /// Gets the <c>Dtmi</c> of the supplemental type that is the parent of this type.
        /// </summary>
        /// <value>The parent supplemental type's identifier.</value>
        public Dtmi ParentType { get; }

        /// <summary>
        /// Gets a collection of <c>DTSupplementalPropertyInfo</c> objects, each of which provides information about a property that can be applied to a DTDL element that has this supplemental type.
        /// </summary>
        /// <value>
        /// A dictionary that maps each string-valued property name to a <c>DTSupplementalPropertyInfo</c> object that describes the property with the given name.
        /// </value>
        public IReadOnlyDictionary<string, DTSupplementalPropertyInfo> Properties { get; private set; }

        /// <summary>
        /// Gets or sets a collection of kinds of allowed cotypes for this supplemental type.
        /// </summary>
        internal HashSet<int> AllowedCotypeVersions { get; set; }

        /// <summary>
        /// Gets or sets a collection of co-cotypes, at least one of which is required for this supplemental type, or null if no co-cotypes are required.
        /// </summary>
        internal HashSet<Dtmi> RequiredCocotypes { get; set; }

        /// <summary>
        /// Gets or sets a collection of co-cotypes that are not permitted for this supplemental type, or null if no co-cotypes are disallowed.
        /// </summary>
        internal HashSet<Dtmi> DisallowedCocotypes { get; set; }

        /// <summary>
        /// Gets or sets an <c>DTSupplementalTypeInfo</c> that points to the parent of this supplemental type, if the parent is also supplemental.
        /// </summary>
        internal DTSupplementalTypeInfo ParentSupplementalType { get; set; }

        /// <summary>
        /// Gets a value indicating whether elements with this supplemental type can have identifiers containing IRI fragments.
        /// </summary>
        internal bool IsMergeable { get; }

        /// <summary>
        /// Determines whether two <c>DTSupplementalTypeInfo</c> objects are not equal.
        /// </summary>
        /// <param name="x">One <c>DTSupplementalTypeInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTSupplementalTypeInfo</c> object to compare to the first.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(DTSupplementalTypeInfo x, DTSupplementalTypeInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return !ReferenceEquals(null, y);
            }

            return !x.Equals(y);
        }

        /// <summary>
        /// Determines whether two <c>DTSupplementalTypeInfo</c> objects are equal.
        /// </summary>
        /// <param name="x">One <c>DTSupplementalTypeInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTSupplementalTypeInfo</c> object to compare to the first.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(DTSupplementalTypeInfo x, DTSupplementalTypeInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return ReferenceEquals(null, y);
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Determines whether the specified <c>DTSupplementalTypeInfo</c> is equal to the current object.
        /// </summary>
        /// <param name="other">The other <c>DTSupplementalTypeInfo</c> to compare against.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(DTSupplementalTypeInfo other)
        {
            return !ReferenceEquals(null, other)
                && this.ExtensionKind == other.ExtensionKind
                && this.ContextId == other.ContextId
                && this.Type == other.Type
                && this.IsAbstract == other.IsAbstract
                && this.ParentType == other.ParentType
                && Helpers.AreDictionariesEquatablyEqual(this.Properties, other.Properties);
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object other)
        {
            return this.Equals((DTSupplementalTypeInfo)other);
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            int hashCode = 0;

            unchecked
            {
                hashCode = (hashCode * 131) + this.ExtensionKind.GetHashCode();
                hashCode = (hashCode * 131) + this.ContextId.GetHashCode();
                hashCode = (hashCode * 131) + this.Type.GetHashCode();
                hashCode = (hashCode * 131) + this.IsAbstract.GetHashCode();
                hashCode = (hashCode * 131) + (this.ParentType == null ? 0 : this.ParentType.GetHashCode());
                hashCode = (hashCode * 131) + this.Properties.GetHashCode();
            }

            return hashCode;
        }

        /// <inheritdoc/>
        bool ITypeChecker.DoesHaveType(Dtmi typeId)
        {
            return this.Type == typeId || (this.ParentSupplementalType != null && ((ITypeChecker)this.ParentSupplementalType).DoesHaveType(typeId));
        }

        /// <summary>
        /// Attach any constraints to properties that are not properties of this supplemental type.
        /// </summary>
        /// <param name="constrainer">A <see cref="IConstrainer"/> to call back to add each constraint.</param>
        internal void AttachConstraints(IConstrainer constrainer)
        {
            foreach (PropertyValueConstraint propertyValueConstraint in this.propertyValueConstraints)
            {
                constrainer.AddPropertyValueConstraint(propertyValueConstraint.PropertyName, propertyValueConstraint.ValueConstraint);
            }

            foreach (SiblingConstraint siblingConstraint in this.siblingConstraints)
            {
                constrainer.AddSiblingConstraint(siblingConstraint);
            }

            if (this.ParentSupplementalType != null)
            {
                this.ParentSupplementalType.AttachConstraints(constrainer);
            }
        }

        /// <summary>
        /// Bind properties that are instance of another property.
        /// </summary>
        /// <param name="propertyInstanceBinder">A <see cref="IPropertyInstanceBinder"/> to call back to add each instance binding.</param>
        internal void BindInstanceProperties(IPropertyInstanceBinder propertyInstanceBinder)
        {
            foreach (KeyValuePair<string, DTSupplementalPropertyInfo> kvp in this.Properties)
            {
                if (kvp.Value.InstanceProperty != null)
                {
                    propertyInstanceBinder.AddInstanceProperty(kvp.Value.InstanceProperty, kvp.Key);
                }
            }

            if (this.ParentSupplementalType != null)
            {
                this.ParentSupplementalType.BindInstanceProperties(propertyInstanceBinder);
            }
        }

        /// <summary>
        /// Update the <paramref name="supplementalPropertySources"/> dictionary with records indicating that this supplemental type is the source for each of its properties.
        /// </summary>
        /// <param name="supplementalPropertySources">The dictionary to update.</param>
        /// <param name="conflictingProperty">The name of any conflicting property if some other type is already recorded as the source for the property.</param>
        /// <param name="conflictingType">The ID of another type that is already recorded as the source for one of this type's properties.</param>
        /// <returns>True if no property conflicts; false if some other type is already recorded as the source for one of this type's properties.</returns>
        internal bool TryRecordPropertySources(Dictionary<string, Dtmi> supplementalPropertySources, out string conflictingProperty, out Dtmi conflictingType)
        {
            foreach (string propName in this.Properties.Keys)
            {
                if (supplementalPropertySources.TryGetValue(propName, out Dtmi otherType) && otherType != this.Type)
                {
                    conflictingProperty = propName;
                    conflictingType = otherType;
                    return false;
                }
                else
                {
                    supplementalPropertySources[propName] = this.Type;
                }
            }

            conflictingProperty = null;
            conflictingType = null;
            return true;
        }

        /// <summary>
        /// Parse a property in a JSON token.
        /// </summary>
        /// <param name="model">The model to add the element to.</param>
        /// <param name="objectPropertyInfoList">List of object info structs for deferred assignments.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element, used for auto ID generation.</param>
        /// <param name="globalize">Treat all nested definitions as though defined globally.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="allowIdReferenceSyntax">Allow an object reference to be made using an object containing nothing but an @id property.</param>
        /// <param name="ignoreElementsWithAutoIDsAndDuplicateNames">Ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.</param>
        /// <param name="valueCollectionProp">The <see cref="JsonLdProperty"/> holding the <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="properties">A collection of properties to update with the property information.</param>
        /// <param name="jsonLdProperties">A dictionary that maps from property name to the <see cref="JsonLdProperty"/> that defines the property.</param>
        /// <param name="singularPropertyLayers">A dictionary from property name to the name of the layer in which the singular property is defined.</param>
        /// <param name="jsonLdElements">A dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the element.</param>
        /// <returns>True if the property name is recognized.</returns>
        internal bool TryParseProperty(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, string layer, Dtmi parentId, Dtmi definedIn, string propName, bool globalize, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames, JsonLdProperty valueCollectionProp, ref Dictionary<string, object> properties, Dictionary<string, JsonLdProperty> jsonLdProperties, Dictionary<string, string> singularPropertyLayers, Dictionary<string, JsonLdElement> jsonLdElements)
        {
            if (!this.Properties.TryGetValue(propName, out DTSupplementalPropertyInfo propertyInfo))
            {
                return this.ParentSupplementalType != null && this.ParentSupplementalType.TryParseProperty(model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, layer, parentId, definedIn, propName, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, valueCollectionProp, ref properties, jsonLdProperties, singularPropertyLayers, jsonLdElements);
            }

            if (jsonLdProperties.TryGetValue(propName, out JsonLdProperty extantProp))
            {
                parsingErrorCollection.Notify(
                    "duplicatePropertyName",
                    elementId: parentId,
                    propertyName: ContextCollection.GetTermOrUri(new Uri(propName)),
                    incidentProperty: valueCollectionProp,
                    extantProperty: extantProp,
                    layer: layer);
                return true;
            }

            jsonLdProperties[propName] = valueCollectionProp;

            bool regexIsDtmi = propertyInfo.Regex != null && propertyInfo.Regex.ToString().StartsWith("^dtmi:");

            if (propertyInfo.Type?.Scheme == "dtmi")
            {
                ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, layer, valueCollectionProp, parentId, definedIn, propName, propertyInfo.DtmiSegment, propertyInfo.IdRequired, propertyInfo.TypeRequired, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, propertyInfo.ChildOf, propertyInfo.MinCount ?? 0, propertyInfo.IsPlural, propertyInfo.ValueConstraint, propertyInfo.Type);
                return true;
            }

            if (propertyInfo.IsPlural)
            {
                if (propertyInfo.Type == null)
                {
                    List<Uri> newIdentifierList = ValueParser.ParsePluralIdentifierValueCollection(aggregateContext, parentId, propName, valueCollectionProp.Values, null, propertyInfo.Regex, layer, parsingErrorCollection, minCount: propertyInfo.MinCount ?? 0, requireDtmi: regexIsDtmi);
                    properties[propName] = (properties.TryGetValue(propName, out object propValue) && propValue != null && propValue is List<Uri> extantIdentifierList && extantIdentifierList.Any()) ?
                        Enumerable.Union(extantIdentifierList, newIdentifierList).ToList() :
                        newIdentifierList;
                }

                return true;
            }

            object newValue;
            string literalTypeString;

            if (propertyInfo.Type == null)
            {
                newValue = ValueParser.ParseSingularIdentifierValueCollection(aggregateContext, parentId, propName, valueCollectionProp.Values, null, propertyInfo.Regex, layer, parsingErrorCollection, isOptional: propertyInfo.IsOptional, requireDtmi: regexIsDtmi);
                literalTypeString = "Identifier";
            }
            else if (propertyInfo.Type.Scheme == "http")
            {
                switch (propertyInfo.Type.Fragment)
                {
                    case "#langString":
                        newValue = ValueParser.ParseLangStringValueCollection(aggregateContext, parentId, propName, valueCollectionProp.Values, propertyInfo.DefaultLanguage, null, null, layer, parsingErrorCollection);
                        literalTypeString = "LangString";
                        break;
                    case "#boolean":
                        newValue = ValueParser.ParseSingularBooleanValueCollection(aggregateContext, parentId, propName, valueCollectionProp.Values, layer, parsingErrorCollection, isOptional: propertyInfo.IsOptional);
                        literalTypeString = "Boolean";
                        break;
                    case "#decimal":
                        newValue = ValueParser.ParseSingularNumericValueCollection(aggregateContext, parentId, propName, valueCollectionProp.Values, null, null, layer, parsingErrorCollection, isOptional: propertyInfo.IsOptional);
                        literalTypeString = "Numeric";
                        break;
                    case "#integer":
                        newValue = ValueParser.ParseSingularIntegerValueCollection(aggregateContext, parentId, propName, valueCollectionProp.Values, null, null, layer, parsingErrorCollection, isOptional: propertyInfo.IsOptional);
                        literalTypeString = "Integer";
                        break;
                    case "#string":
                        newValue = ValueParser.ParseSingularStringValueCollection(aggregateContext, parentId, propName, valueCollectionProp.Values, null, null, layer, parsingErrorCollection, isOptional: propertyInfo.IsOptional);
                        literalTypeString = "String";
                        break;
                    case "#JSON":
                        using (JsonDocument propDoc = JsonDocument.Parse(valueCollectionProp.Values.GetJsonText()))
                        {
                            newValue = propDoc.RootElement.Clone();
                            literalTypeString = "Json";
                        }

                        break;
                    default:
                        return true;
                }

                if (propertyInfo.RequiredLiteral != null && !propertyInfo.RequiredLiteral.Equals(newValue))
                {
                    parsingErrorCollection.Notify(
                        $"notRequired{literalTypeString}Value",
                        elementId: parentId,
                        propertyName: ContextCollection.GetTermOrUri(new Uri(propName)),
                        valueRestriction: propertyInfo.RequiredLiteral.ToString(),
                        incidentProperty: valueCollectionProp,
                        layer: layer);

                    return true;
                }
            }
            else
            {
                return true;
            }

            if (properties.TryGetValue(propName, out object propertyObj) && propertyObj != null)
            {
                if (!Helpers.AreObjectsLiteralEqual(propertyObj, newValue))
                {
                    parsingErrorCollection.Notify(
                        $"inconsistent{literalTypeString}Values",
                        elementId: parentId,
                        propertyName: ContextCollection.GetTermOrUri(new Uri(propName)),
                        propertyValue: newValue.ToString(),
                        valueRestriction: propertyObj.ToString(),
                        incidentProperty: valueCollectionProp,
                        extantProperty: extantProp,
                        layer: layer);
                }
            }
            else
            {
                properties[propName] = newValue;
                singularPropertyLayers[propName] = layer;
            }

            return true;
        }

        /// <summary>
        /// Check that all non-optional properties have been set.
        /// </summary>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which missing-required-property parsing errors are added as appropriate.</param>
        /// <param name="elementId">The identifier of the element.</param>
        /// <param name="elt">A <see cref="JsonLdElement"/> representing the source of the element.</param>
        /// <param name="jsonLdProperties">A dictionary that maps from property name to the <see cref="JsonLdProperty"/> that defines the property.</param>
        internal void CheckForRequiredProperties(ParsingErrorCollection parsingErrorCollection, Dtmi elementId, JsonLdElement elt, Dictionary<string, JsonLdProperty> jsonLdProperties)
        {
            foreach (KeyValuePair<string, DTSupplementalPropertyInfo> kvp in this.Properties)
            {
                string propertyTerm = ContextCollection.GetTermOrUri(new Uri(kvp.Key));

                if (!kvp.Value.IsOptional && !jsonLdProperties.ContainsKey(kvp.Key) && !jsonLdProperties.ContainsKey(propertyTerm))
                {
                    string validationCode =
                        kvp.Value.Type == null ? "missingSupplementalIdentifierProperty" :
                        kvp.Value.Type.Scheme == "http" ? "missingSupplementalLiteralProperty" :
                        kvp.Value.Type.Scheme == "dtmi" ? "missingSupplementalObjectProperty" :
                        null;

                    parsingErrorCollection.Quit(
                        validationCode,
                        elementId: elementId,
                        propertyName: ContextCollection.GetTermOrUri(new Uri(kvp.Key)),
                        cotype: ContextCollection.GetTermOrUri(this.Type),
                        element: elt);
                }
            }
        }

        /// <summary>
        /// Try to set an object property with a given <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="elementId">The identifier of the element.</param>
        /// <param name="propertyName">The name of the property whose value to set if the property is recognized.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="key">The key for dictionary properties.</param>
        /// <param name="layer">The name of the layer being parsed.</param>
        /// <param name="propertyProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="properties">A collection of properties to update with the property information.</param>
        /// <param name="singularPropertyLayers">A dictionary from property name to the name of the layer in which the singular property is defined.</param>
        /// <param name="jsonLdElements">A dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the element.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if the property name is recognized.</returns>
        internal bool TrySetObjectProperty(Dtmi elementId, string propertyName, object value, string key, string layer, JsonLdProperty propertyProp, ref Dictionary<string, object> properties, Dictionary<string, string> singularPropertyLayers, Dictionary<string, JsonLdElement> jsonLdElements, ParsingErrorCollection parsingErrorCollection)
        {
            if (this.ParentSupplementalType != null && this.ParentSupplementalType.TrySetObjectProperty(elementId, propertyName, value, key, layer, propertyProp, ref properties, singularPropertyLayers, jsonLdElements, parsingErrorCollection))
            {
                return true;
            }

            DTSupplementalPropertyInfo propertyInfo;
            if (!this.Properties.TryGetValue(propertyName, out propertyInfo) || (propertyInfo.Type.Scheme != "dtmi" && propertyInfo.Type.Scheme != "urn"))
            {
                return false;
            }

            if (propertyInfo.DictionaryKey != null)
            {
                if (!properties.ContainsKey(propertyName) || properties[propertyName] == null)
                {
                    properties[propertyName] = new Dictionary<string, object>();
                }

                if (key != null)
                {
                    ((IDictionary<string, object>)properties[propertyName])[key] = value;
                }
            }
            else if (propertyInfo.IsPlural)
            {
                if (!properties.ContainsKey(propertyName) || properties[propertyName] == null)
                {
                    properties[propertyName] = new List<object>();
                }

                IList<object> objectList = (IList<object>)properties[propertyName];
                if (!objectList.Contains(value))
                {
                    objectList.Add(value);
                }
            }
            else
            {
                if (properties.TryGetValue(propertyName, out object propertyObj) && propertyObj != null)
                {
                    if (Helpers.GetObjectId(propertyObj) != Helpers.GetObjectId(value))
                    {
                        string term = ContextCollection.GetTermOrUri(new Uri(propertyName));
                        JsonLdProperty extantProp = jsonLdElements[singularPropertyLayers[propertyName]].Properties.FirstOrDefault(p => p.Name == propertyName || p.Name == term);

                        parsingErrorCollection.Notify(
                            "inconsistentObjectValues",
                            elementId: elementId,
                            propertyName: term,
                            propertyValue: Helpers.GetObjectId(value).ToString(),
                            valueRestriction: Helpers.GetObjectId(propertyObj).ToString(),
                            incidentProperty: propertyProp,
                            extantProperty: extantProp,
                            layer: layer);
                    }
                }
                else
                {
                    properties[propertyName] = value;
                    singularPropertyLayers[propertyName] = layer;
                }
            }

            return true;
        }

        /// <summary>
        /// Adds a property to this supplemental type.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyTypeUri">URI that defines the type of the property.</param>
        /// <param name="maxCount">The maximum count of permitted values of the property.</param>
        /// <param name="minCount">The minimum count of permitted values of the property.</param>
        /// <param name="regex">A regex that constrains the permissible values, or null if no pattern constraint.</param>
        /// <param name="isPlural">True if the property is plural.</param>
        /// <param name="isOptional">True if the property is optional.</param>
        /// <param name="defaultLanguage">The default language code for a language-tagged string literal property.</param>
        /// <param name="dtmiSeg">A DTMI segment identifier, used for auto ID generation.</param>
        /// <param name="dictionaryKey">The name of the child property that acts as a dictionary key, or null if this property is not expressed as a dictionary.</param>
        /// <param name="idRequired">A boolean value indicating whether an @id must be present.</param>
        /// <param name="typeRequired">A boolean value indicating whether a @type must be present.</param>
        /// <param name="childOf">The identifier of a parent element in which the value of this property must be defined.</param>
        /// <param name="instanceProperty">The name of a property of which this property's value must be an instance.</param>
        /// <param name="requiredValues">A list of values that are permitted, or null if no requirement.</param>
        /// <param name="requiredValuesString">A string describing the values that are permitted.</param>
        /// <param name="requiredLiteral">A literal value that is required, or null if no requirement.</param>
        internal void AddProperty(string propertyName, Uri propertyTypeUri, int? maxCount, int? minCount, Regex regex, bool isPlural, bool isOptional, string defaultLanguage, string dtmiSeg, string dictionaryKey, bool idRequired, bool typeRequired, Dtmi childOf, string instanceProperty, List<Dtmi> requiredValues, string requiredValuesString, object requiredLiteral)
        {
            ((Dictionary<string, DTSupplementalPropertyInfo>)this.Properties)[propertyName] = new DTSupplementalPropertyInfo(propertyTypeUri, maxCount, minCount, regex, isPlural, isOptional, defaultLanguage, dtmiSeg, dictionaryKey, idRequired, typeRequired, childOf, instanceProperty, requiredValues, requiredValuesString, requiredLiteral);
        }

        /// <summary>
        /// Adds a type constraint to this supplemantal type.
        /// </summary>
        /// <param name="propertyName">Name of the property whose type to constrain.</param>
        /// <param name="valueConstraint">A <see cref="ValueConstraint"/> for values of this property.</param>
        internal void AddPropertyValueConstraint(string propertyName, ValueConstraint valueConstraint)
        {
            this.propertyValueConstraints.Add(new PropertyValueConstraint() { PropertyName = propertyName, ValueConstraint = valueConstraint });
        }

        /// <summary>
        /// Adds a type constraint to this supplemantal type.
        /// </summary>
        /// <param name="siblingConstraint">A <see cref="SiblingConstraint"/> for elements of this property.</param>
        internal void AddSiblingConstraint(SiblingConstraint siblingConstraint)
        {
            this.siblingConstraints.Add(siblingConstraint);
        }

        /// <summary>
        /// Check whether an element with this supplemental type obeys restrictions that can only be assessed after object properties have been assigned.
        /// </summary>
        /// <param name="properties">A collection of properties containing the property information for the element.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="elementId">The identifier of the element.</param>
        /// <param name="jsonLdElements">A dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the element.</param>
        internal void CheckRestrictions(Dictionary<string, object> properties, ParsingErrorCollection parsingErrorCollection, Dtmi elementId, Dictionary<string, JsonLdElement> jsonLdElements)
        {
            foreach (KeyValuePair<string, object> property in properties)
            {
                if (!this.Properties.TryGetValue(property.Key, out DTSupplementalPropertyInfo propertyInfo))
                {
                    if (this.ParentSupplementalType != null)
                    {
                        this.ParentSupplementalType.CheckRestrictions(properties, parsingErrorCollection, elementId, jsonLdElements);
                    }

                    continue;
                }

                if (propertyInfo.IsPlural && propertyInfo.MaxCount != null)
                {
                    int valueCount = 0;
                    string validationCode = null;

                    if (propertyInfo.Type == null)
                    {
                        List<Uri> identifierList = property.Value as List<Uri>;
                        valueCount = identifierList.Count;
                        validationCode = "identifierCountAboveMax";
                    }
                    else if (propertyInfo.Type.Scheme == "dtmi")
                    {
                        if (propertyInfo.DictionaryKey != null)
                        {
                            Dictionary<string, object> objectDict = property.Value as Dictionary<string, object>;
                            valueCount = objectDict.Count;
                        }
                        else
                        {
                            List<object> objectList = property.Value as List<object>;
                            valueCount = objectList.Count;
                        }

                        validationCode = "objectCountAboveMax";
                    }

                    if (valueCount > propertyInfo.MaxCount)
                    {
                        string term = ContextCollection.GetTermOrUri(new Uri(property.Key));
                        JsonLdElement elt = jsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == property.Key || p.Name == term)).Value;

                        parsingErrorCollection.Notify(
                            validationCode,
                            elementId: elementId,
                            propertyName: term,
                            observedCount: valueCount,
                            expectedCount: propertyInfo.MaxCount,
                            incidentProperty: elt?.Properties?.FirstOrDefault(p => p.Name == property.Key));
                    }
                }
            }
        }

        private static void ParseValueCollection(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, string layer, JsonLdProperty valueCollectionProp, Dtmi parentId, Dtmi definedIn, string propName, string dtmiSeg, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames, Dtmi childOf, int minCount, bool isPlural, ValueConstraint valueConstraint, Uri propertyTypeUri)
        {
            int valueCount = 0;

            foreach (JsonLdValue value in valueCollectionProp.Values.Values)
            {
                if (!isPlural && valueCount > 0)
                {
                    string term = ContextCollection.GetTermOrUri(new Uri(propName));
                    parsingErrorCollection.Notify(
                        "objectMultipleValues",
                        elementId: parentId,
                        propertyName: term,
                        incidentValues: valueCollectionProp.Values,
                        layer: layer);
                    return;
                }

                switch (value.ValueType)
                {
                    case JsonLdValueType.String:
                        if (parentId != null)
                        {
                            if (aggregateContext.TryCreateDtmi(value.StringValue, out Dtmi elementId))
                            {
                                objectPropertyInfoList.Add(new ParsedObjectPropertyInfo() { ElementId = parentId, PropertyName = propName, Layer = layer, JsonLdProperty = valueCollectionProp, ReferencedElementId = elementId, KeyProperty = null, ExpectedKinds = null, AllowedVersions = null, ChildOf = childOf, BadTypeCauseFormat = null, BadTypeLocatedCauseFormat = null, BadTypeActionFormat = null });

                                if (valueConstraint != null && elementPropertyConstraints != null)
                                {
                                    elementPropertyConstraints.Add(new ElementPropertyConstraint() { ParentId = parentId, PropertyName = propName, ElementId = elementId, ValueConstraint = valueConstraint });
                                }

                                ++valueCount;
                            }
                            else
                            {
                                parsingErrorCollection.Notify(
                                    "badDtmiOrTerm",
                                    elementId: parentId,
                                    propertyName: ContextCollection.GetTermOrUri(new Uri(propName)),
                                    propertyValue: value.StringValue,
                                    incidentProperty: valueCollectionProp,
                                    incidentValue: value,
                                    layer: layer);
                            }
                        }

                        break;
                    case JsonLdValueType.Element:
                        if (propertyTypeUri is Dtmi propertyTypeDtmi && aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(propertyTypeDtmi, out DTSupplementalTypeInfo supplementalTypeInfo))
                        {
                            if (TryParseExtensionElement(supplementalTypeInfo.ExtensionKind, model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, value.ElementValue, layer, parentId, definedIn, propName, valueCollectionProp, dtmiSeg, idRequired, typeRequired, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, propertyTypeUri.AbsoluteUri))
                            {
                                ++valueCount;
                            }
                        }
                        else
                        {
                            parsingErrorCollection.Notify(
                                "refObjectNotAllowed",
                                elementId: parentId,
                                propertyName: ContextCollection.GetTermOrUri(new Uri(propName)),
                                incidentProperty: valueCollectionProp,
                                incidentValue: value,
                                layer: layer);
                        }

                        break;
                    default:
                        {
                            parsingErrorCollection.Notify(
                                "refNotStringOrObject",
                                elementId: parentId,
                                propertyName: ContextCollection.GetTermOrUri(new Uri(propName)),
                                incidentProperty: valueCollectionProp,
                                incidentValue: value,
                                layer: layer);
                        }

                        break;
                }
            }

            if (valueCount < minCount)
            {
                parsingErrorCollection.Notify(
                    "objectCountBelowMin",
                    elementId: parentId,
                    propertyName: ContextCollection.GetTermOrUri(new Uri(propName)),
                    observedCount: valueCount,
                    expectedCount: minCount,
                    incidentProperty: valueCollectionProp,
                    layer: layer);
            }
        }

        private struct PropertyValueConstraint
        {
            public string PropertyName { get; set; }

            public ValueConstraint ValueConstraint { get; set; }
        }
    }
}
