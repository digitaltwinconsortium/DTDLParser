using DTDLParser;

namespace ParserUnitTest
{
    using System.IO;

    /// <summary>
    /// A class for testing the <see cref="ModelParser"/> using JSON test cases.
    /// </summary>
    public partial class ParserUnitTest
    {
        private const string TestFolder = "../../../../../../test-cases/generated";

        private void TestParser(string testName)
        {
            string filePath = Path.Combine(TestFolder, $"{testName}.json");
            StreamReader testReader = new StreamReader(filePath);
            string testText = testReader.ReadToEnd();
            ParserUnitTester.ExecuteTest(testText, useAsyncApi: false);
            ParserUnitTester.ExecuteTest(testText, useAsyncApi: true);
        }
    }
}
