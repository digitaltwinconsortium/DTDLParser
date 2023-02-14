namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts supplemental type information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class SupplementalTypeDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalTypeDigest"/> class.
        /// </summary>
        /// <param name="supplementalTypeObj">A <c>JObject</c> from the metamodel digest containing information about a supplemental type.</param>
        public SupplementalTypeDigest(JObject supplementalTypeObj)
        {
            this.IsAbstract = ((JValue)supplementalTypeObj["abstract"]).Value<bool>();

            this.IsMergeable = ((JValue)supplementalTypeObj["mergeable"]).Value<bool>();

            this.Parent = ((JValue)supplementalTypeObj["parent"]).Value<string>();

            this.ExtensionKind = ((JValue)supplementalTypeObj["extensionKind"]).Value<string>();

            this.ExtensionContext = ((JValue)supplementalTypeObj["extensionContext"]).Value<string>();

            this.Cotypes = ((JArray)supplementalTypeObj["cotypes"]).Select(t => ((JValue)t).Value<string>()).ToList();

            this.CotypeVersions = ((JArray)supplementalTypeObj["cotypeVersions"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.Cocotypes = supplementalTypeObj.TryGetValue("cocotypes", out JToken cocotypes) ? ((JArray)cocotypes).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.Discotypes = supplementalTypeObj.TryGetValue("discotypes", out JToken discotypes) ? ((JArray)discotypes).Select(t => ((JValue)t).Value<string>()).ToList() : null;

            this.Properties = ((JObject)supplementalTypeObj["properties"]).Properties().ToDictionary(p => p.Name, p => new SupplementalPropertyDigest((JObject)p.Value));

            this.Constraints = ((JArray)supplementalTypeObj["constraints"]).Select(t => new SupplementalConstraintDigest((JObject)t)).ToList();

            this.Siblings = ((JArray)supplementalTypeObj["siblings"]).Select(t => new SiblingConstraintDigest((JObject)t)).ToList();
        }

        /// <summary>
        /// Gets a value indicating whether the supplemental type is abstract.
        /// </summary>
        public bool IsAbstract { get; }

        /// <summary>
        /// Gets a value indicating whether elements with the supplemental type can have identifiers containing IRI fragments.
        /// </summary>
        public bool IsMergeable { get; }

        /// <summary>
        /// Gets the URI of the parent type of the supplemental type.
        /// </summary>
        public string Parent { get; }

        /// <summary>
        /// Gets th extension kind of the supplemental type.
        /// </summary>
        public string ExtensionKind { get; }

        /// <summary>
        /// Gets the context specifier that refers to the extension in which the supplemental type is defined.
        /// </summary>
        public string ExtensionContext { get; }

        /// <summary>
        /// Gets a list of names of material classes which may be cotyped with the supplemental type.
        /// </summary>
        public List<string> Cotypes { get; }

        /// <summary>
        /// Gets a list of DTDL versions of material classes which may be cotyped with the supplemental type.
        /// </summary>
        public List<int> CotypeVersions { get; }

        /// <summary>
        /// Gets a list of URIs of supplemental classes which may be co-cotyped with the supplemental type.
        /// </summary>
        public List<string> Cocotypes { get; }

        /// <summary>
        /// Gets a list of URIs of supplemental classes which must not be co-cotyped with the supplemental type.
        /// </summary>
        public List<string> Discotypes { get; }

        /// <summary>
        /// Gets a dictionary that maps from property URI to a <see cref="SupplementalPropertyDigest"/> object providing details about the property.
        /// </summary>
        public Dictionary<string, SupplementalPropertyDigest> Properties { get; }

        /// <summary>
        /// Gets a list of <see cref="SupplementalConstraintDigest"/> objects, each of which provides details about a constraint.
        /// </summary>
        public List<SupplementalConstraintDigest> Constraints { get; }

        /// <summary>
        /// Gets a list of <see cref="SiblingConstraintDigest"/> objects, each of which provides details about a sibling constraint.
        /// </summary>
        public List<SiblingConstraintDigest> Siblings { get; }
    }
}
