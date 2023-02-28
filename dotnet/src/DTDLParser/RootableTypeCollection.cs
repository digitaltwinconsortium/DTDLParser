namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A collection of rootable DTDL types, which are the types permitted at the top level of a DTDL model.
    /// </summary>
    internal static partial class RootableTypeCollection
    {
        private const string TypeKeyword = "@type";

        private static readonly Dictionary<int, HashSet<string>> RootableTypeStrings;

        /// <summary>
        /// Gets a textual description of the types that are rootable types.
        /// </summary>
        internal static Dictionary<int, string> RootableTypeDescriptions { get; }

        /// <summary>
        /// Determines whether a list of strings includes a rootable type string.
        /// </summary>
        /// <param name="types">The list of strings.</param>
        /// <param name="dtdlVersion">The version of DTDL in which the object is defined.</param>
        /// <returns>True if rootable type.</returns>
        internal static bool IncludesRootableType(List<string> types, int dtdlVersion)
        {
            return RootableTypeStrings.TryGetValue(dtdlVersion, out HashSet<string> rootableTypeStrings) && (types?.Any(t => rootableTypeStrings.Contains(t)) ?? false);
        }
    }
}
