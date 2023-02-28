namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// A static class that holds an extension method for <c>IReadOnlyDictionary</c>.
    /// </summary>
    internal static class ReadOnlyDictionaryExpander
    {
        /// <summary>
        /// Return an <c>IReadOnlyDictionary</c> that augments a given static <c>IReadOnlyDictionary</c> with a copy of another <c>IReadOnlyDictionary</c>.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the expanded dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the expanded dictionary.</typeparam>
        /// <param name="core">A static <c>IReadOnlyDictionary</c> object to augment with the <paramref name="expansion"/>.</param>
        /// <param name="expansion">A <c>IReadOnlyDictionary</c> object to copy and use to augment the <paramref name="core"/>.</param>
        /// <returns>A combined <c>IReadOnlyDictionary</c> object.</returns>
        internal static IReadOnlyDictionary<TKey, TValue> ExpandWith<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> core, IReadOnlyDictionary<TKey, TValue> expansion)
        {
            return new CombinedReadOnlyDictionary<TKey, TValue>(core, new Dictionary<TKey, TValue>((IDictionary<TKey, TValue>)expansion));
        }
    }
}
