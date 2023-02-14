namespace DTDLParser
{
    /// <summary>
    /// Generator for a break between statements.
    /// </summary>
    public class CsBreak : CsStatement
    {
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
            codeWriter.Break();
        }
    }
}
