namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A collection of partition DTDL types, which are the types that are outlined from a DTDL model.
    /// </summary>
    internal static partial class PartitionTypeCollection
    {
        private static readonly HashSet<string> PartitionTypeStrings;

        /// <summary>
        /// Gets a textual description of the types that are partition types.
        /// </summary>
        internal static string PartitionTypeDescription { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to the maximum allowed size of a partition in bytes.
        /// </summary>
        internal static Dictionary<int, int> PartitionMaxBytes { get; }

        /// <summary>
        /// Determines whether a list of strings includes a partition type string.
        /// </summary>
        /// <param name="types">The list of strings.</param>
        /// <returns>True if partition type.</returns>
        internal static bool IncludesPartitionType(List<string> types)
        {
            return types?.Any(t => PartitionTypeStrings.Contains(t)) ?? false;
        }
    }
}
