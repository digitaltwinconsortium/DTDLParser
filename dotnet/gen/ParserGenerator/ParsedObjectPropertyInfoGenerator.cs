namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>ParsedObjectPropertyInfo</c> partial class.
    /// </summary>
    public class ParsedObjectPropertyInfoGenerator : ITypeGenerator
    {
        private readonly string baseEnumName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedObjectPropertyInfoGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        public ParsedObjectPropertyInfoGenerator(string baseName)
        {
            this.baseEnumName = NameFormatter.FormatNameAsEnum(baseName);
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass infoClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "ParsedObjectPropertyInfo", completeness: Completeness.Partial);
            infoClass.Summary("Class containing information about an object property being parsed.");

            CsProperty expectedKindsProperty = infoClass.Property(Access.Internal, Novelty.Normal, $"HashSet<{this.baseEnumName}>", "ExpectedKinds");
            expectedKindsProperty.Summary($"Gets or sets a collection of <c>{this.baseEnumName}</c> indicating the expected kinds for this property.");
            expectedKindsProperty.Get().Set();
        }
    }
}
