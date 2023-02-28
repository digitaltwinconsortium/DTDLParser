namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a constraint on or with respect to a sibling of an element.
    /// </summary>
    internal class SiblingConstraint
    {
        /// <summary>
        /// Gets or sets the name of a property whose value acts as a key to identify a sibling element.
        /// </summary>
        internal string KeyPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the ID of a supplemental property whose value refers to a sibling element by the sibling's key value.
        /// </summary>
        internal Dtmi KeyrefPropertyId { get; set; }

        /// <summary>
        /// Gets or sets a list of type identifiers, one of which is required for the sibling element.
        /// </summary>
        internal List<Dtmi> RequiredTypes { get; set; }

        /// <summary>
        /// Gets or sets a string describing the required types.
        /// </summary>
        internal string RequiredTypesString { get; set; }

        /// <summary>
        /// Gets or sets a type identifier that the sibling element is not allowed to have, or null to not specify a disallowed type.
        /// </summary>
        internal Dtmi DisallowedType { get; set; }

        /// <summary>
        /// Gets or sets the name of the disallowed type.
        /// </summary>
        internal string DisallowedTypeName { get; set; }

        /// <summary>
        /// Gets or sets the ID of a supplemental property whose value must be unique across all siblings that specify the same keyref value.
        /// </summary>
        internal Dtmi UniqueReference { get; set; }

        /// <summary>
        /// Gets or sets the ID of a supplemental property whose value is the ID of a supplemental property that must be present on a sibling element.
        /// </summary>
        internal Dtmi SupplementalPropertyPath { get; set; }
    }
}
