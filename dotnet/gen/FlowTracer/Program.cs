namespace DTDLParser
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Main program for FlowTracer.
    /// </summary>
    internal static class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("FlowTracer shFolder dotFile");
                    return 1;
                }

                string shFolder = args[0];
                string dotFile = args[1];

                ExtractGraphComponentsFromShFolder(
                    shFolder,
                    out Dictionary<string, string> steps,
                    out Dictionary<string, string> artifacts,
                    out List<string> edges,
                    out Dictionary<string, string> inJobs,
                    out Dictionary<string, string> outJobs);

                OutputGraphToDotFile(dotFile, steps, artifacts, edges, inJobs, outJobs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FlowTracer failed with exception: {ex.Message}");
                return 1;
            }

            return 0;
        }

        private static void ExtractGraphComponentsFromShFolder(
            string shFolder,
            out Dictionary<string, string> steps,
            out Dictionary<string, string> artifacts,
            out List<string> edges,
            out Dictionary<string, string> inJobs,
            out Dictionary<string, string> outJobs)
        {
            steps = new Dictionary<string, string>();
            artifacts = new Dictionary<string, string>();
            edges = new List<string>();
            inJobs = new Dictionary<string, string>();
            outJobs = new Dictionary<string, string>();

            foreach (string cmdFilename in Directory.GetFiles(shFolder, "*.sh", SearchOption.TopDirectoryOnly))
            {
                string job = null;
                string step = '"' + Path.GetFileNameWithoutExtension(cmdFilename) + '"';

                StreamReader cmdFileReader = new StreamReader(cmdFilename);

                string cmdLine = cmdFileReader.ReadLine();
                while (cmdLine != null)
                {
                    string artifact;
                    if (TryGetJob(cmdLine, ref job))
                    {
                        steps[step] = job;
                    }
                    else if (TryGetArtifact("in", cmdLine, ref artifacts, out artifact))
                    {
                        edges.Add($"{artifact} -> {step}");
                        if (!inJobs.TryAdd(artifact, job) && inJobs[artifact] != job)
                        {
                            inJobs[artifact] = null;
                        }
                    }
                    else if (TryGetArtifact("out", cmdLine, ref artifacts, out artifact))
                    {
                        edges.Add($"{step} -> {artifact}");
                        outJobs[artifact] = job;
                    }

                    cmdLine = cmdFileReader.ReadLine();
                }

                cmdFileReader.Close();
            }
        }

        private static void OutputGraphToDotFile(
            string dotFile,
            Dictionary<string, string> steps,
            Dictionary<string, string> artifacts,
            List<string> edges,
            Dictionary<string, string> inJobs,
            Dictionary<string, string> outJobs)
        {
            FileStream dotStream = new FileStream($"{dotFile}", FileMode.Create, FileAccess.Write);
            StreamWriter dotWriter = new StreamWriter(dotStream);
            IndentedTextWriter indentedDotWriter = new IndentedTextWriter(dotWriter, "  ");

            indentedDotWriter.WriteLine("digraph {");
            indentedDotWriter.Indent++;

            OutputNodesToDotWriter(indentedDotWriter, steps, artifacts, inJobs, outJobs);

            foreach (string edge in edges)
            {
                indentedDotWriter.WriteLine(edge);
            }

            indentedDotWriter.Indent--;
            indentedDotWriter.WriteLine("}");

            indentedDotWriter.Close();
        }

        private static void OutputNodesToDotWriter(
            IndentedTextWriter indentedDotWriter,
            Dictionary<string, string> steps,
            Dictionary<string, string> artifacts,
            Dictionary<string, string> inJobs,
            Dictionary<string, string> outJobs)
        {
            indentedDotWriter.WriteLine("{");
            indentedDotWriter.Indent++;

            foreach (KeyValuePair<string, string> step in steps)
            {
                indentedDotWriter.WriteLine($"{step.Key} [shape=cylinder style=filled fillcolor={GetColorFromJob(step.Value)}]");
            }

            foreach (KeyValuePair<string, string> artifact in artifacts)
            {
                string job = outJobs.TryGetValue(artifact.Key, out string outJob) ? outJob : inJobs[artifact.Key];
                indentedDotWriter.WriteLine($"{artifact.Key} [shape={artifact.Value} style=filled fillcolor={GetColorFromJob(job)}]");
            }

            indentedDotWriter.Indent--;
            indentedDotWriter.WriteLine("}");
        }

        private static bool TryGetJob(string cmdLine, ref string job)
        {
            if (cmdLine.StartsWith("#:: job "))
            {
                string[] tokens = cmdLine.Split(' ');
                job = tokens[2];
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool TryGetArtifact(string direction, string cmdLine, ref Dictionary<string, string> artifacts, out string artifact)
        {
            if (cmdLine.StartsWith($"#:: {direction} "))
            {
                string[] tokens = cmdLine.Split(' ');
                artifact = '"' + tokens[3] + '"';
                artifacts.TryAdd(artifact, GetShapeFromCode(tokens[2]));
                return true;
            }
            else
            {
                artifact = null;
                return false;
            }
        }

        private static string GetShapeFromCode(string code)
        {
            switch (code)
            {
                case "json":
                    return "box";
                case "dotnet":
                    return "folder";
                case "exe":
                case "dll":
                    return "ellipse";
                case "nupkg":
                    return "box3d";
                case "md":
                    return "note";
                case "xml":
                case "html":
                    return "hexagon";
                default:
                    return null;
            }
        }

        private static string GetColorFromJob(string job)
        {
            switch (job)
            {
                case "dtdl":
                    return "lightgray";
                case "dotnet":
                    return "chartreuse";
                default:
                    return "white";
            }
        }
    }
}
