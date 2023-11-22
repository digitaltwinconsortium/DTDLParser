namespace DTDLParser
{
    using System.IO;

    /// <summary>
    /// Generator for tutorial project file.
    /// </summary>
    public class ProjectGenerator
    {
        private StreamWriter projectWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectGenerator"/> class.
        /// </summary>
        /// <param name="projectFolder">The folder in which to create the project file.</param>
        /// <param name="projectName">The name of the C# project.</param>
        public ProjectGenerator(string projectFolder, string projectName)
        {
            FileStream projectStream = new FileStream(Path.Combine(projectFolder, $"{projectName}.csproj"), FileMode.Create, FileAccess.Write);
            this.projectWriter = new StreamWriter(projectStream);
            this.projectWriter.WriteLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            this.projectWriter.WriteLine();
            this.projectWriter.WriteLine("  <PropertyGroup>");
            this.projectWriter.WriteLine("    <OutputType>Exe</OutputType>");
            this.projectWriter.WriteLine("    <TargetFramework>net8.0</TargetFramework>");
            this.projectWriter.WriteLine("  </PropertyGroup>");
            this.projectWriter.WriteLine();
            this.projectWriter.WriteLine("  <ItemGroup>");
            this.projectWriter.WriteLine(@"    <ProjectReference Include=""..\..\..\dotnet\src\DTDLParser\DTDLParser.csproj"" />");
            this.projectWriter.WriteLine("  </ItemGroup>");

            this.projectWriter.WriteLine();
            this.projectWriter.WriteLine("</Project>");
        }

        /// <summary>
        /// Complete the generation of the project file.
        /// </summary>
        public void Complete()
        {
            this.projectWriter.Close();
        }
    }
}
