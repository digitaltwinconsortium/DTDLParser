namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>IRootable</c> interface.
    /// </summary>
    public class RootableGenerator : ITypeGenerator
    {
        private readonly bool isLayeringSupported;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootableGenerator"/> class.
        /// </summary>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public RootableGenerator(bool isLayeringSupported)
        {
            this.isLayeringSupported = isLayeringSupported;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsInterface rootableInterface = parserLibrary.Interface(Access.Public, "IRootable");

            rootableInterface.Summary("Interface for additional methods on elements that are allowed at the root of a model file.");

            CsMethod getJsonLdMethod = rootableInterface.Method(Access.Implicit, "JsonElement", "GetJsonLd");
            getJsonLdMethod.Summary("Gets a <c>JsonElement</c> that holds the portion of the DTDL model that defines this rootable element.");
            getJsonLdMethod.Returns("A <c>JsonElement</c> object containing the JSON structure.");
            if (this.isLayeringSupported)
            {
                getJsonLdMethod.Param("string", "layer", "Optional name of the layer whose JSON to return.", "\"\"");
            }

            CsMethod getJsonLdTextMethod = rootableInterface.Method(Access.Implicit, "string", "GetJsonLdText");
            getJsonLdTextMethod.Summary("Gets a JSON string that holds the portion of the DTDL model that defines this rootable element.");
            getJsonLdTextMethod.Returns("A string containing the JSON text.");
            if (this.isLayeringSupported)
            {
                getJsonLdTextMethod.Param("string", "layer", "Optional name of the layer whose JSON text to return.", "\"\"");
            }
        }
    }
}
