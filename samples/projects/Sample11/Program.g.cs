/* This file was automatically generated from code snippets embedded in file Sample11_PinpointErrorsInResolvedModels.md */
/* The associated project file Sample11.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample11
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample11_ObtainInvalidDtdlText
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""extends"": ""dtmi:example:anotherInterface;1"",
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""currentDistance"",
                  ""schema"": ""double""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserSample11_ObtainReferencedDtdlText
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

            #region Snippet:DtdlParserSample11_CreateModelParserWithDtmiResolver
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

            var modelParser = new ModelParser(new ParsingOptions { DtmiResolver = dtmiResolver });
            #endregion

            #region Snippet:DtdlParserSample11_CallParse
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
                Console.WriteLine("DTDL model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine(err);
                }
            }
            #endregion

            #region Snippet:DtdlParserSample11_CreateDtdlParseLocator
            DtdlParseLocator parseLocator = (
                int parseIndex,
                int parseLine,
                out string sourceName,
                out int sourceLine) =>
            {
                sourceName = "string variable 'jsonText'";
                sourceLine = parseLine;
                return true;
            };
            #endregion

            #region Snippet:DtdlParserSample11_CallParseWithLocator
            try
            {
                var objectModel = modelParser.Parse(jsonText, parseLocator);
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("DTDL model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine(err);
                }
            }
            #endregion

            #region Snippet:DtdlParserSample11_RegisterDtdlResolveLocator
            DtdlResolveLocator resolveLocator = (
                Dtmi resolveDtmi,
                int resolveLine,
                out string sourceName,
                out int sourceLine) =>
            {
                sourceName = $"dictionary entry 'otherJsonTexts[new Dtmi(\"{resolveDtmi}\")]'";
                sourceLine = resolveLine;
                return true;
            };

            modelParser = new ModelParser(new ParsingOptions { DtmiResolver = dtmiResolver, DtdlResolveLocator = resolveLocator });
            #endregion

            try
            {
                var objectModel = modelParser.Parse(jsonText, parseLocator);
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("DTDL model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine(err);
                }
            }

            #region Snippet:DtdlParserSample11_CorrectPropertyName
            jsonText =
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

            try
            {
                var objectModel = modelParser.Parse(jsonText, parseLocator);
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("DTDL model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine(err);
                }
            }
        }
    }
}
