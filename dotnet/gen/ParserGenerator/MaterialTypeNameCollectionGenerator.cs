namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>MaterialTypeNameCollection</c> class.
    /// </summary>
    public class MaterialTypeNameCollectionGenerator : ITypeGenerator
    {
        private readonly List<string> typeNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialTypeNameCollectionGenerator"/> class.
        /// </summary>
        /// <param name="classNames">An enmeration of class names from the DTDL metamodel digest.</param>
        /// <param name="contexts">An enumeration of dictionaries of term definitions.</param>
        public MaterialTypeNameCollectionGenerator(IEnumerable<string> classNames, IEnumerable<Dictionary<string, string>> contexts)
        {
            this.typeNames = new List<string>();

            foreach (string className in classNames)
            {
                this.typeNames.Add(className);

                foreach (Dictionary<string, string> termDefinitions in contexts)
                {
                    if (termDefinitions.TryGetValue(className, out string typeId))
                    {
                        this.typeNames.Add(typeId);
                    }
                }
            }
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass collectionClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "MaterialTypeNameCollection", Multiplicity.Static);
            collectionClass.Summary("A collection of all material type names.");

            collectionClass.Field(Access.Private, "HashSet<string>", "TypeNames", "new HashSet<string>()", multiplicity: Multiplicity.Static, mutability: Mutability.ReadOnly);

            CsConstructor constructor = collectionClass.Constructor(Access.Implicit, Multiplicity.Static);

            foreach (string typeName in this.typeNames)
            {
                constructor.Body.Line($"TypeNames.Add(\"{typeName}\");");
            }

            CsMethod isMaterialTypeMethod = collectionClass.Method(Access.Internal, Novelty.Normal, ParserGeneratorValues.ObverseTypeBoolean, "IsMaterialType", Multiplicity.Static);
            isMaterialTypeMethod.Summary("Indicates whether a given type is material or supplemental.");
            isMaterialTypeMethod.Param(ParserGeneratorValues.ObverseTypeString, "typeString", "The type to check.");
            isMaterialTypeMethod.Returns("True if the type is material.");
            isMaterialTypeMethod.Body.Line("return TypeNames.Contains(typeString);");
        }
    }
}
