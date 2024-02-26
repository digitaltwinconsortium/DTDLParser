namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a propertyDigest.IsPlural object property on a class that is materialized in the parser object model.
    /// </summary>
    public class PluralObjectProperty : ObjectProperty
    {
        private bool isLayeringSupported;
        private string kindEnum;
        private string kindProperty;
        private Dictionary<string, string> breakoutPropertyTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluralObjectProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        /// <param name="kindProperty">The property on the DTDL base obverse class that indicates the kind of DTDL element.</param>
        public PluralObjectProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, bool isLayeringSupported, string kindEnum, string kindProperty)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.isLayeringSupported = isLayeringSupported;
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
            get => PropertyRepresentation.List;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => $"List<{this.ClassName}>";
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            base.GenerateConstructorCode(sorted);

            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"this.{this.ObversePropertyName} = new {this.ConcretePropertyType}();");

                foreach (KeyValuePair<string, string> kvp in this.breakoutPropertyTypes)
                {
                    sorted.Line($"this.{NameFormatter.FormatNameAsProperty(kvp.Key)} = new List<{kvp.Value}>();");
                }
            }
        }

        /// <inheritdoc/>
        public override void AddEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& Helpers.AreListsIdEqual(this.{this.ObversePropertyName}, other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& Helpers.AreListsDeepEqual(this.{this.ObversePropertyName}, other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"hashCode = (hashCode * 131) + Helpers.GetListIdHashCode(this.{this.ObversePropertyName});");
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
                indentedTextWriter.WriteLine($"{this.PropertyName}: string[];");

                foreach (KeyValuePair<string, string> kvp in this.breakoutPropertyTypes)
                {
                    indentedTextWriter.WriteLine($"{kvp.Key}: string[];");
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
                CsProperty property = obverseClass.Property(Access.Public, Novelty.Normal, $"IReadOnlyList<{kvp.Value}>", NameFormatter.FormatNameAsProperty(kvp.Key));
                property.Summary($"Gets a subset of values from the '{this.PropertyName}' property of the corresponding DTDL element, for which the values have type {subtypeString}.");
                property.Value($"The {subtypeString} values from the '{this.PropertyName}' property of the DTDL element.");
                property.Get().Set(Access.Internal);
            }

            if (!this.PropertyDigest.IsInherited && this.PropertyDigest.Breakout.Any())
            {
                CsMethod breakoutMethod = obverseClass.Method(Access.Private, Novelty.Normal, "void", $"BreakOut{this.ObversePropertyName}");
                CsSwitch switchOnKind = breakoutMethod.Body.ForEach($"{this.ClassName} element in this.{this.ObversePropertyName}").Switch($"element.{this.kindProperty}");

                foreach (KeyValuePair<string, List<string>> kvp in this.PropertyDigest.Breakout)
                {
                    foreach (string subtype in kvp.Value)
                    {
                        switchOnKind.Case($"{this.kindEnum}.{subtype}");
                    }

                    switchOnKind.Line($"((List<{this.breakoutPropertyTypes[kvp.Key]}>)this.{NameFormatter.FormatNameAsProperty(kvp.Key)}).Add(({this.breakoutPropertyTypes[kvp.Key]})element);");
                    switchOnKind.Line("break;");
                }
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            return outerScope.ForEach($"{this.ClassName} {varName} in this.{this.ObversePropertyName}");
        }

        /// <inheritdoc/>
        public override CsScope One(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}.First()";

            return outerScope.If($"this.{this.ObversePropertyName}.Any()");
        }

        /// <inheritdoc/>
        public override CsScope CheckPresence(CsScope outerScope)
        {
            return outerScope.If($"this.{this.ObversePropertyName}.Any()");
        }

        /// <inheritdoc/>
        public override void AddCaseToTrySetObjectPropertySwitch(CsSwitch switchOnProperty, string layerVar, string jsonLdPropVar, string valueVar, string keyPropVar, string keyValueVar, string parsingErrorCollectionVar)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                switchOnProperty.Case($"\"{this.PropertyName}\"");

                foreach (string propertyNameUri in this.PropertyNameUris.Values)
                {
                    switchOnProperty.Case($"\"{propertyNameUri}\"");
                }

                CsScope addPropertyScope = this.isLayeringSupported ? switchOnProperty.If($"this.{ParserGeneratorValues.LayersPropertyName}.Count < 2 || !this.{this.ObversePropertyName}.Any(e => e.{ParserGeneratorValues.IdentifierName} == element.{ParserGeneratorValues.IdentifierName})") : switchOnProperty;
                addPropertyScope.Line($"(({this.ConcretePropertyType})this.{this.ObversePropertyName}).Add(({this.ClassName}){valueVar});");

                switchOnProperty.Line("return true;");
            }
        }

        /// <inheritdoc/>
        public override bool TryAddCaseToGetChildrenSwitch(CsSwitch switchOnChildrenProperty)
        {
            switchOnChildrenProperty.Case($"\"{this.PropertyName}\"");
            switchOnChildrenProperty.Line($"return this.{this.ObversePropertyName};");
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
                    checkRestrictionsMethodBody.If($"this.{this.ObversePropertyName}.Count > {propertyVersionDigest.MaxCount} && Helpers.CountUnique(this.{this.ObversePropertyName}.Select(e => e.{ParserGeneratorValues.IdentifierName})) > {propertyVersionDigest.MaxCount}")
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

            scope.Line($"{this.VersionedClassName[dtdlVersion]}.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, {valueConstraints}, aggregateContext, parsingErrorCollection, prop, {layerVar}, this.{ParserGeneratorValues.IdentifierName}, globalize ? null : {definedIn}, \"{this.PropertyName}\", {dtmiSegment}, null, {minCount}, isPlural: true, idRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].IdRequired)}, typeRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].TypeRequired)}, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.{this.AllowedVersionsField}V{dtdlVersion});");
        }

        private void AddSpecificJsonWritingCode(CsScope scope, string propertyName, string obversePropertyName, string className)
        {
            string varName = $"{propertyName}Elt";
            scope.Line($"jsonWriter.WritePropertyName(\"{propertyName}\");");
            scope.Line("jsonWriter.WriteStartArray();");
            scope.Break();
            scope.ForEach($"{className} {varName} in this.{obversePropertyName}")
                .Line($"jsonWriter.WriteStringValue({varName}.{ParserGeneratorValues.IdentifierName}.ToString());");
            scope.Break();
            scope.Line("jsonWriter.WriteEndArray();");
            scope.Break();
        }
    }
}
