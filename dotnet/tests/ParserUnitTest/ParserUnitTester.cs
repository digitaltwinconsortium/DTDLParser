namespace ParserUnitTest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using DTDLParser;
    using DTDLParser.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A class for testing the <see cref="ModelParser"/> using JSON test cases.
    /// </summary>
    internal static partial class ParserUnitTester
    {
        private const string ClassPrefix = "DT";
        private const string ClassSuffix = "Info";

        /// <summary>
        /// Execute a test against the <see cref="ModelParser"/>.
        /// </summary>
        /// <param name="testText">JSON text of the test to execute.</param>
        /// <param name="useAsyncApi">Use the asynchronous parse/resolve API.</param>
        /// <param name="suppressResubmission">True if the test should suppress calling GetDtdl() on the parsed model and resubmitting the result to the parser.</param>
        public static void ExecuteTest(string testText, bool useAsyncApi, bool suppressResubmission = false)
        {
            JToken testToken = JToken.Parse(testText);

            if (testToken is JObject testCaseObject)
            {
                ExecuteTestCase(testCaseObject, useAsyncApi, suppressResubmission);
            }
            else if (testToken is JArray testCaseArray)
            {
                foreach (JToken testCaseToken in testCaseArray)
                {
                    ExecuteTestCase((JObject)testCaseToken, useAsyncApi, suppressResubmission);
                }
            }
            else
            {
                Assert.Fail("Invalid test text, neither JSON object nor array");
            }
        }

        private static string GetJsonTextFromToken(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.String:
                    return $"\"{token.Value<string>().Replace("\"", "\\\"")}\"";
                case JTokenType.Boolean:
                    return token.Value<bool>() ? "true" : "false";
                default:
                    return token.ToString();
            }
        }

        private static void CheckModelParser(ModelParser modelParser, JObject expectationObject)
        {
            IReadOnlyDictionary<Dtmi, DTSupplementalTypeInfo> supplementalTypes = modelParser.GetSupplementalTypes();

            if (expectationObject.TryGetValue("typeCount", out JToken typeCountToken))
            {
                int typeCount = ((JValue)typeCountToken).Value<int>();
                Assert.AreEqual(typeCount, supplementalTypes.Count());
            }

            JArray typesArray = expectationObject.TryGetValue("types", out JToken typesToken) ? (JArray)typesToken : new JArray();

            foreach (JToken typeToken in typesArray)
            {
                JObject typeObject = (JObject)typeToken;

                Dtmi typeId = new Dtmi(((JValue)typeObject["id"]).Value<string>());

                Assert.IsTrue(supplementalTypes.ContainsKey(typeId), $"{typeId} not found in supplemental types collection.");
                DTSupplementalTypeInfo typeInfo = supplementalTypes[typeId];

                if (typeObject.TryGetValue("abstract", out JToken abstractToken))
                {
                    bool isAbstract = ((JValue)abstractToken).Value<bool>();
                    Assert.AreEqual(isAbstract, typeInfo.IsAbstract);
                }

                if (typeObject.TryGetValue("extensionKind", out JToken extensionKindToken))
                {
                    DTExtensionKind extensionKind = (DTExtensionKind)Enum.Parse(typeof(DTExtensionKind), ((JValue)extensionKindToken).Value<string>());
                    Assert.AreEqual(extensionKind, typeInfo.ExtensionKind);
                }

                if (typeObject.TryGetValue("context", out JToken contextToken))
                {
                    Dtmi contextId = new Dtmi(((JValue)contextToken).Value<string>());
                    Assert.AreEqual(contextId, typeInfo.ContextId);
                }

                if (typeObject.TryGetValue("parent", out JToken parentToken))
                {
                    Dtmi parentTypeId = new Dtmi(((JValue)parentToken).Value<string>());
                    Assert.AreEqual(parentTypeId, typeInfo.ParentType);
                }

                if (typeObject.TryGetValue("properties", out JToken propertiesToken))
                {
                    JObject propertiesObject = (JObject)propertiesToken;
                    Assert.AreEqual(propertiesObject.Count, typeInfo.Properties.Count);

                    foreach (KeyValuePair<string, JToken> kvp in propertiesObject)
                    {
                        Assert.IsTrue(typeInfo.Properties.ContainsKey(kvp.Key), $"Property {typeId} not found in supplemental type {typeId}.");
                        DTSupplementalPropertyInfo propertyInfo = typeInfo.Properties[kvp.Key];

                        JObject propertyObject = (JObject)kvp.Value;

                        if (propertyObject.TryGetValue("type", out JToken propTypeToken))
                        {
                            string propType = ((JValue)propTypeToken).Value<string>();
                            Assert.AreEqual(propType, propertyInfo.Type.AbsoluteUri);
                        }

                        if (propertyObject.TryGetValue("plural", out JToken pluralToken))
                        {
                            bool isPlural = ((JValue)pluralToken).Value<bool>();
                            Assert.AreEqual(isPlural, propertyInfo.IsPlural);
                        }

                        if (propertyObject.TryGetValue("optional", out JToken optionalToken))
                        {
                            bool isOptional = ((JValue)optionalToken).Value<bool>();
                            Assert.AreEqual(isOptional, propertyInfo.IsOptional);
                        }

                        if (propertyObject.TryGetValue("defaultLanguage", out JToken defaultLanguageToken))
                        {
                            string defaultLanguage = ((JValue)defaultLanguageToken).Value<string>();
                            Assert.AreEqual(defaultLanguage, propertyInfo.DefaultLanguage);
                        }

                        if (propertyObject.TryGetValue("dictionaryKey", out JToken dictionaryKeyToken))
                        {
                            string dictionaryKey = ((JValue)dictionaryKeyToken).Value<string>();
                            Assert.AreEqual(dictionaryKey, propertyInfo.DictionaryKey);
                        }
                    }
                }
            }
        }

        private static void CheckObjectModel(TestModel testModel, JObject expectationObject)
        {
            if (expectationObject.ContainsKey("elementCount"))
            {
                int elementCount = ((JValue)expectationObject["elementCount"]).Value<int>();
                Assert.AreEqual(elementCount, testModel.Size);
            }

            JArray elementsArray = expectationObject.ContainsKey("elements") ? (JArray)expectationObject["elements"] : new JArray();

            foreach (JToken elementToken in elementsArray)
            {
                JObject elementObject = (JObject)elementToken;

                Dtmi elementId = new Dtmi(((JValue)elementObject["id"]).Value<string>());
                string elementType = DtdlTypeNameToCsClassName(((JValue)elementObject["type"]).Value<string>());

                Assert.IsTrue(testModel.IsElementInModel(elementId), $"{elementId} not found in model.");
                var element = testModel.GetElement(elementId);

                Assert.AreEqual(elementType, element.GetType().Name);

                JObject propertiesObject = elementObject.ContainsKey("properties") ? (JObject)elementObject["properties"] : new JObject();

                HashSet<string> missingProperties = propertiesObject.Properties().Select(p => p.Name).ToHashSet();

                TypeInfo typeInfo = element.GetType().GetTypeInfo();

                if (elementObject.ContainsKey("supplementalTypes"))
                {
                    PropertyInfo supplementalTypeInfo = (PropertyInfo)typeInfo.DeclaredMembers.Where(m => m.Name == "SupplementalTypes").FirstOrDefault();
                    Assert.IsNotNull(supplementalTypeInfo);

                    IReadOnlyCollection<Dtmi> actualTypes = (IReadOnlyCollection<Dtmi>)supplementalTypeInfo.GetValue(element);
                    AssertListsAreEqual((JArray)elementObject["supplementalTypes"], actualTypes.ToList());
                }

                if (elementObject.ContainsKey("supplementalProperties"))
                {
                    PropertyInfo supplementalPropertyInfo = (PropertyInfo)typeInfo.DeclaredMembers.Where(m => m.Name == "SupplementalProperties").FirstOrDefault();
                    Assert.IsNotNull(supplementalPropertyInfo);

                    Dictionary<string, object> actualProperties = new Dictionary<string, object>((IDictionary<string, object>)supplementalPropertyInfo.GetValue(element));
                    AssertDictionariesAreEqual((JObject)elementObject["supplementalProperties"], actualProperties);
                }

                if (elementObject.ContainsKey("undefinedTypes"))
                {
                    AssertListsAreEqual((JArray)elementObject["undefinedTypes"], element.UndefinedTypes.ToList());
                }

                if (elementObject.ContainsKey("undefinedProperties"))
                {
                    AssertDictionariesAreEqual((JObject)elementObject["undefinedProperties"], new Dictionary<string, JsonElement>(element.UndefinedProperties));
                }

                while (typeInfo != null)
                {
                    foreach (MemberInfo memberInfo in typeInfo.DeclaredMembers)
                    {
                        PropertyInfo propertyInfo = memberInfo as PropertyInfo;
                        if (propertyInfo != null)
                        {
                            string propertyName = CsPropertyNameToDtdlPropertyName(propertyInfo.Name);
                            if (propertiesObject.ContainsKey(propertyName))
                            {
                                missingProperties.Remove(propertyName);

                                object actualValue = propertyInfo.GetValue(element);

                                JToken expectedToken = propertiesObject[propertyName];
                                switch (expectedToken.Type)
                                {
                                    case JTokenType.Null:
                                        Assert.IsNull(actualValue);
                                        break;
                                    case JTokenType.Array:
                                        Assert.IsNotNull(actualValue);
                                        Assert.IsTrue(actualValue is IList);
                                        AssertListsAreEqual((JArray)expectedToken, (IList)actualValue);
                                        break;
                                    case JTokenType.Object:
                                        Assert.IsNotNull(actualValue);
                                        Assert.IsTrue(actualValue is IDictionary);
                                        AssertDictionariesAreEqual((JObject)expectedToken, (IDictionary)actualValue);
                                        break;
                                    default:
                                        Assert.IsNotNull(actualValue);
                                        Assert.IsTrue(expectedToken is JValue);
                                        AssertValuesAreEqual((JValue)expectedToken, actualValue);
                                        break;
                                }
                            }
                        }
                    }

                    typeInfo = typeInfo.BaseType?.GetTypeInfo();
                }

                Assert.IsFalse(missingProperties.Any());
            }
        }

        private static void CheckException(Exception exception, JObject expectationObject)
        {
            if (exception is ParsingException parsingException)
            {
                Assert.IsTrue(expectationObject.ContainsKey("parsingErrors"));

                JArray expectedErrorsArray = (JArray)expectationObject["parsingErrors"];
                IList<ParsingError> actualErrorsList = parsingException.Errors;

                Assert.IsTrue(actualErrorsList.Any());

                foreach (ParsingError actualError in actualErrorsList)
                {
                    JObject matchingErrorObject = GetMatchingExpectedError(expectedErrorsArray, actualError);

                    AssertErrorFieldMatches(matchingErrorObject, "SecondaryID", actualError.SecondaryID?.AbsoluteUri);
                    AssertErrorFieldMatches(matchingErrorObject, "Property", actualError.Property);
                    AssertErrorFieldMatches(matchingErrorObject, "AuxProperty", actualError.AuxProperty);
                    AssertErrorFieldMatches(matchingErrorObject, "Type", actualError.Type);
                    AssertErrorFieldMatches(matchingErrorObject, "Value", actualError.Value);
                    AssertErrorFieldMatches(matchingErrorObject, "Restriction", actualError.Restriction);
                    AssertErrorFieldMatches(matchingErrorObject, "Transformation", actualError.Transformation);
                    AssertErrorFieldMatches(matchingErrorObject, "PrimaryIndex", actualError.PrimaryLocationName);
                    AssertErrorFieldMatches(matchingErrorObject, "PrimaryDtmi", actualError.PrimaryLocationName);
                    AssertErrorFieldMatches(matchingErrorObject, "PrimaryStart", actualError.PrimaryLocationStart.ToString());
                    AssertNumericErrorFieldMatchesOrFollows(matchingErrorObject, "PrimaryEnd", "PrimaryStart", actualError.PrimaryLocationEnd);
                    AssertErrorFieldMatches(matchingErrorObject, "SecondaryIndex", actualError.SecondaryLocationName);
                    AssertErrorFieldMatches(matchingErrorObject, "SecondaryDtmi", actualError.SecondaryLocationName);
                    AssertErrorFieldMatches(matchingErrorObject, "SecondaryStart", actualError.SecondaryLocationStart.ToString());
                    AssertNumericErrorFieldMatchesOrFollows(matchingErrorObject, "SecondaryEnd", "SecondaryStart", actualError.SecondaryLocationEnd);
                }
            }
            else if (exception is ResolutionException resolutionException)
            {
                Assert.IsTrue(expectationObject.ContainsKey("unresolvedIdentifiers"));

                JArray expectedIdentifiersArray = (JArray)expectationObject["unresolvedIdentifiers"];
                IList<Dtmi> actualIdentifiers = resolutionException.UndefinedIdentifiers;

                Assert.AreEqual(expectedIdentifiersArray.Count, actualIdentifiers.Count);

                foreach (JToken expectedIdentifierToken in expectedIdentifiersArray)
                {
                    Uri expectedIdentifier = new Uri(((JValue)expectedIdentifierToken).Value<string>());
                    Assert.IsTrue(actualIdentifiers.Contains(expectedIdentifier));
                }

                if (expectationObject.ContainsKey("unresolvedIdentifierReferences"))
                {
                    JArray expectedIdentifierReferencesArray = (JArray)expectationObject["unresolvedIdentifierReferences"];
                    List<DtmiReference> actualIdentifierReferences = resolutionException.UndefinedIdentifierReferences;

                    Assert.AreEqual(expectedIdentifierReferencesArray.Count, actualIdentifierReferences.Count);

                    foreach (JToken expectedIdentifierReferenceToken in expectedIdentifierReferencesArray)
                    {
                        JObject expectedIdentifierReferenceObj = (JObject)expectedIdentifierReferenceToken;
                        Dtmi expectedRefIdentifier = new Dtmi(((JValue)expectedIdentifierReferenceObj["ID"]).Value<string>());
                        string expectedRefName =
                            expectedIdentifierReferenceObj.TryGetValue("Index", out JToken indexToken) ? ((JValue)indexToken).Value<string>() :
                            ((JValue)expectedIdentifierReferenceObj["Dtmi"]).Value<string>();
                        int expectedRefLine = ((JValue)expectedIdentifierReferenceObj["Line"]).Value<int>();

                        Assert.IsTrue(actualIdentifierReferences.Any(r => r.Identifier == expectedRefIdentifier && r.LocationName == expectedRefName && r.LocationLine == expectedRefLine));
                    }
                }
            }
            else
            {
                Assert.Fail($"Unexpected exception type thrown: {exception}");
            }
        }

        private static void ValidateInstances(TestModel testModel, JArray instancesArray)
        {
            foreach (JToken instanceToken in instancesArray)
            {
                JObject instanceObj = (JObject)instanceToken;
                Dtmi elementId = new Dtmi(((JValue)instanceObj["subject"]).Value<string>());

                var element = testModel.GetElement(elementId);
                string jsonText = ((JValue)instanceObj["submit"]).Value<string>();
                bool shouldThrow = instanceObj.TryGetValue("throws", out JToken throwsToken) ? ((JValue)throwsToken).Value<bool>() : false;
                bool shouldConform = instanceObj.TryGetValue("conforms", out JToken conformsToken) ? ((JValue)conformsToken).Value<bool>() : false;

                try
                {
                    bool doesConform = !element.ValidateInstance(jsonText).Any();
                    if (shouldThrow)
                    {
                        Assert.Fail("Expected validation exception not thrown");
                    }

                    if (shouldConform && !doesConform)
                    {
                        Assert.Fail($"Instance should conform but ValidateInstance() returned false: >>{jsonText}<<");
                    }
                    else if (!shouldConform && doesConform)
                    {
                        Assert.Fail($"Instance should not conform but ValidateInstance() returned true: >>{jsonText}<<");
                    }
                }
                catch (ValidationException)
                {
                    if (!shouldThrow)
                    {
                        Assert.Fail("Validation exception unexpectedly thrown");
                    }
                }
            }
        }

        private static JObject GetMatchingExpectedError(JArray expectedErrorsArray, ParsingError actualError)
        {
            foreach (JToken expectedErrorToken in expectedErrorsArray)
            {
                JObject expectedErrorObject = (JObject)expectedErrorToken;
                if (((JValue)expectedErrorObject["ValidationID"]).Value<string>() == actualError.ValidationID.AbsoluteUri &&
                    (!expectedErrorObject.TryGetValue("PrimaryID", out JToken primaryIdToken) || ((JValue)primaryIdToken).Value<string>() == actualError.PrimaryID?.AbsoluteUri))
                {
                    return expectedErrorObject;
                }
            }

            Assert.Fail($"Actual error with ValidationID={actualError.ValidationID} and PrimaryID={actualError.PrimaryID?.ToString() ?? "null"} is not among expected errors.");
            return null;
        }

        private static void AssertErrorFieldMatches(JObject expectedErrorObject, string fieldName, string actualErrorField)
        {
            if (expectedErrorObject.ContainsKey(fieldName))
            {
                JToken expectedErrorToken = expectedErrorObject[fieldName];
                if (expectedErrorToken.Type == JTokenType.Array)
                {
                    Assert.IsTrue(((JArray)expectedErrorToken).Any(t => ((JValue)t).Value<string>() == actualErrorField));
                }
                else
                {
                    Assert.AreEqual(GetJValueAsString((JValue)expectedErrorToken), actualErrorField);
                }
            }
        }

        private static void AssertNumericErrorFieldMatchesOrFollows(JObject expectedErrorObject, string fieldName, string altFieldName, int actualErrorField)
        {
            if (expectedErrorObject.TryGetValue(fieldName, out JToken expectedValue))
            {
                Assert.AreEqual(((JValue)expectedValue).Value<int>(), actualErrorField);
            }
            else if (expectedErrorObject.TryGetValue(altFieldName, out expectedValue) && actualErrorField != 0)
            {
                Assert.AreEqual(((JValue)expectedValue).Value<int>(), actualErrorField);
            }
        }

        private static void AssertValuesAreEqual(JValue expectedValue, object actualValue)
        {
            Assert.AreEqual(GetJValueAsString(expectedValue), TestModel.GetObjectAsString(actualValue));
        }

        private static void AssertListsAreEqual(JArray expectedArray, IList actualList)
        {
            Assert.AreEqual(expectedArray.Count, actualList.Count);

            List<string> expectedStrings = expectedArray.Select(t => GetJValueAsString((JValue)t)).ToList();
            expectedStrings.Sort();

            List<string> actualStrings = actualList.Cast<object>().Select(o => TestModel.GetObjectAsString(o)).ToList();
            actualStrings.Sort();

            Assert.IsTrue(expectedStrings.SequenceEqual(actualStrings));
        }

        private static void AssertDictionariesAreEqual(JObject expectedObject, IDictionary actualDictionary)
        {
            Assert.AreEqual(expectedObject.Count, actualDictionary.Count);

            foreach (KeyValuePair<string, JToken> expectedPair in expectedObject)
            {
                Assert.IsTrue(actualDictionary.Contains(expectedPair.Key));

                if (expectedPair.Value.Type == JTokenType.Array)
                {
                    if (actualDictionary[expectedPair.Key] is JsonElement jsonElt)
                    {
                        Assert.AreEqual(jsonElt.ValueKind, JsonValueKind.Array);
                        AssertListsAreEqual((JArray)expectedPair.Value, jsonElt.EnumerateArray().ToList());
                    }
                    else if (actualDictionary[expectedPair.Key] is IList actualList)
                    {
                        AssertListsAreEqual((JArray)expectedPair.Value, actualList);
                    }
                    else
                    {
                        Assert.Fail("Actual value of dictionary is not array or list.");
                    }
                }
                else if (expectedPair.Value.Type == JTokenType.Object)
                {
                    Assert.AreEqual(((JsonElement)actualDictionary[expectedPair.Key]).ValueKind, JsonValueKind.Object);
                    AssertDictionariesAreEqual((JObject)expectedPair.Value, ((JsonElement)actualDictionary[expectedPair.Key]).EnumerateObject().ToDictionary(e => e.Name, e => e.Value));
                }
                else
                {
                    Assert.IsTrue(expectedPair.Value is JValue);
                    AssertValuesAreEqual((JValue)expectedPair.Value, actualDictionary[expectedPair.Key]);
                }
            }
        }

        private static string GetJValueAsString(JValue val)
        {
            switch (val.Type)
            {
                case JTokenType.String:
                    return val.Value<string>();
                case JTokenType.Integer:
                    return val.Value<int>().ToString();
                case JTokenType.Float:
                    return val.Value<double>().ToString();
                case JTokenType.Boolean:
                    return val.Value<bool>().ToString();
                case JTokenType.Null:
                    return null;
                default:
                    Assert.Fail($"Unsupported JTokenType in unit test case: {val.Type}");
                    return null;
            }
        }

        private static string DtdlTypeNameToCsClassName(string name)
        {
            return ClassPrefix + char.ToUpperInvariant(name[0]) + name.Substring(1) + ClassSuffix;
        }

        private static string CsPropertyNameToDtdlPropertyName(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        private static bool LocateForParse(int parseIndex, int parseLine, out string sourceName, out int sourceLine)
        {
            sourceName = parseIndex.ToString();
            sourceLine = parseLine;
            return true;
        }

        private static bool LocateForResolve(Dtmi resolveDtmi, int resolveLine, out string sourceName, out int sourceLine)
        {
            sourceName = resolveDtmi.ToString();
            sourceLine = resolveLine;
            return true;
        }
    }
}
