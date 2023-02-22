/* This file was automatically generated from code snippets embedded in file Tutorial14_ValidateDataAgainstSchemaDefinitions.md */
/* The associated project file Tutorial14.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial14
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial14_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial14_ObtainDtdlText
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;3"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""installation"",
                  ""schema"": {
                    ""@type"": ""Object"",
                    ""fields"": [
                      {
                        ""name"": ""revision"",
                        ""schema"": ""integer""
                      },
                      {
                        ""name"": ""installed"",
                        ""schema"": ""date""
                      },
                      {
                        ""name"": ""note"",
                        ""schema"": ""string""
                      }
                    ]
                  }
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial14_CallParse
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
            #endregion

            #region Snippet:DtdlParserTutorial14_DisplayElements
            Console.WriteLine($"{objectModel.Count} elements in model:");
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine(modelElement.Value.EntityKind.ToString().PadRight(12) + modelElement.Key);
            }
            #endregion

            #region Snippet:DtdlParserTutorial14_GetValidator
            Func<DTEntityInfo, string, string, string> schemaValidator = (s, t, v) =>
            {
                IReadOnlyCollection<string> violations = s.ValidateInstance($"{v}");
                return violations.Any() ? string.Join(" AND ", violations) : $"{v} IS A VALID {t}";
            };
            #endregion

            #region Snippet:DtdlParserTutorial14_GetIntegerAndStringSchemas
            var integerSchema = objectModel[new Dtmi("dtmi:dtdl:instance:Schema:integer;2")];
            var stringSchema = objectModel[new Dtmi("dtmi:dtdl:instance:Schema:string;2")];
            #endregion

            #region Snippet:DtdlParserTutorial14_DisplayIntegerAndStringValidation
            Console.WriteLine(schemaValidator(integerSchema, "INTEGER", "3"));
            Console.WriteLine(schemaValidator(stringSchema, "STRING", "3"));
            Console.WriteLine();

            Console.WriteLine(schemaValidator(integerSchema, "INTEGER", "\"3\""));
            Console.WriteLine(schemaValidator(stringSchema, "STRING", "\"3\""));
            Console.WriteLine();

            Console.WriteLine(schemaValidator(integerSchema, "INTEGER", "3.0"));
            Console.WriteLine(schemaValidator(stringSchema, "STRING", "3.0"));
            Console.WriteLine();

            Console.WriteLine(schemaValidator(integerSchema, "INTEGER", "\"hello\""));
            Console.WriteLine(schemaValidator(stringSchema, "STRING", "\"hello\""));
            #endregion

            #region Snippet:DtdlParserTutorial14_GetDateValidator
            var dateSchema = objectModel[new Dtmi("dtmi:dtdl:instance:Schema:date;2")];
            #endregion

            #region Snippet:DtdlParserTutorial14_DisplayDateValidation
            Console.WriteLine(schemaValidator(dateSchema, "DATE", "\"2017-05-29\""));
            Console.WriteLine(schemaValidator(dateSchema, "DATE", "2017-05-29"));
            Console.WriteLine(schemaValidator(dateSchema, "DATE", "\"17-05-29\""));
            Console.WriteLine(schemaValidator(dateSchema, "DATE", "\"20170529\""));
            Console.WriteLine(schemaValidator(dateSchema, "DATE", "\"2017-15-29\""));
            Console.WriteLine(schemaValidator(dateSchema, "DATE", "\"2017-02-29\""));
            Console.WriteLine(schemaValidator(dateSchema, "DATE", "\"2016-02-29\""));
            #endregion

            #region Snippet:DtdlParserTutorial14_GetInstallationObject
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];

            string installationName = "installation";
            var installation = (DTPropertyInfo)anInterface.Contents[installationName];
            #endregion

            #region Snippet:DtdlParserTutorial14_DisplayObjectValidation
            Console.WriteLine(schemaValidator(installation.Schema, "INSTALLATION",
                "{ \"revision\": 3, \"installed\": \"2017-05-29\", \"note\": \"easy breezy\" }"));
            Console.WriteLine(schemaValidator(installation.Schema, "INSTALLATION",
                "{ \"revision\": 3, \"installed\": \"2017-05-29\", \"note\": \"whoops\", \"foo\": \"bar\" }"));
            Console.WriteLine(schemaValidator(installation.Schema, "INSTALLATION",
                "{ \"revision\": 3, \"installed\": \"2017-05-29\" }"));
            Console.WriteLine(schemaValidator(installation.Schema, "INSTALLATION",
              "{ \"revision\": 3, \"installed\": \"17-05-29\" }"));
            Console.WriteLine(schemaValidator(installation.Schema, "INSTALLATION", "{ }"));
            #endregion

            #region Snippet:DtdlParserTutorial14_DisplayMultipleViolations
            Console.WriteLine(schemaValidator(installation.Schema, "INSTALLATION",
                "{ \"revision\": 3, \"installed\": \"17-05-29\", \"note\": \"whoops\", \"foo\": \"bar\" }"));
            #endregion
        }
    }
}
