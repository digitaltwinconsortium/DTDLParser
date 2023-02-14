namespace DTDLParser
{
    using System;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Main program for RemodelGenerator.
    /// </summary>
    internal class Program
    {
        private static readonly int ExtLength = ".json".Length;

        private static int Main(string[] args)
        {
            try
            {
                if (args.Count() < 4)
                {
                    Console.WriteLine("RemodelGenerator outRemodelFolder metamodelDigestFile objectModelConventionsFile dtdlConfigFile");
                    return 1;
                }

                string outRemodelFolder = args[0];
                string metamodelDigestFile = args[1];
                string objectModelConventionsFile = args[2];
                string dtdlConfigFile = args[3];

                JObject languageConfigObject = (JObject)JToken.Parse(File.OpenText(dtdlConfigFile).ReadToEnd());

                string digestText = File.OpenText(metamodelDigestFile).ReadToEnd();
                MetamodelDigest metamodelDigest = new MetamodelDigest(digestText);

                ObjectModelCustomizer objectModelCustomizer = new ObjectModelCustomizer(File.OpenText(objectModelConventionsFile).ReadToEnd());

                NameFormatter.SetObjectModelCustomizer(objectModelCustomizer);

                RemodelerGenerator remodelGenerator = new RemodelerGenerator(
                    metamodelDigest.DtdlVersions.Max(),
                    metamodelDigest.BaseClass,
                    metamodelDigest.Contexts,
                    metamodelDigest.ReservedIdPrefixes,
                    metamodelDigest.MaterialClasses,
                    metamodelDigest.ExtensibleMaterialClasses,
                    metamodelDigest.DtdlVersionsRestrictingKeywords,
                    (JObject)languageConfigObject["partnerExtensions"]);

                SuggesterGenerator suggesterGenerator = new SuggesterGenerator(metamodelDigest.Apocrypha[metamodelDigest.DtdlVersions.Max()]);

                CsLibrary remodelLibrary = new CsLibrary(outRemodelFolder, "DTDLParser");
                remodelLibrary.Using("System");
                remodelLibrary.Using("System.Collections.Generic");
                remodelLibrary.Using("System.Linq");
                remodelLibrary.Using("Newtonsoft.Json.Linq");
                remodelLibrary.SubNamespace(ParserGeneratorValues.ElementSubNamespace);

                remodelGenerator.GenerateCode(remodelLibrary);
                suggesterGenerator.GenerateCode(remodelLibrary);

                remodelLibrary.Generate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RemodelGenerator failed with exception: {ex.Message}");
                return 1;
            }

            return 0;
        }
    }
}
