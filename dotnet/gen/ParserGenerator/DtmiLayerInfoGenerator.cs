namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>DtmiLayerInfo</c> partial class.
    /// </summary>
    public class DtmiLayerInfoGenerator : ITypeGenerator
    {
        private readonly bool isLayeringSupported;

        /// <summary>
        /// Initializes a new instance of the <see cref="DtmiLayerInfoGenerator"/> class.
        /// </summary>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public DtmiLayerInfoGenerator(bool isLayeringSupported)
        {
            this.isLayeringSupported = isLayeringSupported;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            if (this.isLayeringSupported)
            {
                CsStruct infoStruct = parserLibrary.Struct(Access.Public, Novelty.Normal, "DtmiLayerInfo");
                infoStruct.Summary("The <c>DtmiLayerInfo</c> struct characterizes an element that may require addtional layer definitions to be complete.");

                infoStruct.Field(Access.Public, "Dtmi", "Dtmi", description: "The identifier of the element to define.");

                infoStruct.Field(Access.Public, "IReadOnlyCollection<string>", "KnownLayers", description: "A collection of names of layers that are known and which therefore should be omitted from the definitions provided for the element.");
            }
        }
    }
}
