namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>TestDtmiPartialResolver</c> unit-test support class.
    /// </summary>
    public class TestDtmiPartialResolverGenerator
    {
        private readonly bool isLayeringSupported;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDtmiPartialResolverGenerator"/> class.
        /// </summary>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public TestDtmiPartialResolverGenerator(bool isLayeringSupported)
        {
            this.isLayeringSupported = isLayeringSupported;
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            if (this.isLayeringSupported)
            {
                CsClass resolverClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "TestDtmiPartialResolver", exports: "IResolutionChecker");
                resolverClass.Summary("A test implementation of a <see cref=\"DtmiPartialResolver\"/> and a <see cref=\"DtmiPartialResolverAsync\"/>.");

                this.AddFields(resolverClass);
                this.AddConstructor(resolverClass);
                this.AddCheckMethod(resolverClass);
                this.AddGetResolverMethod(resolverClass);
                this.AddGetResolverAsyncMethod(resolverClass);
                this.AddResolveMethod(resolverClass);
                this.AddResolveAsyncMethod(resolverClass);
            }
        }

        private void AddFields(CsClass resolverClass)
        {
            resolverClass.Field(Access.Private, "JArray", "partialResolutionArray");
            resolverClass.Field(Access.Private, "bool", "useAsyncApi");
            resolverClass.Field(Access.Private, "int", "expectedResolutionCount");
            resolverClass.Field(Access.Private, "int", "actualResolutionCount");
        }

        private void AddConstructor(CsClass resolverClass)
        {
            CsConstructor constructor = resolverClass.Constructor(Access.Public);
            constructor.Param("JObject", "testCaseObject", "A <c>JObject</c> containing information about the test case.");
            constructor.Param("bool", "useAsyncApi", "Use the asynchronous parse/resolve API.");

            constructor.Body
                .Line("this.partialResolutionArray = null;")
                .Line("this.useAsyncApi = useAsyncApi;")
                .Line("this.expectedResolutionCount = 0;")
                .Line("this.actualResolutionCount = 0;")
                .Break();

            constructor.Body.If("testCaseObject.ContainsKey(\"partialResolution\")")
                .Line("this.partialResolutionArray = (JArray)testCaseObject[\"partialResolution\"];")
                .Line("this.expectedResolutionCount = this.partialResolutionArray.Count;");
        }

        private void AddCheckMethod(CsClass resolverClass)
        {
            CsMethod method = resolverClass.Method(Access.Public, Novelty.Normal, "void", "Check");
            method.Summary("Check that resolution has happened correctly.");
            method.Param("bool", "isModelValid", "True if the model is expected to be valid.");

            method.Body
                .If("isModelValid")
                    .Line("Assert.AreEqual(this.expectedResolutionCount, this.actualResolutionCount);")
                .Else()
                    .Line("Assert.IsTrue(this.actualResolutionCount <= this.expectedResolutionCount);");
        }

        private void AddGetResolverMethod(CsClass resolverClass)
        {
            CsMethod method = resolverClass.Method(Access.Public, Novelty.Normal, "DtmiPartialResolver", "GetResolver");
            method.Summary("Get an appropriate initialization value for the parser's <c>DtmiPartialResolver</c> property.");
            method.Returns("A <see cref=\"DtmiPartialResolver\"/> or null, as appropriate.");

            method.Body.Line("return this.partialResolutionArray != null && !this.useAsyncApi ? this.Resolve : (DtmiPartialResolver)null;");
        }

        private void AddGetResolverAsyncMethod(CsClass resolverClass)
        {
            CsMethod method = resolverClass.Method(Access.Public, Novelty.Normal, "DtmiPartialResolverAsync", "GetResolverAsync");
            method.Summary("Get an appropriate initialization value for the parser's <c>DtmiPartialResolverAsync</c> property.");
            method.Returns("A <see cref=\"DtmiPartialResolverAsync\"/> or null, as appropriate.");

            method.Body.Line("return this.partialResolutionArray != null && this.useAsyncApi ? this.ResolveAsync : (DtmiPartialResolverAsync)null;");
        }

        private void AddResolveMethod(CsClass resolverClass)
        {
            CsMethod method = resolverClass.Method(Access.Private, Novelty.Normal, "IEnumerable<string>", "Resolve");
            method.Param("IReadOnlyCollection<DtmiLayerInfo>", "dtmiLayers");

            method.Body
                .Line("Assert.IsTrue(this.actualResolutionCount < this.expectedResolutionCount);")
                .Line("JObject partialResolutionObject = (JObject)this.partialResolutionArray[this.actualResolutionCount];")
                .Break()
                .Line("Assert.AreEqual(partialResolutionObject[\"request\"].Count(), dtmiLayers.Count());")
                .Break();

            method.Body.ForEach("KeyValuePair<string, JToken> kvp in (JObject)partialResolutionObject[\"request\"]")
                .Line("DtmiLayerInfo actualDtmiLayerInfo = dtmiLayers.Where(l => l.Dtmi.AbsoluteUri == kvp.Key).FirstOrDefault();")
                .Line("Assert.IsNotNull(actualDtmiLayerInfo);")
                .Break()
                .Line("List<string> expectedLayers = ((JArray)kvp.Value).Select(t => ((JValue)t).Value<string>()).ToList();")
                .Line("expectedLayers.Sort();")
                .Break()
                .Line("List<string> actualLayers = actualDtmiLayerInfo.KnownLayers.ToList();")
                .Line("actualLayers.Sort();")
                .Break()
                .Line("Assert.IsTrue(expectedLayers.SequenceEqual(actualLayers));");

            method.Body
                .Line("++this.actualResolutionCount;")
                .Break();

            method.Body.If("partialResolutionObject[\"response\"].Type != JTokenType.Null")
                .Line("return ((JArray)partialResolutionObject[\"response\"]).Select(t => t.ToString());");

            method.Body.Line("return null;");
        }

        private void AddResolveAsyncMethod(CsClass resolverClass)
        {
            CsMethod method = resolverClass.Method(Access.Private, Novelty.Normal, "IAsyncEnumerable<string>", "ResolveAsync", asynchrony: Asynchrony.Async);
            method.Suppress("CS1998", "Async method lacks 'await' operators and will run synchronously");
            method.Suppress("CS8425", "'CancellationToken' is not decorated with the 'EnumeratorCancellation' attribute, so the cancellation token parameter from the generated 'IAsyncEnumerable<>.GetAsyncEnumerator' will be unconsumed");
            method.Param("IReadOnlyCollection<DtmiLayerInfo>", "dtmiLayers");
            method.Param("CancellationToken", "_");

            method.Body
                .Line("IEnumerable<string> values = this.Resolve(dtmiLayers);")
                .If("values != null")
                    .ForEach("string value in values")
                        .Line("yield return value;");
        }
    }
}
