namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>RootableTypeCollection</c> partial class.
    /// </summary>
    public class RootableTypeCollectionGenerator : ITypeGenerator
    {
        private readonly Dictionary<int, List<string>> rootableClasses;
        private readonly Dictionary<string, Dictionary<string, string>> contexts;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootableTypeCollectionGenerator"/> class.
        /// </summary>
        /// <param name="rootableClasses">A list of partition classes defined in the metamodel digest.</param>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        public RootableTypeCollectionGenerator(Dictionary<int, List<string>> rootableClasses, Dictionary<string, Dictionary<string, string>> contexts)
        {
            this.rootableClasses = rootableClasses;
            this.contexts = contexts;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass collectionClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "RootableTypeCollection", Multiplicity.Static, Completeness.Partial);
            collectionClass.Summary("A collection of rootable DTDL types, which are the types permitted at the top level of a DTDL model.");

            CsConstructor constructor = collectionClass.Constructor(Access.Implicit, Multiplicity.Static);

            constructor.Body.Line("RootableTypeStrings = new Dictionary<int, HashSet<string>>();");

            foreach (KeyValuePair<int, List<string>> kvp in this.rootableClasses)
            {
                constructor.Body.Break();
                constructor.Body.Line($"RootableTypeStrings[{kvp.Key}] = new HashSet<string>();");

                foreach (string typeName in kvp.Value)
                {
                    constructor.Body.Line($"RootableTypeStrings[{kvp.Key}].Add(\"{typeName}\");");
                }
            }

            constructor.Body.Break();
            constructor.Body.Line("RootableTypeDescriptions = new Dictionary<int, string>();");

            foreach (KeyValuePair<int, List<string>> kvp in this.rootableClasses)
            {
                constructor.Body.Line($"RootableTypeDescriptions[{kvp.Key}] = string.Join(\" or \", RootableTypeStrings[{kvp.Key}].Select(t => $\"'{{t}}'\"));");
            }

            foreach (KeyValuePair<int, List<string>> kvp in this.rootableClasses)
            {
                constructor.Body.Break();
                Dictionary<string, string> context = this.contexts[ParserGeneratorValues.GetDtdlContextIdString(kvp.Key)];
                foreach (string typeName in kvp.Value)
                {
                    constructor.Body.Line($"RootableTypeStrings[{kvp.Key}].Add(\"{context[typeName]}\");");
                }
            }
        }
    }
}
