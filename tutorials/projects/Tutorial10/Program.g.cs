/* This file was automatically generated from code snippets embedded in file Tutorial10_ResolveExternalReferences.md */
/* The associated project file Tutorial10.csproj and expected output file expect.txt were also automatically generated. */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DTDLParser;
using DTDLParser.Models;

namespace Tutorial10
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial10_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial10_ObtainDtdlText
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""extends"": ""dtmi:example:anotherInterface;1"",
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""expectedDistance"",
                  ""schema"": ""double""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial10_DeclareObjectModelVar
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = null;
            #endregion

            #region Snippet:DtdlParserTutorial10_CallParse
            try
            {
                objectModel = modelParser.Parse(jsonText);
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine($"DTDL model is invalid: {ex}");
            }
            #endregion

            #region Snippet:DtdlParserTutorial10_ObtainReferencedDtdlText
            var otherJsonTexts = new Dictionary<Dtmi, string>();

            otherJsonTexts[new Dtmi("dtmi:example:anotherInterface;1")] =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anotherInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": ""Telemetry"",
                  ""name"": ""currentDistance"",
                  ""schema"": ""double""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial10_NewParserRegisterDtmiResolver
            DtmiResolver dtmiResolver = (IReadOnlyCollection<Dtmi> dtmis) =>
            {
                var refJsonTexts = new List<string>();

                foreach (Dtmi dtmi in dtmis)
                {
                    if (otherJsonTexts.TryGetValue(dtmi, out string refJsonText))
                    {
                        refJsonTexts.Add(refJsonText);
                    }
                }

                return refJsonTexts;
            };

            modelParser = new ModelParser(new ParsingOptions { DtmiResolver = dtmiResolver});
            #endregion

            try
            {
                objectModel = modelParser.Parse(jsonText);
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine($"DTDL model is invalid: {ex}");
            }

            #region Snippet:DtdlParserTutorial10_GetInterfacesById
            var anInterface = (DTInterfaceInfo)objectModel[new Dtmi("dtmi:example:anInterface;1")];
            var anotherInterface = (DTInterfaceInfo)objectModel[new Dtmi("dtmi:example:anotherInterface;1")];
            #endregion

            #region Snippet:DtdlParserTutorial10_DisplayExtendingInterfaces
            if (anInterface.Extends.Any())
            {
                Console.WriteLine($"anInterface extends:");
                foreach (DTInterfaceInfo extendedInterface in anInterface.Extends)
                {
                    Console.WriteLine($"  {extendedInterface.Id}");
                }
            }

            if (anotherInterface.Extends.Any())
            {
                Console.WriteLine($"anotherInterface extends:");
                foreach (DTInterfaceInfo extendedInterface in anotherInterface.Extends)
                {
                    Console.WriteLine($"  {extendedInterface.Id}");
                }
            }
            #endregion

            #region Snippet:DtdlParserTutorial10_DisplayExtendedInterfaces
            if (anInterface.ExtendedBy.Any())
            {
                Console.WriteLine($"anInterface is extended by:");
                foreach (DTInterfaceInfo extendedInterface in anInterface.ExtendedBy)
                {
                    Console.WriteLine($"  {extendedInterface.Id}");
                }
            }

            if (anotherInterface.ExtendedBy.Any())
            {
                Console.WriteLine($"anotherInterface is extended by:");
                foreach (DTInterfaceInfo extendedInterface in anotherInterface.ExtendedBy)
                {
                    Console.WriteLine($"  {extendedInterface.Id}");
                }
            }
            #endregion
        }
    }
}
