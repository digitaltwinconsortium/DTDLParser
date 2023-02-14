namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# if statement.
    /// </summary>
    public class CsIf : CsScope
    {
        private IStatementAdder parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsIf"/> class.
        /// </summary>
        /// <param name="ifText">Text for the parenthesized portion of the if statement.</param>
        /// <param name="parent">The scope that is the parent of this <see cref="CsIf"/> object.</param>
        public CsIf(string ifText, IStatementAdder parent)
            : base($"if ({ifText})")
        {
            this.parent = parent;
        }

        /// <summary>
        /// Add a C# else if statement following this if.
        /// </summary>
        /// <param name="elseIfText">Text for the parenthesized portion of the else if statement.</param>
        /// <returns>The <see cref="CsElseIf"/> object added.</returns>
        public CsElseIf ElseIf(string elseIfText)
        {
            CsElseIf csElseIf = new CsElseIf(elseIfText, this.parent);
            this.parent.AddStatement(csElseIf);
            return csElseIf;
        }

        /// <summary>
        /// Add a C# else statement following this if.
        /// </summary>
        /// <returns>The <see cref="CsElse"/> object added.</returns>
        public CsElse Else()
        {
            CsElse csElse = new CsElse();
            this.parent.AddStatement(csElse);
            return csElse;
        }

        /// <summary>
        /// Add a line of text to the if body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsIf Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
