namespace DTDLParser
{
    /// <summary>
    /// The type of a JSON-LD value.
    /// </summary>
    internal enum JsonLdValueType
    {
        /// <summary>The value is a JSON null.</summary>
        Null,

        /// <summary>The value is a JSON boolean.</summary>
        Boolean,

        /// <summary>The value is a JSON number.</summary>
        Number,

        /// <summary>The value is a JSON string.</summary>
        String,

        /// <summary>The value is a JSON-LD element, represented by a JSON object.</summary>
        Element,
    }
}
