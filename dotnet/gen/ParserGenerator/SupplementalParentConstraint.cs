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
        private string requiredParentCotypesArg;
        private string requiredParentCotypeStringArg;
        private string noneOtherTypeArg;
        private string someOtherTypeArg;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalParentConstraint"/> class.
        /// </summary>
        /// <param name="supplementalConstraintDigest">A <see cref="SupplementalConstraintDigest"/> providing supplemental constraint information extracted from the metamodel digest.</param>
        public SupplementalParentConstraint(SupplementalConstraintDigest supplementalConstraintDigest)
        {
            this.parentPropertyName = supplementalConstraintDigest.PropertyName;

            if (supplementalConstraintDigest.RequiredTypes != null)
            {
                this.requiredParentCotypesArg = $"new List<{ParserGeneratorValues.IdentifierType}> {{ {string.Join(", ", supplementalConstraintDigest.RequiredTypes.Select(t => $"new {ParserGeneratorValues.IdentifierType}(\"{t}\")"))} }}";
                this.requiredParentCotypeStringArg = $"\"{supplementalConstraintDigest.RequiredTypesString}\"";
            }
            else
            {
                this.requiredParentCotypesArg = "null";
                this.requiredParentCotypeStringArg = "null";
            }

            this.noneOtherTypeArg = supplementalConstraintDigest.NoneOtherType != null ? $"new {ParserGeneratorValues.IdentifierType}(\"{supplementalConstraintDigest.NoneOtherType}\")" : "null";
            this.someOtherTypeArg = supplementalConstraintDigest.SomeOtherType != null ? $"new {ParserGeneratorValues.IdentifierType}(\"{supplementalConstraintDigest.SomeOtherType}\")" : "null";
        }

        /// <summary>
        /// Add the constraint to a supplemental type instance.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        /// <param name="infoVariableName">Name of the supplementaly type info variable to which to add the constraint.</param>
        public void AddParentConstraint(CsScope scope, string infoVariableName)
        {
            scope.Line($"{infoVariableName}.AddParentConstraint(\"{this.parentPropertyName}\", {this.requiredParentCotypesArg}, {this.requiredParentCotypeStringArg}, {this.noneOtherTypeArg}, {this.someOtherTypeArg});");
        }
    }
}
