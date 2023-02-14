namespace DTDLParser
{
    /// <summary>
    /// Code-generation elements for declaring and parsing a specific literal type.
    /// </summary>
    public class BooleanLiteralType : ILiteralType
    {
        /// <inheritdoc/>
        public bool CanBeNull(bool isOptional)
        {
            return false;
        }

        /// <inheritdoc/>
        public string GetSingularType(bool isOptional)
        {
            return ParserGeneratorValues.ObverseTypeBoolean;
        }

        /// <inheritdoc/>
        public string GetInitialValue(bool isOptional)
        {
            return ParserGeneratorValues.ObverseValueFalse;
        }

        /// <inheritdoc/>
        public string GetParseExpression(string stringVar)
        {
            return $"{ParserGeneratorValues.ObverseTypeBoolean}.Parse({stringVar})";
        }
    }
}
