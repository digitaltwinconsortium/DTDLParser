namespace DTDLParser
{
    /// <summary>
    /// Mutability of a C# declaration.
    /// </summary>
    public enum Mutability
    {
        /// <summary>const.</summary>
        Constant,

        /// <summary>readonly.</summary>
        ReadOnly,

        /// <summary>Not const or readonly.</summary>
        Mutable,
    }
}
