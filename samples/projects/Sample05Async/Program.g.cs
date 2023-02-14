/* This file was automatically generated from code snippets embedded in file Sample05_InspectObjectModelAdvancedAsync.md */
/* The associated project file Sample05Async.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Sample05Async
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserSample05Async_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserSample05Async_ObtainDtdlText
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

            #region Snippet:DtdlParserSample05Async_CallParseAsync
            var parseTask = modelParser.ParseAsync(jsonText);
            #endregion

            #region Snippet:DtdlParserSample05Async_CallWait
            parseTask.Wait();
            IReadOnlyDictionary<Dtmi, DTEntityInfo> objectModel = parseTask.Result;
            #endregion

            #region Snippet:DtdlParserSample05Async_DisplayElements
            Console.WriteLine($"{objectModel.Count} elements in model:");
            foreach (KeyValuePair<Dtmi, DTEntityInfo> modelElement in objectModel)
            {
                Console.WriteLine(modelElement.Key);
            }
            #endregion

            #region Snippet:DtdlParserSample05Async_DisplayDoubleTerm
            Console.WriteLine(ModelParser.GetTermOrUri(new Dtmi("dtmi:dtdl:instance:Schema:double;2")));
            #endregion

            #region Snippet:DtdlParserSample05Async_GetInterfaceById
            var anInterfaceId = new Dtmi("dtmi:example:anInterface;1");
            var anInterface = (DTInterfaceInfo)objectModel[anInterfaceId];
            #endregion

            #region Snippet:DtdlParserSample05Async_DisplayInterfaceContentPropertiesByKind
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

            #region Snippet:DtdlParserSample05Async_DisplayObjectModelEntityProperties
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
                            Console.WriteLine($"  {refSingle.Id}");
                        }
                        else if (propertyValue is IList refList)
                        {
                            foreach (object refObj in refList)
                            {
                                if (refObj is DTEntityInfo refElement)
                                {
                                    Console.WriteLine($"  {refElement.Id}");
                                }
                            }
                        }
                        else if (propertyValue is IDictionary refDict)
                        {
                            foreach (object refObj in refDict.Values)
                            {
                                if (refObj is DTEntityInfo refElement)
                                {
                                    Console.WriteLine($"  {refElement.Id}");
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
