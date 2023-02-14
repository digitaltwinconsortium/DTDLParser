namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Base class for C# declaration generators.
    /// </summary>
    public class CsDeclaration : IComparable<CsDeclaration>
    {
        private readonly Access access;
        private readonly Novelty novelty;
        private readonly Multiplicity multiplicity;
        private readonly Asynchrony asynchrony;
        private readonly Mutability mutability;
        private readonly Completeness completeness;

        private bool inheritDoc;
        private List<string> summaryLines;
        private List<string> remarksLines;
        private List<string> attributes;
        private List<string> suppressions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsDeclaration"/> class.
        /// </summary>
        /// <param name="access">Access level of declaration.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of declaration.</param>
        /// <param name="name">Name of declaration.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="asynchrony">Aync or Sync.</param>
        /// <param name="mutability">Constant, ReadOnly, or Mutable.</param>
        /// <param name="completeness">Partial or Full.</param>
        public CsDeclaration(
            Access access,
            Novelty novelty,
            string type,
            string name,
            Multiplicity multiplicity = Multiplicity.Instance,
            Asynchrony asynchrony = Asynchrony.Sync,
            Mutability mutability = Mutability.Mutable,
            Completeness completeness = Completeness.Full)
        {
            this.access = access;
            this.novelty = novelty;
            this.Type = type;
            this.Name = name;
            this.multiplicity = multiplicity;
            this.asynchrony = asynchrony;
            this.mutability = mutability;
            this.completeness = completeness;

            this.inheritDoc = false;
            this.summaryLines = new List<string>();
            this.remarksLines = new List<string>();
            this.attributes = new List<string>();
            this.suppressions = new List<string>();
        }

        /// <summary>
        /// Gets the value of the type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the value of the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the name decorated with appropriate keywords.
        /// </summary>
        public string DecoratedName
        {
            get
            {
                StringBuilder decoratedName = new StringBuilder();

                switch (this.access)
                {
                    case Access.Public:
                        decoratedName.Append("public ");
                        break;
                    case Access.Internal:
                        decoratedName.Append("internal ");
                        break;
                    case Access.ProtectedInternal:
                        decoratedName.Append("protected internal ");
                        break;
                    case Access.Protected:
                        decoratedName.Append("protected ");
                        break;
                    case Access.PrivateProtected:
                        decoratedName.Append("private protected ");
                        break;
                    case Access.Private:
                        decoratedName.Append("private ");
                        break;
                }

                if (this.multiplicity == Multiplicity.Static && this.mutability != Mutability.Constant)
                {
                    decoratedName.Append("static ");
                }

                switch (this.novelty)
                {
                    case Novelty.Abstract:
                        decoratedName.Append("abstract ");
                        break;
                    case Novelty.Virtual:
                        decoratedName.Append("virtual ");
                        break;
                    case Novelty.Override:
                        decoratedName.Append("override ");
                        break;
                    case Novelty.New:
                        decoratedName.Append("new ");
                        break;
                }

                if (this.asynchrony == Asynchrony.Async)
                {
                    decoratedName.Append("async ");
                }

                if (this.mutability == Mutability.Constant)
                {
                    decoratedName.Append("const ");
                }

                if (this.completeness == Completeness.Partial)
                {
                    decoratedName.Append("partial ");
                }

                if (this.mutability == Mutability.ReadOnly)
                {
                    decoratedName.Append("readonly ");
                }

                if (this.Type != null)
                {
                    decoratedName.Append($"{this.Type} ");
                }

                decoratedName.Append(this.Name);

                return decoratedName.ToString();
            }
        }

        /// <summary>
        /// Inherit documentation comments from base class.
        /// </summary>
        public void InheritDoc()
        {
            this.inheritDoc = true;
        }

        /// <summary>
        /// Add a line of summary text describing the declaration.
        /// </summary>
        /// <param name="text">Text for the summary.</param>
        public void Summary(string text)
        {
            if (!text.EndsWith("."))
            {
                throw new StyleException($"Summary text of declaration '{this.Name}' must end with a period -- SA1629.");
            }

            this.summaryLines.Add(text);
        }

        /// <summary>
        /// Add a line of remarks text for the declaration.
        /// </summary>
        /// <param name="text">Text for the remarks.</param>
        public void Remarks(string text)
        {
            if (!text.EndsWith("."))
            {
                throw new StyleException($"Remarks text of declaration '{this.Name}' must end with a period -- SA1629.");
            }

            this.remarksLines.Add(text);
        }

        /// <summary>
        /// Add an attribute to the declaration.
        /// </summary>
        /// <param name="text">Text of the attribute.</param>
        public void Attribute(string text)
        {
            this.attributes.Add(text);
        }

        /// <summary>
        /// Suppress a warning for the scope of the declaration.
        /// </summary>
        /// <param name="code">The warning code.</param>
        /// <param name="message">The warning message.</param>
        public void Suppress(string code, string message = null)
        {
            this.suppressions.Add(message != null ? $"{code} // {message}" : code);
        }

        /// <summary>
        /// Comparison method that implements the declaration order if StyleCop Analyzers.
        /// </summary>
        /// <param name="other">Other <see cref="CsDeclaration"/> to compare with.</param>
        /// <returns>Less than 0 if this sorts first, greater than 0 if other sorts first, 0 if sort identically.</returns>
        public int CompareTo(CsDeclaration other)
        {
            int accessComparison = EffectiveAccess(this.access).CompareTo(EffectiveAccess(other.access));
            if (accessComparison != 0)
            {
                return accessComparison;
            }

            if (this.multiplicity != other.multiplicity)
            {
                return this.multiplicity.CompareTo(other.multiplicity);
            }

            if (this.mutability != other.mutability)
            {
                return this.mutability.CompareTo(other.mutability);
            }

            if (this.Type != null && other.Type != null)
            {
                int typeComparison = this.Type.CompareTo(other.Type);
                if (typeComparison != 0)
                {
                    return typeComparison;
                }
            }

            int nameComparison = this.Name.CompareTo(other.Name);
            return nameComparison;
        }

        /// <summary>
        /// Generate code for the declaration.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for generating the declaration code.</param>
        public virtual void GenerateCode(CodeWriter codeWriter)
        {
        }

        /// <summary>
        /// Write the summary comments for the declaration.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for writing the declaration comments.</param>
        protected void WriteSummary(CodeWriter codeWriter)
        {
            codeWriter.Break();

            if (this.inheritDoc)
            {
                codeWriter.WriteLine("/// <inheritdoc/>");
            }
            else if (this.summaryLines.Any())
            {
                codeWriter.WriteLine("/// <summary>");

                foreach (string summaryLine in this.summaryLines)
                {
                    codeWriter.WriteLine($"/// {summaryLine}");
                }

                codeWriter.WriteLine("/// </summary>");
            }
        }

        /// <summary>
        /// Write the remarks comments for the declaration.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for writing the declaration comments.</param>
        protected void WriteRemarks(CodeWriter codeWriter)
        {
            if (this.remarksLines.Any())
            {
                codeWriter.WriteLine("/// <remarks>");

                foreach (string remarksLine in this.remarksLines)
                {
                    codeWriter.WriteLine($"/// {remarksLine}");
                }

                codeWriter.WriteLine("/// </remarks>");
            }
        }

        /// <summary>
        /// Write the attributes for the declaration.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for writing the declaration attributes.</param>
        protected void WriteAttributes(CodeWriter codeWriter)
        {
            foreach (string attribute in this.attributes)
            {
                codeWriter.WriteLine($"[{attribute}]");
            }
        }

        /// <summary>
        /// Write pragma warning disables for the declaration.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for writing the pragmas.</param>
        protected void WriteDisables(CodeWriter codeWriter)
        {
            foreach (string suppression in this.suppressions)
            {
                codeWriter.WriteJustifiedLine($"#pragma warning disable {suppression}", precedeBreak: false);
            }
        }

        /// <summary>
        /// Write pragma warning restores for the declaration.
        /// </summary>
        /// <param name="codeWriter">A <see cref="CodeWriter"/> object for writing the pragmas.</param>
        protected void WriteRestores(CodeWriter codeWriter)
        {
            foreach (string suppression in this.suppressions)
            {
                codeWriter.WriteJustifiedLine($"#pragma warning restore {suppression}", precedeBreak: true);
            }
        }

        private static Access EffectiveAccess(Access access)
        {
            return access == Access.Implicit ? Access.Public : access;
        }
    }
}
