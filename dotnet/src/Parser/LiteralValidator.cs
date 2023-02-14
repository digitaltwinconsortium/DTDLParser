namespace DTDLParser
{
    using System;
    using System.Text.Json;

    /// <summary>
    /// A class for validating literal values in the parser.
    /// </summary>
    internal static class LiteralValidator
    {
        /// <summary>
        /// Determine whether a <c>JsonElement</c> has the value of a given object.
        /// </summary>
        /// <param name="elt">The <c>JsonElement</c> to check.</param>
        /// <param name="value">The object to check against.</param>
        /// <returns>True if the values match.</returns>
        internal static bool HasValue(JsonElement elt, object value)
        {
            Type valueType = value.GetType();
            if (valueType == typeof(string))
            {
                return elt.ValueKind == JsonValueKind.String && (string)value == elt.GetString();
            }
            else if (valueType == typeof(int))
            {
                return elt.ValueKind == JsonValueKind.Number && (int)value == elt.GetInt32();
            }
            else if (valueType == typeof(long))
            {
                return elt.ValueKind == JsonValueKind.Number && (long)value == elt.GetInt64();
            }
            else if (valueType == typeof(float))
            {
                return elt.ValueKind == JsonValueKind.Number && (float)value == elt.GetSingle();
            }
            else if (valueType == typeof(double))
            {
                return elt.ValueKind == JsonValueKind.Number && (double)value == elt.GetDouble();
            }
            else if (valueType == typeof(bool))
            {
                return (elt.ValueKind == JsonValueKind.True && (bool)value == true) || (elt.ValueKind == JsonValueKind.False && (bool)value == false);
            }
            else
            {
                return false;
            }
        }
    }
}
