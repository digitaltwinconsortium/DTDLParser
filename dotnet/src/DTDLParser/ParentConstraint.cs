namespace DTDLParser.Models
{
    using System.Collections.Generic;
    using DTDLParser;

    /// <summary>
    /// Class <c>ParentConstraint</c> provides information about constraints on a structural parent imposed by a supplemental type.
    /// </summary>
    internal struct ParentConstraint
    {
        /// <summary>
        /// Gets or sets the name of the property from the parent.
        /// </summary>
        public string ParentPropertyName { get; set; }

        /// <summary>
        /// Gets or sets a list of cotype identifiers, one of which the parent is required to have.
        /// </summary>
        public List<Dtmi> RequiredParentCotypes { get; set; }

        /// <summary>
        /// Gets or sets a string describing the required cotype.
        /// </summary>
        public string RequiredParentCotypeString { get; set; }

        /// <summary>
        /// Gets or sets a type identifier that no other instance among the property values is permitted to have.
        /// </summary>
        public Dtmi NoneOtherType { get; set; }

        /// <summary>
        /// Gets or sets a type identifier that some other instance among the property values is required to have.
        /// </summary>
        public Dtmi SomeOtherType { get; set; }
    }
}
