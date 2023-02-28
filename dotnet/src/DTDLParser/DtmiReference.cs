namespace DTDLParser
{
    /// <summary>
    /// Describes an external reference to a DTMI and the location of the reference within a DTDL model.
    /// </summary>
    public class DtmiReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DtmiReference"/> class.
        /// </summary>
        /// <param name="identifier">Referenced DTMI that lacks a definition.</param>
        /// <param name="locationName">Name of the source file in which the DTMI is referenced.</param>
        /// <param name="locationLine">Line number within the source file in which the DTMI is referenced.</param>
        internal DtmiReference(Dtmi identifier, string locationName, int locationLine)
        {
            this.Identifier = identifier;
            this.LocationName = locationName;
            this.LocationLine = locationLine;
        }

        /// <summary>
        /// Gets the referenced DTMI that lacks a definition.
        /// </summary>
        public Dtmi Identifier { get; }

        /// <summary>
        /// Gets the name of the source file in which the DTMI is referenced.
        /// </summary>
        public string LocationName { get; }

        /// <summary>
        /// Gets the line number within the source file in which the DTMI is referenced.
        /// </summary>
        public int LocationLine { get; }
    }
}
