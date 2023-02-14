namespace DTDLParser
{
    /// <summary>
    /// Generator for C# class.
    /// </summary>
    public class CsClass : CsType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsClass"/> class.
        /// </summary>
        /// <param name="access">Access level of class.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="typeName">The name of the class being declared.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="completeness">Partial or Full.</param>
        /// <param name="exports">Base class and/or interfaces exported by this class.</param>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsClass(Access access, Novelty novelty, string typeName, Multiplicity multiplicity = Multiplicity.Instance, Completeness completeness = Completeness.Full, string exports = null, string subNamespace = null)
            : base(TypeType.Reference, access, novelty, typeName, multiplicity, completeness, exports, subNamespace)
        {
        }

        /// <inheritdoc />
        protected override bool AllowNonPrivateFields { get => false; }
    }
}
