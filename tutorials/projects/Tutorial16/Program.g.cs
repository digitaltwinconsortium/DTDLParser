/* This file was automatically generated from code snippets embedded in file Tutorial16_ValidateAndInspectAContextuallyIncompleteModel.md */
/* The associated project file Tutorial16.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial16
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial16_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial16_ObtainDtdlText
            string jsonText =
            @"{
              ""@context"": [ ""dtmi:dtdl:context;3"", ""dtmi:hypothetical:example:schemaEncoding;1"" ],
              ""@id"": ""dtmi:ex:anInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": [ ""Telemetry"", ""EncodedBoolean"" ],
                  ""name"": ""intAsBool"",
                  ""schema"": ""integer"",
                  ""falseValue"": 0,
                  ""trueValue"": 1
                },
                {
                  ""@type"": [ ""Telemetry"", ""EncodedBoolean"" ],
                  ""name"": ""stringAsBool"",
                  ""schema"": ""string"",
                  ""falseValue"": ""FALSE"",
                  ""trueValue"": ""TRUE""
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial16_DeclareObjectModelVar
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = null;
            #endregion

            #region Snippet:DtdlParserTutorial16_CallParse
            try
            {
                objectModel = modelParser.Parse(jsonText);
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("DTDL model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine(err);
                }
            }
            #endregion

            #region Snippet:DtdlParserTutorial16_NewParserSetAllowUndefinedExtensions
            modelParser = new ModelParser(new ParsingOptions() { AllowUndefinedExtensions = true });
            #endregion

            try
            {
                objectModel = modelParser.Parse(jsonText);
                Console.WriteLine($"DTDL model is valid!");
            }
            catch (ResolutionException ex)
            {
                Console.WriteLine($"DTDL model is referentially incomplete: {ex}");
            }
            catch (ParsingException ex)
            {
                Console.WriteLine("DTDL model is invalid:");
                foreach (ParsingError err in ex.Errors)
                {
                    Console.WriteLine(err);
                }
            }

            #region Snippet:DtdlParserTutorial16_DisplayElements
            Console.WriteLine($"{objectModel.Count} elements in model:");
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine(modelElement.Key);
            }
            #endregion

            #region Snippet:DtdlParserTutorial16_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:ex:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserTutorial16_GetTelemetriesByName
            string intAsBoolName = "intAsBool";
            string stringAsBoolName = "stringAsBool";

            var intAsBool = (DTTelemetryInfo)anInterface.Contents[intAsBoolName];
            var stringAsBool = (DTTelemetryInfo)anInterface.Contents[stringAsBoolName];
            #endregion

            #region Snippet:DtdlParserTutorial16_DisplayTelemetryPrimaryTypes
            Console.WriteLine($"intAsBool primary type = {intAsBool.EntityKind}");
            Console.WriteLine($"stringAsBool primary type = {stringAsBool.EntityKind}");
            #endregion

            #region Snippet:DtdlParserTutorial16_DisplayTelemetrySupplementalTypes
            Console.WriteLine("intAsBool supplemental types = " + string.Join(", ", intAsBool.SupplementalTypes.Select(t => t.ToString())));
            Console.WriteLine("stringAsBool supplemental types = " + string.Join(", ", stringAsBool.SupplementalTypes.Select(t => t.ToString())));
            #endregion

            #region Snippet:DtdlParserTutorial16_DisplayTelemetryUndefinedTypes
            Console.WriteLine("intAsBool undefined types = " + string.Join(", ", intAsBool.UndefinedTypes));
            Console.WriteLine("stringAsBool undefined types = " + string.Join(", ", stringAsBool.UndefinedTypes));
            #endregion

            #region Snippet:DtdlParserTutorial16_DisplayTelemetrySupplementalProperties
            Console.WriteLine("intAsBool supplemental properties = " + string.Join(", ", intAsBool.SupplementalProperties.Select(t => t.Key)));
            Console.WriteLine("stringAsBool supplemental properties = " + string.Join(", ", stringAsBool.SupplementalProperties.Select(t => t.Key)));
            #endregion

            #region Snippet:DtdlParserTutorial16_DisplayTelemetryUndefinedProperties
            Console.WriteLine($"intAsBool undefined properties:");
            foreach (KeyValuePair<string, JsonElement> kvp in intAsBool.UndefinedProperties)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine($"stringAsBool undefined properties:");
            foreach (KeyValuePair<string, JsonElement> kvp in stringAsBool.UndefinedProperties)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
            #endregion
        }
    }
}
