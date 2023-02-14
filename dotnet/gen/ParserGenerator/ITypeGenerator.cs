namespace DTDLParser
{
    /// <summary>
    /// Interface for classes that generate code for C# types.
    /// </summary>
    public interface ITypeGenerator
    {
        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        void GenerateCode(CsLibrary parserLibrary);
    }
}
