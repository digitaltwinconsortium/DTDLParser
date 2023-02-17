/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using DTDLParser.Models;

    /// <summary>
    /// A static class that holds various helper functions.
    /// </summary>
    internal static partial class Helpers
    {
        /// <summary>
        /// Checks the equality of two dictionaries by deeply comparing their values.
        /// </summary>
        /// <typeparam name="T">The object type of the dictionary values.</typeparam>
        /// <param name="dict1">One of the dictionaries to compare for equality.</param>
        /// <param name="dict2">The dictionary to compare against.</param>
        /// <returns>True if the dictionaries are deeply equal.</returns>
        internal static bool AreDictionariesDeepEqual<T>(IReadOnlyDictionary<string, T> dict1, IReadOnlyDictionary<string, T> dict2)
            where T : DTEntityInfo
        {
            if (dict1.Count != dict2.Count)
            {
                return false;
            }

            foreach (KeyValuePair<string, T> kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out T value) || !kvp.Value.DeepEquals(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the equality of two dictionaries by deeply comparing their values or comparing their literal values.
        /// </summary>
        /// <param name="dict1">One of the dictionaries to compare for equality.</param>
        /// <param name="dict2">The dictionary to compare against.</param>
        /// <returns>True if the dictionaries are deeply equal.</returns>
        internal static bool AreDictionariesDeepOrLiteralEqual(IReadOnlyDictionary<string, object> dict1, IReadOnlyDictionary<string, object> dict2)
        {
            if (dict1.Count != dict2.Count)
            {
                return false;
            }

            foreach (KeyValuePair<string, object> kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out object value))
                {
                    return false;
                }

                if (kvp.Value is DTEntityInfo val1 && value is DTEntityInfo val2)
                {
                    if (!val1.DeepEquals(val2))
                    {
                        return false;
                    }
                }
                else if (kvp.Value is Uri id1 && value is Uri id2)
                {
                    if (id1 != id2)
                    {
                        return false;
                    }
                }
                else if (kvp.Value is List<Uri> uris1 && value is List<Uri> uris2)
                {
                    if (!uris1.SequenceEqual(uris2))
                    {
                        return false;
                    }
                }
                else if (((IComparable)kvp.Value).CompareTo((IComparable)value) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the equality of two dictionaries by comparing their identifier values.
        /// </summary>
        /// <typeparam name="T">The object type of the dictionary values.</typeparam>
        /// <param name="dict1">One of the dictionaries to compare for equality.</param>
        /// <param name="dict2">The dictionary to compare against.</param>
        /// <returns>True if the dictionaries are equal.</returns>
        internal static bool AreDictionariesIdEqual<T>(IReadOnlyDictionary<string, T> dict1, IReadOnlyDictionary<string, T> dict2)
            where T : DTEntityInfo
        {
            if (dict1.Count != dict2.Count)
            {
                return false;
            }

            foreach (KeyValuePair<string, T> kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out T value) || kvp.Value.Id != value.Id)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the equality of two dictionaries by comparing their identifier values or literal values.
        /// </summary>
        /// <param name="dict1">One of the dictionaries to compare for equality.</param>
        /// <param name="dict2">The dictionary to compare against.</param>
        /// <returns>True if the dictionaries are equal.</returns>
        internal static bool AreDictionariesIdOrLiteralEqual(IReadOnlyDictionary<string, object> dict1, IReadOnlyDictionary<string, object> dict2)
        {
            if (dict1.Count != dict2.Count)
            {
                return false;
            }

            foreach (KeyValuePair<string, object> kvp in dict1)
            {
                if (!dict2.TryGetValue(kvp.Key, out object value))
                {
                    return false;
                }

                if (kvp.Value is DTEntityInfo val1 && value is DTEntityInfo val2)
                {
                    if (val1.Id != val2.Id)
                    {
                        return false;
                    }
                }
                else if (kvp.Value is Uri id1 && value is Uri id2)
                {
                    if (id1 != id2)
                    {
                        return false;
                    }
                }
                else if (kvp.Value is List<Uri> uris1 && value is List<Uri> uris2)
                {
                    if (!uris1.SequenceEqual(uris2))
                    {
                        return false;
                    }
                }
                else if (kvp.Value is JsonElement elt1 && value is JsonElement elt2)
                {
                    if (!AreJsonElementsEqual(elt1, elt2))
                    {
                        return false;
                    }
                }
                else if (kvp.Value is List<object> objList1 && value is List<object> objList2)
                {
                    if (!AreListsIdOrLiteralEqual(objList1, objList2))
                    {
                        return false;
                    }
                }
                else if (((IComparable)kvp.Value).CompareTo((IComparable)value) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the equality of two lists by deeply comparing their values.
        /// </summary>
        /// <typeparam name="T">The object type of the list values.</typeparam>
        /// <param name="list1">One of the lists to compare for equality.</param>
        /// <param name="list2">The list to compare against.</param>
        /// <returns>True if the lists are deeply equal.</returns>
        internal static bool AreListsDeepEqual<T>(IReadOnlyList<T> list1, IReadOnlyList<T> list2)
            where T : DTEntityInfo
        {
            IEnumerator<T> enum1 = list1.GetEnumerator();
            IEnumerator<T> enum2 = list2.GetEnumerator();

            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                {
                    return false;
                }

                if (!enum1.Current.DeepEquals(enum2.Current))
                {
                    return false;
                }
            }

            return !enum2.MoveNext();
        }

        /// <summary>
        /// Checks the equality of two lists by deeply comparing their values or comparing their literal values.
        /// </summary>
        /// <param name="list1">One of the lists to compare for equality.</param>
        /// <param name="list2">The list to compare against.</param>
        /// <returns>True if the lists are deeply equal.</returns>
        internal static bool AreListsDeepOrLiteralEqual(IReadOnlyList<object> list1, IReadOnlyList<object> list2)
        {
            IEnumerator<object> enum1 = list1.GetEnumerator();
            IEnumerator<object> enum2 = list2.GetEnumerator();

            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                {
                    return false;
                }

                if (enum1.Current is DTEntityInfo val1 && enum2.Current is DTEntityInfo val2)
                {
                    if (!val1.DeepEquals(val2))
                    {
                        return false;
                    }
                }
                else if (enum1.Current is Uri id1 && enum2.Current is Uri id2)
                {
                    if (id1 != id2)
                    {
                        return false;
                    }
                }
                else if (enum1.Current is List<Uri> uris1 && enum2.Current is List<Uri> uris2)
                {
                    if (!uris1.SequenceEqual(uris2))
                    {
                        return false;
                    }
                }
                else if (((IComparable)enum1.Current).CompareTo((IComparable)enum2.Current) != 0)
                {
                    return false;
                }
            }

            return !enum2.MoveNext();
        }

        /// <summary>
        /// Checks the equality of two lists by comparing their identifier values.
        /// </summary>
        /// <typeparam name="T">The object type of the list values.</typeparam>
        /// <param name="list1">One of the lists to compare for equality.</param>
        /// <param name="list2">The list to compare against.</param>
        /// <returns>True if the lists are equal.</returns>
        internal static bool AreListsIdEqual<T>(IReadOnlyList<T> list1, IReadOnlyList<T> list2)
            where T : DTEntityInfo
        {
            IEnumerator<T> enum1 = list1.GetEnumerator();
            IEnumerator<T> enum2 = list2.GetEnumerator();

            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                {
                    return false;
                }

                if (enum1.Current.Id != enum2.Current.Id)
                {
                    return false;
                }
            }

            return !enum2.MoveNext();
        }

        /// <summary>
        /// Checks the equality of two lists by comparing their identifier values or literal values.
        /// </summary>
        /// <param name="list1">One of the lists to compare for equality.</param>
        /// <param name="list2">The list to compare against.</param>
        /// <returns>True if the lists are equal.</returns>
        internal static bool AreListsIdOrLiteralEqual(IReadOnlyList<object> list1, IReadOnlyList<object> list2)
        {
            IEnumerator<object> enum1 = list1.GetEnumerator();
            IEnumerator<object> enum2 = list2.GetEnumerator();

            while (enum1.MoveNext())
            {
                if (!enum2.MoveNext())
                {
                    return false;
                }

                if (enum1.Current is DTEntityInfo val1 && enum2.Current is DTEntityInfo val2)
                {
                    if (val1.Id != val2.Id)
                    {
                        return false;
                    }
                }
                else if (enum1.Current is Uri id1 && enum2.Current is Uri id2)
                {
                    if (id1 != id2)
                    {
                        return false;
                    }
                }
                else if (enum1.Current is List<Uri> uris1 && enum2.Current is List<Uri> uris2)
                {
                    if (!uris1.SequenceEqual(uris2))
                    {
                        return false;
                    }
                }
                else if (((IComparable)enum1.Current).CompareTo((IComparable)enum2.Current) != 0)
                {
                    return false;
                }
            }

            return !enum2.MoveNext();
        }

        /// <summary>
        /// Gets the identifier of an object assumed to be a DTEntityInfo.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The identifier of the object.</returns>
        internal static Dtmi GetObjectId(object obj)
        {
            return ((DTEntityInfo)obj).Id;
        }

        /// <summary>
        /// Computes a hash code of a dictionary based on its identifier values.
        /// </summary>
        /// <typeparam name="T">The object type of the dictionary values.</typeparam>
        /// <param name="dict">The dictionary to hash.</param>
        /// <returns>The hash value of the dictionary.</returns>
        internal static int GetDictionaryIdHashCode<T>(IReadOnlyDictionary<string, T> dict)
            where T : DTEntityInfo
        {
            int hashCode = 0;
            foreach (KeyValuePair<string, T> kvp in dict)
            {
                unchecked
                {
                    hashCode += (kvp.Key.GetHashCode() * 131) * kvp.Value.Id.GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Computes a hash code of a dictionary based on its identifier values or literal values.
        /// </summary>
        /// <param name="dict">The dictionary to hash.</param>
        /// <returns>The hash value of the dictionary.</returns>
        internal static int GetDictionaryIdOrLiteralHashCode(IReadOnlyDictionary<string, object> dict)
        {
            int hashCode = 0;
            foreach (KeyValuePair<string, object> kvp in dict)
            {
                int valueHashCode;
                if (kvp.Value is List<Uri> uris)
                {
                    valueHashCode = 0;
                    foreach (Uri uri in uris)
                    {
                        unchecked
                        {
                            valueHashCode = (valueHashCode * 131) + uri.GetHashCode();
                        }
                    }
                }
                else if (kvp.Value is JsonElement elt)
                {
                    valueHashCode = GetJsonElementHashCode(elt);
                }
                else if (kvp.Value is List<object> objList)
                {
                    valueHashCode = GetListIdOrLiteralHashCode(objList);
                }
                else
                {
                    valueHashCode = ((kvp.Value as DTEntityInfo)?.Id ?? kvp.Value).GetHashCode();
                }

                unchecked
                {
                    hashCode += (kvp.Key.GetHashCode() * 131) + valueHashCode;
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Computes a hash code of a list based on its identifier values.
        /// </summary>
        /// <typeparam name="T">The object type of the list values.</typeparam>
        /// <param name="list">The list to hash.</param>
        /// <returns>The hash value of the list.</returns>
        internal static int GetListIdHashCode<T>(IReadOnlyList<T> list)
            where T : DTEntityInfo
        {
            int hashCode = 0;
            foreach (T element in list)
            {
                unchecked
                {
                    hashCode = (hashCode * 131) + element.Id.GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Computes a hash code of a list based on its identifier values or literal values.
        /// </summary>
        /// <param name="list">The list to hash.</param>
        /// <returns>The hash value of the list.</returns>
        internal static int GetListIdOrLiteralHashCode(IReadOnlyList<object> list)
        {
            int hashCode = 0;
            foreach (object element in list)
            {
                unchecked
                {
                    hashCode = (hashCode * 131) + ((element as DTEntityInfo)?.Id ?? element).GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Write a JSON representation of the value passed as a C# object.
        /// </summary>
        /// <param name="jsonWriter">A <c>Utf8JsonWriter</c> object with which to write the JSON representation.</param>
        /// <param name="value">The value to write.</param>
        internal static void WriteToJson(Utf8JsonWriter jsonWriter, object value)
        {
            if (value == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }

            Type valueType = value.GetType();
            if (valueType == typeof(JsonDocument))
            {
                ((JsonDocument)value).WriteTo(jsonWriter);
            }
            else if (valueType == typeof(string))
            {
                jsonWriter.WriteStringValue((string)value);
            }
            else if (valueType == typeof(int))
            {
                jsonWriter.WriteNumberValue((int)value);
            }
            else if (valueType == typeof(long))
            {
                jsonWriter.WriteNumberValue((long)value);
            }
            else if (valueType == typeof(float))
            {
                jsonWriter.WriteNumberValue((float)value);
            }
            else if (valueType == typeof(double))
            {
                jsonWriter.WriteNumberValue((double)value);
            }
            else if (valueType == typeof(bool))
            {
                jsonWriter.WriteBooleanValue((bool)value);
            }
            else if (valueType == typeof(Dictionary<string, string>))
            {
                jsonWriter.WriteStartObject();
                foreach (KeyValuePair<string, string> kvp in (Dictionary<string, string>)value)
                {
                    jsonWriter.WriteString(kvp.Key, kvp.Value);
                }

                jsonWriter.WriteEndObject();
            }
            else if (valueType == typeof(Uri))
            {
                jsonWriter.WriteStringValue(((Uri)value).ToString());
            }
            else if (valueType == typeof(Dtmi))
            {
                jsonWriter.WriteStringValue(((Dtmi)value).ToString());
            }
            else
            {
                jsonWriter.WriteStringValue(((DTEntityInfo)value).Id.ToString());
            }
        }
    }
}
