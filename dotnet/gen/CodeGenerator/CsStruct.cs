namespace DTDLParser
{
    /// <summary>
    /// Generator for C# struct.
    /// </summary>
    public class CsStruct : CsType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsStruct"/> class.
        /// </summary>
        /// <param name="access">Access level of struct.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="typeName">The name of the struct being declared.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="completeness">Partial or Full.</param>
        /// <param name="exports">Base struct and/or interfaces exported by this struct.</param>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsStruct(Access access, Novelty novelty, string typeName, Multiplicity multiplicity = Multiplicity.Instance, Completeness completeness = Completeness.Full, string exports = null, string subNamespace = null)
            : base(TypeType.Value, access, novelty, typeName, multiplicity, completeness, exports, subNamespace)
        {
        }

        /// <inheritdoc />
        protected override bool AllowNonPrivateFields { get => true; }
    }
}
