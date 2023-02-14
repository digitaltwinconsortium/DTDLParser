namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts material property information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class MaterialPropertyDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialPropertyDigest"/> class.
        /// </summary>
        public MaterialPropertyDigest()
        {
            this.IsLiteral = false;
            this.Class = null;
            this.DictionaryKey = null;
            this.IsAbstract = false;
            this.Datatype = null;
            this.IsPlural = false;
            this.IsOptional = false;
            this.DtmiSegment = null;
            this.IsInherited = false;
            this.IsShadowed = false;
            this.PropertyVersions = new Dictionary<int, PropertyVersionDigest>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialPropertyDigest"/> class.
        /// </summary>
        /// <param name="materialPropertyObj">A <c>JObject</c> from the metamodel digest containing information about a material property.</param>
        public MaterialPropertyDigest(JObject materialPropertyObj)
        {
            JObject versionlessPropertyObj = (JObject)materialPropertyObj["_"];

            this.IsLiteral = ((JValue)versionlessPropertyObj["literal"]).Value<bool>();

            this.Class = versionlessPropertyObj.TryGetValue("class", out JToken classValue) ? ((JValue)classValue).Value<string>() : null;

            this.DictionaryKey = versionlessPropertyObj.TryGetValue("dictionaryKey", out JToken dictionaryKey) ? ((JValue)dictionaryKey).Value<string>() : null;

            this.IsAbstract = ((JValue)versionlessPropertyObj["abstract"]).Value<bool>();

            this.Datatype = versionlessPropertyObj.TryGetValue("datatype", out JToken datatype) ? ((JValue)datatype).Value<string>() : null;

            this.IsPlural = ((JValue)versionlessPropertyObj["plural"]).Value<bool>();

            this.IsOptional = ((JValue)versionlessPropertyObj["optional"]).Value<bool>();

            this.DtmiSegment = versionlessPropertyObj.TryGetValue("dtmiSegment", out JToken dtmiSegment) ? ((JValue)dtmiSegment).Value<string>() : null;

            this.IsInherited = ((JValue)versionlessPropertyObj["inherited"]).Value<bool>();

            this.InheritedFrom = ((JValue)versionlessPropertyObj["inheritedFrom"]).Value<string>();

            this.IsShadowed = ((JValue)versionlessPropertyObj["shadowed"]).Value<bool>();

            this.Description = ((JObject)versionlessPropertyObj["description"]).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>());

            this.PropertyVersions = materialPropertyObj.Properties().Where(p1 => int.TryParse(p1.Name, out int _)).ToDictionary(p2 => int.Parse(p2.Name), p2 => new PropertyVersionDigest((JObject)p2.Value));
        }

        /// <summary>
        /// Gets a value indicating whether the property is a literal value.
        /// </summary>
        public bool IsLiteral { get; }

        /// <summary>
        /// Gets the class for an object property.
        /// </summary>
        public string Class { get; }

        /// <summary>
        /// Gets the name of a property of a child element that is used for the dictionary key of the property.
        /// </summary>
        public string DictionaryKey { get; }

        /// <summary>
        /// Gets a value indicating whether the type of the property is an abstract type.
        /// </summary>
        public bool IsAbstract { get; }

        /// <summary>
        /// Gets the datatype for a literal property.
        /// </summary>
        public string Datatype { get; }

        /// <summary>
        /// Gets a value indicating whether the property is plural.
        /// </summary>
        public bool IsPlural { get; }

        /// <summary>
        /// Gets a value indicating whether the property is optional.
        /// </summary>
        public bool IsOptional { get; }

        /// <summary>
        /// Gets the name of a property of a child element that is used to determine a segment for an auto-generated identifier.
        /// </summary>
        public string DtmiSegment { get; }

        /// <summary>
        /// Gets a value indicating whether the property is inherited from a proper superclass.
        /// </summary>
        public bool IsInherited { get; }

        /// <summary>
        /// Gets the name of the class from which the property is inherited.
        /// </summary>
        public string InheritedFrom { get; }

        /// <summary>
        /// Gets a value indicating whether the property has a shadow field to hold the original value if it is updatable via import.
        /// </summary>
        public bool IsShadowed { get; }

        /// <summary>
        /// Gets a language map description of the property.
        /// </summary>
        public Dictionary<string, string> Description { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a <see cref="PropertyVersionDigest"/> object providing version-specific details about the property.
        /// </summary>
        public Dictionary<int, PropertyVersionDigest> PropertyVersions { get; }
    }
}
