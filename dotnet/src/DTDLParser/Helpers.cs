namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;

    /// <summary>
    /// A static class that holds various helper functions.
    /// </summary>
    internal static partial class Helpers
    {
        private const int HashCodeJsonUndefined = 0x01010101;
        private const int HashCodeJsonTrue = 0x02020202;
        private const int HashCodeJsonFalse = 0x04040404;
        private const int HashCodeJsonNull = 0x08080808;

        private static readonly HashSet<char> Vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };

        /// <summary>
        /// Checks the value equality of two objects if both are literals.
        /// </summary>
        /// <param name="val1">One of the objects to compare for equality.</param>
        /// <param name="val2">The object to compare against.</param>
        /// <returns>True if the objects are both literals and are equal.</returns>
        internal static bool AreObjectsLiteralEqual(object val1, object val2)
        {
            if (val1 is string string1 && val2 is string string2)
            {
                return string1 == string2;
            }
            else if (val1 is double double1)
            {
                if (val2 is int int2)
                {
                    return double1 == (double)int2;
                }
                else if (val2 is double double2)
                {
                    return double1 == double2;
                }
                else
                {
                    return false;
                }
            }
            else if (val1 is int int1)
            {
                if (val2 is int int2)
                {
                    return int1 == int2;
                }
                else if (val2 is double double2)
                {
                    return (double)int1 == double2;
                }
                else
                {
                    return false;
                }
            }
            else if (val1 is bool bool1 && val2 is bool bool2)
            {
                return bool1 == bool2;
            }
            else if (val1 is Uri uri1 && val2 is Uri uri2)
            {
                return uri1 == uri2;
            }
            else if (val1 is JsonElement elt1 && val2 is JsonElement elt2)
            {
                return elt1.ToString() == elt2.ToString();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the equality of two dictionaries whose values are literal types.
        /// </summary>
        /// <typeparam name="T">The literal type of the dictionary values.</typeparam>
        /// <param name="dict1">One of the dictionaries to compare for equality.</param>
        /// <param name="dict2">The dictionary to compare against.</param>
        /// <returns>True if the dictionaries are equal.</returns>
        internal static bool AreDictionariesLiteralEqual<T>(IReadOnlyDictionary<string, T> dict1, IReadOnlyDictionary<string, T> dict2)
            where T : IComparable
        {
            if (dict1.Count != dict2.Count)
            {
                return false;
            }

            foreach (KeyValuePair<string, T> kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out T value))
                {
                    return false;
                }

                if (kvp.Value.CompareTo(value) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the equality of two dictionaries whose values export the <c>IEquatable</c> interface.
        /// </summary>
        /// <typeparam name="T">The type of the dictionary values.</typeparam>
        /// <param name="dict1">One of the dictionaries to compare for equality.</param>
        /// <param name="dict2">The dictionary to compare against.</param>
        /// <returns>True if the dictionaries are equal.</returns>
        internal static bool AreDictionariesEquatablyEqual<T>(IReadOnlyDictionary<string, T> dict1, IReadOnlyDictionary<string, T> dict2)
            where T : IEquatable<T>
        {
            if (dict1.Count != dict2.Count)
            {
                return false;
            }

            foreach (KeyValuePair<string, T> kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out T value))
                {
                    return false;
                }

                if (!kvp.Value.Equals(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Finds a set of keys whose literal values are different across two dictionaries.
        /// </summary>
        /// <typeparam name="T">The literal type of the dictionary values.</typeparam>
        /// <param name="dict1">One of the dictionaries to compare.</param>
        /// <param name="dict2">The dictionary to compare against.</param>
        /// <returns>A list of string keys that are present in both dictionaries but with different values.</returns>
        internal static List<string> GetKeysWithDifferingLiteralValues<T>(IReadOnlyDictionary<string, T> dict1, IReadOnlyDictionary<string, T> dict2)
            where T : IComparable
        {
            List<string> differingKeys = new List<string>();

            foreach (KeyValuePair<string, T> kvp in dict1)
            {
                if (dict2.TryGetValue(kvp.Key, out T value))
                {
                    if (kvp.Value.CompareTo(value) != 0)
                    {
                        differingKeys.Add(kvp.Key);
                    }
                }
            }

            return differingKeys;
        }

        /// <summary>
        /// Computes a hash code of a dictionary based on its literal values.
        /// </summary>
        /// <typeparam name="T">The literal type of the dictionary values.</typeparam>
        /// <param name="dict">The dictionary to hash.</param>
        /// <returns>The hash value of the dictionary.</returns>
        internal static int GetDictionaryLiteralHashCode<T>(IReadOnlyDictionary<string, T> dict)
        {
            int hashCode = 0;

            foreach (KeyValuePair<string, T> kvp in dict)
            {
                unchecked
                {
                    hashCode += (kvp.Key.GetHashCode() * 131) + kvp.Value.GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Computes a hash code of a list based on its literal values.
        /// </summary>
        /// <typeparam name="T">The literal type of the list values.</typeparam>
        /// <param name="list">The list to hash.</param>
        /// <returns>The hash value of the list.</returns>
        internal static int GetListLiteralHashCode<T>(IReadOnlyList<T> list)
        {
            int hashCode = 0;

            foreach (T element in list)
            {
                unchecked
                {
                    hashCode = (hashCode * 131) + element.GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Computes a hash code of a set based on its literal values.
        /// </summary>
        /// <typeparam name="T">The literal type of the set values.</typeparam>
        /// <param name="set">The set to hash.</param>
        /// <returns>The hash value of the set.</returns>
        internal static int GetSetLiteralHashCode<T>(ISet<T> set)
        {
            int hashCode = 0;

            foreach (T element in set)
            {
                unchecked
                {
                    hashCode += element.GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Get a string representation of basic datatypes integer, string, and boolean.
        /// </summary>
        /// <param name="datatype">The <c>Type</c> value to represent.</param>
        /// <param name="addArticle">True if an article should prepend the datatype representation.</param>
        /// <returns>The string representation.</returns>
        internal static string GetDatytypeString(Type datatype, bool addArticle = false)
        {
            if (datatype == typeof(int))
            {
                return addArticle ? "an integer" : "integer";
            }
            else if (datatype == typeof(string))
            {
                return addArticle ? "a string" : "string";
            }
            else if (datatype == typeof(bool))
            {
                return addArticle ? "a boolean" : "boolean";
            }
            else
            {
                throw new Exception("unrecognized type");
            }
        }

        /// <summary>
        /// Return a string that adds quotation marks and an appropriate indefinite article to the given string .
        /// </summary>
        /// <param name="noun">The string to which to add quotation marks and n article.</param>
        /// <returns>The resulting string.</returns>
        internal static string QuoteAndArticlize(string noun)
        {
            return Vowels.Contains(char.ToLower(noun.FirstOrDefault())) ? $"an '{noun}'" : $"a '{noun}'";
        }

        /// <summary>
        /// Count the unique elements in an enumeration.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumeration.</typeparam>
        /// <param name="items">The enumearation containing the elements.</param>
        /// <returns>The count of unique elements.</returns>
        internal static int CountUnique<T>(IEnumerable<T> items)
        {
            int count = 0;
            HashSet<T> hashSet = new HashSet<T>();

            foreach (T item in items)
            {
                count += hashSet.Add(item) ? 1 : 0;
            }

            return count;
        }

        /// <summary>
        /// Get a sorted list of all distinct elements in an enumeration.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumeration.</typeparam>
        /// <param name="items">The enumearation containing the elements.</param>
        /// <returns>A sorted list of distinct elements.</returns>
        internal static List<T> SortUnique<T>(IEnumerable<T> items)
        {
            List<T> uniqueItems = new HashSet<T>(items).ToList();
            uniqueItems.Sort();
            return uniqueItems;
        }

        /// <summary>
        /// Determine whether an enumeration contains exactly one unique item and provide the value if so.
        /// </summary>
        /// <typeparam name="T">The type of elements in the enumeration.</typeparam>
        /// <param name="items">The enumearation containing the elements.</param>
        /// <param name="uniqueItem">Out parameter to receive the value of the unique item.</param>
        /// <returns>True if exactly one unique value is present in the enumeration.</returns>
        internal static bool TryGetSingleUniqueValue<T>(IEnumerable<T> items, out T uniqueItem)
            where T : IEquatable<T>
        {
            IEnumerator<T> itemEnum = items.GetEnumerator();
            if (!itemEnum.MoveNext())
            {
                uniqueItem = default(T);
                return false;
            }

            uniqueItem = itemEnum.Current;
            while (itemEnum.MoveNext())
            {
                if (!itemEnum.Current.Equals(uniqueItem))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AreJsonElementsEqual(JsonElement elt1, JsonElement elt2)
        {
            switch (elt1.ValueKind)
            {
                case JsonValueKind.Undefined:
                    return false;
                case JsonValueKind.Object:
                    foreach (JsonProperty prop1 in elt1.EnumerateObject())
                    {
                        if (!elt2.TryGetProperty(prop1.Name, out JsonElement child2) || !AreJsonElementsEqual(prop1.Value, child2))
                        {
                            return false;
                        }
                    }

                    foreach (JsonProperty prop2 in elt2.EnumerateObject())
                    {
                        if (!elt1.TryGetProperty(prop2.Name, out JsonElement _))
                        {
                            return false;
                        }
                    }

                    return true;
                case JsonValueKind.Array:
                    if (elt1.GetArrayLength() != elt2.GetArrayLength())
                    {
                        return false;
                    }

                    for (int ix = 0; ix < elt1.GetArrayLength(); ++ix)
                    {
                        if (!AreJsonElementsEqual(elt1[ix], elt2[ix]))
                        {
                            return false;
                        }
                    }

                    return true;
                case JsonValueKind.String:
                    return elt1.GetString() == elt2.GetString();
                case JsonValueKind.Number:
                    return elt1.GetDecimal() == elt2.GetDecimal();
                case JsonValueKind.True:
                    return elt2.ValueKind == JsonValueKind.True;
                case JsonValueKind.False:
                    return elt2.ValueKind == JsonValueKind.False;
                case JsonValueKind.Null:
                    return elt2.ValueKind == JsonValueKind.Null;
                default:
                    return false;
            }
        }

        private static int GetJsonElementHashCode(JsonElement elt)
        {
            switch (elt.ValueKind)
            {
                case JsonValueKind.Undefined:
                    return HashCodeJsonUndefined;

                case JsonValueKind.Object:
                    int objHashCode = 0;
                    foreach (JsonProperty prop in elt.EnumerateObject())
                    {
                        unchecked
                        {
                            objHashCode += (prop.Name.GetHashCode() * 131) + GetJsonElementHashCode(prop.Value);
                        }
                    }

                    return objHashCode;

                case JsonValueKind.Array:
                    int arrayHashCode = 0;
                    foreach (JsonElement child in elt.EnumerateArray())
                    {
                        unchecked
                        {
                            arrayHashCode = (arrayHashCode * 131) + GetJsonElementHashCode(child);
                        }
                    }

                    return arrayHashCode;

                case JsonValueKind.String:
                    return elt.GetString().GetHashCode();

                case JsonValueKind.Number:
                    return elt.GetDecimal().GetHashCode();

                case JsonValueKind.True:
                    return HashCodeJsonTrue;

                case JsonValueKind.False:
                    return HashCodeJsonFalse;

                case JsonValueKind.Null:
                    return HashCodeJsonNull;

                default:
                    return 0;
            }
        }
    }
}
