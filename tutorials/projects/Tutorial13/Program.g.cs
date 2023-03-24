/* This file was automatically generated from code snippets embedded in file Tutorial13_InspectSupplementalTypesAndPropertiesV3.md */
/* The associated project file Tutorial13.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial13
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial13_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial13_GetSupplementalTypes
            IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> supplementalTypes = modelParser.GetSupplementalTypes();
            #endregion

            #region Snippet:DtdlParserTutorial13_GetDistanceTypeInfo
            Dtmi quantitativeTypesContext = new Dtmi("dtmi:dtdl:extension:quantitativeTypes;1");
            DTSupplementalTypeInfo distanceTypeInfo = supplementalTypes.First(t => ModelParser.GetTermOrUri(t.Key) == "Distance" && t.Value.ContextId == quantitativeTypesContext).Value;
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayDistanceTypeAndProperties
            Console.WriteLine($"Distance type = {distanceTypeInfo.Type}");

            Console.WriteLine($"Distance properties:");

            foreach (KeyValuePair<string, DTSupplementalPropertyInfo> distanceProp in distanceTypeInfo.Properties)
            {
                string optional = distanceProp.Value.IsOptional ? "optional" : "required";
                string plural = distanceProp.Value.IsPlural ? "plural" : "singular";
                string distanceType = ModelParser.GetTermOrUri(distanceProp.Value.Type);
                Console.WriteLine($"  {distanceProp.Key} => {distanceType}, {optional}, {plural}");
                Console.WriteLine($"    child of: {distanceProp.Value.ChildOf}");
            }
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayUnitTerm
            Console.WriteLine(ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit")));
            #endregion

            #region Snippet:DtdlParserTutorial13_GetImplicitElement
            IReadOnlyDictionary<Dtmi, DTEntityInfo> implicitElements = modelParser.GetImplicitElements();
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayLengthUnits
            Dtmi lengthUnitId = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:LengthUnit");
            DTEnumInfo lengthUnitEnum = (DTEnumInfo)implicitElements[lengthUnitId];
            foreach (DTEnumValueInfo enumVal in lengthUnitEnum.EnumValues)
            {
                Console.WriteLine(ModelParser.GetTermOrUri(enumVal.Id));
            }
            #endregion

            #region Snippet:DtdlParserTutorial13_ObtainDtdlText
            string jsonText =
            @"{
              ""@context"": [
                ""dtmi:dtdl:context;3"",
                ""dtmi:dtdl:extension:quantitativeTypes;1""
              ],
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

            #region Snippet:DtdlParserTutorial13_CallParse
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayElements
            Console.WriteLine($"{objectModel.Count} elements in model:");
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine(modelElement.Key);
            }
            #endregion

            #region Snippet:DtdlParserTutorial13_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserTutorial13_GetTelemetryByName
            string currentDistanceName = "currentDistance";
            var currentDistance = (DTTelemetryInfo)anInterface.Contents[currentDistanceName];
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayTelemetryTypes
            Console.WriteLine($"currentDistace primary type = {currentDistance.EntityKind}");
            Console.WriteLine("currentDistance supplemental types = " + string.Join(", ", currentDistance.SupplementalTypes.Select(t => t.ToString())));
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayDistanceTerm
            Console.WriteLine($"term for dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance is {ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance"))}");
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayTelemetryProperties
            Console.WriteLine($"currentDistance schema = {currentDistance.Schema.Id}");

            var currentDistanceUnit = (DTEnumValueInfo)currentDistance.SupplementalProperties["dtmi:dtdl:extension:quantitativeTypes:v1:property:unit"];
            Console.WriteLine($"currentDistance unit = {currentDistanceUnit.Id}");
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayKilometreTypes
            Console.WriteLine($"kilometre primary type = {currentDistanceUnit.EntityKind}");
            Console.WriteLine("kilometre supplemental types:");
            foreach (Dtmi kilometreSupplementalType in currentDistanceUnit.SupplementalTypes)
            {
                Console.WriteLine($"  {kilometreSupplementalType}");
            }
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayKilometreProperties
            Console.WriteLine($"kilometre properties:");
            Console.WriteLine($"  name: {currentDistanceUnit.Name}");
            Console.WriteLine($"  displayName: {currentDistanceUnit.DisplayName["en"]}");
            foreach (KeyValuePair<string, object> kvp in currentDistanceUnit.SupplementalProperties)
            {
                Console.WriteLine($"  {kvp.Key}: {(kvp.Value is DTEntityInfo entity ? entity.Id : kvp.Value)}");
            }
            #endregion

            #region Snippet:DtdlParserTutorial13_DisplayUnitValueIndepentOfVersion
            KeyValuePair<string, object> unitProperty = currentDistance.SupplementalProperties.FirstOrDefault(p => ModelParser.GetTermOrUri(new Dtmi(p.Key)) == "unit");
            if (unitProperty.Value != null)
            {
                Console.WriteLine($"currentDistance has unit {ModelParser.GetTermOrUri(((DTEntityInfo)unitProperty.Value).Id)}");
            }
            #endregion
        }
    }
}
