namespace DTDLParser
{
    /// <summary>
    /// Code-generation elements for declaring and parsing a specific literal type.
    /// </summary>
    public class IntegerLiteralType : ILiteralType
    {
        /// <inheritdoc/>
        public bool CanBeNull(bool isOptional)
        {
            return isOptional;
        }

        /// <inheritdoc/>
        public string GetSingularType(bool isOptional)
        {
            return isOptional ? $"{ParserGeneratorValues.ObverseTypeInteger}?" : ParserGeneratorValues.ObverseTypeInteger;
        }

        /// <inheritdoc/>
        public string GetInitialValue(bool isOptional)
        {
            return isOptional ? $"({ParserGeneratorValues.ObverseTypeInteger}?)null" : "0";
        }

        /// <inheritdoc/>
        public string GetParseExpression(string stringVar)
        {
            return $"{ParserGeneratorValues.ObverseTypeInteger}.Parse({stringVar})";
        }
    }
}
