using DTDLParser;

namespace ParserUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A class for testing the <see cref="Dtmi"/> class using JSON test cases.
    /// </summary>
    internal class DtmiUnitTester
    {
        /// <summary>
        /// Execute a test case against the <see cref="Dtmi"/> class.
        /// </summary>
        /// <param name="testCaseText">JSON text of the test case to execute.</param>
        public static void ExecuteTest(string testCaseText)
        {
            JObject testCaseObject = (JObject)JToken.Parse(testCaseText);

            bool valid = ((JValue)testCaseObject["valid"]).Value<bool>();
            string idText = ((JValue)testCaseObject["idText"]).Value<string>();

            if (valid)
            {
                try
                {
                    JObject expectationObject = (JObject)testCaseObject["expect"];

                    Dtmi dtmi = null;
                    Assert.IsTrue(Dtmi.TryCreateDtmi(idText, out dtmi));
                    CheckDtmiProperties(dtmi, expectationObject);

                    Dtmi newDtmi = new Dtmi(idText);
                    CheckDtmiProperties(newDtmi, expectationObject);
                }
                catch (ParsingException)
                {
                    Assert.Fail("Exception unexpectedly thrown");
                }
            }
            else
            {
                Dtmi dtmi;
                Assert.IsFalse(Dtmi.TryCreateDtmi(idText, out dtmi));

                Assert.ThrowsException<ParsingException>(() => new Dtmi(idText));
            }
        }

        private static void CheckDtmiProperties(Dtmi dtmi, JObject expectationObject)
        {
            if (expectationObject.ContainsKey("MajorVersion"))
            {
                int majorVersion = ((JValue)expectationObject["MajorVersion"]).Value<int>();
                Assert.AreEqual(majorVersion, dtmi.MajorVersion);
            }

            if (expectationObject.ContainsKey("MinorVersion"))
            {
                int minorVersion = ((JValue)expectationObject["MinorVersion"]).Value<int>();
                Assert.AreEqual(minorVersion, dtmi.MinorVersion);
            }

            if (expectationObject.ContainsKey("CompleteVersion"))
            {
                double completeVersion = ((JValue)expectationObject["CompleteVersion"]).Value<double>();
                Assert.AreEqual(completeVersion, dtmi.CompleteVersion);
            }

            if (expectationObject.ContainsKey("Versionless"))
            {
                string versionless = ((JValue)expectationObject["Versionless"]).Value<string>();
                Assert.AreEqual(versionless, dtmi.Versionless);
            }

            if (expectationObject.ContainsKey("Labels"))
            {
                JArray labels = (JArray)expectationObject["Labels"];
                Assert.AreEqual(labels.Count, dtmi.Labels.Count());

                List<string> expectedStrings = labels.Select(t => ((JValue)t).Value<string>()).ToList();
                Assert.IsTrue(expectedStrings.SequenceEqual(dtmi.Labels));
            }

            if (expectationObject.ContainsKey("IsReserved"))
            {
                bool isReserved = ((JValue)expectationObject["IsReserved"]).Value<bool>();
                Assert.AreEqual(isReserved, dtmi.IsReserved);
            }

            if (expectationObject.ContainsKey("Fragment"))
            {
                string fragment = ((JValue)expectationObject["Fragment"]).Value<string>();
                Assert.AreEqual(fragment, dtmi.Fragment);
            }
        }
    }
}
