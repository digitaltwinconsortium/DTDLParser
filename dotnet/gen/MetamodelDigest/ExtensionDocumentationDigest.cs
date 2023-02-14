namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts extension documenation information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class ExtensionDocumentationDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionDocumentationDigest"/> class.
        /// </summary>
        /// <param name="extensionDocumentationObj">A <c>JObject</c> from the metamodel digest containing documentation information about an extension.</param>
        public ExtensionDocumentationDigest(JObject extensionDocumentationObj)
        {
            this.Description = ((JObject)extensionDocumentationObj["description"]).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>());
        }

        /// <summary>
        /// Gets a language map describing the extension.
        /// </summary>
        public Dictionary<string, string> Description { get; }
    }
}
