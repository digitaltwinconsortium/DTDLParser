/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    using DTDLParser.Models;

    /// <summary>
    /// Partial class for suggesting IRI/term substitutions for contexts, types, and properties.
    /// </summary>
    public partial class Suggester
    {
        private Dictionary<string, string> GetPropSuggestions(HashSet<string> disallowedItems)
        {
            Dictionary<string, string> suggestions = new Dictionary<string, string>();
            foreach (string disallowedItem in disallowedItems)
            {
                suggestions[disallowedItem] = "Prop_" + string.Concat(disallowedItem.Where(c => char.IsLetterOrDigit(c)));
            }

            return suggestions;
        }

        private Dictionary<string, string> GetTypeSuggestions(HashSet<string> disallowedItems)
        {
            Dictionary<string, string> suggestions = new Dictionary<string, string>();
            foreach (string disallowedItem in disallowedItems)
            {
                suggestions[disallowedItem] = "Type_" + string.Concat(disallowedItem.Where(c => char.IsLetterOrDigit(c)));
            }

            return suggestions;
        }
    }
}
