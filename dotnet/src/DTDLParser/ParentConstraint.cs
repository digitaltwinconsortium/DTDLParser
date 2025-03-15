namespace DTDLParser.Models
{
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
        /// Gets or sets a cotype identifier that the parent is required to have.
        /// </summary>
        public Dtmi RequiredParentCotype { get; set; }

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
