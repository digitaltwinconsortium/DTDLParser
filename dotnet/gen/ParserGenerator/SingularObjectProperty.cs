namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a singular object property on a class that is materialized in the parser object model.
    /// </summary>
    public class SingularObjectProperty : ObjectProperty
    {
        private const string RequiredValuesFieldSuffix = "RequiredValues";

        private readonly string requiredValuesField;

        private readonly PropertyRestrictionRequiredValues propertyRestrictionRequiredValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingularObjectProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        public SingularObjectProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.requiredValuesField = $"{this.ObversePropertyName}{RequiredValuesFieldSuffix}";
            this.propertyRestrictionRequiredValues = propertyRestrictions.ContainsKey(2) ? (PropertyRestrictionRequiredValues)propertyRestrictions[2].Where(r => r.GetType() == typeof(PropertyRestrictionRequiredValues)).FirstOrDefault() : null;
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => this.PropertyDigest.IsOptional ? PropertyRepresentation.NullableItem : PropertyRepresentation.Item;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => this.ClassName;
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            base.GenerateConstructorCode(sorted);

            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"this.{this.ObversePropertyName} = null;");
            }
        }

        /// <inheritdoc/>
        public override void AddEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& this.{this.ObversePropertyName}?.{ParserGeneratorValues.IdentifierName} == other.{this.ObversePropertyName}?.{ParserGeneratorValues.IdentifierName}");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& (this.{this.ObversePropertyName}?.DeepEquals(other.{this.ObversePropertyName}) ?? ReferenceEquals(null, other.{this.ObversePropertyName}))");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"hashCode = (hashCode * 131) + (ReferenceEquals(null, this.{this.ObversePropertyName}) ? 0 : this.{this.ObversePropertyName}.{ParserGeneratorValues.IdentifierName}.GetHashCode());");
            }
        }

        /// <inheritdoc/>
        public override void AddJsonWritingCode(CsScope scope)
        {
            base.AddJsonWritingCode(scope);

            if (!this.PropertyDigest.IsInherited)
            {
                scope.Line($"jsonWriter.WriteString(\"{this.PropertyName}\", this.{this.ObversePropertyName}?.{ParserGeneratorValues.IdentifierName}?.ToString());");
            }
        }

        /// <inheritdoc/>
        public override void AddTypeScriptType(IndentedTextWriter indentedTextWriter)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                string optionalityIndicator = this.PropertyDigest.IsOptional ? "?" : string.Empty;
                indentedTextWriter.WriteLine($"{this.PropertyName}{optionalityIndicator}: string;");

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

            if (!this.PropertyDigest.IsInherited && this.PropertyDigest.PropertyVersions.Any(d => d.Value.IsAllowed))
            {
                obverseClass.Field(Access.Private, ParserGeneratorValues.ObverseTypeString, this.PropertyLayerFieldName, "null");
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}";

            return outerScope.If($"this.{this.ObversePropertyName} != null");
        }

        /// <inheritdoc/>
        public override CsScope One(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}";

            return outerScope.If($"this.{this.ObversePropertyName} != null");
        }

        /// <inheritdoc/>
        public override CsScope CheckPresence(CsScope outerScope)
        {
            return outerScope.If($"this.{this.ObversePropertyName} != null");
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

                CsIf ifPropNotNull = switchOnProperty.If($"this.{this.ObversePropertyName} != null");
                CsIf ifMismatch = ifPropNotNull.If($"this.{this.ObversePropertyName}.{ParserGeneratorValues.IdentifierName} != {elementVar}.{ParserGeneratorValues.IdentifierName}");
                ifMismatch.Line($"JsonLdProperty extantProp = this.JsonLdElements[this.{this.PropertyLayerFieldName}].Properties.FirstOrDefault(p => p.Name == \"{this.PropertyName}\");");

                ifMismatch
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line($"\"inconsistentObjectValues\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyName: \"{this.PropertyName}\",")
                        .Line($"propertyValue: {elementVar}.{ParserGeneratorValues.IdentifierName}.ToString(),")
                        .Line($"valueRestriction: this.{this.ObversePropertyName}.{ParserGeneratorValues.IdentifierName}.ToString(),")
                        .Line($"incidentProperty: {jsonLdPropVar},")
                        .Line("extantProperty: extantProp,")
                        .Line($"layer: {layerVar});");

                ifPropNotNull.Else()
                    .Line($"this.{this.ObversePropertyName} = ({this.ClassName}){elementVar};")
                    .Line($"this.{this.PropertyLayerFieldName} = {layerVar};");

                switchOnProperty.Line("return true;");
            }
        }

        /// <inheritdoc/>
        public override void AddRestrictions(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName, bool classIsAugmentable)
        {
            base.AddRestrictions(checkRestrictionsMethodBody, dtdlVersion, typeName, classIsAugmentable);

            if (this.PropertyDigest.PropertyVersions.Any(v => v.Value.IsAllowed) && classIsAugmentable)
            {
                CsIf ifInstanceProperties = checkRestrictionsMethodBody.If($"this.{this.InstancePropertiesField} != null");
                CsForEach forEachInstanceProperty = ifInstanceProperties.ForEach($"string instanceProp in this.{this.InstancePropertiesField}");
                CsIf ifTryGetValue = forEachInstanceProperty.If($"this.supplementalProperties.TryGetValue(instanceProp, out object supplementalProperty)");
                ifTryGetValue
                    .Line($"IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.{this.ObversePropertyName}.{ParserGeneratorValues.ValidateInstanceMethodName}(jsonElement) : this.{this.ObversePropertyName}.{ParserGeneratorValues.ValidateInstanceMethodName}(supplementalProperty.ToString());");
                CsIf ifInvalid = ifTryGetValue.If("violations.Any()");

                ifInvalid
                    .Line("string violationPostscript = violations.Count > 1 ? $\", plus {violations.Count - 1} other violation(s)\" : string.Empty;")
                    .Line("string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));")
                    .Line("JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);")
                    .Line($"JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{this.PropertyName}\")).Value?.Properties?.First(p => p.Name == \"{this.PropertyName}\");")
                    .Break();

                ifInvalid
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line($"\"nonConformantPropertyValue\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("propertyName: instanceTerm,")
                        .Line($"governingPropertyName: \"{this.PropertyName}\",")
                        .Line("violations: violations,")
                        .Line("violationCount: violations.Count,")
                        .Line("incidentValues: instancePropProp.Values,")
                        .Line("governingValues: conformanceProp.Values);");
            }
        }

        /// <inheritdoc/>
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            string valueConstraints = classIsAugmentable ? $"this.{this.ValueConstraintsField}" : "null";
            string definedIn = classIsPartition ? $"this.{ParserGeneratorValues.IdentifierName}" : $"{definedInVar} ?? this.{ParserGeneratorValues.IdentifierName}";
            string dtmiSegment = this.PropertyDigest.DtmiSegment != null ? $"\"{this.PropertyDigest.DtmiSegment}\"" : "null";
            int minCount = this.PropertyDigest.PropertyVersions[dtdlVersion].MinCount ?? 0;

            scope.Line($"{this.VersionedClassName[dtdlVersion]}.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, {valueConstraints}, aggregateContext, parsingErrorCollection, prop, {layerVar}, this.{ParserGeneratorValues.IdentifierName}, globalize ? null : {definedIn}, \"{this.PropertyName}\", {dtmiSegment}, null, {minCount}, isPlural: false, idRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].IdRequired)}, typeRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].TypeRequired)}, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.{this.AllowedVersionsField}V{dtdlVersion});");
        }
    }
}
