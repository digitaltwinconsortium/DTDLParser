namespace DTDLParser
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Main program for SampleExtractor.
    /// </summary>
    internal static class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                if (args.Count() < 2)
                {
                    Console.WriteLine("SampleExtractor projectsFolder samplesConfig");
                    return 1;
                }

                string projectsFolder = args[0];
                string samplesConfig = args[1];

                if (!Directory.Exists(projectsFolder))
                {
                    Directory.CreateDirectory(projectsFolder);
                }

                SolutionGenerator solutionGenerator = new SolutionGenerator(projectsFolder);

                int sampleCount = 0;
                int resultCode = 0;

                foreach (string sampleFilepath in new FileEnumeration(samplesConfig))
                {
                    string sampleFilename = Path.GetFileName(sampleFilepath);
                    string projectName = GetProjectName(sampleFilepath);
                    string projectFolder = Path.Combine(projectsFolder, projectName);
                    if (!Directory.Exists(projectFolder))
                    {
                        Directory.CreateDirectory(projectFolder);
                    }

                    solutionGenerator.AddProject(projectName);

                    FileStream sampleStream = new FileStream(sampleFilepath, FileMode.Open, FileAccess.Read);
                    StreamReader sampleReader = new StreamReader(sampleStream);

                    ProjectGenerator projectGenerator = new ProjectGenerator(projectFolder, projectName);
                    ProgramGenerator programGenerator = new ProgramGenerator(projectFolder, sampleFilename, projectName, sampleReader);
                    OutputGenerator outputGenerator = new OutputGenerator(projectFolder, sampleReader);

                    string markdownLine = sampleReader.ReadLine();
                    while (markdownLine != null)
                    {
                        if (markdownLine.StartsWith("```C# Snippet:"))
                        {
                            programGenerator.AddCodeSnippet(markdownLine);
                        }
                        else if (markdownLine.StartsWith("[repeat]: #"))
                        {
                            programGenerator.RepeatCodeSnippet(markdownLine);
                        }
                        else if (markdownLine == "```Console")
                        {
                            outputGenerator.AddOutputText();
                        }

                        markdownLine = sampleReader.ReadLine();
                    }

                    projectGenerator.Complete();
                    programGenerator.Complete();
                    outputGenerator.Complete();

                    sampleReader.Close();

                    ++sampleCount;

                    if (programGenerator.AnyErrors)
                    {
                        resultCode = 1;
                    }
                }

                Console.WriteLine($"Generated sample projects from {sampleCount} sample Markdown files");

                solutionGenerator.Complete();

                return resultCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SampleExtractor failed with exception: {ex.Message}");
                return 1;
            }
        }

        private static string GetProjectName(string filepath)
        {
            string filename = Path.GetFileName(filepath);
            int underIndex = filename.LastIndexOf('_');
            string baseProjName = filename.Substring(0, underIndex);

            string projSuffix = filename.EndsWith("Async.md") ? "Async" : string.Empty;

            return baseProjName + projSuffix;
        }
    }
}
