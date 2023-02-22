/* This file was automatically generated from code snippets embedded in file Tutorial01_HelloWorldAsync.md */
/* The associated project file Tutorial01Async.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial01Async
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial01Async_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial01Async_ObtainOneDtdlText
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

            #region Snippet:DtdlParserTutorial01Async_CallParseAsyncOnJsonObject
            var parseTask = modelParser.ParseAsync(jsonText1);
            #endregion

            #region Snippet:DtdlParserTutorial01Async_CallWait
            try
            {
                parseTask.Wait();
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is ResolutionException)
                {
                    Console.WriteLine($"DTDL model is referentially incomplete: {ex.InnerException}");
                }
                else if (ex.InnerException is ParsingException)
                {
                    Console.WriteLine($"DTDL model is invalid: {ex.InnerException}");
                }
                else
                {
                    throw;
                }
            }
            #endregion

            #region Snippet:DtdlParserTutorial01Async_ObtainAnotherDtdlText
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

            #region Snippet:DtdlParserTutorial01Async_CallParseAsyncOnJsonArray
            parseTask = modelParser.ParseAsync(jsonText);
            #endregion

            try
            {
                parseTask.Wait();
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is ResolutionException)
                {
                    Console.WriteLine($"DTDL model is referentially incomplete: {ex.InnerException}");
                }
                else if (ex.InnerException is ParsingException)
                {
                    Console.WriteLine($"DTDL model is invalid: {ex.InnerException}");
                }
                else
                {
                    throw;
                }
            }

            #region Snippet:DtdlParserTutorial01Async_ConstructAsyncEnumeration
            string[] jsonTexts = { jsonText1, jsonText2 };

            #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            async IAsyncEnumerable<string> GetJsonTexts(string[] jsonTexts)
            {
                foreach (string jsonText in jsonTexts)
                {
                    yield return jsonText;
                }
            }
            #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            #endregion

            #region Snippet:DtdlParserTutorial01Async_CallParseAsyncWithEnumeration
            parseTask = modelParser.ParseAsync(GetJsonTexts(jsonTexts));
            #endregion

            try
            {
                parseTask.Wait();
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is ResolutionException)
                {
                    Console.WriteLine($"DTDL model is referentially incomplete: {ex.InnerException}");
                }
                else if (ex.InnerException is ParsingException)
                {
                    Console.WriteLine($"DTDL model is invalid: {ex.InnerException}");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
