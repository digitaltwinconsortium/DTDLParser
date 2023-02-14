namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>ParserUnitTest</c> unit-test support class.
    /// </summary>
    public class ParserUnitTestGenerator
    {
        private readonly Dictionary<char, List<string>> testCases;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserUnitTestGenerator"/> class.
        /// </summary>
        public ParserUnitTestGenerator()
        {
            this.testCases = new Dictionary<char, List<string>>();
        }

        /// <summary>
        /// Add a test case to the unit tests.
        /// </summary>
        /// <param name="testCase">Name of the test case.</param>
        public void AddTestCase(string testCase)
        {
            char batch = char.ToUpperInvariant(testCase[0]);
            if (!this.testCases.TryGetValue(batch, out List<string> batchCases))
            {
                batchCases = new List<string>();
                this.testCases[batch] = batchCases;
            }

            batchCases.Add(testCase);
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass parserUnitTestClass = parserLibrary.Class(Access.Public, Novelty.Normal, "ParserUnitTest", completeness: Completeness.Partial);
            parserUnitTestClass.Attribute("TestClass");
            parserUnitTestClass.Summary("A class for testing the <see cref=\"ModelParser\"/> using JSON test cases.");

            foreach (KeyValuePair<char, List<string>> kvp in this.testCases)
            {
                kvp.Value.Sort();

                CsMethod testParserMethod = parserUnitTestClass.Method(Access.Public, Novelty.Normal, "void", $"TestParser_{kvp.Key}");

                testParserMethod.Attribute("TestMethod");
                foreach (string testCase in kvp.Value)
                {
                    testParserMethod.Attribute($"DataRow(\"{testCase}\")");
                }

                testParserMethod.Summary("Execute a test case against the ModelParser.");
                testParserMethod.Param("string", "testName", "The name of the test case.");
                testParserMethod.Remarks($"This method runs all test cases beginning with letter '{kvp.Key}'.");
                testParserMethod.Body.Line("TestParser(testName);");
            }
        }
    }
}
