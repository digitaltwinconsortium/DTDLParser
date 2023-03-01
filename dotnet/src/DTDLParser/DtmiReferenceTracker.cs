namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Holds an external reference to a DTMI and information for determining locations of references within a DTDL model.
    /// </summary>
    internal class DtmiReferenceTracker
    {
        private Dtmi identifier;
        private List<DtmiReferenceLocator> referenceLocators;

        /// <summary>
        /// Initializes a new instance of the <see cref="DtmiReferenceTracker"/> class.
        /// </summary>
        /// <param name="identifier">Referenced DTMI that lacks a definition.</param>
        internal DtmiReferenceTracker(Dtmi identifier)
        {
            this.identifier = identifier;
            this.referenceLocators = new List<DtmiReferenceLocator>();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            List<string> referenceStrings = new List<string>();
            int nonSourcedCount = 0;
            foreach (DtmiReferenceLocator referenceLocator in this.referenceLocators)
            {
                if (referenceLocator.TryGetString(out string refString))
                {
                    referenceStrings.Add(refString);
                }
                else
                {
                    ++nonSourcedCount;
                }
            }

            if (referenceStrings.Any())
            {
                string othersString =
                    nonSourcedCount == 0 ? string.Empty :
                    nonSourcedCount == 1 ? ", and in 1 other place" :
                    $", and in {nonSourcedCount} other places";
                return $"{this.identifier} (referenced {string.Join(", ", referenceStrings)}{othersString})";
            }
            else
            {
                return $"{this.identifier} (referenced in {nonSourcedCount} {(nonSourcedCount == 1 ? "place" : "places")})";
            }
        }

        /// <summary>
        /// Add a reference to the identifier.
        /// </summary>
        /// <param name="property">A <see cref="JsonLdProperty"/> whose value is an undefined DTMI.</param>
        /// <param name="referencedId">A string reperesenation of the undefined DTMI as used in the DTDL model source.</param>
        internal void AddReference(JsonLdProperty property, string referencedId)
        {
            this.referenceLocators.Add(new DtmiReferenceLocator(property, referencedId));
        }

        /// <summary>
        /// Get a list of <see cref="DtmiReference"/> values.
        /// </summary>
        /// <returns>The list.</returns>
        internal List<DtmiReference> GetDtmiReferences()
        {
            List<DtmiReference> dtmiReferences = new List<DtmiReference>();

            foreach (DtmiReferenceLocator referenceLocator in this.referenceLocators)
            {
                if (referenceLocator.TrySourceDtmiReference(this.identifier, out DtmiReference dtmiReference))
                {
                    dtmiReferences.Add(dtmiReference);
                }
            }

            return dtmiReferences;
        }
    }
}
