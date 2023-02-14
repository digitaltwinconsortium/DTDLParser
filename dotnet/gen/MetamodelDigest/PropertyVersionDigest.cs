namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts DTDL-version-specific material property information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class PropertyVersionDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyVersionDigest"/> class.
        /// </summary>
        /// <param name="propertyVersionObj">A <c>JObject</c> from the metamodel digest containing DTDL-version-specific information about a material property.</param>
        public PropertyVersionDigest(JObject propertyVersionObj)
        {
            this.IdRequired = ((JValue)propertyVersionObj["idRequired"]).Value<bool>();

            this.DefaultLanguage = propertyVersionObj.TryGetValue("defaultLanguage", out JToken defaultLanguage) ? ((JValue)defaultLanguage).Value<string>() : null;

            this.IsAllowed = ((JValue)propertyVersionObj["allowed"]).Value<bool>();

            this.Class = propertyVersionObj.TryGetValue("class", out JToken classValue) ? ((JValue)classValue).Value<string>() : null;

            this.ClassVersions = propertyVersionObj.TryGetValue("versions", out JToken versions) ? ((JArray)versions).Select(v => ((JValue)v).Value<int>()).ToList() : null;

            this.MaxCount = propertyVersionObj.TryGetValue("maxCount", out JToken maxCount) ? ((JValue)maxCount).Value<int?>() : null;

            this.MinCount = propertyVersionObj.TryGetValue("minCount", out JToken minCount) ? ((JValue)minCount).Value<int?>() : null;

            this.MaxInclusive = propertyVersionObj.TryGetValue("maxInclusive", out JToken maxInclusive) ? ((JValue)maxInclusive).Value<int?>() : null;

            this.MinInclusive = propertyVersionObj.TryGetValue("minInclusive", out JToken minInclusive) ? ((JValue)minInclusive).Value<int?>() : null;

            this.MaxLength = propertyVersionObj.TryGetValue("maxLength", out JToken maxLength) ? ((JValue)maxLength).Value<int?>() : null;

            this.Pattern = propertyVersionObj.TryGetValue("pattern", out JToken pattern) ? ((JValue)pattern).Value<string>() : null;

            this.PatternDesc = propertyVersionObj.TryGetValue("patternDesc", out JToken patternDesc) ? ((JObject)patternDesc).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>()) : null;

            this.ValueUris = propertyVersionObj.TryGetValue("valueUris", out JToken valueUris) ? ((JArray)valueUris).Select(v => ((JValue)v).Value<string>()).ToList() : null;

            this.Values = propertyVersionObj.TryGetValue("values", out JToken values) ? ((JArray)values).Select(v => ((JValue)v).Value<string>()).ToList() : null;

            this.TypeRequired = ((JValue)propertyVersionObj["typeRequired"]).Value<bool>();

            this.UniqueProperties = propertyVersionObj.TryGetValue("uniqueProperties", out JToken uniqueProperties) ? ((JArray)uniqueProperties).Select(v => ((JValue)v).Value<string>()).ToList() : null;

            this.Value = propertyVersionObj.TryGetValue("value", out JToken value) ? ((JValue)value).Value<object>() : null;

            this.IsHidden = ((JValue)propertyVersionObj["hidden"]).Value<bool>();

            this.IsDeprecated = ((JValue)propertyVersionObj["deprecated"]).Value<bool>();

            this.UniqueAmong = propertyVersionObj.TryGetValue("uniqueAmong", out JToken uniqueAmong) ? ((JArray)uniqueAmong).Select(v => new ClassPropertyPair((JObject)v)).ToList() : new List<ClassPropertyPair>();
        }

        /// <summary>
        /// Gets a value indicating whether an identifier is required for the property.
        /// </summary>
        public bool IdRequired { get; }

        /// <summary>
        /// Gets the default language code for a language-tagged string literal property.
        /// </summary>
        public string DefaultLanguage { get; }

        /// <summary>
        /// Gets a value indicating whether the property is allowed to be specified in a model.
        /// </summary>
        public bool IsAllowed { get; }

        /// <summary>
        /// Gets the class for an object property.
        /// </summary>
        public string Class { get; }

        /// <summary>
        /// Gets the allowed versions of a class for an object property.
        /// </summary>
        public List<int> ClassVersions { get; }

        /// <summary>
        /// Gets the maximum count of permitted values of the property.
        /// </summary>
        public int? MaxCount { get; }

        /// <summary>
        /// Gets the minimum count of permitted values of the property.
        /// </summary>
        public int? MinCount { get; }

        /// <summary>
        /// Gets the maximum permissible value, or null if no maximum.
        /// </summary>
        public int? MaxInclusive { get; }

        /// <summary>
        /// Gets the minimum permissible value, or null if no minimim.
        /// </summary>
        public int? MinInclusive { get; }

        /// <summary>
        /// Gets the maximum permissible length of a string, or null if no maximum.
        /// </summary>
        public int? MaxLength { get; }

        /// <summary>
        /// Gets a regex that constrains the permissible values, or null if no pattern constraint.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Gets a language map description of the pattern constraint.
        /// </summary>
        public Dictionary<string, string> PatternDesc { get; }

        /// <summary>
        /// Gets a list of element URIs that restricts the set of values the property is permitted to have.
        /// </summary>
        public List<string> ValueUris { get; }

        /// <summary>
        /// Gets a list of element names that restricts the set of values the property is permitted to have.
        /// </summary>
        public List<string> Values { get; }

        /// <summary>
        /// Gets a value indicating whether a type must be specified for the property.
        /// </summary>
        public bool TypeRequired { get; }

        /// <summary>
        /// Gets a list of names of properties of child elements that must be unique.
        /// </summary>
        public List<string> UniqueProperties { get; }

        /// <summary>
        /// Gets a value to be assigned to the property if the property is not set in a model.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Gets a value indicating whether the property should be hidden in the documentation.
        /// </summary>
        public bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether the property should be marked as deprecated in the documentation.
        /// </summary>
        public bool IsDeprecated { get; }

        /// <summary>
        /// Gets a list of <see cref="ClassPropertyPair"/> that characterize uniqueness requirements on the property.
        /// </summary>
        public List<ClassPropertyPair> UniqueAmong { get; }
    }
}
