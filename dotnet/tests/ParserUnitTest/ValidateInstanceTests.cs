using DTDLParser;
using DTDLParser.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserUnitTest
{
    [TestClass]
    public class ValidateInstanceTests
    {

        [TestMethod]
        public void ValidateEnumIntInstance()
        {
            const string dtdl = @"
                {
                  ""@context"": ""dtmi:dtdl:context;2"",
                  ""@id"": ""dtmi:tests:enumInt;1"",
                  ""@type"": ""Interface"",
                  ""displayName"": ""enumInt"",
                  ""contents"": [
                    {
                      ""@type"": ""Property"",
                      ""name"": ""aPropWithEnum"",
                      ""schema"": {
                        ""@type"": ""Object"",
                        ""fields"": [
                          {
                            ""name"": ""anEnumInt"",
                            ""schema"" : {
                              ""@type"": ""Enum"",
                              ""valueSchema"": ""integer"",
                              ""enumValues"": [
                                {
                                  ""name"": ""optionOne"",
                                  ""enumValue"": 1
                                },
                                {
                                  ""name"": ""optionTwo"",
                                  ""enumValue"": 2
                                }
                              ]
                            }
                          }
                        ]
                      }
                    }
                  ]
                }
                ";

            var model = new ModelParser().Parse(dtdl)[new Dtmi("dtmi:tests:enumInt;1")] as DTInterfaceInfo;
            var anEnumIntSchema = model.Properties["aPropWithEnum"].Schema;
            string instanceOk = @"
            {
                ""anEnumInt"" : 2
            }";
            var validations = anEnumIntSchema.ValidateInstance(instanceOk);
            foreach (var validation in validations) Assert.AreEqual("", validation);
            Assert.AreEqual(0, validations.Count);

            string instanceBad = @"
            {
                ""anEnumInt"" : 4
            }";
            validations = anEnumIntSchema.ValidateInstance(instanceBad);
            foreach (var validation in validations) Assert.AreEqual("\"anEnumInt\" does not match any value in the Enum", validation);
            Assert.AreEqual(1, validations.Count);

        }

        [TestMethod]
        public void ValidateEnumStringInstance ()
        {
            const string dtdl = @"
             {
              ""@context"": ""dtmi:dtdl:context;2"",
              ""@id"": ""dtmi:tests:enumString;1"",
              ""@type"": ""Interface"",
              ""displayName"": ""enumString"",
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""aPropWithEnum"",
                  ""schema"": {
                    ""@type"": ""Object"",
                    ""fields"": [
                      {
                        ""name"": ""anEnumString"",
                        ""schema"" : {
                          ""@type"": ""Enum"",
                          ""valueSchema"": ""string"",
                          ""enumValues"": [
                            {
                              ""name"": ""optionOne"",
                              ""enumValue"": ""one""
                            },
                            {
                              ""name"": ""optionTwo"",
                              ""enumValue"": ""two""
                            }
                          ]
                        }
                      }
                    ]
                  }
                }
              ]
            }
            ";
            var model = new ModelParser().Parse(dtdl)[new Dtmi("dtmi:tests:enumString;1")] as DTInterfaceInfo;
            var anEnumStringSchema = model.Properties["aPropWithEnum"].Schema;
            
            string instanceOk = @"
            {
                ""anEnumString"" : ""two""
            }";
            var validations = anEnumStringSchema.ValidateInstance(instanceOk);
            foreach (var validation in validations) Assert.AreEqual("", validation);
            Assert.AreEqual(0, validations.Count);

            string instanceFails = @"
            {
                ""anEnumString"" : ""four""
            }";
            validations = anEnumStringSchema.ValidateInstance(instanceFails);
            foreach (var validation in validations) Assert.AreEqual("\"anEnumString\" does not match any value in the Enum", validation);
            Assert.AreEqual(1, validations.Count);

        }

        [TestMethod]
        public void ValidateDuration()
        {
            const string dtdl = @"
            {
              ""@context"": ""dtmi:dtdl:context;2"",
              ""@id"": ""dtmi:tests:duration;1"",
              ""@type"": ""Interface"",
              ""displayName"": ""duration"",
              ""contents"": [
                {
                  ""@type"": ""Property"",
                  ""name"": ""aPropWithDuration"",
                  ""schema"": {
                    ""@type"": ""Object"",
                    ""fields"": [
                      {
                        ""name"": ""aDuration"",
                        ""schema"": ""duration""
                      }
                    ]
                  }
                }
              ]
            }
            ";
            var model = new ModelParser().Parse(dtdl)[new Dtmi("dtmi:tests:duration;1")] as DTInterfaceInfo;
            var aPropDurationSchema = model.Properties["aPropWithDuration"].Schema;

            string instanceOk = @"
            {
                ""aDuration"" : ""PT1M""
            }";
            var validations = aPropDurationSchema.ValidateInstance(instanceOk);
            foreach (var validation in validations) Assert.AreEqual("", validation);
            Assert.AreEqual(0, validations.Count);

            string instanceFails = @"
            {
                ""aDuration"" : ""notISO""
            }";
            validations = aPropDurationSchema.ValidateInstance(instanceFails);
            foreach (var validation in validations) Assert.AreEqual("\"notISO\" does not conform to the ISO 8601 definition of 'duration'", validation);
            Assert.AreEqual(1, validations.Count);
        }
    }
}
