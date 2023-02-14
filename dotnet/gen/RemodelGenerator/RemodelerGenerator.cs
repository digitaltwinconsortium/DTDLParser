namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Code generator for <c>Remodeler</c> class used in <c>Remodel</c> tool.
    /// </summary>
    public class RemodelerGenerator
    {
        private const string DtdlContextPrefix = "dtmi:dtdl:context;";
        private const string FeatureContextPrefix = "dtmi:dtdl:extension:";

        private readonly int latestDtdlVersion;
        private readonly string baseClassName;
        private readonly HashSet<string> termIriRegexes;
        private readonly HashSet<string> reservedIdPrefixes;
        private readonly Dictionary<string, MaterialClassDigest> materialClasses;
        private readonly List<string> noLongerConcreteClasses;
        private readonly List<string> nonDependentRefProperties;
        private readonly HashSet<string> langStringProperties;
        private readonly HashSet<string> scalarLiteralProperties;
        private readonly bool areKeywordsRestricted;
        private readonly Dictionary<string, List<string>> featureSplitoutTypeMap;
        private readonly JObject partnerExtensionsObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemodelerGenerator"/> class.
        /// </summary>
        /// <param name="latestDtdlVersion">The latest version of DTDL, which the Remodel tool should target for model conversion.</param>
        /// <param name="baseName">The base name for the parser's object model.</param>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        /// <param name="reservedIdPrefixes">A dicictionary that maps from context ID to a list of identifier prefixes that are reserved for this context.</param>
        /// <param name="materialClasses">A a dictionary that maps from class name to a <see cref="MaterialClassDigest"/> object providing details about the named material class.</param>
        /// <param name="extensibleMaterialClasses">A dictionary that maps from DTDL version to a list of strings that each indicate a material class that is extensible.</param>
        /// <param name="dtdlVersionsRestrictingKeywords">A list of DTDL versions that restrict the use of unsupported JSON-LD keywords in models.</param>
        /// <param name="partnerExtensionsObject">A <c>JObject</c> containing partner extension version information from the DTDL config file.</param>
        public RemodelerGenerator(
            int latestDtdlVersion,
            string baseName,
            Dictionary<string, Dictionary<string, string>> contexts,
            Dictionary<string, List<string>> reservedIdPrefixes,
            Dictionary<string, MaterialClassDigest> materialClasses,
            Dictionary<int, List<string>> extensibleMaterialClasses,
            List<int> dtdlVersionsRestrictingKeywords,
            JObject partnerExtensionsObject)
        {
            this.latestDtdlVersion = latestDtdlVersion;
            this.baseClassName = NameFormatter.FormatNameAsClass(baseName);

            this.termIriRegexes = new HashSet<string>();
            foreach (KeyValuePair<string, Dictionary<string, string>> context in contexts)
            {
                foreach (KeyValuePair<string, string> termDefinition in context.Value)
                {
                    this.termIriRegexes.Add(termDefinition.Value.Replace(termDefinition.Key, @"(\w*)"));
                }
            }

            this.reservedIdPrefixes = new HashSet<string>();
            foreach (KeyValuePair<string, List<string>> contextReservedIdPrefixes in reservedIdPrefixes)
            {
                foreach (string reservedIdPrefix in contextReservedIdPrefixes.Value)
                {
                    this.reservedIdPrefixes.Add(reservedIdPrefix);
                }
            }

            this.materialClasses = materialClasses;

            this.noLongerConcreteClasses = materialClasses.Where(c => !c.Value.IsAbstract && !extensibleMaterialClasses[latestDtdlVersion].Contains(c.Key) && !c.Value.ConcreteSubclasses[latestDtdlVersion].Contains(c.Key)).Select(c => c.Key).ToList();

            this.nonDependentRefProperties = materialClasses.SelectMany(c => c.Value.Properties).Where(p => !p.Value.IsLiteral && p.Value.Class == null).Select(p => p.Key).ToList();

            this.langStringProperties = new HashSet<string>(materialClasses.SelectMany(c => c.Value.Properties).Where(p => p.Value.IsLiteral && !p.Value.IsInherited && p.Value.Datatype == "langString").Select(p => p.Key));

            this.scalarLiteralProperties = new HashSet<string>(materialClasses.SelectMany(c => c.Value.Properties).Where(p => p.Value.IsLiteral && !p.Value.IsInherited && p.Value.Datatype != "langString").Select(p => p.Key));

            this.areKeywordsRestricted = dtdlVersionsRestrictingKeywords.Contains(latestDtdlVersion);

            this.featureSplitoutTypeMap = this.GetFeatureSplitoutTypeMap(contexts);

            this.partnerExtensionsObject = partnerExtensionsObject;
        }

        /// <summary>
        /// Generate code for the type.
        /// </summary>
        /// <param name="parserLibrary">A <c>CsLibrary</c> object to which to add the generated code.</param>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass remodelerClass = parserLibrary.Class(Access.Public, Novelty.Normal, "Remodeler", completeness: Completeness.Partial);
            remodelerClass.Summary("Partial class that performs the main operations of upgrading a model to the latest version of DTDL.");

            remodelerClass.Constant(Access.Public, "int", "TargetDtdlVersion", this.latestDtdlVersion.ToString(), "The version of DTDL to which models will be upgraded.");

            remodelerClass.Field(Access.Private, $"IReadOnlyDictionary<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}>", "model");

            this.GenerateStaticConstructor(remodelerClass);
            this.GenerateTryValidateModelMethod(remodelerClass);
            this.GenerateTryRemoveImproperKeywordMethod(remodelerClass);
        }

        private Dictionary<string, List<string>> GetFeatureSplitoutTypeMap(Dictionary<string, Dictionary<string, string>> contexts)
        {
            string latestDtdlContextId = DtdlContextPrefix + this.latestDtdlVersion.ToString();
            Dictionary<string, string> latestDtdlContext = contexts[latestDtdlContextId];

            Dictionary<string, List<string>> typeMap = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, Dictionary<string, string>> context in contexts)
            {
                if (!context.Key.StartsWith(FeatureContextPrefix))
                {
                    continue;
                }

                string extensionName = this.GetExtensionName(context.Key);
                int extensionVersion = this.GetContextVersion(context.Key);
                if (contexts.Keys.Any(k => k.StartsWith(FeatureContextPrefix) && this.GetExtensionName(k) == extensionName && this.GetContextVersion(k) > extensionVersion))
                {
                    continue;
                }

                List<string> relocatedTypes = new List<string>();
                foreach (KeyValuePair<string, string> termDefinition in context.Value)
                {
                    if (termDefinition.Value.Contains(":class:") && !latestDtdlContext.ContainsKey(termDefinition.Key))
                    {
                        foreach (KeyValuePair<string, Dictionary<string, string>> dtdlContext in contexts)
                        {
                            if (dtdlContext.Key.StartsWith(DtdlContextPrefix) && this.GetContextVersion(dtdlContext.Key) != this.latestDtdlVersion)
                            {
                                if (dtdlContext.Value.TryGetValue(termDefinition.Key, out string dtdlIri))
                                {
                                    relocatedTypes.Add(dtdlIri);
                                }
                            }
                        }
                    }
                }

                if (relocatedTypes.Any())
                {
                    typeMap[context.Key] = relocatedTypes;
                }
            }

            return typeMap;
        }

        private void GenerateStaticConstructor(CsClass remodelerClass)
        {
            CsConstructor staticConstructor = remodelerClass.Constructor(Access.Implicit, Multiplicity.Static);

            staticConstructor.Body.Break();
            staticConstructor.Body.Line("termIriRegexes = new List<string>();");
            foreach (string termIriRegex in this.termIriRegexes)
            {
                staticConstructor.Body.Line($"termIriRegexes.Add(@\"{termIriRegex}\");");
            }

            staticConstructor.Body.Break();
            staticConstructor.Body.Line("reservedPrefixes = new List<string>();");
            foreach (string reservedIdPrefix in this.reservedIdPrefixes)
            {
                staticConstructor.Body.Line($"reservedPrefixes.Add(\"{reservedIdPrefix}\");");
            }

            staticConstructor.Body.Break();
            staticConstructor.Body.Line("abstractPropertyConcreteTypeMap = new Dictionary<string, Dictionary<string, string>>();");

            foreach (string noLongerConcreteClass in this.noLongerConcreteClasses)
            {
                staticConstructor.Body.Break();

                string typeMapVar = this.GetTypeMapVar(noLongerConcreteClass);
                staticConstructor.Body.Line($"Dictionary<string, string> {typeMapVar} = new Dictionary<string, string>();");

                foreach (KeyValuePair<string, MaterialClassDigest> discriminantClass in this.materialClasses.Where(c => c.Value.Properties.Any(p => p.Value.Class == noLongerConcreteClass)))
                {
                    foreach (KeyValuePair<string, MaterialPropertyDigest> discriminantProp in discriminantClass.Value.Properties.Where(p => p.Value.Class == noLongerConcreteClass))
                    {
                        staticConstructor.Body.Line($"{typeMapVar}[\"{discriminantProp.Key}\"] = \"{discriminantProp.Value.PropertyVersions[this.latestDtdlVersion].Class}\";");
                    }
                }

                staticConstructor.Body.Line($"abstractPropertyConcreteTypeMap[\"{noLongerConcreteClass}\"] = {typeMapVar};");
            }

            staticConstructor.Body.Break();
            staticConstructor.Body.Line("nonDependentRefPropertyNames = new HashSet<string>();");
            foreach (string nonDependentRefProperty in this.nonDependentRefProperties)
            {
                staticConstructor.Body.Line($"nonDependentRefPropertyNames.Add(\"{nonDependentRefProperty}\");");
            }

            staticConstructor.Body.Break();
            staticConstructor.Body.Line($"areKeywordsRestricted = {ParserGeneratorValues.GetBooleanLiteral(this.areKeywordsRestricted)};");

            staticConstructor.Body.Break();
            staticConstructor.Body.Line("featureSplitoutTypeMap = new Dictionary<string, HashSet<Dtmi>>();");
            foreach (KeyValuePair<string, List<string>> featureSplitout in this.featureSplitoutTypeMap)
            {
                string extensionName = this.GetExtensionName(featureSplitout.Key);
                string typeMapVar = this.GetTypeMapVar(extensionName);
                staticConstructor.Body.Line($"HashSet<Dtmi> {typeMapVar} = new HashSet<Dtmi>();");

                foreach (string relocatedType in featureSplitout.Value)
                {
                    staticConstructor.Body.Line($"{typeMapVar}.Add(new Dtmi(\"{relocatedType}\"));");
                }

                staticConstructor.Body.Line($"featureSplitoutTypeMap[\"{featureSplitout.Key}\"] = {typeMapVar};");
            }

            staticConstructor.Body.Break();
            staticConstructor.Body.Line("partnerMaxVersions = new Dictionary<string, int>();");
            foreach (KeyValuePair<string, JToken> partnerExtension in this.partnerExtensionsObject)
            {
                staticConstructor.Body.Line($"partnerMaxVersions[\"{partnerExtension.Key}\"] = {partnerExtension.Value.Max()};");
            }
        }

        private void GenerateTryValidateModelMethod(CsClass remodelerClass)
        {
            CsMethod method = remodelerClass.Method(Access.Private, Novelty.Normal, "bool", "TryValidateModel");
            method.Param($"IReadOnlyDictionary<{ParserGeneratorValues.IdentifierType}, {this.baseClassName}>", "model", passage: Passage.Out);
            method.Param("IList<ParsingError>", "parsingErrors", passage: Passage.Out);
            method.Param("IList<Dtmi>", "undefinedIdentifiers", passage: Passage.Out);
            method.Param("bool", "allowUndefinedExtensions", null, "false");
            method.Param("int", "maxDtdlVersion", null, "0");

            method.Body.Line("var parsingOptions = new ParsingOptions() { AllowUndefinedExtensions = allowUndefinedExtensions };");

            method.Body.If("maxDtdlVersion > 0")
                .Line("parsingOptions.MaxDtdlVersion = maxDtdlVersion;");

            method.Body.Line("ModelParser parser = new ModelParser(parsingOptions);");

            method.Body
                .Try()
                    .Line("model = parser.Parse(modelComponents.Select(mc => mc.JsonText).Concat(skipComponents.Select(sc => sc.JsonText)));")
                    .Line("parsingErrors = null;")
                    .Line("undefinedIdentifiers = null;")
                    .Line("return true;")
                .Catch("ParsingException pe")
                    .Line("Console.WriteLine();")
                    .Line("Console.WriteLine($\"MODEL INVALID -- {pe.Errors.Count} errors in model, including:\");")
                    .Line("Console.WriteLine(pe.Errors.First().Message);")
                    .Line("model = null;")
                    .Line("parsingErrors = pe.Errors;")
                    .Line("undefinedIdentifiers = new List<Dtmi>();")
                    .Line("return false;")
                .Catch("ResolutionException re")
                    .Line("Console.WriteLine();")
                    .Line("Console.WriteLine($\"MODEL INCOMPLETE -- {re.UndefinedIdentifiers.Count} undefined identifiers in model, including: {re.UndefinedIdentifiers.First()}\");")
                    .Line("model = null;")
                    .Line("parsingErrors = new List<ParsingError>();")
                    .Line("undefinedIdentifiers = re.UndefinedIdentifiers;")
                    .Line("return false;");
        }

        private void GenerateTryRemoveImproperKeywordMethod(CsClass remodelerClass)
        {
            CsMethod method = remodelerClass.Method(Access.Private, Novelty.Normal, "bool", "TryRemoveImproperKeyword");
            method.Param("JProperty", "keywordProperty");
            method.Param("string", "discriminantPropName");

            CsSwitch discriminantPropSwitch = method.Body.Switch("discriminantPropName");

            foreach (string langStringProperty in this.langStringProperties)
            {
                discriminantPropSwitch.Case($"\"{langStringProperty}\"");
            }

            discriminantPropSwitch
                .If("keywordProperty.Name == \"@type\"")
                    .Line("keywordProperty.Remove();")
                    .Line("return true;")
                .Else()
                    .Line("return false;");

            foreach (string scalarLiteralProperty in this.scalarLiteralProperties)
            {
                discriminantPropSwitch.Case($"\"{scalarLiteralProperty}\"");
            }

            discriminantPropSwitch
                .If("keywordProperty.Name == \"@language\"")
                    .Line("keywordProperty.Remove();")
                    .Line("return true;")
                .Else()
                    .Line("return false;");

            discriminantPropSwitch.Default();
            discriminantPropSwitch
                .If("keywordProperty.Name == \"@value\" || keywordProperty.Name == \"@language\"")
                    .Line("keywordProperty.Remove();")
                    .Line("return true;")
                .Else()
                    .Line("return false;");
        }

        private string GetTypeMapVar(string className)
        {
            return $"{char.ToLowerInvariant(className[0])}{className.Substring(1)}TypeMap";
        }

        private string GetExtensionName(string contextId)
        {
            return contextId.Substring(FeatureContextPrefix.Length, contextId.IndexOf(';') - FeatureContextPrefix.Length);
        }

        private int GetContextVersion(string contextId)
        {
            return int.Parse(contextId.Substring(contextId.IndexOf(';') + 1));
        }
    }
}
