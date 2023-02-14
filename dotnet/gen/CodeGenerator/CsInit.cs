namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# init accessor.
    /// </summary>
    public class CsInit : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsInit"/> class.
        /// </summary>
        public CsInit()
            : base("init")
        {
        }

        /// <summary>
        /// Add a line of text to the init body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsInit Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
