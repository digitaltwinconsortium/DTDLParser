/* This file was automatically generated from code snippets embedded in file Sample10_ResolveExternalReferencesAsync.md */
/* The associated project file Sample10Async.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample10Async
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample10Async_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample10Async_ObtainDtdlText
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

            #region Snippet:DtdlParserSample10Async_CallParseAsync
            var parseTask = modelParser.ParseAsync(jsonText);
            #endregion

            #region Snippet:DtdlParserSample10Async_CallWait
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

            #region Snippet:DtdlParserSample10Async_ObtainReferencedDtdlText
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

            #region Snippet:DtdlParserSample10Async_NewParserRegisterDtmiResolverAsync
            #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            async IAsyncEnumerable<string> GetJsonTexts(IReadOnlyCollection<Dtmi> dtmis, Dictionary<Dtmi, string> jsonTexts)
            {
                foreach (Dtmi dtmi in dtmis)
                {
                    if (jsonTexts.TryGetValue(dtmi, out string refJsonText))
                    {
                        yield return refJsonText;
                    }
                }
            }
            #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            modelParser = new ModelParser(new ParsingOptions { DtmiResolverAsync = (IReadOnlyCollection<Dtmi> dtmis, CancellationToken _) =>
            {
                return GetJsonTexts(dtmis, otherJsonTexts);
            }});
            #endregion

            #region Snippet:DtdlParserSample10Async_RepeatCallParseAsync
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
        }
    }
}
