namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>PartitionTypeCollection</c> partial class.
    /// </summary>
    public class PartitionTypeCollectionGenerator : ITypeGenerator
    {
        private readonly List<string> partitionClasses;
        private readonly Dictionary<string, Dictionary<string, string>> contexts;
        private readonly Dictionary<int, PartitionRestriction> partitionRestrictions;
        private readonly List<int> dtdlVersions;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartitionTypeCollectionGenerator"/> class.
        /// </summary>
        /// <param name="partitionClasses">A list of partition classes defined in the metamodel digest.</param>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        /// <param name="partitionRestrictions">A dictionary that maps from DTDL version to a <see cref="PartitionRestriction"/> object that describes restrictions on partition classes.</param>
        /// <param name="dtdlVersions">A list of DTDL (major) version numbers defined in the metamodel digest.</param>
        public PartitionTypeCollectionGenerator(
            List<string> partitionClasses,
            Dictionary<string, Dictionary<string, string>> contexts,
            Dictionary<int, PartitionRestriction> partitionRestrictions,
            List<int> dtdlVersions)
        {
            this.partitionClasses = partitionClasses;
            this.contexts = contexts;
            this.partitionRestrictions = partitionRestrictions;
            this.dtdlVersions = dtdlVersions;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass collectionClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "PartitionTypeCollection", Multiplicity.Static, Completeness.Partial);
            collectionClass.Summary("A collection of partition DTDL types, which are the types that are outlined from a DTDL model.");

            CsConstructor constructor = collectionClass.Constructor(Access.Implicit, Multiplicity.Static);

            constructor.Body.Line("PartitionTypeStrings = new HashSet<string>();");
            foreach (string typeName in this.partitionClasses)
            {
                constructor.Body.Line($"PartitionTypeStrings.Add(\"{typeName}\");");
            }

            constructor.Body.Break();
            constructor.Body.Line("PartitionTypeDescription = string.Join(\" or \", PartitionTypeStrings.Select(t => $\"'{t}'\"));");

            constructor.Body.Break();
            foreach (int dtdlVersion in this.dtdlVersions)
            {
                Dictionary<string, string> context = this.contexts[ParserGeneratorValues.GetDtdlContextIdString(dtdlVersion)];
                foreach (string typeName in this.partitionClasses)
                {
                    constructor.Body.Line($"PartitionTypeStrings.Add(\"{context[typeName]}\");");
                }
            }

            constructor.Body.Break();
            constructor.Body.Line("PartitionMaxBytes = new Dictionary<int, Dictionary<string, int>>();");
            foreach (KeyValuePair<int, PartitionRestriction> kvp in this.partitionRestrictions)
            {
                if (kvp.Value.MaxBytes != null)
                {
                    ValueLimiter.AssignLimitDictionary(constructor.Body, kvp.Value.MaxBytes, $"PartitionMaxBytes[{kvp.Key}]");
                }
            }
        }
    }
}
