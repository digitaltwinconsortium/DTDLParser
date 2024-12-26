/* This file was automatically generated from code snippets embedded in file Tutorial17_UseALimitExtension.md */
/* The associated project file Tutorial17.csproj and expected output file expect.txt were also automatically generated. */

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

namespace Tutorial17
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Snippet:DtdlParserTutorial17_CreateModelParser
            var modelParser = new ModelParser();
            #endregion

            #region Snippet:DtdlParserTutorial17_ObtainDtdlText
            string jsonText =
            @"{
              ""@context"": ""dtmi:dtdl:context;4"",
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""schemas"": [
                {
                  ""@id"": ""dtmi:example:deepArray;1"",
                  ""@type"": ""Array"",
                  ""elementSchema"": {
                    ""@type"": ""Array"",
                    ""elementSchema"": {
                      ""@type"": ""Array"",
                      ""elementSchema"": {
                        ""@type"": ""Array"",
                        ""elementSchema"": {
                          ""@type"": ""Array"",
                          ""elementSchema"": {
                            ""@type"": ""Array"",
                            ""elementSchema"": {
                              ""@type"": ""Array"",
                              ""elementSchema"": {
                                ""@type"": ""Array"",
                                ""elementSchema"": {
                                  ""@type"": ""Array"",
                                  ""elementSchema"": {
                                    ""@type"": ""Array"",
                                    ""elementSchema"": ""double""
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              ]
            }";
            #endregion

            #region Snippet:DtdlParserTutorial17_CallParse
            try
            {
                var objectModel = modelParser.Parse(jsonText);
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

            #region Snippet:DtdlParserTutorial17_AddOnvifContext
            jsonText =
            @"{
              ""@context"": [
                ""dtmi:dtdl:context;4#limitless"",
                ""dtmi:dtdl:limits:onvif;1""
              ],
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""schemas"": [
                {
                  ""@id"": ""dtmi:example:deepArray;1"",
                  ""@type"": ""Array"",
                  ""elementSchema"": {
                    ""@type"": ""Array"",
                    ""elementSchema"": {
                      ""@type"": ""Array"",
                      ""elementSchema"": {
                        ""@type"": ""Array"",
                        ""elementSchema"": {
                          ""@type"": ""Array"",
                          ""elementSchema"": {
                            ""@type"": ""Array"",
                            ""elementSchema"": {
                              ""@type"": ""Array"",
                              ""elementSchema"": {
                                ""@type"": ""Array"",
                                ""elementSchema"": {
                                  ""@type"": ""Array"",
                                  ""elementSchema"": {
                                    ""@type"": ""Array"",
                                    ""elementSchema"": ""double""
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              ]
            }";
            #endregion

            try
            {
                var objectModel = modelParser.Parse(jsonText);
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

            #region Snippet:DtdlParserTutorial17_StandardLimitContext
            jsonText =
            @"{
              ""@context"": [
                ""dtmi:dtdl:context;4#limitless"",
                ""dtmi:dtdl:context;4#limits""
              ],
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""schemas"": [
                {
                  ""@id"": ""dtmi:example:deepArray;1"",
                  ""@type"": ""Array"",
                  ""elementSchema"": {
                    ""@type"": ""Array"",
                    ""elementSchema"": {
                      ""@type"": ""Array"",
                      ""elementSchema"": {
                        ""@type"": ""Array"",
                        ""elementSchema"": {
                          ""@type"": ""Array"",
                          ""elementSchema"": {
                            ""@type"": ""Array"",
                            ""elementSchema"": {
                              ""@type"": ""Array"",
                              ""elementSchema"": {
                                ""@type"": ""Array"",
                                ""elementSchema"": {
                                  ""@type"": ""Array"",
                                  ""elementSchema"": {
                                    ""@type"": ""Array"",
                                    ""elementSchema"": ""double""
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              ]
            }";
            #endregion

            try
            {
                var objectModel = modelParser.Parse(jsonText);
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

            #region Snippet:DtdlParserTutorial17_NewParserWithOptions
            ParsingOptions parsingOptions = new ParsingOptions();
            parsingOptions.ExtensionLimitContexts.Add(new Dtmi("dtmi:dtdl:limits:onvif"));
            modelParser = new ModelParser(parsingOptions);
            #endregion

            jsonText =
            @"{
              ""@context"": [
                ""dtmi:dtdl:context;4#limitless"",
                ""dtmi:dtdl:limits:onvif;1""
              ],
              ""@id"": ""dtmi:example:anInterface;1"",
              ""@type"": ""Interface"",
              ""schemas"": [
                {
                  ""@id"": ""dtmi:example:deepArray;1"",
                  ""@type"": ""Array"",
                  ""elementSchema"": {
                    ""@type"": ""Array"",
                    ""elementSchema"": {
                      ""@type"": ""Array"",
                      ""elementSchema"": {
                        ""@type"": ""Array"",
                        ""elementSchema"": {
                          ""@type"": ""Array"",
                          ""elementSchema"": {
                            ""@type"": ""Array"",
                            ""elementSchema"": {
                              ""@type"": ""Array"",
                              ""elementSchema"": {
                                ""@type"": ""Array"",
                                ""elementSchema"": {
                                  ""@type"": ""Array"",
                                  ""elementSchema"": {
                                    ""@type"": ""Array"",
                                    ""elementSchema"": ""double""
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              ]
            }";

            try
            {
                var objectModel = modelParser.Parse(jsonText);
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
        }
    }
}
