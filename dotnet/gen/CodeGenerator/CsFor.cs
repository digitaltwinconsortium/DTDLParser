namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# for statement.
    /// </summary>
    public class CsFor : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsFor"/> class.
        /// </summary>
        /// <param name="forText">Text for the parenthesized portion of the for statement.</param>
        public CsFor(string forText)
            : base($"for ({forText})")
        {
        }

        /// <summary>
        /// Add a line of text to the for body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsFor Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
