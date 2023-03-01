namespace DTDLParser
{
    using System.ComponentModel;

    /// <summary>
    /// The <c>ParentReference</c> struct indicates a parent element and the property name by which the parent refers to the child that holds the <c>ParentReference</c>.
    /// </summary>
    internal struct ParentReference
    {
        /// <summary>
        /// The value of the '@id' property of DTDL element that refers to the child that holds the <c>ParentReference</c>.
        /// </summary>
        public Dtmi ParentId;

        /// <summary>
        /// The name of the property by which the parent DTDL element refers to the child that holds the <c>ParentReference</c>.
        /// </summary>
        public string PropertyName;

        /// <summary>
        /// Determines whether two <c>ParentReference</c> objects are not equal.
        /// </summary>
        /// <param name="x">One <c>ParentReference</c> object to compare.</param>
        /// <param name="y">Another <c>ParentReference</c> object to compare to the first.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(ParentReference x, ParentReference y)
        {
            return !x.Equals(y);
        }

        /// <summary>
        /// Determines whether two <c>ParentReference</c> objects are equal.
        /// </summary>
        /// <param name="x">One <c>ParentReference</c> object to compare.</param>
        /// <param name="y">Another <c>ParentReference</c> object to compare to the first.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(ParentReference x, ParentReference y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object otherObj)
        {
            return otherObj is ParentReference other && this.Equals(other);
        }

        /// <summary>
        /// Compares to another <c>ParentReference</c> object.
        /// </summary>
        /// <param name="other">The other <c>ParentReference</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public bool Equals(ParentReference other)
        {
            return this.ParentId == other.ParentId && this.PropertyName == other.PropertyName;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            int hashCode = 0;

            unchecked
            {
                hashCode = (hashCode * 131) + this.ParentId.GetHashCode();
                hashCode = (hashCode * 131) + this.PropertyName.GetHashCode();
            }

            return hashCode;
        }
    }
}
