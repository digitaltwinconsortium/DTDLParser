namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.IO;

    /// <summary>
    /// Class for writing lines of generated code to a file.
    /// </summary>
    public class CodeWriter
    {
        private const string Indentation = "    ";

        private readonly StreamWriter streamWriter;
        private readonly IndentedTextWriter indentedTextWriter;

        private bool nextTextNeedsBlank;
        private bool lastLineWasText;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeWriter"/> class.
        /// </summary>
        /// <param name="filePath">Full path of file to generate.</param>
        public CodeWriter(string filePath)
        {
            this.streamWriter = new StreamWriter(filePath);
            this.indentedTextWriter = new IndentedTextWriter(this.streamWriter, Indentation);
            this.indentedTextWriter.Indent = 0;

            this.nextTextNeedsBlank = false;
            this.lastLineWasText = false;
        }

        /// <summary>
        /// Close the file and flush writes.
        /// </summary>
        public void Close()
        {
            this.indentedTextWriter.Close();
            this.streamWriter.Close();
        }

        /// <summary>
        /// Indicate that a blank line is appropriate between lines of code.
        /// </summary>
        public void Break()
        {
            if (this.lastLineWasText)
            {
                this.nextTextNeedsBlank = true;
                this.lastLineWasText = false;
            }
        }

        /// <summary>
        /// Write an open brace and increase indent level.
        /// </summary>
        public void OpenScope()
        {
            this.indentedTextWriter.WriteLine("{");
            this.indentedTextWriter.Indent++;
            this.nextTextNeedsBlank = false;
            this.lastLineWasText = false;
        }

        /// <summary>
        /// Decrease indent level and Write a close brace.
        /// </summary>
        /// <param name="terminate">True if the scope should be terminated with a semicolon; defaults to false.</param>
        public void CloseScope(bool terminate = false)
        {
            this.indentedTextWriter.Indent--;
            this.indentedTextWriter.WriteLine(terminate ? "};" : "}");
            this.nextTextNeedsBlank = true;
            this.lastLineWasText = false;
        }

        /// <summary>
        /// Increase indent level.
        /// </summary>
        public void IncreaseIndent()
        {
            this.indentedTextWriter.Indent++;
        }

        /// <summary>
        /// Decrease indent level.
        /// </summary>
        public void DecreaseIndent()
        {
            this.indentedTextWriter.Indent--;
        }

        /// <summary>
        /// Write a line of code.
        /// </summary>
        /// <param name="text">Code text to write.</param>
        /// <param name="suppressBreak">True if there should be no blank line preceding the text.</param>
        /// <param name="outdent">True if the line should be outdented one level.</param>
        public void WriteLine(string text, bool suppressBreak = false, bool outdent = false)
        {
            if (this.nextTextNeedsBlank)
            {
                if (!suppressBreak)
                {
                    this.indentedTextWriter.WriteLineNoTabs(string.Empty);
                }

                this.nextTextNeedsBlank = false;
            }

            if (outdent)
            {
                this.indentedTextWriter.Indent--;
            }

            this.indentedTextWriter.WriteLine(text);

            if (outdent)
            {
                this.indentedTextWriter.Indent++;
            }

            this.lastLineWasText = true;
        }

        /// <summary>
        /// Write a line of left-justified text.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="precedeBreak">True if the line should precede any upcoming blank line; false if the line should follow any upcoming blank line.</param>
        public void WriteJustifiedLine(string text, bool precedeBreak)
        {
            if (this.nextTextNeedsBlank && !precedeBreak)
            {
                this.indentedTextWriter.WriteLineNoTabs(string.Empty);
                this.indentedTextWriter.WriteLineNoTabs(text);
                this.nextTextNeedsBlank = false;
            }
            else
            {
                this.indentedTextWriter.WriteLineNoTabs(text);
            }
        }
    }
}
