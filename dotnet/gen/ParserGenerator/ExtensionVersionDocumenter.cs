namespace DTDLParser
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Static class for documenting which DTDL language extensions and which versions thereof are supported by the parser.
    /// </summary>
    public static class ExtensionVersionDocumenter
    {
        /// <summary>
        /// Domument the supported extension versions.
        /// </summary>
        /// <param name="docFolder">Path of folder in which to place the generated documentation file.</param>
        /// <param name="docFile">Name of documentation file to generate.</param>
        /// <param name="dtdlConfigFile">Configuration file that specifies which language versions and extensions are supported.</param>
        /// <param name="metamodelDigest">A <see cref="MetamodelDigest"/> object from which to extract information for the extensions.</param>
        public static void DocumentExtensionVersions(string docFolder, string docFile, string dtdlConfigFile, MetamodelDigest metamodelDigest)
        {
            StreamWriter streamWriter = new StreamWriter(Path.Combine(docFolder, docFile));

            JObject languageConfigObject = (JObject)JToken.Parse(File.OpenText(dtdlConfigFile).ReadToEnd());

            streamWriter.WriteLine("# Supported extension contexts");
            streamWriter.WriteLine();
            streamWriter.WriteLine("The chart below itemizes the extension contexts that are currently supported.");
            streamWriter.WriteLine();
            streamWriter.WriteLine("| Extension | Context specifier | DTDL versions |");
            streamWriter.WriteLine("| --- | --- | --- |");

            AddRowsForExtensionCategory("partnerExtensions", ParserGeneratorValues.PartnerExtensionContextId, ParserGeneratorValues.PartnerExtensionDocRef, streamWriter, languageConfigObject, metamodelDigest);
            AddRowsForExtensionCategory("featureExtensions", ParserGeneratorValues.FeatureExtensionContextId, ParserGeneratorValues.FeatureExtensionDocRef, streamWriter, languageConfigObject, metamodelDigest);

            streamWriter.Close();
        }

        private static void AddRowsForExtensionCategory(string extensionCategory, string contextIdTemplate, string docRefTemplate, StreamWriter streamWriter, JObject languageConfigObject, MetamodelDigest metamodelDigest)
        {
            foreach (KeyValuePair<string, JToken> extension in (JObject)languageConfigObject[extensionCategory])
            {
                string extensionLabel = extension.Key;
                string extensionName = char.ToUpperInvariant(extensionLabel[0]) + extensionLabel.Substring(1);

                foreach (JToken extensionVersionToken in (JArray)extension.Value)
                {
                    string extensionVersion = ((JValue)extensionVersionToken).Value<string>();
                    string extensionContext = string.Format(contextIdTemplate, extensionLabel, extensionVersion);

                    List<int> dtdlVersions = metamodelDigest.SupplementalTypes.Values.Where(s => s.ExtensionContext == extensionContext).First().CotypeVersions;
                    string dtdlVersionsString = string.Join(", ", dtdlVersions.Select(v => $"[{v}]({string.Format(ParserGeneratorValues.LanguageDocRef, v)})"));

                    int lowestDtdlVersion = dtdlVersions.Min();
                    string extensionUrl = string.Format(docRefTemplate, extensionLabel, extensionVersion, lowestDtdlVersion);

                    streamWriter.WriteLine($"| [{extensionName} v{extensionVersion}]({extensionUrl}) | `{extensionContext}` | {dtdlVersionsString} |");
                }
            }
        }
    }
}
