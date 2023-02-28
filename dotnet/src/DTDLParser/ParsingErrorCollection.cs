namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A collection of <c>ParsingError</c>.
    /// </summary>
    internal partial class ParsingErrorCollection
    {
        private List<ParsingError> parsingErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingErrorCollection"/> class.
        /// </summary>
        internal ParsingErrorCollection()
        {
            this.parsingErrors = new List<ParsingError>();
        }

        /// <summary>
        /// Throw a <c>ParsingException</c> if any errors are in the collection.
        /// </summary>
        internal void ThrowIfAny()
        {
            if (this.parsingErrors.Any())
            {
                throw new ParsingException(this.parsingErrors);
            }
        }

        /// <summary>
        /// Add a new parsing error to the collection and throw a <c>ParsingException</c>.
        /// </summary>
        /// <param name="validationId">A URI representing the validation condition that identifies the error.</param>
        /// <param name="causeFormat">A format string for a message that describes the cause of the error.</param>
        /// <param name="actionFormat">A format string for a message that describes an action to address the error.</param>
        /// <param name="primaryId">An optional URI that indicates the primary element related to the error.</param>
        /// <param name="secondaryId">An optional URI that indicates a secondary element related to the error.</param>
        /// <param name="property">An optional string indicating a property name that helps to pinpoint the error.</param>
        /// <param name="auxProperty">An optional string indicating an auxiliary property name that helps to pinpoint the error.</param>
        /// <param name="type">An optional string indicating a property type that helps to pinpoint the error.</param>
        /// <param name="value">An optional string indicating a property value that helps to pinpoint the error.</param>
        /// <param name="restriction">An optional string characterizing a restriction on permissible values.</param>
        /// <param name="transformation">An optional string characterizing a transformation whose application caused the error.</param>
        /// <param name="violations">A collection of strings that each indicate a validation failure, if there are any.</param>
        /// <param name="sourceName1">An optional string name of the source file in which the error is primarily in evidence.</param>
        /// <param name="startLine1">An optional integer indicating the starting line number within the source file where the error is primarily in evidence.</param>
        /// <param name="endLine1">An optional integer indicating the ending line number within the source file where the error is primarily in evidence.</param>
        /// <param name="sourceName2">An optional string name of the source file in which the error is secondarily in evidence.</param>
        /// <param name="startLine2">An optional integer indicating the starting line number within the source file where the error is secondarily in evidence.</param>
        /// <param name="endLine2">An optional integer indicating the ending line number within the source file where the error is secondarily in evidence.</param>
        /// <param name="startLine3">An optional integer indicating the starting line number within the source file where the error is tertiarily in evidence.</param>
        /// <param name="endLine3">An optional integer indicating the ending line number within the source file where the error is tertiarily in evidence.</param>
        /// <param name="layer">An optional string indicating the layer in which the error was found.</param>
        internal void AddAndThrow(
            Uri validationId,
            string causeFormat,
            string actionFormat,
            Uri primaryId = null,
            Uri secondaryId = null,
            string property = null,
            string auxProperty = null,
            string type = null,
            string value = null,
            string restriction = null,
            string transformation = null,
            IReadOnlyCollection<string> violations = null,
            string sourceName1 = null,
            int startLine1 = 0,
            int endLine1 = 0,
            string sourceName2 = null,
            int startLine2 = 0,
            int endLine2 = 0,
            int startLine3 = 0,
            int endLine3 = 0,
            string layer = null)
        {
            this.Add(validationId, causeFormat, actionFormat, primaryId, secondaryId, property, auxProperty, type, value, restriction, transformation, violations, sourceName1, startLine1, endLine1, sourceName2, startLine2, endLine2, startLine3, endLine3, layer);
            throw new ParsingException(this.parsingErrors);
        }

        /// <summary>
        /// Add a new parsing error from a <see cref="SingletonParsingException"/> to the collection and throw a <c>ParsingException</c>.
        /// </summary>
        /// <param name="singletonParsingException">The <see cref="SingletonParsingException"/> from which to extract the <see cref="ParsingError"/>.</param>
        internal void AddAndThrow(SingletonParsingException singletonParsingException)
        {
            this.AddAndThrow(
                singletonParsingException.Error.ValidationID,
                singletonParsingException.Error.Cause,
                singletonParsingException.Error.Action,
                singletonParsingException.Error.PrimaryID,
                singletonParsingException.Error.SecondaryID,
                singletonParsingException.Error.Property,
                singletonParsingException.Error.AuxProperty,
                singletonParsingException.Error.Type,
                singletonParsingException.Error.Value,
                singletonParsingException.Error.Restriction,
                singletonParsingException.Error.Transformation,
                singletonParsingException.Error.Violations,
                singletonParsingException.Error.PrimaryLocationName,
                singletonParsingException.Error.PrimaryLocationStart,
                singletonParsingException.Error.PrimaryLocationEnd,
                singletonParsingException.Error.SecondaryLocationName,
                singletonParsingException.Error.SecondaryLocationStart,
                singletonParsingException.Error.SecondaryLocationEnd,
                0,
                0,
                singletonParsingException.Error.Layer);
        }

        /// <summary>
        /// Add a new parsing error to the collection.
        /// </summary>
        /// <param name="validationId">A URI representing the validation condition that identifies the error.</param>
        /// <param name="causeFormat">A format string for a message that describes the cause of the error.</param>
        /// <param name="actionFormat">A format string for a message that describes an action to address the error.</param>
        /// <param name="primaryId">An optional URI that indicates the primary element related to the error.</param>
        /// <param name="secondaryId">An optional URI that indicates a secondary element related to the error.</param>
        /// <param name="property">An optional string indicating a property name that helps to pinpoint the error.</param>
        /// <param name="auxProperty">An optional string indicating an auxiliary property name that helps to pinpoint the error.</param>
        /// <param name="type">An optional string indicating a property type that helps to pinpoint the error.</param>
        /// <param name="value">An optional string indicating a property value that helps to pinpoint the error.</param>
        /// <param name="restriction">An optional string characterizing a restriction on permissible values.</param>
        /// <param name="transformation">An optional string characterizing a transformation whose application caused the error.</param>
        /// <param name="violations">A collection of strings that each indicate a validation failure, if there are any.</param>
        /// <param name="sourceName1">An optional string name of the source file in which the error is primarily in evidence.</param>
        /// <param name="startLine1">An optional integer indicating the starting line number within the source file where the error is primarily in evidence.</param>
        /// <param name="endLine1">An optional integer indicating the ending line number within the source file where the error is primarily in evidence.</param>
        /// <param name="sourceName2">An optional string name of the source file in which the error is secondarily in evidence.</param>
        /// <param name="startLine2">An optional integer indicating the starting line number within the source file where the error is secondarily in evidence.</param>
        /// <param name="endLine2">An optional integer indicating the ending line number within the source file where the error is secondarily in evidence.</param>
        /// <param name="startLine3">An optional integer indicating the starting line number within the source file where the error is tertiarily in evidence.</param>
        /// <param name="endLine3">An optional integer indicating the ending line number within the source file where the error is tertiarily in evidence.</param>
        /// <param name="layer">An optional string indicating the layer in which the error was found.</param>
        internal void Add(
            Uri validationId,
            string causeFormat,
            string actionFormat,
            Uri primaryId = null,
            Uri secondaryId = null,
            string property = null,
            string auxProperty = null,
            string type = null,
            string value = null,
            string restriction = null,
            string transformation = null,
            IReadOnlyCollection<string> violations = null,
            string sourceName1 = null,
            int startLine1 = 0,
            int endLine1 = 0,
            string sourceName2 = null,
            int startLine2 = 0,
            int endLine2 = 0,
            int startLine3 = 0,
            int endLine3 = 0,
            string layer = null)
        {
            ParsingError parsingError = new ParsingError() { ValidationID = validationId };

            ResultFormatter causeFormatter = new ResultFormatter(causeFormat);
            ResultFormatter actionFormatter = new ResultFormatter(actionFormat);

            if (primaryId != null)
            {
                parsingError.PrimaryID = primaryId;
                causeFormatter.Install("primaryId", primaryId.ToString());
                actionFormatter.Install("primaryId", primaryId.ToString());
            }

            if (secondaryId != null)
            {
                parsingError.SecondaryID = secondaryId;
                causeFormatter.Install("secondaryId", secondaryId.ToString());
                actionFormatter.Install("secondaryId", secondaryId.ToString());
            }

            if (property != null)
            {
                parsingError.Property = property;
                causeFormatter.Install("property", property);
                actionFormatter.Install("property", property);
            }

            if (auxProperty != null)
            {
                parsingError.AuxProperty = auxProperty;
                causeFormatter.Install("auxProperty", auxProperty);
                actionFormatter.Install("auxProperty", auxProperty);
            }

            if (type != null)
            {
                parsingError.Type = type;
                causeFormatter.Install("type", type);
                actionFormatter.Install("type", type);
            }

            if (value != null)
            {
                parsingError.Value = value;
                causeFormatter.Install("value", value);
                actionFormatter.Install("value", value);
            }

            if (restriction != null)
            {
                parsingError.Restriction = restriction;
                causeFormatter.Install("restriction", restriction);
                actionFormatter.Install("restriction", restriction);
            }

            if (transformation != null)
            {
                parsingError.Transformation = transformation;
                causeFormatter.Install("transformation", transformation);
                actionFormatter.Install("transformation", transformation);
            }

            if (violations != null)
            {
                parsingError.Violations = violations;
                causeFormatter.Install("firstViolation", violations.First());
                actionFormatter.Install("firstViolation", violations.First());
            }

            parsingError.PrimaryLocationName = sourceName1;
            string sourceNameVal1 = sourceName1 != null && sourceName1 != string.Empty ? sourceName1 : "source";
            causeFormatter.Install("sourceName1", sourceNameVal1);
            actionFormatter.Install("sourceName1", sourceNameVal1);

            parsingError.SecondaryLocationName = sourceName2;
            string sourceNameVal2 = sourceName2 != null && sourceName2 != string.Empty ? sourceName2 : "source";
            causeFormatter.Install("sourceName2", sourceNameVal2);
            actionFormatter.Install("sourceName2", sourceNameVal2);

            string sourceNamePhrase1 = sourceNameVal1 == sourceNameVal2 ? string.Empty : $" in {sourceNameVal1}";
            causeFormatter.Install("source1", sourceNamePhrase1);
            actionFormatter.Install("source1", sourceNamePhrase1);

            string sourceNamePhrase2 = sourceNameVal2 == sourceNameVal1 ? string.Empty : $" in {sourceNameVal2}";
            causeFormatter.Install("source2", sourceNamePhrase2);
            actionFormatter.Install("source2", sourceNamePhrase2);

            parsingError.PrimaryLocationStart = startLine1;
            parsingError.PrimaryLocationEnd = endLine1 > 0 ? endLine1 : startLine1;
            string linePhrase1 =
                startLine1 <= 0 ? string.Empty :
                (endLine1 <= 0 || endLine1 == startLine1) ? $" on line {startLine1}" :
                $" on lines {startLine1}-{endLine1}";
            causeFormatter.Install("line1", linePhrase1);
            actionFormatter.Install("line1", linePhrase1);

            parsingError.SecondaryLocationStart = startLine2;
            parsingError.SecondaryLocationEnd = endLine2 > 0 ? endLine2 : startLine2;
            string linePhrase2 =
                startLine2 <= 0 ? string.Empty :
                (endLine2 <= 0 || endLine2 == startLine2) ? $" on line {startLine2}" :
                $" on lines {startLine2}-{endLine2}";
            causeFormatter.Install("line2", linePhrase2);
            actionFormatter.Install("line2", linePhrase2);

            string linePhrase3 =
                startLine3 <= 0 ? string.Empty :
                (endLine3 <= 0 || endLine3 == startLine3) ? $" on line {startLine3}" :
                $" on lines {startLine3}-{endLine3}";
            causeFormatter.Install("line3", linePhrase3);
            actionFormatter.Install("line3", linePhrase3);

            parsingError.Layer = layer;
            string layerIntro = layer != null && layer != string.Empty ? $"In layer '{layer}', " : string.Empty;
            causeFormatter.Install("layer", layerIntro);
            actionFormatter.Install("layer", layerIntro);

            parsingError.Cause = causeFormatter.ToString();
            parsingError.Action = actionFormatter.ToString();

            this.parsingErrors.Add(parsingError);
        }

        private static void SetCount(
            StringBuilder stringBuilder,
            int varIndex,
            int count,
            string singularItem = "",
            string pluralItem = "",
            string singularVerb = "",
            string pluralVerb = "")
        {
            string countKey = $"{{count{varIndex}}}";
            string itemKey = $"{{item{varIndex}}}";
            string verbKey = $"{{verb{varIndex}}}";

            if (count > 1)
            {
                stringBuilder.Replace(countKey, count.ToString());
                stringBuilder.Replace(itemKey, pluralItem);
                stringBuilder.Replace(verbKey, pluralVerb);
            }
            else
            {
                stringBuilder.Replace(countKey, count == 1 ? "one" : "no");
                stringBuilder.Replace(itemKey, singularItem);
                stringBuilder.Replace(verbKey, singularVerb);
            }
        }
    }
}
