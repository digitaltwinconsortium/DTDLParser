namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Generator for a series of C# code lines whose code-generation order is determined by lexicographical sort rather than the order in which they are added.
    /// </summary>
    /// <remarks>
    /// A <see cref="CsSorted"/> object is appropriate for blocks of code for which:
    /// (a) lines are added in arbitrary order, perhaps by multiple sources,
    /// (b) the order of the lines is not important for correctness, and
    /// (c) a consistent order aids readability and eases diffing across generated versions.
    /// </remarks>
    public class CsSorted : CsStatement
    {
        private List<CsStatement> csStatements;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsSorted"/> class.
        /// </summary>
        public CsSorted()
        {
            this.csStatements = new List<CsStatement>();
        }

        /// <inheritdoc/>
        internal override string SortingText
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Add a line of text to the multi-line statement.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsSorted Line(string text)
        {
            this.csStatements.Add(new CsLine(text));
            return this;
        }

        /// <summary>
        /// Add a nested multiple-line statement within the multi-line statement.
        /// </summary>
        /// <param name="subHeadText">Text for the first line of the nested statement.</param>
        /// <returns>The <see cref="CsMultiLine"/> object added.</returns>
        public CsMultiLine MultiLine(string subHeadText)
        {
            CsMultiLine csMultiLine = new CsMultiLine(subHeadText);
            this.csStatements.Add(csMultiLine);
            return csMultiLine;
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            this.csStatements.Sort((left, right) => left.SortingText.CompareTo(right.SortingText));

            foreach (CsStatement csStatement in this.csStatements)
            {
                csStatement.GenerateCode(codeWriter);
            }
        }
    }
}
