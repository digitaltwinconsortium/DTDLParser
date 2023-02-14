namespace DTDLParser
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that expresses restrictions on allowed strings in DTDL models.
    /// </summary>
    public class StringRestriction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringRestriction"/> class.
        /// </summary>
        /// <param name="restrictionObj">A <c>JObject</c> from the metamodel digest containing restrictions on the string value.</param>
        public StringRestriction(JObject restrictionObj)
        {
            this.MaxLength = restrictionObj.TryGetValue("maxLength", out JToken maxLength) ? ((JValue)maxLength).Value<int?>() : null;

            this.Pattern = restrictionObj.TryGetValue("pattern", out JToken pattern) ? ((JValue)pattern).Value<string>() : null;
        }

        /// <summary>
        /// Gets the maximum permissible length of a string, or null if no maximum.
        /// </summary>
        public int? MaxLength { get; }

        /// <summary>
        /// Gets a regex that constrains the permissible values, or null if no pattern constraint.
        /// </summary>
        public string Pattern { get; }
    }
}
