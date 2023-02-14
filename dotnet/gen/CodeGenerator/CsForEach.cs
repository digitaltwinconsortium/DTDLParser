namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# foreach statement.
    /// </summary>
    public class CsForEach : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsForEach"/> class.
        /// </summary>
        /// <param name="foreachText">Text for the parenthesized portion of the foreach statement.</param>
        /// <param name="withAwait">True if the foreach should be modified with an await.</param>
        public CsForEach(string foreachText, bool withAwait)
            : base($"{(withAwait ? "await " : string.Empty)}foreach ({foreachText})")
        {
        }

        /// <summary>
        /// Add a line of text to the foreach body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsForEach Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
