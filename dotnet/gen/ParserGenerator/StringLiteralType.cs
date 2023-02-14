namespace DTDLParser
{
    /// <summary>
    /// Code-generation elements for declaring and parsing a specific literal type.
    /// </summary>
    public class StringLiteralType : ILiteralType
    {
        /// <inheritdoc/>
        public bool CanBeNull(bool isOptional)
        {
            return true;
        }

        /// <inheritdoc/>
        public string GetSingularType(bool isOptional)
        {
            return ParserGeneratorValues.ObverseTypeString;
        }

        /// <inheritdoc/>
        public string GetInitialValue(bool isOptional)
        {
            return isOptional ? $"({ParserGeneratorValues.ObverseTypeString})null" : "string.Empty";
        }

        /// <inheritdoc/>
        public string GetParseExpression(string stringVar)
        {
            return stringVar;
        }
    }
}
