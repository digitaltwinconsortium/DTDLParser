namespace DTDLParser
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Main program for TutorialExtractor.
    /// </summary>
    internal static class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                if (args.Count() < 2)
                {
                    Console.WriteLine("TutorialExtractor projectsFolder tutorialsConfig");
                    return 1;
                }

                string projectsFolder = args[0];
                string tutorialsConfig = args[1];

                if (!Directory.Exists(projectsFolder))
                {
                    Directory.CreateDirectory(projectsFolder);
                }

                SolutionGenerator solutionGenerator = new SolutionGenerator(projectsFolder);

                int tutorialCount = 0;
                int resultCode = 0;

                foreach (string tutorialFilepath in new FileEnumeration(tutorialsConfig))
                {
                    string tutorialFilename = Path.GetFileName(tutorialFilepath);
                    string projectName = GetProjectName(tutorialFilepath);
                    string projectFolder = Path.Combine(projectsFolder, projectName);
                    if (!Directory.Exists(projectFolder))
                    {
                        Directory.CreateDirectory(projectFolder);
                    }

                    solutionGenerator.AddProject(projectName);

                    FileStream tutorialStream = new FileStream(tutorialFilepath, FileMode.Open, FileAccess.Read);
                    StreamReader tutorialReader = new StreamReader(tutorialStream);

                    ProjectGenerator projectGenerator = new ProjectGenerator(projectFolder, projectName);
                    ProgramGenerator programGenerator = new ProgramGenerator(projectFolder, tutorialFilename, projectName, tutorialReader);
                    OutputGenerator outputGenerator = new OutputGenerator(projectFolder, tutorialReader);

                    string markdownLine = tutorialReader.ReadLine();
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

                        markdownLine = tutorialReader.ReadLine();
                    }

                    projectGenerator.Complete();
                    programGenerator.Complete();
                    outputGenerator.Complete();

                    tutorialReader.Close();

                    ++tutorialCount;

                    if (programGenerator.AnyErrors)
                    {
                        resultCode = 1;
                    }
                }

                Console.WriteLine($"Generated tutorial projects from {tutorialCount} tutorial Markdown files");

                solutionGenerator.Complete();

                return resultCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TutorialExtractor failed with exception: {ex.Message}");
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
