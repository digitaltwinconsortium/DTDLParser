namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# default statement.
    /// </summary>
    public class CsDefault : CsStatement
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
            codeWriter.WriteLine("default:", outdent: true);
        }
    }
}
