namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a property constraint on supplemental type.
    /// </summary>
    public class SupplementalParentConstraint
    {
        private string parentPropertyName;
        private string requiredParentCotypeArg;
        private string requiredParentCotypeStringArg;
        private string adjunctTypeIsUniqueArg;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalParentConstraint"/> class.
        /// </summary>
        /// <param name="supplementalConstraintDigest">A <see cref="SupplementalConstraintDigest"/> providing supplemental constraint information extracted from the metamodel digest.</param>
        public SupplementalParentConstraint(SupplementalConstraintDigest supplementalConstraintDigest)
        {
            this.parentPropertyName = supplementalConstraintDigest.PropertyName;

            string requiredParentCotype = supplementalConstraintDigest.RequiredTypes?.FirstOrDefault();
            if (requiredParentCotype != null)
            {
                this.requiredParentCotypeArg = $"new {ParserGeneratorValues.IdentifierType}(\"{requiredParentCotype}\")";
                this.requiredParentCotypeStringArg = $"\"{supplementalConstraintDigest.RequiredTypesString}\"";
            }
            else
            {
                this.requiredParentCotypeArg = "null";
                this.requiredParentCotypeStringArg = "null";
            }

            this.adjunctTypeIsUniqueArg = supplementalConstraintDigest.OnlyOne ? "true" : "false";
        }

        /// <summary>
        /// Add the constraint to a supplemental type instance.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        /// <param name="infoVariableName">Name of the supplementaly type info variable to which to add the constraint.</param>
        public void AddParentConstraint(CsScope scope, string infoVariableName)
        {
            scope.Line($"{infoVariableName}.AddParentConstraint(\"{this.parentPropertyName}\", {this.requiredParentCotypeArg}, {this.requiredParentCotypeStringArg}, adjunctTypeIsUnique: {this.adjunctTypeIsUniqueArg});");
        }
    }
}
