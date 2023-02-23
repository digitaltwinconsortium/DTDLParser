namespace DTDLParser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Code generator for <c>ModelParser</c> partial class.
    /// </summary>
    public class ModelParserGenerator : ITypeGenerator
    {
        private readonly string baseClassName;
        private readonly bool areDynamicExtensionsSupported;
        private readonly bool isLayeringSupported;
        private readonly string tsFileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelParserGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        /// <param name="areDynamicExtensionsSupported">True if dynamic extensions are supported by any recognized language version.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        /// <param name="tsFileName">Name of the TypeScript file that defines the JSON objects returned by ParseToJson().</param>
        public ModelParserGenerator(string baseName, bool areDynamicExtensionsSupported, bool isLayeringSupported, string tsFileName)
        {
            this.baseClassName = NameFormatter.FormatNameAsClass(baseName);
            this.areDynamicExtensionsSupported = areDynamicExtensionsSupported;
            this.isLayeringSupported = isLayeringSupported;
            this.tsFileName = Path.GetFileName(tsFileName);
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass parserClass = parserLibrary.Class(Access.Public, Novelty.Normal, "ModelParser", completeness: Completeness.Partial, exports: this.areDynamicExtensionsSupported ? "IDisposable" : null);
            parserClass.Summary("Instances of the <c>ModelParser</c> class parse models written in the DTDL language.");
            parserClass.Summary("This class can be used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.");
            parserClass.Remarks("The primary methods on this class are <see cref=\"ModelParser.Parse(IEnumerable{string}, DtdlParseLocator)\"/> and <see cref=\"ModelParser.ParseAsync(IAsyncEnumerable{string}, DtdlParseLocator, CancellationToken,)\"/>.");

            this.GenerateConstructor(parserClass);

            if (this.areDynamicExtensionsSupported)
            {
                this.GenerateConstructorWithMaxReadConcurrency(parserClass);
                this.GenerateDisposeMethod(parserClass);
                this.GenerateLoadExtensionsMethod(parserClass);
            }

            this.GenerateDeclarations(parserClass, isAsync: true);
            this.GenerateDeclarations(parserClass, isAsync: false);

            this.GenerateParseElementMethod(parserClass);

            if (this.isLayeringSupported)
            {
                parserClass.Field(Access.Private, "DtmiPartialResolver", "dtmiPartialResolver");
                parserClass.Field(Access.Private, "DtmiPartialResolverAsync", "dtmiPartialResolverAsync");
            }
        }

        private void GenerateConstructor(CsClass parserClass)
        {
            CsConstructor constructor = parserClass.Constructor(Access.Public);
            constructor.Param("ParsingOptions", "parsingOptions", "An optional <see cref=\"ParsingOptions\"/> object whose properties configure the <see cref=\"ModelParser\"/> behavior.", "null");
            constructor.Param("ModelParsingQuirk", "quirks", "An optional set of <see cref=\"ModelParsingQuirk\"/> values that control parsing of DTDL models.", "ModelParsingQuirk.None");

            if (this.areDynamicExtensionsSupported)
            {
                constructor.Summary("Instances created by this constructor do not permit calls to the <see cref=\"LoadExtensions(IEnumerable{string}, DtdlParseLocator)\"/> method.");
            }

            constructor.Body
                .Line("this.contextCollection = new ContextCollection();")
                .Line("this.supplementalTypeCollection = new SupplementalTypeCollection();")
                .Line("this.standardElementCollection = new StandardElementCollection();")
                .Break();

            CsIf ifParsingOptions = constructor.Body.If("parsingOptions != null");

            ifParsingOptions
                .Line("this.dtmiResolver = parsingOptions.DtmiResolver;")
                .Line("this.dtmiResolverAsync = parsingOptions.DtmiResolverAsync;")
                .Line("this.dtdlResolveLocator = parsingOptions.DtdlResolveLocator;")
                .Line("this.MaxDtdlVersion = parsingOptions.MaxDtdlVersion;")
                .Line("this.AllowUndefinedExtensions = parsingOptions.AllowUndefinedExtensions;");

            if (this.isLayeringSupported)
            {
                ifParsingOptions
                    .Line("this.dtmiPartialResolver = parsingOptions.DtmiPartialResolver;")
                    .Line("this.dtmiPartialResolverAsync = parsingOptions.DtmiPartialResolverAsync;");
            }

            CsElse elseNoParsingOptions = ifParsingOptions.Else();

            elseNoParsingOptions
                .Line("this.dtmiResolver = null;")
                .Line("this.dtmiResolverAsync = null;")
                .Line("this.dtdlResolveLocator = null;")
                .Line("this.MaxDtdlVersion = ParsingOptions.MaxKnownDtdlVersion;")
                .Line("this.AllowUndefinedExtensions = false;");

            if (this.isLayeringSupported)
            {
                elseNoParsingOptions
                    .Line("this.dtmiPartialResolver = null;")
                    .Line("this.dtmiPartialResolverAsync = null;");
            }

            constructor.Body
                .Line("this.jsonLdResolvingReader = new JsonLdReader(this.dtdlResolveLocator);")
                .Line("this.quirks = quirks;")
                .Break()
                .Line("this.readerWriterAsyncLock = null;");
        }

        private void GenerateConstructorWithMaxReadConcurrency(CsClass parserClass)
        {
            CsConstructor constructor = parserClass.Constructor(Access.Public);
            constructor.Param("int", "maxReadConcurrency", "The maximum count of read operations that may proceed concurrently.  A value of 0 permits arbitrary read concurrency, and also avoids the need for calling the <see cref=\"Dispose\"/> method.");
            constructor.Param("ParsingOptions", "parsingOptions", "An optional <see cref=\"ParsingOptions\"/> object whose properties configure the <see cref=\"ModelParser\"/> behavior.", "null");
            constructor.Param("ModelParsingQuirk", "quirks", "An optional set of <see cref=\"ModelParsingQuirk\"/> values that control parsing of DTDL models.", "ModelParsingQuirk.None");

            constructor.Summary("Instances created by this constructor permit calls to the <see cref=\"LoadExtensions(IEnumerable{string}, DtdlParseLocator)\"/> method if <paramref name=\"maxReadConcurrency\"/> > 0.");
            constructor.Remarks($"Read operations include calls to <see cref=\"Parse(IEnumerable{{string}}, DtdlParseLocator)\"/>, <see cref=\"ParseAsync(IAsyncEnumerable{{string}}, DtdlParseLocator, CancellationToken,)\"/>, and <see cref=\"GetSupplementalTypes\"/>.");

            constructor.This("parsingOptions, quirks");

            constructor.Body
                .Line($"this.readerWriterAsyncLock = {(this.areDynamicExtensionsSupported ? "maxReadConcurrency > 0 ? new ReaderWriterAsyncLock(maxReadConcurrency) : null" : "null")};");
        }

        private void GenerateDisposeMethod(CsClass parserClass)
        {
            CsMethod method = parserClass.Method(Access.Public, Novelty.Normal, "void", "Dispose");
            method.Summary("Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.");
            method.Body.Line("((IDisposable)this.readerWriterAsyncLock)?.Dispose();");
        }

        private void GenerateLoadExtensionsMethod(CsClass parserClass)
        {
            CsMethod method = parserClass.Method(Access.Public, Novelty.Normal, "void", "LoadExtensions");
            method.Summary("Load a collection of JSON text strings as DTDL language extensions.");
            method.Param("IEnumerable<string>", "jsonTexts", "The JSON text strings to parse as DTDL language extensions.");
            method.Param("DtdlParseLocator", "dtdlLocator", "An optional <see cref=\"DtdlParseLocator\"/> delegate for converting a parse index and line number into a source name and line number.", "null");
            method.Remarks("Calling this method is permitted only on a <see cref=\"ModelParser\"/> object instantiated with a non-zero <c>maxReadConcurrency</c> value.");

            method.Body.If("this.readerWriterAsyncLock == null")
                .Line("throw new Exception(\"LoadExtensions() method was called on ModelParser object that was instantiated with maxReadConcurrency of 0.\");");

            method.Body.Line("this.readerWriterAsyncLock.EnterWriteLock();");
            CsTry tryLoad = method.Body.Try();

            tryLoad.Line("var parsingErrorCollection = new ParsingErrorCollection();");
            tryLoad.Line("int enumerationIndex = 0;");

            CsForEach forEachJsonText = tryLoad.ForEach("string jsonText in jsonTexts");
            forEachJsonText.Line("JsonLdValueCollection valueCollection = JsonLdReader.ReadForParse(jsonText, enumerationIndex, dtdlLocator, parsingErrorCollection);");

            CsForEach forEachExtensionValue = forEachJsonText.ForEach("JsonLdValue extensionValue in valueCollection.Values");
            forEachExtensionValue.If("extensionValue.ValueType != JsonLdValueType.Element").Line("parsingErrorCollection.Quit(\"extensionNotJsonObject\", incidentValue: extensionValue);");
            forEachExtensionValue
                .Line("DtdlExtension dtdlExtension = new DtdlExtension(extensionValue.ElementValue, parsingErrorCollection);")
                .Line("this.contextCollection.AddExtensionContext(dtdlExtension.ExtensionId, dtdlExtension.Context, parsingErrorCollection);")
                .Break()
                .MultiLine("AggregateContext aggregateContext = new AggregateContext(this.contextCollection, this.supplementalTypeCollection, allowUndefinedExtensions: false, suppressDefinitionMerging: true)")
                    .Line(".GetChildContextFromContextArray(dtdlExtension.Context, parsingErrorCollection, forceAllowLocalContext: true);");
            forEachExtensionValue
                .Break()
                .If("!aggregateContext.AreDynamicExtensionsAllowed")
                    .Line("parsingErrorCollection.Quit(\"extensionNotAllowed\", incidentValue: extensionValue);");
            forEachExtensionValue
                .Line("this.supplementalTypeCollection.AddExtensionMetamodel(dtdlExtension.ExtensionId, dtdlExtension.Metamodels, aggregateContext, parsingErrorCollection);")
                .Line("this.standardElementCollection.AddExtensionModel(dtdlExtension.ExtensionId, dtdlExtension.Models, this.supplementalTypeCollection, aggregateContext, parsingErrorCollection, preserveElementAliases: false);")
                .Break()
                .Line("parsingErrorCollection.ThrowIfAny();");

            tryLoad.Finally()
                .Line("this.readerWriterAsyncLock.ExitWriteLock();");
        }

        private void GenerateDeclarations(CsClass parserClass, bool isAsync)
        {
            this.GenerateParseMethod(parserClass, isAsync);
            this.GenerateParseSingletonMethod(parserClass, isAsync);
            this.GenerateParseToJsonMethod(parserClass, isAsync);
            this.GenerateParseToJsonSingletonMethod(parserClass, isAsync);
            this.GenerateParseInternalMethod(parserClass, isAsync);
            this.GenerateParseAndResolveAsNeededMethod(parserClass, isAsync);
            this.GenerateParseTextsIntoModelMethod(parserClass, isAsync);
        }

        private void GenerateParseMethod(CsClass parserClass, bool isAsync)
        {
            string methodName = this.GetFullName("Parse", isAsync);
            CsMethod method = parserClass.Method(Access.Public, Novelty.Normal, this.GetReturnType($"IReadOnlyDictionary<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}>", isAsync), methodName, asynchrony: isAsync ? Asynchrony.Async : Asynchrony.Sync);
            method.Summary($"{(isAsync ? "Asynchronously parse" : "Parse")} a collection of JSON text strings as DTDL models.");

            method.Param($"{this.GetFullName("I{0}Enumerable", isAsync)}<string>", "jsonTexts", "The JSON text strings to parse as DTDL models.");
            method.Param("DtdlParseLocator", "dtdlLocator", "An optional <see cref=\"DtdlParseLocator\"/> delegate for converting a parse index and line number into a source name and line number.", "null");

            if (isAsync)
            {
                method.Param("CancellationToken", "cancellationToken", "A <c>CancellationToken</c> to cancel the parsing prematurely.", "default");
            }

            method.Returns($"{(isAsync ? "A <c>Task</c> object whose <c>Result</c> property is a" : "A")} dictionary that maps each <c>{ParserGeneratorValues.IdentifierType}</c> to a subclass of <c>{this.baseClassName}</c>.");

            string tokenArg = isAsync ? ", cancellationToken" : string.Empty;
            method.Body
                .Line("Model model;")
                .If("this.readerWriterAsyncLock == null")
                    .Line(this.CallMethod("model", "this.ParseInternal", $"jsonTexts, dtdlLocator{tokenArg}", isAsync))
                .Else()
                    .Line(this.CallMethod(null, "this.readerWriterAsyncLock.EnterReadLock", string.Empty, isAsync))
                    .Try()
                        .Line(this.CallMethod("model", "this.ParseInternal", $"jsonTexts, dtdlLocator{tokenArg}", isAsync))
                    .Finally()
                        .Line("this.readerWriterAsyncLock.ExitReadLock();");

            method.Body
                .Line("return model.Dict;");
        }

        private void GenerateParseSingletonMethod(CsClass parserClass, bool isAsync)
        {
            string methodName = this.GetFullName("Parse", isAsync);
            CsMethod method = parserClass.Method(Access.Public, Novelty.Normal, this.GetReturnType($"IReadOnlyDictionary<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}>", isAsync), methodName, asynchrony: isAsync ? Asynchrony.Async : Asynchrony.Sync);
            method.Summary($"{(isAsync ? "Asynchronously parse" : "Parse")} a JSON text string as DTDL models.");
            method.Param("string", "jsonText", "The JSON text string to parse as DTDL models.");
            method.Param("DtdlParseLocator", "dtdlLocator", "An optional <see cref=\"DtdlParseLocator\"/> delegate for converting a parse index and line number into a source name and line number.", "null");

            if (isAsync)
            {
                method.Param("CancellationToken", "cancellationToken", "A <c>CancellationToken</c> to cancel the parsing prematurely.", "default");
            }

            method.Returns($"{(isAsync ? "A <c>Task</c> object whose <c>Result</c> property is a" : "A")} dictionary that maps each <c>{ParserGeneratorValues.IdentifierType}</c> to a subclass of <c>{this.baseClassName}</c>.");

            if (isAsync)
            {
                method.Body.Line("return await this.ParseAsync(this.StringToAsyncEnumerable(jsonText), dtdlLocator, cancellationToken).ConfigureAwait(false);");
            }
            else
            {
                method.Body.Line("return this.Parse(this.StringToEnumerable(jsonText), dtdlLocator);");
            }
        }

        private void GenerateParseToJsonMethod(CsClass parserClass, bool isAsync)
        {
            string methodName = this.GetFullName("ParseToJson", isAsync);
            CsMethod method = parserClass.Method(Access.Public, Novelty.Normal, this.GetReturnType("string", isAsync), methodName, asynchrony: isAsync ? Asynchrony.Async : Asynchrony.Sync);
            method.Summary($"{(isAsync ? "Asynchronously parse" : "Parse")} a collection of JSON text strings as DTDL models and return the result as a JSON object model.");
            method.Param($"{this.GetFullName("I{0}Enumerable", isAsync)}<string>", "jsonTexts", "The JSON text strings to parse as DTDL models.");
            method.Param("bool", "indent", "Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.", "false");
            method.Returns($"{(isAsync ? "A <c>Task</c> object whose <c>Result</c> property is a" : "A")} string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object in accordance with {this.tsFileName}.");

            CsTry tryParse = method.Body.Try();
            tryParse.Line(this.CallMethod($"IReadOnlyDictionary<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}> model", "this.Parse", "jsonTexts", isAsync));
            tryParse.Break();
            CsScope parseScope = this.GenerateNewJsonWriter(tryParse, "return {0};");
            parseScope.Line("jsonWriter.WriteStartObject();").Break();
            CsForEach forEachKvp = parseScope.ForEach($"KeyValuePair<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}> kvp in model");
            forEachKvp
                .Line("jsonWriter.WritePropertyName(kvp.Key.ToString());")
                .Line("jsonWriter.WriteStartObject();")
                .Line("kvp.Value.WriteToJson(jsonWriter, includeClassId: true);")
                .Line("jsonWriter.WriteEndObject();");
            parseScope.Break();
            parseScope
                .Line("jsonWriter.WriteEndObject();")
                .Break()
                .Line("jsonWriter.Flush();");

            CsCatch catchPex = tryParse.Catch("ParsingException pex");
            CsScope pexScope = this.GenerateNewJsonWriter(catchPex, "throw new Exception({0});");
            pexScope
                .Line("pex.WriteToJson(jsonWriter);")
                .Line("jsonWriter.Flush();");

            CsCatch catchRex = tryParse.Catch("ResolutionException rex");
            CsScope rexScope = this.GenerateNewJsonWriter(catchRex, "throw new Exception({0});");
            rexScope
                .Line("rex.WriteToJson(jsonWriter);")
                .Line("jsonWriter.Flush();");

            tryParse.Catch("Exception")
                .Line("throw;");
        }

        private void GenerateParseToJsonSingletonMethod(CsClass parserClass, bool isAsync)
        {
            string methodName = this.GetFullName("ParseToJson", isAsync);
            CsMethod method = parserClass.Method(Access.Public, Novelty.Normal, this.GetReturnType("string", isAsync), methodName, asynchrony: isAsync ? Asynchrony.Async : Asynchrony.Sync);
            method.Summary($"{(isAsync ? "Asynchronously parse" : "Parse")} a JSON text string as DTDL models and return the result as a JSON object model.");
            method.Param("string", "jsonText", "The JSON text string to parse as DTDL models.");
            method.Param("bool", "indent", "Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.", "false");

            method.Returns($"{(isAsync ? "A <c>Task</c> object whose <c>Result</c> property is a" : "A")} string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object in accordance with {this.tsFileName}.");

            if (isAsync)
            {
                method.Body.Line("return await this.ParseToJsonAsync(this.StringToAsyncEnumerable(jsonText), indent).ConfigureAwait(false);");
            }
            else
            {
                method.Body.Line("return this.ParseToJson(this.StringToEnumerable(jsonText), indent);");
            }
        }

        private CsScope GenerateNewJsonWriter(CsScope scope, string resultFormat)
        {
            CsUsing usingMemStream = scope.Using("MemoryStream memStream = new MemoryStream()");
            usingMemStream.Line("JsonWriterOptions jsonWriterOptions = new JsonWriterOptions { Indented = indent };");

            CsUsing usingJsonWriter = usingMemStream.Using("Utf8JsonWriter jsonWriter = new Utf8JsonWriter(memStream, jsonWriterOptions)");

            usingMemStream.Line(string.Format(resultFormat, "Encoding.UTF8.GetString(memStream.ToArray())"));

            return usingJsonWriter;
        }

        private void GenerateParseInternalMethod(CsClass parserClass, bool isAsync)
        {
            CsMethod method = parserClass.Method(Access.Private, Novelty.Normal, this.GetReturnType("Model", isAsync), this.GetFullName("ParseInternal", isAsync), asynchrony: isAsync ? Asynchrony.Async : Asynchrony.Sync);

            string enumTypeName = this.GetFullName("I{0}Enumerable", isAsync);
            method.Param($"{enumTypeName}<string>", "jsonTexts");
            method.Param("DtdlParseLocator", "dtdlLocator");

            if (isAsync)
            {
                method.Param("CancellationToken", "cancellationToken");
            }

            method.Body
                .Line("var model = new Model();")
                .Line("var objectPropertyInfoList = new List<ParsedObjectPropertyInfo>();")
                .Line("var elementPropertyConstraints = new List<ElementPropertyConstraint>();")
                .Line("var parsingErrorCollection = new ParsingErrorCollection();")
                .Break();

            string tokenArg = isAsync ? ", cancellationToken" : string.Empty;
            method.Body
                .Line(this.CallMethod(null, "this.ParseAndResolveAsNeeded", $"jsonTexts, dtdlLocator{tokenArg}, model, objectPropertyInfoList, elementPropertyConstraints, parsingErrorCollection", isAsync))
                .Line("model.SetObjectProperties(objectPropertyInfoList, parsingErrorCollection, this.standardElementCollection, preserveElementAliases: false);")
                .Line("parsingErrorCollection.ThrowIfAny();")
                .Break();

            method.Body
                .Line("model.CheckElementPropertyConstraints(elementPropertyConstraints, this.supplementalTypeCollection, this.standardElementCollection, parsingErrorCollection, preserveElementAliases: false);")
                .Line("model.ApplyTransformations(parsingErrorCollection);")
                .Line("model.CheckRestrictionsAndSiblingConstraints(elementPropertyConstraints, this.supplementalTypeCollection, parsingErrorCollection);")
                .Line("parsingErrorCollection.ThrowIfAny();")
                .Break();

            method.Body
                .Line("return model;");
        }

        private void GenerateParseAndResolveAsNeededMethod(CsClass parserClass, bool isAsync)
        {
            CsMethod method = parserClass.Method(Access.Private, Novelty.Normal, this.GetReturnType("void", isAsync), this.GetFullName("ParseAndResolveAsNeeded", isAsync), asynchrony: isAsync ? Asynchrony.Async : Asynchrony.Sync);

            string enumTypeName = this.GetFullName("I{0}Enumerable", isAsync);
            method.Param($"{enumTypeName}<string>", "jsonTexts");
            method.Param("DtdlParseLocator", "dtdlLocator");

            if (isAsync)
            {
                method.Param("CancellationToken", "cancellationToken");
            }

            method.Param("Model", "model");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");

            method.Body
                .Line("JsonLdReader jsonLdParsingReader = new JsonLdReader(dtdlLocator);")
                .Break();

            string callQualifier = isAsync ? "await " : string.Empty;
            string coda = isAsync ? ".ConfigureAwait(false)" : string.Empty;
            string tokenArg = isAsync ? ", cancellationToken" : string.Empty;

            CsMultiLine parseTextsIntoModelMultiline1 = method.Body.MultiLine($"{callQualifier}this.{this.GetFullName("ParseTextsIntoModel", isAsync)}(");
            parseTextsIntoModelMultiline1.Line("jsonTexts,");
            if (isAsync)
            {
                parseTextsIntoModelMultiline1.Line("cancellationToken,");
            }

            parseTextsIntoModelMultiline1
                .Line("jsonLdParsingReader,")
                .Line("model,")
                .Line("objectPropertyInfoList,")
                .Line("elementPropertyConstraints,")
                .Line("parsingErrorCollection,")
                .Line("allowReservedIds: (this.quirks & ModelParsingQuirk.TolerateSolecismsInParse) != 0,")
                .Line("allowIdReferenceSyntax: (this.quirks & ModelParsingQuirk.TolerateSolecismsInParse) != 0,")
                .Line($"ignoreElementsWithAutoIDsAndDuplicateNames: (this.quirks & ModelParsingQuirk.TolerateSolecismsInParse) != 0){coda};");
            method.Body.Break();

            if (this.isLayeringSupported)
            {
                CsIf ifPartialResolver = method.Body.If($"{this.GetFullName("this.dtmiPartialResolver", isAsync)} != null");

                ifPartialResolver
                    .Line("List<DtmiLayerInfo> partialElementLayerInfos = new List<DtmiLayerInfo>();")
                    .Break();

                ifPartialResolver
                    .ForEach("var kvp in model.Dict")
                        .If("!kvp.Key.IsReserved && kvp.Value.MaybePartial")
                            .Line("partialElementLayerInfos.Add(new DtmiLayerInfo() { Dtmi = kvp.Key, KnownLayers = kvp.Value.Layers });");

                CsIf ifAnyLayerInfos = ifPartialResolver.If("partialElementLayerInfos.Any()");

                ifAnyLayerInfos
                    .Line($"{enumTypeName}<string> additionalJsonTexts = {this.GetFullName("this.dtmiPartialResolver", isAsync)}(partialElementLayerInfos{tokenArg});");

                CsMultiLine parseTextsIntoModelMultiline2 = ifAnyLayerInfos.MultiLine($"{callQualifier}this.{this.GetFullName("ParseTextsIntoModel", isAsync)}(");
                parseTextsIntoModelMultiline2.Line("additionalJsonTexts,");
                if (isAsync)
                {
                    parseTextsIntoModelMultiline2.Line("cancellationToken,");
                }

                parseTextsIntoModelMultiline2
                    .Line("this.jsonLdResolvingReader,")
                    .Line("model,")
                    .Line("objectPropertyInfoList,")
                    .Line("elementPropertyConstraints,")
                    .Line("parsingErrorCollection,")
                    .Line("allowReservedIds: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0,")
                    .Line("allowIdReferenceSyntax: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0,")
                    .Line($"ignoreElementsWithAutoIDsAndDuplicateNames: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0){coda};");
                method.Body.Break();
            }

            CsWhile whileTrue = method.Body.While("true");

            whileTrue
                .Line("Dictionary<Dtmi, DtmiReferenceTracker> undefinedIdentifiers = new Dictionary<Dtmi, DtmiReferenceTracker>();")
                .Break();

            CsIf ifTryAddElement =
            whileTrue
                .ForEach("ParsedObjectPropertyInfo objectPropertyInfo in objectPropertyInfoList")
                    .If("!model.Dict.ContainsKey(objectPropertyInfo.ReferencedElementId)")
                        .If("!this.standardElementCollection.TryAddElementToModel(model, objectPropertyInfo.ReferencedElementId, preserveElementAliases: false)");

            CsIf ifTerm = ifTryAddElement.If("objectPropertyInfo.ReferencedIdString != null && !objectPropertyInfo.ReferencedIdString.StartsWith(\"dtmi:\")");

            ifTerm
                .Line("JsonLdValue propertyValue = objectPropertyInfo.JsonLdProperty.Values.Values.First(v => v.StringValue == objectPropertyInfo.ReferencedIdString);");

            CsIf ifGotSourceLocTerm = ifTerm.If("objectPropertyInfo.JsonLdProperty.TryGetSourceLocation(out string sourceName1, out int sourceLine1)");

            ifGotSourceLocTerm
                .Line("propertyValue.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2);")
                .MultiLine("parsingErrorCollection.Add(")
                    .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                    .Line("objectPropertyInfo.BadTypeLocatedCauseFormat,")
                    .Line("objectPropertyInfo.BadTypeActionFormat,")
                    .Line("primaryId: objectPropertyInfo.ElementId,")
                    .Line("property: objectPropertyInfo.PropertyName,")
                    .Line("secondaryId: objectPropertyInfo.ReferencedElementId,")
                    .Line("sourceName1: sourceName1,")
                    .Line("startLine1: sourceLine1,")
                    .Line("sourceName2: sourceName2,")
                    .Line("startLine2: startLine2,")
                    .Line("endLine2: endLine2);");

            ifGotSourceLocTerm.Else()
                .MultiLine("parsingErrorCollection.Add(")
                    .Line("new Uri(\"dtmi:dtdl:parsingError:badType\"),")
                    .Line("objectPropertyInfo.BadTypeCauseFormat,")
                    .Line("objectPropertyInfo.BadTypeActionFormat,")
                    .Line("primaryId: objectPropertyInfo.ElementId,")
                    .Line("property: objectPropertyInfo.PropertyName,")
                    .Line("secondaryId: objectPropertyInfo.ReferencedElementId);");

            CsElseIf elseIfNotReserved = ifTerm.ElseIf("ContextCollection.IsIdentifierReserved(objectPropertyInfo.ReferencedElementId.AbsoluteUri)");

            elseIfNotReserved
                .Line("JsonLdValue propertyValue = objectPropertyInfo.JsonLdProperty.Values.Values.First(v => v.StringValue == objectPropertyInfo.ReferencedIdString);")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"undefinedReservedId\",")
                    .Line("elementId: objectPropertyInfo.ElementId,")
                    .Line("propertyName: objectPropertyInfo.PropertyName,")
                    .Line("referenceId: objectPropertyInfo.ReferencedElementId,")
                    .Line("incidentProperty: objectPropertyInfo.JsonLdProperty,")
                    .Line("incidentValue: propertyValue);");

            CsElse elseNotTerm = elseIfNotReserved.Else();

            elseNotTerm
                .If("!undefinedIdentifiers.TryGetValue(objectPropertyInfo.ReferencedElementId, out DtmiReferenceTracker referenceTracker)")
                    .Line("referenceTracker = new DtmiReferenceTracker(objectPropertyInfo.ReferencedElementId);")
                    .Line("undefinedIdentifiers[objectPropertyInfo.ReferencedElementId] = referenceTracker;");

            elseNotTerm
                .Line("referenceTracker.AddReference(objectPropertyInfo.JsonLdProperty, objectPropertyInfo.ReferencedIdString);");

            whileTrue
                .Line("parsingErrorCollection.ThrowIfAny();")
                .Break();

            whileTrue
                .If("!undefinedIdentifiers.Any()")
                    .Line("return;");

            whileTrue
                .If($"{this.GetFullName("this.dtmiResolver", isAsync)} == null")
                    .If($"{this.GetFullName("this.dtmiResolver", !isAsync)} != null")
                        .Line($"throw new ResolutionException(\"Sync/async mismatch: {this.GetFullName("DtmiResolver", !isAsync)} registered but {this.GetFullName("Parse", isAsync)}() method called. Either register a {this.GetFullName("DtmiResolver", isAsync)} or call {this.GetFullName("Parse", !isAsync)}() method instead of {this.GetFullName("Parse", isAsync)}().\", undefinedIdentifiers);")
                    .Else()
                        .Line($"throw new ResolutionException(\"No {this.GetFullName("DtmiResolver", isAsync)} provided to resolve requisite reference(s): \", undefinedIdentifiers, itemizeIdentifers: true);");

            whileTrue
                .Line($"{enumTypeName}<string> additionalJsonTexts = {this.GetFullName("this.dtmiResolver", isAsync)}(undefinedIdentifiers.Keys{tokenArg});")
                .If("additionalJsonTexts == null")
                    .Line($"throw new ResolutionException(\"{this.GetFullName("DtmiResolver", isAsync)} refused to resolve requisite references to element(s): \", undefinedIdentifiers, itemizeIdentifers: true);");

            CsMultiLine parseTextsIntoModelMultiline3 = whileTrue.MultiLine($"{callQualifier}this.{this.GetFullName("ParseTextsIntoModel", isAsync)}(");
            parseTextsIntoModelMultiline3.Line("additionalJsonTexts,");
            if (isAsync)
            {
                parseTextsIntoModelMultiline3.Line("cancellationToken,");
            }

            parseTextsIntoModelMultiline3
                .Line("this.jsonLdResolvingReader,")
                .Line("model,")
                .Line("objectPropertyInfoList,")
                .Line("elementPropertyConstraints,")
                .Line("parsingErrorCollection,")
                .Line("allowReservedIds: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0,")
                .Line("allowIdReferenceSyntax: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0,")
                .Line($"ignoreElementsWithAutoIDsAndDuplicateNames: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0){coda};");

            whileTrue
                .Line("List<Dtmi> requestedIdentifiers = undefinedIdentifiers.Keys.ToList();")
                .ForEach("Dtmi dtmi in requestedIdentifiers")
                    .If("model.Dict.ContainsKey(dtmi)")
                        .Line("undefinedIdentifiers.Remove(dtmi);");

            whileTrue
                .If("undefinedIdentifiers.Any()")
                    .Line($"throw new ResolutionException(\"{this.GetFullName("DtmiResolver", isAsync)} failed to resolve requisite references to element(s): \", undefinedIdentifiers, itemizeIdentifers: true);");
        }

        private void GenerateParseTextsIntoModelMethod(CsClass parserClass, bool isAsync)
        {
            CsMethod method = parserClass.Method(Access.Private, Novelty.Normal, this.GetReturnType("void", isAsync), this.GetFullName("ParseTextsIntoModel", isAsync), asynchrony: isAsync ? Asynchrony.Async : Asynchrony.Sync);

            string enumTypeName = this.GetFullName("I{0}Enumerable", isAsync);
            method.Param($"{enumTypeName}<string>", "jsonTexts");

            if (isAsync)
            {
                method.Param("CancellationToken", "cancellationToken");
            }

            method.Param("JsonLdReader", "jsonLdReader");
            method.Param("Model", "model");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints");
            method.Param("ParsingErrorCollection", "parsingErrorCollection");
            method.Param("bool", "allowReservedIds");
            method.Param("bool", "allowIdReferenceSyntax");
            method.Param("bool", "ignoreElementsWithAutoIDsAndDuplicateNames");

            string coda = isAsync ? ".WithCancellation(cancellationToken).ConfigureAwait(false)" : string.Empty;
            method.Body.Line("int enumerationIndex = 0;");

            CsForEach forEachJsonText = method.Body.ForEach($"string jsonText in jsonTexts{coda}", withAwait: isAsync);
            forEachJsonText
                .Line("JsonLdValueCollection valueCollection = jsonLdReader.Read(jsonText, enumerationIndex, parsingErrorCollection);")
                .ForEach("JsonLdValue jsonLdValue in valueCollection.Values")
                    .Line("this.ParseJsonLdValues(model, objectPropertyInfoList, elementPropertyConstraints, parsingErrorCollection, jsonLdValue, 0, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames);");
            forEachJsonText
                .Line("parsingErrorCollection.ThrowIfAny();")
                .Line("++enumerationIndex;");
        }

        private void GenerateParseElementMethod(CsClass parserClass)
        {
            CsMethod method = parserClass.Method(Access.Internal, Novelty.Normal, "void", "ParseElement", Multiplicity.Static);
            method.Summary("Parse an element encoded in a <see cref=\"JsonLdElement\"/>.");
            method.Param("Model", "model", "The model to add the element to.");
            method.Param("List<ParsedObjectPropertyInfo>", "objectPropertyInfoList", "A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.");
            method.Param("List<ElementPropertyConstraint>", "elementPropertyConstraints", "List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.");
            method.Param("AggregateContext", "aggregateContext", "An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Param("JsonLdElement", "elt", "The <see cref=\"JsonLdElement\"/> to parse.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "globalize", "Treat all nested definitions as though defined globally.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowReservedIds", "Allow elements to define IDs that have reserved prefixes.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "allowIdReferenceSyntax", "Allow an object reference to be made using an object containing nothing but an @id property.");
            method.Param(ParserGeneratorValues.ObverseTypeBoolean, "ignoreElementsWithAutoIDsAndDuplicateNames", "Ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.");
            method.Body.Line($"{this.baseClassName}.TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, null, aggregateContext, parsingErrorCollection, elt, layer: null, parentId: null, definedIn: null, propName: null, propProp: null, dtmiSeg: null, keyProp: null, idRequired: true, typeRequired: true, globalize: globalize, allowReservedIds: allowReservedIds, allowIdReferenceSyntax: allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames: ignoreElementsWithAutoIDsAndDuplicateNames, allowedVersions: null, inferredType: null);");
        }

        private string GetFullName(string baseName, bool isAsync)
        {
            string methodDecorator = isAsync ? "Async" : string.Empty;
            return baseName.Contains("{0}") ? string.Format(baseName, methodDecorator) : $"{baseName}{methodDecorator}";
        }

        private string GetReturnType(string baseType, bool isAsync)
        {
            if (baseType == "void")
            {
                return isAsync ? $"Task" : baseType;
            }
            else
            {
                return isAsync ? $"Task<{baseType}>" : baseType;
            }
        }

        private string CallMethod(string lvalue, string baseMethodName, string argList, bool isAsync)
        {
            string assignment = lvalue != null ? $"{lvalue} = " : string.Empty;
            string callQualifier = isAsync ? "await " : string.Empty;
            string coda = isAsync ? ".ConfigureAwait(false)" : string.Empty;

            return $"{assignment}{callQualifier}{this.GetFullName(baseMethodName, isAsync)}({argList}){coda};";
        }
    }
}
