namespace DTDLParser
{
    using System;

    /// <summary>
    /// Indicates violation of StyleCop.Analyzers rules.
    /// </summary>
    public class StyleException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StyleException"/> class.
        /// </summary>
        /// <param name="message">An explanation of the failure.</param>
        public StyleException(string message)
            : base(message)
        {
        }
    }
}
