/* This file was automatically generated from code snippets embedded in file Sample09_CompareElementsInObjectModels.md */
/* The associated project file Sample09.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample09
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample09_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample09_ObtainFirstDtdlText
            string jsonText1 =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""schemas"": [
                {
                  ""@id"": ""dtmi:example:numericArray;1"",
                  ""@type"": ""Array"",
                  ""elementSchema"": ""double""
                }
              ],
              ""contents"": [
                {
                  ""@type"": ""Telemetry"",
                  ""name"": ""distanceLog"",
                  ""schema"": ""dtmi:example:numericArray;1"",
                  ""displayName"": ""distance log""
                },
                {
                  ""@type"": ""Property"",
                  ""name"": ""expectedDistance"",
                  ""schema"": ""double"",
                  ""displayName"": ""expected distance"",
                  ""writable"": true
                },
                {
                  ""@type"": ""Command"",
                  ""name"": ""setDistance"",
                  ""request"": {
                    ""name"": ""desiredDistance"",
                    ""schema"": ""double"",
                    ""displayName"": ""desired distance""
                  },
                  ""response"": {
                    ""name"": ""reportedDistance"",
                    ""schema"": ""double"",
                    ""displayName"": ""reported distance""
                  }
                },
                {
                  ""@type"": ""Relationship"",
                  ""name"": ""proximity"",
                  ""displayName"": {
                    ""en-US"": ""proximity""
                  }
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserSample09_ObtainSecondDtdlText
            string jsonText2 =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""schemas"": [
                {
                  ""@id"": ""dtmi:example:numericArray;1"",
                  ""@type"": ""Array"",
                  ""elementSchema"": ""integer""
                }
              ],
              ""contents"": [
                {
                  ""@type"": ""Telemetry"",
                  ""name"": ""distanceLog"",
                  ""schema"": ""dtmi:example:numericArray;1"",
                  ""displayName"": ""distance log""
                },
                {
                  ""@type"": ""Property"",
                  ""writable"": true,
                  ""schema"": ""double"",
                  ""name"": ""expectedDistance"",
                  ""displayName"": ""expected distance""
                },
                {
                  ""@type"": ""Command"",
                  ""name"": ""setDistance"",
                  ""request"": {
                    ""@id"": ""dtmi:example:desiredDistance;1"",
                    ""name"": ""desiredDistance"",
                    ""schema"": ""double"",
                    ""displayName"": ""desired distance""
                  },
                  ""response"": {
                    ""name"": ""reportedDistance"",
                    ""schema"": ""double"",
                    ""displayName"": {
                      ""en"": ""reported distance""
                    }
                  }
                },
                {
                  ""@type"": ""Relationship"",
                  ""name"": ""proximity"",
                  ""displayName"": {
                    ""en-US"": ""proximity"",
                    ""es-ES"": ""proximidad""
                  }
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserSample09_CallParse
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel1 = modelParser.Parse(jsonText1);
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel2 = modelParser.Parse(jsonText2);
            #endregion

            #region Snippet:DtdlParserSample09_GetInterfacesById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface1 = (DTInterfaceInfo)objectModel1[anInterfaceId];
            var anInterface2 = (DTInterfaceInfo)objectModel2[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserSample09_DisplayInterfaceProperties
            Console.WriteLine($"anInterface1 type = {anInterface1.EntityKind}");
            Console.WriteLine($"anInterface1 schemas = {string.Join(", ", anInterface1.Schemas.Select(s => s.Id.ToString()))}");
            Console.WriteLine($"anInterface1 contents = {string.Join(", ", anInterface1.Contents.Select(c => c.Value.Name))}");

            Console.WriteLine($"anInterface2 type = {anInterface2.EntityKind}");
            Console.WriteLine($"anInterface2 schemas = {string.Join(", ", anInterface2.Schemas.Select(s => s.Id.ToString()))}");
            Console.WriteLine($"anInterface2 contents = {string.Join(", ", anInterface2.Contents.Select(c => c.Value.Name))}");
            #endregion

            #region Snippet:DtdlParserSample09_ShallowCompareInterfaces
            Console.WriteLine($"Interface anInterface1 == anInterface2 = {anInterface1 == anInterface2}");
            #endregion

            #region Snippet:DtdlParserSample09_DeepCompareInterfaces
            Console.WriteLine($"Interface anInterface1.DeepEquals(anInterface2) = {anInterface1.DeepEquals(anInterface2)}");
            #endregion

            #region Snippet:DtdlParserSample09_GetArraysById
            var numericArrayId = new Dtmi("dtmi:example:numericArray;1");
            var numericArray1 = (DTArrayInfo)objectModel1[numericArrayId];
            var numericArray2 = (DTArrayInfo)objectModel2[numericArrayId];
            #endregion

            #region Snippet:DtdlParserSample09_CompareArrays
            Console.WriteLine($"Array numericArray1 == numericArray2 = {numericArray1 == numericArray2}");
            Console.WriteLine($"Array numericArray1.DeepEquals(numericArray2) = {numericArray1.DeepEquals(numericArray2)}");
            #endregion

            #region Snippet:DtdlParserSample09_QueryTelemetriesByName
            var distanceLog1 = (DTTelemetryInfo)anInterface1.Contents.Values.First(c => c.Name == "distanceLog");
            var distanceLog2 = (DTTelemetryInfo)anInterface2.Contents.Values.First(c => c.Name == "distanceLog");
            #endregion

            #region Snippet:DtdlParserSample09_CompareTelemetries
            Console.WriteLine($"Telemetry distanceLog1 == distanceLog2 = {distanceLog1 == distanceLog2}");
            Console.WriteLine($"Telemetry distanceLog1.DeepEquals(distanceLog2) = {distanceLog1.DeepEquals(distanceLog2)}");
            #endregion

            #region Snippet:DtdlParserSample09_QueryAndCompareProperties
            var expectedDistance1 = (DTPropertyInfo)anInterface1.Contents.Values.First(c => c.Name == "expectedDistance");
            var expectedDistance2 = (DTPropertyInfo)anInterface2.Contents.Values.First(c => c.Name == "expectedDistance");

            Console.WriteLine($"Property expectedDistance1 == expectedDistance2 = {expectedDistance1 == expectedDistance2}");
            Console.WriteLine($"Property expectedDistance1.DeepEquals(expectedDistance2) = {expectedDistance1.DeepEquals(expectedDistance2)}");
            #endregion

            #region Snippet:DtdlParserSample09_DisplayCommandProperties
            var setDistance1 = (DTCommandInfo)anInterface1.Contents.Values.First(c => c.Name == "setDistance");
            var setDistance2 = (DTCommandInfo)anInterface2.Contents.Values.First(c => c.Name == "setDistance");

            Console.WriteLine($"Command setDistance1 name = {setDistance1.Name}");
            Console.WriteLine($"Command setDistance1 type = {setDistance1.EntityKind}");
            Console.WriteLine($"Command setDistance1 request = {setDistance1.Request.Id}");
            Console.WriteLine($"Command setDistance1 response = {setDistance1.Response.Id}");

            Console.WriteLine($"Command setDistance2 name = {setDistance2.Name}");
            Console.WriteLine($"Command setDistance2 type = {setDistance2.EntityKind}");
            Console.WriteLine($"Command setDistance2 request = {setDistance2.Request.Id}");
            Console.WriteLine($"Command setDistance2 response = {setDistance2.Response.Id}");
            #endregion

            #region Snippet:DtdlParserSample09_CompareCommands
            Console.WriteLine($"Command setDistance1 == setDistance2 = {setDistance1 == setDistance2}");
            Console.WriteLine($"Command setDistance1.DeepEquals(setDistance2) = {setDistance1.DeepEquals(setDistance2)}");
            #endregion

            #region Snippet:DtdlParserSample09_CompareRequests
            var desiredDistance1 = (DTCommandPayloadInfo)setDistance1.Request;
            var desiredDistance2 = (DTCommandPayloadInfo)setDistance2.Request;

            Console.WriteLine($"Request desiredDistance1 == desiredDistance2 = {desiredDistance1 == desiredDistance2}");
            Console.WriteLine($"Request desiredDistance1.DeepEquals(desiredDistance2) = {desiredDistance1.DeepEquals(desiredDistance2)}");
            #endregion

            #region Snippet:DtdlParserSample09_DisplayResponseDisplayNames
            var reportedDistance1 = (DTCommandPayloadInfo)setDistance1.Response;
            var reportedDistance2 = (DTCommandPayloadInfo)setDistance2.Response;

            Console.WriteLine($"Response reportedDistance1 displayName = {{ {string.Join(", ", reportedDistance1.DisplayName.Select(kvp => $"\"{kvp.Key}\": \"{kvp.Value}\""))} }}");
            Console.WriteLine($"Response reportedDistance2 displayName = {{ {string.Join(", ", reportedDistance2.DisplayName.Select(kvp => $"\"{kvp.Key}\": \"{kvp.Value}\""))} }}");
            #endregion

            #region Snippet:DtdlParserSample09_CompareResponses
            Console.WriteLine($"Response reportedDistance1 == reportedDistance2 = {reportedDistance1 == reportedDistance2}");
            Console.WriteLine($"Response reportedDistance1.DeepEquals(reportedDistance2) = {reportedDistance1.DeepEquals(reportedDistance2)}");
            #endregion

            #region Snippet:DtdlParserSample09_QueryAndCompareRelationships
            var proximity1 = (DTRelationshipInfo)anInterface1.Contents.Values.First(c => c.Name == "proximity");
            var proximity2 = (DTRelationshipInfo)anInterface2.Contents.Values.First(c => c.Name == "proximity");

            Console.WriteLine($"Relationship proximity1 == proximity2 = {proximity1 == proximity2}");
            Console.WriteLine($"Relationship proximity1.DeepEquals(proximity2) = {proximity1.DeepEquals(proximity2)}");
            #endregion
        }
    }
}
