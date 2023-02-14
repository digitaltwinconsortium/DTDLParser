namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a propertyDigest.IsPlural identifier property on a class that is materialized in the parser object model.
    /// </summary>
    public class PluralIdentifierProperty : IdentifierProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluralIdentifierProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="baseClassName">The C# name of the DTDL base class.</param>
        public PluralIdentifierProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, string baseClassName)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions, baseClassName)
        {
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => PropertyRepresentation.List;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => $"List<{ParserGeneratorValues.IdentifierType}>";
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"this.{this.ObversePropertyName} = new {this.ConcretePropertyType}();");
            }
        }

        /// <inheritdoc/>
        public override void AddEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& this.{this.ObversePropertyName}.SequenceEqual(other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& this.{this.ObversePropertyName}.SequenceEqual(other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"hashCode = (hashCode * 131) + Helpers.GetListLiteralHashCode(this.{this.ObversePropertyName});");
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            return outerScope.ForEach($"{ParserGeneratorValues.IdentifierType} {varName} in this.{this.ObversePropertyName}");
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
        public override void AddCaseToTrySetObjectPropertySwitch(CsSwitch switchOnProperty, string layerVar, string jsonLdPropVar, string elementVar, string keyPropVar, string keyValueVar, string parsingErrorCollectionVar)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                switchOnProperty.Case($"\"{this.PropertyName}\"");

                foreach (string propertyNameUri in this.PropertyNameUris.Values)
                {
                    switchOnProperty.Case($"\"{propertyNameUri}\"");
                }

                switchOnProperty.Line($"(({this.ConcretePropertyType})this.{this.ObversePropertyName}).Add((({this.BaseClassName}){elementVar}).{ParserGeneratorValues.IdentifierName});");
                switchOnProperty.Line("return true;");
            }
        }

        /// <inheritdoc/>
        public override void AddRestrictions(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName, bool classIsAugmentable)
        {
            base.AddRestrictions(checkRestrictionsMethodBody, dtdlVersion, typeName, classIsAugmentable);

            if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed)
            {
                if (propertyVersionDigest.MaxCount != null)
                {
                    checkRestrictionsMethodBody.If($"this.{this.ObversePropertyName}.Count > {propertyVersionDigest.MaxCount} && Helpers.CountUnique(this.{this.ObversePropertyName}) > {propertyVersionDigest.MaxCount}")
                        .Line($"JsonLdProperty propProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{this.PropertyName}\")).Value?.Properties?.First(p => p.Name == \"{this.PropertyName}\");")
                        .MultiLine("parsingErrorCollection.Notify(")
                            .Line("\"identifierCountAboveMax\",")
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
            string maxLengthString = this.PropertyDigest.PropertyVersions[dtdlVersion].MaxLength?.ToString() ?? "null";
            string patternString = this.PropertyDigest.PropertyVersions[dtdlVersion].Pattern != null ? $"{this.ObversePropertyName}{RegexPatternFieldSuffix}{dtdlVersion}" : "null";
            string minCountString = propertyVersionDigest.MinCount?.ToString() ?? "0";

            scope.Line($"List<{ParserGeneratorValues.IdentifierType}> new{this.ObversePropertyName} = ValueParser.ParsePluralIdentifierValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, {maxLengthString}, {patternString}, {layerVar}, parsingErrorCollection, minCount: {minCountString}, requireDtmi: true).Select(u => ({ParserGeneratorValues.IdentifierType})u).ToList();")
                .Line($"this.{this.ObversePropertyName} = this.{this.ObversePropertyName}.Any() ? Enumerable.Union(this.{this.ObversePropertyName}, new{this.ObversePropertyName}).ToList() : new{this.ObversePropertyName};");
        }
    }
}
