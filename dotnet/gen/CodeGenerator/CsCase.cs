namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# case statement.
    /// </summary>
    public class CsCase : CsStatement
    {
        private string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsCase"/> class.
        /// </summary>
        /// <param name="value">Case value.</param>
        public CsCase(string value)
        {
            this.value = value;
        }

        /// <inheritdoc/>
        internal override string SortingText
        {
            get
            {
                return string.Empty;
            }
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            codeWriter.WriteLine($"case {this.value}:", outdent: true);
        }
    }
}
