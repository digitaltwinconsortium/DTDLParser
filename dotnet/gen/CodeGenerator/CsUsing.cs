namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# using statement.
    /// </summary>
    public class CsUsing : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsUsing"/> class.
        /// </summary>
        /// <param name="usingText">Text for the parenthesized portion of the using statement.</param>
        public CsUsing(string usingText)
            : base($"using ({usingText})")
        {
        }

        /// <summary>
        /// Add a line of text to the using body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsUsing Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
