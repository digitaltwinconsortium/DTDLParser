namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Text.Json;

    /// <summary>
    /// A JSON-LD property in a JSON object.
    /// </summary>
    internal class JsonLdProperty
    {
        private readonly IByteLocator byteLocator;
        private readonly long byteIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLdProperty"/> class.
        /// </summary>
        /// <param name="propertyByteIndex">The byte index of the property.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="jsonReader">A <see cref="Utf8JsonReader"/> object that is in the process of reading the JSON-LD source.</param>
        /// <param name="byteLocator">An object that exports the <see cref="IByteLocator"/> interface for converting a byte index to source information.</param>
        /// <param name="jsonBytes">A byte array containing UTF8-encoded JSON-LD text.</param>
        /// <remarks>
        /// precondition: <paramref name="jsonReader"/> points to property value start (literal, open brace, or open bracket).
        /// postcondition: <paramref name="jsonReader"/> points past property value end (to next property name or close brace of enclosing object).
        /// </remarks>
        internal JsonLdProperty(long propertyByteIndex, string name, ref Utf8JsonReader jsonReader, IByteLocator byteLocator, byte[] jsonBytes)
        {
            this.byteLocator = byteLocator;

            this.byteIndex = propertyByteIndex;
            this.Name = name;
            this.Values = new JsonLdValueCollection(ref jsonReader, byteLocator, jsonBytes);
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        internal string Name { get; }

        /// <summary>
        /// Gets a <see cref="JsonLdValueCollection"/> of the values of the property.
        /// </summary>
        internal JsonLdValueCollection Values { get; }

        /// <summary>
        /// Gets the source name and line number corresponding to the property, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the property.</param>
        /// <param name="line">The line number on which the property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocation(out string name, out int line)
        {
            return this.byteLocator.TryGetSourceLocation(this.byteIndex, out name, out line);
        }

        /// <summary>
        /// Get an enumeration of <see cref="IdentifiedByteRange"/> indicating each byte range that is to be outlined and the corresponding identifier.
        /// </summary>
        /// <returns>The enumeration of <see cref="IdentifiedByteRange"/>.</returns>
        internal IEnumerable<IdentifiedByteRange> GetOutlineByteRanges()
        {
            foreach (IdentifiedByteRange byteRange in this.Values.GetOutlineByteRanges())
            {
                yield return byteRange;
            }
        }
    }
}
