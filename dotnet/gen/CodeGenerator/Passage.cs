namespace DTDLParser
{
    /// <summary>
    /// Means for passing a parameter in a C# method.
    /// </summary>
    public enum Passage
    {
        /// <summary>No explicit designation, normal inbound parameter.</summary>
        In,

        /// <summary>'out' designation for outbound paramater.</summary>
        Out,

        /// <summary>'ref' designation for reference parameter.</summary>
        Reference,
    }
}
