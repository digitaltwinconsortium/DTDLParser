namespace DTDLParser
{
    /// <summary>
    /// Struct that holds information about a byte range with a corresponding identifier.
    /// </summary>
    internal struct IdentifiedByteRange
    {
        /// <summary>The index of the first byte in the range, zero-indexed.</summary>
        internal long StartIndex;

        /// <summary>The index of the last byte in the range, zero-indexed.</summary>
        internal long EndIndex;

        /// <summary>The identifier of the definition within the given byte range.</summary>
        internal string Id;
    }
}
