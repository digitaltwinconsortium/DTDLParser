namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Generator for sample program file.
    /// </summary>
    public class ProgramGenerator
    {
        private string markdownFilename;
        private string projectName;
        private StreamReader sampleReader;
        private StreamWriter codeWriter;
        private Dictionary<string, StringBuilder> snippets;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramGenerator"/> class.
        /// </summary>
        /// <param name="projectFolder">The folder in which to create the project file.</param>
        /// <param name="markdownFilename">The name of the Markdown file from which the snippets are extracted.</param>
        /// <param name="projectName">The name of the C# project.</param>
        /// <param name="sampleReader">A <c>StreamReader</c> from which to read lines of a sample Markdown file.</param>
        public ProgramGenerator(string projectFolder, string markdownFilename, string projectName, StreamReader sampleReader)
        {
            this.markdownFilename = markdownFilename;
            this.projectName = projectName;
            this.sampleReader = sampleReader;

            FileStream codeStream = new FileStream(Path.Combine(projectFolder, "Program.g.cs"), FileMode.Create, FileAccess.Write);
            this.codeWriter = new StreamWriter(codeStream);
            this.snippets = new Dictionary<string, StringBuilder>();

            this.AnyErrors = false;

            this.WriteCodeHeader();
        }

        /// <summary>
        /// Gets a value indicating whether there were any errors encountered.
        /// </summary>
        public bool AnyErrors { get; private set; }

        /// <summary>
        /// Add a code snippet from the Markdown file.
        /// </summary>
        /// <param name="markdownLine">Leading line of the code snippet from the Markdown file.</param>
        public void AddCodeSnippet(string markdownLine)
        {
            string snippetName = markdownLine.Substring(6);
            StringBuilder snippet = new StringBuilder();

            this.codeWriter.WriteLine();
            this.codeWriter.WriteLine($"            #region {snippetName}");

            markdownLine = this.sampleReader.ReadLine();
            while (markdownLine != "```")
            {
                string snippetLine = markdownLine == string.Empty ? string.Empty : $"            {markdownLine}";
                snippet.AppendLine(snippetLine);
                this.codeWriter.WriteLine(snippetLine);
                markdownLine = this.sampleReader.ReadLine();
            }

            if (!snippetName.Contains($"{this.projectName}_"))
            {
                this.DisplayError($"Mismatched snippet name: {snippetName}");
            }

            if (!this.snippets.TryAdd(snippetName, snippet))
            {
                this.DisplayError($"Duplicate snippet name: {snippetName}");
            }

            this.codeWriter.WriteLine("            #endregion");
        }

        /// <summary>
        /// Repeat a previously added code snippet.
        /// </summary>
        /// <param name="markdownLine">Comment line in the Markdown file indicating which code snippet to repeat.</param>
        public void RepeatCodeSnippet(string markdownLine)
        {
            string snippetName = markdownLine.Substring(13, markdownLine.Length - 14);

            if (this.snippets.TryGetValue(snippetName, out StringBuilder snippetBuilder))
            {
                this.codeWriter.WriteLine();
                this.codeWriter.Write(snippetBuilder);
            }
            else
            {
                this.DisplayError($"Reference to missing snippet: {snippetName}");
            }
        }

        /// <summary>
        /// Complete the generation of the project file.
        /// </summary>
        public void Complete()
        {
            this.WriteCodeTrailer();
            this.codeWriter.Close();
        }

        private void WriteCodeHeader()
        {
            this.codeWriter.WriteLine($"/* This file was automatically generated from code snippets embedded in file {this.markdownFilename} */");
            this.codeWriter.WriteLine($"/* The associated project file {this.projectName}.csproj and expected output file expect.txt were also automatically generated. */");
            this.codeWriter.WriteLine();
            this.codeWriter.WriteLine("using System;");
            this.codeWriter.WriteLine("using System.Collections;");
            this.codeWriter.WriteLine("using System.Collections.Generic;");
            this.codeWriter.WriteLine("using System.Linq;");
            this.codeWriter.WriteLine("using System.Reflection;");
            this.codeWriter.WriteLine("using System.Text.Json;");
            this.codeWriter.WriteLine("using System.Threading;");
            this.codeWriter.WriteLine("using System.Threading.Tasks;");
            this.codeWriter.WriteLine("using DTDLParser;");
            this.codeWriter.WriteLine("using DTDLParser.Models;");
            this.codeWriter.WriteLine();
            this.codeWriter.WriteLine($"namespace {this.projectName}");
            this.codeWriter.WriteLine("{");

            this.codeWriter.WriteLine("    class Program");
            this.codeWriter.WriteLine("    {");

            this.codeWriter.WriteLine("        static void Main(string[] args)");
            this.codeWriter.Write("        {");
        }

        private void WriteCodeTrailer()
        {
            this.codeWriter.WriteLine("        }");
            this.codeWriter.WriteLine("    }");
            this.codeWriter.WriteLine("}");
        }

        private void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{this.projectName}: {message}");
            Console.ResetColor();

            this.AnyErrors = true;
        }
    }
}
