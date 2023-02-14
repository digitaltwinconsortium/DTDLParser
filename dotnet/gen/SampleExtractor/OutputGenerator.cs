namespace DTDLParser
{
    using System.IO;

    /// <summary>
    /// Generator for sample expected output file.
    /// </summary>
    public class OutputGenerator
    {
        private StreamReader sampleReader;
        private StreamWriter outputWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputGenerator"/> class.
        /// </summary>
        /// <param name="projectFolder">The folder in which to create the project file.</param>
        /// <param name="sampleReader">A <c>StreamReader</c> from which to read lines of a sample Markdown file.</param>
        public OutputGenerator(string projectFolder, StreamReader sampleReader)
        {
            this.sampleReader = sampleReader;

            FileStream outputStream = new FileStream(Path.Combine(projectFolder, "expect.txt"), FileMode.Create, FileAccess.Write);
            this.outputWriter = new StreamWriter(outputStream);
        }

        /// <summary>
        /// Add expected output text from the Markdown file.
        /// </summary>
        public void AddOutputText()
        {
            string markdownLine = this.sampleReader.ReadLine();
            while (markdownLine != "```")
            {
                this.outputWriter.WriteLine(markdownLine);
                markdownLine = this.sampleReader.ReadLine();
            }
        }

        /// <summary>
        /// Complete the generation of the project file.
        /// </summary>
        public void Complete()
        {
            this.outputWriter.Close();
        }
    }
}
