namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts supplemental property information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class SupplementalPropertyDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalPropertyDigest"/> class.
        /// </summary>
        /// <param name="supplementalPropertyObj">A <c>JObject</c> from the metamodel digest containing information about a supplemental property.</param>
        public SupplementalPropertyDigest(JObject supplementalPropertyObj)
        {
            this.PropertyName = ((JValue)supplementalPropertyObj["propertyName"]).Value<string>();

            this.TypeUri = supplementalPropertyObj.TryGetValue("type", out JToken classValue) ? ((JValue)classValue).Value<string>() : null;

            this.TypeName = supplementalPropertyObj.TryGetValue("typeName", out JToken typeTermValue) ? ((JValue)typeTermValue).Value<string>() : null;

            this.MaxCount = supplementalPropertyObj.TryGetValue("maxCount", out JToken maxCount) ? ((JValue)maxCount).Value<int?>() : null;

            this.MinCount = supplementalPropertyObj.TryGetValue("minCount", out JToken minCount) ? ((JValue)minCount).Value<int?>() : null;

            this.Pattern = supplementalPropertyObj.TryGetValue("pattern", out JToken pattern) ? ((JValue)pattern).Value<string>() : null;

            this.IsPlural = ((JValue)supplementalPropertyObj["plural"]).Value<bool>();

            this.IsOptional = ((JValue)supplementalPropertyObj["optional"]).Value<bool>();

            this.DefaultLanguage = supplementalPropertyObj.TryGetValue("defaultLanguage", out JToken defaultLanguage) ? ((JValue)defaultLanguage).Value<string>() : null;

            this.DtmiSegment = supplementalPropertyObj.TryGetValue("dtmiSegment", out JToken dtmiSegment) ? ((JValue)dtmiSegment).Value<string>() : null;

            this.DictionaryKey = supplementalPropertyObj.TryGetValue("dictionaryKey", out JToken dictionaryKey) ? ((JValue)dictionaryKey).Value<string>() : null;

            this.IdRequired = ((JValue)supplementalPropertyObj["idRequired"]).Value<bool>();

            this.TypeRequired = ((JValue)supplementalPropertyObj["typeRequired"]).Value<bool>();

            this.IsDeprecated = ((JValue)supplementalPropertyObj["deprecated"]).Value<bool>();

            this.ChildOf = supplementalPropertyObj.TryGetValue("childOf", out JToken childOf) ? ((JValue)childOf).Value<string>() : null;

            this.InstanceProperty = supplementalPropertyObj.TryGetValue("instanceProperty", out JToken instanceProperty) ? ((JValue)instanceProperty).Value<string>() : null;

            this.Description = ((JObject)supplementalPropertyObj["description"]).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>());

            this.RequiredValues = supplementalPropertyObj.TryGetValue("requiredValues", out JToken requiredValues) ? ((JArray)requiredValues).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredValueNames = supplementalPropertyObj.TryGetValue("requiredValueNames", out JToken requiredValueNames) ? ((JArray)requiredValueNames).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.RequiredValuesString = supplementalPropertyObj.TryGetValue("requiredValuesString", out JToken requiredValuesString) ? ((JValue)requiredValuesString).Value<string>() : null;

            this.RequiredLiteral = supplementalPropertyObj.TryGetValue("requiredLiteral", out JToken requiredLiteral) ? ((JValue)requiredLiteral).ToObject<object>() : null;
        }

        /// <summary>
        /// Gets the JSON-LD term for the property.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the type URI for the property.
        /// </summary>
        public string TypeUri { get; }

        /// <summary>
        /// Gets the type term for the property.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Gets the maximum count of permitted values of the property.
        /// </summary>
        public int? MaxCount { get; }

        /// <summary>
        /// Gets the minimum count of permitted values of the property.
        /// </summary>
        public int? MinCount { get; }

        /// <summary>
        /// Gets a regex that constrains the permissible values, or null if no pattern constraint.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Gets a value indicating whether the property is plural.
        /// </summary>
        public bool IsPlural { get; }

        /// <summary>
        /// Gets a value indicating whether the property is optional.
        /// </summary>
        public bool IsOptional { get; }

        /// <summary>
        /// Gets the default language code for a language-tagged string literal property.
        /// </summary>
        public string DefaultLanguage { get; }

        /// <summary>
        /// Gets a DTMI segment identifier, used for auto ID generation.
        /// </summary>
        public string DtmiSegment { get; }

        /// <summary>
        /// Gets the name of a property of a child element that is used for the dictionary key of the property.
        /// </summary>
        public string DictionaryKey { get; }

        /// <summary>
        /// Gets a value indicating whether an @id must be present.
        /// </summary>
        public bool IdRequired { get; }

        /// <summary>
        /// Gets a value indicating whether a @type must be present.
        /// </summary>
        public bool TypeRequired { get; }

        /// <summary>
        /// Gets a value indicating whether the property is deprecated.
        /// </summary>
        public bool IsDeprecated { get; }

        /// <summary>
        /// Gets or sets the identifier of a parent element in which the value of this property must be defined.
        /// </summary>
        public string ChildOf { get; set; }

        /// <summary>
        /// Gets the name of a property of which this property's value must be an instance.
        /// </summary>
        public string InstanceProperty { get; }

        /// <summary>
        /// Gets a language map description of the property.
        /// </summary>
        public Dictionary<string, string> Description { get; }

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
        /// Gets a literal values that is required, or null if no requirement.
        /// </summary>
        public object RequiredLiteral { get; }
    }
}
