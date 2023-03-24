namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>ParserUnitTest</c> unit-test support class.
    /// </summary>
    public class ParserUnitTestGenerator
    {
        private readonly string className;
        private readonly bool doBatch;
        private readonly List<string> testCases;
        private readonly Dictionary<char, List<string>> testCaseBatches;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserUnitTestGenerator"/> class.
        /// </summary>
        /// <param name="className">Name for the generated class.</param>
        /// <param name="doBatch">True if the test cases should be split into batches by leading character of the test case name.</param>
        public ParserUnitTestGenerator(string className, bool doBatch)
        {
            this.className = className;
            this.doBatch = doBatch;
            this.testCases = new List<string>();
            this.testCaseBatches = new Dictionary<char, List<string>>();
        }

        /// <summary>
        /// Add a test case to the unit tests.
        /// </summary>
        /// <param name="testCase">Name of the test case.</param>
        public void AddTestCase(string testCase)
        {
            if (this.doBatch)
            {
                char batch = char.ToUpperInvariant(testCase[0]);
                if (!this.testCaseBatches.TryGetValue(batch, out List<string> batchCases))
                {
                    batchCases = new List<string>();
                    this.testCaseBatches[batch] = batchCases;
                }

                batchCases.Add(testCase);
            }
            else
            {
                this.testCases.Add(testCase);
            }
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass parserUnitTestClass = parserLibrary.Class(Access.Public, Novelty.Normal, this.className, completeness: Completeness.Partial);
            parserUnitTestClass.Attribute("TestClass");
            parserUnitTestClass.Summary("A class for testing the <see cref=\"ModelParser\"/> using JSON test cases.");

            if (this.doBatch)
            {
                foreach (KeyValuePair<char, List<string>> kvp in this.testCaseBatches)
                {
                    GenerateTestMethod(parserUnitTestClass, $"TestParser_{kvp.Key}", kvp.Value, $"This method runs all test cases beginning with letter '{kvp.Key}'.");
                }
            }
            else
            {
                GenerateTestMethod(parserUnitTestClass, "TestParserAll", this.testCases);
            }
        }

        private static void GenerateTestMethod(CsClass parserUnitTestClass, string methodName, List<string> testCases, string remark = null)
        {
            testCases.Sort();

            CsMethod testParserMethod = parserUnitTestClass.Method(Access.Public, Novelty.Normal, "void", methodName);

            testParserMethod.Attribute("TestMethod");
            foreach (string testCase in testCases)
            {
                testParserMethod.Attribute($"DataRow(\"{testCase}\")");
            }

            testParserMethod.Summary("Execute a test case against the ModelParser.");
            testParserMethod.Param("string", "testName", "The name of the test case.");

            if (remark != null)
            {
                testParserMethod.Remarks(remark);
            }

            testParserMethod.Body.Line("TestParser(testName);");
        }
    }
}
