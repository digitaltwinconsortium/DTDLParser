namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Indicates that one or more model errors were discovered during DTDL parsing.
    /// </summary>
    public class ParsingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class.
        /// </summary>
        /// <param name="errors">A list of the errors that were discovered.</param>
        internal ParsingException(IList<ParsingError> errors)
            : base("Parsing exception -- " + errors.Count + " errors in model -- see Errors property for details on each")
        {
            this.Errors = errors;
        }

        /// <summary>
        /// Gets a list of the errors that were discovered during DTDL parsing.
        /// </summary>
        /// <value>A list of <see cref="ParsingError"/>.</value>
        public IList<ParsingError> Errors { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Message + " -- first error includes message: " + this.Errors.First().Message;
        }
    }
}
