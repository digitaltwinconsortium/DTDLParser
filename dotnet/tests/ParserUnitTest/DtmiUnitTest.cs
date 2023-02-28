namespace ParserUnitTest
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DtmiUnitTest
    {
        const string TestFolder = "../../../../../../test-cases/dtmis";

        [TestMethod]
        [DataRow("DtmiFragment")]
        [DataRow("DtmiFragmentEmpty")]
        [DataRow("DtmiFragmentIllegalChar")]
        [DataRow("DtmiFragmentLeadingDigit")]
        [DataRow("DtmiFragmentLeadingUnderscore")]
        [DataRow("DtmiFragmentTrailingUnderscore")]
        [DataRow("DtmiNoSegments")]
        [DataRow("DtmiNoVersion")]
        [DataRow("DtmiOneSegment")]
        [DataRow("DtmiSchemeHttp")]
        [DataRow("DtmiSchemeUrn")]
        [DataRow("DtmiSegmentEmpty")]
        [DataRow("DtmiSegmentIllegalChar")]
        [DataRow("DtmiSegmentLeadingDigit")]
        [DataRow("DtmiSegmentLeadingUnderscore")]
        [DataRow("DtmiSegmentTrailingUnderscore")]
        [DataRow("DtmiVersionEmpty")]
        [DataRow("DtmiVersionEmptyPlusFragment")]
        [DataRow("DtmiVersionLarge")]
        [DataRow("DtmiVersionLeadingZero")]
        [DataRow("DtmiVersionMajorZero")]
        [DataRow("DtmiVersionMaxLong")]
        [DataRow("DtmiVersionMinorLeadingZero")]
        [DataRow("DtmiVersionMinorMaxLong")]
        [DataRow("DtmiVersionMinorTooLong")]
        [DataRow("DtmiVersionMinorZero")]
        [DataRow("DtmiVersionNonNumeric")]
        [DataRow("DtmiVersionSmall")]
        [DataRow("DtmiVersionTooLong")]
        [DataRow("DtmiVersionTwoPart")]
        [DataRow("DtmiVersionTwoPartPlusFragment")]
        [DataRow("DtmiVersionTwoPartZero")]
        [DataRow("DtmiVersionZero")]
        [DataRow("DtmiVeryLong")]
        public void TestDtmi(string testName)
        {
            string filePath = Path.Combine(TestFolder, $"{testName}.json");
            StreamReader testReader = new StreamReader(filePath);
            string testText = testReader.ReadToEnd();
            DtmiUnitTester.ExecuteTest(testText);
        }
    }
}
