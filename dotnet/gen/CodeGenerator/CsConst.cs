namespace DTDLParser
{
    /// <summary>
    /// Generator for C# const value.
    /// </summary>
    public class CsConst : CsField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsConst"/> class.
        /// </summary>
        /// <param name="access">Access level of const value.</param>
        /// <param name="type">Type of const value.</param>
        /// <param name="name">Name of const value.</param>
        /// <param name="value">Value of const value.</param>
        public CsConst(Access access, string type, string name, string value)
            : base(access, type, name, value, Multiplicity.Static, Mutability.Constant)
        {
        }
    }
}
