/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;
    using DTDLParser.Models;

    /// <summary>
    /// A class for testing the <see cref="ModelParser"/> using JSON test cases.
    /// </summary>
    [TestClass]
    public class DocExampleUnitTest
    {
        private const string TestFolder = "../../../../../../test-cases/doc-examples";

        /// <summary>
        /// Execute a test case against the ModelParser.
        /// </summary>
        /// <param name="testName">The name of the test case.</param>
        [TestMethod]
        [DataRow("annotationDocExample-en-US-1-V1")]
        [DataRow("annotationDocExample-en-US-2-V1")]
        [DataRow("ArrayDocExample-en-US-1-V2")]
        [DataRow("ArrayDocExample-en-US-1-V3")]
        [DataRow("CommandDocExample-en-US-1-V2")]
        [DataRow("CommandDocExample-en-US-1-V3")]
        [DataRow("ComponentDocExample-en-US-1-V2")]
        [DataRow("ComponentDocExample-en-US-1-V3")]
        [DataRow("EnumDocExample-en-US-1-V2")]
        [DataRow("EnumDocExample-en-US-1-V3")]
        [DataRow("historizationDocExample-en-US-1-V1")]
        [DataRow("historizationDocExample-en-US-2-V1")]
        [DataRow("initializationDocExample-en-US-1-V1")]
        [DataRow("initializationDocExample-en-US-2-V1")]
        [DataRow("InterfaceDocExample-en-US-1-V2")]
        [DataRow("InterfaceDocExample-en-US-1-V3")]
        [DataRow("InterfaceDocExample-en-US-2-V2")]
        [DataRow("InterfaceDocExample-en-US-2-V3")]
        [DataRow("InterfaceDocExample-en-US-3-V2")]
        [DataRow("InterfaceDocExample-en-US-3-V3")]
        [DataRow("InterfaceDocExample-en-US-4-V2")]
        [DataRow("InterfaceDocExample-en-US-4-V3")]
        [DataRow("iotcentralDocExample-en-US-1-V2")]
        [DataRow("iotcentralDocExample-en-US-2-V2")]
        [DataRow("iotcentralDocExample-en-US-3-V2")]
        [DataRow("iotcentralDocExample-en-US-4-V2")]
        [DataRow("iotcentralDocExample-en-US-5-V2")]
        [DataRow("iotcentralDocExample-en-US-6-V2")]
        [DataRow("MapDocExample-en-US-1-V2")]
        [DataRow("MapDocExample-en-US-1-V3")]
        [DataRow("ObjectDocExample-en-US-1-V2")]
        [DataRow("ObjectDocExample-en-US-1-V3")]
        [DataRow("optionalityDocExample-en-US-1-V1")]
        [DataRow("optionalityDocExample-en-US-2-V1")]
        [DataRow("overridingDocExample-en-US-1-V1")]
        [DataRow("overridingDocExample-en-US-2-V1")]
        [DataRow("PropertyDocExample-en-US-1-V2")]
        [DataRow("PropertyDocExample-en-US-1-V3")]
        [DataRow("PropertyDocExample-en-US-2-V2")]
        [DataRow("quantitativeTypesDocExample-en-US-1-V1")]
        [DataRow("quantitativeTypesDocExample-en-US-2-V1")]
        [DataRow("RelationshipDocExample-en-US-1-V2")]
        [DataRow("RelationshipDocExample-en-US-1-V3")]
        [DataRow("RelationshipDocExample-en-US-2-V2")]
        [DataRow("RelationshipDocExample-en-US-2-V3")]
        [DataRow("RelationshipDocExample-en-US-3-V2")]
        [DataRow("RelationshipDocExample-en-US-3-V3")]
        [DataRow("SpecExample-Array-V2")]
        [DataRow("SpecExample-Array-V3")]
        [DataRow("SpecExample-Command-V2")]
        [DataRow("SpecExample-Command-V3")]
        [DataRow("SpecExample-CommandPayload-V2")]
        [DataRow("SpecExample-CommandRequest-V3")]
        [DataRow("SpecExample-CommandResponse-V3")]
        [DataRow("SpecExample-Component-V2")]
        [DataRow("SpecExample-Component-V3")]
        [DataRow("SpecExample-Enum-V2")]
        [DataRow("SpecExample-Enum-V3")]
        [DataRow("SpecExample-EnumValue-V2")]
        [DataRow("SpecExample-EnumValue-V3")]
        [DataRow("SpecExample-Field-V2")]
        [DataRow("SpecExample-Field-V3")]
        [DataRow("SpecExample-Interface-V2")]
        [DataRow("SpecExample-Interface-V3")]
        [DataRow("SpecExample-Map-V2")]
        [DataRow("SpecExample-Map-V3")]
        [DataRow("SpecExample-MapKey-V2")]
        [DataRow("SpecExample-MapKey-V3")]
        [DataRow("SpecExample-MapValue-V2")]
        [DataRow("SpecExample-MapValue-V3")]
        [DataRow("SpecExample-Object-V2")]
        [DataRow("SpecExample-Object-V3")]
        [DataRow("SpecExample-Property-V2")]
        [DataRow("SpecExample-Property-V3")]
        [DataRow("SpecExample-Relationship-V2")]
        [DataRow("SpecExample-Relationship-V3")]
        [DataRow("SpecExample-Telemetry-V2")]
        [DataRow("SpecExample-Telemetry-V3")]
        [DataRow("streamingDocExample-en-US-1-V1")]
        [DataRow("TelemetryDocExample-en-US-1-V2")]
        [DataRow("TelemetryDocExample-en-US-1-V3")]
        [DataRow("TelemetryDocExample-en-US-2-V2")]
        [DataRow("UnspecifiedDocExample-en-US-1-V2")]
        [DataRow("UnspecifiedDocExample-en-US-1-V3")]
        [DataRow("UnspecifiedDocExample-en-US-10-V2")]
        [DataRow("UnspecifiedDocExample-en-US-10-V3")]
        [DataRow("UnspecifiedDocExample-en-US-11-V2")]
        [DataRow("UnspecifiedDocExample-en-US-11-V3")]
        [DataRow("UnspecifiedDocExample-en-US-12-V3")]
        [DataRow("UnspecifiedDocExample-en-US-13-V3")]
        [DataRow("UnspecifiedDocExample-en-US-14-V3")]
        [DataRow("UnspecifiedDocExample-en-US-15-V3")]
        [DataRow("UnspecifiedDocExample-en-US-16-V3")]
        [DataRow("UnspecifiedDocExample-en-US-2-V2")]
        [DataRow("UnspecifiedDocExample-en-US-2-V3")]
        [DataRow("UnspecifiedDocExample-en-US-3-V2")]
        [DataRow("UnspecifiedDocExample-en-US-3-V3")]
        [DataRow("UnspecifiedDocExample-en-US-4-V2")]
        [DataRow("UnspecifiedDocExample-en-US-4-V3")]
        [DataRow("UnspecifiedDocExample-en-US-5-V2")]
        [DataRow("UnspecifiedDocExample-en-US-5-V3")]
        [DataRow("UnspecifiedDocExample-en-US-6-V2")]
        [DataRow("UnspecifiedDocExample-en-US-6-V3")]
        [DataRow("UnspecifiedDocExample-en-US-7-V2")]
        [DataRow("UnspecifiedDocExample-en-US-7-V3")]
        [DataRow("UnspecifiedDocExample-en-US-8-V2")]
        [DataRow("UnspecifiedDocExample-en-US-8-V3")]
        [DataRow("UnspecifiedDocExample-en-US-9-V2")]
        [DataRow("UnspecifiedDocExample-en-US-9-V3")]
        public void TestDocExamples(string testName)
        {
            string filePath = Path.Combine(TestFolder, $"{testName}.json");
            StreamReader testReader = new StreamReader(filePath);
            string testText = testReader.ReadToEnd();
            ParserUnitTester.ExecuteTest(testText, useAsyncApi: false);
        }
    }
}
