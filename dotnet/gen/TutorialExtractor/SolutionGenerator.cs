namespace DTDLParser
{
    using System.IO;

    /// <summary>
    /// Generator for tutorials solution file.
    /// </summary>
    public class SolutionGenerator
    {
        private static readonly string[] ProjectGuids =
        {
            "09B5F009-3F9C-4C1E-A491-95403FC03CEA",
            "54C2C037-D3AF-458D-9727-7D5167769B43",
            "83510712-8A08-46AB-8194-76D673E1C321",
            "4FD05CC8-A938-4EEC-9313-0790D2C3E805",
            "43565DEB-1F9B-427A-A550-047A4B9027F3",
            "EBF0383E-C565-4013-AB81-52BCC6AFFE3E",
            "5BCCEA85-66CE-46A3-9241-347078F5F93C",
            "8CA7711F-F1FD-41F7-B759-64215920D4D7",
            "E7FFF723-5695-4FE9-9EC7-BD9E6BB4EF0D",
            "D4CDE9BB-69A2-40B7-9A59-15F9B9EFDBA0",
            "24876D67-74F7-4990-BD69-CE42A23C1F90",
            "568B2B3D-2C7F-4858-9ED0-A1D3E1A84440",
            "07808188-617B-4BFF-8BF6-2EC066F2C212",
            "ADB7F9F9-D45D-414C-A527-8B6C764B0C20",
            "BD677C08-E755-4190-8653-0CFB623BB19D",
            "FAD6C990-2ECB-4F97-B6E1-AB8EDA2CAE78",
            "86C0868C-2843-434D-B381-668234130A9C",
            "FCF5E626-3CF1-452B-BE57-7FD72AFEB650",
            "D71EA67F-7D57-43F4-B4C7-3C90C5D63552",
            "6015ABE3-669F-4201-A0ED-5C699DB29F15",
            "573F7AC4-40E3-4706-8CBB-0679DDE047F5",
            "430F5DA7-DDAF-42E1-B3FF-313FE8DD127B",
            "75E27C0F-B591-4D48-9ACA-AB9C9A85BE6B",
            "9F171531-6429-42E6-AAEB-C9E6C320B8F5",
            "E9AF5711-1171-4BC4-9817-BBCB695CC6BC",
            "B4594A8F-5847-4D35-8D16-4431E38C9341",
            "69D679A9-E85B-4CC6-9584-30E4ECE34F79",
            "9C61A693-EC0C-4DF7-B3F7-8F46BCB5134E",
            "134A2A2E-4E5D-46D2-9B45-372ADFCA6B8C",
            "8FCED641-50EB-4496-9CD1-F5995B865C2B",
            "16123356-CCFC-417A-867B-3DDB05A8E9EB",
            "CA17AA2E-4668-4AA2-8033-ED0AC3DD10D1",
            "06F26045-6A05-46E4-8A0D-ACAD5FCEED72",
            "4145CD11-3B35-49B6-A37E-0F1E4E3D885E",
            "892B7303-3DBD-4020-B97E-CCD7945C7DCD",
            "AB9FDFA2-A867-4C95-9C4C-4C1EC9A9BFBE",
            "428FDF98-B345-4524-88D5-4786FACDA2E4",
            "5244F8D9-E0C2-476F-96F6-53C477B1E8F0",
            "8463BA2E-8661-4602-90A7-91DF979250F5",
            "BDB6085A-A7B9-4C0A-98AA-039A22EA1EC2",
            "1EB6524E-9634-416C-8C46-CAD099D3AFAD",
            "7B4DDA6D-7818-4B36-846E-D876C5C37162",
            "EC5A51D7-7CEC-468D-A90B-8C9AD7AA8776",
            "E02DD136-CCA6-446B-A892-183E2C8DA462",
            "E5E670A3-1BCA-41CA-91A9-4FB7F637905A",
            "13FD390D-F4A0-42CE-8442-699FCD946333",
            "46B1C426-1D13-4589-AB87-01BD40A54ED3",
            "16CA81FC-BAF9-4998-8779-88EC43DDFF1F",
            "EEB8FEFB-1E75-4A33-AB45-5425AD047024",
            "6C8DC98E-2029-4D90-861D-6928ED07F59A",
        };

        private StreamWriter solutionWriter;
        private int projectCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionGenerator"/> class.
        /// </summary>
        /// <param name="solutionFolder">The folder in which to create the solution file.</param>
        public SolutionGenerator(string solutionFolder)
        {
            FileStream solutionStream = new FileStream(Path.Combine(solutionFolder, "Tutorials.sln"), FileMode.Create, FileAccess.Write);
            this.solutionWriter = new StreamWriter(solutionStream);
            this.projectCount = 0;

            this.WriteSolutionHeader();
        }

        /// <summary>
        /// Add a project to the solution.
        /// </summary>
        /// <param name="projectName">The name of the C# project.</param>
        public void AddProject(string projectName)
        {
            string projectGuid = ProjectGuids[this.projectCount];
            this.solutionWriter.WriteLine($"Project(\"{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}\") = \"{projectName}\", \"{projectName}\\{projectName}.csproj\", \"{{{projectGuid}}}\"");
            this.solutionWriter.WriteLine("EndProject");
            ++this.projectCount;
        }

        /// <summary>
        /// Complete the generation of the solution file.
        /// </summary>
        public void Complete()
        {
            this.WriteSolutionTrailer();
            this.solutionWriter.Close();
        }

        private void WriteSolutionHeader()
        {
            this.solutionWriter.WriteLine("Microsoft Visual Studio Solution File, Format Version 12.00");
        }

        private void WriteSolutionTrailer()
        {
            this.solutionWriter.WriteLine("Global");

            this.solutionWriter.WriteLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            this.solutionWriter.WriteLine("\t\tDebug|Any CPU = Debug|Any CPU");
            this.solutionWriter.WriteLine("\t\tRelease|Any CPU = Release|Any CPU");
            this.solutionWriter.WriteLine("\tEndGlobalSection");

            this.solutionWriter.WriteLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");

            for (int projectIndex = 0; projectIndex < this.projectCount; ++projectIndex)
            {
                string projectGuid = ProjectGuids[projectIndex];
                this.solutionWriter.WriteLine($"\t\t{{{projectGuid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
                this.solutionWriter.WriteLine($"\t\t{{{projectGuid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU");
                this.solutionWriter.WriteLine($"\t\t{{{projectGuid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU");
                this.solutionWriter.WriteLine($"\t\t{{{projectGuid}}}.Release|Any CPU.Build.0 = Release|Any CPU");
            }

            this.solutionWriter.WriteLine("\tEndGlobalSection");

            this.solutionWriter.WriteLine("\tGlobalSection(SolutionProperties) = preSolution");
            this.solutionWriter.WriteLine("\t\tHideSolutionNode = FALSE");
            this.solutionWriter.WriteLine("\tEndGlobalSection");

            this.solutionWriter.WriteLine("\tGlobalSection(ExtensibilityGlobals) = postSolution");
            this.solutionWriter.WriteLine("\t\tSolutionGuid = {B228B783-2C30-42C1-86B1-69EB08F92E31}");
            this.solutionWriter.WriteLine("\tEndGlobalSection");

            this.solutionWriter.WriteLine("EndGlobal");
        }
    }
}
