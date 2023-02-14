namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class for adding code that validates JSON instances against objects of material classes.
    /// </summary>
    public static class MaterialClassValidator
    {
        private const string MatchInstanceMethodName = "DoesInstanceMatch";
        private const string BaseCriteriaText = "conforms to the definition in the DTDL language (for primitive schema types) or to the specific element defined in the model (for complex schema types)";

        /// <summary>
        /// Generate appropriate members for the material class.
        /// </summary>
        /// <param name="dtdlVersions">A list of DTDL major version numbers to generate members for.</param>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="kindProperty">The property on the DTDL base obverse class that indicates the kind of DTDL element.</param>
        /// <param name="classIsBase">True if the material class is the DTDL base class.</param>
        /// <param name="classIsAbstract">True if the material class is abstract.</param>
        /// <param name="instanceValidationDigest">An <see cref="InstanceValidationDigest"/> object providing instance validation criteria for the DTDL type.</param>
        /// <param name="propertyDigests">A dictionary that maps from property name to a <see cref="MaterialPropertyDigest"/> object providing details about the property.</param>
        public static void AddMembers(
            List<int> dtdlVersions,
            CsClass obverseClass,
            string kindProperty,
            bool classIsBase,
            bool classIsAbstract,
            InstanceValidationDigest instanceValidationDigest,
            Dictionary<string, MaterialPropertyDigest> propertyDigests)
        {
            GenerateValidateInstanceStringMethod(obverseClass, classIsBase);
            GenerateValidateInstanceElementMethod(obverseClass, kindProperty, classIsBase, classIsAbstract, instanceValidationDigest?.CriteriaText);
            GenerateValidateInstanceInternalMethod(dtdlVersions, obverseClass, classIsBase, classIsAbstract, instanceValidationDigest?.CriteriaText);
            GenerateMatchInstanceMethod(dtdlVersions, obverseClass, classIsBase, classIsAbstract, instanceValidationDigest?.CriteriaText);

            foreach (int dtdlVersion in dtdlVersions)
            {
                GenerateValidateInstanceVersionMethod(dtdlVersion, obverseClass, classIsAbstract, instanceValidationDigest, propertyDigests);
                GenerateMatchInstanceVersionMethod(dtdlVersion, obverseClass, classIsAbstract, instanceValidationDigest, propertyDigests);
            }
        }

        private static void GenerateValidateInstanceStringMethod(CsClass obverseClass, bool classIsBase)
        {
            if (classIsBase)
            {
                CsMethod method = obverseClass.Method(Access.Public, Novelty.Normal, "IReadOnlyCollection<string>", ParserGeneratorValues.ValidateInstanceMethodName);
                method.Summary($"Validate JSON text to determine whether it {BaseCriteriaText}.");
                method.Param("string", "instanceText", "The JSON text to validate.");
                method.Returns("A list of strings that each indicate a validation failure; the list is empty if the text is valid JSON and conforms.");
                method.Remarks("Most subclasses will throw a <c>ValidationException</c> if this method is called, because they have no instance validator defined.");
                method.Remarks("Conformance is meaningfull only for schema types, so only these classes can validate an instance.");

                method.Body.Line("JsonDocument instanceDoc = null;");
                method.Body.Line("JsonElement instanceElt;");
                method.Body.Try()
                        .Line("instanceDoc = JsonDocument.Parse(instanceText);")
                        .Line("instanceElt = instanceDoc.RootElement.Clone();")
                    .Catch("JsonException")
                        .Line("return new List<string>() { $\">>{instanceText}<< is not valid JSON text\" };")
                    .Finally()
                        .Line("instanceDoc?.Dispose();");
                method.Body.Line("return this.ValidateInstance(instanceElt);");
            }
        }

        private static void GenerateValidateInstanceElementMethod(CsClass obverseClass, string kindProperty, bool classIsBase, bool classIsAbstract, string criteriaText)
        {
            if (classIsBase)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Public, Novelty.Virtual, "IReadOnlyCollection<string>", ParserGeneratorValues.ValidateInstanceMethodName);
                baseClassMethod.Summary($"Validate a <c>JsonElement</c> to determine whether it {BaseCriteriaText}.");
                baseClassMethod.Param("JsonElement", "instanceElt", "The <c>JsonElement</c> to validate.");
                baseClassMethod.Returns("A list of strings that each indicate a validation failure; the list is empty if the <c>JsonElement</c> conforms.");
                baseClassMethod.Remarks("This method will throw a <c>ValidationException</c> for subclasses that have no instance validator defined.");
                baseClassMethod.Remarks("Conformance is meaningfull only for schema types, so only these classes can validate an instance.");

                baseClassMethod.Body.Line($"throw new ValidationException(this.{kindProperty}.ToString());");
            }
            else if (!classIsAbstract && criteriaText != null)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Public, Novelty.Override, "IReadOnlyCollection<string>", ParserGeneratorValues.ValidateInstanceMethodName);
                concreteClassMethod.Summary($"Validate a <c>JsonElement</c> to determine whether it {criteriaText}.");
                concreteClassMethod.Param("JsonElement", "instanceElt", "The <c>JsonElement</c> to validate.");
                concreteClassMethod.Returns("A list of strings that each indicate a validation failure; the list is empty if the <c>JsonElement</c> conforms.");

                concreteClassMethod.Body
                    .Line("List<string> violations = new List<string>();")
                    .Line($"this.{ParserGeneratorValues.ValidateInstanceMethodName}(instanceElt, null, violations);")
                    .Line("return violations;");
            }
        }

        private static void GenerateValidateInstanceInternalMethod(List<int> dtdlVersions, CsClass obverseClass, bool classIsBase, bool classIsAbstract, string criteriaText)
        {
            if (classIsBase)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Virtual, "bool", ParserGeneratorValues.ValidateInstanceMethodName);
                baseClassMethod.Summary($"Validate a <c>JsonElement</c> to determine whether it {BaseCriteriaText}.");
                baseClassMethod.Param("JsonElement", "instanceElt", "The <c>JsonElement</c> to validate.");
                baseClassMethod.Param("string", "instanceName", "If the instance is a property in a JSON object, the name corresponding to <paramref name=\"instanceElt\"/>; otherwise, null.");
                baseClassMethod.Param("List<string>", "violations", "A list of strings to which to add any validation failures.");

                baseClassMethod.Body.Line("return true;");
            }
            else if (!classIsAbstract && criteriaText != null)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, "bool", ParserGeneratorValues.ValidateInstanceMethodName);
                concreteClassMethod.Summary($"Validate a <c>JsonElement</c> to determine whether it {criteriaText}.");
                concreteClassMethod.Param("JsonElement", "instanceElt", "The <c>JsonElement</c> to validate.");
                concreteClassMethod.Param("string", "instanceName", "If the instance is a property in a JSON object, the name corresponding to <paramref name=\"instanceElt\"/>; otherwise, null.");
                concreteClassMethod.Param("List<string>", "violations", "A list of strings to which to add any validation failures.");

                if (dtdlVersions.Any())
                {
                    CsSwitch switchOnDtdlVersion = concreteClassMethod.Body.Switch($"this.{ParserGeneratorValues.DtdlVersionPropertyName}");

                    foreach (int dtdlVersion in dtdlVersions)
                    {
                        switchOnDtdlVersion.Case(dtdlVersion.ToString())
                            .Line($"return this.{ParserGeneratorValues.ValidateInstanceMethodName}V{dtdlVersion}(instanceElt, instanceName, violations);");
                    }
                }

                concreteClassMethod.Body.Line("return true;");
            }
        }

        private static void GenerateValidateInstanceVersionMethod(int dtdlVersion, CsClass obverseClass, bool classIsAbstract, InstanceValidationDigest instanceValidationDigest, Dictionary<string, MaterialPropertyDigest> propertyDigests)
        {
            if (!classIsAbstract && instanceValidationDigest?.CriteriaText != null)
            {
                InstanceConditionDigest elementConditionDigest = instanceValidationDigest.ElementConditions[dtdlVersion];
                InstanceConditionDigest childConditionDigest = instanceValidationDigest.ChildConditions[dtdlVersion];

                CsMethod method = obverseClass.Method(Access.Private, Novelty.Normal, "bool", $"{ParserGeneratorValues.ValidateInstanceMethodName}V{dtdlVersion}");
                method.Param("JsonElement", "instanceElt");
                method.Param("string", "instanceName");
                method.Param("List<string>", "violations");

                AddValidationChecks(dtdlVersion, obverseClass, method.Body, elementConditionDigest, propertyDigests, "instanceElt", "instanceName", "Element", "return false", instanceValidationDigest.TypeText, instanceValidationDigest.ConformanceText);

                if (elementConditionDigest.JsonType == "object")
                {
                    CsForEach forEachProperty = method.Body.ForEach("JsonProperty child in instanceElt.EnumerateObject()");
                    AddValidationChecks(dtdlVersion, obverseClass, forEachProperty, childConditionDigest, propertyDigests, "child.Value", "child.Name", "Child", "continue", string.Empty, string.Empty);
                }
                else if (elementConditionDigest.JsonType == "array")
                {
                    CsForEach forEachProperty = method.Body.ForEach("JsonElement child in instanceElt.EnumerateArray()");
                    AddValidationChecks(dtdlVersion, obverseClass, forEachProperty, childConditionDigest, propertyDigests, "child", "null", "Child", "continue", string.Empty, string.Empty);
                }

                method.Body.Line("return true;");
            }
        }

        private static void GenerateMatchInstanceMethod(List<int> dtdlVersions, CsClass obverseClass, bool classIsBase, bool classIsAbstract, string criteriaText)
        {
            if (classIsBase)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Virtual, "bool", MatchInstanceMethodName);
                baseClassMethod.Summary($"Determine whether a <c>JsonElement</c> matches this DTDL element.");
                baseClassMethod.Param("JsonElement", "instanceElt", "The <c>JsonElement</c> to match.");
                baseClassMethod.Param("string", "instanceName", "If the instance is a property in a JSON object, the name corresponding to <paramref name=\"instanceElt\"/>; otherwise, null.");
                baseClassMethod.Returns("True if the <c>JsonElement</c> matches; false otherwise.");

                baseClassMethod.Body.Line("return false;");
            }
            else if (!classIsAbstract && criteriaText != null)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, "bool", MatchInstanceMethodName);
                concreteClassMethod.Summary($"Determine whether a <c>JsonElement</c> matches this DTDL element.");
                concreteClassMethod.Param("JsonElement", "instanceElt", "The <c>JsonElement</c> to match.");
                concreteClassMethod.Param("string", "instanceName", "If the instance is a property in a JSON object, the name corresponding to <paramref name=\"instanceElt\"/>; otherwise, null.");
                concreteClassMethod.Returns("True if the <c>JsonElement</c> matches; false otherwise.");

                if (dtdlVersions.Any())
                {
                    CsSwitch switchOnDtdlVersion = concreteClassMethod.Body.Switch($"this.{ParserGeneratorValues.DtdlVersionPropertyName}");

                    foreach (int dtdlVersion in dtdlVersions)
                    {
                        switchOnDtdlVersion.Case(dtdlVersion.ToString())
                            .Line($"return this.{MatchInstanceMethodName}V{dtdlVersion}(instanceElt, instanceName);");
                    }
                }

                concreteClassMethod.Body.Line("return false;");
            }
        }

        private static void GenerateMatchInstanceVersionMethod(int dtdlVersion, CsClass obverseClass, bool classIsAbstract, InstanceValidationDigest instanceValidationDigest, Dictionary<string, MaterialPropertyDigest> propertyDigests)
        {
            if (!classIsAbstract && instanceValidationDigest?.CriteriaText != null)
            {
                InstanceConditionDigest elementConditionDigest = instanceValidationDigest.ElementConditions[dtdlVersion];

                CsMethod method = obverseClass.Method(Access.Private, Novelty.Normal, "bool", $"{MatchInstanceMethodName}V{dtdlVersion}");
                method.Param("JsonElement", "instanceElt");
                method.Param("string", "instanceName");

                if (elementConditionDigest.HasValue != null)
                {
                    method.Body.If($"!LiteralValidator.HasValue(instanceElt, this.{NameFormatter.FormatNameAsProperty(elementConditionDigest.HasValue)})")
                        .Line("return false;");
                }

                if (elementConditionDigest.NameHasValue != null)
                {
                    method.Body.If($"instanceName != this.{NameFormatter.FormatNameAsProperty(elementConditionDigest.NameHasValue)}")
                        .Line("return false;");
                }

                method.Body.Line("return true;");
            }
        }

        private static void AddValidationChecks(int dtdlVersion, CsClass obverseClass, CsScope scope, InstanceConditionDigest instanceConditionDigest, Dictionary<string, MaterialPropertyDigest> propertyDigests, string eltVar, string nameVar, string level, string termination, string typeText, string conformanceText)
        {
            if (instanceConditionDigest.JsonType != null)
            {
                AddJsonTypeCheck(scope, instanceConditionDigest.JsonType, eltVar, termination, typeText);
            }

            if (instanceConditionDigest.Datatype != null)
            {
                AddDatatypeCheck(scope, instanceConditionDigest.Datatype, eltVar, termination, conformanceText);
            }

            if (instanceConditionDigest.InstanceProperty != null)
            {
                foreach (string instanceProperty in instanceConditionDigest.InstanceProperty)
                {
                    AddInstancePropertyCheck(scope, instanceProperty, propertyDigests, eltVar, nameVar, termination);
                }
            }

            if (instanceConditionDigest.Pattern != null)
            {
                AddPatternCheck(dtdlVersion, obverseClass, scope, instanceConditionDigest.Pattern, $"{eltVar}.GetString()", $"{level}Value", termination, conformanceText);
            }

            if (instanceConditionDigest.NamePattern != null)
            {
                AddPatternCheck(dtdlVersion, obverseClass, scope, instanceConditionDigest.NamePattern, nameVar, $"{level}Name", termination, conformanceText);
            }

            if (instanceConditionDigest.HasValue != null)
            {
                scope.If($"!LiteralValidator.HasValue({eltVar}, this.{NameFormatter.FormatNameAsProperty(instanceConditionDigest.HasValue)})")
                    .Line($"violations.Add($\">>{{{eltVar}.GetRawText()}}<< does not have value {{this.{NameFormatter.FormatNameAsProperty(instanceConditionDigest.HasValue)}}}\");")
                    .Line($"{termination};");
            }

            if (instanceConditionDigest.NameHasValue != null)
            {
                scope.If($"{nameVar} != this.{NameFormatter.FormatNameAsProperty(instanceConditionDigest.NameHasValue)}")
                    .Line($"violations.Add($\">>{{{nameVar}}}<< does not have value {{this.{NameFormatter.FormatNameAsProperty(instanceConditionDigest.NameHasValue)}}}\");")
                    .Line($"{termination};");
            }
        }

        private static void AddJsonTypeCheck(CsScope scope, string jsonType, string eltVar, string termination, string typeText)
        {
            if (jsonType == "boolean")
            {
                scope.If($"{eltVar}.ValueKind != JsonValueKind.True && {eltVar}.ValueKind != JsonValueKind.False")
                    .Line($"violations.Add($\">>{{{eltVar}.GetRawText()}}<< is not {typeText}\");")
                    .Line($"{termination};");
            }
            else
            {
                scope.If($"{eltVar}.ValueKind != JsonValueKind.{NameFormatter.FormatNameAsEnumValue(jsonType)}")
                    .Line($"violations.Add($\">>{{{eltVar}.GetRawText()}}<< is not {typeText}\");")
                    .Line($"{termination};");
            }
        }

        private static void AddDatatypeCheck(CsScope scope, string datatype, string eltVar, string termination, string conformanceText)
        {
            switch (datatype)
            {
                case "int":
                case "long":
                    scope.If($"!{datatype}.TryParse({eltVar}.GetRawText(), out {datatype} _)")
                        .Line($"violations.Add($\"{{{eltVar}.GetRawText()}} does not conform to {conformanceText}\");")
                        .Line($"{termination};");
                    break;
                case "float":
                case "double":
                    scope.If($"!{datatype}.TryParse({eltVar}.GetRawText(), out {datatype} val) || {datatype}.IsInfinity(val)")
                        .Line($"violations.Add($\"{{{eltVar}.GetRawText()}} does not conform to {conformanceText}\");")
                        .Line($"{termination};");
                    break;
                case "date":
                case "dateTime":
                case "time":
                    scope.If($"!DateTime.TryParse({eltVar}.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out DateTime _)")
                        .Line($"violations.Add($\"\\\"{{{eltVar}.GetString()}}\\\" does not conform to {conformanceText}\");")
                        .Line($"{termination};");
                    break;
            }
        }

        private static void AddInstancePropertyCheck(CsScope scope, string instanceProperty, Dictionary<string, MaterialPropertyDigest> propertyDigests, string eltVar, string nameVar, string termination)
        {
            string propertyName = NameFormatter.FormatNameAsProperty(instanceProperty);
            string matchName = $"matchFrom{propertyName}";

            if (!propertyDigests[instanceProperty].IsPlural)
            {
                scope.If($"!this.{propertyName}.{ParserGeneratorValues.ValidateInstanceMethodName}({eltVar}, {nameVar}, violations)")
                    .Line($"{termination};");
            }
            else
            {
                if (propertyDigests[instanceProperty].DictionaryKey == null)
                {
                    scope.Line($"var {matchName} = this.{propertyName}.Where(val => val.{MatchInstanceMethodName}({eltVar}, {nameVar})).FirstOrDefault();");
                }
                else
                {
                    scope.Line($"var {matchName} = this.{propertyName}.Where(kvp => kvp.Value.{MatchInstanceMethodName}({eltVar}, {nameVar})).FirstOrDefault().Value;");
                }

                scope
                    .If($"{matchName} == null")
                        .Line($"violations.Add({nameVar} != null ? $\"\\\"{{{nameVar}}}\\\" does not match any name in schema\" : $\"{{{eltVar}.GetRawText()}} does not match any value in schema\");")
                        .Line($"{termination};")
                    .ElseIf($"!{matchName}.{ParserGeneratorValues.ValidateInstanceMethodName}({eltVar}, {nameVar}, violations)")
                        .Line($"{termination};");
            }
        }

        private static void AddPatternCheck(int dtdlVersion, CsClass obverseClass, CsScope scope, string pattern, string stringVal, string prefix, string termination, string conformanceText)
        {
            string fieldName = $"{prefix}InstanceRegexPatternV{dtdlVersion}";

            obverseClass.Field(Access.Private, "Regex", fieldName, $"new Regex(@\"{pattern}\", RegexOptions.Compiled)", Multiplicity.Static, Mutability.ReadOnly);

            scope.If($"!{fieldName}.IsMatch({stringVal})")
                .Line($"violations.Add($\"\\\"{{{stringVal}}}\\\" does not conform to {conformanceText}\");")
                .Line($"{termination};");
        }
    }
}
