/* This file was automatically generated from code snippets embedded in file Tutorial08_InspectComplexSchemasStandard.md */
/* The associated project file Tutorial08.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial08
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial08_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial08_ObtainDtdlTextReferencingGeoPoint
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": ""Telemetry"",
                  ""name"": ""currentLocation"",
                  ""schema"": ""point""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial08_CallParse
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
            #endregion

            #region Snippet:DtdlParserTutorial08_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserTutorial08_GetTelemetryByName
            string currentLocationName = "currentLocation";
            var currentLocation = (DTTelemetryInfo)anInterface.Contents[currentLocationName];
            #endregion

            #region Snippet:DtdlParserTutorial08_DisplayTelemetrySchema
            Console.WriteLine($"currentLocation schema is {currentLocation.Schema.Id}");
            #endregion

            #region Snippet:DtdlParserTutorial08_DisplayTelemetrySchemaTerm
            Console.WriteLine($"currentLocation schema term is {ModelParser.GetTermOrUri(currentLocation.Schema.Id)}");
            #endregion

            #region Snippet:DtdlParserTutorial08_DisplayPointKind
            var pointId = new Dtmi("dtmi:standard:schema:geospatial:point;3");
            var point = objectModel[pointId];
            Console.WriteLine($"point type is {point.EntityKind}");
            #endregion

            #region Snippet:DtdlParserTutorial08_DisplayPointKindIndirect
            Console.WriteLine($"point type is {currentLocation.Schema.EntityKind}");
            #endregion

            #region Snippet:DtdlParserTutorial08_QueryPointObjectFields
            var pointObject = (DTObjectInfo)point;
            DTFieldInfo typeField = pointObject.Fields.First(f => f.Name == "type");
            DTFieldInfo coordinatesField = pointObject.Fields.First(f => f.Name == "coordinates");
            #endregion

            #region Snippet:DtdlParserTutorial08_DisplayType
            Console.WriteLine($"type field has schema type {typeField.Schema.EntityKind}");
            Console.WriteLine($"type field schema has enum value {((DTEnumInfo)typeField.Schema).EnumValues[0].EnumValue}");
            #endregion

            #region Snippet:DtdlParserTutorial08_DisplayCoordinates
            Console.WriteLine($"coordinates field has schema type {coordinatesField.Schema.EntityKind}");
            Console.WriteLine($"coordinates field schema has element schema {((DTArrayInfo)coordinatesField.Schema).ElementSchema.Id}");
            #endregion
        }
    }
}
