namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a propertyDigest.IsPlural object property on a class that is materialized as a dictionary in the parser object model.
    /// </summary>
    public class DictionaryProperty : ObjectProperty
    {
        private string kindEnum;
        private string kindProperty;
        private Dictionary<string, string> breakoutPropertyTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        /// <param name="kindProperty">The property on the DTDL base obverse class that indicates the kind of DTDL element.</param>
        public DictionaryProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, string kindEnum, string kindProperty)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.KeyProperty = propertyDigest.DictionaryKey;
            this.kindEnum = kindEnum;
            this.kindProperty = kindProperty;
            this.breakoutPropertyTypes = new Dictionary<string, string>();

            if (!propertyDigest.IsInherited && propertyDigest.Breakout.Any())
            {
                foreach (KeyValuePair<string, List<string>> kvp in propertyDigest.Breakout)
                {
                    this.breakoutPropertyTypes[kvp.Key] = kvp.Value.Count == 1 ? NameFormatter.FormatNameAsClass(kvp.Value[0]) : this.ClassName;
                }
            }
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => PropertyRepresentation.Dictionary;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => $"Dictionary<string, {this.ClassName}>";
        }

        /// <inheritdoc/>
        public override string KeyProperty { get; }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            base.GenerateConstructorCode(sorted);

            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"this.{this.ObversePropertyName} = new {this.ConcretePropertyType}();");

                foreach (KeyValuePair<string, string> kvp in this.breakoutPropertyTypes)
                {
                    sorted.Line($"this.{NameFormatter.FormatNameAsProperty(kvp.Key)} = new Dictionary<string, {kvp.Value}>();");
                }
            }
        }

        /// <inheritdoc/>
        public override void AddEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& Helpers.AreDictionariesIdEqual(this.{this.ObversePropertyName}, other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& Helpers.AreDictionariesDeepEqual(this.{this.ObversePropertyName}, other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"hashCode = (hashCode * 131) + Helpers.GetDictionaryIdHashCode(this.{this.ObversePropertyName});");
            }
        }

        /// <inheritdoc/>
        public override void AddJsonWritingCode(CsScope scope)
        {
            base.AddJsonWritingCode(scope);

            if (!this.PropertyDigest.IsInherited)
            {
                this.AddSpecificJsonWritingCode(scope, this.PropertyName, this.ObversePropertyName, this.ClassName);

                foreach (KeyValuePair<string, string> kvp in this.breakoutPropertyTypes)
                {
                    this.AddSpecificJsonWritingCode(scope, kvp.Key, NameFormatter.FormatNameAsProperty(kvp.Key), kvp.Value);
                }
            }
        }

        /// <inheritdoc/>
        public override void AddTypeScriptType(IndentedTextWriter indentedTextWriter)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                indentedTextWriter.WriteLine($"{this.PropertyName}: {{ [{this.PropertyDigest.DictionaryKey}: string]: string }};");

                foreach (KeyValuePair<string, string> kvp in this.breakoutPropertyTypes)
                {
                    indentedTextWriter.WriteLine($"{kvp.Key}: {{ [{this.PropertyDigest.DictionaryKey}: string]: string }};");
                }

                if (this.PropertyDigest.ReverseAs != null)
                {
                    indentedTextWriter.WriteLine($"{this.PropertyDigest.ReverseAs}: string[];");
                }
            }
        }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);

            foreach (KeyValuePair<string, string> kvp in this.breakoutPropertyTypes)
            {
                string subtypeString = string.Join(" or ", this.PropertyDigest.Breakout[kvp.Key]);
                CsProperty property = obverseClass.Property(Access.Public, Novelty.Normal, $"IReadOnlyDictionary<string, {kvp.Value}>", NameFormatter.FormatNameAsProperty(kvp.Key));
                property.Summary($"Gets a subset of values from the '{this.PropertyName}' property of the corresponding DTDL element, for which the values have type {subtypeString}.");
                property.Value($"The {subtypeString} values from the '{this.PropertyName}' property of the DTDL element.");
                property.Get().Set(Access.Internal);
            }

            if (!this.PropertyDigest.IsInherited && this.PropertyDigest.Breakout.Any())
            {
                CsMethod breakoutMethod = obverseClass.Method(Access.Private, Novelty.Normal, "void", $"BreakOut{this.ObversePropertyName}");
                CsSwitch switchOnKind = breakoutMethod.Body.ForEach($"KeyValuePair<string, {this.ClassName}> kvp in this.{this.ObversePropertyName}").Switch($"kvp.Value.{this.kindProperty}");

                foreach (KeyValuePair<string, List<string>> kvp in this.PropertyDigest.Breakout)
                {
                    foreach (string subtype in kvp.Value)
                    {
                        switchOnKind.Case($"{this.kindEnum}.{subtype}");
                    }

                    switchOnKind.Line($"((Dictionary<string, {this.breakoutPropertyTypes[kvp.Key]}>)this.{NameFormatter.FormatNameAsProperty(kvp.Key)}).Add(kvp.Key, ({this.breakoutPropertyTypes[kvp.Key]})kvp.Value);");
                    switchOnKind.Line("break;");
                }
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            return outerScope.ForEach($"{this.ClassName} {varName} in this.{this.ObversePropertyName}.Values");
        }

        /// <inheritdoc/>
        public override CsScope One(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}.Values.First()";

            return outerScope.If($"this.{this.ObversePropertyName}.Any()");
        }

        /// <inheritdoc/>
        public override CsScope CheckPresence(CsScope outerScope)
        {
            return outerScope.If($"this.{this.ObversePropertyName}.Any()");
        }

        /// <inheritdoc/>
        public override void AddObjectPropertiesToArray(CsScope scope, string arrayVariable, string referencesVariable)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                CsScope iterationScope = scope.ForEach($"KeyValuePair<string, {this.ClassName}> kvp in this.{this.ObversePropertyName}");
                iterationScope.Line($"{arrayVariable}.Add(new JArray() {{ \"{this.PropertyName}\", kvp.Value.{ParserGeneratorValues.IdentifierName}.ToString(), kvp.Key }});");
                iterationScope.Line($"{referencesVariable}.Add(kvp.Value.{ParserGeneratorValues.IdentifierName});");
            }
        }

        /// <inheritdoc/>
        public override void AddCaseToTrySetObjectPropertySwitch(CsSwitch switchOnProperty, string layerVar, string jsonLdPropVar, string elementVar, string keyPropVar, string keyValueVar, string parsingErrorCollectionVar)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                switchOnProperty.Case($"\"{this.PropertyName}\"");

                foreach (string propertyNameUri in this.PropertyNameUris.Values)
                {
                    switchOnProperty.Case($"\"{propertyNameUri}\"");
                }

                CsIf ifKeyNotNull = switchOnProperty.If($"{keyValueVar} != null");
                CsIf ifGetExtantValue = ifKeyNotNull.If($"this.{this.ObversePropertyName}.TryGetValue({keyValueVar}, out DTContentInfo extantValue)");
                CsIf ifIdMismatch = ifGetExtantValue.If($"extantValue.{ParserGeneratorValues.IdentifierName} != {elementVar}.{ParserGeneratorValues.IdentifierName}");

                ifIdMismatch
                    .Line($"JsonLdProperty keyPropProp = element.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == {keyPropVar})).Value?.Properties?.First(p => p.Name == {keyPropVar});")
                    .Line($"JsonLdProperty dupKeyProp = extantValue.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == {keyPropVar})).Value?.Properties?.First(p => p.Name == {keyPropVar});")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"nonUniquePropertyValue\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyName: \"{this.PropertyName}\",")
                        .Line($"nestedName: {keyPropVar},")
                        .Line($"nestedValue: {keyValueVar},")
                        .Line("incidentProperty: dupKeyProp,")
                        .Line("extantProperty: keyPropProp);");

                ifGetExtantValue.Else()
                    .Line($"(({this.ConcretePropertyType})this.{this.ObversePropertyName}).Add({keyValueVar}, ({this.ClassName}){elementVar});");

                switchOnProperty.Line("return true;");
            }
        }

        /// <inheritdoc/>
        public override bool TryAddCaseToGetChildSwitch(CsSwitch switchOnChildrenProperty, string keyPropertyNameVar, string keyPropertyValueVar, string childVar)
        {
            HashSet<string> propertiesAdded = new HashSet<string>();

            switchOnChildrenProperty.Case($"\"{this.PropertyName}\"");

            foreach (KeyValuePair<int, PropertyVersionDigest> kvp in this.PropertyDigest.PropertyVersions)
            {
                if (kvp.Value.UniqueProperties != null)
                {
                    foreach (string uniqueProperty in kvp.Value.UniqueProperties)
                    {
                        if (!propertiesAdded.Contains(uniqueProperty))
                        {
                            if (this.PropertyDigest.DictionaryKey == uniqueProperty)
                            {
                                switchOnChildrenProperty.If($"{keyPropertyNameVar} == \"{uniqueProperty}\" && this.{this.ObversePropertyName}.ContainsKey({keyPropertyValueVar})")
                                    .Line($"{childVar} = this.{this.ObversePropertyName}[{keyPropertyValueVar}];")
                                    .Line("return true;");
                            }
                            else
                            {
                                switchOnChildrenProperty.If($"{keyPropertyNameVar} == \"{uniqueProperty}\"")
                                    .Line($"{childVar} = this.{this.ObversePropertyName}.Values.Where(v => (string)v.{NameFormatter.FormatNameAsProperty(uniqueProperty)} == {keyPropertyValueVar}).FirstOrDefault();")
                                    .Line($"return {childVar} != null;");
                            }

                            propertiesAdded.Add(uniqueProperty);
                        }
                    }
                }
            }

            switchOnChildrenProperty.Line("break;");
            return true;
        }

        /// <inheritdoc/>
        public override void AddRestrictions(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName, bool classIsAugmentable)
        {
            base.AddRestrictions(checkRestrictionsMethodBody, dtdlVersion, typeName, classIsAugmentable);

            if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed)
            {
                if (propertyVersionDigest.MaxCount != null)
                {
                    checkRestrictionsMethodBody.If($"this.{this.ObversePropertyName}.Count > {propertyVersionDigest.MaxCount}")
                        .Line($"JsonLdProperty propProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{this.PropertyName}\")).Value?.Properties?.First(p => p.Name == \"{this.PropertyName}\");")
                        .MultiLine("parsingErrorCollection.Notify(")
                            .Line("\"objectCountAboveMax\",")
                            .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                            .Line($"propertyName: \"{this.PropertyName}\",")
                            .Line($"observedCount: this.{this.ObversePropertyName}.Count,")
                            .Line($"expectedCount: {propertyVersionDigest.MaxCount},")
                            .Line("incidentProperty: propProp);");
                }
            }
        }

        /// <inheritdoc/>
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            string valueConstraints = classIsAugmentable ? $"this.{this.ValueConstraintsField}" : "null";
            string definedIn = classIsPartition ? $"this.{ParserGeneratorValues.IdentifierName}" : $"{definedInVar} ?? this.{ParserGeneratorValues.IdentifierName}";
            string dtmiSegment = this.PropertyDigest.DtmiSegment != null ? $"\"{this.PropertyDigest.DtmiSegment}\"" : "null";
            int minCount = this.PropertyDigest.PropertyVersions[dtdlVersion].MinCount ?? 0;

            scope.Line($"{this.VersionedClassName[dtdlVersion]}.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, {valueConstraints}, aggregateContext, parsingErrorCollection, prop, {layerVar}, this.{ParserGeneratorValues.IdentifierName}, globalize ? null : {definedIn}, \"{this.PropertyName}\", {dtmiSegment}, \"{this.KeyProperty}\", {minCount}, isPlural: true, idRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].IdRequired)}, typeRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].TypeRequired)}, globalize: globalize, allowReservedIds: allowReservedIds, allowIdReferenceSyntax: allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames: ignoreElementsWithAutoIDsAndDuplicateNames, allowedVersions: this.{this.AllowedVersionsField}V{dtdlVersion});");
        }

        private void AddSpecificJsonWritingCode(CsScope scope, string propertyName, string obversePropertyName, string className)
        {
            string varName = $"{propertyName}Pair";
            scope.Line($"jsonWriter.WritePropertyName(\"{propertyName}\");");
            scope.Line("jsonWriter.WriteStartObject();");
            scope.Break();
            scope.ForEach($"KeyValuePair<string, {className}> {varName} in this.{obversePropertyName}")
                .Line($"jsonWriter.WriteString({varName}.Key, {varName}.Value.{ParserGeneratorValues.IdentifierName}.ToString());");
            scope.Break();
            scope.Line("jsonWriter.WriteEndObject();");
            scope.Break();
        }
    }
}
