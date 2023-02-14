namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>ParserUnitTester</c> partial class.
    /// </summary>
    public class ParserUnitTesterGenerator
    {
        private readonly bool areDynamicExtensionsSupported;
        private readonly bool isLayeringSupported;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserUnitTesterGenerator"/> class.
        /// </summary>
        /// <param name="areDynamicExtensionsSupported">True if dynamic extensions are supported by any recognized language version.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public ParserUnitTesterGenerator(bool areDynamicExtensionsSupported, bool isLayeringSupported)
        {
            this.areDynamicExtensionsSupported = areDynamicExtensionsSupported;
            this.isLayeringSupported = isLayeringSupported;
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass parserUnitTesterClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "ParserUnitTester", Multiplicity.Static, Completeness.Partial);

            this.GenerateExecuteTestCaseMethod(parserUnitTesterClass);
            this.GenerateGetModelParserMethod(parserUnitTesterClass);
            this.GenerateTestExtensionMethod(parserUnitTesterClass);
            this.GenerateTestInputMethod(parserUnitTesterClass);
        }

        private void GenerateExecuteTestCaseMethod(CsClass parserUnitTesterClass)
        {
            CsMethod executeTestCaseMethod = parserUnitTesterClass.Method(Access.Private, Novelty.Normal, "void", "ExecuteTestCase", Multiplicity.Static);
            executeTestCaseMethod.Param("JObject", "testCaseObject");
            executeTestCaseMethod.Param("bool", "useAsyncApi");
            executeTestCaseMethod.Param("bool", "suppressResubmission");

            executeTestCaseMethod.Body
                .Line("JToken optionsToken = testCaseObject[\"options\"];")
                .Line("int remainingOptionCount = optionsToken.Count();")
                .Break();

            executeTestCaseMethod.Body
                .Line("bool allowUndefinedExtensions = optionsToken.Any(t => ((JValue)t).Value<string>() == \"AllowUndefinedExtensions\");")
                .Line("remainingOptionCount -= allowUndefinedExtensions ? 1 : 0;")
                .Break();

            executeTestCaseMethod.Body
                .If("remainingOptionCount > 0")
                    .Line("Assert.Inconclusive(\"Unrecognized ModelParsingOption\");");

            executeTestCaseMethod.Body
                .Line("bool hasQuirks = testCaseObject.TryGetValue(\"quirks\", out JToken quirkToken);")
                .Line("bool hasUnrecognizedQuirk = hasQuirks && quirkToken.Any(t => !Enum.TryParse(((JValue)t).Value<string>(), out ModelParsingQuirk _));")
                .If("hasUnrecognizedQuirk")
                    .Line("Assert.Inconclusive(\"Unrecognized ModelParsingQuirk\");");

            executeTestCaseMethod.Body
                .Line("bool valid = ((JValue)testCaseObject[\"valid\"]).Value<bool>();")
                .Line("ModelParsingQuirk quirks = hasQuirks ? quirkToken.Select(t => (ModelParsingQuirk)Enum.Parse(typeof(ModelParsingQuirk), ((JValue)t).Value<string>())).Aggregate(ModelParsingQuirk.None, (x, y) => x | y) : ModelParsingQuirk.None;")
                .Line("int? maxDtdlVersion = testCaseObject.ContainsKey(\"maxDtdlVersion\") ? ((JValue)testCaseObject[\"maxDtdlVersion\"]).Value<int?>() : null;")
                .Line("int maxReadConcurrency = testCaseObject.ContainsKey(\"maxReadConcurrency\") ? ((JValue)testCaseObject[\"maxReadConcurrency\"]).Value<int>() : 0;")
                .Break();

            executeTestCaseMethod.Body
                .Line("bool inputGiven = testCaseObject.TryGetValue(\"input\", out JToken inputToken);")
                .Break();

            CsScope modelParserScope = this.GetModelParser(executeTestCaseMethod.Body);

            modelParserScope
                .If("testCaseObject.TryGetValue(\"extensions\", out JToken loadToken)")
                    .Line("TestExtension(modelParser, loadToken, testCaseObject, extensionValid: valid || inputGiven);");

            modelParserScope
                .If("inputGiven")
                    .Line("TestInput(modelParser, inputToken, valid, quirks, testCaseObject, useAsyncApi, suppressResubmission, resolutionChecker, partialResolutionChecker);");
        }

        private void GenerateGetModelParserMethod(CsClass parserUnitTesterClass)
        {
            CsMethod getModelParserMethod = parserUnitTesterClass.Method(Access.Private, Novelty.Normal, "ModelParser", "GetModelParser", Multiplicity.Static);
            getModelParserMethod.Param("int?", "maxDtdlVersion");
            getModelParserMethod.Param("bool", "allowUndefinedExtensions");
            getModelParserMethod.Param("ModelParsingQuirk", "quirks");
            getModelParserMethod.Param("JObject", "testCaseObject");
            getModelParserMethod.Param("bool", "useAsyncApi");
            getModelParserMethod.Param("int", "maxReadConcurrency");
            getModelParserMethod.Param("IResolutionChecker", "resolutionChecker", passage: Passage.Out);
            getModelParserMethod.Param("IResolutionChecker", "partialResolutionChecker", passage: Passage.Out);

            getModelParserMethod.Body
                .Line("TestDtmiResolver dtmiResolver = new TestDtmiResolver(testCaseObject, useAsyncApi);")
                .Line("resolutionChecker = dtmiResolver;");

            if (this.isLayeringSupported)
            {
                getModelParserMethod.Body
                    .Line("TestDtmiPartialResolver dtmiPartialResolver = new TestDtmiPartialResolver(testCaseObject, useAsyncApi);")
                    .Line("partialResolutionChecker = dtmiPartialResolver;");
            }
            else
            {
                getModelParserMethod.Body.Line("partialResolutionChecker = null;");
            }

            CsScope parsingOptionsScope = getModelParserMethod.Body.Scope("var parsingOptions = new ParsingOptions()", terminate: true);
            parsingOptionsScope
                .Line("DtmiResolver = dtmiResolver.GetResolver(),")
                .Line("DtmiResolverAsync = dtmiResolver.GetResolverAsync(),")
                .Line("DtdlResolveLocator = LocateForResolve,")
                .Line("AllowUndefinedExtensions = allowUndefinedExtensions,");

            getModelParserMethod.Body.If("maxDtdlVersion != null")
                .Line("parsingOptions.MaxDtdlVersion = (int)maxDtdlVersion;");

            if (this.isLayeringSupported)
            {
                parsingOptionsScope.Line("DtmiPartialResolver = dtmiPartialResolver.GetResolver(),");
                parsingOptionsScope.Line("DtmiPartialResolverAsync = dtmiPartialResolver.GetResolverAsync(),");
            }

            string optionalFirstArg = this.areDynamicExtensionsSupported ? "maxReadConcurrency, " : string.Empty;
            getModelParserMethod.Body.Line($"return new ModelParser({optionalFirstArg}parsingOptions, quirks);");
        }

        private void GenerateTestExtensionMethod(CsClass parserUnitTesterClass)
        {
            CsMethod testExtensionMethod = parserUnitTesterClass.Method(Access.Private, Novelty.Normal, "void", "TestExtension", Multiplicity.Static);
            testExtensionMethod.Param("ModelParser", "modelParser");
            testExtensionMethod.Param("JToken", "loadToken");
            testExtensionMethod.Param("JObject", "testCaseObject");
            testExtensionMethod.Param("bool", "extensionValid");

            if (this.areDynamicExtensionsSupported)
            {
                CsTry tryLoad = testExtensionMethod.Body.Try();
                tryLoad.Line("modelParser.LoadExtensions(((JArray)loadToken).Select(t => GetJsonTextFromToken(t)));");
                tryLoad.If("!extensionValid")
                    .Line("Assert.Fail(\"Expected exception not thrown by Load()\");");
                tryLoad.If("testCaseObject.TryGetValue(\"expect\", out JToken expectationToken)")
                    .Line("CheckModelParser(modelParser, (JObject)expectationToken);");

                CsCatch catchParsingException = tryLoad.Catch("ParsingException pe");
                catchParsingException.If("extensionValid")
                    .Line("Assert.Fail($\"Exception unexpectedly thrown by Load(): {string.Join(\", \", pe.Errors.Select(e => $\"{e.ValidationID.AbsoluteUri} -- {e.Cause}\") ?? new List<string>())}\");");
                catchParsingException.If("testCaseObject.TryGetValue(\"expect\", out JToken expectationToken)")
                    .Line("CheckException(pe, (JObject)expectationToken);");
            }
            else
            {
                testExtensionMethod.Body.Line("Assert.Fail(\"Test case specifies extensions to load, but dynamic extensions are not supported.\");");
            }
        }

        private void GenerateTestInputMethod(CsClass parserUnitTesterClass)
        {
            CsMethod testInputMethod = parserUnitTesterClass.Method(Access.Private, Novelty.Normal, "void", "TestInput", Multiplicity.Static);
            testInputMethod.Param("ModelParser", "modelParser");
            testInputMethod.Param("JToken", "inputToken");
            testInputMethod.Param("bool", "valid");
            testInputMethod.Param("ModelParsingQuirk", "quirks");
            testInputMethod.Param("JObject", "testCaseObject");
            testInputMethod.Param("bool", "useAsyncApi");
            testInputMethod.Param("bool", "suppressResubmission");
            testInputMethod.Param("IResolutionChecker", "resolutionChecker");
            testInputMethod.Param("IResolutionChecker", "partialResolutionChecker");

            testInputMethod.Body
                .Line("TestModel testModel = new TestModel(modelParser, ((JArray)inputToken).Select(t => GetJsonTextFromToken(t)), LocateForParse, useAsyncApi);")
                .Line("string parseMethodName = useAsyncApi ? \"ParseAsync\" : \"Parse\";")
                .Break();

            CsTry tryParse = testInputMethod.Body.Try();

            tryParse.Line("testModel.DoParse();");

            tryParse.If("!valid").Line("Assert.Fail($\"Expected exception not thrown by {parseMethodName}()\");");

            tryParse.If("testCaseObject.TryGetValue(\"expect\", out JToken expectationToken)").Line("CheckObjectModel(testModel, (JObject)expectationToken);");

            tryParse.If("testCaseObject.TryGetValue(\"instances\", out JToken instancesToken)").Line("ValidateInstances(testModel, (JArray)instancesToken);");

            tryParse
                .Line("bool tolerateSolecismsInParse = (quirks & ModelParsingQuirk.TolerateSolecismsInParse) != 0;")
                .Line("bool tolerateSolecismsInResolve = (quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0;")
                .Line("bool parseAndResolveConcurOnSolecisms = tolerateSolecismsInParse == tolerateSolecismsInResolve;")
                .Break();

            CsIf ifResubmit = tryParse.If("!suppressResubmission && parseAndResolveConcurOnSolecisms");

            ifResubmit
                .Line("TestModel resubmittedTestModel = new TestModel(modelParser, testModel.GetDtdl(), LocateForParse, useAsyncApi);")
                .Line("resubmittedTestModel.DoParse();")
                .Break();

            ifResubmit.If("expectationToken != null").Line("CheckObjectModel(resubmittedTestModel, (JObject)expectationToken);");

            ifResubmit.Line("Assert.IsTrue(resubmittedTestModel.IsModelEquivalent(testModel));");

            CsCatch catchException = tryParse.Catch("Exception ex");

            catchException.If("ex is AssertFailedException").Line("throw;");

            catchException.If("valid").Line("Assert.Fail($\"Exception unexpectedly thrown by {parseMethodName}(): {string.Join(\", \", ((ParsingException)ex).Errors.Select(e => $\"{e.ValidationID.AbsoluteUri} -- {e.Cause}\") ?? new List<string>())}\");");

            catchException.If("testCaseObject.TryGetValue(\"expect\", out JToken expectationToken)").Line("CheckException(ex, (JObject)expectationToken);");

            testInputMethod.Body.Line("resolutionChecker.Check(valid);");

            if (this.isLayeringSupported)
            {
                testInputMethod.Body.Line("partialResolutionChecker.Check(valid);");
            }
        }

        private CsScope GetModelParser(CsScope outerScope)
        {
            const string callGetModelParser = "ModelParser modelParser = GetModelParser(maxDtdlVersion, allowUndefinedExtensions, quirks, testCaseObject, useAsyncApi, maxReadConcurrency, out IResolutionChecker resolutionChecker, out IResolutionChecker partialResolutionChecker)";

            if (this.areDynamicExtensionsSupported)
            {
                return outerScope.Using(callGetModelParser);
            }
            else
            {
                outerScope.Line($"{callGetModelParser};");
                return outerScope;
            }
        }
    }
}
