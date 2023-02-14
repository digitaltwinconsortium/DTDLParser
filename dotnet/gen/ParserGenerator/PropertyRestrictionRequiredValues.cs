namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a restriction on the set of values a property is allowed to have.
    /// </summary>
    public class PropertyRestrictionRequiredValues : IPropertyRestriction
    {
        private string propertyName;
        private string requiredValues;
        private string conditionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyRestrictionRequiredValues"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="valueUris">A list of URI strings that collectively itemize the allowed values.</param>
        /// <param name="values">A list of strings that collectively itemize the allowed values.</param>
        public PropertyRestrictionRequiredValues(string propertyName, List<string> valueUris, List<string> values)
        {
            this.propertyName = propertyName;
            this.requiredValues = string.Join(" or ", values.Select(v => $"'{v}'"));
            this.conditionString = $"this.{NameFormatter.FormatNameAsProperty(this.propertyName)} != null" + string.Concat(valueUris.Select(v => $" && this.{NameFormatter.FormatNameAsProperty(this.propertyName)}.{ParserGeneratorValues.IdentifierName}.AbsoluteUri != \"{v}\""));
        }

        /// <inheritdoc/>
        public void AddRestriction(CsScope checkRestrictionsMethodBody, string typeName, MaterialProperty materialProperty)
        {
            CsIf ifCondition = checkRestrictionsMethodBody.If(this.conditionString);

            ifCondition.Line($"JsonLdProperty propProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == \"{this.propertyName}\")).Value?.Properties?.First();");

            ifCondition
                .MultiLine("parsingErrorCollection.Notify(")
                    .Line("\"incorrectPropertyValue\",")
                    .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                    .Line($"propertyName: \"{this.propertyName}\",")
                    .Line($"propertyValue: ContextCollection.GetTermOrUri(this.{NameFormatter.FormatNameAsProperty(this.propertyName)}.{ParserGeneratorValues.IdentifierName}),")
                    .Line($"valueRestriction: \"{this.requiredValues}\",")
                    .Line("incidentProperty: propProp);");
        }
    }
}
