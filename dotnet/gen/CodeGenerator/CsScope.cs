namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Generator for a C# code statement with an initial line of code followed by brace-enclosed lines.
    /// </summary>
    public class CsScope : CsStatement, IStatementAdder
    {
        private string headText;
        private bool terminate;
        private List<CsStatement> csStatements;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsScope"/> class.
        /// </summary>
        /// <param name="headText">Text for the first line of the statement.</param>
        /// <param name="terminate">True if the scope should be terminated with a semicolon; defaults to false.</param>
        public CsScope(string headText, bool terminate = false)
        {
            this.headText = headText;
            this.terminate = terminate;
            this.csStatements = new List<CsStatement>();

            this.SuppressBreak = false;
            this.DoubleIndent = false;
        }

        /// <inheritdoc/>
        internal override string SortingText
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the blank line preceding the scope should be suppressed.
        /// </summary>
        protected bool SuppressBreak { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the scope should be doubly indented instead of singly indented.
        /// </summary>
        protected bool DoubleIndent { get; set; }

        /// <summary>
        /// Add a line of text to the scope.
        /// </summary>
        /// <param name="text">Text to add.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsScope Line(string text)
        {
            this.csStatements.Add(new CsLine(text));
            return this;
        }

        /// <summary>
        /// Add a multiple-line statement to the scope.
        /// </summary>
        /// <param name="subHeadText">Text for the first line of the statement.</param>
        /// <returns>The <see cref="CsMultiLine"/> object added.</returns>
        public CsMultiLine MultiLine(string subHeadText)
        {
            CsMultiLine csMultiLine = new CsMultiLine(subHeadText);
            this.csStatements.Add(csMultiLine);
            return csMultiLine;
        }

        /// <summary>
        /// Add to the scope a sequence of statements that will be lexicographically sorted at code-generation time.
        /// </summary>
        /// <returns>The <see cref="CsSorted"/> object added.</returns>
        public CsSorted Sorted()
        {
            CsSorted csSorted = new CsSorted();
            this.csStatements.Add(csSorted);
            return csSorted;
        }

        /// <summary>
        /// Add a nested scope to the scope.
        /// </summary>
        /// <param name="subHeadText">Text for the first line of the nested scope.</param>
        /// <param name="terminate">True if the scope should be terminated with a semicolon; defaults to false.</param>
        /// <returns>The <see cref="CsScope"/> object added.</returns>
        public CsScope Scope(string subHeadText, bool terminate = false)
        {
            CsScope csScope = new CsScope(subHeadText, terminate);
            this.csStatements.Add(csScope);
            return csScope;
        }

        /// <summary>
        /// Add a C# while statement to the scope.
        /// </summary>
        /// <param name="whileText">Text for the parenthesized portion of the while statement.</param>
        /// <returns>The <see cref="CsWhile"/> object added.</returns>
        public CsWhile While(string whileText)
        {
            CsWhile csWhile = new CsWhile(whileText);
            this.csStatements.Add(csWhile);
            return csWhile;
        }

        /// <summary>
        /// Add a C# for statement to the scope.
        /// </summary>
        /// <param name="forText">Text for the parenthesized portion of the for statement.</param>
        /// <returns>The <see cref="CsFor"/> object added.</returns>
        public CsFor For(string forText)
        {
            CsFor csFor = new CsFor(forText);
            this.csStatements.Add(csFor);
            return csFor;
        }

        /// <summary>
        /// Add a C# foreach statement to the scope.
        /// </summary>
        /// <param name="foreachText">Text for the parenthesized portion of the foreach statement.</param>
        /// <param name="withAwait">True if the foreach should be modified with an await.</param>
        /// <returns>The <see cref="CsForEach"/> object added.</returns>
        public CsForEach ForEach(string foreachText, bool withAwait = false)
        {
            CsForEach csForEach = new CsForEach(foreachText, withAwait);
            this.csStatements.Add(csForEach);
            return csForEach;
        }

        /// <summary>
        /// Add a C# if statement to the scope.
        /// </summary>
        /// <param name="ifText">Text for the parenthesized portion of the if statement.</param>
        /// <returns>The <see cref="CsIf"/> object added.</returns>
        public CsIf If(string ifText)
        {
            CsIf csIf = new CsIf(ifText, this);
            this.csStatements.Add(csIf);
            return csIf;
        }

        /// <summary>
        /// Add a C# switch statement to the scope.
        /// </summary>
        /// <param name="switchText">Text for the parenthesized portion of the switch statement.</param>
        /// <returns>The <see cref="CsSwitch"/> object added.</returns>
        public CsSwitch Switch(string switchText)
        {
            CsSwitch csSwitch = new CsSwitch(switchText);
            this.csStatements.Add(csSwitch);
            return csSwitch;
        }

        /// <summary>
        /// Add a C# switch expression to the scope.
        /// </summary>
        /// <param name="expressionText">Text for the expression preceding the 'switch' keyword.</param>
        /// <param name="terminate">True if the expression should be terminated with a semicolon; defaults to true.</param>
        /// <returns>The <see cref="CsSwitchExpr"/> object added.</returns>
        public CsSwitchExpr SwitchExpr(string expressionText, bool terminate = true)
        {
            CsSwitchExpr csSwitchExpr = new CsSwitchExpr(expressionText, terminate);
            this.csStatements.Add(csSwitchExpr);
            return csSwitchExpr;
        }

        /// <summary>
        /// Add a C# try statement to the scope.
        /// </summary>
        /// <returns>The <see cref="CsTry"/> object added.</returns>
        public CsTry Try()
        {
            CsTry csTry = new CsTry(this);
            this.csStatements.Add(csTry);
            return csTry;
        }

        /// <summary>
        /// Add a C# using statement to the scope.
        /// </summary>
        /// <param name="usingText">Text for the parenthesized portion of the using statement.</param>
        /// <returns>The <see cref="CsUsing"/> object added.</returns>
        public CsUsing Using(string usingText)
        {
            CsUsing csUsing = new CsUsing(usingText);
            this.csStatements.Add(csUsing);
            return csUsing;
        }

        /// <summary>
        /// Add a break between statements.
        /// </summary>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsScope Break()
        {
            this.csStatements.Add(new CsBreak());
            return this;
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            if (this.headText != null)
            {
                codeWriter.WriteLine(this.headText, suppressBreak: this.SuppressBreak);
            }

            codeWriter.OpenScope();

            if (this.DoubleIndent)
            {
                codeWriter.IncreaseIndent();
            }

            foreach (CsStatement csStatement in this.csStatements)
            {
                csStatement.GenerateCode(codeWriter);
            }

            if (this.DoubleIndent)
            {
                codeWriter.DecreaseIndent();
            }

            codeWriter.CloseScope(this.terminate);
        }

        /// <inheritdoc/>
        void IStatementAdder.AddStatement(CsStatement csStatement)
        {
            this.csStatements.Add(csStatement);
        }
    }
}
