/* This file was automatically generated from code snippets embedded in file Tutorial02_FixInvalidDtdlModelAsync.md */
/* The associated project file Tutorial02Async.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial02Async
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial02Async_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial02Async_ObtainInvalidDtdlText
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

            #region Snippet:DtdlParserTutorial02Async_CallParseAsync
            var parseTask = modelParser.ParseAsync(jsonText);
            #endregion

            #region Snippet:DtdlParserTutorial02Async_CallWait
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
                else if (ex.InnerException is ParsingException pe)
                {
                    Console.WriteLine("DTDL model is invalid:");
                    foreach (ParsingError err in pe.Errors)
                    {
                        Console.WriteLine(err);
                    }
                }
                else
                {
                    throw;
                }
            }
            #endregion

            #region Snippet:DtdlParserTutorial02Async_CorrectPropertyName
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

            #region Snippet:DtdlParserTutorial02Async_RepeatCallParseAsync
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
                else if (ex.InnerException is ParsingException pe)
                {
                    Console.WriteLine("DTDL model is invalid:");
                    foreach (ParsingError err in pe.Errors)
                    {
                        Console.WriteLine(err);
                    }
                }
                else
                {
                    throw;
                }
            }

            #region Snippet:DtdlParserTutorial02Async_CorrectTypeName
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

            parseTask = modelParser.ParseAsync(jsonText);

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
                else if (ex.InnerException is ParsingException pe)
                {
                    Console.WriteLine("DTDL model is invalid:");
                    foreach (ParsingError err in pe.Errors)
                    {
                        Console.WriteLine(err);
                    }
                }
                else
                {
                    throw;
                }
            }

            #region Snippet:DtdlParserTutorial02Async_AddRequiredProperty
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

            parseTask = modelParser.ParseAsync(jsonText);

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
                else if (ex.InnerException is ParsingException pe)
                {
                    Console.WriteLine("DTDL model is invalid:");
                    foreach (ParsingError err in pe.Errors)
                    {
                        Console.WriteLine(err);
                    }
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
