namespace DTDLParser
{
    /// <summary>
    /// Static class that holds values used by the Parser.
    /// </summary>
    internal static class ParserValues
    {
        /// <summary>
        /// The URI scheme for DTMIs.
        /// </summary>
        internal const string DtmiScheme = "dtmi";

        /// <summary>
        /// The URI prefix for DTMIs.
        /// </summary>
        internal const string DtmiPrefix = DtmiScheme + ":";

        /// <summary>
        /// The URI prefix for blank nodes.
        /// </summary>
        internal const string BlankNodePrefix = "_:";

        /// <summary>
        /// The '@type' value for DTDL language extension definitions.
        /// </summary>
        internal const string LanguageExtensionType = "DtdlExtension";
    }
}
