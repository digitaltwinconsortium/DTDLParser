namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    /// <summary>
    /// A collection of JSON-LD values.
    /// </summary>
    internal class JsonLdValueCollection
    {
        private readonly IByteLocator byteLocator;
        private readonly byte[] jsonBytes;
        private readonly List<JsonLdValue> values;
        private readonly long startByteIndex;
        private readonly long endByteIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLdValueCollection"/> class.
        /// </summary>
        /// <param name="jsonReader">A <see cref="Utf8JsonReader"/> object that is in the process of reading the JSON-LD source.</param>
        /// <param name="byteLocator">An object that exports the <see cref="IByteLocator"/> interface for converting a byte index to source information.</param>
        /// <param name="jsonBytes">A byte array containing UTF8-encoded JSON-LD text.</param>
        /// <remarks>
        /// precondition: <paramref name="jsonReader"/> points to value start (literal, open brace, or open bracket).
        /// postcondition: <paramref name="jsonReader"/> points past value end (to subsequent token, if any).
        /// </remarks>
        internal JsonLdValueCollection(ref Utf8JsonReader jsonReader, IByteLocator byteLocator, byte[] jsonBytes)
        {
            this.byteLocator = byteLocator;
            this.jsonBytes = jsonBytes;

            this.HasPluralRepresentation = jsonReader.TokenType == JsonTokenType.StartArray;
            this.values = new List<JsonLdValue>();

            this.startByteIndex = jsonReader.TokenStartIndex;
            this.endByteIndex = -1;

            int arrayDepth = 0;
            do
            {
                if (jsonReader.TokenType == JsonTokenType.StartArray)
                {
                    ++arrayDepth;
                }
                else if (jsonReader.TokenType == JsonTokenType.EndArray)
                {
                    --arrayDepth;
                }
                else
                {
                    this.values.Add(new JsonLdValue(ref jsonReader, byteLocator, jsonBytes));
                }

                this.endByteIndex = jsonReader.TokenStartIndex;
                jsonReader.Read();
            }
            while (arrayDepth > 0);
        }

        /// <summary>
        /// Gets a value indicating whether the JSON-LD representation of the value collection uses JSON array notation.
        /// </summary>
        internal bool HasPluralRepresentation { get; }

        /// <summary>
        /// Gets the count of values in the collection.
        /// </summary>
        internal int Count
        {
            get => this.values.Count;
        }

        /// <summary>
        /// Gets an enumeration of <see cref="JsonLdValue"/> representing values in the collection.
        /// </summary>
        internal IEnumerable<JsonLdValue> Values
        {
            get => this.values;
        }

        /// <summary>
        /// Gets the raw JSON text corresponding to this value collection, whether or not it conforms to JSON-LD syntax.
        /// </summary>
        /// <returns>A string containing the raw JSON text.</returns>
        internal string GetJsonText()
        {
            if (this.startByteIndex == this.endByteIndex)
            {
                JsonLdValue singleValue = this.values.First();
                switch (singleValue.ValueType)
                {
                    case JsonLdValueType.Null:
                        return "null";
                    case JsonLdValueType.Boolean:
                        return singleValue.BooleanValue ? "true" : "false";
                    case JsonLdValueType.Number:
                        return singleValue.NumberValue.ToString();
                    case JsonLdValueType.String:
                        return $"\"{singleValue.StringValue.Replace("\"", "\\\"")}\"";
                    default:
                        return null;
                }
            }
            else
            {
                return Encoding.UTF8.GetString(this.jsonBytes, (int)this.startByteIndex, (int)this.endByteIndex - (int)this.startByteIndex + 1);
            }
        }

        /// <summary>
        /// Gets the source name and line number range corresponding to the value collection, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the value collection.</param>
        /// <param name="startLine">The line number on which the value collection begins, one-indexed.</param>
        /// <param name="endLine">The line number on which the value collection ends, one-indexed.</param>
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
        /// Report the association between an identifier and a given byte range to the given <see cref="IByteRangeRecorder"/>.
        /// </summary>
        /// <param name="byteRangeRecorder">The <see cref="IByteRangeRecorder"/> to which to report the association.</param>
        internal void ReportIdentifiedByteRanges(IByteRangeRecorder byteRangeRecorder)
        {
            foreach (JsonLdValue jsonLdValue in this.values)
            {
                jsonLdValue.ReportIdentifiedByteRange(byteRangeRecorder);
            }
        }

        /// <summary>
        /// Get an enumeration of <see cref="IdentifiedByteRange"/> indicating each byte range that is to be outlined and the corresponding identifier.
        /// </summary>
        /// <returns>The enumeration of <see cref="IdentifiedByteRange"/>.</returns>
        internal IEnumerable<IdentifiedByteRange> GetOutlineByteRanges()
        {
            foreach (JsonLdValue jsonLdValue in this.values)
            {
                foreach (IdentifiedByteRange byteRange in jsonLdValue.GetOutlineByteRanges())
                {
                    yield return byteRange;
                }
            }
        }
    }
}
