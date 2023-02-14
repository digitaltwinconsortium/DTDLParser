namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a singular identifier property on a class that is materialized in the parser object model.
    /// </summary>
    public class SingularIdentifierProperty : IdentifierProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingularIdentifierProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="baseClassName">The C# name of the DTDL base class.</param>
        public SingularIdentifierProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, string baseClassName)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions, baseClassName)
        {
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => this.PropertyDigest.IsOptional ? PropertyRepresentation.NullableItem : PropertyRepresentation.Item;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => ParserGeneratorValues.IdentifierType;
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
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
                sorted.Line($"&& this.{this.ObversePropertyName} == other.{this.ObversePropertyName}");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& this.{this.ObversePropertyName} == other.{this.ObversePropertyName}");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"hashCode = (hashCode * 131) + (ReferenceEquals(null, this.{this.ObversePropertyName}) ? 0 : this.{this.ObversePropertyName}.GetHashCode());");
            }
        }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);

            if (this.PropertyDigest.PropertyVersions.Any(d => d.Value.IsAllowed))
            {
                obverseClass.Field(Access.Private, ParserGeneratorValues.ObverseTypeString, this.PropertyLayerFieldName, "null");
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}";

            if (this.PropertyDigest.IsOptional)
            {
                return outerScope.If($"this.{this.ObversePropertyName} != null");
            }
            else
            {
                return outerScope;
            }
        }

        /// <inheritdoc/>
        public override CsScope One(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}";

            if (this.PropertyDigest.IsOptional)
            {
                return outerScope.If($"this.{this.ObversePropertyName} != null");
            }
            else
            {
                return outerScope;
            }
        }

        /// <inheritdoc/>
        public override CsScope CheckPresence(CsScope outerScope)
        {
            if (this.PropertyDigest.IsOptional)
            {
                return outerScope.If($"this.{this.ObversePropertyName} != null");
            }
            else
            {
                return outerScope;
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

                switchOnProperty.Line($"this.{this.ObversePropertyName} = (({this.BaseClassName}){elementVar}).{ParserGeneratorValues.IdentifierName};");
                switchOnProperty.Line("return true;");
            }
        }

        /// <inheritdoc/>
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            string maxLengthString = this.PropertyDigest.PropertyVersions[dtdlVersion].MaxLength?.ToString() ?? "null";
            string patternString = this.PropertyDigest.PropertyVersions[dtdlVersion].Pattern != null ? $"{this.ObversePropertyName}{RegexPatternFieldSuffix}{dtdlVersion}" : "null";
            string isOptionalString = $"isOptional: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.IsOptional)}";
            string newVar = $"new{this.ObversePropertyName}";

            scope.Line($"{ParserGeneratorValues.IdentifierType} {newVar} = ({ParserGeneratorValues.IdentifierType})ValueParser.ParseSingularIdentifierValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, {maxLengthString}, {patternString}, {layerVar}, parsingErrorCollection, {isOptionalString}, requireDtmi: true);");

            CsIf ifAlreadyPresent = scope.If($"this.{this.PropertyLayerFieldName} != null");
            CsIf ifMismatch = ifAlreadyPresent.If($"this.{this.ObversePropertyName} != {newVar}");
            ifMismatch.Line($"JsonLdProperty extantProp = this.JsonLdElements[this.{this.PropertyLayerFieldName}].Properties.FirstOrDefault(p => p.Name == \"{this.PropertyName}\");");

            ifMismatch
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line($"\"inconsistentIdentifierValues\",")
                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                    .Line($"propertyName: \"{this.PropertyName}\",")
                    .Line($"propertyValue: {newVar}.ToString(),")
                    .Line($"valueRestriction: this.{this.ObversePropertyName}.ToString(),")
                    .Line($"incidentProperty: {propVar},")
                    .Line("extantProperty: extantProp,")
                    .Line($"layer: {layerVar});");

            CsElse elseNotAlreadyPresent = ifAlreadyPresent.Else();
            elseNotAlreadyPresent
                .Line($"this.{this.ObversePropertyName} = {newVar};")
                .Line($"this.{this.PropertyLayerFieldName} = {layerVar};");
        }
    }
}
