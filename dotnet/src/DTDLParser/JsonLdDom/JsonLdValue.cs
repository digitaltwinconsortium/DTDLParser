namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Text.Json;

    /// <summary>
    /// A JSON-LD value.
    /// </summary>
    internal class JsonLdValue
    {
        private readonly IByteLocator byteLocator;
        private readonly long startByteIndex;
        private readonly long endByteIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLdValue"/> class.
        /// </summary>
        /// <param name="jsonReader">A <see cref="Utf8JsonReader"/> object that is in the process of reading the JSON-LD source.</param>
        /// <param name="byteLocator">An object that exports the <see cref="IByteLocator"/> interface for converting a byte index to source information.</param>
        /// <param name="jsonBytes">A byte array containing UTF8-encoded JSON-LD text.</param>
        /// <remarks>
        /// precondition: <paramref name="jsonReader"/> points to value start (literal or open brace).
        /// postcondition: <paramref name="jsonReader"/> points to value end (literal or close brace).
        /// </remarks>
        internal JsonLdValue(ref Utf8JsonReader jsonReader, IByteLocator byteLocator, byte[] jsonBytes)
        {
            this.byteLocator = byteLocator;

            this.startByteIndex = jsonReader.TokenStartIndex;
            this.endByteIndex = -1;

            switch (jsonReader.TokenType)
            {
                case JsonTokenType.Null:
                    this.ValueType = JsonLdValueType.Null;
                    this.LiteralValue = null;
                    break;
                case JsonTokenType.True:
                    this.ValueType = JsonLdValueType.Boolean;
                    this.BooleanValue = true;
                    this.LiteralValue = this.BooleanValue;
                    break;
                case JsonTokenType.False:
                    this.ValueType = JsonLdValueType.Boolean;
                    this.BooleanValue = false;
                    this.LiteralValue = this.BooleanValue;
                    break;
                case JsonTokenType.Number:
                    this.ValueType = JsonLdValueType.Number;
                    this.NumberValue = jsonReader.GetDouble();
                    this.LiteralValue = this.NumberValue;
                    break;
                case JsonTokenType.String:
                    this.ValueType = JsonLdValueType.String;
                    this.StringValue = jsonReader.GetString();
                    this.LiteralValue = this.StringValue;
                    break;
                case JsonTokenType.StartObject:
                    this.ValueType = JsonLdValueType.Element;
                    this.ElementValue = new JsonLdElement(ref jsonReader, byteLocator, jsonBytes);
                    this.LiteralValue = null;
                    break;
            }

            this.endByteIndex = jsonReader.TokenStartIndex;
        }

        /// <summary>
        /// Gets the <see cref="JsonLdValueType"/> of the value.
        /// </summary>
        internal JsonLdValueType ValueType { get; }

        /// <summary>
        /// Gets the JSON value if it is a boolean, number, or string.
        /// </summary>
        internal object LiteralValue { get; }

        /// <summary>
        /// Gets a value indicating whether the JSON value is true or false, if it is a boolean.
        /// </summary>
        internal bool BooleanValue { get; }

        /// <summary>
        /// Gets the JSON value if it is a number.
        /// </summary>
        internal double NumberValue { get; }

        /// <summary>
        /// Gets the JSON value if it is a string.
        /// </summary>
        internal string StringValue { get; }

        /// <summary>
        /// Gets the JSON value if it is an element, represented by a JSON object.
        /// </summary>
        internal JsonLdElement ElementValue { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            switch (this.ValueType)
            {
                case JsonLdValueType.Null:
                    return "null";
                case JsonLdValueType.Boolean:
                    return this.BooleanValue ? "true" : "false";
                case JsonLdValueType.Number:
                    return this.NumberValue.ToString();
                case JsonLdValueType.String:
                    return $"\"{this.StringValue}\"";
                case JsonLdValueType.Element:
                    return this.ElementValue.Id != null ? $"'{this.ElementValue.Id}'" : string.Empty;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the source name and line number range corresponding to the value, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the value.</param>
        /// <param name="startLine">The line number on which the value begins, one-indexed.</param>
        /// <param name="endLine">The line number on which the value ends, one-indexed.</param>
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
        internal void ReportIdentifiedByteRange(IByteRangeRecorder byteRangeRecorder)
        {
            if (this.ValueType == JsonLdValueType.Element)
            {
                this.ElementValue.ReportIdentifiedByteRange(byteRangeRecorder);
            }
        }

        /// <summary>
        /// Get an enumeration of <see cref="IdentifiedByteRange"/> indicating each byte range that is to be outlined and the corresponding identifier.
        /// </summary>
        /// <returns>The enumeration of <see cref="IdentifiedByteRange"/>.</returns>
        internal IEnumerable<IdentifiedByteRange> GetOutlineByteRanges()
        {
            if (this.ValueType == JsonLdValueType.Element)
            {
                foreach (IdentifiedByteRange byteRange in this.ElementValue.GetOutlineByteRanges())
                {
                    yield return byteRange;
                }
            }
        }
    }
}
