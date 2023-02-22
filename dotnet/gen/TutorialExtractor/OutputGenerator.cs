namespace DTDLParser
{
    using System.IO;

    /// <summary>
    /// Generator for tutorial expected output file.
    /// </summary>
    public class OutputGenerator
    {
        private StreamReader tutorialReader;
        private StreamWriter outputWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputGenerator"/> class.
        /// </summary>
        /// <param name="projectFolder">The folder in which to create the project file.</param>
        /// <param name="tutorialReader">A <c>StreamReader</c> from which to read lines of a tutorial Markdown file.</param>
        public OutputGenerator(string projectFolder, StreamReader tutorialReader)
        {
            this.tutorialReader = tutorialReader;

            FileStream outputStream = new FileStream(Path.Combine(projectFolder, "expect.txt"), FileMode.Create, FileAccess.Write);
            this.outputWriter = new StreamWriter(outputStream);
        }

        /// <summary>
        /// Add expected output text from the Markdown file.
        /// </summary>
        public void AddOutputText()
        {
            string markdownLine = this.tutorialReader.ReadLine();
            while (markdownLine != "```")
            {
                this.outputWriter.WriteLine(markdownLine);
                markdownLine = this.tutorialReader.ReadLine();
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
