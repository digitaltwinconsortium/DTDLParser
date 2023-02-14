namespace DTDLParser
{
    using System.Text;
    using System.Text.Json;

    /// <summary>
    /// A static class for reading JSON-LD in JSON text into a <see cref="JsonLdValueCollection"/> object.
    /// </summary>
    internal class JsonLdReader
    {
        private DtdlParseLocator parseLocator;
        private DtdlResolveLocator resolveLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLdReader"/> class.
        /// </summary>
        /// <param name="parseLocator">A <see cref="DtdlParseLocator"/> delegate that converts an enumeration index and line number into a source name and line number.</param>
        internal JsonLdReader(DtdlParseLocator parseLocator)
        {
            this.parseLocator = parseLocator;
            this.resolveLocator = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLdReader"/> class.
        /// </summary>
        /// <param name="resolveLocator">A <see cref="DtdlResolveLocator"/> delegate that converts a resolve DTMI and line number into a source name and line number.</param>
        internal JsonLdReader(DtdlResolveLocator resolveLocator)
        {
            this.parseLocator = null;
            this.resolveLocator = resolveLocator;
        }

        /// <summary>
        /// Read the JSON-LD in <paramref name="jsonText"/> into a new <see cref="JsonLdValueCollection"/> object.
        /// </summary>
        /// <param name="jsonText">A string containing JSON text to read.</param>
        /// <param name="parseIndex">An index into an enumeration of JSON text strings passed to the parser.</param>
        /// <param name="parseLocator">A <see cref="DtdlParseLocator"/> delegate that converts an enumeration index and line number into a source name and line number.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <returns>A <see cref="JsonLdValueCollection"/> containing <see cref="JsonLdElement"/> objects representing the parsed JSON text.</returns>
        internal static JsonLdValueCollection ReadForParse(string jsonText, int parseIndex, DtdlParseLocator parseLocator, ParsingErrorCollection parsingErrorCollection)
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonText);

            ParseByteLocator parseByteLocator = new ParseByteLocator(parseIndex, jsonBytes, parseLocator);

            Utf8JsonReader jsonReader = new Utf8JsonReader(jsonBytes);
            jsonReader.Read();

            JsonLdValueCollection valueCollection = null;
            try
            {
                valueCollection = new JsonLdValueCollection(ref jsonReader, parseByteLocator, jsonBytes);
            }
            catch (JsonException jsonException)
            {
                string preamble = string.Empty;
                int lineNumber = jsonException.LineNumber != null ? (int)jsonException.LineNumber + 1 : 0;
                if (parseLocator(parseIndex, lineNumber, out string sourceName, out int sourceLine))
                {
                    preamble = $"In {sourceName}, ";
                    lineNumber = sourceLine;
                }

                int bytePositionInLine = jsonException.BytePositionInLine != null ? (int)jsonException.BytePositionInLine + 1 : 0;
                string message = preamble + jsonException.Message
                    .Replace($"LineNumber: {jsonException.LineNumber}", $"LineNumber: {lineNumber}")
                    .Replace($"BytePositionInLine: {jsonException.BytePositionInLine}", $"BytePositionInLine: {bytePositionInLine}");

                parsingErrorCollection.AddAndThrow(
                    null,
                    message,
                    string.Empty,
                    sourceName1: sourceName,
                    startLine1: lineNumber);
            }
            catch (SingletonParsingException singletonParsingException)
            {
                parsingErrorCollection.AddAndThrow(singletonParsingException);
            }

            return valueCollection;
        }

        /// <summary>
        /// Read the JSON-LD in <paramref name="jsonText"/> into a new <see cref="JsonLdValueCollection"/> object.
        /// </summary>
        /// <param name="jsonText">A string containing JSON text to read.</param>
        /// <param name="resolveLocator">A <see cref="DtdlResolveLocator"/> delegate that converts a resolve DTMI and line number into a source name and line number.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <returns>A <see cref="JsonLdValueCollection"/> containing <see cref="JsonLdElement"/> objects representing the parsed JSON text.</returns>
        internal static JsonLdValueCollection ReadForResolve(string jsonText, DtdlResolveLocator resolveLocator, ParsingErrorCollection parsingErrorCollection)
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonText);

            ResolveByteLocator resolveByteLocator = new ResolveByteLocator(jsonBytes, resolveLocator);

            Utf8JsonReader jsonReader = new Utf8JsonReader(jsonBytes);
            jsonReader.Read();

            JsonLdValueCollection valueCollection = null;

            try
            {
                valueCollection = new JsonLdValueCollection(ref jsonReader, resolveByteLocator, jsonBytes);
            }
            catch (JsonException jsonException)
            {
                int lineNumber = jsonException.LineNumber != null ? (int)jsonException.LineNumber + 1 : 0;
                int bytePositionInLine = jsonException.BytePositionInLine != null ? (int)jsonException.BytePositionInLine + 1 : 0;
                string message = jsonException.Message
                    .Replace($"LineNumber: {jsonException.LineNumber}", $"LineNumber: {lineNumber}")
                    .Replace($"BytePositionInLine: {jsonException.BytePositionInLine}", $"BytePositionInLine: {bytePositionInLine}");

                parsingErrorCollection.AddAndThrow(
                    null,
                    message,
                    string.Empty,
                    startLine1: lineNumber);
            }
            catch (SingletonParsingException singletonParsingException)
            {
                parsingErrorCollection.AddAndThrow(singletonParsingException);
            }

            valueCollection.ReportIdentifiedByteRanges(resolveByteLocator);

            return valueCollection;
        }

        /// <summary>
        /// Read the JSON-LD in <paramref name="jsonText"/> into a new <see cref="JsonLdValueCollection"/> object.
        /// </summary>
        /// <param name="jsonText">A string containing JSON text to read.</param>
        /// <param name="parseIndex">An index into an enumeration of JSON text strings passed to the parser.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <returns>A <see cref="JsonLdValueCollection"/> containing <see cref="JsonLdElement"/> objects representing the parsed JSON text.</returns>
        internal JsonLdValueCollection Read(string jsonText, int parseIndex, ParsingErrorCollection parsingErrorCollection)
        {
            if (this.parseLocator != null)
            {
                return ReadForParse(jsonText, parseIndex, this.parseLocator, parsingErrorCollection);
            }
            else
            {
                return ReadForResolve(jsonText, this.resolveLocator, parsingErrorCollection);
            }
        }
    }
}
