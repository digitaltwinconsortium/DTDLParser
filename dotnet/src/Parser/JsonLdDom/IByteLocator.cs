namespace DTDLParser
{
    /// <summary>
    /// Interface for transforming a byte index in a JSON document to a source name and line number.
    /// </summary>
    internal interface IByteLocator
    {
        /// <summary>
        /// Gets the source name and line number corresponding to a byte index, if possible.
        /// </summary>
        /// <param name="byteIndex">The index of a byte in the JSON text, zero-indexed.</param>
        /// <param name="sourceName">The name of the JSON text containing the indexed byte.</param>
        /// <param name="sourceLine">The line number on which the indexed byte lies, one-indexed.</param>
        /// <returns>True if the conversion from byte index to source information is successful.</returns>
        bool TryGetSourceLocation(long byteIndex, out string sourceName, out int sourceLine);
    }
}
