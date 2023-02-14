namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Class describing a constraint on a value.
    /// </summary>
    internal class ValueConstraint
    {
        /// <summary>
        /// Gets or sets a list of type identifiers, one of which is required for the element.
        /// </summary>
        internal List<Dtmi> RequiredTypes { get; set; }

        /// <summary>
        /// Gets or sets a string describing the required types.
        /// </summary>
        internal string RequiredTypesString { get; set; }

        /// <summary>
        /// Gets or sets a list of value identifiers, one of which is required for the element.
        /// </summary>
        internal List<Dtmi> RequiredValues { get; set; }

        /// <summary>
        /// Gets or sets a string describing the required values.
        /// </summary>
        internal string RequiredValuesString { get; set; }

        /// <summary>
        /// Gets or sets a literal value that is required.
        /// </summary>
        internal object RequiredLiteral { get; set; }

        /// <summary>
        /// Gets or sets the name of a property whose value acts as a key to identify a sibling element.
        /// </summary>
        internal string SiblingKeyPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the ID of a supplemental property whose value refers to a sibling element by the sibling's key value.
        /// </summary>
        internal Dtmi SiblingKeyrefPropertyId { get; set; }

        /// <summary>
        /// Gets or sets the ID of a supplemental property whose value is the ID of a supplemental property on a sibling element whose class constraint acts as a required type for the element.
        /// </summary>
        internal Dtmi SiblingClassOfPropertyId { get; set; }

        /// <summary>
        /// Gets or sets the ID of a supplemental property whose value is the ID of a supplemental property on a sibling element whose parent constraint acts as a required parent for the element.
        /// </summary>
        internal Dtmi SiblingParentOfPropertyId { get; set; }
    }
}
