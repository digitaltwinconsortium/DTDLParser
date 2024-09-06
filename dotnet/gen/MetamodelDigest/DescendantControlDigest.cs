namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts descendant control information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class DescendantControlDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescendantControlDigest"/> class.
        /// </summary>
        /// <param name="descendantControlObj">The descendant control object extracted from the metamodel digest.</param>
        public DescendantControlDigest(JObject descendantControlObj)
        {
            this.DtdlVersion = ((JValue)descendantControlObj["dtdlVersion"]).Value<int>();

            this.RootClass = ((JValue)descendantControlObj["rootClass"]).Value<string>();

            this.DefiningClass = ((JValue)descendantControlObj["definingClass"]).Value<string>();

            this.PropertyNames = ((JArray)descendantControlObj["properties"]).Select(t => ((JValue)t).Value<string>()).ToList();

            this.IsNarrow = ((JValue)descendantControlObj["narrow"]).Value<bool>();

            this.Entailments = ((JObject)descendantControlObj["entailments"]).Properties().ToDictionary(p => p.Name, p => new List<string>(((JArray)p.Value).Select(t => ((JValue)t).Value<string>())));

            this.ExcludeType = descendantControlObj.TryGetValue("excludeType", out JToken excludeType) ? ((JValue)excludeType).Value<string>() : null;

            this.DatatypeProperty = descendantControlObj.TryGetValue("datatypeProperty", out JToken datatypeProperty) ? ((JValue)datatypeProperty).Value<string>() : null;

            this.MaxDepth = descendantControlObj.TryGetValue("maxDepth", out JToken specMaxDepth) ?
                ((JObject)specMaxDepth).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<int>()) :
                null;

            this.MaxCount = descendantControlObj.TryGetValue("maxCount", out JToken specMaxCount) ?
                ((JObject)specMaxCount).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<int>()) :
                null;

            this.ImportProperties = descendantControlObj.TryGetValue("importProperties", out JToken importProperties) ? ((JArray)importProperties).Select(t => ((JValue)t).Value<string>()).ToList() : null;
        }

        /// <summary>
        /// Gets the DTDL version in which the descendant control is defined.
        /// </summary>
        public int DtdlVersion { get; }

        /// <summary>
        /// Gets the name of the concete class the control pertains to.
        /// </summary>
        public string RootClass { get; }

        /// <summary>
        /// Gets the name of the class in which the the control is defined.
        /// </summary>
        public string DefiningClass { get; }

        /// <summary>
        /// Gets the names of the properties for which this control is relevant.
        /// </summary>
        public List<string> PropertyNames { get; }

        /// <summary>
        /// Gets a value indicating whether whether the descendant hierarchy should be scanned only along relevant properties.
        /// </summary>
        public bool IsNarrow { get; }

        /// <summary>
        /// Gets a dictionary that maps from object properties of the <c>RootClass</c> to a list of types for the property.
        /// </summary>
        public Dictionary<string, List<string>> Entailments { get; }

        /// <summary>
        /// Gets the type that is to be excluded from the relevant properties.
        /// </summary>
        public string ExcludeType { get; }

        /// <summary>
        /// Gets the property whose value determines the required datatype of the relevant properties.
        /// </summary>
        public string DatatypeProperty { get; }

        /// <summary>
        /// Gets the maximum allowed count of relevant properties in a hierarchical chain, according to a limit spec.
        /// </summary>
        public Dictionary<string, int> MaxDepth { get; }

        /// <summary>
        /// Gets the maximum allowed count of relevant properties in a descendant hierarchy, according to a limit spec.
        /// </summary>
        public Dictionary<string, int> MaxCount { get; }

        /// <summary>
        /// Gets a list of names of properties whose values should be imported from the relevant descendants.
        /// </summary>
        public List<string> ImportProperties { get; }
    }
}
