namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# set accessor.
    /// </summary>
    public class CsSet : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsSet"/> class.
        /// </summary>
        public CsSet()
            : base("set")
        {
        }

        /// <summary>
        /// Add a line of text to the set body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsSet Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
