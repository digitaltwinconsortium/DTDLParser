namespace DTDLParser
{
    /// <summary>
    /// The kind of C# property for a material class.
    /// </summary>
    public enum PropertyRepresentation
    {
        /// <summary>Property is represented as a singular item.</summary>
        Item,

        /// <summary>Property is represented as a singular item.</summary>
        NullableItem,

        /// <summary>Property is represented as a list.</summary>
        List,

        /// <summary>Property is represented as a dictionary.</summary>
        Dictionary,
    }
}
