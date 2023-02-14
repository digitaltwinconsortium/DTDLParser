namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Partial class for suggesting IRI/term substitutions for contexts, types, and properties.
    /// </summary>
    public partial class Suggester
    {
        /// <summary>
        /// Suggested context value to add for an undefined extension.
        /// </summary>
        public const string UndefinedExtensionContextSuggestion = "dtmi:undefinedExtension:context;1";

        /// <summary>
        /// A list of validation IDs that indicate a disallowed context specifier in the model.
        /// </summary>
        public static readonly HashSet<Uri> DisallowedContextSpecifierValidationIds = new HashSet<Uri>()
        {
            new Uri("dtmi:dtdl:parsingError:nonDtmiContextSpecifier"),
            new Uri("dtmi:dtdl:parsingError:invalidContextSpecifier"),
            new Uri("dtmi:dtdl:parsingError:missingContextVersion"),
        };

        /// <summary>
        /// A list of validation IDs that indicate a disallowed type value in the model.
        /// </summary>
        public static readonly HashSet<Uri> DisallowedTypeValueValidationIds = new HashSet<Uri>()
        {
            new Uri("dtmi:dtdl:parsingError:typeInvalidDtmi"),
            new Uri("dtmi:dtdl:parsingError:typeIrrelevantDtmiOrTerm"),
            new Uri("dtmi:dtdl:parsingError:typeNotDtmiNorTerm"),
            new Uri("dtmi:dtdl:parsingError:typeUndefinedTerm"),
        };

        /// <summary>
        /// A list of validation IDs that indicate a disallowed property name in the model.
        /// </summary>
        public static readonly HashSet<Uri> DisallowedPropertyNameValidationIds = new HashSet<Uri>()
        {
            new Uri("dtmi:dtdl:parsingError:propertyInvalidDtmi"),
            new Uri("dtmi:dtdl:parsingError:propertyIrrelevantDtmiOrTerm"),
            new Uri("dtmi:dtdl:parsingError:propertyNotDtmiNorTerm"),
            new Uri("dtmi:dtdl:parsingError:propertyUndefinedTerm"),
        };

        private FileInfo suggestFile;
        private Substitutions substitutions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Suggester"/> class.
        /// </summary>
        /// <param name="suggestFile">A <c>FileInfo</c> object representing the JSON file to which to write suggested substitutions.</param>
        /// <param name="substitutions">A <see cref="Substitutions"/> object providing IRI/term substitutions that are already specified.</param>
        public Suggester(FileInfo suggestFile, Substitutions substitutions)
        {
            this.suggestFile = suggestFile;
            this.substitutions = substitutions;
        }

        /// <summary>
        /// Try to suggest values for a substitutions file that would address parsing errors in modified model.
        /// </summary>
        /// <param name="parsingErrors">A list of parsing errors in the modified model.</param>
        /// <returns>True if suggested substitutions were generated.</returns>
        public bool TrySuggestSubstitutions(IList<ParsingError> parsingErrors)
        {
            if (!this.TryIdentifyDisallowedItems(parsingErrors, out HashSet<string> disallowedContextSpecifiers, out HashSet<string> disallowedTypeValues, out HashSet<string> disallowedPropertyNames))
            {
                return false;
            }

            string contextAddition = UndefinedExtensionContextSuggestion;
            Dictionary<string, string> contextSuggestions = this.GetContextSuggestions(disallowedContextSpecifiers);
            Dictionary<string, string> typeSuggestions = this.GetTypeSuggestions(disallowedTypeValues);
            Dictionary<string, string> propertySuggestions = this.GetPropSuggestions(disallowedPropertyNames);

            if (this.substitutions != null)
            {
                contextAddition = this.substitutions.ContextAddition;
                this.IncorporateExtantSubstitutions(contextSuggestions, this.substitutions.ContextSubstitutions);
                this.IncorporateExtantSubstitutions(typeSuggestions, this.substitutions.TypeSubstitutions);
                this.IncorporateExtantSubstitutions(propertySuggestions, this.substitutions.PropertySubstitutions);
            }

            this.WriteSuggestFile(contextAddition, contextSuggestions, typeSuggestions, propertySuggestions);

            return true;
        }

        private void IncorporateExtantSubstitutions(Dictionary<string, string> suggestions, Dictionary<string, string> extantSubstitutions)
        {
            foreach (KeyValuePair<string, string> extantSub in extantSubstitutions)
            {
                if (!suggestions.ContainsKey(extantSub.Key))
                {
                    suggestions[extantSub.Key] = suggestions.TryGetValue(extantSub.Value, out string newSuggestion) ? newSuggestion : extantSub.Value;
                }
            }
        }

        private bool TryIdentifyDisallowedItems(IList<ParsingError> parsingErrors, out HashSet<string> disallowedContextSpecifiers, out HashSet<string> disallowedTypeValues, out HashSet<string> disallowedPropertyNames)
        {
            disallowedContextSpecifiers = new HashSet<string>();
            disallowedTypeValues = new HashSet<string>();
            disallowedPropertyNames = new HashSet<string>();

            if (parsingErrors == null)
            {
                return false;
            }

            foreach (ParsingError parsingError in parsingErrors)
            {
                if (DisallowedContextSpecifierValidationIds.Contains(parsingError.ValidationID))
                {
                    disallowedContextSpecifiers.Add(parsingError.Value);
                }
                else if (DisallowedTypeValueValidationIds.Contains(parsingError.ValidationID))
                {
                    disallowedTypeValues.Add(parsingError.Value);
                }
                else if (DisallowedPropertyNameValidationIds.Contains(parsingError.ValidationID))
                {
                    disallowedPropertyNames.Add(parsingError.Property);
                }
            }

            return disallowedContextSpecifiers.Any() || disallowedTypeValues.Any() || disallowedPropertyNames.Any();
        }

        private Dictionary<string, string> GetContextSuggestions(HashSet<string> disallowedContextSpecifiers)
        {
            Dictionary<string, string> suggestions = new Dictionary<string, string>();

            foreach (string disallowedContextSpecifier in disallowedContextSpecifiers)
            {
                string contextWithoutIllegalChars = Regex.Replace(disallowedContextSpecifier, @"[^A-Za-z0-9]+", ":");

                string contextWithLegalPrefix = $"dtmi:{contextWithoutIllegalChars.Substring(contextWithoutIllegalChars.IndexOf(':') + 1)}";

                string contextWithoutIllegalLabels = Regex.Replace(contextWithLegalPrefix, @":(\d+)", @":x$1");

                string contextWithVersionSuffix = contextWithoutIllegalLabels.EndsWith(':') ? Regex.Replace(contextWithoutIllegalLabels, @":$", ";1") : $"{contextWithoutIllegalLabels};1";

                suggestions[disallowedContextSpecifier] = contextWithVersionSuffix;
            }

            return suggestions;
        }

        private void WriteSuggestFile(string contextAddition, Dictionary<string, string> contextSuggestions, Dictionary<string, string> typeSuggestions, Dictionary<string, string> propertySuggestions)
        {
            JObject suggestObj = new JObject();

            suggestObj["contextAddition"] = new JValue(contextAddition);
            suggestObj["contextSubstitutions"] = new JObject(contextSuggestions.Select(s => new JProperty(s.Key, new JValue(s.Value))));
            suggestObj["typeSubstitutions"] = new JObject(typeSuggestions.Select(s => new JProperty(s.Key, new JValue(s.Value))));
            suggestObj["propertySubstitutions"] = new JObject(propertySuggestions.Select(s => new JProperty(s.Key, new JValue(s.Value))));

            FileStream writeStream = this.suggestFile.OpenWrite();
            writeStream.SetLength(0);
            StreamWriter streamWriter = new StreamWriter(writeStream);
            streamWriter.WriteLine(suggestObj.ToString());
            streamWriter.Close();
        }
    }
}
