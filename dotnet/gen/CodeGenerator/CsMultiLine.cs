namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Generator for a multiple-line C# code statement.
    /// </summary>
    public class CsMultiLine : CsStatement
    {
        private string headText;
        private List<CsStatement> csStatements;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsMultiLine"/> class.
        /// </summary>
        /// <param name="headText">Text for the first line of the statement.</param>
        public CsMultiLine(string headText)
        {
            this.headText = headText;
            this.csStatements = new List<CsStatement>();
        }

        /// <inheritdoc/>
        internal override string SortingText
        {
            get
            {
                return this.headText;
            }
        }

        /// <summary>
        /// Add a line of text to the multi-line statement.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsMultiLine Line(string text)
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

        /// <summary>
        /// Add to the multi-line statement a sequence of statements that will be lexicographically sorted at code-generation time.
        /// </summary>
        /// <returns>The <see cref="CsSorted"/> object added.</returns>
        public CsSorted Sorted()
        {
            CsSorted csSorted = new CsSorted();
            this.csStatements.Add(csSorted);
            return csSorted;
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            codeWriter.WriteLine(this.headText);
            codeWriter.IncreaseIndent();

            foreach (CsStatement csStatement in this.csStatements)
            {
                csStatement.GenerateCode(codeWriter);
            }

            codeWriter.DecreaseIndent();
        }
    }
}
