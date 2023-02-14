namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# else statement.
    /// </summary>
    public class CsElse : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsElse"/> class.
        /// </summary>
        public CsElse()
            : base("else")
        {
            this.SuppressBreak = true;
        }

        /// <summary>
        /// Add a line of text to the else body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsElse Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
