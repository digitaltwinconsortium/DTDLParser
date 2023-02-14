namespace DTDLParser
{
    /// <summary>
    /// Generator for C# field.
    /// </summary>
    public class CsField : CsDeclaration
    {
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsField"/> class.
        /// </summary>
        /// <param name="access">Access level of field.</param>
        /// <param name="type">Type of field.</param>
        /// <param name="name">Name of field.</param>
        /// <param name="value">Optional value for field.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="mutability">Constant, ReadOnly, or Mutable.</param>
        public CsField(Access access, string type, string name, string value = null, Multiplicity multiplicity = Multiplicity.Instance, Mutability mutability = Mutability.Mutable)
            : base(access, Novelty.Normal, type, name, multiplicity: multiplicity, mutability: mutability)
        {
            this.value = value;
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            this.WriteSummary(codeWriter);
            this.WriteRemarks(codeWriter);

            this.WriteDisables(codeWriter);
            this.WriteAttributes(codeWriter);

            if (this.value != null)
            {
                codeWriter.WriteLine($"{this.DecoratedName} = {this.value};");
            }
            else
            {
                codeWriter.WriteLine($"{this.DecoratedName};");
            }

            this.WriteRestores(codeWriter);
        }
    }
}
