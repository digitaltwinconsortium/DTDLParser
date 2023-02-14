/* This file was automatically generated from code snippets embedded in file Sample03_PinpointModelingErrorsAsync.md */
/* The associated project file Sample03Async.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample03Async
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample03Async_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample03Async_ObtainInvalidDtdlText
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

            #region Snippet:DtdlParserSample03Async_CreateDtdlParseLocator
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

            #region Snippet:DtdlParserSample03Async_CallParseAsync
            var parseTask = modelParser.ParseAsync(jsonText, locator);
            #endregion

            #region Snippet:DtdlParserSample03Async_CallWait
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

            #region Snippet:DtdlParserSample03Async_CorrectPropertyName
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

            #region Snippet:DtdlParserSample03Async_RepeatCallParseAsync
            parseTask = modelParser.ParseAsync(jsonText, locator);
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

            #region Snippet:DtdlParserSample03Async_CorrectTypeName
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

            parseTask = modelParser.ParseAsync(jsonText, locator);

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

            #region Snippet:DtdlParserSample03Async_AddRequiredProperty
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

            parseTask = modelParser.ParseAsync(jsonText, locator);

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
