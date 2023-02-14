namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Indicates that the resolution of a DTMI failed.
    /// </summary>
    /// <remarks>
    /// If, when parsing a model, the <see cref="ModelParser"/> encounters a dependent reference to an identifier that lacks a definition, it attempts to obtain a definition from a registered <see cref="DtmiResolver"/> or <see cref="DtmiResolverAsync"/>.
    /// This situation can trigger the present exception in one of three circumstances.
    /// First, if the parsing is synchronous and no <see cref="DtmiResolver"/> is registered, or if the parsing is asynchronous and no <see cref="DtmiResolverAsync"/> is registered.
    /// Second, if the regestered resolver returns a value of null, indicating that it is unable to obtain the requested definition.
    /// Third, if the registered resolver returns a set of definitions, but the set does not include a definition for one or more of the identfiers passed to the resolver.
    /// </remarks>
    public class ResolutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="message">An explanation of the failure.</param>
        /// <param name="undefinedIdentifiers">Dictionary that maps from each undefined DTMI to a <see cref="DtmiReferenceTracker"/> providing reference source location info.</param>
        /// <param name="itemizeIdentifers">Boolean value indicating whether to add an itemized list of identifiers to the exception message.</param>
        internal ResolutionException(string message, Dictionary<Dtmi, DtmiReferenceTracker> undefinedIdentifiers = null, bool itemizeIdentifers = false)
            : base(message + (itemizeIdentifers ? string.Join(", ", undefinedIdentifiers.Values) : string.Empty))
        {
            if (undefinedIdentifiers != null)
            {
                this.UndefinedIdentifiers = undefinedIdentifiers.Keys.ToList();

                this.UndefinedIdentifierReferences = new List<DtmiReference>();
                foreach (DtmiReferenceTracker referenceTracker in undefinedIdentifiers.Values)
                {
                    this.UndefinedIdentifierReferences.AddRange(referenceTracker.GetDtmiReferences());
                }
            }
            else
            {
                this.UndefinedIdentifiers = new List<Dtmi>();
                this.UndefinedIdentifierReferences = new List<DtmiReference>();
            }
        }

        /// <summary>
        /// Gets a list of DTMIs that lack definitions and require resolution.
        /// </summary>
        /// <value>A list of undefined DTMIs.</value>
        /// <remarks>
        /// If the registered resolver returns definitions for some but not all of the requested identifiers, this property lists only the identifiers that still lack definitions after the resolver returns.
        /// </remarks>
        public List<Dtmi> UndefinedIdentifiers { get; }

        /// <summary>
        /// Gets a list of <see cref="DtmiReference"/> values that each describe a reference to an undefined DTMI in the <see cref="UndefinedIdentifiers"/> list.
        /// </summary>
        /// <value>A list of <see cref="DtmiReference"/> values.</value>
        public List<DtmiReference> UndefinedIdentifierReferences { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Message;
        }
    }
}
