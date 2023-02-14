namespace DTDLParser
{
    /// <summary>
    /// Class for generating a series of alternatives in an if/elseif/.../elseif/else structure.
    /// </summary>
    public class AlternativeSeries
    {
        private CsScope outerScope;
        private CsIf conditionalScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlternativeSeries"/> class.
        /// </summary>
        /// <param name="outerScope">Scope in which to add the conditional scope.</param>
        public AlternativeSeries(CsScope outerScope)
        {
            this.outerScope = outerScope;
            this.conditionalScope = null;
        }

        /// <summary>
        /// Get a <see cref="CsScope"/> for a condition in the series.
        /// </summary>
        /// <param name="condition">Text for the parenthesized portion of an if or else-if statement.</param>
        /// <returns>A <see cref="CsScope"/> to which conditional code can be added.</returns>
        public CsScope GetScope(string condition)
        {
            if (this.conditionalScope != null)
            {
                if (condition != null)
                {
                    return this.conditionalScope.ElseIf(condition);
                }
                else
                {
                    return this.conditionalScope.Else();
                }
            }
            else
            {
                if (condition != null)
                {
                    this.conditionalScope = this.outerScope.If(condition);
                    return this.conditionalScope;
                }
                else
                {
                    return this.outerScope;
                }
            }
        }
    }
}
