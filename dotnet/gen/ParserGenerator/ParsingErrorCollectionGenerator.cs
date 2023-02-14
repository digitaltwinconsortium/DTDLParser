namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Code generator for <c>ParsingErrorCollection</c> partial class.
    /// </summary>
    public class ParsingErrorCollectionGenerator : ITypeGenerator
    {
        private readonly JObject errorMessageObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingErrorCollectionGenerator"/> class.
        /// </summary>
        /// <param name="errorMessagesObj">A <c>JObject</c> containing information for generating parsing error messages.</param>
        public ParsingErrorCollectionGenerator(JObject errorMessagesObj)
        {
            this.errorMessageObj = errorMessagesObj;
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            CsClass collectionClass = parserLibrary.Class(Access.Internal, Novelty.Normal, "ParsingErrorCollection", completeness: Completeness.Partial);
            collectionClass.Summary("A collection of <c>ParsingError</c>.");

            this.GenerateValidationIdFields(collectionClass);

            this.GenerateNotifyMethod(collectionClass);

            this.GenerateQuitMethod(collectionClass);
        }

        private void GenerateValidationIdFields(CsClass collectionClass)
        {
            foreach (KeyValuePair<string, JToken> kvp in this.errorMessageObj)
            {
                collectionClass.Field(
                    Access.Private,
                    "Uri",
                    this.GetValidationId(kvp.Key),
                    $"new Uri(\"dtmi:dtdl:parsingError:{kvp.Key}\")",
                    Multiplicity.Static,
                    Mutability.ReadOnly);
            }
        }

        private void GenerateNotifyMethod(CsClass collectionClass)
        {
            CsMethod notifyMethod = collectionClass.Method(Access.Internal, Novelty.Normal, "void", "Notify");
            notifyMethod.Summary("Generate an appropriate parsing error and add it to the collection.");
            this.AddMethodParameters(notifyMethod);

            this.DeclareSourceVariables(notifyMethod.Body, 1);
            this.DeclareSourceVariables(notifyMethod.Body, 2);

            CsSwitch switchOnValidationCode = notifyMethod.Body.Switch("validationCode");

            foreach (KeyValuePair<string, JToken> kvp in this.errorMessageObj)
            {
                string validationCode = kvp.Key;
                JObject validationObj = (JObject)kvp.Value;

                switchOnValidationCode.Case($"\"{kvp.Key}\"");
                this.CheckForRequiredValues(switchOnValidationCode, validationCode, validationObj);

                if (this.TryGetLocatedScope(switchOnValidationCode, validationObj, out CsIf ifLocated, out int locationCount))
                {
                    this.AddError(ifLocated, validationCode, validationObj, locationCount: locationCount);
                    this.AddError(ifLocated.Else(), validationCode, validationObj, locationCount: 0);
                }
                else
                {
                    this.AddError(switchOnValidationCode.Scope(null), validationCode, validationObj, locationCount: 0);
                }

                switchOnValidationCode.Line("return;");
            }

            switchOnValidationCode.Default().Line("throw new InvalidOperationException($\"Missing error-generation case for validation code {validationCode}.\");");
        }

        private void GenerateQuitMethod(CsClass collectionClass)
        {
            CsMethod quitMethod = collectionClass.Method(Access.Internal, Novelty.Normal, "void", "Quit");
            quitMethod.Summary("Generate an appropriate parsing error, add it to the collection, and throw a <c>ParsingException</c>.");
            this.AddMethodParameters(quitMethod);

            quitMethod.Body
                .Line($"this.Notify({string.Join(", ", quitMethod.ParamNames)});")
                .Line("throw new ParsingException(this.parsingErrors);");
        }

        private void AddMethodParameters(CsMethod method)
        {
            method.Param("string", "validationCode", "A string uniquely identifying the validation condition that identifies the error.");

            method.Param("Uri", "contextId", "A context identifer relevant to the error.", "null");
            method.Param("Uri", "elementId", "A URI that identifies the element in which the error was found.", "null");
            method.Param("Uri", "referenceId", "A URI that is referenced and thereby causes or contributes to the error.", "null");
            method.Param("Uri", "governingId", "A URI of an element that governs the value in which the error was found.", "null");
            method.Param("Uri", "typeId", "A URI that identifies a type relevant to the error.", "null");

            method.Param("string", "identifier", "A string that represents an identifier that is not necessarily valid.", "null");
            method.Param("string", "cotype", "A string representing a required or prohibited co-type on the element in which the error was found.", "null");
            method.Param("string", "relation", "A string describing a relationship to another element that is required or prohibited.", "null");
            method.Param("string", "referenceType", "A string describing a type that is directly or indirectly referenced and thereby causes or contributes to the error.", "null");

            method.Param("string", "propertyName", "A string representing the name of the property among whose values the error was found.", "null");
            method.Param("string", "propertyConjunction", "A string describing a conjunction of property names among whose values the error was found.", "null");
            method.Param("string", "propertyDisjunction", "A string describing a disjunction of property names among whose values the error was found.", "null");
            method.Param("string", "governingPropertyName", "A string representing the name of a property that governs the value in which the error was found.", "null");
            method.Param("string", "constraintName", "A string representing the name of a constraint relevant to the error.", "null");
            method.Param("string", "nestedName", "A string indicating a property name within an element that is a value of the incident property.", "null");
            method.Param("string", "literalPropertyName", "A string indicating a property on a JSON object value that is a localized string or representational literal.", "null");
            method.Param("string", "refPropertyName", "A string representing the name of a property that references another element that has a property that contributes to the error.", "null");

            method.Param("string", "propertyValue", "A string indicating a value of the property that is erroneous.", "null");
            method.Param("string", "constraintValue", "A string indicating a constraint that identifies or relates to the error.", "null");
            method.Param("string", "contextValue", "A string indicating the value of a context specifier.", "null");
            method.Param("string", "termValue", "A string indicating the value of a term definition in a local context block.", "null");
            method.Param("string", "nestedValue", "A string indicating the value of a property within an element that is a value of the incident property.", "null");
            method.Param("string", "refValue", "A string indicating the value of a referenced property value.", "null");
            method.Param("string", "valueConjunction", "A string indicating a conjunction of property values.", "null");

            method.Param("string", "nameValuePair", "A string describing a name-value pair relevant to the error.", "null");

            method.Param("string", "typeRestriction", "A string representing a required or prohibited type of the erroneous value.", "null");
            method.Param("string", "valueRestriction", "A string representing a required or prohibited value for the erroneous value.", "null");
            method.Param("string", "relationRestriction", "A string representing a requirement or prohibition on a relationship to another elemnt.", "null");
            method.Param("string", "versionRestriction", "A string describing a the required or prohibited versions for a value.", "null");

            method.Param("string", "langCode", "A string representing a language code.", "null");
            method.Param("string", "keyword", "A string indicating a JSON-LD keyword relevant to the error.", "null");
            method.Param("string", "pattern", "A string representing a regular expression pattern.", "null");
            method.Param("string", "limit", "A string describing a numeric limit.", "null");
            method.Param("string", "range", "A string describing a range of values.", "null");
            method.Param("string", "datatype", "A string describing a literal datatype.", "null");
            method.Param("string", "elementType", "A string describing a DTDL element type.", "null");
            method.Param("string", "term", "A string indicating a JSON-LD term relevant to the error.", "null");
            method.Param("string", "layer", "The name of the layer in which the error was found.", "null");
            method.Param("string", "version", "A version number related to the error.", "null");

            method.Param("string", "partition", "A string indicating the partition in which the error was found.", "null");
            method.Param("string", "refPartition", "A string indicating a partition that is referenced by the erroneous property.", "null");

            method.Param("IReadOnlyCollection<string>", "violations", "A collection of strings that each indicate a validation failure, if there are any.", "null");

            method.Param("int?", "observedCount", "A numerical value for the observed count of some item.", "null");
            method.Param("int?", "expectedCount", "A numerical value for the expected count of some item.", "null");
            method.Param("int?", "violationCount", "A numerical value for the count of validation failures.", "null");

            method.Param("JsonLdContextComponent", "contextComponent", "A <see cref=\"JsonLdContextComponent\"/> in which the error was found.", "null");
            method.Param("JsonLdElement", "element", "A <see cref=\"JsonLdElement\"/> in which the error was found.", "null");
            method.Param("JsonLdElement", "extantElement", "A <see cref=\"JsonLdElement\"/> that is extant.", "null");
            method.Param("JsonLdElement", "siblingElement", "A <see cref=\"JsonLdElement\"/> that is a sibling of <paramref name=\"element\"/>.", "null");
            method.Param("JsonLdElement", "ancestorElement", "Used in conjunction with <paramref name=\"descendantElement\"/>, a <see cref=\"JsonLdElement\"/> higher in the hierarchy that contains the error.", "null");
            method.Param("JsonLdElement", "descendantElement", "Used in conjunction with <paramref name=\"ancestorElement\"/>, a <see cref=\"JsonLdElement\"/> lower in the hierarchy that contains the error.", "null");
            method.Param("JsonLdProperty", "incidentProperty", "A <see cref=\"JsonLdProperty\"/> in which the error was found.", "null");
            method.Param("JsonLdProperty", "extantProperty", "A <see cref=\"JsonLdProperty\"/> for an extant property related to the error.", "null");
            method.Param("JsonLdProperty", "governingProperty", "A <see cref=\"JsonLdProperty\"/> for a governing property related to the error.", "null");
            method.Param("JsonLdProperty", "refProperty", "A <see cref=\"JsonLdProperty\"/> for a property that references another element that has a property that contributes to the error.", "null");
            method.Param("JsonLdValueCollection", "incidentValues", "A <see cref=\"JsonLdValueCollection\"/> that holds values of the erroneous property.", "null");
            method.Param("JsonLdValueCollection", "governingValues", "A <see cref=\"JsonLdValueCollection\"/> that holds values of a governing property.", "null");
            method.Param("JsonLdValue", "incidentValue", "A <see cref=\"JsonLdValue\"/> that holds the erroneous property value.", "null");
            method.Param("JsonLdValue", "extantValue", "A <see cref=\"JsonLdValue\"/> that holds an extant value related to the error.", "null");
            method.Param("DtdlExtensionProperty", "extensionProperty", "A <see cref=\"DtdlExtensionProperty\"/> that holds information about a property defined by an extension.", "null");
        }

        private void CheckForRequiredValues(CsScope scope, string validationCode, JObject validationObj)
        {
            IEnumerable<string> requiredParameters = ((JObject)validationObj["required"]).Properties().Select(p => ((JValue)p.Value).Value<string>());
            if (requiredParameters.Any())
            {
                scope.If(string.Join(" || ", requiredParameters.Select(p => $"{p} == null")))
                    .Line($"throw new ArgumentException(\"Missing required parameter {string.Join(" or ", requiredParameters)} when generating {validationCode} ParsingError.\");");
            }
        }

        private bool TryGetLocatedScope(CsScope scope, JObject validationObj, out CsIf ifLocated, out int locationCount)
        {
            JObject optionalObj = (JObject)validationObj["optional"];

            if (optionalObj.TryGetValue("source2", out JToken source2))
            {
                ifLocated = scope.If(this.GetLocationCondition(optionalObj["source1"], 1, validationObj) + " && " + this.GetLocationCondition(source2, 2, validationObj));
                locationCount = 2;
                return true;
            }
            else if (optionalObj.TryGetValue("source1", out JToken source1))
            {
                ifLocated = scope.If(this.GetLocationCondition(source1, 1, validationObj));
                locationCount = 1;
                return true;
            }
            else
            {
                ifLocated = null;
                locationCount = 0;
                return false;
            }
        }

        private string GetLocationCondition(JToken sourceToken, int sourceIndex, JObject validationObj)
        {
            switch (((JValue)sourceToken).Value<string>())
            {
                case "contextComponent":
                    return $"contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "element":
                    return $"element != null && element.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "extantElement":
                    return $"extantElement != null && extantElement.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "siblingElement":
                    return $"siblingElement != null && siblingElement.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "ancestorElement":
                    return $"ancestorElement != null && ancestorElement.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "descendantElement":
                    return $"descendantElement != null && descendantElement.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "incidentProperty":
                    return $"incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "extantProperty":
                    return $"extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "governingProperty":
                    return $"governingProperty != null && governingProperty.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "refProperty":
                    return $"refProperty != null && refProperty.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "incidentValues":
                    return $"incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "governingValues":
                    return $"governingValues != null && governingValues.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "incidentValue":
                    return $"incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "extantValue":
                    return $"extantValue != null && extantValue.TryGetSourceLocation(out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "term":
                    return $"contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "graphProp":
                    return $"element != null && element.TryGetSourceLocationForGraph(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "contextProp":
                    return $"element != null && element.TryGetSourceLocationForContext(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "idProp":
                    return $"element != null && element.TryGetSourceLocationForId(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "typeProp":
                    return $"element != null && element.TryGetSourceLocationForType(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "valueProp":
                    return $"element != null && element.TryGetSourceLocationForValue(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "languageProp":
                    return $"element != null && element.TryGetSourceLocationForLanguage(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "keyword":
                    return $"element != null && element.TryGetSourceLocationForKeyword({this.GetVarOrConst(validationObj, "keyword")}, out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "extantIdProp":
                    return $"extantElement != null && extantElement.TryGetSourceLocationForId(out sourceName{sourceIndex}, out startLine{sourceIndex})";
                case "constraintChildOf":
                    return $"extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint(\"dtmm:childOf\", out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "constraintClass":
                    return $"extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint(\"sh:class\", out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "constraintMaxCount":
                    return $"extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint(\"sh:maxCount\", out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "constraintMinCount":
                    return $"extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint(\"sh:minCount\", out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "constraintPath":
                    return $"extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint(\"sh:path\", out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                case "constraintPattern":
                    return $"extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint(\"sh:pattern\", out sourceName{sourceIndex}, out startLine{sourceIndex}, out endLine{sourceIndex})";
                default:
                    return null;
            }
        }

        private void AddError(CsScope scope, string validationCode, JObject validationObj, int locationCount)
        {
            bool includeLocation = locationCount > 0;

            List<string> argStrings = new List<string>();
            argStrings.Add(this.GetValidationId(validationCode));

            this.AddBuiltArg(scope, argStrings, validationObj, "cause", includeLocation);
            this.AddBuiltArg(scope, argStrings, validationObj, "action", includeLocation);

            this.AddArgs(argStrings, validationObj, "required", "count");
            this.AddArgs(argStrings, validationObj, "optional", "source");

            this.AddSourceArgs(argStrings, locationCount, 1);
            this.AddSourceArgs(argStrings, locationCount, 2);

            this.CallFunction(scope, "this.Add", argStrings);
        }

        private string GetValidationId(string validationCode)
        {
            return char.ToUpperInvariant(validationCode[0]) + validationCode.Substring(1) + "ValidationId";
        }

        private void AddBuiltArg(CsScope scope, List<string> argStrings, JObject validationObj, string baseArgTag, bool includeLocation)
        {
            string locatedArgTag = "located" + char.ToUpperInvariant(baseArgTag[0]) + baseArgTag.Substring(1);
            string validationString = this.GetValidationString(validationObj, includeLocation ? locatedArgTag : baseArgTag, baseArgTag);

            JObject requiredObj = (JObject)validationObj["required"];

            if (requiredObj.Properties().Any(p => p.Name.StartsWith("count")) && validationString.Contains("{count"))
            {
                string builderVar = $"{baseArgTag}Builder";
                scope.Line($"StringBuilder {builderVar} = new StringBuilder({validationString});");

                this.CallSetCount(scope, 1, validationObj, validationString, builderVar);
                this.CallSetCount(scope, 2, validationObj, validationString, builderVar);

                scope.Break();

                argStrings.Add($"{builderVar}.ToString()");
            }
            else
            {
                argStrings.Add(validationString);
            }
        }

        private string GetValidationString(JObject validationObj, string primaryTag, string backupTag)
        {
            return "\"" + ((JValue)(validationObj.TryGetValue(primaryTag, out JToken token) ? token : validationObj[backupTag])).Value<string>() + "\"";
        }

        private void CallSetCount(CsScope scope, int countIndex, JObject validationObj, string validationString, string builderVar)
        {
            if (((JObject)validationObj["required"]).TryGetValue($"count{countIndex}", out JToken countToken) && validationString.Contains($"{{count{countIndex}}}"))
            {
                JObject supportObj = validationObj.TryGetValue("support", out JToken supportToken) ? (JObject)supportToken : new JObject();

                string itemString = this.GetSupportingString(supportObj, "item", countIndex);
                string verbString = this.GetSupportingString(supportObj, "verb", countIndex);

                string countVar = ((JValue)countToken).Value<string>();
                scope.Line($"SetCount({builderVar}, {countIndex}, (int){countVar}{itemString}{verbString});");
            }
        }

        private string GetSupportingString(JObject supportObj, string param, int countIndex)
        {
            if (supportObj.TryGetValue($"{param}{countIndex}", out JToken itemToken))
            {
                JObject itemObj = (JObject)itemToken;
                return $", \"{((JValue)itemObj["singular"]).Value<string>()}\", \"{((JValue)itemObj["plural"]).Value<string>()}\"";
            }
            else
            {
                return string.Empty;
            }
        }

        private void AddArgs(List<string> argStrings, JObject validationObj, string argSetTag, string exclusionPrefix)
        {
            foreach (KeyValuePair<string, JToken> kvp in (JObject)validationObj[argSetTag])
            {
                string paramName = kvp.Key;
                if (!paramName.StartsWith(exclusionPrefix))
                {
                    string paramValue = ((JValue)kvp.Value).Value<string>();
                    argStrings.Add($"{paramName}: {this.GetVarOrConst(validationObj, paramValue)}");
                }
            }
        }

        private void DeclareSourceVariables(CsScope scope, int sourceIndex)
        {
            scope
                .Line($"string sourceName{sourceIndex} = string.Empty;")
                .Line($"int startLine{sourceIndex} = 0;")
                .Line($"int endLine{sourceIndex} = 0;")
                .Break();
        }

        private void AddSourceArgs(List<string> argStrings, int locationCount, int sourceIndex)
        {
            if (locationCount >= sourceIndex)
            {
                argStrings.Add($"sourceName{sourceIndex}: sourceName{sourceIndex}");
                argStrings.Add($"startLine{sourceIndex}: startLine{sourceIndex}");
                argStrings.Add($"endLine{sourceIndex}: endLine{sourceIndex}");
            }
        }

        private void CallFunction(CsScope scope, string func, IEnumerable<string> argStrings)
        {
            CsMultiLine multiLine = scope.MultiLine($"{func}(");

            IEnumerator<string> argEnum = argStrings.GetEnumerator();
            argEnum.MoveNext();
            string argVal = argEnum.Current;
            while (argEnum.MoveNext())
            {
                multiLine.Line($"{argVal},");
                argVal = argEnum.Current;
            }

            multiLine.Line($"{argVal});");
        }

        private string GetVarOrConst(JObject validationObj, string varName)
        {
            if (validationObj.TryGetValue("constOverride", out JToken overrideToken) && ((JObject)overrideToken).TryGetValue(varName, out JToken constToken))
            {
                return $"\"{((JValue)constToken).Value<string>()}\"";
            }
            else
            {
                return varName;
            }
        }
    }
}
