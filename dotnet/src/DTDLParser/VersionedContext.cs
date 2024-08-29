namespace DTDLParser
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for holding information from DTDL or extension context definitions.
    /// </summary>
    internal class VersionedContext
    {
        private Dictionary<string, Uri> termDict;
        private Dictionary<string, string> prefixDict;
        private Dictionary<Uri, string> reverseTermDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionedContext"/> class.
        /// </summary>
        /// <param name="contextSpecifer">The JSON-LD specifier for the context.</param>
        /// <param name="majorVersion">The major version of the context.</param>
        /// <param name="minorVersion">The minor version of the context.</param>
        /// <param name="limitsDtdlVersion">The version of DTDL for which this context defines limits, or zero if not extension or no limits defined.</param>
        /// <param name="limitSpec">A string to use for referencing the limits defined by this context, or null if not extension or no limits defined.</param>
        /// <param name="mergeDefinitions">An indication of whether definitions whose identifiers contain IRI fragments should be merged.</param>
        internal VersionedContext(string contextSpecifer, int majorVersion, int minorVersion, int limitsDtdlVersion, string limitSpec, bool mergeDefinitions = false)
        {
            this.termDict = new Dictionary<string, Uri>();
            this.prefixDict = new Dictionary<string, string>();
            this.reverseTermDict = new Dictionary<Uri, string>();

            this.ContextSpecifier = contextSpecifer;
            this.MajorVersion = majorVersion;
            this.MinorVersion = minorVersion;
            this.LimitsDtdlVersion = limitsDtdlVersion;
            this.LimitSpec = limitSpec;
            this.MergeDefinitions = mergeDefinitions;
            this.MergeableTypeIds = new List<Dtmi>();
            this.IdDefinitionReservedPrefixes = new List<string>();
        }

        /// <summary>
        /// Gets the JSON-LD specfier for the context.
        /// </summary>
        internal string ContextSpecifier { get; }

        /// <summary>
        /// Gets the major version of the context.
        /// </summary>
        internal int MajorVersion { get; }

        /// <summary>
        /// Gets the minor version of the context.
        /// </summary>
        internal int MinorVersion { get; }

        /// <summary>
        /// Gets the version of DTDL for which this context defines limits, or zero if not extension or no limits defined.
        /// </summary>
        internal int LimitsDtdlVersion { get; }

        /// <summary>
        /// Gets a string to use for referencing the limits defined by this context, or null if not extension or no limits defined.
        /// </summary>
        internal string LimitSpec { get; }

        /// <summary>
        /// Gets a value indicating whether definitions whose identifiers contain IRI fragments should be merged.
        /// </summary>
        internal bool MergeDefinitions { get; }

        /// <summary>
        /// Gets a list of type IDs of for types that are marked as mergeable.
        /// </summary>
        internal List<Dtmi> MergeableTypeIds { get; }

        /// <summary>
        /// Gets a list of prefixes that ID definitions in models are restricted from using.
        /// </summary>
        internal List<string> IdDefinitionReservedPrefixes { get; }

        /// <summary>
        /// Add a term definition to the context.
        /// </summary>
        /// <param name="term">A string representing the term to define.</param>
        /// <param name="uri">A <c>URI</c> to which the term should be mapped.</param>
        /// <param name="isMergeableType">Indicates whether the IRI defined by the term is a type that is mergeable.</param>
        internal void AddTermDefinition(string term, Uri uri, bool isMergeableType = false)
        {
            this.termDict[term] = uri;
            this.reverseTermDict[uri] = term;

            if (isMergeableType)
            {
                this.MergeableTypeIds.Add(uri as Dtmi);
            }
        }

        /// <summary>
        /// Add a prefix definition to the context.
        /// </summary>
        /// <param name="term">A string representing the term prefix to define.</param>
        /// <param name="prefix">A string prefix to which the term should be mapped.</param>
        internal void AddPrefixDefinition(string term, string prefix)
        {
            this.prefixDict[term] = prefix;
        }

        /// <summary>
        /// Add a DTMI prefix to the set of prefixes that ID definitions in models are restricted from using.
        /// </summary>
        /// <param name="prefix">The string prefix to reserve.</param>
        internal void ReserveIdDefinitionPrefix(string prefix)
        {
            this.IdDefinitionReservedPrefixes.Add(prefix);
        }

        /// <summary>
        /// Get a <c>Uri</c> object given a term putatively defined in the context.
        /// </summary>
        /// <param name="term">A term defined in the context.</param>
        /// <param name="uri">Out param to receive the <c>Uri</c>.</param>
        /// <returns>True if the term is found in the context.</returns>
        internal bool TryGetUri(string term, out Uri uri)
        {
            return this.termDict.TryGetValue(term, out uri);
        }

        /// <summary>
        /// Get an IRI prefix given a term prefix putatively defined in the context.
        /// </summary>
        /// <param name="term">A term prefix defined in the context.</param>
        /// <param name="prefix">Out param to receive the prefix string.</param>
        /// <returns>True if the term prefix is found in the context.</returns>
        internal bool TryGetPrefix(string term, out string prefix)
        {
            return this.prefixDict.TryGetValue(term, out prefix);
        }

        /// <summary>
        /// Get a term from a URI if defined in the context.
        /// </summary>
        /// <param name="uri">The URI for which to get the term.</param>
        /// <param name="term">Out param to receive the term.</param>
        /// <returns>True if the URI is found in the context.</returns>
        internal bool TryGetTerm(Uri uri, out string term)
        {
            return this.reverseTermDict.TryGetValue(uri, out term);
        }
    }
}
