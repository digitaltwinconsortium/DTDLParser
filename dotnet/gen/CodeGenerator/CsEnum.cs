namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Generator for a C# enum.
    /// </summary>
    public class CsEnum : CsDeclaration, ICsFile
    {
        private List<EnumVal> enumVals;
        private bool sorted;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsEnum"/> class.
        /// </summary>
        /// <param name="access">Access level of enum.</param>
        /// <param name="typeName">The name of the enum being declared.</param>
        /// <param name="sorted">True if the enum values should be sorted by name.</param>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsEnum(Access access, string typeName, bool sorted = false, string subNamespace = null)
            : base(access, Novelty.Normal, "enum", typeName)
        {
            this.enumVals = new List<EnumVal>();
            this.sorted = sorted;

            this.TypeName = typeName;
            this.SubNamespace = subNamespace;
        }

        /// <summary>
        /// Gets the name of the type being declared.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Gets an additional component to be added to the library namespace, or null if there is none.
        /// </summary>
        public string SubNamespace { get; }

        /// <summary>
        /// Add a C# value to the enum.
        /// </summary>
        /// <param name="name">Name of value.</param>
        /// <param name="description">Text description of value.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsEnum Value(string name, string description)
        {
            if (description != null && !description.EndsWith("."))
            {
                throw new StyleException($"Documentation text of enum value '{name}' must end with a period -- SA1629.");
            }

            this.enumVals.Add(new EnumVal() { Name = name, Description = description });
            return this;
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            this.WriteSummary(codeWriter);
            this.WriteRemarks(codeWriter);

            this.WriteDisables(codeWriter);
            this.WriteAttributes(codeWriter);

            codeWriter.WriteLine(this.DecoratedName);
            codeWriter.OpenScope();

            if (this.sorted)
            {
                this.enumVals.Sort((left, right) => left.Name.CompareTo(right.Name));
            }

            foreach (EnumVal enumVal in this.enumVals)
            {
                codeWriter.WriteLine($"/// <summary>{enumVal.Description}</summary>");
                codeWriter.WriteLine($"{enumVal.Name},");
                codeWriter.Break();
            }

            codeWriter.CloseScope();
            this.WriteRestores(codeWriter);
        }

        private struct EnumVal
        {
            public string Name;
            public string Description;
        }
    }
}
