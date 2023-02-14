namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a property constraint on supplemental type.
    /// </summary>
    public class SupplementalConstraint
    {
        private string propertyName;
        private List<string> valueConstraintInitializers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalConstraint"/> class.
        /// </summary>
        /// <param name="supplementalConstraintDigest">A <see cref="SupplementalConstraintDigest"/> providing supplemental constraint information extracted from the metamodel digest.</param>
        public SupplementalConstraint(SupplementalConstraintDigest supplementalConstraintDigest)
        {
            this.propertyName = supplementalConstraintDigest.PropertyName;

            this.valueConstraintInitializers = new List<string>();

            if (supplementalConstraintDigest.RequiredTypes != null)
            {
                string requiredTypes = string.Join(", ", supplementalConstraintDigest.RequiredTypes.Select(s => $"new {ParserGeneratorValues.IdentifierType}(\"{s}\")"));
                string requiredTypesString = $"\"{supplementalConstraintDigest.RequiredTypesString}\"";

                this.valueConstraintInitializers.Add($"RequiredTypes = new List<{ParserGeneratorValues.IdentifierType}>() {{ {requiredTypes} }}, RequiredTypesString = {requiredTypesString}");
            }

            if (supplementalConstraintDigest.RequiredValues != null)
            {
                string requiredValues = string.Join(", ", supplementalConstraintDigest.RequiredValues.Select(s => $"new {ParserGeneratorValues.IdentifierType}(\"{s}\")"));
                string requiredValuesString = $"\"{supplementalConstraintDigest.RequiredValuesString}\"";

                this.valueConstraintInitializers.Add($"RequiredValues = new List<{ParserGeneratorValues.IdentifierType}>() {{ {requiredValues} }}, RequiredValuesString = {requiredValuesString}");
            }

            if (supplementalConstraintDigest.RequiredLiteral != null)
            {
                string requiredLiteral = ToJsonLiteral(supplementalConstraintDigest.RequiredLiteral, escapeQuotes: false);
                this.valueConstraintInitializers.Add($"RequiredLiteral = {requiredLiteral}");
            }

            if (supplementalConstraintDigest.SiblingKeyrefProperty != null)
            {
                string siblingKeyPropertyName = $"\"{supplementalConstraintDigest.SiblingKeyPropertyName}\"";
                string siblingKeyrefPropertyId = $"new {ParserGeneratorValues.IdentifierType}(\"{supplementalConstraintDigest.SiblingKeyrefProperty}\")";

                this.valueConstraintInitializers.Add($"SiblingKeyPropertyName = {siblingKeyPropertyName}, SiblingKeyrefPropertyId = {siblingKeyrefPropertyId}");
            }

            if (supplementalConstraintDigest.SiblingClassOfProperty != null)
            {
                string siblingClassOfPropertyId = $"new {ParserGeneratorValues.IdentifierType}(\"{supplementalConstraintDigest.SiblingClassOfProperty}\")";

                this.valueConstraintInitializers.Add($"SiblingClassOfPropertyId = {siblingClassOfPropertyId}");
            }

            if (supplementalConstraintDigest.SiblingParentOfProperty != null)
            {
                string siblingParentOfPropertyId = $"new {ParserGeneratorValues.IdentifierType}(\"{supplementalConstraintDigest.SiblingParentOfProperty}\")";

                this.valueConstraintInitializers.Add($"SiblingParentOfPropertyId = {siblingParentOfPropertyId}");
            }
        }

        /// <summary>
        /// Add the constraint to a supplemental type instance.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        /// <param name="infoVariableName">Name of the supplementaly type info variable to which to add the constraint.</param>
        public void AddPropertyValueConstraint(CsScope scope, string infoVariableName)
        {
            scope.Line($"{infoVariableName}.AddPropertyValueConstraint(\"{this.propertyName}\", new ValueConstraint() {{ {string.Join(", ", this.valueConstraintInitializers)} }});");
        }

        private static string ToJsonLiteral(object value, bool escapeQuotes)
        {
            Type valueType = value.GetType();
            if (valueType == typeof(string))
            {
                return escapeQuotes ? $"\\\"{(string)value}\\\"" : $"\"{(string)value}\"";
            }
            else if (valueType == typeof(bool))
            {
                return ParserGeneratorValues.GetBooleanLiteral((bool)value);
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
