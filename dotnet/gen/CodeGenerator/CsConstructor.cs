namespace DTDLParser
{
    /// <summary>
    /// Generator for C# constructor.
    /// </summary>
    public class CsConstructor : CsMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsConstructor"/> class.
        /// </summary>
        /// <param name="access">Access level of method.</param>
        /// <param name="name">Name of class or struct.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        public CsConstructor(Access access, string name, Multiplicity multiplicity = Multiplicity.Instance)
            : base(hasBody: true, access, Novelty.Normal, null, name, multiplicity: multiplicity)
        {
            if (access != Access.Implicit && access != Access.Private)
            {
                this.Summary($"Initializes a new instance of the <see cref=\"{name}\"/> class.");
            }
        }

        /// <summary>
        /// Add a base initializer to the method.
        /// </summary>
        /// <param name="initText">Initialization text.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsConstructor Base(string initText)
        {
            this.Preamble($": base({initText})");
            return this;
        }

        /// <summary>
        /// Add a this initializer to the method.
        /// </summary>
        /// <param name="initText">Initialization text.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsConstructor This(string initText)
        {
            this.Preamble($": this({initText})");
            return this;
        }
    }
}
