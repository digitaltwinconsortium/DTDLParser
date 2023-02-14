namespace DTDLParser
{
    using System.IO;

    /// <summary>
    /// A class for validating the <see cref="ModelParser"/> using tests from the DTDL specification.
    /// </summary>
    public partial class SpecificationUnitTest
    {
        private const string TestFolder = "../../../../../../test-cases/specification";

        private void TestParser(string testName)
        {
            string filePath = Path.Combine(TestFolder, $"{testName}.json");
            StreamReader testReader = new StreamReader(filePath);
            string testText = testReader.ReadToEnd();
            ParserUnitTester.ExecuteTest(testText, useAsyncApi: false, suppressResubmission: true);
        }
    }
}
