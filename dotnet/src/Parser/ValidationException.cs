namespace DTDLParser
{
    using System;

    /// <summary>
    /// A call to the <c>ValidateInstance()</c> method was made on a class for which instance validation is not meaningful.
    /// </summary>
    /// <remarks>
    /// This exception indicates a usage error.
    /// Every concrete class that does not support instance validation always throws this exception on a call to <c>ValidateInstance()</c>.
    /// No concrete class that support instance validation ever throws this exception.
    /// </remarks>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="typeName">The DTDL type corresponding to the obverse class on which ValidateInstance() was called.</param>
        internal ValidationException(string typeName)
            : base($"The ValidateInstance() method was called on an object representing the DTDL {typeName} type, but validation is not meaningful for this type.")
        {
        }
    }
}
