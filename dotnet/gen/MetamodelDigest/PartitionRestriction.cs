namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that expresses restrictions on partition classes in DTDL models.
    /// </summary>
    public class PartitionRestriction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartitionRestriction"/> class.
        /// </summary>
        /// <param name="restrictionObj">A <c>JObject</c> from the metamodel digest containing restrictions on partition classes.</param>
        public PartitionRestriction(JObject restrictionObj)
        {
            this.MaxBytes = restrictionObj.TryGetValue("maxBytes", out JToken specMaxBytes) ?
                ((JObject)specMaxBytes).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<int>()) :
                null;
        }

        /// <summary>
        /// Gets the maximum permissible size in bytes of a partition, according to a limit spec, or null if no maximum.
        /// </summary>
        public Dictionary<string, int> MaxBytes { get; }
    }
}
