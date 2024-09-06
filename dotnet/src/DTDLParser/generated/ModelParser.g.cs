/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using DTDLParser.Models;

    /// <summary>
    /// Instances of the <c>ModelParser</c> class parse models written in the DTDL language.
    /// This class can be used to determine whether one or more DTDL models are valid, to identify specific modeling errors, and to enable inspection of model contents.
    /// </summary>
    /// <remarks>
    /// The primary methods on this class are <see cref="ModelParser.Parse(IEnumerable{string}, DtdlParseLocator)"/> and <see cref="ModelParser.ParseAsync(IAsyncEnumerable{string}, DtdlParseLocator, CancellationToken,)"/>.
    /// </remarks>
    public partial class ModelParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelParser"/> class.
        /// </summary>
        /// <param name="parsingOptions">An optional <see cref="ParsingOptions"/> object whose properties configure the <see cref="ModelParser"/> behavior.</param>
        /// <param name="quirks">An optional set of <see cref="ModelParsingQuirk"/> values that control parsing of DTDL models.</param>
        public ModelParser(ParsingOptions parsingOptions = null, ModelParsingQuirk quirks = ModelParsingQuirk.None)
        {
            this.contextCollection = new ContextCollection(parsingOptions?.ExtensionLimitContexts ?? new List<Dtmi>());
            this.supplementalTypeCollection = new SupplementalTypeCollection();
            this.standardElementCollection = new StandardElementCollection();

            if (parsingOptions != null)
            {
                this.dtmiResolver = parsingOptions.DtmiResolver;
                this.dtmiResolverAsync = parsingOptions.DtmiResolverAsync;
                this.dtdlResolveLocator = parsingOptions.DtdlResolveLocator;
                this.MaxDtdlVersion = Math.Min(Math.Max(parsingOptions.MaxDtdlVersion, ParsingOptions.MinKnownDtdlVersion), ParsingOptions.MaxKnownDtdlVersion);
                this.AllowUndefinedExtensions = parsingOptions.AllowUndefinedExtensions;
            }
            else
            {
                this.dtmiResolver = null;
                this.dtmiResolverAsync = null;
                this.dtdlResolveLocator = null;
                this.MaxDtdlVersion = ParsingOptions.MaxKnownDtdlVersion;
                this.AllowUndefinedExtensions = WhenToAllow.PerDefault;
            }

            this.jsonLdResolvingReader = new JsonLdReader(this.dtdlResolveLocator);
            this.quirks = quirks;

            this.readerWriterAsyncLock = null;
        }

        /// <summary>
        /// Gets an object model representing all the model-level elements implicitly available for reference.
        /// </summary>
        /// <returns>A dictionary that maps each <c>Dtmi</c> to a subclass of <c>DTEntityInfo</c>.</returns>
        public IReadOnlyDictionary<Dtmi, DTEntityInfo> GetImplicitElements()
        {
            IReadOnlyDictionary<Dtmi, DTEntityInfo> standardElements;

            this.readerWriterAsyncLock?.EnterReadLock();
            try
            {
                standardElements = this.standardElementCollection.GetStandardElements();
            }
            finally
            {
                this.readerWriterAsyncLock?.ExitReadLock();
            }

            return standardElements;
        }

        /// <summary>
        /// Parse a collection of JSON text strings as DTDL models.
        /// </summary>
        /// <param name="jsonTexts">The JSON text strings to parse as DTDL models.</param>
        /// <param name="dtdlLocator">An optional <see cref="DtdlParseLocator"/> delegate for converting a parse index and line number into a source name and line number.</param>
        /// <returns>A dictionary that maps each <c>Dtmi</c> to a subclass of <c>DTEntityInfo</c>.</returns>
        public IReadOnlyDictionary<Dtmi, DTEntityInfo> Parse(IEnumerable<string> jsonTexts, DtdlParseLocator dtdlLocator = null)
        {
            Model model;
            if (this.readerWriterAsyncLock == null)
            {
                model = this.ParseInternal(jsonTexts, dtdlLocator);
            }
            else
            {
                this.readerWriterAsyncLock.EnterReadLock();
                try
                {
                    model = this.ParseInternal(jsonTexts, dtdlLocator);
                }
                finally
                {
                    this.readerWriterAsyncLock.ExitReadLock();
                }
            }

            return model.Dict;
        }

        /// <summary>
        /// Parse a JSON text string as DTDL models.
        /// </summary>
        /// <param name="jsonText">The JSON text string to parse as DTDL models.</param>
        /// <param name="dtdlLocator">An optional <see cref="DtdlParseLocator"/> delegate for converting a parse index and line number into a source name and line number.</param>
        /// <returns>A dictionary that maps each <c>Dtmi</c> to a subclass of <c>DTEntityInfo</c>.</returns>
        public IReadOnlyDictionary<Dtmi, DTEntityInfo> Parse(string jsonText, DtdlParseLocator dtdlLocator = null)
        {
            return this.Parse(this.StringToEnumerable(jsonText), dtdlLocator);
        }

        /// <summary>
        /// Parse a collection of JSON text strings as DTDL models and return the result as a JSON object model.
        /// </summary>
        /// <param name="jsonTexts">The JSON text strings to parse as DTDL models.</param>
        /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
        /// <returns>A string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object in accordance with DtdlOm.d.ts.</returns>
        public string ParseToJson(IEnumerable<string> jsonTexts, bool indent = false)
        {
            try
            {
                IReadOnlyDictionary<Dtmi, DTEntityInfo> model = this.Parse(jsonTexts);

                using (MemoryStream memStream = new MemoryStream())
                {
                    JsonWriterOptions jsonWriterOptions = new JsonWriterOptions { Indented = indent };
                    using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(memStream, jsonWriterOptions))
                    {
                        jsonWriter.WriteStartObject();

                        foreach (KeyValuePair<Dtmi, DTEntityInfo> kvp in model)
                        {
                            jsonWriter.WritePropertyName(kvp.Key.ToString());
                            jsonWriter.WriteStartObject();
                            kvp.Value.WriteToJson(jsonWriter, includeClassId: true);
                            jsonWriter.WriteEndObject();
                        }

                        jsonWriter.WriteEndObject();

                        jsonWriter.Flush();
                    }

                    return Encoding.UTF8.GetString(memStream.ToArray());
                }
            }
            catch (ParsingException pex)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    JsonWriterOptions jsonWriterOptions = new JsonWriterOptions { Indented = indent };
                    using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(memStream, jsonWriterOptions))
                    {
                        pex.WriteToJson(jsonWriter);
                        jsonWriter.Flush();
                    }

                    throw new Exception(Encoding.UTF8.GetString(memStream.ToArray()));
                }
            }
            catch (ResolutionException rex)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    JsonWriterOptions jsonWriterOptions = new JsonWriterOptions { Indented = indent };
                    using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(memStream, jsonWriterOptions))
                    {
                        rex.WriteToJson(jsonWriter);
                        jsonWriter.Flush();
                    }

                    throw new Exception(Encoding.UTF8.GetString(memStream.ToArray()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Parse a JSON text string as DTDL models and return the result as a JSON object model.
        /// </summary>
        /// <param name="jsonText">The JSON text string to parse as DTDL models.</param>
        /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
        /// <returns>A string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object in accordance with DtdlOm.d.ts.</returns>
        public string ParseToJson(string jsonText, bool indent = false)
        {
            return this.ParseToJson(this.StringToEnumerable(jsonText), indent);
        }

        /// <summary>
        /// Asynchronously parse a collection of JSON text strings as DTDL models.
        /// </summary>
        /// <param name="jsonTexts">The JSON text strings to parse as DTDL models.</param>
        /// <param name="dtdlLocator">An optional <see cref="DtdlParseLocator"/> delegate for converting a parse index and line number into a source name and line number.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to cancel the parsing prematurely.</param>
        /// <returns>A <c>Task</c> object whose <c>Result</c> property is a dictionary that maps each <c>Dtmi</c> to a subclass of <c>DTEntityInfo</c>.</returns>
        public async Task<IReadOnlyDictionary<Dtmi, DTEntityInfo>> ParseAsync(IAsyncEnumerable<string> jsonTexts, DtdlParseLocator dtdlLocator = null, CancellationToken cancellationToken = default)
        {
            Model model;
            if (this.readerWriterAsyncLock == null)
            {
                model = await this.ParseInternalAsync(jsonTexts, dtdlLocator, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await this.readerWriterAsyncLock.EnterReadLockAsync().ConfigureAwait(false);
                try
                {
                    model = await this.ParseInternalAsync(jsonTexts, dtdlLocator, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    this.readerWriterAsyncLock.ExitReadLock();
                }
            }

            return model.Dict;
        }

        /// <summary>
        /// Asynchronously parse a JSON text string as DTDL models.
        /// </summary>
        /// <param name="jsonText">The JSON text string to parse as DTDL models.</param>
        /// <param name="dtdlLocator">An optional <see cref="DtdlParseLocator"/> delegate for converting a parse index and line number into a source name and line number.</param>
        /// <param name="cancellationToken">A <c>CancellationToken</c> to cancel the parsing prematurely.</param>
        /// <returns>A <c>Task</c> object whose <c>Result</c> property is a dictionary that maps each <c>Dtmi</c> to a subclass of <c>DTEntityInfo</c>.</returns>
        public async Task<IReadOnlyDictionary<Dtmi, DTEntityInfo>> ParseAsync(string jsonText, DtdlParseLocator dtdlLocator = null, CancellationToken cancellationToken = default)
        {
            return await this.ParseAsync(this.StringToAsyncEnumerable(jsonText), dtdlLocator, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously parse a collection of JSON text strings as DTDL models and return the result as a JSON object model.
        /// </summary>
        /// <param name="jsonTexts">The JSON text strings to parse as DTDL models.</param>
        /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
        /// <returns>A <c>Task</c> object whose <c>Result</c> property is a string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object in accordance with DtdlOm.d.ts.</returns>
        public async Task<string> ParseToJsonAsync(IAsyncEnumerable<string> jsonTexts, bool indent = false)
        {
            try
            {
                IReadOnlyDictionary<Dtmi, DTEntityInfo> model = await this.ParseAsync(jsonTexts).ConfigureAwait(false);

                using (MemoryStream memStream = new MemoryStream())
                {
                    JsonWriterOptions jsonWriterOptions = new JsonWriterOptions { Indented = indent };
                    using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(memStream, jsonWriterOptions))
                    {
                        jsonWriter.WriteStartObject();

                        foreach (KeyValuePair<Dtmi, DTEntityInfo> kvp in model)
                        {
                            jsonWriter.WritePropertyName(kvp.Key.ToString());
                            jsonWriter.WriteStartObject();
                            kvp.Value.WriteToJson(jsonWriter, includeClassId: true);
                            jsonWriter.WriteEndObject();
                        }

                        jsonWriter.WriteEndObject();

                        jsonWriter.Flush();
                    }

                    return Encoding.UTF8.GetString(memStream.ToArray());
                }
            }
            catch (ParsingException pex)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    JsonWriterOptions jsonWriterOptions = new JsonWriterOptions { Indented = indent };
                    using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(memStream, jsonWriterOptions))
                    {
                        pex.WriteToJson(jsonWriter);
                        jsonWriter.Flush();
                    }

                    throw new Exception(Encoding.UTF8.GetString(memStream.ToArray()));
                }
            }
            catch (ResolutionException rex)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    JsonWriterOptions jsonWriterOptions = new JsonWriterOptions { Indented = indent };
                    using (Utf8JsonWriter jsonWriter = new Utf8JsonWriter(memStream, jsonWriterOptions))
                    {
                        rex.WriteToJson(jsonWriter);
                        jsonWriter.Flush();
                    }

                    throw new Exception(Encoding.UTF8.GetString(memStream.ToArray()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously parse a JSON text string as DTDL models and return the result as a JSON object model.
        /// </summary>
        /// <param name="jsonText">The JSON text string to parse as DTDL models.</param>
        /// <param name="indent">Optional boolean parameter to indent the returned JSON text for improved readability; defaults to false.</param>
        /// <returns>A <c>Task</c> object whose <c>Result</c> property is a string representing a JSON object that maps each DTMI as a string to a DTDL element encoded as a JSON object in accordance with DtdlOm.d.ts.</returns>
        public async Task<string> ParseToJsonAsync(string jsonText, bool indent = false)
        {
            return await this.ParseToJsonAsync(this.StringToAsyncEnumerable(jsonText), indent).ConfigureAwait(false);
        }

        /// <summary>
        /// Parse an element encoded in a <see cref="JsonLdElement"/>.
        /// </summary>
        /// <param name="model">The model to add the element to.</param>
        /// <param name="objectPropertyInfoList">A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> to parse.</param>
        /// <param name="globalize">Treat all nested definitions as though defined globally.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="tolerateSolecisms">Tolerate specific minor invalidities to support backward compatibility.</param>
        internal static void ParseElement(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, bool globalize, bool allowReservedIds, bool tolerateSolecisms)
        {
            DTEntityInfo.TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, null, aggregateContext, parsingErrorCollection, elt, layer: null, parentId: null, definedIn: null, propName: null, propProp: null, dtmiSeg: null, keyProp: null, idRequired: true, typeRequired: true, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: null, inferredType: null);
        }

        private Model ParseInternal(IEnumerable<string> jsonTexts, DtdlParseLocator dtdlLocator)
        {
            var model = new Model();
            var objectPropertyInfoList = new List<ParsedObjectPropertyInfo>();
            var elementPropertyConstraints = new List<ElementPropertyConstraint>();
            var parsingErrorCollection = new ParsingErrorCollection();

            this.ParseAndResolveAsNeeded(jsonTexts, dtdlLocator, model, objectPropertyInfoList, elementPropertyConstraints, parsingErrorCollection);
            model.SetObjectProperties(objectPropertyInfoList, parsingErrorCollection, this.standardElementCollection, preserveElementAliases: false);
            parsingErrorCollection.ThrowIfAny();

            model.CheckElementPropertyConstraints(elementPropertyConstraints, this.supplementalTypeCollection, this.standardElementCollection, parsingErrorCollection, preserveElementAliases: false);
            model.ApplyTransformations(parsingErrorCollection);
            model.CheckRestrictionsAndSiblingConstraints(elementPropertyConstraints, this.supplementalTypeCollection, parsingErrorCollection);
            parsingErrorCollection.ThrowIfAny();

            return model;
        }

        private async Task ParseAndResolveAsNeededAsync(IAsyncEnumerable<string> jsonTexts, DtdlParseLocator dtdlLocator, CancellationToken cancellationToken, Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, ParsingErrorCollection parsingErrorCollection)
        {
            JsonLdReader jsonLdParsingReader = new JsonLdReader(dtdlLocator);

            await this.ParseTextsIntoModelAsync(
                jsonTexts,
                cancellationToken,
                jsonLdParsingReader,
                model,
                objectPropertyInfoList,
                elementPropertyConstraints,
                parsingErrorCollection,
                allowReservedIds: false,
                tolerateSolecisms: (this.quirks & ModelParsingQuirk.TolerateSolecismsInParse) != 0).ConfigureAwait(false);

            while (true)
            {
                Dictionary<Dtmi, DtmiReferenceTracker> undefinedIdentifiers = new Dictionary<Dtmi, DtmiReferenceTracker>();

                foreach (ParsedObjectPropertyInfo objectPropertyInfo in objectPropertyInfoList)
                {
                    if (!model.Dict.ContainsKey(objectPropertyInfo.ReferencedElementId))
                    {
                        if (!this.standardElementCollection.TryAddElementToModel(model, objectPropertyInfo.ReferencedElementId, preserveElementAliases: false))
                        {
                            if (objectPropertyInfo.ReferencedIdString != null && !objectPropertyInfo.ReferencedIdString.StartsWith("dtmi:"))
                            {
                                JsonLdValue propertyValue = objectPropertyInfo.JsonLdProperty.Values.Values.First(v => v.StringValue == objectPropertyInfo.ReferencedIdString);
                                if (objectPropertyInfo.JsonLdProperty.TryGetSourceLocation(out string sourceName1, out int sourceLine1))
                                {
                                    propertyValue.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2);
                                    parsingErrorCollection.Add(
                                        new Uri("dtmi:dtdl:parsingError:badType"),
                                        objectPropertyInfo.BadTypeLocatedCauseFormat,
                                        objectPropertyInfo.BadTypeActionFormat,
                                        primaryId: objectPropertyInfo.ElementId,
                                        property: objectPropertyInfo.PropertyName,
                                        secondaryId: objectPropertyInfo.ReferencedElementId,
                                        sourceName1: sourceName1,
                                        startLine1: sourceLine1,
                                        sourceName2: sourceName2,
                                        startLine2: startLine2,
                                        endLine2: endLine2);
                                }
                                else
                                {
                                    parsingErrorCollection.Add(
                                        new Uri("dtmi:dtdl:parsingError:badType"),
                                        objectPropertyInfo.BadTypeCauseFormat,
                                        objectPropertyInfo.BadTypeActionFormat,
                                        primaryId: objectPropertyInfo.ElementId,
                                        property: objectPropertyInfo.PropertyName,
                                        secondaryId: objectPropertyInfo.ReferencedElementId);
                                }
                            }
                            else if (ContextCollection.IsIdentifierReserved(objectPropertyInfo.ReferencedElementId.AbsoluteUri))
                            {
                                JsonLdValue propertyValue = objectPropertyInfo.JsonLdProperty.Values.Values.First(v => v.StringValue == objectPropertyInfo.ReferencedIdString);
                                parsingErrorCollection.Notify(
                                    "undefinedReservedId",
                                    elementId: objectPropertyInfo.ElementId,
                                    propertyName: objectPropertyInfo.PropertyName,
                                    referenceId: objectPropertyInfo.ReferencedElementId,
                                    incidentProperty: objectPropertyInfo.JsonLdProperty,
                                    incidentValue: propertyValue);
                            }
                            else
                            {
                                if (!undefinedIdentifiers.TryGetValue(objectPropertyInfo.ReferencedElementId, out DtmiReferenceTracker referenceTracker))
                                {
                                    referenceTracker = new DtmiReferenceTracker(objectPropertyInfo.ReferencedElementId);
                                    undefinedIdentifiers[objectPropertyInfo.ReferencedElementId] = referenceTracker;
                                }

                                referenceTracker.AddReference(objectPropertyInfo.JsonLdProperty, objectPropertyInfo.ReferencedIdString);
                            }
                        }
                    }
                }

                parsingErrorCollection.ThrowIfAny();

                if (!undefinedIdentifiers.Any())
                {
                    return;
                }

                if (this.dtmiResolverAsync == null)
                {
                    if (this.dtmiResolver != null)
                    {
                        throw new ResolutionException("Sync/async mismatch: DtmiResolver registered but ParseAsync() method called. Either register a DtmiResolverAsync or call Parse() method instead of ParseAsync().", undefinedIdentifiers);
                    }
                    else
                    {
                        throw new ResolutionException("No DtmiResolverAsync provided to resolve requisite reference(s): ", undefinedIdentifiers, itemizeIdentifers: true);
                    }
                }

                IAsyncEnumerable<string> additionalJsonTexts = this.dtmiResolverAsync(undefinedIdentifiers.Keys, cancellationToken);
                if (additionalJsonTexts == null)
                {
                    throw new ResolutionException("DtmiResolverAsync refused to resolve requisite references to element(s): ", undefinedIdentifiers, itemizeIdentifers: true);
                }

                await this.ParseTextsIntoModelAsync(
                    additionalJsonTexts,
                    cancellationToken,
                    this.jsonLdResolvingReader,
                    model,
                    objectPropertyInfoList,
                    elementPropertyConstraints,
                    parsingErrorCollection,
                    allowReservedIds: false,
                    tolerateSolecisms: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0).ConfigureAwait(false);
                List<Dtmi> requestedIdentifiers = undefinedIdentifiers.Keys.ToList();
                foreach (Dtmi dtmi in requestedIdentifiers)
                {
                    if (model.Dict.ContainsKey(dtmi))
                    {
                        undefinedIdentifiers.Remove(dtmi);
                    }
                }

                if (undefinedIdentifiers.Any())
                {
                    throw new ResolutionException("DtmiResolverAsync failed to resolve requisite references to element(s): ", undefinedIdentifiers, itemizeIdentifers: true);
                }
            }
        }

        private async Task ParseTextsIntoModelAsync(IAsyncEnumerable<string> jsonTexts, CancellationToken cancellationToken, JsonLdReader jsonLdReader, Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, ParsingErrorCollection parsingErrorCollection, bool allowReservedIds, bool tolerateSolecisms)
        {
            int enumerationIndex = 0;
            await foreach (string jsonText in jsonTexts.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                JsonLdValueCollection valueCollection = jsonLdReader.Read(jsonText, enumerationIndex, parsingErrorCollection);
                foreach (JsonLdValue jsonLdValue in valueCollection.Values)
                {
                    this.ParseJsonLdValues(model, objectPropertyInfoList, elementPropertyConstraints, parsingErrorCollection, jsonLdValue, 0, allowReservedIds, tolerateSolecisms);
                }

                parsingErrorCollection.ThrowIfAny();
                ++enumerationIndex;
            }
        }

        private async Task<Model> ParseInternalAsync(IAsyncEnumerable<string> jsonTexts, DtdlParseLocator dtdlLocator, CancellationToken cancellationToken)
        {
            var model = new Model();
            var objectPropertyInfoList = new List<ParsedObjectPropertyInfo>();
            var elementPropertyConstraints = new List<ElementPropertyConstraint>();
            var parsingErrorCollection = new ParsingErrorCollection();

            await this.ParseAndResolveAsNeededAsync(jsonTexts, dtdlLocator, cancellationToken, model, objectPropertyInfoList, elementPropertyConstraints, parsingErrorCollection).ConfigureAwait(false);
            model.SetObjectProperties(objectPropertyInfoList, parsingErrorCollection, this.standardElementCollection, preserveElementAliases: false);
            parsingErrorCollection.ThrowIfAny();

            model.CheckElementPropertyConstraints(elementPropertyConstraints, this.supplementalTypeCollection, this.standardElementCollection, parsingErrorCollection, preserveElementAliases: false);
            model.ApplyTransformations(parsingErrorCollection);
            model.CheckRestrictionsAndSiblingConstraints(elementPropertyConstraints, this.supplementalTypeCollection, parsingErrorCollection);
            parsingErrorCollection.ThrowIfAny();

            return model;
        }

        private void ParseAndResolveAsNeeded(IEnumerable<string> jsonTexts, DtdlParseLocator dtdlLocator, Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, ParsingErrorCollection parsingErrorCollection)
        {
            JsonLdReader jsonLdParsingReader = new JsonLdReader(dtdlLocator);

            this.ParseTextsIntoModel(
                jsonTexts,
                jsonLdParsingReader,
                model,
                objectPropertyInfoList,
                elementPropertyConstraints,
                parsingErrorCollection,
                allowReservedIds: false,
                tolerateSolecisms: (this.quirks & ModelParsingQuirk.TolerateSolecismsInParse) != 0);

            while (true)
            {
                Dictionary<Dtmi, DtmiReferenceTracker> undefinedIdentifiers = new Dictionary<Dtmi, DtmiReferenceTracker>();

                foreach (ParsedObjectPropertyInfo objectPropertyInfo in objectPropertyInfoList)
                {
                    if (!model.Dict.ContainsKey(objectPropertyInfo.ReferencedElementId))
                    {
                        if (!this.standardElementCollection.TryAddElementToModel(model, objectPropertyInfo.ReferencedElementId, preserveElementAliases: false))
                        {
                            if (objectPropertyInfo.ReferencedIdString != null && !objectPropertyInfo.ReferencedIdString.StartsWith("dtmi:"))
                            {
                                JsonLdValue propertyValue = objectPropertyInfo.JsonLdProperty.Values.Values.First(v => v.StringValue == objectPropertyInfo.ReferencedIdString);
                                if (objectPropertyInfo.JsonLdProperty.TryGetSourceLocation(out string sourceName1, out int sourceLine1))
                                {
                                    propertyValue.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2);
                                    parsingErrorCollection.Add(
                                        new Uri("dtmi:dtdl:parsingError:badType"),
                                        objectPropertyInfo.BadTypeLocatedCauseFormat,
                                        objectPropertyInfo.BadTypeActionFormat,
                                        primaryId: objectPropertyInfo.ElementId,
                                        property: objectPropertyInfo.PropertyName,
                                        secondaryId: objectPropertyInfo.ReferencedElementId,
                                        sourceName1: sourceName1,
                                        startLine1: sourceLine1,
                                        sourceName2: sourceName2,
                                        startLine2: startLine2,
                                        endLine2: endLine2);
                                }
                                else
                                {
                                    parsingErrorCollection.Add(
                                        new Uri("dtmi:dtdl:parsingError:badType"),
                                        objectPropertyInfo.BadTypeCauseFormat,
                                        objectPropertyInfo.BadTypeActionFormat,
                                        primaryId: objectPropertyInfo.ElementId,
                                        property: objectPropertyInfo.PropertyName,
                                        secondaryId: objectPropertyInfo.ReferencedElementId);
                                }
                            }
                            else if (ContextCollection.IsIdentifierReserved(objectPropertyInfo.ReferencedElementId.AbsoluteUri))
                            {
                                JsonLdValue propertyValue = objectPropertyInfo.JsonLdProperty.Values.Values.First(v => v.StringValue == objectPropertyInfo.ReferencedIdString);
                                parsingErrorCollection.Notify(
                                    "undefinedReservedId",
                                    elementId: objectPropertyInfo.ElementId,
                                    propertyName: objectPropertyInfo.PropertyName,
                                    referenceId: objectPropertyInfo.ReferencedElementId,
                                    incidentProperty: objectPropertyInfo.JsonLdProperty,
                                    incidentValue: propertyValue);
                            }
                            else
                            {
                                if (!undefinedIdentifiers.TryGetValue(objectPropertyInfo.ReferencedElementId, out DtmiReferenceTracker referenceTracker))
                                {
                                    referenceTracker = new DtmiReferenceTracker(objectPropertyInfo.ReferencedElementId);
                                    undefinedIdentifiers[objectPropertyInfo.ReferencedElementId] = referenceTracker;
                                }

                                referenceTracker.AddReference(objectPropertyInfo.JsonLdProperty, objectPropertyInfo.ReferencedIdString);
                            }
                        }
                    }
                }

                parsingErrorCollection.ThrowIfAny();

                if (!undefinedIdentifiers.Any())
                {
                    return;
                }

                if (this.dtmiResolver == null)
                {
                    if (this.dtmiResolverAsync != null)
                    {
                        throw new ResolutionException("Sync/async mismatch: DtmiResolverAsync registered but Parse() method called. Either register a DtmiResolver or call ParseAsync() method instead of Parse().", undefinedIdentifiers);
                    }
                    else
                    {
                        throw new ResolutionException("No DtmiResolver provided to resolve requisite reference(s): ", undefinedIdentifiers, itemizeIdentifers: true);
                    }
                }

                IEnumerable<string> additionalJsonTexts = this.dtmiResolver(undefinedIdentifiers.Keys);
                if (additionalJsonTexts == null)
                {
                    throw new ResolutionException("DtmiResolver refused to resolve requisite references to element(s): ", undefinedIdentifiers, itemizeIdentifers: true);
                }

                this.ParseTextsIntoModel(
                    additionalJsonTexts,
                    this.jsonLdResolvingReader,
                    model,
                    objectPropertyInfoList,
                    elementPropertyConstraints,
                    parsingErrorCollection,
                    allowReservedIds: false,
                    tolerateSolecisms: (this.quirks & ModelParsingQuirk.TolerateSolecismsInResolve) != 0);
                List<Dtmi> requestedIdentifiers = undefinedIdentifiers.Keys.ToList();
                foreach (Dtmi dtmi in requestedIdentifiers)
                {
                    if (model.Dict.ContainsKey(dtmi))
                    {
                        undefinedIdentifiers.Remove(dtmi);
                    }
                }

                if (undefinedIdentifiers.Any())
                {
                    throw new ResolutionException("DtmiResolver failed to resolve requisite references to element(s): ", undefinedIdentifiers, itemizeIdentifers: true);
                }
            }
        }

        private void ParseTextsIntoModel(IEnumerable<string> jsonTexts, JsonLdReader jsonLdReader, Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, ParsingErrorCollection parsingErrorCollection, bool allowReservedIds, bool tolerateSolecisms)
        {
            int enumerationIndex = 0;
            foreach (string jsonText in jsonTexts)
            {
                JsonLdValueCollection valueCollection = jsonLdReader.Read(jsonText, enumerationIndex, parsingErrorCollection);
                foreach (JsonLdValue jsonLdValue in valueCollection.Values)
                {
                    this.ParseJsonLdValues(model, objectPropertyInfoList, elementPropertyConstraints, parsingErrorCollection, jsonLdValue, 0, allowReservedIds, tolerateSolecisms);
                }

                parsingErrorCollection.ThrowIfAny();
                ++enumerationIndex;
            }
        }
    }
}
