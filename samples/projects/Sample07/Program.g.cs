/* This file was automatically generated from code snippets embedded in file Sample07_InspectComplexSchemasReferenced.md */
/* The associated project file Sample07.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample07
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample07_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample07_ObtainDtdlTextContainingMap
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""schemas"": [
                {
                  ""@id"": ""dtmi:example:allotment;1"",
                  ""@type"": ""Map"",
                  ""mapKey"": {
                    ""name"": ""item"",
                    ""schema"": ""string""
                  },
                  ""mapValue"": {
                    ""name"": ""count"",
                    ""schema"": ""integer""
                  }
                }
              ],
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""expectedAllotment"",
                  ""schema"": ""dtmi:example:allotment;1""
                },
                {
                  ""@type"": ""Telemetry"",
                  ""name"": ""runningAllotment"",
                  ""schema"": ""dtmi:example:allotment;1""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserSample07_CallParse
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
            #endregion

            #region Snippet:DtdlParserSample07_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserSample07_GetContentsByName
            string expectedAllotmentName = "expectedAllotment";
            var expectedAllotment = (DTPropertyInfo)anInterface.Contents[expectedAllotmentName];

            string runningAllotmentName = "runningAllotment";
            var runningAllotment = (DTTelemetryInfo)anInterface.Contents[runningAllotmentName];
            #endregion

            #region Snippet:DtdlParserSample07_DisplayContentsSchema
            Console.WriteLine($"expectedAllotment schema is {expectedAllotment.Schema.Id}");
            Console.WriteLine($"runningAllotment schema is {runningAllotment.Schema.Id}");
            #endregion

            #region Snippet:DtdlParserSample07_DisplayAllotmentKind
            var allotmentId = new Dtmi("dtmi:example:allotment;1");
            var allotment = objectModel[allotmentId];
            Console.WriteLine($"allotment type is {allotment.EntityKind}");
            #endregion

            #region Snippet:DtdlParserSample07_DisplayAllotmentKindIndirect
            Console.WriteLine($"allotment type is {expectedAllotment.Schema.EntityKind}");
            Console.WriteLine($"allotment type is {runningAllotment.Schema.EntityKind}");
            #endregion

            #region Snippet:DtdlParserSample07_DisplayAllotmentMapProperties
            var allotmentMap = (DTMapInfo)allotment;

            Console.WriteLine($"map key name is {allotmentMap.MapKey.Name}");
            Console.WriteLine($"map key schema is {allotmentMap.MapKey.Schema.Id}");
            Console.WriteLine($"map value name is {allotmentMap.MapValue.Name}");
            Console.WriteLine($"map value schema is {allotmentMap.MapValue.Schema.Id}");
            #endregion

            #region Snippet:DtdlParserSample07_DisplayAllotmentMapPropertySchemaTerms
            Console.WriteLine($"map key schema term is {ModelParser.GetTermOrUri(allotmentMap.MapKey.Schema.Id)}");
            Console.WriteLine($"map value schema term is {ModelParser.GetTermOrUri(allotmentMap.MapValue.Schema.Id)}");
            #endregion
        }
    }
}
