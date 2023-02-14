namespace DTDLParser
{
    /// <summary>
    /// Represents a restriction on the set of values a property is allowed to have.
    /// </summary>
    public class PropertyRestrictionUniqueProperties : IPropertyRestriction
    {
        private string propertyName;
        private string uniquePropertyName;
        private string uniquePropertyObverseName;
        private string hashSetName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyRestrictionUniqueProperties"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="uniquePropertyName">The name of the property that must be unique.</param>
        public PropertyRestrictionUniqueProperties(string propertyName, string uniquePropertyName)
        {
            this.propertyName = propertyName;
            this.uniquePropertyName = uniquePropertyName;
            this.uniquePropertyObverseName = NameFormatter.FormatNameAsProperty(uniquePropertyName);
            this.hashSetName = string.Format("{0}{1}Set", this.propertyName, this.uniquePropertyObverseName);
        }

        /// <inheritdoc/>
        public void AddRestriction(CsScope checkRestrictionsMethodBody, string typeName, MaterialProperty materialProperty)
        {
            checkRestrictionsMethodBody.Line($"HashSet<object> {this.hashSetName} = new HashSet<object>();");

            string varName = "item";
            CsScope iterationScope = materialProperty.Iterate(checkRestrictionsMethodBody, ref varName);

            CsIf ifDuplication = iterationScope.If($"{this.hashSetName}.Contains({varName}.{this.uniquePropertyObverseName})");

            ifDuplication
                .Line($"var dupItem = this.{materialProperty.ObversePropertyName}.FirstOrDefault(i => i.JsonLdElements.Any(e => e.Value.Properties.Any(p => p.Name == \"{this.uniquePropertyName}\" && p.Values.Values.Any(v => Helpers.AreObjectsLiteralEqual(v.LiteralValue, {varName}.{this.uniquePropertyObverseName})))));")
                .Line($"JsonLdProperty itemProp = {varName}.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{this.uniquePropertyName}\")).Value?.Properties?.First(p => p.Name == \"{this.uniquePropertyName}\");")
                .Line($"JsonLdProperty dupItemProp = dupItem?.JsonLdElements?.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{this.uniquePropertyName}\")).Value?.Properties?.First(p => p.Name == \"{this.uniquePropertyName}\");");

            ifDuplication
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"nonUniquePropertyValue\",")
                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                    .Line($"propertyName: \"{this.propertyName}\",")
                    .Line($"nestedName: \"{this.uniquePropertyName}\",")
                    .Line($"nestedValue: {varName}.{this.uniquePropertyObverseName}.ToString(),")
                    .Line("incidentProperty: dupItemProp,")
                    .Line("extantProperty: itemProp);");

            iterationScope.Line($"{this.hashSetName}.Add(item.{this.uniquePropertyObverseName});");
        }
    }
}
