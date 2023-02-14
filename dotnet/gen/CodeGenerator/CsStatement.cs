namespace DTDLParser
{
    /// <summary>
    /// Generator for C# statement.
    /// </summary>
    public abstract class CsStatement
    {
        /// <summary>
        /// Gets text for sorting against other <c>CsStatement</c> objects.
        /// </summary>
        internal abstract string SortingText { get; }

        /// <summary>
        /// Generate code for the statement.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for generating the statement code.</param>
        public abstract void GenerateCode(CodeWriter codeWriter);
    }
}
