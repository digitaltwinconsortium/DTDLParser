/* This file was automatically generated from code snippets embedded in file Sample10_ResolveExternalReferences.md */
/* The associated project file Sample10.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample10
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample10_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample10_ObtainDtdlText
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

            #region Snippet:DtdlParserSample10_CallParse
            try
            {
                var objectModel = modelParser.Parse(jsonText);
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

            #region Snippet:DtdlParserSample10_ObtainReferencedDtdlText
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

            #region Snippet:DtdlParserSample10_NewParserRegisterDtmiResolver
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
                var objectModel = modelParser.Parse(jsonText);
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
        }
    }
}
