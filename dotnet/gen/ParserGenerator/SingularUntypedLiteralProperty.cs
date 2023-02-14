namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a singular untyped literal property on a class that is materialized in the parser object model.
    /// </summary>
    public class SingularUntypedLiteralProperty : UntypedLiteralProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingularUntypedLiteralProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        public SingularUntypedLiteralProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
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
            get => "object";
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"this.{this.ObversePropertyName} = {(this.PropertyDigest.IsOptional ? "null" : $"new {this.ConcretePropertyType}()")};");
            }
        }

        /// <inheritdoc/>
        public override void AddEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line(
                    "&& " +
                    (this.PropertyDigest.IsOptional ? $"this.{this.ObversePropertyName} == null ? other.{this.ObversePropertyName} == null : " : string.Empty) +
                    $"((IComparable)this.{this.ObversePropertyName}).CompareTo((IComparable)other.{this.ObversePropertyName}) == 0");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line(
                    "&& " +
                    (this.PropertyDigest.IsOptional ? $"this.{this.ObversePropertyName} == null ? other.{this.ObversePropertyName} == null : " : string.Empty) +
                    $"((IComparable)this.{this.ObversePropertyName}).CompareTo((IComparable)other.{this.ObversePropertyName}) == 0");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                if (this.PropertyDigest.IsOptional)
                {
                    sorted.Line($"hashCode = (hashCode * 131) + (ReferenceEquals(null, this.{this.ObversePropertyName}) ? 0 : this.{this.ObversePropertyName}.GetHashCode());");
                }
                else
                {
                    sorted.Line($"hashCode = (hashCode * 131) + this.{this.ObversePropertyName}.GetHashCode();");
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
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            string newVar = $"new{this.ObversePropertyName}";

            scope.Line($"object {newVar} = ValueParser.ParseSingularLiteralValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, {layerVar}, parsingErrorCollection, isOptional: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.IsOptional)}, typeFragment: out this.{this.DatatypeField});");

            CsIf ifAlreadyPresent = scope.If($"this.{this.PropertyLayerFieldName} != null");
            CsIf ifMismatch = ifAlreadyPresent.If($"((IComparable)this.{this.ObversePropertyName}).CompareTo((IComparable){newVar}) != 0");
            ifMismatch.Line($"JsonLdProperty extantProp = this.JsonLdElements[this.{this.PropertyLayerFieldName}].Properties.FirstOrDefault(p => p.Name == \"{this.PropertyName}\");");

            ifMismatch
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line($"\"inconsistentLiteralValues\",")
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
                                    .Line("\"notRequiredLiteralValue\",")
                                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                                    .Line($"propertyName: \"{this.PropertyName}\",")
                                    .Line($"propertyValue: {newVar}.ToString(),")
                                    .Line($"valueRestriction: valueConstraint.RequiredLiteral.ToString(),")
                                    .Line($"incidentProperty: {propVar},")
                                    .Line($"layer: {layerVar});");
            }
        }
    }
}
