namespace DTDLParser
{
    /// <summary>
    /// The kind of C# property for a material class.
    /// </summary>
    public enum PropertyKind
    {
        /// <summary>A literal property</summary>
        Literal,

        /// <summary>An object property.</summary>
        Object,

        /// <summary>An identifier property.</summary>
        Identifier,

        /// <summary>An internal property.</summary>
        Internal,
    }
}
