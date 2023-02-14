namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The <c>DtdlExtensionMetamodel</c> class encapsulates an element in the metamodel section of a DTDL extension JSON object.
    /// </summary>
    internal class DtdlExtensionMetamodelElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtdlExtensionMetamodelElement"/> class.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the extension metamodel element.</param>
        /// <param name="metamodelElt">A <see cref="JsonLdElement"/> object representing the extension metamodel element.</param>
        /// <param name="extensionId">The context identifier for the extension that defines this metamodel element.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        internal DtdlExtensionMetamodelElement(AggregateContext aggregateContext, JsonLdElement metamodelElt, Dtmi extensionId, ParsingErrorCollection parsingErrorCollection)
        {
            this.Id = IdValidator.ParseIdProperty(aggregateContext, metamodelElt, layer: null, parentId: null, propName: null, dtmiSeg: null, idRequired: true, allowReservedIds: true, parsingErrorCollection);

            this.SetTypes(metamodelElt, extensionId, parsingErrorCollection);

            this.SubClassOf = null;
            this.AllowedCotypes = null;
            this.RequiredCocotypes = null;
            this.DisallowedCocotypes = null;
            this.Properties = new Dictionary<string, DtdlExtensionProperty>();
            foreach (JsonLdProperty jsonLdProp in metamodelElt.Properties)
            {
                switch (jsonLdProp.Name)
                {
                    case "rdfs:subClassOf":
                        this.SubClassOf = this.GetSingleStringValue(jsonLdProp.Values, jsonLdProp.Name, extensionId, parsingErrorCollection);
                        break;
                    case "sh:or":
                        this.AllowedCotypes = this.GetShaclClassConstraintValues(jsonLdProp.Values, jsonLdProp.Name, extensionId, parsingErrorCollection);
                        break;
                    case "sh:and":
                        this.RequiredCocotypes = this.GetShaclClassConstraintValues(jsonLdProp.Values, jsonLdProp.Name, extensionId, parsingErrorCollection);
                        break;
                    case "sh:not":
                        this.DisallowedCocotypes = this.GetShaclClassConstraintValues(jsonLdProp.Values, jsonLdProp.Name, extensionId, parsingErrorCollection);
                        break;
                    case "sh:property":
                        this.Properties = this.GetExtensionProperties(jsonLdProp.Values, extensionId, parsingErrorCollection);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the identifier of the extension element as a <see cref="Dtmi"/> object.
        /// </summary>
        internal Dtmi Id { get; }

        /// <summary>
        /// Gets a value indicating whether the element has a type of dtmm:Abstract.
        /// </summary>
        internal bool IsAbstract { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the element has a type of dtmm:Mergeable.
        /// </summary>
        internal bool IsMergeable { get; private set; }

        /// <summary>
        /// Gets the value of the 'rdfs:subClassOf' property.
        /// </summary>
        internal JsonLdValue SubClassOf { get; }

        /// <summary>
        /// Gets a list of the 'sh:class' constraints from the 'sh:or' property.
        /// </summary>
        internal List<JsonLdValue> AllowedCotypes { get; }

        /// <summary>
        /// Gets a list of the 'sh:class' constraints from the 'sh:and' property.
        /// </summary>
        internal List<JsonLdValue> RequiredCocotypes { get; }

        /// <summary>
        /// Gets a list of the 'sh:class' constraints from the 'sh:not' property.
        /// </summary>
        internal List<JsonLdValue> DisallowedCocotypes { get; }

        /// <summary>
        /// Gets a dictionary that maps from 'sh:path' values to <see cref="DtdlExtensionProperty"/> objects representing 'sh:property' elements.
        /// </summary>
        internal Dictionary<string, DtdlExtensionProperty> Properties { get; }

        private void SetTypes(JsonLdElement metamodelElt, Dtmi extensionId, ParsingErrorCollection parsingErrorCollection)
        {
            if (metamodelElt.Types == null)
            {
                parsingErrorCollection.Quit(
                    "missingTypeInfo",
                    element: metamodelElt,
                    contextId: extensionId,
                    elementId: this.Id);
            }

            bool isClass = false;
            bool isNodeShape = false;
            this.IsAbstract = false;
            this.IsMergeable = false;
            foreach (string typeString in metamodelElt.Types)
            {
                switch (typeString)
                {
                    case "rdfs:Class":
                        isClass = true;
                        break;
                    case "sh:NodeShape":
                        isNodeShape = true;
                        break;
                    case "dtmm:Abstract":
                        this.IsAbstract = true;
                        break;
                    case "dtmm:Mergeable":
                        this.IsMergeable = true;
                        break;
                }
            }

            if (!isClass || !isNodeShape)
            {
                parsingErrorCollection.Quit(
                    "missingEssentialTypes",
                    element: metamodelElt,
                    contextId: extensionId,
                    elementId: this.Id);
            }
        }

        private JsonLdValue GetSingleStringValue(JsonLdValueCollection valueCollection, string constraintName, Dtmi extensionId, ParsingErrorCollection parsingErrorCollection)
        {
            if (valueCollection.Count == 0)
            {
                parsingErrorCollection.Notify(
                    "stringConstraintNoValue",
                    incidentValues: valueCollection,
                    contextId: extensionId,
                    elementId: this.Id,
                    constraintName: constraintName);
                return null;
            }

            if (valueCollection.Count > 1)
            {
                parsingErrorCollection.Notify(
                    "stringConstraintMultipleValues",
                    incidentValues: valueCollection,
                    contextId: extensionId,
                    elementId: this.Id,
                    constraintName: constraintName);
                return null;
            }

            JsonLdValue jsonLdValue = valueCollection.Values.First();

            if (jsonLdValue.ValueType != JsonLdValueType.String)
            {
                parsingErrorCollection.Notify(
                    "stringConstraintNotString",
                    incidentValues: valueCollection,
                    contextId: extensionId,
                    elementId: this.Id,
                    constraintName: constraintName);
                return null;
            }

            return jsonLdValue;
        }

        private List<JsonLdValue> GetShaclClassConstraintValues(JsonLdValueCollection valueCollection, string constraintName, Dtmi extensionId, ParsingErrorCollection parsingErrorCollection)
        {
            List<JsonLdValue> types = new List<JsonLdValue>();

            foreach (JsonLdValue jsonLdValue in valueCollection.Values)
            {
                if (jsonLdValue.ValueType != JsonLdValueType.Element)
                {
                    parsingErrorCollection.Notify(
                        "constraintNotObject",
                        incidentValue: jsonLdValue,
                        contextId: extensionId,
                        elementId: this.Id,
                        constraintName: constraintName);
                    return null;
                }

                JsonLdProperty classProperty = jsonLdValue.ElementValue.Properties.FirstOrDefault(p => p.Name == "sh:class");
                if (classProperty == null)
                {
                    parsingErrorCollection.Notify(
                        "constraintMissingClass",
                        incidentValue: jsonLdValue,
                        contextId: extensionId,
                        elementId: this.Id,
                        constraintName: constraintName);
                    return null;
                }

                types.Add(this.GetSingleStringValue(classProperty.Values, "sh:class", extensionId, parsingErrorCollection));
            }

            return types;
        }

        private Dictionary<string, DtdlExtensionProperty> GetExtensionProperties(JsonLdValueCollection valueCollection, Dtmi extensionId, ParsingErrorCollection parsingErrorCollection)
        {
            Dictionary<string, JsonLdValue> jsonLdValues = new Dictionary<string, JsonLdValue>();
            Dictionary<string, DtdlExtensionProperty> extensionProperties = new Dictionary<string, DtdlExtensionProperty>();

            foreach (JsonLdValue jsonLdValue in valueCollection.Values)
            {
                if (jsonLdValue.ValueType != JsonLdValueType.Element)
                {
                    parsingErrorCollection.Notify(
                        "propertyElementNotObject",
                        incidentValue: jsonLdValue,
                        contextId: extensionId,
                        elementId: this.Id);
                    continue;
                }

                DtdlExtensionProperty extensionProp = new DtdlExtensionProperty(jsonLdValue.ElementValue, this.Id, extensionId, parsingErrorCollection);
                if (extensionProp.Path == null)
                {
                    parsingErrorCollection.Notify(
                        "pathNotString",
                        incidentValue: jsonLdValue,
                        contextId: extensionId,
                        elementId: this.Id);
                    continue;
                }

                if (jsonLdValues.TryGetValue(extensionProp.Path, out JsonLdValue extantJsonLdValue))
                {
                    parsingErrorCollection.Notify(
                        "pathNotUnique",
                        incidentValue: jsonLdValue,
                        extantValue: extantJsonLdValue,
                        contextId: extensionId,
                        elementId: this.Id,
                        constraintValue: extensionProp.Path);
                    continue;
                }

                jsonLdValues[extensionProp.Path] = jsonLdValue;
                extensionProperties[extensionProp.Path] = extensionProp;
            }

            return extensionProperties;
        }
    }
}
