namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts standard sub-element information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class StandardSubElementDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardSubElementDigest"/> class.
        /// </summary>
        /// <param name="subElementObj">A <c>JObject</c> from the metamodel digest containing information about a sub-element of a standard element.</param>
        public StandardSubElementDigest(JObject subElementObj)
        {
            this.Name = ((JValue)subElementObj["name"]).Value<string>();

            this.Schema = subElementObj.TryGetValue("schema", out JToken schemaToken) ? ((JValue)schemaToken).Value<string>() : null;

            this.Description = subElementObj.TryGetValue("description", out JToken descriptioToken) ? ((JObject)descriptioToken).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>()) : new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the name of the sub-element.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the schema of the element, if any.
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// Gets a language map describing element.
        /// </summary>
        public Dictionary<string, string> Description { get; }
    }
}
