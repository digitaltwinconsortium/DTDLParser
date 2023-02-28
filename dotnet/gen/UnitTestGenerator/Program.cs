namespace DTDLParser
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Main program for UnitTestGenerator.
    /// </summary>
    internal class Program
    {
        private static readonly int ExtLength = ".json".Length;

        private static int Main(string[] args)
        {
            try
            {
                if (args.Count() < 6)
                {
                    Console.WriteLine("UnitTestGenerator outUnitTestFolder testCaseFolder docExampleFolder specTestFolder metamodelDigestFile objectModelConventionsFile");
                    return 1;
                }

                string outUnitTestFolder = args[0];
                string testCaseFolder = args[1];
                string docExampleFolder = args[2];
                string specTestFolder = args[3];
                string metamodelDigestFile = args[4];
                string objectModelConventionsFile = args[5];

                string digestText = File.OpenText(metamodelDigestFile).ReadToEnd();
                MetamodelDigest metamodelDigest = new MetamodelDigest(digestText);

                ObjectModelCustomizer objectModelCustomizer = new ObjectModelCustomizer(File.OpenText(objectModelConventionsFile).ReadToEnd());

                NameFormatter.SetObjectModelCustomizer(objectModelCustomizer);

                bool areDynamicExtensionsSupported = metamodelDigest.DtdlVersionsAllowingDynamicExtensions.Any();

                ParserUnitTesterGenerator parserUnitTesterGenerator = new ParserUnitTesterGenerator(areDynamicExtensionsSupported, metamodelDigest.IsLayeringSupported);
                TestModelGenerator testModelGenerator = new TestModelGenerator(metamodelDigest.BaseClass);
                TestDtmiPartialResolverGenerator testDtmiPartialResolverGenerator = new TestDtmiPartialResolverGenerator(metamodelDigest.IsLayeringSupported);

                ParserUnitTestGenerator parserUnitTestGenerator = new ParserUnitTestGenerator();
                int caseCount = 0;
                foreach (string testCaseFilename in Directory.GetFiles(testCaseFolder, @"*.json"))
                {
                    parserUnitTestGenerator.AddTestCase(Path.GetFileNameWithoutExtension(testCaseFilename));
                    ++caseCount;
                }

                Console.WriteLine($"Generated tests for {caseCount} test cases");

                DocExampleUnitTestGenerator docExampleUnitTestGenerator = new DocExampleUnitTestGenerator();
                int exampleCount = 0;
                foreach (string docExampleFilename in Directory.GetFiles(docExampleFolder, @"*.json"))
                {
                    docExampleUnitTestGenerator.AddTestCase(Path.GetFileNameWithoutExtension(docExampleFilename));
                    ++exampleCount;
                }

                Console.WriteLine($"Generated tests for {exampleCount} documentation examples");

                SpecificationUnitTestGenerator specificationUnitTestGenerator = new SpecificationUnitTestGenerator();
                int stipulationCount = 0;
                foreach (string specTestFilename in Directory.GetFiles(specTestFolder, @"*.json"))
                {
                    specificationUnitTestGenerator.AddTestCase(Path.GetFileNameWithoutExtension(specTestFilename));
                    ++stipulationCount;
                }

                Console.WriteLine($"Generated tests for {stipulationCount} specification stipulations");

                CsLibrary unitTestLibrary = new CsLibrary(outUnitTestFolder, "ParserUnitTest");
                unitTestLibrary.Using("System");
                unitTestLibrary.Using("System.Collections.Generic");
                unitTestLibrary.Using("System.IO");
                unitTestLibrary.Using("System.Linq");
                unitTestLibrary.Using("System.Threading");
                unitTestLibrary.Using("System.Threading.Tasks");
                unitTestLibrary.Using("Microsoft.VisualStudio.TestTools.UnitTesting");
                unitTestLibrary.Using("Newtonsoft.Json.Linq");
                unitTestLibrary.Using("DTDLParser");
                unitTestLibrary.Using("DTDLParser.Models");
                unitTestLibrary.SubNamespace(ParserGeneratorValues.ElementSubNamespace);

                parserUnitTesterGenerator.GenerateCode(unitTestLibrary);
                testModelGenerator.GenerateCode(unitTestLibrary);
                testDtmiPartialResolverGenerator.GenerateCode(unitTestLibrary);

                parserUnitTestGenerator.GenerateCode(unitTestLibrary);
                docExampleUnitTestGenerator.GenerateCode(unitTestLibrary);
                specificationUnitTestGenerator.GenerateCode(unitTestLibrary);

                unitTestLibrary.Generate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UnitTestGenerator failed with exception: {ex.Message}");
                return 1;
            }

            return 0;
        }
    }
}
