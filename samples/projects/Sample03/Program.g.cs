/* This file was automatically generated from code snippets embedded in file Sample03_PinpointModelingErrors.md */
/* The associated project file Sample03.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample03
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample03_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample03_ObtainInvalidDtdlText
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""content"": [
                {
                  ""@type"": ""Telemtry"",
                  ""name"": ""currentDistance""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserSample03_CreateDtdlParseLocator
            DtdlParseLocator locator = (
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

            #region Snippet:DtdlParserSample03_CallParse
            try
            {
                var objectModel = modelParser.Parse(jsonText, locator);
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

            #region Snippet:DtdlParserSample03_CorrectPropertyName
            jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": ""Telemtry"",
                  ""name"": ""currentDistance""
                }
              ]
            }";
            #endregion

            try
            {
                var objectModel = modelParser.Parse(jsonText, locator);
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

            #region Snippet:DtdlParserSample03_CorrectTypeName
            jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": ""Telemetry"",
                  ""name"": ""currentDistance""
                }
              ]
            }";
            #endregion

            try
            {
                var objectModel = modelParser.Parse(jsonText, locator);
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

            #region Snippet:DtdlParserSample03_AddRequiredProperty
            jsonText =
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

            try
            {
                var objectModel = modelParser.Parse(jsonText, locator);
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
