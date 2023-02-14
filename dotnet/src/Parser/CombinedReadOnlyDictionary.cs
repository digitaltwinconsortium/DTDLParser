namespace DTDLParser
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An implementation of <c>IReadOnlyDictionary</c> that combines two non-overlapping <c>IReadOnlyDictionary</c> objects.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the combined dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the combined dictionary.</typeparam>
    internal class CombinedReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private IReadOnlyDictionary<TKey, TValue> dict1;
        private IReadOnlyDictionary<TKey, TValue> dict2;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinedReadOnlyDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="dict1">One of the <c>IReadOnlyDictionary</c> objects to combine.</param>
        /// <param name="dict2">The other <c>IReadOnlyDictionary</c> object to combine.</param>
        internal CombinedReadOnlyDictionary(IReadOnlyDictionary<TKey, TValue> dict1, IReadOnlyDictionary<TKey, TValue> dict2)
        {
            this.dict1 = dict1;
            this.dict2 = dict2;
        }

        /// <inheritdoc/>
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => this.dict1.Keys.Concat(this.dict2.Keys);

        /// <inheritdoc/>
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => this.dict1.Values.Concat(this.dict2.Values);

        /// <inheritdoc/>
        int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => this.dict1.Count + this.dict2.Count;

        /// <inheritdoc/>
        TValue IReadOnlyDictionary<TKey, TValue>.this[TKey key] => this.dict1.TryGetValue(key, out TValue value) ? value : this.dict2[key];

        /// <inheritdoc/>
        bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return this.dict1.ContainsKey(key) || this.dict2.ContainsKey(key);
        }

        /// <inheritdoc/>
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            foreach (var item in this.dict1)
            {
                yield return item;
            }

            foreach (var item in this.dict2)
            {
                yield return item;
            }
        }

        /// <inheritdoc/>
        bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return this.dict1.TryGetValue(key, out value) ? true : this.dict2.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
        }
    }
}
