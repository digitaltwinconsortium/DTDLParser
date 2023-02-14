namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a language-tagged string literal property on a class that is materialized in the parser object model.
    /// </summary>
    public class LangStringLiteralProperty : LiteralProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LangStringLiteralProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        public LangStringLiteralProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => PropertyRepresentation.Dictionary;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType
        {
            get => "Dictionary<string, string>";
        }

        /// <inheritdoc/>
        public override string KeyProperty
        {
            get => "@language";
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
                sorted.Line($"&& Helpers.AreDictionariesLiteralEqual(this.{this.ObversePropertyName}, other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"&& Helpers.AreDictionariesLiteralEqual(this.{this.ObversePropertyName}, other.{this.ObversePropertyName})");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                sorted.Line($"hashCode = (hashCode * 131) + Helpers.GetDictionaryLiteralHashCode(this.{this.ObversePropertyName});");
            }
        }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);

            if (this.PropertyDigest.PropertyVersions.Any(d => d.Value.IsAllowed))
            {
                obverseClass.Field(Access.Private, "Dictionary<string, string>", this.PropertyLayerFieldName, "null");
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            return outerScope.ForEach($"string {varName} in this.{this.ObversePropertyName}.Values");
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
        public override void AddLiteralPropertiesToArray(CsScope scope, string arrayVariable)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                CsScope iterationScope = scope.ForEach($"KeyValuePair<string, string> kvp in this.{this.ObversePropertyName}");
                iterationScope.Line($"{arrayVariable}.Add(new JArray() {{ \"{this.PropertyName}\", kvp.Value, kvp.Key, \"#langString\" }});");
            }
        }

        /// <inheritdoc/>
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            string maxLengthString = this.PropertyDigest.PropertyVersions[dtdlVersion].MaxLength?.ToString() ?? "null";
            string patternString = this.PropertyDigest.PropertyVersions[dtdlVersion].Pattern != null ? $"{this.ObversePropertyName}{RegexPatternFieldSuffix}{dtdlVersion}" : "null";
            string newVar = $"new{this.ObversePropertyName}";
            string codesVar = $"{this.PropertyName}Codes";

            scope.Line($"Dictionary<string, string> {newVar} = ValueParser.ParseLangStringValueCollection({aggregateContextVar}, this.{ParserGeneratorValues.IdentifierName}, \"{this.PropertyName}\", prop.Values, \"{this.PropertyDigest.PropertyVersions[dtdlVersion].DefaultLanguage}\", {maxLengthString}, {patternString}, {layerVar}, parsingErrorCollection);");
            scope.Line($"List<string> {codesVar} = Helpers.GetKeysWithDifferingLiteralValues(this.{this.ObversePropertyName}, {newVar});");

            CsIf ifInconsistent = scope.If($"{codesVar}.Any()");

            ifInconsistent
                .Line($"JsonLdProperty extantProp = Helpers.TryGetSingleUniqueValue({codesVar}.Select(c => this.{this.PropertyLayerFieldName}[c]), out string uniqueCodeLayer) && this.JsonLdElements[uniqueCodeLayer].Properties.Any(p => p.Name == \"{this.PropertyName}\") ? this.JsonLdElements[uniqueCodeLayer].Properties.First(p => p.Name == \"{this.PropertyName}\") : null;")
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"inconsistentLangStringValues\",")
                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                    .Line($"propertyName: \"{this.PropertyName}\",")
                    .Line($"langCode: string.Join(\" and \", {codesVar}.Select(c => $\"'{{c}}'\")),")
                    .Line($"observedCount: {codesVar}.Count,")
                    .Line($"incidentProperty: {propVar},")
                    .Line("extantProperty: extantProp,")
                    .Line($"layer: {layerVar});");

            ifInconsistent.ElseIf($"this.{this.ObversePropertyName}.Any()")
                .ForEach($"KeyValuePair<string, string> kvp in {newVar}")
                    .Line($"(({this.ConcretePropertyType})this.{this.ObversePropertyName})[kvp.Key] = kvp.Value;")
                    .Line($"this.{this.PropertyLayerFieldName}[kvp.Key] = {layerVar};");

            ifInconsistent.Else()
                .Line($"this.{this.ObversePropertyName} = {newVar};")
                .Line($"this.{this.PropertyLayerFieldName} = {newVar}.ToDictionary(e => e.Key, e => {layerVar});");
        }
    }
}
