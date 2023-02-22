/* This file was automatically generated from code snippets embedded in file Tutorial01_HelloWorld.md */
/* The associated project file Tutorial01.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial01
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial01_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial01_ObtainOneDtdlText
            string jsonText1 =
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

            #region Snippet:DtdlParserTutorial01_CallParseOnJsonObject
            try
            {
                var objectModel = modelParser.Parse(jsonText1);
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

            #region Snippet:DtdlParserTutorial01_ObtainAnotherDtdlText
            string jsonText2 =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anotherInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""expectedDistance"",
                  ""schema"": ""double"",
                  ""writable"": true
                }
              ]
            }";

            string jsonText = $"[ {jsonText1}, {jsonText2} ]";
            #endregion

            #region Snippet:DtdlParserTutorial01_CallParseOnJsonArray
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

            #region Snippet:DtdlParserTutorial01_ConstructEnumerationOfDtdlTexts
            string[] jsonTexts = { jsonText1, jsonText2 };
            #endregion

            #region Snippet:DtdlParserTutorial01_CallParseWithEnumeration
            try
            {
                var objectModel = modelParser.Parse(jsonTexts);
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
        }
    }
}
