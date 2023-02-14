namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a sibling constraint on supplemental type.
    /// </summary>
    public class SupplementalSiblingConstraint
    {
        private List<string> constraintInitializers;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalSiblingConstraint"/> class.
        /// </summary>
        /// <param name="siblingConstraintDigest">A <see cref="SiblingConstraintDigest"/> providing sibling constraint information extracted from the metamodel digest.</param>
        public SupplementalSiblingConstraint(SiblingConstraintDigest siblingConstraintDigest)
        {
            string keyPropertyName = $"\"{siblingConstraintDigest.KeyPropertyName}\"";
            string keyrefPropertyId = $"new {ParserGeneratorValues.IdentifierType}(\"{siblingConstraintDigest.KeyrefProperty}\")";

            this.constraintInitializers = new List<string>() { $"KeyPropertyName = {keyPropertyName}, KeyrefPropertyId = {keyrefPropertyId}" };

            if (siblingConstraintDigest.RequiredTypes != null)
            {
                string requiredTypes = string.Join(", ", siblingConstraintDigest.RequiredTypes.Select(s => $"new {ParserGeneratorValues.IdentifierType}(\"{s}\")"));
                string requiredTypesString = $"\"{siblingConstraintDigest.RequiredTypesString}\"";

                this.constraintInitializers.Add($"RequiredTypes = new List<{ParserGeneratorValues.IdentifierType}>() {{ {requiredTypes} }}, RequiredTypesString = {requiredTypesString}");
            }

            if (siblingConstraintDigest.DisallowedType != null)
            {
                string disallowedType = $"new {ParserGeneratorValues.IdentifierType}(\"{siblingConstraintDigest.DisallowedType}\")";
                string disallowedTypeName = $"\"{siblingConstraintDigest.DisallowedTypeName}\"";

                this.constraintInitializers.Add($"DisallowedType = {disallowedType}, DisallowedTypeName = {disallowedTypeName}");
            }

            if (siblingConstraintDigest.UniqueReferenceId != null)
            {
                string uniqueReference = $"new {ParserGeneratorValues.IdentifierType}(\"{siblingConstraintDigest.UniqueReferenceId}\")";

                this.constraintInitializers.Add($"UniqueReference = {uniqueReference}");
            }

            if (siblingConstraintDigest.SupplementalPropertyId != null)
            {
                string supplementalPropertyPath = $"new {ParserGeneratorValues.IdentifierType}(\"{siblingConstraintDigest.SupplementalPropertyId}\")";

                this.constraintInitializers.Add($"SupplementalPropertyPath = {supplementalPropertyPath}");
            }
        }

        /// <summary>
        /// Add the constraint to a supplemental type instance.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        /// <param name="infoVariableName">Name of the supplementaly type info variable to which to add the constraint.</param>
        public void AddSiblingConstraint(CsScope scope, string infoVariableName)
        {
            scope.Line($"{infoVariableName}.AddSiblingConstraint(new SiblingConstraint() {{ {string.Join(", ", this.constraintInitializers)} }});");
        }
    }
}
