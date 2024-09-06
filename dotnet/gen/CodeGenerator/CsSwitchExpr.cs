namespace DTDLParser
{
    /// <summary>
    /// Generator for a C# switch expression.
    /// </summary>
    public class CsSwitchExpr : CsScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsSwitchExpr"/> class.
        /// </summary>
        /// <param name="expressionText">Text for the expression preceding the 'switch' keyword.</param>
        /// <param name="terminate">True if the expression should be terminated with a semicolon; defaults to true.</param>
        public CsSwitchExpr(string expressionText, bool terminate = true)
            : base($"{expressionText} switch", terminate)
        {
        }

        /// <summary>
        /// Add an expression arm to the switch expression.
        /// </summary>
        /// <param name="pattern">Match expression.</param>
        /// <param name="value">Value when matched.</param>
        /// <param name="caseGuard">Case guard for arm.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsSwitchExpr Arm(string pattern, string value, string caseGuard = null)
        {
            if (caseGuard != null)
            {
                this.Line($"{pattern} when {caseGuard} => {value},");
            }
            else
            {
                this.Line($"{pattern} => {value},");
            }

            return this;
        }
    }
}
