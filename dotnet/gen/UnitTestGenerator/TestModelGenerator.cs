namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>TestModel</c> unit-test support class.
    /// </summary>
    public class TestModelGenerator
    {
        private readonly string baseClassName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestModelGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        public TestModelGenerator(string baseName)
        {
            this.baseClassName = NameFormatter.FormatNameAsClass(baseName);
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass testModelClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "TestModel");
            testModelClass.Summary("A class for testing the <see cref=\"ModelParser\"/> using JSON test cases.");

            testModelClass.Field(Access.Private, "ModelParser", "modelParser");
            testModelClass.Field(Access.Private, "IEnumerable<string>", "jsonTexts");
            testModelClass.Field(Access.Private, "DtdlParseLocator", "dtdlLocator");
            testModelClass.Field(Access.Private, "bool", "useAsyncApi");
            testModelClass.Field(Access.Private, $"IReadOnlyDictionary<Dtmi, {this.baseClassName}>", "result");

            CsConstructor constructor = testModelClass.Constructor(Access.Public);
            constructor.Param("ModelParser", "modelParser", "The <c>ModelParser</c> to use for parsing the test model.");
            constructor.Param("IEnumerable<string>", "jsonTexts", "The JSON text strings to parse.");
            constructor.Param("DtdlParseLocator", "dtdlLocator", "A <c>DtdlLocator</c> for locating errors in the JSON sources.");
            constructor.Param("bool", "useAsyncApi", "Use the asynchronous parse/resolve API.");
            constructor.Body.Line("this.modelParser = modelParser;");
            constructor.Body.Line("this.jsonTexts = jsonTexts;");
            constructor.Body.Line("this.dtdlLocator = dtdlLocator;");
            constructor.Body.Line("this.useAsyncApi = useAsyncApi;");

            CsProperty sizeProp = testModelClass.Property(Access.Public, Novelty.Normal, "int", "Size");
            sizeProp.Summary("Gets the size of the parsed model.");
            sizeProp.Body().Get().Line("return this.result.Count;");

            CsMethod getObjectAsStringMethod = testModelClass.Method(Access.Public, Novelty.Normal, "string", "GetObjectAsString", Multiplicity.Static);
            getObjectAsStringMethod.Summary("Get a string representation of the object, which for DTDL elements is a string representation of the element's ID.");
            getObjectAsStringMethod.Param("object", "obj", "The object to represent.");
            getObjectAsStringMethod.Returns("The string representation.");
            getObjectAsStringMethod.Body.Line($"return (obj as {this.baseClassName})?.Id.ToString() ?? obj?.ToString();");

            CsMethod doParseMethod = testModelClass.Method(Access.Public, Novelty.Normal, "void", "DoParse");
            doParseMethod.Summary("Perform the parsing.");
            CsIf ifAsync = doParseMethod.Body.If("this.useAsyncApi");
            ifAsync
                .Line($"Task<IReadOnlyDictionary<Dtmi, {this.baseClassName}>> parseTask = this.modelParser.ParseAsync(this.EnumerableStringToAsync(this.jsonTexts), this.dtdlLocator);")
                .Try()
                    .Line("parseTask.Wait();")
                    .Line("this.result = parseTask.Result;")
                .Catch("AggregateException ae")
                    .If("ae.InnerExceptions.Count() == 1")
                        .Line("throw ae.InnerExceptions.First();")
                    .Else()
                        .Line("throw;");
            ifAsync.Else()
                .Line("this.result = this.modelParser.Parse(this.jsonTexts, this.dtdlLocator);");

            CsMethod isElementInModelMethod = testModelClass.Method(Access.Public, Novelty.Normal, "bool", "IsElementInModel");
            isElementInModelMethod.Summary("Indicate whether the given element ID is present in the model.");
            isElementInModelMethod.Param(ParserGeneratorValues.IdentifierType, "elementId", "The identifier to check for.");
            isElementInModelMethod.Returns("True if present.");
            isElementInModelMethod.Body.Line("return this.result.ContainsKey(elementId);");

            CsMethod isModelEquivalentMethod = testModelClass.Method(Access.Public, Novelty.Normal, "bool", "IsModelEquivalent");
            isModelEquivalentMethod.Summary("Indicate whether another model has equivalent content to this model.");
            isModelEquivalentMethod.Param("TestModel", "other", "The other model to compare.");
            isModelEquivalentMethod.Returns("True if the two models are equivalent.");
            isModelEquivalentMethod.Body.ForEach($"KeyValuePair<Dtmi, {this.baseClassName}> kvp in other.result")
                .If("!this.result.ContainsKey(kvp.Key) || !this.result[kvp.Key].Equals(kvp.Value) || this.result[kvp.Key].GetHashCode() != kvp.Value.GetHashCode()")
                    .Line("return false;");
            isModelEquivalentMethod.Body.Line("return true;");

            CsMethod getElementMethod = testModelClass.Method(Access.Public, Novelty.Normal, this.baseClassName, "GetElement");
            getElementMethod.Summary($"Get the <see cref=\"{this.baseClassName}\"/> indicated by the given element ID.");
            getElementMethod.Param(ParserGeneratorValues.IdentifierType, "elementId", "The identifer of the element to retrieve.");
            getElementMethod.Returns("The element from the model.");
            getElementMethod.Body.Line("return this.result[elementId];");

            CsMethod getDtdlMethod = testModelClass.Method(Access.Public, Novelty.Normal, "List<string>", "GetDtdl");
            getDtdlMethod.Summary("Get a list of DTDL texts corresponding to this object model.");
            getDtdlMethod.Returns("A <c>List</c> of strings of DTDL text.");
            getDtdlMethod.Body
                .Line("List<string> dtdlTexts = new List<string>();")
                .ForEach($"{this.baseClassName} element in this.result.Values")
                    .If("element is IRootable rootableElement")
                        .Line("string jsonText = rootableElement.GetJsonLdText();")
                        .If("jsonText != string.Empty")
                            .Line("dtdlTexts.Add(jsonText);");
            getDtdlMethod.Body
                .Line("return dtdlTexts;");

            CsMethod enumerableStringToAsyncMethod = testModelClass.Method(Access.Private, Novelty.Normal, "IAsyncEnumerable<string>", "EnumerableStringToAsync", asynchrony: Asynchrony.Async);
            enumerableStringToAsyncMethod.Suppress("CS1998", "Async method lacks 'await' operators and will run synchronously");
            enumerableStringToAsyncMethod.Param("IEnumerable<string>", "values");
            enumerableStringToAsyncMethod.Body
                .ForEach("string value in values")
                    .Line("yield return value;");
        }
    }
}
