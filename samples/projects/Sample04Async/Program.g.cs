/* This file was automatically generated from code snippets embedded in file Sample04_InspectObjectModelAsync.md */
/* The associated project file Sample04Async.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample04Async
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample04Async_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample04Async_ObtainDtdlText
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""expectedDistance"",
                  ""schema"": ""double""
                },
                {
                  ""@type"": ""Telemetry"",
                  ""name"": ""currentDistance"",
                  ""schema"": ""double""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserSample04Async_CallParseAsync
            var parseTask = modelParser.ParseAsync(jsonText);
            #endregion

            #region Snippet:DtdlParserSample04Async_CallWait
            parseTask.Wait();
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = parseTask.Result;
            #endregion

            #region Snippet:DtdlParserSample04Async_DisplayElements
            Console.WriteLine($"{objectModel.Count} elements in model:");
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine(modelElement.Key);
            }
            #endregion

            #region Snippet:DtdlParserSample04Async_DisplayDoubleTerm
            Console.WriteLine(ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:instance:Schema:double;2")));
            #endregion

            #region Snippet:DtdlParserSample04Async_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserSample04Async_DisplayInterfaceContentIds
            foreach (KeyValuePair<string, DTContentInfo> contentElement in anInterface.Contents)
            {
                Console.WriteLine($"name \"{contentElement.Key}\" => {contentElement.Value.Id}");
            }
            #endregion

            #region Snippet:DtdlParserSample04Async_DisplayInterfaceContentKinds
            foreach (KeyValuePair<string, DTContentInfo> contentElement in anInterface.Contents)
            {
                Console.WriteLine($"name \"{contentElement.Key}\" => {contentElement.Value.EntityKind}");
            }
            #endregion

            #region Snippet:DtdlParserSample04Async_GetPropertyByName
            string expectedDistanceName = "expectedDistance";
            var expectedDistance = (DTPropertyInfo)anInterface.Contents[expectedDistanceName];
            #endregion

            #region Snippet:DtdlParserSample04Async_GetTelemetryById
            Dtmi currentDistanceId = new Dtmi("dtmi:example:anInterface:_contents:__currentDistance;1");
            var currentDistance = (DTTelemetryInfo)objectModel[currentDistanceId];
            #endregion

            #region Snippet:DtdlParserSample04Async_DisplayContentSchema
            Console.WriteLine($"expectedDistance schema is {expectedDistance.Schema.Id}");
            Console.WriteLine($"currentDistance schema is {currentDistance.Schema.Id}");
            #endregion
        }
    }
}
