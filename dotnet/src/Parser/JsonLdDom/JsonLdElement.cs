namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;

    /// <summary>
    /// A JSON-LD element, represented by a JSON object.
    /// </summary>
    internal class JsonLdElement
    {
        private static readonly HashSet<byte> WhitespaceBytes = new HashSet<byte>(Encoding.UTF8.GetBytes(" \t\r\n"));

        private readonly IByteLocator byteLocator;
        private readonly byte[] jsonBytes;
        private readonly List<JsonLdProperty> properties;
        private readonly long startByteIndex;
        private readonly long endByteIndex;
        private readonly bool outline;

        private long graphByteIndex;
        private long contextByteIndex;
        private long idByteIndex;
        private long typeByteIndex;
        private long valueByteIndex;
        private long languageByteIndex;

        private long contextValueStartByteIndex;
        private long contextValueEndByteIndex;

        private Dictionary<string, long> keywordByteIndices;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLdElement"/> class.
        /// </summary>
        /// <param name="jsonReader">A <see cref="Utf8JsonReader"/> object that is in the process of reading the JSON-LD source.</param>
        /// <param name="byteLocator">An object that exports the <see cref="IByteLocator"/> interface for converting a byte index to source information.</param>
        /// <param name="jsonBytes">A byte array containing UTF8-encoded JSON-LD text.</param>
        /// <remarks>
        /// precondition: <paramref name="jsonReader"/> points to open brace.
        /// postcondition: <paramref name="jsonReader"/> points to close brace.
        /// </remarks>
        internal JsonLdElement(ref Utf8JsonReader jsonReader, IByteLocator byteLocator, byte[] jsonBytes)
        {
            this.byteLocator = byteLocator;
            this.jsonBytes = jsonBytes;

            this.PropertyCount = 0;

            this.Graph = null;
            this.Context = null;
            this.Id = null;
            this.Types = null;
            this.Keywords = new List<string>();

            this.properties = new List<JsonLdProperty>();

            this.startByteIndex = jsonReader.TokenStartIndex;
            this.endByteIndex = -1;

            this.graphByteIndex = -1;
            this.contextByteIndex = -1;
            this.idByteIndex = -1;
            this.typeByteIndex = -1;
            this.valueByteIndex = -1;
            this.languageByteIndex = -1;

            this.contextValueStartByteIndex = -1;
            this.contextValueEndByteIndex = -1;

            this.keywordByteIndices = new Dictionary<string, long>();

            jsonReader.Read();
            while (jsonReader.TokenType == JsonTokenType.PropertyName)
            {
                ++this.PropertyCount;
                long propertyByteIndex = jsonReader.TokenStartIndex;
                string propertyName = jsonReader.GetString();
                jsonReader.Read();

                if (propertyName[0] == '@')
                {
                    switch (propertyName)
                    {
                        case "@graph":
                            this.SetGraph(propertyByteIndex, ref jsonReader);
                            break;
                        case "@context":
                            this.SetContext(propertyByteIndex, ref jsonReader);
                            break;
                        case "@id":
                            this.SetId(propertyByteIndex, ref jsonReader);
                            break;
                        case "@type":
                            this.SetTypes(propertyByteIndex, ref jsonReader);
                            break;
                        case "@value":
                            this.SetValue(propertyByteIndex, ref jsonReader);
                            break;
                        case "@language":
                            this.SetLanguage(propertyByteIndex, ref jsonReader);
                            break;
                        default:
                            this.RecordKeyword(propertyName, propertyByteIndex, ref jsonReader);
                            break;
                    }

                    jsonReader.Read();
                }
                else
                {
                    this.properties.Add(new JsonLdProperty(propertyByteIndex, propertyName, ref jsonReader, byteLocator, jsonBytes));
                }
            }

            this.endByteIndex = jsonReader.TokenStartIndex;

            this.outline = PartitionTypeCollection.IncludesPartitionType(this.Types);
        }

        /// <summary>
        /// Gets the count of JSON properties in this element, including those starting with '@'.
        /// </summary>
        internal int PropertyCount { get; }

        /// <summary>
        /// Gets a <see cref="JsonLdValueCollection"/> of the values of the '@graph' property.
        /// </summary>
        internal List<JsonLdElement> Graph { get; private set; }

        /// <summary>
        /// Gets an array of <see cref="JsonLdContextComponent"/> representing the context components specified by the element's '@context' property.
        /// </summary>
        internal JsonLdContextComponent[] Context { get; private set; }

        /// <summary>
        /// Gets a string indicating the identifier specified by the element's '@id' property.
        /// </summary>
        internal string Id { get; private set; }

        /// <summary>
        /// Gets a list of strings indicating the types specified by the element's '@type' property.
        /// </summary>
        internal List<string> Types { get; private set; }

        /// <summary>
        /// Gets a <see cref="JsonLdValue"/> indicating the value specified by the element's '@value' property.
        /// </summary>
        internal JsonLdValue Value { get; private set; }

        /// <summary>
        /// Gets a string indicating the language specified by the element's '@language' property.
        /// </summary>
        internal string Language { get; private set; }

        /// <summary>
        /// Gets a list of strings indicating unsupported JSON-LD keyword properties.
        /// </summary>
        internal List<string> Keywords { get; private set; }

        /// <summary>
        /// Gets an enumeration of <see cref="JsonLdProperty"/> representing the properties (other than those prefixed by '@') defined by the element.
        /// </summary>
        internal IEnumerable<JsonLdProperty> Properties
        {
            get => this.properties;
        }

        /// <summary>
        /// Gets the JSON text of the element, with any nested elements outlined if their types include a partition type.
        /// </summary>
        /// <param name="contextString">A '@context' string value to add to the element, replacing any context already defined by the element, if any.</param>
        /// <returns>The JSON text of the element, outlined as appropriate.</returns>
        internal string GetOutlinedJsonText(string contextString)
        {
            StringBuilder jsonTextBuilder = new StringBuilder();

            long byteIndex = this.GetCopyByteIndex(jsonTextBuilder, contextString);

            foreach (JsonLdProperty jsonLdProperty in this.Properties)
            {
                foreach (IdentifiedByteRange byteRange in jsonLdProperty.GetOutlineByteRanges())
                {
                    this.AppendJsonBytes(jsonTextBuilder, byteIndex, byteRange.StartIndex - 1, contextString);
                    jsonTextBuilder.Append($"\"{byteRange.Id}\"");
                    byteIndex = byteRange.EndIndex + 1;
                }
            }

            this.AppendJsonBytes(jsonTextBuilder, byteIndex, this.endByteIndex, contextString);

            return jsonTextBuilder.ToString();
        }

        /// <summary>
        /// Gets the source name and line number range corresponding to the element, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="startLine">The line number on which the element begins, one-indexed.</param>
        /// <param name="endLine">The line number on which the element ends, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocation(out string name, out int startLine, out int endLine)
        {
            bool gotStart = this.byteLocator.TryGetSourceLocation(this.startByteIndex, out name, out startLine);
            if (this.startByteIndex == this.endByteIndex)
            {
                endLine = startLine;
                return gotStart;
            }
            else
            {
                bool gotEnd = this.byteLocator.TryGetSourceLocation(this.endByteIndex, out _, out endLine);
                return gotStart && gotEnd;
            }
        }

        /// <summary>
        /// Gets the source name and line number corresponding to the '@graph' property, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@graph' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForGraph(out string name, out int line)
        {
            return this.byteLocator.TryGetSourceLocation(this.graphByteIndex, out name, out line);
        }

        /// <summary>
        /// Gets the source name and line number corresponding to the '@context' property, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@context' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForContext(out string name, out int line)
        {
            return this.byteLocator.TryGetSourceLocation(this.contextByteIndex, out name, out line);
        }

        /// <summary>
        /// Gets the source name and line number corresponding to the '@id' property, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@id' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForId(out string name, out int line)
        {
            return this.byteLocator.TryGetSourceLocation(this.idByteIndex, out name, out line);
        }

        /// <summary>
        /// Gets the source name and line number corresponding to the '@type' property, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@type' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForType(out string name, out int line)
        {
            return this.byteLocator.TryGetSourceLocation(this.typeByteIndex, out name, out line);
        }

        /// <summary>
        /// Gets the source name and line number corresponding to the '@value' property, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@value' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForValue(out string name, out int line)
        {
            return this.byteLocator.TryGetSourceLocation(this.valueByteIndex, out name, out line);
        }

        /// <summary>
        /// Gets the source name and line number corresponding to the '@language' property, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@language' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForLanguage(out string name, out int line)
        {
            return this.byteLocator.TryGetSourceLocation(this.languageByteIndex, out name, out line);
        }

        /// <summary>
        /// Gets the source name and line number corresponding to the given keyword property, if possible.
        /// </summary>
        /// <param name="propertyName">The property name used as a keyword.</param>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@value' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForKeyword(string propertyName, out string name, out int line)
        {
            switch (propertyName)
            {
                case "@graph":
                    return this.TryGetSourceLocationForGraph(out name, out line);
                case "@context":
                    return this.TryGetSourceLocationForContext(out name, out line);
                case "@id":
                    return this.TryGetSourceLocationForId(out name, out line);
                case "@type":
                    return this.TryGetSourceLocationForType(out name, out line);
                case "@value":
                    return this.TryGetSourceLocationForValue(out name, out line);
                case "@language":
                    return this.TryGetSourceLocationForLanguage(out name, out line);
            }

            if (this.keywordByteIndices.TryGetValue(propertyName, out long byteIndex))
            {
                return this.byteLocator.TryGetSourceLocation(byteIndex, out name, out line);
            }
            else
            {
                name = null;
                line = 0;
                return false;
            }
        }

        /// <summary>
        /// Report the association between an identifier and a given byte range to the given <see cref="IByteRangeRecorder"/>.
        /// </summary>
        /// <param name="byteRangeRecorder">The <see cref="IByteRangeRecorder"/> to which to report the association.</param>
        internal void ReportIdentifiedByteRange(IByteRangeRecorder byteRangeRecorder)
        {
            if (this.startByteIndex >= 0 && this.endByteIndex >= 0 && this.Id != null)
            {
                byteRangeRecorder.RecordByteRange(this.startByteIndex, this.endByteIndex, this.Id);
            }
        }

        /// <summary>
        /// Get an enumeration of <see cref="IdentifiedByteRange"/> indicating each byte range that is to be outlined and the corresponding identifier.
        /// </summary>
        /// <returns>The enumeration of <see cref="IdentifiedByteRange"/>.</returns>
        internal IEnumerable<IdentifiedByteRange> GetOutlineByteRanges()
        {
            if (this.outline)
            {
                yield return new IdentifiedByteRange() { StartIndex = this.startByteIndex, EndIndex = this.endByteIndex, Id = this.Id };
            }
            else
            {
                foreach (JsonLdProperty jsonLdProperty in this.Properties)
                {
                    foreach (IdentifiedByteRange byteRange in jsonLdProperty.GetOutlineByteRanges())
                    {
                        yield return byteRange;
                    }
                }
            }
        }

        private long GetCopyByteIndex(StringBuilder jsonTextBuilder, string contextString)
        {
            if (contextString == null || this.Context != null)
            {
                return this.startByteIndex;
            }

            long nextByteIndex = this.startByteIndex + 1;
            while (WhitespaceBytes.Contains(this.jsonBytes[nextByteIndex]))
            {
                ++nextByteIndex;
            }

            jsonTextBuilder.Append(Encoding.UTF8.GetString(this.jsonBytes, (int)this.startByteIndex, (int)nextByteIndex - (int)this.startByteIndex));
            jsonTextBuilder.Append($"\"@context\": {contextString},");
            jsonTextBuilder.Append(Encoding.UTF8.GetString(this.jsonBytes, (int)this.startByteIndex + 1, (int)nextByteIndex - (int)this.startByteIndex - 1));

            return nextByteIndex;
        }

        private void AppendJsonBytes(StringBuilder jsonTextBuilder, long startByteIndex, long endByteIndex, string contextString)
        {
            if (contextString != null && startByteIndex < this.contextValueStartByteIndex && this.contextValueEndByteIndex < endByteIndex)
            {
                jsonTextBuilder.Append(Encoding.UTF8.GetString(this.jsonBytes, (int)startByteIndex, (int)this.contextValueStartByteIndex - (int)startByteIndex));
                jsonTextBuilder.Append(contextString);
                jsonTextBuilder.Append(Encoding.UTF8.GetString(this.jsonBytes, (int)this.contextValueEndByteIndex + 1, (int)endByteIndex - (int)this.contextValueEndByteIndex));
            }
            else
            {
                jsonTextBuilder.Append(Encoding.UTF8.GetString(this.jsonBytes, (int)startByteIndex, (int)endByteIndex - (int)startByteIndex + 1));
            }
        }

        // precondition: jsonReader points to @graph value start (open brace or bracket)
        // postcondition: jsonReader points to @graph value end (close brace or bracket)
        private void SetGraph(long propertyByteIndex, ref Utf8JsonReader jsonReader)
        {
            if (this.graphByteIndex >= 0)
            {
                if (this.byteLocator.TryGetSourceLocation(this.graphByteIndex, out string sourceName, out int firstSourceLine) && this.byteLocator.TryGetSourceLocation(propertyByteIndex, out _, out int secondSourceLine))
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedGraphProperty"),
                        $"In {sourceName}, invalid JSON -- '@graph' property repeated on lines {firstSourceLine} and {secondSourceLine}.",
                        "Remove one or more '@graph' properties so that only one remains.",
                        sourceName1: sourceName,
                        startLine1: firstSourceLine,
                        sourceName2: sourceName,
                        startLine2: secondSourceLine);
                }
                else
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedGraphProperty"),
                        "Invalid JSON -- '@graph' property repeated in JSON object.",
                        "Remove one or more '@graph' properties so that only one remains.");
                }
            }

            this.graphByteIndex = propertyByteIndex;

            if (jsonReader.TokenType == JsonTokenType.StartObject)
            {
                this.Graph = new List<JsonLdElement>() { new JsonLdElement(ref jsonReader, this.byteLocator, this.jsonBytes) };
            }
            else if (jsonReader.TokenType == JsonTokenType.StartArray)
            {
                this.Graph = new List<JsonLdElement>();

                jsonReader.Read();
                while (jsonReader.TokenType != JsonTokenType.EndArray)
                {
                    if (jsonReader.TokenType == JsonTokenType.StartObject)
                    {
                        this.Graph.Add(new JsonLdElement(ref jsonReader, this.byteLocator, this.jsonBytes));
                    }
                    else
                    {
                        this.byteLocator.TryGetSourceLocation(jsonReader.TokenStartIndex, out string sourceName, out int sourceLine);
                        throw new SingletonParsingException(
                            new Dtmi("dtmi:dtdl:parsingError:graphNotObject"),
                            "In {sourceName1}, '@graph' value{line1} is not a JSON object.",
                            "Change all values of property '@graph' to JSON objects.",
                            sourceName1: sourceName,
                            startLine1: sourceLine);
                    }

                    jsonReader.Read();
                }
            }
            else
            {
                this.byteLocator.TryGetSourceLocation(jsonReader.TokenStartIndex, out string sourceName, out int sourceLine);
                throw new SingletonParsingException(
                    new Dtmi("dtmi:dtdl:parsingError:graphNotObject"),
                    "In {sourceName1}, '@graph' value{line1} is not a JSON object.",
                    "Change all values of property '@graph' to JSON objects.",
                    sourceName1: sourceName,
                    startLine1: sourceLine);
            }
        }

        // precondition: jsonReader points to @context value start (string literal, open brace, or open bracket)
        // postcondition: jsonReader points to @context value end (string literal, close brace, or close bracket)
        private void SetContext(long propertyByteIndex, ref Utf8JsonReader jsonReader)
        {
            if (this.contextByteIndex >= 0)
            {
                if (this.byteLocator.TryGetSourceLocation(this.contextByteIndex, out string sourceName, out int firstSourceLine) && this.byteLocator.TryGetSourceLocation(propertyByteIndex, out _, out int secondSourceLine))
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedContextProperty"),
                        $"In {sourceName}, invalid JSON -- '@context' property repeated on lines {firstSourceLine} and {secondSourceLine}.",
                        "Remove one or more '@context' properties so that only one remains.",
                        sourceName1: sourceName,
                        startLine1: firstSourceLine,
                        sourceName2: sourceName,
                        startLine2: secondSourceLine);
                }
                else
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedContextProperty"),
                        "Invalid JSON -- '@context' property repeated in JSON object.",
                        "Remove one or more '@context' properties so that only one remains.");
                }
            }

            this.contextByteIndex = propertyByteIndex;
            this.contextValueStartByteIndex = jsonReader.TokenStartIndex;

            if (jsonReader.TokenType == JsonTokenType.StartArray)
            {
                List<JsonLdContextComponent> contextComponents = new List<JsonLdContextComponent>();

                jsonReader.Read();
                while (jsonReader.TokenType != JsonTokenType.EndArray)
                {
                    contextComponents.Add(new JsonLdContextComponent(ref jsonReader, this.byteLocator));
                    jsonReader.Read();
                }

                this.Context = contextComponents.ToArray();
                this.contextValueEndByteIndex = jsonReader.TokenStartIndex;
            }
            else
            {
                JsonLdContextComponent contextComponent = new JsonLdContextComponent(ref jsonReader, this.byteLocator);
                this.Context = new JsonLdContextComponent[] { contextComponent };
                this.contextValueEndByteIndex = jsonReader.TokenStartIndex + (contextComponent.IsLocal ? 0 : contextComponent.RemoteId.Length + 1);
            }
        }

        // precondition: jsonReader points to @id string literal value
        // postcondition: jsonReader points to @id string literal value
        private void SetId(long propertyByteIndex, ref Utf8JsonReader jsonReader)
        {
            if (this.idByteIndex >= 0)
            {
                if (this.byteLocator.TryGetSourceLocation(this.idByteIndex, out string sourceName, out int firstSourceLine) && this.byteLocator.TryGetSourceLocation(propertyByteIndex, out _, out int secondSourceLine))
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedIdProperty"),
                        $"In {sourceName}, invalid JSON -- '@id' property repeated on lines {firstSourceLine} and {secondSourceLine}.",
                        "Remove one or more '@id' properties so that only one remains.",
                        sourceName1: sourceName,
                        startLine1: firstSourceLine,
                        sourceName2: sourceName,
                        startLine2: secondSourceLine);
                }
                else
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedIdProperty"),
                        "Invalid JSON -- '@id' property repeated in JSON object.",
                        "Remove one or more '@id' properties so that only one remains.");
                }
            }

            this.idByteIndex = propertyByteIndex;

            if (jsonReader.TokenType == JsonTokenType.String)
            {
                this.Id = jsonReader.GetString();
            }
            else
            {
                this.Id = string.Empty;
                jsonReader.Skip();
            }
        }

        // precondition: jsonReader points to @type value start (string literal or open bracket)
        // postcondition: jsonReader points to @type value end (string literal or close bracket)
        private void SetTypes(long propertyByteIndex, ref Utf8JsonReader jsonReader)
        {
            if (this.typeByteIndex >= 0)
            {
                if (this.byteLocator.TryGetSourceLocation(this.typeByteIndex, out string sourceName, out int firstSourceLine) && this.byteLocator.TryGetSourceLocation(propertyByteIndex, out _, out int secondSourceLine))
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedTypeProperty"),
                        $"In {sourceName}, invalid JSON -- '@type' property repeated on lines {firstSourceLine} and {secondSourceLine}.",
                        "Remove one or more '@type' properties so that only one remains.",
                        sourceName1: sourceName,
                        startLine1: firstSourceLine,
                        sourceName2: sourceName,
                        startLine2: secondSourceLine);
                }
                else
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedTypeProperty"),
                        "Invalid JSON -- '@type' property repeated in JSON object.",
                        "Remove one or more '@type' properties so that only one remains.");
                }
            }

            this.typeByteIndex = propertyByteIndex;

            if (jsonReader.TokenType == JsonTokenType.String)
            {
                this.Types = new List<string>() { jsonReader.GetString() };
            }
            else if (jsonReader.TokenType == JsonTokenType.StartArray)
            {
                this.Types = new List<string>();

                jsonReader.Read();
                while (jsonReader.TokenType != JsonTokenType.EndArray)
                {
                    this.Types.Add(jsonReader.TokenType == JsonTokenType.String ? jsonReader.GetString() : null);
                    jsonReader.Read();
                }
            }
            else
            {
                this.Types = new List<string>();
                jsonReader.Skip();
            }
        }

        // precondition: jsonReader points to @value literal value
        // postcondition: jsonReader points to @value literal value
        private void SetValue(long propertyByteIndex, ref Utf8JsonReader jsonReader)
        {
            if (this.valueByteIndex >= 0)
            {
                if (this.byteLocator.TryGetSourceLocation(this.valueByteIndex, out string sourceName, out int firstSourceLine) && this.byteLocator.TryGetSourceLocation(propertyByteIndex, out _, out int secondSourceLine))
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedValueProperty"),
                        $"In {sourceName}, invalid JSON -- '@value' property repeated on lines {firstSourceLine} and {secondSourceLine}.",
                        "Remove one or more '@value' properties so that only one remains.",
                        sourceName1: sourceName,
                        startLine1: firstSourceLine,
                        sourceName2: sourceName,
                        startLine2: secondSourceLine);
                }
                else
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedValueProperty"),
                        "Invalid JSON -- '@value' property repeated in JSON object.",
                        "Remove one or more '@value' properties so that only one remains.");
                }
            }

            this.valueByteIndex = propertyByteIndex;

            this.Value = new JsonLdValue(ref jsonReader, this.byteLocator, this.jsonBytes);
        }

        // precondition: jsonReader points to @language string literal value
        // postcondition: jsonReader points to @language string literal value
        private void SetLanguage(long propertyByteIndex, ref Utf8JsonReader jsonReader)
        {
            if (this.languageByteIndex >= 0)
            {
                if (this.byteLocator.TryGetSourceLocation(this.languageByteIndex, out string sourceName, out int firstSourceLine) && this.byteLocator.TryGetSourceLocation(propertyByteIndex, out _, out int secondSourceLine))
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedLanguageProperty"),
                        $"In {sourceName}, invalid JSON -- '@language' property repeated on lines {firstSourceLine} and {secondSourceLine}.",
                        "Remove one or more '@language' properties so that only one remains.",
                        sourceName1: sourceName,
                        startLine1: firstSourceLine,
                        sourceName2: sourceName,
                        startLine2: secondSourceLine);
                }
                else
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedLanguageProperty"),
                        "Invalid JSON -- '@language' property repeated in JSON object.",
                        "Remove one or more '@language' properties so that only one remains.");
                }
            }

            this.languageByteIndex = propertyByteIndex;

            if (jsonReader.TokenType == JsonTokenType.String)
            {
                this.Language = jsonReader.GetString();
            }
            else
            {
                this.Language = string.Empty;
                jsonReader.Skip();
            }
        }

        // precondition: jsonReader points to keyword value start
        // postcondition: jsonReader points to keyword value end
        private void RecordKeyword(string propertyName, long propertyByteIndex, ref Utf8JsonReader jsonReader)
        {
            if (this.keywordByteIndices.TryGetValue(propertyName, out long byteIndex))
            {
                if (this.byteLocator.TryGetSourceLocation(byteIndex, out string sourceName, out int firstSourceLine) && this.byteLocator.TryGetSourceLocation(propertyByteIndex, out _, out int secondSourceLine))
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedKeyword"),
                        $"In {sourceName}, invalid JSON -- '{propertyName}' property repeated on lines {firstSourceLine} and {secondSourceLine}.",
                        $"Remove one or more '{propertyName}' properties so that at most one remains.",
                        sourceName1: sourceName,
                        startLine1: firstSourceLine,
                        sourceName2: sourceName,
                        startLine2: secondSourceLine);
                }
                else
                {
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:repeatedKeyword"),
                        $"Invalid JSON -- '{propertyName}' property repeated in JSON object.",
                        $"Remove one or more '{propertyName}' properties so that at most one remains.");
                }
            }

            this.Keywords.Add(propertyName);
            this.keywordByteIndices[propertyName] = propertyByteIndex;

            jsonReader.Skip();
        }
    }
}
