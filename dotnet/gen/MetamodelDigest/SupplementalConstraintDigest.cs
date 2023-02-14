namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts supplemental constraint information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class SupplementalConstraintDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalConstraintDigest"/> class.
        /// </summary>
        /// <param name="supplementalConstraintObj">A <c>JObject</c> from the metamodel digest containing information about a supplemental constraint.</param>
        public SupplementalConstraintDigest(JObject supplementalConstraintObj)
        {
            this.PropertyName = ((JValue)supplementalConstraintObj["property"]).Value<string>();

            this.RequiredTypes = supplementalConstraintObj.TryGetValue("requiredTypes", out JToken requiredTypes) ? ((JArray)requiredTypes).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredTypeNames = supplementalConstraintObj.TryGetValue("requiredTypeNames", out JToken requiredTypeNames) ? ((JArray)requiredTypeNames).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredTypesString = supplementalConstraintObj.TryGetValue("requiredTypesString", out JToken requiredTypesString) ? ((JValue)requiredTypesString).Value<string>() : null;

            this.RequiredValues = supplementalConstraintObj.TryGetValue("requiredValues", out JToken requiredValues) ? ((JArray)requiredValues).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredValueNames = supplementalConstraintObj.TryGetValue("requiredValueNames", out JToken requiredValueNames) ? ((JArray)requiredValueNames).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredValuesString = supplementalConstraintObj.TryGetValue("requiredValuesString", out JToken requiredValuesString) ? ((JValue)requiredValuesString).Value<string>() : null;

            this.RequiredLiteral = supplementalConstraintObj.TryGetValue("requiredLiteral", out JToken requiredLiteral) ? ((JValue)requiredLiteral).ToObject<object>() : null;

            this.SiblingKeyPropertyName = supplementalConstraintObj.TryGetValue("siblingKey", out JToken siblingKey) ? ((JValue)siblingKey).Value<string>() : null;

            this.SiblingKeyrefProperty = supplementalConstraintObj.TryGetValue("siblingKeyref", out JToken siblingKeyref) ? ((JValue)siblingKeyref).Value<string>() : null;

            this.SiblingClassOfProperty = supplementalConstraintObj.TryGetValue("siblingClassOf", out JToken siblingClassOf) ? ((JValue)siblingClassOf).Value<string>() : null;

            this.SiblingParentOfProperty = supplementalConstraintObj.TryGetValue("siblingParentOf", out JToken siblingParentOf) ? ((JValue)siblingParentOf).Value<string>() : null;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets a list of type URIs, one of which must apply to the property.
        /// </summary>
        public List<string> RequiredTypes { get; }

        /// <summary>
        /// Gets a list of type names, one of which must apply to the property.
        /// </summary>
        public List<string> RequiredTypeNames { get; }

        /// <summary>
        /// Gets a string describing the required types for the property.
        /// </summary>
        public string RequiredTypesString { get; }

        /// <summary>
        /// Gets a list of value URIs, one of which must be the value of the property.
        /// </summary>
        public List<string> RequiredValues { get; }

        /// <summary>
        /// Gets a list of value names, one of which must be the value of the property.
        /// </summary>
        public List<string> RequiredValueNames { get; }

        /// <summary>
        /// Gets a string describing the required values for the property.
        /// </summary>
        public string RequiredValuesString { get; }

        /// <summary>
        /// Gets a literal value that is required, or null if no requirement.
        /// </summary>
        public object RequiredLiteral { get; }

        /// <summary>
        /// Gets the name of a property whose value acts as a key to identify a sibling element.
        /// </summary>
        public string SiblingKeyPropertyName { get; }

        /// <summary>
        /// Gets the identifier of a supplemental property whose value refers to a sibling element by the sibling's key value.
        /// </summary>
        public string SiblingKeyrefProperty { get; }

        /// <summary>
        /// Gets the ID of a supplemental property whose value is the ID of a supplemental property on a sibling element whose class constraint acts as a required type for the element.
        /// </summary>
        public string SiblingClassOfProperty { get; }

        /// <summary>
        /// Gets the ID of a supplemental property whose value is the ID of a supplemental property on a sibling element whose parent constraint acts as a required parent for the element.
        /// </summary>
        public string SiblingParentOfProperty { get; }
    }
}
