namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# catch statement.
    /// </summary>
    public class CsCatch : CsScope
    {
        private IStatementAdder parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsCatch"/> class.
        /// </summary>
        /// <param name="catchText">Text for the parenthesized portion of the catch statement.</param>
        /// <param name="parent">The scope that is the parent of this <see cref="CsCatch"/> object.</param>
        public CsCatch(string catchText, IStatementAdder parent)
            : base($"catch ({catchText})")
        {
            this.parent = parent;

            this.SuppressBreak = true;
        }

        /// <summary>
        /// Add another C# catch statement following this catch.
        /// </summary>
        /// <param name="catchText">Text for the parenthesized portion of the catch statement.</param>
        /// <returns>The <see cref="CsCatch"/> object added.</returns>
        public CsCatch Catch(string catchText)
        {
            CsCatch csCatch = new CsCatch(catchText, this.parent);
            this.parent.AddStatement(csCatch);
            return csCatch;
        }

        /// <summary>
        /// Add a C# finally statement following this catch.
        /// </summary>
        /// <returns>The <see cref="CsFinally"/> object added.</returns>
        public CsFinally Finally()
        {
            CsFinally csFinally = new CsFinally();
            this.parent.AddStatement(csFinally);
            return csFinally;
        }

        /// <summary>
        /// Add a line of text to the catch body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsCatch Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
