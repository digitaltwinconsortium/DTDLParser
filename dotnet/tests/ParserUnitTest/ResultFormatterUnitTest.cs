namespace ParserUnitTest
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResultFormatterUnitTest
    {
        const string TestFolder = "../../../../../../test-cases/phrases";

        [TestMethod]
        [DataRow("PhrasesWithUndecoratedIdentifer")]
        [DataRow("PhrasesWithNominativeIdentifer")]
        [DataRow("PhrasesWithPossessiveIdentifer")]
        [DataRow("PhrasesWithRelativeIdentifer")]
        [DataRow("PhrasesWithElectiveIdentifer")]
        [DataRow("PhrasesWithMultipleIdentifers")]
        public void TestResultFormatter(string testName)
        {
            string filePath = Path.Combine(TestFolder, $"{testName}.json");
            StreamReader testReader = new StreamReader(filePath);
            string testText = testReader.ReadToEnd();
            ResultFormatterUnitTester.ExecuteTest(testText);
        }
    }
}
