namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts standard element information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class StandardElementDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardElementDigest"/> class.
        /// </summary>
        /// <param name="standardElementObj">A <c>JObject</c> from the metamodel digest containing information about a standard element.</param>
        public StandardElementDigest(JObject standardElementObj)
        {
            this.ElementId = ((JValue)standardElementObj["id"]).Value<string>();

            this.ElementName = ((JValue)standardElementObj["name"]).Value<string>();

            this.Description = ((JObject)standardElementObj["description"]).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>());
        }

        /// <summary>
        /// Gets the identifier of the element.
        /// </summary>
        public string ElementId { get; }

        /// <summary>
        /// Gets the name of the element.
        /// </summary>
        public string ElementName { get; }

        /// <summary>
        /// Gets a language map describing element.
        /// </summary>
        public Dictionary<string, string> Description { get; }
    }
}
