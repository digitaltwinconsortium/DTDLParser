namespace DTDLParser
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>SpecificationUnitTest</c> unit-test support class.
    /// </summary>
    public class SpecificationUnitTestGenerator
    {
        private const int VersionOffset = 1;
        private const int VersionWidth = 1;

        private readonly Dictionary<int, Dictionary<string, List<string>>> testCases;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationUnitTestGenerator"/> class.
        /// </summary>
        public SpecificationUnitTestGenerator()
        {
            this.testCases = new Dictionary<int, Dictionary<string, List<string>>>();
        }

        /// <summary>
        /// Add a test case to the unit tests.
        /// </summary>
        /// <param name="testCase">Name of the test case.</param>
        public void AddTestCase(string testCase)
        {
            string batch = testCase.Substring(0, testCase.IndexOf("-"));
            int version = int.Parse(testCase.Substring(testCase.Length - VersionOffset, VersionWidth));

            if (!this.testCases.TryGetValue(version, out Dictionary<string, List<string>> batchVersions))
            {
                batchVersions = new Dictionary<string, List<string>>();
                this.testCases[version] = batchVersions;
            }

            if (!batchVersions.TryGetValue(batch, out List<string> batchCases))
            {
                batchCases = new List<string>();
                batchVersions[batch] = batchCases;
            }

            batchCases.Add(testCase);
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass specificationUnitTestClass = parserLibrary.Class(Access.Public, Novelty.Normal, "SpecificationUnitTest", completeness: Completeness.Partial);
            specificationUnitTestClass.Attribute("TestClass");
            specificationUnitTestClass.Summary("A class for validating the <see cref=\"ModelParser\"/> using tests from the DTDL specification.");

            foreach (KeyValuePair<int, Dictionary<string, List<string>>> kvp1 in this.testCases)
            {
                foreach (KeyValuePair<string, List<string>> kvp2 in kvp1.Value)
                {
                    kvp2.Value.Sort();

                    CsMethod runSpecificationTestsMethod = specificationUnitTestClass.Method(Access.Public, Novelty.Normal, "void", $"TestParser_{kvp2.Key}_{kvp1.Key}");

                    runSpecificationTestsMethod.Attribute("TestMethod");

                    foreach (string testCase in kvp2.Value)
                    {
                        runSpecificationTestsMethod.Attribute($"DataRow(\"{testCase}\")");
                    }

                    runSpecificationTestsMethod.Summary("Execute a test against the ModelParser.");
                    runSpecificationTestsMethod.Param("string", "testName", "The name of the test.");
                    runSpecificationTestsMethod.Remarks($"This method runs all V{kvp1.Key} test cases for '{kvp2.Key}' stipulations.");
                    runSpecificationTestsMethod.Body.Line("TestParser(testName);");
                }
            }
        }
    }
}
