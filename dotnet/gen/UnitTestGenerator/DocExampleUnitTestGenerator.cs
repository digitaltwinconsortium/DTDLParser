namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>DocExampleUnitTest</c> unit-test support class.
    /// </summary>
    public class DocExampleUnitTestGenerator
    {
        private readonly List<string> testCases;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocExampleUnitTestGenerator"/> class.
        /// </summary>
        public DocExampleUnitTestGenerator()
        {
            this.testCases = new List<string>();
        }

        /// <summary>
        /// Add a test case to the unit tests.
        /// </summary>
        /// <param name="testCase">Name of the test case.</param>
        public void AddTestCase(string testCase)
        {
            this.testCases.Add(testCase);
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass docExampleUnitTestClass = parserLibrary.Class(Access.Public, Novelty.Normal, "DocExampleUnitTest");
            docExampleUnitTestClass.Attribute("TestClass");
            docExampleUnitTestClass.Summary("A class for testing the <see cref=\"ModelParser\"/> using JSON test cases.");

            docExampleUnitTestClass.Constant(Access.Private, "string", "TestFolder", "\"../../../../../../test-cases/doc-examples\"");

            this.testCases.Sort();

            CsMethod testDocExamplesMethod = docExampleUnitTestClass.Method(Access.Public, Novelty.Normal, "void", "TestDocExamples");

            testDocExamplesMethod.Attribute("TestMethod");
            foreach (string testCase in this.testCases)
            {
                testDocExamplesMethod.Attribute($"DataRow(\"{testCase}\")");
            }

            testDocExamplesMethod.Summary("Execute a test case against the ModelParser.");
            testDocExamplesMethod.Param("string", "testName", "The name of the test case.");
            testDocExamplesMethod.Body
                .Line("string filePath = Path.Combine(TestFolder, $\"{testName}.json\");")
                .Line("StreamReader testReader = new StreamReader(filePath);")
                .Line("string testText = testReader.ReadToEnd();")
                .Line("ParserUnitTester.ExecuteTest(testText, useAsyncApi: false);");
        }
    }
}
