/* This file was automatically generated from code snippets embedded in file Tutorial05_InspectObjectModelAdvanced.md */
/* The associated project file Tutorial05.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial05
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial05_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial05_ObtainDtdlText
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
                },
                {
                  ""@type"": ""Command"",
                  ""name"": ""setDistance"",
                  ""request"": {
                    ""name"": ""desiredDistance"",
                    ""schema"": ""double""
                  },
                  ""response"": {
                    ""name"": ""reportedDistance"",
                    ""schema"": ""double""
                  }
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial05_CallParse
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = modelParser.Parse(jsonText);
            #endregion

            #region Snippet:DtdlParserTutorial05_DisplayElements
            Console.WriteLine($"{objectModel.Count} elements in model:");
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine(modelElement.Key);
            }
            #endregion

            #region Snippet:DtdlParserTutorial05_DisplayDoubleTerm
            Console.WriteLine(ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:instance:Schema:double;2")));
            #endregion

            #region Snippet:DtdlParserTutorial05_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserTutorial05_DisplayInterfaceContentValuesByKind
            foreach (KeyValuePair<string, DTContentInfo> contentElement in anInterface.Contents)
            {
                switch (contentElement.Value.EntityKind)
                {
                    case DTEntityKind.Property:
                        var propertyElement = (DTPropertyInfo)contentElement.Value;
                        Console.WriteLine($"Property '{propertyElement.Name}'");
                        Console.WriteLine($"  schema: {propertyElement.Schema.Id?.ToString() ?? "(none)"}");
                        Console.WriteLine($"  writable: {(propertyElement.Writable ? "true" : "false")}");
                        break;
                    case DTEntityKind.Telemetry:
                        var telemetryElement = (DTTelemetryInfo)contentElement.Value;
                        Console.WriteLine($"Telemetry '{telemetryElement.Name}'");
                        Console.WriteLine($"  schema: {telemetryElement.Schema.Id?.ToString() ?? "(none)"}");
                        break;
                    case DTEntityKind.Command:
                        var commandElement = (DTCommandInfo)contentElement.Value;
                        Console.WriteLine($"Command '{commandElement.Name}'");
                        Console.WriteLine($"  request schema: {commandElement.Request.Schema.Id?.ToString() ?? "(none)"}");
                        Console.WriteLine($"  response schema: {commandElement.Response.Schema.Id?.ToString() ?? "(none)"}");
                        break;
                    case DTEntityKind.Relationship:
                        var relationshipElement = (DTRelationshipInfo)contentElement.Value;
                        Console.WriteLine($"Relationship '{relationshipElement.Name}'");
                        Console.WriteLine($"  target: {relationshipElement.Target?.ToString() ?? "(none)"}");
                        Console.WriteLine($"  writable: {(relationshipElement.Writable ? "true" : "false")}");
                        break;
                    case DTEntityKind.Component:
                        var componentElement = (DTComponentInfo)contentElement.Value;
                        Console.WriteLine($"Component '{componentElement.Name}'");
                        Console.WriteLine($"  schema: {componentElement.Schema.Id}");
                        break;
                }
            }
            #endregion

            #region Snippet:DtdlParserTutorial05_DisplayInterfaceSyntheticPropertyValues
            foreach (KeyValuePair<string, DTPropertyInfo> propertyElement in anInterface.Properties)
            {
                Console.WriteLine($"Property '{propertyElement.Value.Name}'");
                Console.WriteLine($"  schema: {propertyElement.Value.Schema.Id?.ToString() ?? "(none)"}");
                Console.WriteLine($"  writable: {(propertyElement.Value.Writable ? "true" : "false")}");
            }

            foreach (KeyValuePair<string, DTTelemetryInfo> telemetryElement in anInterface.Telemetries)
            {
                Console.WriteLine($"Telemetry '{telemetryElement.Value.Name}'");
                Console.WriteLine($"  schema: {telemetryElement.Value.Schema.Id?.ToString() ?? "(none)"}");
            }

            foreach (KeyValuePair<string, DTCommandInfo> commandElement in anInterface.Commands)
            {
                Console.WriteLine($"Command '{commandElement.Value.Name}'");
                Console.WriteLine($"  request schema: {commandElement.Value.Request.Schema.Id?.ToString() ?? "(none)"}");
                Console.WriteLine($"  response schema: {commandElement.Value.Response.Schema.Id?.ToString() ?? "(none)"}");
            }

            foreach (KeyValuePair<string, DTRelationshipInfo> relationshipElement in anInterface.Relationships)
            {
                Console.WriteLine($"Relationship '{relationshipElement.Value.Name}'");
                Console.WriteLine($"  target: {relationshipElement.Value.Target?.ToString() ?? "(none)"}");
                Console.WriteLine($"  writable: {(relationshipElement.Value.Writable ? "true" : "false")}");
            }

            foreach (KeyValuePair<string, DTComponentInfo> componentElement in anInterface.Components)
            {
                Console.WriteLine($"Component '{componentElement.Value.Name}'");
                Console.WriteLine($"  schema: {componentElement.Value.Schema.Id}");
            }
            #endregion

            #region Snippet:DtdlParserTutorial05_DisplayObjectModelEntityProperties
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine($"{modelElement.Key} refers to:");

                TypeInfo typeInfo = modelElement.Value.GetType().GetTypeInfo();
                foreach (MemberInfo memberInfo in typeInfo.DeclaredMembers)
                {
                    if (memberInfo is PropertyInfo propertyInfo)
                    {
                        object propertyValue = propertyInfo.GetValue(modelElement.Value);
                        if (propertyValue is DTEntityInfo refSingle)
                        {
                            Console.WriteLine($"  {refSingle.Id} via member {memberInfo.Name}");
                        }
                        else if (propertyValue is IList refList)
                        {
                            foreach (object refObj in refList)
                            {
                                if (refObj is DTEntityInfo refElement)
                                {
                                    Console.WriteLine($"  {refElement.Id} via member {memberInfo.Name}");
                                }
                            }
                        }
                        else if (propertyValue is IDictionary refDict)
                        {
                            foreach (object refObj in refDict.Values)
                            {
                                if (refObj is DTEntityInfo refElement)
                                {
                                    Console.WriteLine($"  {refElement.Id} via member {memberInfo.Name}");
                                }
                            }
                        }
                    }
                }
            }
            #endregion
        }
    }
}
