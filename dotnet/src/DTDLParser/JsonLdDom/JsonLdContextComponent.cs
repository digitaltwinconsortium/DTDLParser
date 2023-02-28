namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Text.Json;

    /// <summary>
    /// One component of a JSON-LD context block; can be either local or remote.
    /// </summary>
    internal class JsonLdContextComponent
    {
        private readonly IByteLocator byteLocator;
        private readonly long startByteIndex;
        private readonly long endByteIndex;
        private readonly Dictionary<string, long> termByteIndices;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonLdContextComponent"/> class.
        /// </summary>
        /// <param name="jsonReader">A <see cref="Utf8JsonReader"/> object that is in the process of reading the JSON-LD source.</param>
        /// <param name="byteLocator">An object that exports the <see cref="IByteLocator"/> interface for converting a byte index to source information.</param>
        /// <remarks>
        /// precondition: <paramref name="jsonReader"/> points to value start (literal or open brace).
        /// postcondition: <paramref name="jsonReader"/> points to value end (literal or close brace).
        /// </remarks>
        internal JsonLdContextComponent(ref Utf8JsonReader jsonReader, IByteLocator byteLocator)
        {
            this.byteLocator = byteLocator;

            this.startByteIndex = jsonReader.TokenStartIndex;
            this.endByteIndex = -1;

            if (jsonReader.TokenType == JsonTokenType.String)
            {
                this.IsLocal = false;
                this.RemoteId = jsonReader.GetString();
                this.endByteIndex = jsonReader.TokenStartIndex;
                this.termByteIndices = null;
                return;
            }

            if (jsonReader.TokenType != JsonTokenType.StartObject)
            {
                this.byteLocator.TryGetSourceLocation(jsonReader.TokenStartIndex, out string sourceName, out int sourceLine);
                throw new SingletonParsingException(
                    new Dtmi("dtmi:dtdl:parsingError:invalidContext"),
                    "In {sourceName1}, @context value{line1} is not a JSON string or object.",
                    "Replace the context component{line1} with either a JSON string indicating a remote context specifier or a JSON object containing local term definitions.",
                    sourceName1: sourceName,
                    startLine1: sourceLine);
            }

            this.IsLocal = true;
            this.Terms = new Dictionary<string, string>();
            this.termByteIndices = new Dictionary<string, long>();

            jsonReader.Read();
            while (jsonReader.TokenType == JsonTokenType.PropertyName)
            {
                string term = jsonReader.GetString();

                if (this.termByteIndices.TryGetValue(term, out long extantTermByteIndex))
                {
                    if (this.byteLocator.TryGetSourceLocation(jsonReader.TokenStartIndex, out string sourceName, out int sourceLine1) &&
                        this.byteLocator.TryGetSourceLocation(extantTermByteIndex, out _, out int sourceLine2))
                    {
                        throw new SingletonParsingException(
                            new Dtmi("dtmi:dtdl:parsingError:duplicateTermInContextObject"),
                            $"In {sourceName}, context defines term '{term}' on line {sourceLine1}, but '{term}' is already defined on line {sourceLine2}.",
                            $"Modify the context so that it contains only one definition for '{term}'.",
                            sourceName1: sourceName,
                            startLine1: sourceLine1,
                            startLine2: sourceLine2);
                    }
                    else
                    {
                        throw new SingletonParsingException(
                            new Dtmi("dtmi:dtdl:parsingError:duplicateTermInContextObject"),
                            $"Context includes multiple definitions for term '{term}'.",
                            $"Modify the context so that it contains only one definition for '{term}'.");
                    }
                }

                this.termByteIndices[term] = jsonReader.TokenStartIndex;

                jsonReader.Read();
                string definition;
                if (jsonReader.TokenType == JsonTokenType.Null)
                {
                    definition = null;
                }
                else if (jsonReader.TokenType == JsonTokenType.String)
                {
                    definition = jsonReader.GetString();
                }
                else if (jsonReader.TokenType == JsonTokenType.StartObject)
                {
                    definition = this.GetStringValueFromIdProperty(term, ref jsonReader, byteLocator);
                }
                else
                {
                    this.byteLocator.TryGetSourceLocation(jsonReader.TokenStartIndex, out string sourceName, out int sourceLine);
                    throw new SingletonParsingException(
                        new Dtmi("dtmi:dtdl:parsingError:termDefinitionNotStringOrObject"),
                        $"In {{sourceName1}}, context definition{{line1}} for term '{term}' is not a JSON string or object.",
                        $"Remove the term definition for '{term}' or replace the value with a URI string or a JSON object with an '@id' property whose value is a JSON string representing a valid URI or URI prefix.",
                        sourceName1: sourceName,
                        startLine1: sourceLine);
                }

                this.Terms[term] = definition;

                jsonReader.Read();
            }

            this.endByteIndex = jsonReader.TokenStartIndex;
        }

        /// <summary>
        /// Gets a value indicating whether the context component is local (a set of term definitions) as opposed to remote (a context identfiier).
        /// </summary>
        internal bool IsLocal { get; }

        /// <summary>
        /// Gets a context identifier, if the context component is remote.
        /// </summary>
        internal string RemoteId { get; }

        /// <summary>
        /// Gets a dictionary of terms and definitions, if the context component is local.
        /// </summary>
        internal Dictionary<string, string> Terms { get; }

        /// <summary>
        /// Gets the source name and line number range corresponding to the context component, if possible.
        /// </summary>
        /// <param name="name">The name of the JSON text containing the context component.</param>
        /// <param name="startLine">The line number on which the context component begins, one-indexed.</param>
        /// <param name="endLine">The line number on which the context component ends, one-indexed.</param>
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
        /// Gets the source name and line number corresponding a given term defined in a context block, if possible.
        /// </summary>
        /// <param name="term">The term whose source location is to be determined.</param>
        /// <param name="name">The name of the JSON text containing the element.</param>
        /// <param name="line">The line number on which the '@context' property is defined, one-indexed.</param>
        /// <returns>True if the source information could be obtained.</returns>
        internal bool TryGetSourceLocationForTerm(string term, out string name, out int line)
        {
            if (this.termByteIndices != null && this.termByteIndices.TryGetValue(term, out long termByteIndex))
            {
                return this.byteLocator.TryGetSourceLocation(termByteIndex, out name, out line);
            }
            else
            {
                name = null;
                line = 0;
                return false;
            }
        }

        // precondition: jsonReader points to open brace
        // postcondition: jsonReader points to close brace
        private string GetStringValueFromIdProperty(string term, ref Utf8JsonReader jsonReader, IByteLocator byteLocator)
        {
            long objectStartByteIndex = jsonReader.TokenStartIndex;
            string definition = null;

            jsonReader.Read();
            while (jsonReader.TokenType != JsonTokenType.EndObject)
            {
                if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.GetString() == "@id")
                {
                    jsonReader.Read();
                    if (jsonReader.TokenType != JsonTokenType.String)
                    {
                        this.byteLocator.TryGetSourceLocation(jsonReader.TokenStartIndex, out string sourceName, out int sourceLine);
                        throw new SingletonParsingException(
                            new Dtmi("dtmi:dtdl:parsingError:termDefinitionIdPropertyNotString"),
                            $"In {{sourceName1}}, context definition for term '{term}' is a JSON object whose '@id' property value{{line1}} is not a string.",
                            $"Remove the term definition for '{term}' or replace the '@id' property with a JSON string representing a valid URI or URI prefix.",
                            sourceName1: sourceName,
                            startLine1: sourceLine);
                    }

                    definition = jsonReader.GetString();
                }
                else
                {
                    jsonReader.Skip();
                }

                jsonReader.Read();
            }

            if (definition == null)
            {
                this.byteLocator.TryGetSourceLocation(objectStartByteIndex, out string sourceName, out int sourceLine);
                throw new SingletonParsingException(
                    new Dtmi("dtmi:dtdl:parsingError:termDefinitionMissingIdProperty"),
                    $"In {{sourceName1}}, context definition for term '{term}'{{line1}} is a JSON object with no '@id' property.",
                    $"Add an '@id' property whose value is a URI string to the object that defines term '{term}'.",
                    sourceName1: sourceName,
                    startLine1: sourceLine);
            }

            return definition;
        }
    }
}
