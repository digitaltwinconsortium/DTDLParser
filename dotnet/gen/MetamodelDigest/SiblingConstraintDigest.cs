namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts sibling constraint information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class SiblingConstraintDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiblingConstraintDigest"/> class.
        /// </summary>
        /// <param name="supplementalConstraintObj">A <c>JObject</c> from the metamodel digest containing information about a supplemental constraint.</param>
        public SiblingConstraintDigest(JObject supplementalConstraintObj)
        {
            this.KeyPropertyName = ((JValue)supplementalConstraintObj["key"]).Value<string>();

            this.KeyrefProperty = ((JValue)supplementalConstraintObj["keyref"]).Value<string>();

            this.RequiredTypes = supplementalConstraintObj.TryGetValue("requiredTypes", out JToken requiredTypes) ? ((JArray)requiredTypes).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredTypeNames = supplementalConstraintObj.TryGetValue("requiredTypeNames", out JToken requiredTypeNames) ? ((JArray)requiredTypeNames).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredTypesString = supplementalConstraintObj.TryGetValue("requiredTypesString", out JToken requiredTypesString) ? ((JValue)requiredTypesString).Value<string>() : null;

            this.DisallowedType = supplementalConstraintObj.TryGetValue("disallowedType", out JToken disallowedType) ? ((JValue)disallowedType).Value<string>() : null;

            this.DisallowedTypeName = supplementalConstraintObj.TryGetValue("disallowedTypeName", out JToken disallowedTypeName) ? ((JValue)disallowedTypeName).Value<string>() : null;

            this.UniqueReferenceId = supplementalConstraintObj.TryGetValue("uniqueReference", out JToken uniqueReference) ? ((JValue)uniqueReference).Value<string>() : null;

            this.SupplementalPropertyId = supplementalConstraintObj.TryGetValue("supplementalProperty", out JToken supplementalProperty) ? ((JValue)supplementalProperty).Value<string>() : null;
        }

        /// <summary>
        /// Gets the name of a property whose value acts as a key to identify a sibling element.
        /// </summary>
        public string KeyPropertyName { get; }

        /// <summary>
        /// Gets the identifier of a supplemental property whose value refers to a sibling element by the sibling's key value.
        /// </summary>
        public string KeyrefProperty { get; }

        /// <summary>
        /// Gets a list of type identifiers, one of which is required for the sibling element.
        /// </summary>
        public List<string> RequiredTypes { get; }

        /// <summary>
        /// Gets a list of type names, one of which is required for the sibling element.
        /// </summary>
        public List<string> RequiredTypeNames { get; }

        /// <summary>
        /// Gets a string describing the required types.
        /// </summary>
        public string RequiredTypesString { get; }

        /// <summary>
        /// Gets a type identifier that the sibling element is not allowed to have, or null to not specify a disallowed type.
        /// </summary>
        public string DisallowedType { get; }

        /// <summary>
        /// Gets a type name that the sibling element is not allowed to have.
        /// </summary>
        public string DisallowedTypeName { get; }

        /// <summary>
        /// Gets the identifier of a supplemental property whose value must be unique across all siblings that specify the same keyref value.
        /// </summary>
        public string UniqueReferenceId { get; }

        /// <summary>
        /// Gets the identifier of a supplemental property whose value is the ID of a supplemental property that must be present on a sibling element.
        /// </summary>
        public string SupplementalPropertyId { get; }
    }
}
