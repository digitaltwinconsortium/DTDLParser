namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Main program for ParserGenerator.
    /// </summary>
    internal class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                if (args.Count() < 4)
                {
                    Console.WriteLine("ParserGenerator outParserFolder metamodelDigestFile objectModelConventionsFile errorMessagesFile");
                    return 1;
                }

                string outParserFolder = args[0];
                string metamodelDigestFile = args[1];
                string objectModelConventionsFile = args[2];
                string errorMessagesFile = args[3];

                string digestText = File.OpenText(metamodelDigestFile).ReadToEnd();
                JObject metamodelDigestObj = (JObject)JToken.Parse(digestText);
                MetamodelDigest metamodelDigest = new MetamodelDigest(digestText);

                ObjectModelCustomizer objectModelCustomizer = new ObjectModelCustomizer(File.OpenText(objectModelConventionsFile).ReadToEnd());

                NameFormatter.SetObjectModelCustomizer(objectModelCustomizer);

                string errorMessagesText = File.OpenText(errorMessagesFile).ReadToEnd();
                JObject errorMessagesObj = (JObject)JToken.Parse(errorMessagesText);

                CsLibrary parserLibrary = new CsLibrary(outParserFolder, "DTDLParser");
                parserLibrary.Using("System");
                parserLibrary.Using("System.Collections.Generic");
                parserLibrary.Using("System.ComponentModel");
                parserLibrary.Using("System.Globalization");
                parserLibrary.Using("System.Linq");
                parserLibrary.Using("System.Text");
                parserLibrary.Using("System.Text.Json");
                parserLibrary.Using("System.Text.RegularExpressions");
                parserLibrary.Using("System.Threading");
                parserLibrary.Using("System.Threading.Tasks");
                parserLibrary.SubNamespace(ParserGeneratorValues.ElementSubNamespace);

                bool areDynamicExtensionsSupported = metamodelDigest.DtdlVersionsAllowingDynamicExtensions.Any();

                List<ITypeGenerator> typeGenerators = new List<ITypeGenerator>();

                typeGenerators.Add(new ContextCollectionGenerator(metamodelDigest.Contexts, metamodelDigest.SupplementalTypes, metamodelDigest.DtdlVersionsAllowingLocalTerms, metamodelDigest.DtdlVersionsAllowingDynamicExtensions, metamodelDigest.DtdlVersionsRestrictingKeywords, metamodelDigest.ContextsMergeDefinitions, metamodelDigest.ReservedIdPrefixes, metamodelDigest.AffiliateContextsImplicitDtdlVersions, areDynamicExtensionsSupported));
                typeGenerators.Add(new AggregateContextGenerator(metamodelDigest.BaseClass, areDynamicExtensionsSupported, metamodelDigest.SupplementalTypes));
                typeGenerators.Add(new HelpersGenerator(metamodelDigest.BaseClass));
                typeGenerators.Add(new StandardElementCollectionGenerator(metamodelDigest.BaseClass, metamodelDigest.Aliases));
                typeGenerators.Add(new ParsedObjectPropertyInfoGenerator(metamodelDigest.BaseClass));
                typeGenerators.Add(new DtmiLayerInfoGenerator(metamodelDigest.IsLayeringSupported));
                typeGenerators.Add(new DtmiResolverGenerator(metamodelDigest.IsLayeringSupported));
                typeGenerators.Add(new RootableGenerator(metamodelDigest.IsLayeringSupported));
                typeGenerators.Add(new ModelGenerator(metamodelDigest.BaseClass));
                typeGenerators.Add(new ModelParserGenerator(metamodelDigest.BaseClass, areDynamicExtensionsSupported, metamodelDigest.IsLayeringSupported));
                typeGenerators.Add(new ParsingOptionsGenerator(metamodelDigest.IsLayeringSupported, metamodelDigest.DtdlVersions));
                typeGenerators.Add(new SupplementalTypeInfoGenerator(metamodelDigest.BaseClass, metamodelDigest.ExtensionKinds, metamodelDigest.MaterialClasses));
                typeGenerators.Add(new PartitionTypeCollectionGenerator(metamodelDigest.PartitionClasses, metamodelDigest.Contexts, metamodelDigest.PartitionRestrictions, metamodelDigest.DtdlVersions));
                typeGenerators.Add(new RootableTypeCollectionGenerator(metamodelDigest.RootableClasses, metamodelDigest.Contexts));
                typeGenerators.Add(new IdValidatorGenerator(metamodelDigest.IdentifierDefinitionRestrictions, metamodelDigest.IdentifierReferenceRestrictions));
                typeGenerators.Add(new MaterialTypeNameCollectionGenerator(metamodelDigest.MaterialClasses.Keys, metamodelDigest.Contexts.Values));
                typeGenerators.Add(new BaseKindEnumGenerator(metamodelDigest.MaterialClasses, metamodelDigest.BaseClass));
                typeGenerators.Add(new ExtensionKindEnumGenerator(metamodelDigest.ExtensionKinds));
                typeGenerators.Add(new SupplementalTypeCollectionGenerator(metamodelDigest.SupplementalTypes, metamodelDigest.Contexts, metamodelDigest.ExtensibleMaterialClasses, metamodelDigest.BaseClass));
                typeGenerators.Add(new ParsingErrorCollectionGenerator(errorMessagesObj));

                typeGenerators.AddRange(GenerateMaterialClasses(metamodelDigest, objectModelCustomizer));

                foreach (ITypeGenerator typeGenerator in typeGenerators)
                {
                    typeGenerator.GenerateCode(parserLibrary);
                }

                parserLibrary.Generate();

                StreamWriter elementsFile = new StreamWriter(Path.Combine(outParserFolder, ParserGeneratorValues.ElementsFileName), false, Encoding.UTF8);
                elementsFile.WriteLine(metamodelDigestObj["elements"].ToString());
                elementsFile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ParserGenerator failed with exception: {ex.Message}");
                return 1;
            }

            return 0;
        }

        private static List<ITypeGenerator> GenerateMaterialClasses(MetamodelDigest metamodelDigest, ObjectModelCustomizer objectModelCustomizer)
        {
            string baseKindEnum = NameFormatter.FormatNameAsEnum(metamodelDigest.BaseClass);
            string baseKindProperty = NameFormatter.FormatNameAsEnumProperty(metamodelDigest.BaseClass);

            Dictionary<int, List<ExtensibleMaterialClass>> extensibleMaterialClasses = new Dictionary<int, List<ExtensibleMaterialClass>>();
            foreach (KeyValuePair<int, List<string>> kvp in metamodelDigest.ExtensibleMaterialClasses)
            {
                extensibleMaterialClasses[kvp.Key] = new List<ExtensibleMaterialClass>();
                foreach (string extensibleMaterialClassName in kvp.Value)
                {
                    extensibleMaterialClasses[kvp.Key].Add(new ExtensibleMaterialClass(kvp.Key, extensibleMaterialClassName, baseKindEnum));
                }
            }

            DescendantControlFactory descendantControlFactory = new DescendantControlFactory(baseKindEnum, baseKindProperty);

            List<IDescendantControl> descendantControls = new List<IDescendantControl>();
            foreach (DescendantControlDigest descendantControlDigest in metamodelDigest.DescendantControls)
            {
                descendantControls.AddRange(descendantControlFactory.Create(descendantControlDigest));
            }

            Dictionary<string, string> parentMap = metamodelDigest.MaterialClasses.Where(kvp => kvp.Value.ParentClass != null).ToDictionary(kvp => NameFormatter.FormatNameAsClass(kvp.Key), kvp => NameFormatter.FormatNameAsClass(kvp.Value.ParentClass));

            List<ITypeGenerator> typeGenerators = new List<ITypeGenerator>();
            foreach (KeyValuePair<string, MaterialClassDigest> kvp in metamodelDigest.MaterialClasses)
            {
                typeGenerators.Add(new MaterialClass(
                    kvp.Key,
                    kvp.Value.ParentClass,
                    metamodelDigest.BaseClass,
                    kvp.Value,
                    objectModelCustomizer,
                    metamodelDigest.Contexts,
                    metamodelDigest.ClassIdentifierDefinitionRestrictions,
                    extensibleMaterialClasses,
                    parentMap,
                    descendantControls,
                    metamodelDigest.DtdlVersionsWithApocryphalPropertyCotypeDependency,
                    metamodelDigest.Apocrypha,
                    Access.Public,
                    isLayeringSupported: metamodelDigest.IsLayeringSupported));
            }

            typeGenerators.Add(new MaterialClass(
                ParserGeneratorValues.ReferenceObverseName,
                metamodelDigest.BaseClass,
                metamodelDigest.BaseClass,
                new MaterialClassDigest(),
                objectModelCustomizer,
                metamodelDigest.Contexts,
                metamodelDigest.ClassIdentifierDefinitionRestrictions,
                extensibleMaterialClasses,
                parentMap,
                descendantControls,
                metamodelDigest.DtdlVersionsWithApocryphalPropertyCotypeDependency,
                metamodelDigest.Apocrypha,
                Access.Internal,
                isLayeringSupported: metamodelDigest.IsLayeringSupported));

            return typeGenerators;
        }
    }
}
