namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Partial class that performs the main operations of upgrading a model to the latest version of DTDL.
    /// </summary>
    public partial class Remodeler
    {
        private const string DtdlContextPrefix = "dtmi:dtdl:context;";
        private const string PartnerContextPrefix = "dtmi:{0}:context;";

        private static List<string> termIriRegexes;
        private static List<string> reservedPrefixes;
        private static Dictionary<string, Dictionary<string, string>> abstractPropertyConcreteTypeMap;
        private static HashSet<string> nonDependentRefPropertyNames;
        private static bool areKeywordsRestricted;
        private static Dictionary<string, HashSet<Dtmi>> featureSplitoutTypeMap;
        private static Dictionary<string, int> partnerMaxVersions;

        private Substitutions substitutions;
        private Suggester suggester;
        private Summarizer summarizer;
        private List<ModelComponent> modelComponents;
        private List<ModelComponent> skipComponents;

        private IList<ParsingError> postUpdateParsingErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Remodeler"/> class.
        /// </summary>
        /// <param name="sourceFolder">A <c>DirectoryInfo</c> object representing the root of a directory tree containing DTDL source model.</param>
        /// <param name="skipFolders">An array of <c>DirectoryInfo</c> objects representing the roots of subtrees (within source tree) to skip converting.</param>
        /// <param name="substitutions">A <see cref="Substitutions"/> object providing IRI/term substitutions for contexts, types, and properties.</param>
        /// <param name="suggester">A <see cref="Suggester"/> object that can suggest IRI/term substitutions for contexts, types, and properties.</param>
        /// <param name="summarizer">A <see cref="Summarizer"/> object for logging a summary of changes performed".</param>
        /// <param name="filePattern">File pattern to use for enumerating files, e.g., "*.json".</param>
        public Remodeler(DirectoryInfo sourceFolder, DirectoryInfo[] skipFolders, Substitutions substitutions, Suggester suggester, Summarizer summarizer, string filePattern)
        {
            this.substitutions = substitutions;
            this.suggester = suggester;
            this.summarizer = summarizer;

            this.modelComponents = new List<ModelComponent>();
            this.skipComponents = new List<ModelComponent>();

            this.postUpdateParsingErrors = null;

            Console.Write("Reading source files");

            foreach (FileInfo sourceFile in sourceFolder.EnumerateFiles(filePattern, SearchOption.AllDirectories))
            {
                Console.Write(".");
                FileStream readStream = sourceFile.OpenRead();
                StreamReader streamReader = new StreamReader(readStream);
                string jsonText = streamReader.ReadToEnd();
                Encoding encoding = streamReader.CurrentEncoding;
                streamReader.Close();

                string componentName = sourceFile.FullName.Substring(sourceFolder.FullName.Length + 1);
                ModelComponent newComponent = new ModelComponent(componentName, encoding, jsonText);

                if (skipFolders.Any(s => sourceFile.FullName.StartsWith(s.FullName)))
                {
                    this.skipComponents.Add(newComponent);
                }
                else
                {
                    this.modelComponents.Add(newComponent);
                }
            }

            this.model = null;

            Console.WriteLine();
        }

        /// <summary>
        /// Try to confirm that the source model is valid.
        /// </summary>
        /// <param name="allowUndefinedExtensions">Allow undefined extensions in models.</param>
        /// <returns>True if the model is valid according to a previous version of DTDL.</returns>
        public bool TryValidateSourceModel(bool allowUndefinedExtensions)
        {
            Announcer.Heading("Validating source DTDL model collection");

            return this.TryValidateModel(out this.model, out _, out _, allowUndefinedExtensions, TargetDtdlVersion - 1);
        }

        /// <summary>
        /// Canonicalize class, property, and element names (e.g., "dtmi:dtdl:property:schema;2" => "schema").
        /// </summary>
        public void CanonicalizeNames()
        {
            Announcer.Heading("Canonicalizing class, property, and element names");

            foreach (ModelComponent modelComponent in this.modelComponents)
            {
                Announcer.Tick();

                foreach (string termIriRegex in termIriRegexes)
                {
                    if (this.summarizer.IsSummarizing && Regex.IsMatch(modelComponent.JsonText, $"\"{termIriRegex}\""))
                    {
                        this.summarizer.ModelCanonicalized(modelComponent.Name);
                    }

                    modelComponent.JsonText = Regex.Replace(modelComponent.JsonText, $"\"{termIriRegex}\"", "\"$1\"");
                }
            }
        }

        /// <summary>
        /// Replace abstract classes with appropriate concrete subclasses (e.g., "CommandPayload" => "CommandRequest").
        /// </summary>
        public void ReplaceAbstractWithConcreteSubclasses()
        {
            Announcer.Heading("Replacing abstract classes with appropriate concrete subclasses");

            foreach (ModelComponent modelComponent in this.modelComponents)
            {
                Announcer.Tick();

                foreach (JProperty typeProperty in modelComponent.JsonContainer.DescendantsAndSelf().Where(jt => jt.Type == JTokenType.Property && ((JProperty)jt).Name == "@type").Select(jt => (JProperty)jt).ToList())
                {
                    foreach (KeyValuePair<string, Dictionary<string, string>> abstractSubclassMap in abstractPropertyConcreteTypeMap)
                    {
                        JToken typeValue =
                            typeProperty.Value.Type == JTokenType.Array ? ((JArray)typeProperty.Value).Children().FirstOrDefault(c => c.Type == JTokenType.String && ((JValue)c).Value<string>() == abstractSubclassMap.Key) :
                            typeProperty.Value.Type == JTokenType.String && ((JValue)typeProperty.Value).Value<string>() == abstractSubclassMap.Key ? typeProperty.Value :
                            null;

                        if (typeValue != null)
                        {
                            JProperty elementProp = typeProperty.Parent.Parent.Type == JTokenType.Property ? (JProperty)typeProperty.Parent.Parent : (JProperty)typeProperty.Parent.Parent.Parent;
                            if (abstractSubclassMap.Value.TryGetValue(elementProp.Name, out string concreteType))
                            {
                                typeValue.Replace(new JValue(concreteType));
                                this.summarizer.ModelConcretized(modelComponent.Name);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove and replace invalid types per substitution rules.
        /// </summary>
        public void ReplaceOrRemoveInvalidTypes()
        {
            Announcer.Heading("Removing and replacing invalid types per substitution rules");

            foreach (ModelComponent modelComponent in this.modelComponents)
            {
                Announcer.Tick();

                foreach (JProperty typeProperty in modelComponent.JsonContainer.DescendantsAndSelf().Where(jt => jt.Type == JTokenType.Property && ((JProperty)jt).Name == "@type").Select(jt => (JProperty)jt).ToList())
                {
                    if (typeProperty.Value.Type == JTokenType.Array)
                    {
                        foreach (JToken typeValue in ((JArray)typeProperty.Value).Children().Where(c => c.Type == JTokenType.String).ToList())
                        {
                            if (this.substitutions != null && this.substitutions.TypeSubstitutions.TryGetValue(((JValue)typeValue).Value<string>(), out string substitute))
                            {
                                if (substitute != null)
                                {
                                    typeValue.Replace(new JValue(substitute));
                                    this.summarizer.ModelTypeReplaced(modelComponent.Name);
                                }
                                else
                                {
                                    typeValue.Remove();
                                    this.summarizer.ModelTypeRemoved(modelComponent.Name);
                                }
                            }
                        }
                    }
                    else if (typeProperty.Value.Type == JTokenType.String)
                    {
                        if (this.substitutions != null && this.substitutions.TypeSubstitutions.TryGetValue(((JValue)typeProperty.Value).Value<string>(), out string substitute))
                        {
                            if (substitute != null)
                            {
                                typeProperty.Value.Replace(new JValue(substitute));
                                this.summarizer.ModelTypeReplaced(modelComponent.Name);
                            }
                            else
                            {
                                typeProperty.Remove();
                                this.summarizer.ModelTypeRemoved(modelComponent.Name);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove and replace invalid properties per substitution rules.
        /// </summary>
        public void ReplaceOrRemoveInvalidProperties()
        {
            Announcer.Heading("Removing and replacing invalid properties per substitution rules");

            foreach (ModelComponent modelComponent in this.modelComponents)
            {
                Announcer.Tick();

                foreach (JProperty property in modelComponent.JsonContainer.DescendantsAndSelf().Where(jt => jt.Type == JTokenType.Property).Select(jt => (JProperty)jt).ToList())
                {
                    if (this.substitutions != null && this.substitutions.PropertySubstitutions.TryGetValue(property.Name, out string substitute))
                    {
                        if (substitute != null)
                        {
                            property.Replace(new JProperty(substitute, property.Value));
                            this.summarizer.ModelPropertyReplaced(modelComponent.Name);
                        }
                        else
                        {
                            property.Remove();
                            this.summarizer.ModelPropertyRemoved(modelComponent.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove improper JSON-LD keywords (e.g., "@reverse").
        /// </summary>
        public void RemoveImproperKeywords()
        {
            if (areKeywordsRestricted)
            {
                Announcer.Heading("Removing improper JSON-LD keywords");

                foreach (ModelComponent modelComponent in this.modelComponents)
                {
                    Announcer.Tick();

                    foreach (JProperty keywordProperty in modelComponent.JsonContainer.DescendantsAndSelf().Where(jt => jt.Type == JTokenType.Property && ((JProperty)jt).Name.StartsWith('@') && ((JProperty)jt).Name != "@context" && ((JProperty)jt).Name != "@id").Select(jt => (JProperty)jt).ToList())
                    {
                        if (keywordProperty.Name != "@type" && keywordProperty.Name != "@value" && keywordProperty.Name != "@language")
                        {
                            keywordProperty.Remove();
                            this.summarizer.ModelKeywordRemoved(modelComponent.Name);
                        }
                        else
                        {
                            JProperty discriminantProp = keywordProperty.Parent.Parent != null ? (keywordProperty.Parent.Parent.Type == JTokenType.Property ? (JProperty)keywordProperty.Parent.Parent : (JProperty)keywordProperty.Parent.Parent?.Parent) : null;
                            if (this.TryRemoveImproperKeyword(keywordProperty, discriminantProp?.Name ?? string.Empty))
                            {
                                this.summarizer.ModelKeywordRemoved(modelComponent.Name);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Upgrading contexts to lastest DTDL/partner versions, apply substitution rules, and add QuantitativeTypes context if needed.
        /// </summary>
        public void UpgradeContexts()
        {
            Announcer.Heading($"Upgrading contexts and applying substitution rules");

            Dictionary<string, HashSet<string>> featureElements = new Dictionary<string, HashSet<string>>();
            foreach (KeyValuePair<string, HashSet<Dtmi>> kvp in featureSplitoutTypeMap)
            {
                featureElements[kvp.Key] = new HashSet<string>(this.model.Where(e => e.Value.SupplementalTypes.Any(t => kvp.Value.Contains(t))).Select(e => (e.Value.DefinedIn ?? e.Value.Id).AbsoluteUri));
            }

            HashSet<string> extendedElements = new HashSet<string>(this.model.Where(e => e.Value.UndefinedTypes.Any()).Select(e => (e.Value.DefinedIn ?? e.Value.Id).AbsoluteUri));

            foreach (ModelComponent modelComponent in this.modelComponents)
            {
                Announcer.Tick();

                List<string> featureContextsNeeded = new List<string>();
                foreach (KeyValuePair<string, HashSet<string>> kvp in featureElements)
                {
                    if (modelComponent.JsonContainer.DescendantsAndSelf().Any(jt => jt.Type == JTokenType.Property && ((JProperty)jt).Name == "@id" && kvp.Value.Contains(((JValue)((JProperty)jt).Value).Value<string>())))
                    {
                        featureContextsNeeded.Add(kvp.Key);
                    }
                }

                bool addContextNeeded = modelComponent.JsonContainer.DescendantsAndSelf().Any(jt => jt.Type == JTokenType.Property && ((JProperty)jt).Name == "@id" && extendedElements.Contains(((JValue)((JProperty)jt).Value).Value<string>()));
                List<string> partnersPrecedingDtdl = new List<string>();
                bool undefinedContextAdded = false;
                bool contextReplaced = false;
                bool contextRemoved = false;

                foreach (JProperty contextProperty in modelComponent.JsonContainer.DescendantsAndSelf().Where(jt => jt.Type == JTokenType.Property && ((JProperty)jt).Name == "@context").Select(jt => (JProperty)jt).ToList())
                {
                    bool dtdlContextPresent = false;
                    List<string> partnerNames = new List<string>();
                    List<string> otherContexts = new List<string>();

                    if (contextProperty.Value.Type == JTokenType.Array)
                    {
                        foreach (JToken contextValue in ((JArray)contextProperty.Value).Children().Where(c => c.Type == JTokenType.String))
                        {
                            if (this.summarizer.IsSummarizing && ((JValue)contextValue).Value<string>().StartsWith(DtdlContextPrefix) && partnerNames.Any())
                            {
                                foreach (string partnerName in partnerNames)
                                {
                                    partnersPrecedingDtdl.Add(partnerName);
                                }
                            }

                            this.ProcessContextValue(contextValue, ref dtdlContextPresent, ref partnerNames, ref otherContexts, ref contextReplaced, ref contextRemoved);
                        }
                    }
                    else if (contextProperty.Value.Type == JTokenType.String)
                    {
                        this.ProcessContextValue(contextProperty.Value, ref dtdlContextPresent, ref partnerNames, ref otherContexts, ref contextReplaced, ref contextRemoved);
                    }

                    List<string> newContextValues = new List<string>();

                    if (dtdlContextPresent)
                    {
                        newContextValues.Add($"{DtdlContextPrefix}{TargetDtdlVersion}");
                    }

                    foreach (string partnerName in partnerNames)
                    {
                        int partnerVersion = Math.Min(TargetDtdlVersion, partnerMaxVersions[partnerName]);
                        newContextValues.Add($"{string.Format(PartnerContextPrefix, partnerName)}{partnerVersion}");
                    }

                    if (dtdlContextPresent)
                    {
                        newContextValues.AddRange(featureContextsNeeded);
                    }

                    newContextValues.AddRange(otherContexts);

                    if (dtdlContextPresent && addContextNeeded && !otherContexts.Any())
                    {
                        if (this.substitutions == null)
                        {
                            throw new Exception("An unknown context specifier must be added to the model, but no substitutions file was provided to define the context addition. For details, type: Remodel --explain subs");
                        }

                        newContextValues.Add(this.substitutions.ContextAddition);
                        undefinedContextAdded = true;
                    }

                    if (newContextValues.Any())
                    {
                        contextProperty.Value.Replace(newContextValues.Count == 1 ? (JToken)new JValue(newContextValues.First()) : (JToken)new JArray(newContextValues));
                    }
                    else
                    {
                        contextProperty.Remove();
                    }
                }

                this.summarizer.ContextChanged(
                    modelComponent.Name,
                    partnerContextsReordered: partnersPrecedingDtdl,
                    featureContextsAdded: featureContextsNeeded,
                    undefinedExtensionContextAdded: undefinedContextAdded,
                    contextReplaced: contextReplaced,
                    contextRemoved: contextRemoved);
            }
        }

        /// <summary>
        /// Generate new version numbers for user IDs.
        /// </summary>
        public void GenerateNewVersionNumbersForUserIds()
        {
            Announcer.Heading("Generating new version numbers for user IDs");

            this.IdentifyUserIds(out List<JToken> idTokens, out List<JToken> refTokens);

            this.CalculateNewVersionNummbers(idTokens, out Dictionary<string, int> maxVersions, out Dictionary<string, int> minVersions);

            this.UpdateUserIds(idTokens, refTokens, maxVersions, minVersions);
        }

        /// <summary>
        /// Try to confirm that the modified model is valid.
        /// </summary>
        /// <param name="allowUndefinedExtensions">Allow undefined extensions in models.</param>
        /// <returns>True if the model is valid according to the current version of DTDL.</returns>
        public bool TryValidateModifiedModel(bool allowUndefinedExtensions)
        {
            Announcer.Heading("Validating modified DTDL model collection");

            return this.TryValidateModel(out _, out this.postUpdateParsingErrors, out _, allowUndefinedExtensions);
        }

        /// <summary>
        /// Try to suggest values for a substitutions file that would address parsing errors in modified model.
        /// </summary>
        /// <returns>True if suggested substitutions were generated.</returns>
        public bool TrySuggestSubstitutions()
        {
            return this.suggester != null && this.suggester.TrySuggestSubstitutions(this.postUpdateParsingErrors);
        }

        /// <summary>
        /// Write the modified model to new files created in the specified destination folder.
        /// </summary>
        /// <param name="destFolder">A <c>DirectoryInfo</c> object representing the root of a directory tree to which to write the modified model.</param>
        public void WriteDestinationFiles(DirectoryInfo destFolder)
        {
            Announcer.Heading("Writing destination files");

            foreach (ModelComponent modelComponent in this.modelComponents)
            {
                Announcer.Tick();
                string destName = Path.Combine(destFolder.FullName, modelComponent.Name);
                Directory.CreateDirectory(Path.GetDirectoryName(destName));
                FileStream writeStream = new FileStream(destName, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(writeStream, modelComponent.Encoding);
                streamWriter.Write(modelComponent.JsonText);
                streamWriter.Close();
            }
        }

        private void ProcessContextValue(
            JToken contextValue,
            ref bool dtdlContextPresent,
            ref List<string> partnerNames,
            ref List<string> otherContexts,
            ref bool contextReplacement,
            ref bool contextRemoval)
        {
            string contextString = ((JValue)contextValue).Value<string>();

            if (contextString.StartsWith(DtdlContextPrefix))
            {
                dtdlContextPresent = true;
                return;
            }

            foreach (string partnerName in partnerMaxVersions.Keys)
            {
                if (contextString.StartsWith(string.Format(PartnerContextPrefix, partnerName)))
                {
                    partnerNames.Add(partnerName);
                    return;
                }
            }

            if (this.substitutions != null && this.substitutions.ContextSubstitutions.TryGetValue(contextString, out string substitute))
            {
                if (substitute != null)
                {
                    otherContexts.Add(substitute);
                    contextReplacement = true;
                }
                else
                {
                    contextRemoval = true;
                }

                return;
            }

            otherContexts.Add(contextString);
        }

        private void IdentifyUserIds(out List<JToken> idTokens, out List<JToken> refTokens)
        {
            Announcer.Subheading("Identifying user IDs in model");

            idTokens = new List<JToken>();
            refTokens = new List<JToken>();

            foreach (ModelComponent modelComponent in this.modelComponents)
            {
                Announcer.Tick();

                foreach (JProperty idOrRefProperty in modelComponent.JsonContainer.DescendantsAndSelf().Where(jt => jt.Type == JTokenType.Property && ((JProperty)jt).Name != "@context").Select(jt => (JProperty)jt).ToList())
                {
                    if (idOrRefProperty.Value.Type == JTokenType.Array)
                    {
                        foreach (JToken idOrRefValue in ((JArray)idOrRefProperty.Value).Children().Where(c => c.Type == JTokenType.String).ToList())
                        {
                            string idOrRefString = ((JValue)idOrRefValue).Value<string>();
                            if (idOrRefString.StartsWith("dtmi:") && !reservedPrefixes.Any(p => idOrRefString.StartsWith(p)))
                            {
                                if (nonDependentRefPropertyNames.Contains(idOrRefProperty.Name))
                                {
                                    idOrRefValue.Replace(new JValue(idOrRefString.Substring(0, idOrRefString.IndexOf(';'))));
                                }
                                else if (idOrRefProperty.Name == "@id")
                                {
                                    idTokens.Add(idOrRefValue);
                                }
                                else
                                {
                                    refTokens.Add(idOrRefValue);
                                }
                            }
                        }
                    }
                    else if (idOrRefProperty.Value.Type == JTokenType.String)
                    {
                        string idOrRefString = ((JValue)idOrRefProperty.Value).Value<string>();
                        if (idOrRefString.StartsWith("dtmi:") && !reservedPrefixes.Any(p => idOrRefString.StartsWith(p)))
                        {
                            if (nonDependentRefPropertyNames.Contains(idOrRefProperty.Name))
                            {
                                idOrRefProperty.Value.Replace(new JValue(idOrRefString.Substring(0, idOrRefString.IndexOf(';'))));
                            }
                            else if (idOrRefProperty.Name == "@id")
                            {
                                idTokens.Add(idOrRefProperty.Value);
                            }
                            else
                            {
                                refTokens.Add(idOrRefProperty.Value);
                            }
                        }
                    }
                }
            }
        }

        private void CalculateNewVersionNummbers(List<JToken> idTokens, out Dictionary<string, int> maxVersions, out Dictionary<string, int> minVersions)
        {
            Announcer.Subheading("Calculating new version numbers for user IDs");

            maxVersions = new Dictionary<string, int>();
            minVersions = new Dictionary<string, int>();

            foreach (JToken idToken in idTokens)
            {
                Announcer.Tick();

                string idString = ((JValue)idToken).Value<string>();
                int delimIx = idString.IndexOf(';');
                string baseId = idString.Substring(0, delimIx);
                int version = int.Parse(idString.Substring(delimIx + 1));

                if (!maxVersions.TryGetValue(baseId, out int maxVersion) || version > maxVersion)
                {
                    maxVersions[baseId] = version;
                }

                if (!minVersions.TryGetValue(baseId, out int minVersion) || version < minVersion)
                {
                    minVersions[baseId] = version;
                }
            }
        }

        private void UpdateUserIds(List<JToken> idTokens, List<JToken> refTokens, Dictionary<string, int> maxVersions, Dictionary<string, int> minVersions)
        {
            Announcer.Subheading("Updating user IDs with new version numbers");

            foreach (JToken idToken in idTokens.Concat(refTokens))
            {
                Announcer.Tick();

                string idOrRefString = ((JValue)idToken).Value<string>();
                int delimIx = idOrRefString.IndexOf(';');
                string baseId = idOrRefString.Substring(0, delimIx);

                if (maxVersions.TryGetValue(baseId, out int maxVersion) && minVersions.TryGetValue(baseId, out int minVersion))
                {
                    int offset = maxVersion - minVersion + 1;
                    int newVersion = int.Parse(idOrRefString.Substring(delimIx + 1)) + offset;
                    string newIdOrRefString = $"{baseId};{newVersion}";
                    idToken.Replace(new JValue(newIdOrRefString));
                    this.summarizer.UserIdChanged(idOrRefString, newIdOrRefString);
                }
            }
        }
    }
}
