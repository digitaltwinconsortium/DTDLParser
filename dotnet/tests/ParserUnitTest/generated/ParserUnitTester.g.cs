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

    internal static partial class ParserUnitTester
    {
        private static ModelParser GetModelParser(int? maxDtdlVersion, WhenToAllow allowUndefinedExtensions, ModelParsingQuirk quirks, JObject testCaseObject, bool useAsyncApi, int maxReadConcurrency, out IResolutionChecker resolutionChecker, out IResolutionChecker partialResolutionChecker)
        {
            TestDtmiResolver dtmiResolver = new TestDtmiResolver(testCaseObject, useAsyncApi);
            resolutionChecker = dtmiResolver;
            partialResolutionChecker = null;
            var parsingOptions = new ParsingOptions()
            {
                DtmiResolver = dtmiResolver.GetResolver(),
                DtmiResolverAsync = dtmiResolver.GetResolverAsync(),
                DtdlResolveLocator = LocateForResolve,
                AllowUndefinedExtensions = allowUndefinedExtensions,
            };

            if (maxDtdlVersion != null)
            {
                parsingOptions.MaxDtdlVersion = (int)maxDtdlVersion;
            }

            return new ModelParser(parsingOptions, quirks);
        }

        private static void ExecuteTestCase(JObject testCaseObject, bool useAsyncApi, bool suppressResubmission)
        {
            JToken optionsToken = testCaseObject["options"];
            int remainingOptionCount = optionsToken.Count();

            bool allowUndefinedExtensions = optionsToken.Any(t => ((JValue)t).Value<string>() == "AllowUndefinedExtensions");
            remainingOptionCount -= allowUndefinedExtensions ? 1 : 0;

            bool disallowUndefinedExtensions = optionsToken.Any(t => ((JValue)t).Value<string>() == "DisallowUndefinedExtensions");
            remainingOptionCount -= disallowUndefinedExtensions ? 1 : 0;

            WhenToAllow allowUndefinedExtensionsInTest = allowUndefinedExtensions ? WhenToAllow.Always : disallowUndefinedExtensions ? WhenToAllow.Never : WhenToAllow.PerDefault;

            if (remainingOptionCount > 0)
            {
                Assert.Inconclusive("Unrecognized ModelParsingOption");
            }

            bool hasQuirks = testCaseObject.TryGetValue("quirks", out JToken quirkToken);
            bool hasUnrecognizedQuirk = hasQuirks && quirkToken.Any(t => !Enum.TryParse(((JValue)t).Value<string>(), out ModelParsingQuirk _));
            if (hasUnrecognizedQuirk)
            {
                Assert.Inconclusive("Unrecognized ModelParsingQuirk");
            }

            bool valid = ((JValue)testCaseObject["valid"]).Value<bool>();
            ModelParsingQuirk quirks = hasQuirks ? quirkToken.Select(t => (ModelParsingQuirk)Enum.Parse(typeof(ModelParsingQuirk), ((JValue)t).Value<string>())).Aggregate(ModelParsingQuirk.None, (x, y) => x | y) : ModelParsingQuirk.None;
            int? maxDtdlVersion = testCaseObject.ContainsKey("maxDtdlVersion") ? ((JValue)testCaseObject["maxDtdlVersion"]).Value<int?>() : null;
            int maxReadConcurrency = testCaseObject.ContainsKey("maxReadConcurrency") ? ((JValue)testCaseObject["maxReadConcurrency"]).Value<int>() : 0;

            bool inputGiven = testCaseObject.TryGetValue("input", out JToken inputToken);

            ModelParser modelParser = GetModelParser(maxDtdlVersion, allowUndefinedExtensionsInTest, quirks, testCaseObject, useAsyncApi, maxReadConcurrency, out IResolutionChecker resolutionChecker, out IResolutionChecker partialResolutionChecker);
            if (testCaseObject.TryGetValue("extensions", out JToken loadToken))
            {
                TestExtension(modelParser, loadToken, testCaseObject, extensionValid: valid || inputGiven);
            }

            if (inputGiven)
            {
                TestInput(modelParser, inputToken, valid, quirks, testCaseObject, useAsyncApi, suppressResubmission, resolutionChecker, partialResolutionChecker);
            }
        }

        private static void TestExtension(ModelParser modelParser, JToken loadToken, JObject testCaseObject, bool extensionValid)
        {
            Assert.Fail("Test case specifies extensions to load, but dynamic extensions are not supported.");
        }

        private static void TestInput(ModelParser modelParser, JToken inputToken, bool valid, ModelParsingQuirk quirks, JObject testCaseObject, bool useAsyncApi, bool suppressResubmission, IResolutionChecker resolutionChecker, IResolutionChecker partialResolutionChecker)
        {
            TestModel testModel = new TestModel(modelParser, ((JArray)inputToken).Select(t => GetJsonTextFromToken(t)), LocateForParse, useAsyncApi);
            string parseMethodName = useAsyncApi ? "ParseAsync" : "Parse";

            try
            {
                testModel.DoParse();
                if (!valid)
                {
                    Assert.Fail($"Expected exception not thrown by {parseMethodName}()");
                }

                if (testCaseObject.TryGetValue("expect", out JToken expectationToken))
                {
                    CheckObjectModel(testModel, (JObject)expectationToken);
                }

                if (testCaseObject.TryGetValue("instances", out JToken instancesToken))
                {
                    ValidateInstances(testModel, (JArray)instancesToken);
                }

                bool tolerateSolecismsInParse = (quirks & ModelParsingQuirk.TolerateSolecismsInParse) != 0;
                bool tolerateSolecismsInResolve = (quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0;
                bool parseAndResolveConcurOnSolecisms = tolerateSolecismsInParse == tolerateSolecismsInResolve;

                if (!suppressResubmission && parseAndResolveConcurOnSolecisms)
                {
                    TestModel resubmittedTestModel = new TestModel(modelParser, testModel.GetDtdl(), LocateForParse, useAsyncApi);
                    resubmittedTestModel.DoParse();

                    if (expectationToken != null)
                    {
                        CheckObjectModel(resubmittedTestModel, (JObject)expectationToken);
                    }

                    Assert.IsTrue(resubmittedTestModel.IsModelEquivalent(testModel));
                }
            }
            catch (Exception ex)
            {
                if (ex is AssertFailedException)
                {
                    throw;
                }

                if (valid)
                {
                    Assert.Fail($"Exception unexpectedly thrown by {parseMethodName}(): {string.Join(", ", ((ParsingException)ex).Errors.Select(e => $"{e.ValidationID.AbsoluteUri} -- {e.Cause}") ?? new List<string>())}");
                }

                if (testCaseObject.TryGetValue("expect", out JToken expectationToken))
                {
                    CheckException(ex, (JObject)expectationToken);
                }
            }

            resolutionChecker.Check(valid);
        }
    }
}
