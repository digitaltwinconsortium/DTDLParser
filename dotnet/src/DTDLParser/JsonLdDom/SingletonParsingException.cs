namespace DTDLParser
{
    using System;

    /// <summary>
    /// Indicates that an error was discovered during DTDL parsing.
    /// </summary>
    internal class SingletonParsingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonParsingException"/> class.
        /// </summary>
        /// <param name="error">The error that was discovered.</param>
        internal SingletonParsingException(ParsingError error)
            : base(error.Message)
        {
            this.Error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonParsingException"/> class.
        /// </summary>
        /// <param name="validationId">A URI representing the validation condition that identifies the error.</param>
        /// <param name="cause">A message that describes the cause of the error.</param>
        /// <param name="action">A message that describes an action to address the error.</param>
        /// <param name="primaryId">An optional URI that indicates the primary element related to the error.</param>
        /// <param name="secondaryId">An optional URI that indicates a secondary element related to the error.</param>
        /// <param name="property">An optional string indicating a property name that helps to pinpoint the error.</param>
        /// <param name="auxProperty">An optional string indicating an auxiliary property name that helps to pinpoint the error.</param>
        /// <param name="type">An optional string indicating a property type that helps to pinpoint the error.</param>
        /// <param name="value">An optional string indicating a property value that helps to pinpoint the error.</param>
        /// <param name="restriction">An optional string characterizing the permissible range of values.</param>
        /// <param name="transformation">An optional string characterizing a transformation whose application caused the error.</param>
        /// <param name="sourceName1">An optional string name of the source file in which the error is primarily in evidence.</param>
        /// <param name="startLine1">An optional integer indicating the starting line number within the source file where the error is primarily in evidence.</param>
        /// <param name="endLine1">An optional integer indicating the ending line number within the source file where the error is primarily in evidence.</param>
        /// <param name="sourceName2">An optional string name of the source file in which the error is secondarily in evidence.</param>
        /// <param name="startLine2">An optional integer indicating the starting line number within the source file where the error is secondarily in evidence.</param>
        /// <param name="endLine2">An optional integer indicating the ending line number within the source file where the error is secondarily in evidence.</param>
        internal SingletonParsingException(
            Uri validationId,
            string cause,
            string action,
            Uri primaryId = null,
            Uri secondaryId = null,
            string property = null,
            string auxProperty = null,
            string type = null,
            string value = null,
            string restriction = null,
            string transformation = null,
            string sourceName1 = null,
            int startLine1 = 0,
            int endLine1 = 0,
            string sourceName2 = null,
            int startLine2 = 0,
            int endLine2 = 0)
        {
            this.Error = new ParsingError()
            {
                ValidationID = validationId,
                Cause = cause,
                Action = action,
                PrimaryID = primaryId,
                SecondaryID = secondaryId,
                Property = property,
                AuxProperty = auxProperty,
                Type = type,
                Value = value,
                Restriction = restriction,
                Transformation = transformation,
                PrimaryLocationName = sourceName1,
                PrimaryLocationStart = startLine1,
                PrimaryLocationEnd = endLine1,
                SecondaryLocationName = sourceName2,
                SecondaryLocationStart = startLine2,
                SecondaryLocationEnd = endLine2,
            };
        }

        /// <summary>
        /// Gets the error that was discovered during DTDL parsing.
        /// </summary>
        internal ParsingError Error { get; }
    }
}
