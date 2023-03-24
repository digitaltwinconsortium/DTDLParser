namespace DTDLParser
{
    using System;

    /// <summary>
    /// Enum <c>WhenToAllow</c> is a value type for a property that indicates when a particular syntax, function, behavior, or feature indicated by the property is allowed to be used in a DTDL model.
    /// </summary>
    public enum WhenToAllow
    {
        /// <summary>
        /// The syntax, function, behavior, or feature is allowed or disallowed per the default specified by the metamodel for the DTDL language version.
        /// The defaults may be different for different language versions.
        /// </summary>
        PerDefault = 0,

        /// <summary>
        /// The syntax, function, behavior, or feature is always allowed for every model written in any DTDL language version.
        /// </summary>
        Always = 1,

        /// <summary>
        /// The syntax, function, behavior, or feature is never allowed for any model written in any DTDL language version.
        /// </summary>
        Never = 2,
    }
}
