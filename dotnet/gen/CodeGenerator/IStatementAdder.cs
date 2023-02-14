namespace DTDLParser
{
    /// <summary>
    /// Interface for adding C# statements to a scope.
    /// </summary>
    public interface IStatementAdder
    {
        /// <summary>
        /// Add a <see cref="CsStatement"/> to the scope.
        /// </summary>
        /// <param name="csStatement">The <see cref="CsStatement"/> to add.</param>
        void AddStatement(CsStatement csStatement);
    }
}
