namespace DTDLParser
{
    /// <summary>
    /// Tolerability conditions for apocryphal terms and URIs in models.
    /// </summary>
    public enum ApocryphaTolerance
    {
        /// <summary>Item is always tolerated in the given role, even in the absence of an unknown context specifier.</summary>
        Valid,

        /// <summary>Item is never tolerated in the given role, even in the presence of an unknown context specifier.</summary>
        Invalid,

        /// <summary>Item is tolerated in the given role only in the presence of an unknown context specifier.</summary>
        Tenable,
    }
}
