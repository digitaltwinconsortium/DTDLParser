namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# finally statement.
    /// </summary>
    public class CsFinally : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsFinally"/> class.
        /// </summary>
        public CsFinally()
            : base("finally")
        {
            this.SuppressBreak = true;
        }

        /// <summary>
        /// Add a line of text to the finally body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsFinally Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
