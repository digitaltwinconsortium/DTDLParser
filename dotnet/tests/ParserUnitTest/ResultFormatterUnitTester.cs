using DTDLParser;

namespace ParserUnitTest
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A class for testing the <see cref="ResultFormatter"/> class using JSON test cases.
    /// </summary>
    internal class ResultFormatterUnitTester
    {
        /// <summary>
        /// Execute a test case against the <see cref="ResultFormatter"/> class.
        /// </summary>
        /// <param name="testCaseText">JSON text of the test case to execute.</param>
        public static void ExecuteTest(string testCaseText)
        {
            JArray testCaseArray = (JArray)JToken.Parse(testCaseText);

            foreach (JToken testCaseToken in testCaseArray)
            {
                JObject testCaseObject = (JObject)testCaseToken;

                string phrase = ((JValue)testCaseObject["phrase"]).Value<string>();
                JObject installObj = (JObject)testCaseObject["install"];
                string expect = ((JValue)testCaseObject["expect"]).Value<string>();

                ResultFormatter resultFormatter = new ResultFormatter(phrase);
                foreach (KeyValuePair<string, JToken> kvp in installObj)
                {
                    resultFormatter.Install(kvp.Key, ((JValue)kvp.Value).Value<string>());
                }

                Assert.AreEqual(expect, resultFormatter.ToString());
            }
        }
    }
}
