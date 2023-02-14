namespace DTDLParser
{
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
            this.MaxBytes = restrictionObj.TryGetValue("maxBytes", out JToken maxBytes) ? ((JValue)maxBytes).Value<int?>() : null;
        }

        /// <summary>
        /// Gets the maximum permissible size in bytes of a partition, or null if no maximum.
        /// </summary>
        public int? MaxBytes { get; }
    }
}
