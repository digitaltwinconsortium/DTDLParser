namespace DTDLParser
{
    /// <summary>
    /// Class for transforming a byte index in a JSON document to a source name and line number via a <see cref="DtdlParseLocator"/>.
    /// </summary>
    internal class ParseByteLocator : IByteLocator
    {
        private readonly int enumerationIndex;
        private readonly byte[] jsonBytes;
        private readonly DtdlParseLocator dtdlParseLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseByteLocator"/> class.
        /// </summary>
        /// <param name="enumerationIndex">An index into the enumeration of JSON text strings passed to the <see cref="ModelParser"/>.</param>
        /// <param name="jsonBytes">A byte array containing UTF8-encoded JSON-LD text.</param>
        /// <param name="dtdlParseLocator">A <see cref="DtdlParseLocator"/> delegate that converts an enumeration index and line number into a source name and line number.</param>
        internal ParseByteLocator(int enumerationIndex, byte[] jsonBytes, DtdlParseLocator dtdlParseLocator)
        {
            this.enumerationIndex = enumerationIndex;
            this.jsonBytes = jsonBytes;
            this.dtdlParseLocator = dtdlParseLocator;
        }

        /// <inheritdoc/>
        bool IByteLocator.TryGetSourceLocation(long byteIndex, out string sourceName, out int sourceLine)
        {
            if (byteIndex < 0 || byteIndex >= this.jsonBytes.Length)
            {
                sourceName = null;
                sourceLine = 0;
                return false;
            }

            int parseLine = 1;
            for (long ix = 0; ix < byteIndex; ++ix)
            {
                if (this.jsonBytes[ix] == '\n')
                {
                    ++parseLine;
                }
            }

            if (this.dtdlParseLocator != null && this.dtdlParseLocator(this.enumerationIndex, parseLine, out sourceName, out sourceLine))
            {
                return true;
            }

            sourceName = null;
            sourceLine = 0;
            return false;
        }
    }
}
