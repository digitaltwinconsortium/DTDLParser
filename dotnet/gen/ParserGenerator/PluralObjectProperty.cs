namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a propertyDigest.IsPlural object property on a class that is materialized in the parser object model.
    /// </summary>
    public class PluralObjectProperty : ObjectProperty
    {
        private bool isLayeringSupported;

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
        public PluralObjectProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, bool isLayeringSupported)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.isLayeringSupported = isLayeringSupported;
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
            if (!this.PropertyDigest.IsInherited)
            {
                string varName = $"{this.PropertyName}Elt";
                scope.Line($"jsonWriter.WritePropertyName(\"{this.PropertyName}\");");
                scope.Line("jsonWriter.WriteStartArray();");
                scope.Break();
                scope.ForEach($"{this.ClassName} {varName} in this.{this.ObversePropertyName}")
                    .Line($"jsonWriter.WriteStringValue({varName}.{ParserGeneratorValues.IdentifierName}.ToString());");
                scope.Break();
                scope.Line("jsonWriter.WriteEndArray();");
            }
        }

        /// <inheritdoc/>
        public override void AddTypeScriptType(IndentedTextWriter indentedTextWriter)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                indentedTextWriter.WriteLine($"{this.PropertyName}: string[];");
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

            scope.Line($"{this.VersionedClassName[dtdlVersion]}.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, {valueConstraints}, aggregateContext, parsingErrorCollection, prop, {layerVar}, this.{ParserGeneratorValues.IdentifierName}, globalize ? null : {definedIn}, \"{this.PropertyName}\", {dtmiSegment}, null, {minCount}, isPlural: true, idRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].IdRequired)}, typeRequired: {ParserGeneratorValues.GetBooleanLiteral(this.PropertyDigest.PropertyVersions[dtdlVersion].TypeRequired)}, globalize: globalize, allowReservedIds: allowReservedIds, allowIdReferenceSyntax: allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames: ignoreElementsWithAutoIDsAndDuplicateNames, allowedVersions: this.{this.AllowedVersionsField}V{dtdlVersion});");
        }
    }
}
