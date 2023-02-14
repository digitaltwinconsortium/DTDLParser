namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for storing a sequence of versions of a given DTDL or extension context.
    /// </summary>
    internal class ContextHistory
    {
        private readonly List<VersionedContext> versionedContexts;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextHistory"/> class.
        /// </summary>
        /// <param name="versionedContexts">A list of <see cref="VersionedContext"/> representing the history.</param>
        internal ContextHistory(List<VersionedContext> versionedContexts)
        {
            this.versionedContexts = versionedContexts;
            this.AvailableVersions = string.Join(", ", Helpers.SortUnique(versionedContexts.Select(vc => vc.MajorVersion)).Select(mv => mv.ToString()));
        }

        /// <summary>
        /// Gets a string representation of the version numbers of the context in the history.
        /// </summary>
        internal string AvailableVersions { get; private set; }

        /// <summary>
        /// Add a <see cref="VersionedContext"/> element to the context history.
        /// </summary>
        /// <param name="versionedContext">The context to add.</param>
        internal void AddVersion(VersionedContext versionedContext)
        {
            this.versionedContexts.Add(versionedContext);
            this.AvailableVersions += $", {versionedContext.MajorVersion}";
        }

        /// <summary>
        /// Get a term from a URI if defined in the context history.
        /// </summary>
        /// <param name="uri">The URI for which to get the term.</param>
        /// <param name="term">Out param to receive the term.</param>
        /// <returns>True if the URI is found in the context history.</returns>
        internal bool TryGetTerm(Uri uri, out string term)
        {
            foreach (VersionedContext versionedContext in this.versionedContexts)
            {
                if (versionedContext.TryGetTerm(uri, out term))
                {
                    return true;
                }
            }

            term = null;
            return false;
        }

        /// <summary>
        /// Check whether a term has a definition in the context history.
        /// </summary>
        /// <param name="term">The term to check.</param>
        /// <returns>True if the term has a definition in the context history.</returns>
        internal bool IsTermInContext(string term)
        {
            return this.versionedContexts.Any(vc => vc.TryGetUri(term, out Uri _) || vc.TryGetPrefix(term, out string _));
        }

        /// <summary>
        /// Get a context from the history that best matches the provided <paramref name="majorVersion"/> and <paramref name="minorVersion"/>, if available.
        /// </summary>
        /// <param name="majorVersion">The major version of the context to return; must be an exact match.</param>
        /// <param name="minorVersion">The minor version of the context to return; the best match is the greatest value not exceeding this value.</param>
        /// <param name="bestMatchContext">Out parameter to receive the best matching <see cref="VersionedContext"/>.</param>
        /// <returns>True if a matching context version is found in the history.</returns>
        internal bool TryGetMatchingContext(int majorVersion, int minorVersion, out VersionedContext bestMatchContext)
        {
            bestMatchContext = null;

            foreach (VersionedContext versionedContext in this.versionedContexts)
            {
                if (versionedContext.MajorVersion == majorVersion &&
                    versionedContext.MinorVersion <= minorVersion &&
                    (bestMatchContext == null || versionedContext.MinorVersion > bestMatchContext.MinorVersion))
                {
                    bestMatchContext = versionedContext;
                }
            }

            return bestMatchContext != null;
        }

        /// <summary>
        /// Gets a value indicating whether an identifier is restricted from being used in a model.
        /// </summary>
        /// <param name="id">The identifier to assess.</param>
        /// <returns>True if the identifier is restricted.</returns>
        internal bool IsIdentifierReserved(string id)
        {
            return this.versionedContexts.Any(vc => vc.IdDefinitionReservedPrefixes.Any(p => id.StartsWith(p)));
        }
    }
}
