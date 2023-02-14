namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# switch statement.
    /// </summary>
    public class CsSwitch : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsSwitch"/> class.
        /// </summary>
        /// <param name="switchText">Text for the parenthesized portion of the switch statement.</param>
        public CsSwitch(string switchText)
            : base($"switch ({switchText})")
        {
            this.DoubleIndent = true;
        }

        /// <summary>
        /// Add a case statement to the switch.
        /// </summary>
        /// <param name="value">Case value.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsSwitch Case(string value)
        {
            CsCase csCase = new CsCase(value);
            ((IStatementAdder)this).AddStatement(csCase);
            return this;
        }

        /// <summary>
        /// Add a default statement to the switch.
        /// </summary>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsSwitch Default()
        {
            CsDefault csDefault = new CsDefault();
            ((IStatementAdder)this).AddStatement(csDefault);
            return this;
        }

        /// <summary>
        /// Add a line of text to the switch body.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public new CsSwitch Line(string text)
        {
            base.Line(text);
            return this;
        }
    }
}
