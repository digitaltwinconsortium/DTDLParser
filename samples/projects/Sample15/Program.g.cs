/* This file was automatically generated from code snippets embedded in file Sample15_SetLimitOnAllowedDtdlVersion.md */
/* The associated project file Sample15.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample15
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample15_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample15_DtdlV2Text
            string jsonTextV2 =
            @"{
              ""@context"": ""dtmi:dtdl:context;2"",
              ""@id"": ""dtmi:example:anInterface;1"",
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

            #region Snippet:DtdlParserSample15_DtdlV3Text
            string jsonTextV3 =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
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

            #region Snippet:DtdlParserSample15_DtdlV99Text
            string jsonTextV99 =
            @"{
              ""@context"": ""dtmi:dtdl:context;99"",
              ""@id"": ""dtmi:example:anInterface;1"",
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

            #region Snippet:DtdlParserSample15_CallParse
            try
            {
                Console.Write("Parsing DTDL v2 JSON text... ");
                modelParser.Parse(jsonTextV2);
                Console.WriteLine("model is valid!");

                Console.Write("Parsing DTDL v3 JSON text... ");
                modelParser.Parse(jsonTextV3);
                Console.WriteLine("model is valid!");

                Console.Write("Parsing DTDL v99 JSON text... ");
                modelParser.Parse(jsonTextV99);
                Console.WriteLine("model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"model is incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine($"Validation ID = {err.ValidationID}");
                    Console.WriteLine($"Cause of error: {err.Cause}");
                    Console.WriteLine($"Action to fix: {err.Action}");
                }
            }
            #endregion

            #region Snippet:DtdlParserSample15_DisplayParserMaxDtdlVersion
            Console.WriteLine($"MaxDtdlVersion is {modelParser.MaxDtdlVersion}");
            #endregion

            #region Snippet:DtdlParserSample15_NewParsingOptions
            ParsingOptions parsingOptions = new ParsingOptions();
            #endregion

            #region Snippet:DtdlParserSample15_DisplayOptionsMaxDtdlVersion
            Console.WriteLine($"MaxDtdlVersion is {parsingOptions.MaxDtdlVersion}");
            #endregion

            #region Snippet:DtdlParserSample15_NewParserWithClienOptions
            parsingOptions.MaxDtdlVersion = 2;
            modelParser = new ModelParser(parsingOptions);
            #endregion

            Console.WriteLine($"MaxDtdlVersion is {modelParser.MaxDtdlVersion}");

            try
            {
                Console.Write("Parsing DTDL v2 JSON text... ");
                modelParser.Parse(jsonTextV2);
                Console.WriteLine("model is valid!");

                Console.Write("Parsing DTDL v3 JSON text... ");
                modelParser.Parse(jsonTextV3);
                Console.WriteLine("model is valid!");

                Console.Write("Parsing DTDL v99 JSON text... ");
                modelParser.Parse(jsonTextV99);
                Console.WriteLine("model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"model is incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine($"Validation ID = {err.ValidationID}");
                    Console.WriteLine($"Cause of error: {err.Cause}");
                    Console.WriteLine($"Action to fix: {err.Action}");
                }
            }
        }
    }
}
