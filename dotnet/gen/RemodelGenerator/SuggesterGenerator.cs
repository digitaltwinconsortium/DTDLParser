namespace DTDLParser
{
    /// <summary>
    /// Code generator for <c>Suggester</c> class used in <c>Remodel</c> tool.
    /// </summary>
    public class SuggesterGenerator
    {
        private ApocryphaTolerance cotypeUndefinedTermTolerance;
        private ApocryphaTolerance propertyUndefinedTermTolerance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggesterGenerator"/> class.
        /// </summary>
        /// <param name="apocryphaDigest">A <see cref="ApocryphaDigest"/> containing information about apocrypha for the lastest version of DTDL.</param>
        public SuggesterGenerator(ApocryphaDigest apocryphaDigest)
        {
            this.cotypeUndefinedTermTolerance = apocryphaDigest.UndefinedTermAsCotype;
            this.propertyUndefinedTermTolerance = apocryphaDigest.UndefinedTermAsProperty;
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass suggesterClass = parserLibrary.Class(Access.Public, Novelty.Normal, "Suggester", completeness: Completeness.Partial);
            suggesterClass.Summary("Partial class for suggesting IRI/term substitutions for contexts, types, and properties.");

            this.GenerateGetSuggestionsMethod(suggesterClass, "Type", this.cotypeUndefinedTermTolerance);
            this.GenerateGetSuggestionsMethod(suggesterClass, "Prop", this.propertyUndefinedTermTolerance);
        }

        private void GenerateGetSuggestionsMethod(CsClass suggesterClass, string suggestionKind, ApocryphaTolerance undefinedTermTolerance)
        {
            CsMethod method = suggesterClass.Method(Access.Private, Novelty.Normal, "Dictionary<string, string>", $"Get{suggestionKind}Suggestions");
            method.Param("HashSet<string>", "disallowedItems");

            string suggestion =
                undefinedTermTolerance == ApocryphaTolerance.Invalid ? "null" :
                $"\"{suggestionKind}_\" + string.Concat(disallowedItem.Where(c => char.IsLetterOrDigit(c)))";

            method.Body
                .Line("Dictionary<string, string> suggestions = new Dictionary<string, string>();")
                .ForEach("string disallowedItem in disallowedItems")
                    .Line($"suggestions[disallowedItem] = {suggestion};");

            method.Body.Line("return suggestions;");
        }
    }
}
