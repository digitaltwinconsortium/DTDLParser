namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    /// <summary>
    /// Provides information about a model error that was discovered during DTDL parsing.
    /// </summary>
    public class ParsingError
    {
        /// <summary>
        /// Gets the primary identifier of an element in the model relating to the error, if any.
        /// </summary>
        /// <value>The ID of a relevant element.</value>
        public Uri PrimaryID { get; internal set; }

        /// <summary>
        /// Gets the secondary identifier of an element in the model relating to the error, if any.
        /// </summary>
        /// <value>The ID of a relevant element.</value>
        public Uri SecondaryID { get; internal set; }

        /// <summary>
        /// Gets the specific property in the model relating to the error, if any.
        /// </summary>
        /// <value>The relevant property.</value>
        public string Property { get; internal set; }

        /// <summary>
        /// Gets an auxiliary property in the model relating to the error, if any.
        /// </summary>
        /// <value>The relevant auxiliary property.</value>
        public string AuxProperty { get; internal set; }

        /// <summary>
        /// Gets the type of the specific property in the model relating to the error, if any.
        /// </summary>
        /// <value>The relevant type.</value>
        public string Type { get; internal set; }

        /// <summary>
        /// Gets the value of the specific property in the model relating to the error, if any.
        /// </summary>
        /// <value>The relevant property value.</value>
        public string Value { get; internal set; }

        /// <summary>
        /// Gets a string characterizing a restriction on permissible values, if such a restriction has been violated.
        /// </summary>
        /// <value>A string description of the relevant restriction.</value>
        public string Restriction { get; internal set; }

        /// <summary>
        /// Gets a string characterizing a transformation whose application caused the error.
        /// </summary>
        /// <value>A string description of the relevant transformation.</value>
        public string Transformation { get; internal set; }

        /// <summary>
        /// Gets a collection of strings that each indicate a validation failure, if there are any.
        /// </summary>
        /// <value>A list of strings, one per validation failure.</value>
        public IReadOnlyCollection<string> Violations { get; internal set; }

        /// <summary>
        /// Gets a textual description of the cause of the error.
        /// </summary>
        /// <value>A description of the error cause.</value>
        public string Cause { get; internal set; }

        /// <summary>
        /// Gets a textual description of an action to resolve the error.
        /// </summary>
        /// <value>A description of the recommeded action.</value>
        public string Action { get; internal set; }

        /// <summary>
        /// Gets the name of the source file in which the error is primarily in evidence, or null if not determinable.
        /// </summary>
        /// <value>The source name, commonly a file name.</value>
        public string PrimaryLocationName { get; internal set; }

        /// <summary>
        /// Gets the starting line number within the source file where the error is primarily in evidence, or zero if not determinable.
        /// </summary>
        /// <value>Starting line number of the error.</value>
        public int PrimaryLocationStart { get; internal set; }

        /// <summary>
        /// Gets the ending line number within the source file where the error is primarily in evidence, or zero if not determinable.
        /// </summary>
        /// <value>Ending line number of the error.</value>
        public int PrimaryLocationEnd { get; internal set; }

        /// <summary>
        /// Gets the name of the source file in which the error is secondarily in evidence, or null if none or not determinable.
        /// </summary>
        /// <value>The source name, commonly a file name.</value>
        public string SecondaryLocationName { get; internal set; }

        /// <summary>
        /// Gets the starting line number within the source file where the error is secondarily in evidence, or zero if none or not determinable.
        /// </summary>
        /// <value>Starting line number of the error.</value>
        public int SecondaryLocationStart { get; internal set; }

        /// <summary>
        /// Gets the ending line number within the source file where the error is secondarily in evidence, or zero if none or not determinable.
        /// </summary>
        /// <value>Ending line number of the error.</value>
        public int SecondaryLocationEnd { get; internal set; }

        /// <summary>
        /// Gets the name of the layer in which the error was found, if any.
        /// </summary>
        /// <value>Name of the relevant layer.</value>
        /// <remarks>
        /// If layers are not supported, the value can be ignored, since it will always be the empty string.
        /// </remarks>
        public string Layer { get; internal set; }

        /// <summary>
        /// Gets a textual message describing the error; this is a concatenation of <c>Cause</c> and <c>Action</c>.
        /// </summary>
        public string Message
        {
            get
            {
                return this.Cause + (this.Action != null ? " " + this.Action : string.Empty);
            }
        }

        /// <summary>
        /// Gets a valididation ID that characterizes the category of error.
        /// </summary>
        /// <value>A unique identifier for the category of error.</value>
        public Uri ValidationID { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Message;
        }

        /// <summary>
        /// Write a JSON representation of the parsing error.
        /// </summary>
        /// <param name="jsonWriter">A <c>Utf8JsonWriter</c> object with which to write the JSON representation.</param>
        internal virtual void WriteToJson(Utf8JsonWriter jsonWriter)
        {
            jsonWriter.WriteStartObject();

            jsonWriter.WriteString("PrimaryID", this.PrimaryID?.ToString());
            jsonWriter.WriteString("SecondaryID", this.SecondaryID?.ToString());
            jsonWriter.WriteString("Property", this.Property);
            jsonWriter.WriteString("AuxProperty", this.AuxProperty);
            jsonWriter.WriteString("Type", this.Type);
            jsonWriter.WriteString("Value", this.Value);
            jsonWriter.WriteString("Restriction", this.Restriction);
            jsonWriter.WriteString("Transformation", this.Transformation);

            jsonWriter.WritePropertyName("Violations");
            if (this.Violations != null)
            {
                jsonWriter.WriteStartArray();

                foreach (string violation in this.Violations)
                {
                    jsonWriter.WriteStringValue(violation);
                }

                jsonWriter.WriteEndArray();
            }
            else
            {
                jsonWriter.WriteNullValue();
            }

            jsonWriter.WriteString("Cause", this.Cause);
            jsonWriter.WriteString("Action", this.Action);
            jsonWriter.WriteString("ValidationID", this.ValidationID.ToString());

            jsonWriter.WriteEndObject();
        }
    }
}
