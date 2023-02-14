namespace DTDLParser
{
    /// <summary>
    /// Code-generation elements for declaring and parsing a specific literal type.
    /// </summary>
    public interface ILiteralType
    {
        /// <summary>
        /// Indicates whether it is possible for the type to be null.
        /// </summary>
        /// <param name="isOptional">True if the property is optional.</param>
        /// <returns>True if the type can be null.</returns>
        bool CanBeNull(bool isOptional);

        /// <summary>
        /// Gets the type declaration for a singular value.
        /// </summary>
        /// <param name="isOptional">True if the property is optional.</param>
        /// <returns>A string representation of the type.</returns>
        string GetSingularType(bool isOptional);

        /// <summary>
        /// Gets an appropriate initial value.
        /// </summary>
        /// <param name="isOptional">True if the property is optional.</param>
        /// <returns>A string representation of the value.</returns>
        string GetInitialValue(bool isOptional);

        /// <summary>
        /// Gets a parse expression for a singular value.
        /// </summary>
        /// <param name="stringVar">Name of a string variable that holds the value to parse.</param>
        /// <returns>A string representation of the parse expression.</returns>
        string GetParseExpression(string stringVar);
    }
}
