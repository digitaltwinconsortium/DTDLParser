namespace DTDLParser
{
    /// <summary>
    /// INterface for recording the identifier corresponding to a given byte range in DTDL JSON text.
    /// </summary>
    internal interface IByteRangeRecorder
    {
        /// <summary>
        /// Record the association between an identifier and a given byte range.
        /// </summary>
        /// <param name="startIndex">The index of the first byte in the range, zero-indexed.</param>
        /// <param name="endIndex">The index of the last byte in the range, zero-indexed.</param>
        /// <param name="id">The identifier of the definition within the given byte range.</param>
        void RecordByteRange(long startIndex, long endIndex, string id);
    }
}
