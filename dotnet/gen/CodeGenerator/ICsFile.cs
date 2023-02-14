namespace DTDLParser
{
    /// <summary>
    /// Interface for top-level declarations in C# code files.
    /// </summary>
    public interface ICsFile
    {
        /// <summary>
        /// Gets the type name of the declaration, used to generate the filename.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets an additional component to be added to the library namespace, or null if there is none.
        /// </summary>
        string SubNamespace { get; }

        /// <summary>
        /// Generate code for the file.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for generating the file code.</param>
        void GenerateCode(CodeWriter codeWriter);
    }
}
