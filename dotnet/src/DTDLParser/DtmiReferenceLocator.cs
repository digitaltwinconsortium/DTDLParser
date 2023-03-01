namespace DTDLParser
{
    using System.Linq;

    /// <summary>
    /// Holds information for determining the location of an external reference within a DTDL model.
    /// </summary>
    internal class DtmiReferenceLocator
    {
        private JsonLdProperty property;
        private string referencedId;
        private string sourceName;
        private int sourceLine;
        private bool hasSource;
        private bool isCached;

        /// <summary>
        /// Initializes a new instance of the <see cref="DtmiReferenceLocator"/> class.
        /// </summary>
        /// <param name="property">A <see cref="JsonLdProperty"/> whose value is an undefined DTMI.</param>
        /// <param name="referencedId">A string reperesenation of the undefined DTMI as used in the DTDL model source.</param>
        internal DtmiReferenceLocator(JsonLdProperty property, string referencedId)
        {
            this.property = property;
            this.referencedId = referencedId;
            this.isCached = false;
        }

        /// <summary>
        /// Get a string representation  of the reference source, if available.
        /// </summary>
        /// <param name="refString">Out-parameter to receive the string.</param>
        /// <returns>True if the source information could be determined.</returns>
        internal bool TryGetString(out string refString)
        {
            this.Extract();

            if (this.hasSource)
            {
                refString = $"in {this.sourceName} at line {this.sourceLine}";
                return true;
            }
            else
            {
                refString = null;
                return false;
            }
        }

        /// <summary>
        /// Get a <see cref="DtmiReference"/> with source location information, if possible.
        /// </summary>
        /// <param name="identifier">Referenced DTMI that lacks a definition.</param>
        /// <param name="dtmiReference">Out-parameter that holds the generated <c>DtmiReference</c> if source location could be determined; otherwise null.</param>
        /// <returns>True if the source information could be determined.</returns>
        internal bool TrySourceDtmiReference(Dtmi identifier, out DtmiReference dtmiReference)
        {
            this.Extract();

            if (this.hasSource)
            {
                dtmiReference = new DtmiReference(identifier, this.sourceName, this.sourceLine);
                return true;
            }
            else
            {
                dtmiReference = null;
                return false;
            }
        }

        private void Extract()
        {
            if (!this.isCached)
            {
                JsonLdValue propertyValue = this.property.Values.Values.FirstOrDefault(v => v.StringValue == this.referencedId);
                this.hasSource = propertyValue?.TryGetSourceLocation(out this.sourceName, out this.sourceLine, out _) ?? false;
                this.isCached = true;
            }
        }
    }
}
