namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# get accessor.
    /// </summary>
    public class CsGet : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsGet"/> class.
        /// </summary>
        public CsGet()
            : base("get")
        {
        }

        /// <summary>
        /// Add a line of text to the get body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsGet Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
