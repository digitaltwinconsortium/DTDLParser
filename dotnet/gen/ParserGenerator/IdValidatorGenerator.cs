namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Code generator for <c>IdValidator</c> class.
    /// </summary>
    public class IdValidatorGenerator : ITypeGenerator
    {
        private readonly Dictionary<int, StringRestriction> identifierDefinitionRestrictions;
        private readonly Dictionary<int, StringRestriction> identifierReferenceRestrictions;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdValidatorGenerator"/> class.
        /// </summary>
        /// <param name="identifierDefinitionRestrictions">A dictionary that maps from DTDL version to a <see cref="StringRestriction"/> object that describes restrictions on identifiers used in element definitions.</param>
        /// <param name="identifierReferenceRestrictions">A a dictionary that maps from DTDL version to a <see cref="StringRestriction"/> object that describes restrictions on identifiers used in element references.</param>
        public IdValidatorGenerator(Dictionary<int, StringRestriction> identifierDefinitionRestrictions, Dictionary<int, StringRestriction> identifierReferenceRestrictions)
        {
            this.identifierDefinitionRestrictions = identifierDefinitionRestrictions;
            this.identifierReferenceRestrictions = identifierReferenceRestrictions;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass restrictionsClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "IdValidator", Multiplicity.Static, Completeness.Partial);
            restrictionsClass.Summary("A static class for determining whether a string is a valid identifier.");

            CsConstructor constructor = restrictionsClass.Constructor(Access.Implicit, Multiplicity.Static);

            foreach (KeyValuePair<int, StringRestriction> kvp in this.identifierDefinitionRestrictions)
            {
                if (kvp.Value.MaxLength != null)
                {
                    ValueLimiter.AssignLimitDictionary(constructor.Body, kvp.Value.MaxLength, $"IdDefinitionMaxLengths[{kvp.Key}]");
                }

                if (kvp.Value.Pattern != null)
                {
                    constructor.Body.Line($"IdDefinitionRegexPatterns[{kvp.Key}] = new Regex(@\"{kvp.Value.Pattern}\", RegexOptions.Compiled);");
                }
            }

            constructor.Body.Break();

            foreach (KeyValuePair<int, StringRestriction> kvp in this.identifierReferenceRestrictions)
            {
                if (kvp.Value.MaxLength != null)
                {
                    ValueLimiter.AssignLimitDictionary(constructor.Body, kvp.Value.MaxLength, $"IdReferenceMaxLengths[{kvp.Key}]");
                }

                if (kvp.Value.Pattern != null)
                {
                    constructor.Body.Line($"IdReferenceRegexPatterns[{kvp.Key}] = new Regex(@\"{kvp.Value.Pattern}\", RegexOptions.Compiled);");
                }
            }
        }
    }
}
