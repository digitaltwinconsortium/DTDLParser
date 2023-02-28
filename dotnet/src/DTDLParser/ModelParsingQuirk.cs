namespace DTDLParser
{
    using System;

    /// <summary>
    /// Enum <c>ModelParsingQuirk</c> defines quirks that may change the parser's behavior to admit models that are invalid in narrowly defined ways.
    /// Use of these quirks is strongly discouraged unless the effect and consequences are well understood.
    /// </summary>
    /// <remarks>
    /// To specify more than one quirk, use bitwise OR to combine quirk values.
    /// </remarks>
    [Flags]
    public enum ModelParsingQuirk
    {
        /// <summary>
        /// No quirk.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// The <c>TolerateSolecismsInParse</c> quirk tells the parser while parsing a submitted model to tolerate specific minor invalidities to support backward compatibility.
        /// Specifically, this quirk instructs the parser while parsing a submitted model...
        /// ...to permit defining DTDL elements whose identifiers have reserved prefixes,
        /// ...to treat a JSON object containing nothing but an @id property as though it were a JSON string whose value is the value of the @id property, and
        /// ...to ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.
        /// </summary>
        TolerateSolecismsInParse = 0x0001,

        /// <summary>
        /// The <c>TolerateSolecismsInResolve</c> quirk tells the parser during DTMI resolution to tolerate specific minor invalidities to support backward compatibility.
        /// Specifically, this quirk instructs the parser during DTMI resolution\...
        /// ...to permit defining DTDL elements whose identifiers have reserved prefixes,
        /// ...to treat a JSON object containing nothing but an @id property as though it were a JSON string whose value is the value of the @id property, and
        /// ...to ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.
        /// </summary>
        TolerateSolecismsInResolve = 0x0002,
    }
}
