namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for transforming a byte index in a JSON document to a source name and line number via a <see cref="DtdlResolveLocator"/>.
    /// </summary>
    internal class ResolveByteLocator : IByteLocator, IByteRangeRecorder
    {
        private readonly byte[] jsonBytes;
        private readonly DtdlResolveLocator dtdlResolveLocator;
        private List<IdentifiedByteRange> identifiedByteRanges;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveByteLocator"/> class.
        /// </summary>
        /// <param name="jsonBytes">A byte array containing UTF8-encoded JSON-LD text.</param>
        /// <param name="dtdlResolveLocator">A <see cref="DtdlResolveLocator"/> delegate that converts a resolve DTMI and line number into a source name and line number.</param>
        internal ResolveByteLocator(byte[] jsonBytes, DtdlResolveLocator dtdlResolveLocator)
        {
            this.jsonBytes = jsonBytes;
            this.dtdlResolveLocator = dtdlResolveLocator;
            this.identifiedByteRanges = new List<IdentifiedByteRange>();
        }

        /// <inheritdoc/>
        bool IByteLocator.TryGetSourceLocation(long byteIndex, out string sourceName, out int sourceLine)
        {
            IdentifiedByteRange matchingRange = this.identifiedByteRanges.Where(r => r.StartIndex <= byteIndex && byteIndex <= r.EndIndex).FirstOrDefault();

            if (matchingRange.EndIndex == default(IdentifiedByteRange).EndIndex)
            {
                sourceName = null;
                sourceLine = 0;
                return false;
            }

            int resolveLine = 1;
            for (long ix = matchingRange.StartIndex; ix < byteIndex; ++ix)
            {
                if (this.jsonBytes[ix] == '\n')
                {
                    ++resolveLine;
                }
            }

            if (this.dtdlResolveLocator != null && this.dtdlResolveLocator(new Dtmi(matchingRange.Id, skipValidation: true), resolveLine, out sourceName, out sourceLine))
            {
                return true;
            }

            sourceName = null;
            sourceLine = 0;
            return false;
        }

        /// <inheritdoc/>
        void IByteRangeRecorder.RecordByteRange(long startIndex, long endIndex, string id)
        {
            this.identifiedByteRanges.Add(new IdentifiedByteRange() { StartIndex = startIndex, EndIndex = endIndex, Id = id });
        }
    }
}
