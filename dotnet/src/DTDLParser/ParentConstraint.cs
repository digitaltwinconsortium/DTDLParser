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
        /// Gets or sets a string describing hte required cotype.
        /// </summary>
        public string RequiredParentCotypeString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the adjunct type must be on only one element in the values of the parent path.
        /// </summary>
        public bool AdjunctTypeIsUnique { get; set; }
    }
}
