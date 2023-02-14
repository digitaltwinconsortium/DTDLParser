namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# while statement.
    /// </summary>
    public class CsWhile : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsWhile"/> class.
        /// </summary>
        /// <param name="whileText">Text for the parenthesized portion of the while statement.</param>
        public CsWhile(string whileText)
            : base($"while ({whileText})")
        {
        }

        /// <summary>
        /// Add a line of text to the while body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsWhile Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
