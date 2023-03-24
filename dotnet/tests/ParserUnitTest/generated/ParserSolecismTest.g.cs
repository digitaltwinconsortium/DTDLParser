/* This is an auto-generated file.  Do not modify. */

namespace ParserUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;
    using DTDLParser;
    using DTDLParser.Models;

    /// <summary>
    /// A class for testing the <see cref="ModelParser"/> using JSON test cases.
    /// </summary>
    [TestClass]
    public partial class ParserSolecismTest
    {
        /// <summary>
        /// Execute a test case against the ModelParser.
        /// </summary>
        /// <param name="testName">The name of the test case.</param>
        [TestMethod]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnParseV2")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnParseV3")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnParseWithParseToleratingV2")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnParseWithParseToleratingV3")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnParseWithResolveToleratingV2")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnParseWithResolveToleratingV3")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnResolveV2")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnResolveV3")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnResolveWithParseToleratingV2")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnResolveWithParseToleratingV3")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnResolveWithResolveToleratingV2")]
        [DataRow("InterfaceContentsDuplicateNameAutogenIdOnResolveWithResolveToleratingV3")]
        [DataRow("InterfaceExtendsIdReferenceInvalidDtmiV2")]
        [DataRow("InterfaceExtendsIdReferenceInvalidDtmiV3")]
        [DataRow("InterfaceExtendsIdReferenceInvalidDtmiWithQuirkToTolerateV2")]
        [DataRow("InterfaceExtendsIdReferenceInvalidDtmiWithQuirkToTolerateV3")]
        [DataRow("InterfaceExtendsIdReferenceOnParseV2")]
        [DataRow("InterfaceExtendsIdReferenceOnParseV3")]
        [DataRow("InterfaceExtendsIdReferenceOnParseWithParseToleratingV2")]
        [DataRow("InterfaceExtendsIdReferenceOnParseWithParseToleratingV3")]
        [DataRow("InterfaceExtendsIdReferenceOnParseWithResolveToleratingV2")]
        [DataRow("InterfaceExtendsIdReferenceOnParseWithResolveToleratingV3")]
        [DataRow("InterfaceExtendsIdReferenceOnResolveV2")]
        [DataRow("InterfaceExtendsIdReferenceOnResolveV3")]
        [DataRow("InterfaceExtendsIdReferenceOnResolveWithParseToleratingV2")]
        [DataRow("InterfaceExtendsIdReferenceOnResolveWithParseToleratingV3")]
        [DataRow("InterfaceExtendsIdReferenceOnResolveWithResolveToleratingV2")]
        [DataRow("InterfaceExtendsIdReferenceOnResolveWithResolveToleratingV3")]
        [DataRow("InterfaceIdDtdlPrefixV2")]
        [DataRow("InterfaceIdDtdlPrefixV3")]
        [DataRow("InterfaceIdDtdlPrefixWithQuirkToTolerateV2")]
        [DataRow("InterfaceIdDtdlPrefixWithQuirkToTolerateV3")]
        [DataRow("InterfaceIdStandardPrefixV2")]
        [DataRow("InterfaceIdStandardPrefixV3")]
        [DataRow("InterfaceIdStandardPrefixWithQuirkToTolerateV2")]
        [DataRow("InterfaceIdStandardPrefixWithQuirkToTolerateV3")]
        [DataRow("InterfaceTelemetryIdDtdlPrefixV2")]
        [DataRow("InterfaceTelemetryIdDtdlPrefixV3")]
        [DataRow("InterfaceTelemetryIdDtdlPrefixWithQuirkToTolerateV2")]
        [DataRow("InterfaceTelemetryIdDtdlPrefixWithQuirkToTolerateV3")]
        [DataRow("InterfaceTelemetryIdStandardPrefixV2")]
        [DataRow("InterfaceTelemetryIdStandardPrefixV3")]
        [DataRow("InterfaceTelemetryIdStandardPrefixWithQuirkToTolerateV2")]
        [DataRow("InterfaceTelemetryIdStandardPrefixWithQuirkToTolerateV3")]
        public void TestParserAll(string testName)
        {
            TestParser(testName);
        }
    }
}
