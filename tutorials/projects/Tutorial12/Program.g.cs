/* This file was automatically generated from code snippets embedded in file Tutorial12_InspectSupplementalTypesAndPropertiesV2.md */
/* The associated project file Tutorial12.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial12
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial12_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial12_GetSupplementalTypes
            IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> supplementalTypes = modelParser.GetSupplementalTypes();
            #endregion

            #region Snippet:DtdlParserTutorial12_GetDistanceTypeInfo
            DTSupplementalTypeInfo distanceTypeInfo = supplementalTypes.First(t => ModelParser.GetTermOrUri(t.Key) == "Distance" && t.Key.MajorVersion == 2).Value;
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayDistanceTypeAndProperties
            Console.WriteLine($"Distance type = {distanceTypeInfo.Type}");

            Console.WriteLine($"Distance properties:");

            foreach (KeyValuePair<string, DTSupplementalPropertyInfo> distanceProp in distanceTypeInfo.Properties)
            {
                string optional = distanceProp.Value.IsOptional ? "optional" : "required";
                string plural = distanceProp.Value.IsPlural ? "plural" : "singular";
                Dtmi distancePropType = new Dtmi(distanceProp.Value.Type);
                DTExtensionKind kind = supplementalTypes[distancePropType].ExtensionKind;
                Console.WriteLine($"  {distanceProp.Key} => {distanceProp.Value.Type}, {kind}, {optional}, {plural}");
            }
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayUnitTerm
            Console.WriteLine(ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:property:unit;2")));
            #endregion

            #region Snippet:DtdlParserTutorial12_ObtainDtdlText
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;2"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": [ ""Telemetry"", ""Distance"" ],
                  ""name"": ""currentDistance"",
                  ""schema"": ""double"",
                  ""unit"": ""kilometre""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial12_CallParse
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayElements
            Console.WriteLine($"{objectModel.Count} elements in model:");
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine(modelElement.Key);
            }
            #endregion

            #region Snippet:DtdlParserTutorial12_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserTutorial12_GetTelemetryByName
            string currentDistanceName = "currentDistance";
            var currentDistance = (DTTelemetryInfo)anInterface.Contents[currentDistanceName];
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayTelemetryTypes
            Console.WriteLine($"currentDistace primary type = {currentDistance.EntityKind}");
            Console.WriteLine("currentDistance supplemental types = " + string.Join(", ", currentDistance.SupplementalTypes.Select(t => t.ToString())));
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayDistanceTerm
            Console.WriteLine($"term for dtmi:standard:class:Distance;2 is {ModelParser.GetTermOrUri(new Dtmi("dtmi:standard:class:Distance;2"))}");
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayTelemetryProperties
            Console.WriteLine($"currentDistance schema = {currentDistance.Schema.Id}");

            var currentDistanceUnit = (DTUnitInfo)currentDistance.SupplementalProperties["dtmi:dtdl:property:unit;2"];
            Console.WriteLine($"currentDistance unit = {currentDistanceUnit.Id}");
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayKilometreTypes
            Console.WriteLine($"kilometre primary type = {currentDistanceUnit.EntityKind}");
            Console.WriteLine("kilometre supplemental types = " + string.Join(", ", currentDistanceUnit.SupplementalTypes.Select(t => t.ToString())));
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayKilometreProperties
            Console.WriteLine($"kilometre properties:");
            Console.WriteLine($"  displayName: {currentDistanceUnit.DisplayName["en"]}");
            Console.WriteLine($"  symbol: {currentDistanceUnit.Symbol}");
            foreach (KeyValuePair<string, object> kvp in currentDistanceUnit.SupplementalProperties)
            {
                Console.WriteLine($"  {kvp.Key}: {((DTEntityInfo)kvp.Value).Id}");
            }
            #endregion

            #region Snippet:DtdlParserTutorial12_DisplayUnitValueIndepentOfVersion
            KeyValuePair<string, object> unitProperty = currentDistance.SupplementalProperties.FirstOrDefault(p => ModelParser.GetTermOrUri(new Dtmi(p.Key)) == "unit");
            if (unitProperty.Value != null)
            {
                Console.WriteLine($"currentDistance has unit {ModelParser.GetTermOrUri(((DTEntityInfo)unitProperty.Value).Id)}");
            }
            #endregion
        }
    }
}
