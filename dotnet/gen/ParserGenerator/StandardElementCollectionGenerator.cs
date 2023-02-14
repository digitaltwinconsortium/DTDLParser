namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>StandardElementCollection</c> partial class.
    /// </summary>
    public class StandardElementCollectionGenerator : ITypeGenerator
    {
        private readonly string baseClassName;
        private readonly Dictionary<string, string> aliases;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandardElementCollectionGenerator"/> class.
        /// </summary>
        /// <param name="baseName">The base name for the parser's object model.</param>
        /// <param name="aliases">A dictionary that maps from alias class URI to the alias property URI for the class.</param>
        public StandardElementCollectionGenerator(string baseName, Dictionary<string, string> aliases)
        {
            this.baseClassName = NameFormatter.FormatNameAsClass(baseName);
            this.aliases = aliases;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass standardElementsClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "StandardElementCollection", completeness: Completeness.Partial);
            standardElementsClass.Summary("A collection of values of standard elements from the DTDL metamodel.");

            this.GeneratePopulateEndogenousAliasesMethod(standardElementsClass);
        }

        private void GeneratePopulateEndogenousAliasesMethod(CsClass standardElementsClass)
        {
            CsMethod method = standardElementsClass.Method(Access.Private, Novelty.Normal, "void", "PopulateEndogenousAliases", Multiplicity.Static);

            method.Body.Line("Dictionary<Dtmi, string> aliasTypePropertyMap = new Dictionary<Dtmi, string>();");
            foreach (KeyValuePair<string, string> kvp in this.aliases)
            {
                method.Body.Line($"aliasTypePropertyMap[new Dtmi(\"{kvp.Key}\")] = \"{kvp.Value}\";");
            }

            method.Body.Break();

            method.Body.Line("Dictionary<Dtmi, string> aliasElementPropertyMap = new Dictionary<Dtmi, string>();");
            method.Body.ForEach("var kvp in EndogenousStandardModel.Dict")
                .ForEach("Dtmi supplementalTypeId in kvp.Value.SupplementalTypes")
                    .If("aliasTypePropertyMap.TryGetValue(supplementalTypeId, out string propName)")
                        .Line("aliasElementPropertyMap[kvp.Key] = propName;")
                        .Line("break;");

            CsForEach forEachAlias = method.Body.ForEach("KeyValuePair<Dtmi, string> kvp in aliasElementPropertyMap");
            forEachAlias
                .Line("List<Dtmi> equivalentIds = new List<Dtmi>();")
                .Line("Dtmi fromId = kvp.Key;")
                .Line("Dtmi toId = null;");

            forEachAlias.While("aliasElementPropertyMap.TryGetValue(fromId, out string propName) && !EndogenousAliases.TryGetValue(fromId, out toId)")
                .Line("equivalentIds.Add(fromId);")
                .Line("fromId = ((DTEntityInfo)EndogenousStandardModel.Dict[fromId].SupplementalProperties[propName]).Id;");

            forEachAlias.ForEach("Dtmi equivalentId in equivalentIds")
                .Line("EndogenousAliases[equivalentId] = toId ?? fromId;");
        }
    }
}
