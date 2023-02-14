namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class <c>DtdlExtensionProperty</c> provides information about a property that can be applied to a DTDL element that has a supplemental type.
    /// </summary>
    internal class DtdlExtensionProperty
    {
        private Dictionary<string, JsonLdValue> constraintJsonLdValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="DtdlExtensionProperty"/> class.
        /// </summary>
        /// <param name="propertyElt">A <see cref="JsonLdElement"/> object representing the extension property.</param>
        /// <param name="parentId">The identifier of the parent element.</param>
        /// <param name="contextId">The context identifier for the extension that defines this property.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        internal DtdlExtensionProperty(JsonLdElement propertyElt, Dtmi parentId, Dtmi contextId, ParsingErrorCollection parsingErrorCollection)
        {
            this.constraintJsonLdValues = new Dictionary<string, JsonLdValue>();
            this.Path = null;
            this.Class = null;
            this.ChildOf = null;
            this.MaxCount = null;
            this.MinCount = null;
            this.Pattern = null;

            foreach (JsonLdProperty jsonLdProp in propertyElt.Properties)
            {
                switch (jsonLdProp.Name)
                {
                    case "sh:path":
                        this.Path = this.GetSingleStringValue(jsonLdProp.Values, jsonLdProp.Name, parentId, contextId, parsingErrorCollection);
                        break;
                    case "sh:class":
                        this.Class = this.GetSingleStringValue(jsonLdProp.Values, jsonLdProp.Name, parentId, contextId, parsingErrorCollection);
                        break;
                    case "dtmm:childOf":
                        this.ChildOf = this.GetSingleStringValue(jsonLdProp.Values, jsonLdProp.Name, parentId, contextId, parsingErrorCollection);
                        break;
                    case "sh:maxCount":
                        this.MaxCount = this.GetSingleIntegerValue(jsonLdProp.Values, jsonLdProp.Name, parentId, contextId, parsingErrorCollection);
                        break;
                    case "sh:minCount":
                        this.MinCount = this.GetSingleIntegerValue(jsonLdProp.Values, jsonLdProp.Name, parentId, contextId, parsingErrorCollection);
                        break;
                    case "sh:pattern":
                        this.Pattern = this.GetSingleStringValue(jsonLdProp.Values, jsonLdProp.Name, parentId, contextId, parsingErrorCollection);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the value of the 'sh:path' constraint.
        /// </summary>
        internal string Path { get; }

        /// <summary>
        /// Gets the value of the 'sh:class' constraint.
        /// </summary>
        internal string Class { get; }

        /// <summary>
        /// Gets the value of the 'dtmm:childOf' constraint.
        /// </summary>
        internal string ChildOf { get; }

        /// <summary>
        /// Gets the value of the 'sh:maxCount' constraint.
        /// </summary>
        internal int? MaxCount { get; }

        /// <summary>
        /// Gets the value of the 'sh:minCount' constraint.
        /// </summary>
        internal int? MinCount { get; }

        /// <summary>
        /// Gets the value of the 'sh:pattern' constraint.
        /// </summary>
        internal string Pattern { get; }

        /// <summary>
        /// Gets the source name and line number range corresponding to the constraint, if possible.
        /// </summary>
        /// <param name="constraintName">The name of the constraint.</param>
        /// <param name="name">The name of the JSON text containing the value.</param>
        /// <param name="startLine">The line number on which the value begins, one-indexed.</param>
        /// <param name="endLine">The line number on which the value ends, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForConstraint(string constraintName, out string name, out int startLine, out int endLine)
        {
            if (this.constraintJsonLdValues.TryGetValue(constraintName, out JsonLdValue constraintJsonLdValue))
            {
                return constraintJsonLdValue.TryGetSourceLocation(out name, out startLine, out endLine);
            }
            else
            {
                name = null;
                startLine = 0;
                endLine = 0;
                return false;
            }
        }

        private string GetSingleStringValue(JsonLdValueCollection valueCollection, string constraintName, Dtmi parentId, Dtmi contextId, ParsingErrorCollection parsingErrorCollection)
        {
            if (valueCollection.Count == 0)
            {
                parsingErrorCollection.Notify(
                    "stringConstraintNoValue",
                    incidentValues: valueCollection,
                    contextId: contextId,
                    elementId: parentId,
                    constraintName: constraintName);
                return null;
            }

            if (valueCollection.Count > 1)
            {
                parsingErrorCollection.Notify(
                    "stringConstraintMultipleValues",
                    incidentValues: valueCollection,
                    contextId: contextId,
                    elementId: parentId,
                    constraintName: constraintName);
                return null;
            }

            JsonLdValue jsonLdValue = valueCollection.Values.First();
            this.constraintJsonLdValues[constraintName] = jsonLdValue;

            if (jsonLdValue.ValueType != JsonLdValueType.String)
            {
                parsingErrorCollection.Notify(
                    "stringConstraintNotString",
                    incidentValues: valueCollection,
                    contextId: contextId,
                    elementId: parentId,
                    constraintName: constraintName);
                return null;
            }

            return jsonLdValue.StringValue;
        }

        private int? GetSingleIntegerValue(JsonLdValueCollection valueCollection, string constraintName, Dtmi parentId, Dtmi contextId, ParsingErrorCollection parsingErrorCollection)
        {
            if (valueCollection.Count == 0)
            {
                parsingErrorCollection.Notify(
                    "intConstraintNoValue",
                    incidentValues: valueCollection,
                    contextId: contextId,
                    elementId: parentId,
                    constraintName: constraintName);
                return null;
            }

            if (valueCollection.Count > 1)
            {
                parsingErrorCollection.Notify(
                    "intConstraintMultipleValues",
                    incidentValues: valueCollection,
                    contextId: contextId,
                    elementId: parentId,
                    constraintName: constraintName);
                return null;
            }

            JsonLdValue jsonLdValue = valueCollection.Values.First();
            this.constraintJsonLdValues[constraintName] = jsonLdValue;

            if (jsonLdValue.ValueType != JsonLdValueType.Number || jsonLdValue.NumberValue != (int)jsonLdValue.NumberValue)
            {
                parsingErrorCollection.Notify(
                    "intConstraintNotInteger",
                    incidentValues: valueCollection,
                    contextId: contextId,
                    elementId: parentId,
                    constraintName: constraintName);
                return null;
            }

            return (int)jsonLdValue.NumberValue;
        }
    }
}
