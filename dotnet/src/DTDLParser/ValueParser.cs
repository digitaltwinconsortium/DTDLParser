namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class <c>ValueParser</c> parses JSON-LD property values.
    /// </summary>
    internal static class ValueParser
    {
        private static readonly Regex LanguageCodeRegex = new Regex(@"^[a-z]{2,4}(-[A-Z][a-z]{3})?(-([A-Z]{2}|[0-9]{3}))?$", RegexOptions.Compiled);

        /// <summary>
        /// Parse a singular string value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the string property to parse.</param>
        /// <param name="propertyName">The name of the string property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="maxLength">The maximum permissible length of a string, or null if no maximum.</param>
        /// <param name="pattern">A regex that constrains the permissible values, or null if no pattern constraint.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="isOptional">true if the property is optional.</param>
        /// <returns>The string value extracted.</returns>
        internal static string ParseSingularStringValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection, bool isOptional)
        {
            if (valueCollection.Count > 1)
            {
                parsingErrorCollection?.Notify(
                    "stringMultipleValues",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer);
                return null;
            }

            return ParsePluralStringValueCollection(aggregateContext, elementId, propertyName, valueCollection, maxLength, pattern, layer, parsingErrorCollection, minCount: isOptional ? 0 : 1).FirstOrDefault();
        }

        /// <summary>
        /// Parse a plural string value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the string property to parse.</param>
        /// <param name="propertyName">The name of the string property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="maxLength">The maximum permissible length of an string string, or null if no maximum.</param>
        /// <param name="pattern">A regex that constrains the permissible values, or null if no pattern constraint.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="minCount">The minimum count of string values required.</param>
        /// <returns>The string values extracted.</returns>
        internal static List<string> ParsePluralStringValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection, int minCount)
        {
            List<string> strings = new List<string>();

            if (valueCollection.Count < minCount)
            {
                parsingErrorCollection?.Notify(
                    "stringCountBelowMin",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer,
                    observedCount: valueCollection.Count,
                    expectedCount: minCount);
            }

            foreach (JsonLdValue value in valueCollection.Values)
            {
                if (TryParseStringValue(aggregateContext, elementId, propertyName, value, maxLength, pattern, layer, parsingErrorCollection, out string stringValue, "string", requireString: true))
                {
                    strings.Add(stringValue);
                }
            }

            return strings;
        }

        /// <summary>
        /// Parse a singular integer value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the integer property to parse.</param>
        /// <param name="propertyName">The name of the integer property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="minInclusive">The minimum permissible value, or null if no minimim.</param>
        /// <param name="maxInclusive">The maximum permissible value, or null if no maximum.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="isOptional">true if the property is optional.</param>
        /// <returns>The integer value extracted.</returns>
        internal static int? ParseSingularIntegerValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, int? minInclusive, int? maxInclusive, string layer, ParsingErrorCollection parsingErrorCollection, bool isOptional)
        {
            if (valueCollection.Count > 1)
            {
                parsingErrorCollection?.Notify(
                    "integerMultipleValues",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer);

                return null;
            }

            List<int> ints = ParsePluralIntegerValueCollection(aggregateContext, elementId, propertyName, valueCollection, minInclusive, maxInclusive, layer, parsingErrorCollection, minCount: isOptional ? 0 : 1);
            return ints.Count > 0 ? (int?)ints.First() : null;
        }

        /// <summary>
        /// Parse a plural integer value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the integer property to parse.</param>
        /// <param name="propertyName">The name of the integer property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="minInclusive">The minimum permissible value, or null if no minimim.</param>
        /// <param name="maxInclusive">The maximum permissible value, or null if no maximum.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="minCount">The minimum count of integer values required.</param>
        /// <returns>The integer values extracted.</returns>
        internal static List<int> ParsePluralIntegerValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, int? minInclusive, int? maxInclusive, string layer, ParsingErrorCollection parsingErrorCollection, int minCount)
        {
            List<int> ints = new List<int>();

            if (valueCollection.Count < minCount)
            {
                parsingErrorCollection?.Notify(
                    "integerCountBelowMin",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer,
                    observedCount: valueCollection.Count,
                    expectedCount: minCount);
            }

            foreach (JsonLdValue value in valueCollection.Values)
            {
                if (TryParseIntegerValue(aggregateContext, elementId, propertyName, value, minInclusive, maxInclusive, layer, parsingErrorCollection, out int? intValue, "integer", requireInteger: true))
                {
                    ints.Add((int)intValue);
                }
            }

            return ints;
        }

        /// <summary>
        /// Parse a singular numeric value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the numeric property to parse.</param>
        /// <param name="propertyName">The name of the numeric property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="minInclusive">The minimum permissible value, or null if no minimim.</param>
        /// <param name="maxInclusive">The maximum permissible value, or null if no maximum.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="isOptional">true if the property is optional.</param>
        /// <returns>The numeric value extracted.</returns>
        internal static double? ParseSingularNumericValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, double? minInclusive, double? maxInclusive, string layer, ParsingErrorCollection parsingErrorCollection, bool isOptional)
        {
            if (valueCollection.Count > 1)
            {
                parsingErrorCollection?.Notify(
                    "numericMultipleValues",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer);

                return null;
            }

            List<double> numbers = ParsePluralNumericValueCollection(aggregateContext, elementId, propertyName, valueCollection, minInclusive, maxInclusive, layer, parsingErrorCollection, minCount: isOptional ? 0 : 1);
            return numbers.Count > 0 ? (double?)numbers.First() : null;
        }

        /// <summary>
        /// Parse a plural numeric value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the numeric property to parse.</param>
        /// <param name="propertyName">The name of the numeric property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="minInclusive">The minimum permissible value, or null if no minimim.</param>
        /// <param name="maxInclusive">The maximum permissible value, or null if no maximum.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="minCount">The minimum count of numeric values required.</param>
        /// <returns>The numeric values extracted.</returns>
        internal static List<double> ParsePluralNumericValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, double? minInclusive, double? maxInclusive, string layer, ParsingErrorCollection parsingErrorCollection, int minCount)
        {
            List<double> numbers = new List<double>();

            if (valueCollection.Count < minCount)
            {
                parsingErrorCollection?.Notify(
                    "numericCountBelowMin",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer,
                    observedCount: valueCollection.Count,
                    expectedCount: minCount);
            }

            foreach (JsonLdValue value in valueCollection.Values)
            {
                if (TryParseNumericValue(aggregateContext, elementId, propertyName, value, minInclusive, maxInclusive, layer, parsingErrorCollection, out double? numberValue, "numeric", requireNumeric: true))
                {
                    numbers.Add((double)numberValue);
                }
            }

            return numbers;
        }

        /// <summary>
        /// Parse a singular boolean value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the boolean property to parse.</param>
        /// <param name="propertyName">The name of the boolean property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="isOptional">true if the property is optional.</param>
        /// <returns>The boolean value extracted.</returns>
        internal static bool ParseSingularBooleanValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, string layer, ParsingErrorCollection parsingErrorCollection, bool isOptional)
        {
            if (valueCollection.Count > 1)
            {
                parsingErrorCollection?.Notify(
                    "booleanMultipleValues",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer);

                return false;
            }

            List<bool> bools = ParsePluralBooleanValueCollection(elementId, propertyName, valueCollection, layer, parsingErrorCollection, minCount: isOptional ? 0 : 1, aggregateContext: aggregateContext);
            return bools.Count > 0 ? bools.First() : false;
        }

        /// <summary>
        /// Parse a plural boolean value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="elementId">The DTMI of the element that holds the boolean property to parse.</param>
        /// <param name="propertyName">The name of the boolean property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="minCount">The minimum count of boolean values required.</param>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <returns>The boolean values extracted.</returns>
        internal static List<bool> ParsePluralBooleanValueCollection(Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, string layer, ParsingErrorCollection parsingErrorCollection, int minCount, AggregateContext aggregateContext)
        {
            List<bool> bools = new List<bool>();

            if (valueCollection.Count < minCount)
            {
                parsingErrorCollection?.Notify(
                    "booleanCountBelowMin",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer,
                    observedCount: valueCollection.Count,
                    expectedCount: minCount);
            }

            foreach (JsonLdValue value in valueCollection.Values)
            {
                if (TryParseBooleanValue(aggregateContext, elementId, propertyName, value, layer, parsingErrorCollection, out bool boolValue, "boolean", requireBoolean: true))
                {
                    bools.Add(boolValue);
                }
            }

            return bools;
        }

        /// <summary>
        /// Parse a singular string, integer, or boolean value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elementId">The DTMI of the element that holds the literal property to parse.</param>
        /// <param name="propertyName">The name of the literal property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="isOptional">true if the property is optional.</param>
        /// <param name="typeFragment">The fragment portion of the XSD URI indicating the type of literal.</param>
        /// <returns>The literal value extracted.</returns>
        internal static object ParseSingularLiteralValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, string layer, ParsingErrorCollection parsingErrorCollection, bool isOptional, out string typeFragment)
        {
            if (valueCollection.Count == 0)
            {
                parsingErrorCollection?.Notify(
                    "literalCountBelowMin",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer,
                    observedCount: valueCollection.Count,
                    expectedCount: 1);

                typeFragment = null;
                return null;
            }

            if (valueCollection.Count > 1)
            {
                parsingErrorCollection?.Notify(
                    "literalMultipleValues",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer);

                typeFragment = null;
                return null;
            }

            bool isElement = valueCollection.Values.First().ValueType == JsonLdValueType.Element;

            if ((isElement || valueCollection.Values.First().ValueType == JsonLdValueType.String) &&
                TryParseStringValue(aggregateContext, elementId, propertyName, valueCollection.Values.First(), null, null, layer, parsingErrorCollection, out string stringValue, "literal", requireString: false))
            {
                typeFragment = "#string";
                return stringValue;
            }

            if (isElement || valueCollection.Values.First().ValueType == JsonLdValueType.Number)
            {
                if (TryParseIntegerValue(aggregateContext, elementId, propertyName, valueCollection.Values.First(), null, null, layer, parsingErrorCollection, out int? intValue, "literal", requireInteger: false))
                {
                    typeFragment = "#integer";
                    return (int)intValue;
                }
                else if (TryParseNumericValue(aggregateContext, elementId, propertyName, valueCollection.Values.First(), null, null, layer, parsingErrorCollection, out double? numberValue, "literal", requireNumeric: false))
                {
                    typeFragment = "#decimal";
                    return (double)numberValue;
                }
            }

            if ((isElement || valueCollection.Values.First().ValueType == JsonLdValueType.Boolean) &&
                TryParseBooleanValue(aggregateContext, elementId, propertyName, valueCollection.Values.First(), layer, parsingErrorCollection, out bool boolValue, "literal", requireBoolean: false))
            {
                typeFragment = "#boolean";
                return boolValue;
            }

            parsingErrorCollection?.Notify(
                "literalNotValid",
                elementId: elementId,
                propertyName: propertyName,
                incidentValues: valueCollection,
                layer: layer);

            typeFragment = null;
            return null;
        }

        /// <summary>
        /// Parse a singular identifier value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="elementId">The DTMI of the element that holds the identifier property to parse.</param>
        /// <param name="propertyName">The name of the identifier property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="maxLength">The maximum permissible length of an identifier string, or null if no maximum.</param>
        /// <param name="pattern">A regex that constrains the permissible values, or null if no pattern constraint.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="isOptional">True if the property is optional.</param>
        /// <param name="requireDtmi">True if the value of the property must be a DTMI.</param>
        /// <returns>The identifier value extracted.</returns>
        internal static Uri ParseSingularIdentifierValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection, bool isOptional, bool requireDtmi)
        {
            if (valueCollection.Count > 1)
            {
                parsingErrorCollection?.Notify(
                    "identifierMultipleValues",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValues: valueCollection,
                    layer: layer);

                return null;
            }

            return ParsePluralIdentifierValueCollection(aggregateContext, elementId, propertyName, valueCollection, maxLength, pattern, layer, parsingErrorCollection, minCount: isOptional ? 0 : 1, requireDtmi: requireDtmi).FirstOrDefault();
        }

        /// <summary>
        /// Parse a plural identifier value from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="elementId">The DTMI of the element that holds the identifier property to parse.</param>
        /// <param name="propertyName">The name of the identifier property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="maxLength">The maximum permissible length of an identifier string, or null if no maximum.</param>
        /// <param name="pattern">A regex that constrains the permissible values, or null if no pattern constraint.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <param name="minCount">The minimum count of identifier values required.</param>
        /// <param name="requireDtmi">True if property values must be a DTMI.</param>
        /// <returns>The identifier values extracted.</returns>
        internal static List<Uri> ParsePluralIdentifierValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection, int minCount, bool requireDtmi)
        {
            List<Uri> uris = new List<Uri>();

            if (valueCollection.Count < minCount)
            {
                parsingErrorCollection?.Notify(
                    "identifierCountBelowMin",
                    elementId: elementId,
                    propertyName: propertyName,
                    typeRestriction: requireDtmi ? "DTMI" : "URI",
                    incidentValues: valueCollection,
                    layer: layer,
                    observedCount: valueCollection.Count,
                    expectedCount: minCount);
            }

            foreach (JsonLdValue value in valueCollection.Values)
            {
                if (TryParseIdentifierValue(elementId, aggregateContext, propertyName, value, maxLength, pattern, layer, parsingErrorCollection, requireDtmi, out Uri uri))
                {
                    uris.Add(uri);
                }
            }

            return uris;
        }

        /// <summary>
        /// Parse language string values from a <see cref="JsonLdValueCollection"/>.
        /// </summary>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="elementId">The DTMI of the element that holds the language string property to parse.</param>
        /// <param name="propertyName">The name of the language string property to parse.</param>
        /// <param name="valueCollection">The <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="defaultLang">The default language.</param>
        /// <param name="maxLength">The maximum permissible length of a string, or null if no maximum.</param>
        /// <param name="pattern">A regex that constrains the permissible values, or null if no pattern constraint.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <returns>A dictionary that maps language codes to strings.</returns>
        internal static Dictionary<string, string> ParseLangStringValueCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, string defaultLang, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection)
        {
            if (!valueCollection.HasPluralRepresentation && valueCollection.Values.First().ValueType == JsonLdValueType.Element)
            {
                return GetDictionaryFromLanguageMap(elementId, propertyName, valueCollection.Values.First().ElementValue, maxLength, pattern, layer, parsingErrorCollection);
            }
            else
            {
                return GetDictionaryFromLanguageTaggedStringCollection(aggregateContext, elementId, propertyName, valueCollection, defaultLang, maxLength, pattern, layer, parsingErrorCollection);
            }
        }

        private static bool TryParseStringValue(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValue value, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection, out string stringValue, string literalType, bool requireString)
        {
            stringValue = null;

            if (value.ValueType == JsonLdValueType.String)
            {
                stringValue = value.StringValue;
            }
            else if (value.ValueType == JsonLdValueType.Element)
            {
                CheckForInvalidPropertiesInValueObject(aggregateContext, value.ElementValue, elementId, propertyName, layer, parsingErrorCollection);

                if (value.ElementValue.Value == null)
                {
                    parsingErrorCollection?.Notify(
                        $"{literalType}ObjectNoValue",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);

                    return false;
                }

                bool typeDeclaredString = false;
                bool typeDeclaredNonString = false;
                if (value.ElementValue.Types != null)
                {
                    if (value.ElementValue.Types.Count != 1)
                    {
                        parsingErrorCollection?.Notify(
                            "literalTypeNotSingular",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                        return false;
                    }

                    typeDeclaredString = value.ElementValue.Types.Contains("xsd:string") || value.ElementValue.Types.Contains("http://www.w3.org/2001/XMLSchema#string");
                    typeDeclaredNonString = !typeDeclaredString;
                }

                if (typeDeclaredNonString)
                {
                    if (requireString)
                    {
                        parsingErrorCollection?.Notify(
                            "stringTypeNotString",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                if (value.ElementValue.Value.ValueType != JsonLdValueType.String)
                {
                    if (requireString || typeDeclaredString)
                    {
                        parsingErrorCollection?.Notify(
                            "stringValueNotString",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                stringValue = value.ElementValue.Value.StringValue;
            }
            else
            {
                if (requireString)
                {
                    parsingErrorCollection?.Notify(
                        "stringNotString",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);
                }

                return false;
            }

            if (maxLength != null && stringValue.Length > maxLength)
            {
                parsingErrorCollection?.Notify(
                    "stringTooLong",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValue: value,
                    propertyValue: stringValue,
                    layer: layer,
                    expectedCount: maxLength);
            }

            if (pattern != null && !pattern.IsMatch(stringValue))
            {
                parsingErrorCollection?.Notify(
                    "stringInvalid",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValue: value,
                    propertyValue: stringValue,
                    layer: layer,
                    pattern: pattern.ToString());
            }

            return true;
        }

        private static bool TryParseIntegerValue(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValue value, int? minInclusive, int? maxInclusive, string layer, ParsingErrorCollection parsingErrorCollection, out int? intValue, string literalType, bool requireInteger)
        {
            intValue = null;

            if (value.ValueType == JsonLdValueType.Number && value.NumberValue == (int)value.NumberValue)
            {
                intValue = (int)value.NumberValue;
            }
            else if (value.ValueType == JsonLdValueType.Element)
            {
                CheckForInvalidPropertiesInValueObject(aggregateContext, value.ElementValue, elementId, propertyName, layer, parsingErrorCollection);

                if (value.ElementValue.Value == null)
                {
                    parsingErrorCollection?.Notify(
                        $"{literalType}ObjectNoValue",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);

                    return false;
                }

                bool typeDeclaredInteger = false;
                bool typeDeclaredNonInteger = false;
                if (value.ElementValue.Types != null)
                {
                    if (value.ElementValue.Types.Count != 1)
                    {
                        parsingErrorCollection?.Notify(
                            "literalTypeNotSingular",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                        return false;
                    }

                    typeDeclaredInteger = value.ElementValue.Types.Contains("xsd:integer") || value.ElementValue.Types.Contains("http://www.w3.org/2001/XMLSchema#integer");
                    typeDeclaredNonInteger = !typeDeclaredInteger;
                }

                if (typeDeclaredNonInteger)
                {
                    if (requireInteger)
                    {
                        parsingErrorCollection?.Notify(
                            "integerTypeNotInteger",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                if (value.ElementValue.Value.ValueType != JsonLdValueType.Number || value.ElementValue.Value.NumberValue != (int)value.ElementValue.Value.NumberValue)
                {
                    if (requireInteger || typeDeclaredInteger)
                    {
                        parsingErrorCollection?.Notify(
                            "integerValueNotInteger",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                intValue = (int)value.ElementValue.Value.NumberValue;
            }
            else
            {
                if (requireInteger)
                {
                    parsingErrorCollection?.Notify(
                        "integerNotInteger",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);
                }

                return false;
            }

            if (minInclusive != null && maxInclusive != null && minInclusive == maxInclusive && intValue != minInclusive)
            {
                parsingErrorCollection?.Notify(
                    "valueNotExact",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValue: value,
                    propertyValue: intValue.ToString(),
                    layer: layer,
                    limit: minInclusive.ToString());
            }
            else if (minInclusive != null && intValue < minInclusive)
            {
                if (maxInclusive != null)
                {
                    parsingErrorCollection?.Notify(
                        "valueBelowRange",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: intValue.ToString(),
                        layer: layer,
                        range: $"[{minInclusive}, {maxInclusive}]");
                }
                else
                {
                    parsingErrorCollection?.Notify(
                        "valueBelowMin",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: intValue.ToString(),
                        layer: layer,
                        limit: minInclusive.ToString());
                }
            }
            else if (maxInclusive != null && intValue > maxInclusive)
            {
                if (minInclusive != null)
                {
                    parsingErrorCollection?.Notify(
                        "valueAboveRange",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: intValue.ToString(),
                        layer: layer,
                        range: $"[{minInclusive}, {maxInclusive}]");
                }
                else
                {
                    parsingErrorCollection?.Notify(
                        "valueAboveMax",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: intValue.ToString(),
                        layer: layer,
                        limit: maxInclusive.ToString());
                }
            }

            return true;
        }

        private static bool TryParseNumericValue(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValue value, double? minInclusive, double? maxInclusive, string layer, ParsingErrorCollection parsingErrorCollection, out double? numberValue, string literalType, bool requireNumeric)
        {
            numberValue = null;

            if (value.ValueType == JsonLdValueType.Number)
            {
                numberValue = value.NumberValue;
            }
            else if (value.ValueType == JsonLdValueType.Element)
            {
                CheckForInvalidPropertiesInValueObject(aggregateContext, value.ElementValue, elementId, propertyName, layer, parsingErrorCollection);

                if (value.ElementValue.Value == null)
                {
                    parsingErrorCollection?.Notify(
                        $"{literalType}ObjectNoValue",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);

                    return false;
                }

                bool typeDeclaredNumeric = false;
                bool typeDeclaredNonNumeric = false;
                if (value.ElementValue.Types != null)
                {
                    if (value.ElementValue.Types.Count != 1)
                    {
                        parsingErrorCollection?.Notify(
                            "literalTypeNotSingular",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                        return false;
                    }

                    typeDeclaredNumeric = value.ElementValue.Types.Contains("xsd:decimal") || value.ElementValue.Types.Contains("http://www.w3.org/2001/XMLSchema#decimal");
                    typeDeclaredNonNumeric = !typeDeclaredNumeric;
                }

                if (typeDeclaredNonNumeric)
                {
                    if (requireNumeric)
                    {
                        parsingErrorCollection?.Notify(
                            "numericTypeNotNumeric",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                if (value.ElementValue.Value.ValueType != JsonLdValueType.Number)
                {
                    if (requireNumeric || typeDeclaredNumeric)
                    {
                        parsingErrorCollection?.Notify(
                            "numericValueNotNumeric",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                numberValue = value.ElementValue.Value.NumberValue;
            }
            else
            {
                if (requireNumeric)
                {
                    parsingErrorCollection?.Notify(
                        "numericNotNumeric",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);
                }

                return false;
            }

            if (minInclusive != null && maxInclusive != null && minInclusive == maxInclusive && numberValue != minInclusive)
            {
                parsingErrorCollection?.Notify(
                    "valueNotExact",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValue: value,
                    propertyValue: numberValue.ToString(),
                    layer: layer,
                    limit: minInclusive.ToString());
            }
            else if (minInclusive != null && numberValue < minInclusive)
            {
                if (maxInclusive != null)
                {
                    parsingErrorCollection?.Notify(
                        "valueBelowRange",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: numberValue.ToString(),
                        layer: layer,
                        limit: $"[{minInclusive}, {maxInclusive}]");
                }
                else
                {
                    parsingErrorCollection?.Notify(
                        "valueBelowMin",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: numberValue.ToString(),
                        layer: layer,
                        limit: minInclusive.ToString());
                }
            }
            else if (maxInclusive != null && numberValue > maxInclusive)
            {
                if (minInclusive != null)
                {
                    parsingErrorCollection?.Notify(
                        "valueAboveRange",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: numberValue.ToString(),
                        layer: layer,
                        limit: $"[{minInclusive}, {maxInclusive}]");
                }
                else
                {
                    parsingErrorCollection?.Notify(
                        "valueAboveMax",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        propertyValue: numberValue.ToString(),
                        layer: layer,
                        limit: maxInclusive.ToString());
                }
            }

            return true;
        }

        private static bool TryParseBooleanValue(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValue value, string layer, ParsingErrorCollection parsingErrorCollection, out bool boolValue, string literalType, bool requireBoolean)
        {
            boolValue = false;

            if (value.ValueType == JsonLdValueType.Boolean)
            {
                boolValue = value.BooleanValue;
            }
            else if (value.ValueType == JsonLdValueType.Element)
            {
                CheckForInvalidPropertiesInValueObject(aggregateContext, value.ElementValue, elementId, propertyName, layer, parsingErrorCollection);

                if (value.ElementValue.Value == null)
                {
                    parsingErrorCollection?.Notify(
                        $"{literalType}ObjectNoValue",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);

                    return false;
                }

                bool typeDeclaredBoolean = false;
                bool typeDeclaredNonBoolean = false;
                if (value.ElementValue.Types != null)
                {
                    if (value.ElementValue.Types.Count != 1)
                    {
                        parsingErrorCollection?.Notify(
                            "literalTypeNotSingular",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                        return false;
                    }

                    typeDeclaredBoolean = value.ElementValue.Types.Contains("xsd:boolean") || value.ElementValue.Types.Contains("http://www.w3.org/2001/XMLSchema#boolean");
                    typeDeclaredNonBoolean = !typeDeclaredBoolean;
                }

                if (typeDeclaredNonBoolean)
                {
                    if (requireBoolean)
                    {
                        parsingErrorCollection?.Notify(
                            "booleanTypeNotBoolean",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                if (value.ElementValue.Value.ValueType != JsonLdValueType.Boolean)
                {
                    if (requireBoolean || typeDeclaredBoolean)
                    {
                        parsingErrorCollection?.Notify(
                            "booleanValueNotBoolean",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: value,
                            layer: layer);
                    }

                    return false;
                }

                boolValue = value.ElementValue.Value.BooleanValue;
            }
            else
            {
                if (requireBoolean)
                {
                    parsingErrorCollection?.Notify(
                        "booleanNotBoolean",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: value,
                        layer: layer);
                }

                return false;
            }

            return true;
        }

        private static bool TryParseIdentifierValue(Dtmi elementId, AggregateContext aggregateContext, string propertyName, JsonLdValue value, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection, bool requireDtmi, out Uri uri)
        {
            uri = null;

            if (value.ValueType != JsonLdValueType.String)
            {
                parsingErrorCollection?.Notify(
                    "identifierNotString",
                    elementId: elementId,
                    propertyName: propertyName,
                    typeRestriction: requireDtmi ? "DTMI" : "URI",
                    incidentValue: value,
                    layer: layer);

                return false;
            }

            if (!aggregateContext.TryCreateUri(value.StringValue, out uri, requireDtmi: requireDtmi))
            {
                parsingErrorCollection?.Notify(
                    requireDtmi ? "identifierNotDtmi" : "identifierNotUri",
                    elementId: elementId,
                    propertyName: propertyName,
                    typeRestriction: requireDtmi ? "DTMI" : "URI",
                    incidentValue: value,
                    propertyValue: value.StringValue,
                    layer: layer);

                return false;
            }

            if (maxLength != null && uri.AbsoluteUri.Length > maxLength)
            {
                parsingErrorCollection?.Notify(
                    "identifierTooLong",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValue: value,
                    propertyValue: uri.AbsoluteUri,
                    layer: layer,
                    expectedCount: maxLength);
            }

            if (pattern != null && !pattern.IsMatch(uri.AbsoluteUri))
            {
                parsingErrorCollection?.Notify(
                    "identifierInvalid",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentValue: value,
                    propertyValue: uri.AbsoluteUri,
                    layer: layer,
                    pattern: pattern.ToString());
            }

            return true;
        }

        private static Dictionary<string, string> GetDictionaryFromLanguageTaggedStringCollection(AggregateContext aggregateContext, Dtmi elementId, string propertyName, JsonLdValueCollection valueCollection, string defaultLang, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection)
        {
            Dictionary<string, JsonLdValue> langStringValues = new Dictionary<string, JsonLdValue>();
            var dict = new Dictionary<string, string>();

            foreach (JsonLdValue langStringValue in valueCollection.Values)
            {
                if (langStringValue.ValueType != JsonLdValueType.String && langStringValue.ValueType != JsonLdValueType.Element)
                {
                    parsingErrorCollection?.Notify(
                        "langStringElementNotStringOrObject",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: langStringValue,
                        layer: layer);

                    continue;
                }

                string langCode = null;
                string langValue = null;

                if (langStringValue.ValueType == JsonLdValueType.String)
                {
                    langCode = defaultLang;
                    langValue = langStringValue.StringValue;
                }
                else
                {
                    if (langStringValue.ElementValue.Language == null)
                    {
                        langCode = defaultLang;
                    }
                    else
                    {
                        if (langStringValue.ElementValue.Language == string.Empty)
                        {
                            parsingErrorCollection?.Notify(
                                "langStringElementCodeNotString",
                                elementId: elementId,
                                propertyName: propertyName,
                                element: langStringValue.ElementValue,
                                layer: layer);

                            continue;
                        }

                        langCode = langStringValue.ElementValue.Language;

                        if (!IsValidLanguageCode(langCode))
                        {
                            parsingErrorCollection?.Notify(
                                "langStringElementInvalidCode",
                                elementId: elementId,
                                propertyName: propertyName,
                                element: langStringValue.ElementValue,
                                langCode: langCode,
                                layer: layer);
                        }
                    }

                    if (langStringValue.ElementValue.Value == null)
                    {
                        parsingErrorCollection?.Notify(
                            "langStringElementNoValue",
                            elementId: elementId,
                            propertyName: propertyName,
                            element: langStringValue.ElementValue,
                            layer: layer);
                    }
                    else if (langStringValue.ElementValue.Value.ValueType != JsonLdValueType.String)
                    {
                        parsingErrorCollection?.Notify(
                            "langStringElementValueNotString",
                            elementId: elementId,
                            propertyName: propertyName,
                            element: langStringValue.ElementValue,
                            layer: layer);

                        continue;
                    }
                    else
                    {
                        langValue = langStringValue.ElementValue.Value.StringValue;
                    }

                    if (langStringValue.ElementValue.Context != null)
                    {
                        parsingErrorCollection?.Notify(
                            "langStringElementContext",
                            elementId: elementId,
                            propertyName: propertyName,
                            element: langStringValue.ElementValue,
                            layer: layer);
                    }

                    if (langStringValue.ElementValue.Id != null)
                    {
                        parsingErrorCollection?.Notify(
                            "langStringElementId",
                            elementId: elementId,
                            propertyName: propertyName,
                            element: langStringValue.ElementValue,
                            layer: layer);
                    }

                    if (langStringValue.ElementValue.Graph != null)
                    {
                        parsingErrorCollection?.Notify(
                            "langStringElementGraph",
                            elementId: elementId,
                            propertyName: propertyName,
                            element: langStringValue.ElementValue,
                            layer: layer);
                    }

                    if (aggregateContext.RestrictKeywords)
                    {
                        if (langStringValue.ElementValue.Types != null)
                        {
                            parsingErrorCollection?.Notify(
                                "langStringElementType",
                                elementId: elementId,
                                propertyName: propertyName,
                                element: langStringValue.ElementValue,
                                layer: layer);
                        }

                        foreach (string keyword in langStringValue.ElementValue.Keywords)
                        {
                            parsingErrorCollection?.Notify(
                                "langStringElementKeyword",
                                elementId: elementId,
                                propertyName: propertyName,
                                element: langStringValue.ElementValue,
                                keyword: keyword,
                                layer: layer);
                        }
                    }

                    foreach (JsonLdProperty langStringProp in langStringValue.ElementValue.Properties)
                    {
                        parsingErrorCollection?.Notify(
                            "langStringElementInvalidProp",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentProperty: langStringProp,
                            literalPropertyName: langStringProp.Name,
                            layer: layer);
                    }
                }

                if (langValue != null && maxLength != null && langValue.Length > maxLength)
                {
                    parsingErrorCollection?.Notify(
                        "langStringValueTooLong",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: langStringValue.ValueType == JsonLdValueType.String ? langStringValue : langStringValue.ElementValue.Value,
                        propertyValue: langValue,
                        layer: layer,
                        expectedCount: maxLength);
                }

                if (langValue != null && pattern != null && !pattern.IsMatch(langValue))
                {
                    parsingErrorCollection?.Notify(
                        "langStringValueInvalid",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: langStringValue.ValueType == JsonLdValueType.String ? langStringValue : langStringValue.ElementValue.Value,
                        propertyValue: langValue,
                        layer: layer,
                        pattern: pattern.ToString());
                }

                if (langStringValues.TryGetValue(langCode, out JsonLdValue extantLangStringValue))
                {
                    parsingErrorCollection?.Notify(
                        "langStringElementCodeNotUnique",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentValue: langStringValue,
                        extantValue: extantLangStringValue,
                        langCode: langCode,
                        layer: layer);
                }
                else
                {
                    langStringValues[langCode] = langStringValue;
                    dict[langCode] = langValue;
                }
            }

            return dict;
        }

        private static Dictionary<string, string> GetDictionaryFromLanguageMap(Dtmi elementId, string propertyName, JsonLdElement elt, int? maxLength, Regex pattern, string layer, ParsingErrorCollection parsingErrorCollection)
        {
            Dictionary<string, JsonLdProperty> langStringProps = new Dictionary<string, JsonLdProperty>();
            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (elt.Language != null && elt.Value != null && elt.Value.ValueType == JsonLdValueType.String && elt.PropertyCount == 2)
            {
                parsingErrorCollection?.Notify(
                    "langTagValueInLangMap",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    nameValuePair: $"{{ \"{elt.Language}\": \"{elt.Value.StringValue}\" }}",
                    layer: layer);
            }
            else
            {
                if (elt.Language != null)
                {
                    parsingErrorCollection?.Notify(
                        "langMapObjKeyword",
                        elementId: elementId,
                        propertyName: propertyName,
                        element: elt,
                        keyword: "@language",
                        layer: layer);
                }

                if (elt.Value != null)
                {
                    parsingErrorCollection?.Notify(
                        "langMapObjKeyword",
                        elementId: elementId,
                        propertyName: propertyName,
                        element: elt,
                        keyword: "@value",
                        layer: layer);
                }
            }

            if (elt.Context != null)
            {
                parsingErrorCollection?.Notify(
                    "langMapObjKeyword",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    keyword: "@context",
                    layer: layer);
            }

            if (elt.Id != null)
            {
                parsingErrorCollection?.Notify(
                    "langMapObjKeyword",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    keyword: "@id",
                    layer: layer);
            }

            if (elt.Types != null)
            {
                parsingErrorCollection?.Notify(
                    "langMapObjKeyword",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    keyword: "@type",
                    layer: layer);
            }

            if (elt.Graph != null)
            {
                parsingErrorCollection?.Notify(
                    "langMapObjKeyword",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    keyword: "@graph",
                    layer: layer);
            }

            foreach (string keyword in elt.Keywords)
            {
                parsingErrorCollection?.Notify(
                    "langMapObjKeyword",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    keyword: keyword,
                    layer: layer);
            }

            foreach (JsonLdProperty langStringProp in elt.Properties)
            {
                if (!IsValidLanguageCode(langStringProp.Name))
                {
                    parsingErrorCollection?.Notify(
                        "langStringInvalidCode",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentProperty: langStringProp,
                        langCode: langStringProp.Name,
                        layer: layer);
                }

                string langValue = null;

                if (langStringProp.Values.Count != 1 || langStringProp.Values.HasPluralRepresentation || langStringProp.Values.Values.First().ValueType != JsonLdValueType.String)
                {
                    parsingErrorCollection?.Notify(
                        "langStringValueNotString",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentProperty: langStringProp,
                        langCode: langStringProp.Name,
                        layer: layer);
                }
                else
                {
                    langValue = langStringProp.Values.Values.First().StringValue;

                    if (maxLength != null && langValue.Length > maxLength)
                    {
                        parsingErrorCollection?.Notify(
                            "langStringValueTooLong",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: langStringProp.Values.Values.First(),
                            propertyValue: langValue,
                            layer: layer,
                            expectedCount: maxLength);
                    }

                    if (pattern != null && !pattern.IsMatch(langValue))
                    {
                        parsingErrorCollection?.Notify(
                            "langStringValueInvalid",
                            elementId: elementId,
                            propertyName: propertyName,
                            incidentValue: langStringProp.Values.Values.First(),
                            propertyValue: langValue,
                            layer: layer,
                            pattern: pattern.ToString());
                    }
                }

                if (langStringProps.TryGetValue(langStringProp.Name, out JsonLdProperty extantLangStringProp))
                {
                    parsingErrorCollection?.Notify(
                        "langStringCodeNotUnique",
                        elementId: elementId,
                        propertyName: propertyName,
                        incidentProperty: langStringProp,
                        extantProperty: extantLangStringProp,
                        langCode: langStringProp.Name,
                        layer: layer);
                }
                else
                {
                    langStringProps[langStringProp.Name] = langStringProp;
                    dict[langStringProp.Name] = langValue;
                }
            }

            return dict;
        }

        private static void CheckForInvalidPropertiesInValueObject(AggregateContext aggregateContext, JsonLdElement elt, Dtmi elementId, string propertyName, string layer, ParsingErrorCollection parsingErrorCollection)
        {
            if (elt.Context != null)
            {
                parsingErrorCollection?.Notify(
                    "valueObjectContext",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    layer: layer);
            }

            if (elt.Id != null)
            {
                parsingErrorCollection?.Notify(
                    "valueObjectId",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    layer: layer);
            }

            if (elt.Graph != null)
            {
                parsingErrorCollection?.Notify(
                    "valueObjectGraph",
                    elementId: elementId,
                    propertyName: propertyName,
                    element: elt,
                    layer: layer);
            }

            if (aggregateContext.RestrictKeywords)
            {
                if (elt.Language != null)
                {
                    parsingErrorCollection?.Notify(
                        "valueObjectLanguage",
                        elementId: elementId,
                        propertyName: propertyName,
                        element: elt,
                        layer: layer);
                }

                foreach (string keyword in elt.Keywords)
                {
                    parsingErrorCollection?.Notify(
                        "valueObjectKeyword",
                        elementId: elementId,
                        propertyName: propertyName,
                        element: elt,
                        keyword: keyword,
                        layer: layer);
                }
            }

            foreach (JsonLdProperty objProp in elt.Properties)
            {
                parsingErrorCollection?.Notify(
                    "valueObjectInvalidProp",
                    elementId: elementId,
                    propertyName: propertyName,
                    incidentProperty: objProp,
                    literalPropertyName: objProp.Name,
                    layer: layer);
            }
        }

        private static bool IsValidLanguageCode(string langCode)
        {
            return LanguageCodeRegex.IsMatch(langCode);
        }
    }
}
