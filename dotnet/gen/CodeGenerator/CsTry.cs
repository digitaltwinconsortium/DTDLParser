namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# try statement.
    /// </summary>
    public class CsTry : CsScope
    {
        private IStatementAdder parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsTry"/> class.
        /// </summary>
        /// <param name="parent">The scope that is the parent of this <see cref="CsTry"/> object.</param>
        public CsTry(IStatementAdder parent)
            : base($"try")
        {
            this.parent = parent;
        }

        /// <summary>
        /// Add a C# catch statement following this try.
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
        /// Add a C# finally statement following this try.
        /// </summary>
        /// <returns>The <see cref="CsFinally"/> object added.</returns>
        public CsFinally Finally()
        {
            CsFinally csFinally = new CsFinally();
            this.parent.AddStatement(csFinally);
            return csFinally;
        }

        /// <summary>
        /// Add a line of text to the try body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsTry Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
