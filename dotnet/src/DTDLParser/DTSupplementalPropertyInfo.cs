namespace DTDLParser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using DTDLParser;

    /// <summary>
    /// Class <c>DTSupplementalPropertyInfo</c> provides information about a property that can be applied to a DTDL element that has a supplemental type.
    /// </summary>
    public class DTSupplementalPropertyInfo : IEquatable<DTSupplementalPropertyInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DTSupplementalPropertyInfo"/> class.
        /// </summary>
        /// <param name="type">URI that defines the type of the property.</param>
        /// <param name="maxCount">The maximum count of permitted values of the property.</param>
        /// <param name="minCount">The minimum count of permitted values of the property.</param>
        /// <param name="maxInclusive">The maximum permissible value.</param>
        /// <param name="minInclusive">The minimum permissible value.</param>
        /// <param name="maxLength">The maximum permissible length of a string.</param>
        /// <param name="regex">A regex that constrains the permissible values, or null if no pattern constraint.</param>
        /// <param name="hasUniqueValue">True if the property's value must be unique across the corresponding properties in all sibling objects.</param>
        /// <param name="isPlural">True if the property is plural.</param>
        /// <param name="isOptional">True if the property is optional.</param>
        /// <param name="defaultLanguage">The default language code for a language-tagged string literal property.</param>
        /// <param name="dtmiSeg">A DTMI segment identifier, used for auto ID generation.</param>
        /// <param name="dictionaryKey">The name of the child property that acts as a dictionary key, or null if this property is not expressed as a dictionary.</param>
        /// <param name="idRequired">A boolean value indicating whether an @id must be present.</param>
        /// <param name="typeRequired">A boolean value indicating whether a @type must be present.</param>
        /// <param name="childOf">The identifier of a parent element in which the value of this property must be defined.</param>
        /// <param name="instanceProperty">The name of a property of which this property's value must be an instance.</param>
        /// <param name="requiredValues">A list of values that are permitted, or null if no requirement.</param>
        /// <param name="requiredValuesString">A string describing the values that are permitted.</param>
        /// <param name="requiredLiteral">A literal value that is required, or null if no requirement.</param>
        internal DTSupplementalPropertyInfo(Uri type, int? maxCount, int? minCount, int? maxInclusive, int? minInclusive, int? maxLength, Regex regex, bool hasUniqueValue, bool isPlural, bool isOptional, string defaultLanguage, string dtmiSeg, string dictionaryKey, bool idRequired, bool typeRequired, Dtmi childOf, string instanceProperty, List<Dtmi> requiredValues, string requiredValuesString, object requiredLiteral)
        {
            this.Type = type;
            this.IsPlural = isPlural;
            this.IsOptional = isOptional;
            this.DefaultLanguage = defaultLanguage;
            this.DtmiSegment = dtmiSeg;
            this.DictionaryKey = dictionaryKey;
            this.IdRequired = idRequired;
            this.TypeRequired = typeRequired;
            this.ChildOf = childOf;
            this.MaxCount = maxCount;
            this.MinCount = minCount;
            this.MaxInclusive = maxInclusive;
            this.MinInclusive = minInclusive;
            this.MaxLength = maxLength;
            this.Regex = regex;
            this.HasUniqueValue = hasUniqueValue;
            this.InstanceProperty = instanceProperty;
            this.RequiredLiteral = requiredLiteral;

            if (type?.Scheme == "dtmi")
            {
                this.ValueConstraint = new ValueConstraint() { RequiredTypes = new List<Dtmi>() { new Dtmi(type) }, RequiredTypesString = ContextCollection.GetTermOrUri(type), RequiredValues = requiredValues, RequiredValuesString = requiredValuesString };
            }
        }

        /// <summary>
        /// Gets a <c>Uri</c> that indicates the type of the property.
        /// </summary>
        /// <value>The type of the property.</value>
        /// <remarks>
        /// The type of the <c>Type</c> property is a <c>Uri</c> instead of a <c>Dtmi</c> because when a property has a literal value in DTDL documents, its types is given by an XSD type.
        /// When the type of a property is a DTDL type, its Uri will be a Dtmi.
        /// </remarks>
        public Uri Type { get; }

        /// <summary>
        /// Gets a value indicating whether the property is a singular value or a collection of values.
        /// </summary>
        /// <value>True if the property is a collection of values.</value>
        public bool IsPlural { get; }

        /// <summary>
        /// Gets a value indicating whether the property is optional or mandatory.
        /// </summary>
        /// <value>True if the property is optional.</value>
        public bool IsOptional { get; }

        /// <summary>
        /// Gets the default language code for a language-tagged string literal property.
        /// </summary>
        /// <value>The default language code for a language-tagged string.</value>
        public string DefaultLanguage { get; }

        /// <summary>
        /// Gets a DTMI segment identifier, used for auto ID generation.
        /// </summary>
        /// <value>The segment identifier used for DTMI generation.</value>
        public string DtmiSegment { get; }

        /// <summary>
        /// Gets a value indicating the name of the nested property that determines the dictionary key.
        /// </summary>
        /// <value>The name of the nested property used as a dictionary key.</value>
        /// <remarks>
        /// This property has a non-null value only when a plural property is exposed as a <c>>Dictionary</c> instead of a <c>List</c>.
        /// If the property is singular or not a dictionary, the <c>DictionaryKey</c> property will be null.
        /// </remarks>
        public string DictionaryKey { get; }

        /// <summary>
        /// Gets a value indicating whether an @id must be present.
        /// </summary>
        /// <value>True if the model requieres an identifier for the element.</value>
        public bool IdRequired { get; }

        /// <summary>
        /// Gets a value indicating whether a @type must be present.
        /// </summary>
        /// <value>True if the model requieres the type of the element to be specified.</value>
        public bool TypeRequired { get; }

        /// <summary>
        /// Gets the identifier of a parent element in which the value of this property must be defined.
        /// </summary>
        public Dtmi ChildOf { get; }

        /// <summary>
        /// Gets a <see cref="ValueConstraint"/> that should be applied to the property.
        /// </summary>
        internal ValueConstraint ValueConstraint { get; }

        /// <summary>
        /// Gets a value indicating the maximum allowed count of values for this property.
        /// </summary>
        internal int? MaxCount { get; }

        /// <summary>
        /// Gets a value indicating the minimum required count of values for this property.
        /// </summary>
        internal int? MinCount { get; }

        /// <summary>
        /// Gets the maximum permissible value, or null if no maximum.
        /// </summary>
        internal int? MaxInclusive { get; }

        /// <summary>
        /// Gets the minimum permissible value, or null if no minimim.
        /// </summary>
        internal int? MinInclusive { get; }

        /// <summary>
        /// Gets the maximum permissible length of a string, or null if no maximum.
        /// </summary>
        internal int? MaxLength { get; }

        /// <summary>
        /// Gets a regular expression that constrains the permissible values, or null if no pattern constraint.
        /// </summary>
        internal Regex Regex { get; }

        /// <summary>
        /// Gets a value indicating whether the property's value must be unique across the corresponding properties in all sibling objects.
        /// </summary>
        internal bool HasUniqueValue { get; }

        /// <summary>
        /// Gets the name of a property of which this property's value must be an instance.
        /// </summary>
        internal string InstanceProperty { get; }

        /// <summary>
        /// Gets a literal value that is required, or null if no requirement.
        /// </summary>
        internal object RequiredLiteral { get; }

        /// <summary>
        /// Determines whether two <c>DTSupplementalPropertyInfo</c> objects are not equal.
        /// </summary>
        /// <param name="x">One <c>DTSupplementalPropertyInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTSupplementalPropertyInfo</c> object to compare to the first.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(DTSupplementalPropertyInfo x, DTSupplementalPropertyInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return !ReferenceEquals(null, y);
            }

            return !x.Equals(y);
        }

        /// <summary>
        /// Determines whether two <c>DTSupplementalPropertyInfo</c> objects are equal.
        /// </summary>
        /// <param name="x">One <c>DTSupplementalPropertyInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTSupplementalPropertyInfo</c> object to compare to the first.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(DTSupplementalPropertyInfo x, DTSupplementalPropertyInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return ReferenceEquals(null, y);
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Determines whether the specified <c>DTSupplementalPropertyInfo</c> is equal to the current object.
        /// </summary>
        /// <param name="other">The other <c>DTSupplementalPropertyInfo</c> to compare against.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(DTSupplementalPropertyInfo other)
        {
            return !ReferenceEquals(null, other)
                && this.Type == other.Type
                && this.IsPlural == other.IsPlural
                && this.DictionaryKey == other.DictionaryKey;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object other)
        {
            return this.Equals((DTSupplementalPropertyInfo)other);
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            int hashCode = 0;

            unchecked
            {
                hashCode = (hashCode * 131) + (this.Type?.GetHashCode() ?? 0);
                hashCode = (hashCode * 131) + this.IsPlural.GetHashCode();
                hashCode = (hashCode * 131) + this.DictionaryKey.GetHashCode();
            }

            return hashCode;
        }
    }
}
