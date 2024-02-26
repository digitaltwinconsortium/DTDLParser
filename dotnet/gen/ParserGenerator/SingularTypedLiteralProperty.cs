namespace DTDLParser
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a singular typed literal property on a class that is materialized in the parser object model.
    /// </summary>
    public class SingularTypedLiteralProperty : TypedLiteralProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingularTypedLiteralProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="datatype">A string representation of the datatype of the literal property.</param>
        /// <param name="literalType">An object that exports the <see cref="ILiteralType"/> interface to support declaring and parsing a specific literal type.</param>
        public SingularTypedLiteralProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, string datatype, ILiteralType literalType)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions, datatype, literalType)
        {
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => this.LiteralType.CanBeNull(this.PropertyDigest.IsOptional) ? PropertyRepresentation.NullableItem : PropertyRepresentation.Item;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => this.LiteralType.GetSingularType(this.PropertyDigest.IsOptional);
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"this.{this.ObversePropertyName} = {this.LiteralType.GetInitialValue(this.PropertyDigest.IsOptional)};");
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
                if (this.LiteralType.CanBeNull(this.PropertyDigest.IsOptional))
                {
                    sorted.Line($"hashCode = (hashCode * 131) + (this.{this.ObversePropertyName}?.GetHashCode() ?? 0);");
                }
                else
                {
                    sorted.Line($"hashCode = (hashCode * 131) + this.{this.ObversePropertyName}.GetHashCode();");
                }
            }
        }

        /// <inheritdoc/>
        public override void AddJsonWritingCode(CsScope scope)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                switch (this.Datatype)
                {
                    case "string":
                        scope.Line($"jsonWriter.WriteString(\"{this.PropertyName}\", this.{this.ObversePropertyName});");
                        break;
                    case "integer":
                        if (this.LiteralType.CanBeNull(this.PropertyDigest.IsOptional))
                        {
                            scope
                                .If($"this.{this.ObversePropertyName} != null")
                                    .Line($"jsonWriter.WriteNumber(\"{this.PropertyName}\", ({ParserGeneratorValues.ObverseTypeInteger})this.{this.ObversePropertyName});")
                                .Else()
                                    .Line($"jsonWriter.WriteNull(\"{this.PropertyName}\");");
                        }
                        else
                        {
                            scope.Line($"jsonWriter.WriteNumber(\"{this.PropertyName}\", this.{this.ObversePropertyName});");
                        }

                        break;
                    case "boolean":
                        scope.Line($"jsonWriter.WriteBoolean(\"{this.PropertyName}\", this.{this.ObversePropertyName});");
                        break;
                    default:
                        throw new Exception($"parsing logic for {this.Datatype} type not written yet");
                }
            }
        }

        /// <inheritdoc/>
        public override void AddTypeScriptType(IndentedTextWriter indentedTextWriter)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                string optionalityIndicator = this.PropertyDigest.IsOptional ? "?" : string.Empty;
                switch (this.Datatype)
                {
                    case "string":
                        indentedTextWriter.WriteLine($"{this.PropertyName}{optionalityIndicator}: string;");
                        break;
                    case "integer":
                        indentedTextWriter.WriteLine($"{this.PropertyName}{optionalityIndicator}: number;");
                        break;
                    case "boolean":
                        indentedTextWriter.WriteLine($"{this.PropertyName}: boolean;");
                        break;
                    default:
                        throw new Exception($"TypeScript type logic for {this.Datatype} type not written yet");
                }
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

            if (this.LiteralType.CanBeNull(this.PropertyDigest.IsOptional))
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

            if (this.LiteralType.CanBeNull(this.PropertyDigest.IsOptional))
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
            if (this.LiteralType.CanBeNull(this.PropertyDigest.IsOptional))
            {
                return outerScope.If($"this.{this.ObversePropertyName} != null");
            }
            else
            {
                return outerScope;
            }
        }

        /// <inheritdoc/>
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            string maxLengthString = this.PropertyDigest.PropertyVersions[dtdlVersion].MaxLength?.ToString() ?? "null";
            string patternString = this.PropertyDigest.PropertyVersions[dtdlVersion].Pattern != null ? $"{this.ObversePropertyName}{RegexPatternFieldSuffix}{dtdlVersion}" : "null";
            string minInclusiveString = this.PropertyDigest.PropertyVersions[dtdlVersion].MinInclusive?.ToString() ?? "null";
            string maxInclusiveString = this.PropertyDigest.PropertyVersions[dtdlVersion].MaxInclusive?.ToString() ?? "null";
            string isOptionalString = $"isOptional: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.IsOptional)}";
            string newVar = $"new{this.ObversePropertyName}";

            switch (this.Datatype)
            {
                case "string":
                    scope.Line($"string {newVar} = ValueParser.ParseSingularStringValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, {maxLengthString}, {patternString}, {layerVar}, parsingErrorCollection, {isOptionalString});");
                    break;
                case "duration":
                    scope.Line($"TimeSpan? {newVar} = ValueParser.ParseSingularDurationValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, {layerVar}, parsingErrorCollection, {isOptionalString});");
                    break;
                case "integer":
                    scope.Line($"int? {newVar} = ValueParser.ParseSingularIntegerValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, {minInclusiveString}, {maxInclusiveString}, {layerVar}, parsingErrorCollection, {isOptionalString});");
                    break;
                case "boolean":
                    scope.Line($"bool {newVar} = ValueParser.ParseSingularBooleanValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, {layerVar}, parsingErrorCollection, {isOptionalString});");
                    break;
                default:
                    throw new Exception($"parsing logic for {this.Datatype} type not written yet");
            }

            CsIf ifAlreadyPresent = scope.If($"this.{this.PropertyLayerFieldName} != null");
            CsIf ifMismatch = ifAlreadyPresent.If($"this.{this.ObversePropertyName} != {newVar}");
            ifMismatch.Line($"JsonLdProperty extantProp = this.JsonLdElements[this.{this.PropertyLayerFieldName}].Properties.FirstOrDefault(p => p.Name == \"{this.PropertyName}\");");

            ifMismatch
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line($"\"inconsistent{Capitalize(this.Datatype)}Values\",")
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

            if (classIsAugmentable)
            {
                elseNotAlreadyPresent
                    .Break()
                    .If($"this.{this.ValueConstraintsField} != null")
                        .ForEach($"ValueConstraint valueConstraint in this.{this.ValueConstraintsField}")
                            .If($"valueConstraint.RequiredLiteral != null && !valueConstraint.RequiredLiteral.Equals({newVar})")
                                .MultiLine("parsingErrorCollection.Notify(")
                                    .Line($"\"notRequired{Capitalize(this.Datatype)}Value\",")
                                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                                    .Line($"propertyName: \"{this.PropertyName}\",")
                                    .Line($"propertyValue: {newVar}.ToString(),")
                                    .Line($"valueRestriction: valueConstraint.RequiredLiteral.ToString(),")
                                    .Line($"incidentProperty: {propVar},")
                                    .Line($"layer: {layerVar});");
            }

            if (this.Datatype == "string")
            {
                elseNotAlreadyPresent.Line($"((Dictionary<string, {ParserGeneratorValues.ObverseTypeString}>)this.{ParserGeneratorValues.StringPropertiesPropertyName})[\"{this.PropertyName}\"] = {newVar};");
            }
        }

        private static string Capitalize(string val)
        {
            return char.ToUpperInvariant(val[0]) + val.Substring(1);
        }
    }
}
