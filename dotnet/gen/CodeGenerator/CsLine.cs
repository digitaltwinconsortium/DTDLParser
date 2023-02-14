namespace DTDLParser
{
    /// <summary>
    /// Generator for a line of C# code.
    /// </summary>
    public class CsLine : CsStatement
    {
        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsLine"/> class.
        /// </summary>
        /// <param name="text">Code text.</param>
        public CsLine(string text)
        {
            this.text = text;
        }

        /// <inheritdoc/>
        internal override string SortingText
        {
            get
            {
                return this.text;
            }
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            codeWriter.WriteLine(this.text);
        }
    }
}
