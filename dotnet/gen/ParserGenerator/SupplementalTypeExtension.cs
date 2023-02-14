namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Code generator for instances of standard supplemental types.
    /// </summary>
    public class SupplementalTypeExtension : SupplementalType
    {
        private string kindEnum;
        private string isAbstract;
        private string isMergeable;
        private string extensionKind;
        private string extensionContext;
        private List<SupplementalProperty> properties;
        private List<SupplementalConstraint> constraints;
        private List<SupplementalCotype> cotypes;
        private List<int> cotypeVersions;
        private List<string> cocotypes;
        private List<string> discotypes;
        private List<SupplementalSiblingConstraint> siblingConstraints;
        private string infoVariableName;
        private string parentTypeVariableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalTypeExtension"/> class.
        /// </summary>
        /// <param name="typeUri">The URI of the supplemental type.</param>
        /// <param name="supplementalTypeDigest">A <see cref="SupplementalTypeDigest"/> object providing details about the supplemental type.</param>
        /// <param name="kindEnum">The enum type used to represent DTDL element kind.</param>
        public SupplementalTypeExtension(string typeUri, SupplementalTypeDigest supplementalTypeDigest, string kindEnum)
            : base(typeUri)
        {
            this.kindEnum = kindEnum;

            this.isAbstract = $"isAbstract: {ParserGeneratorValues.GetBooleanLiteral(supplementalTypeDigest.IsAbstract)}";
            this.isMergeable = $"isMergeable: {ParserGeneratorValues.GetBooleanLiteral(supplementalTypeDigest.IsMergeable)}";
            this.extensionKind = supplementalTypeDigest.ExtensionKind;
            this.extensionContext = supplementalTypeDigest.ExtensionContext;

            this.properties = supplementalTypeDigest.Properties.Select(p => new SupplementalProperty(p.Key, p.Value)).ToList();
            this.constraints = supplementalTypeDigest.Constraints.Select(c => new SupplementalConstraint(c)).ToList();
            this.cotypes = supplementalTypeDigest.Cotypes.Select(c => new SupplementalCotype(c, kindEnum)).ToList();
            this.cotypeVersions = supplementalTypeDigest.CotypeVersions;
            this.cocotypes = supplementalTypeDigest.Cocotypes ?? new List<string>();
            this.discotypes = supplementalTypeDigest.Discotypes ?? new List<string>();
            this.siblingConstraints = supplementalTypeDigest.Siblings.Select(s => new SupplementalSiblingConstraint(s)).ToList();

            this.infoVariableName = GetInfoVariableName(typeUri);
            this.parentTypeVariableName = GetTypeVariableName(supplementalTypeDigest.Parent);
        }

        /// <inheritdoc/>
        public override void DefineInfoVariable(CsScope scope, Dictionary<string, string> contextIdVariables)
        {
            scope.Line($"DTSupplementalTypeInfo {this.infoVariableName} = new DTSupplementalTypeInfo(DTExtensionKind.{this.extensionKind}, {contextIdVariables[this.extensionContext]}, {this.TypeVariableName}, {this.isAbstract}, {this.isMergeable}, {this.parentTypeVariableName});");

            foreach (SupplementalProperty property in this.properties)
            {
                property.AddProperty(scope, this.infoVariableName);
            }

            foreach (SupplementalConstraint constraint in this.constraints)
            {
                constraint.AddPropertyValueConstraint(scope, this.infoVariableName);
            }

            if (this.cotypes.Any())
            {
                scope.Line($"{this.infoVariableName}.AllowedCotypeKinds = new HashSet<{this.kindEnum}>() {{ {string.Join(", ", this.cotypes.Select(c => c.KindValue))} }};");
            }

            if (this.cotypeVersions.Any())
            {
                scope.Line($"{this.infoVariableName}.AllowedCotypeVersions = new HashSet<int>() {{ {string.Join(", ", this.cotypeVersions.Select(c => c.ToString()))} }};");
            }

            if (this.cocotypes.Any())
            {
                scope.Line($"{this.infoVariableName}.RequiredCocotypes = new HashSet<Dtmi>() {{ {string.Join(", ", this.cocotypes.Select(c => $"new Dtmi(\"{c}\")"))} }};");
            }

            if (this.discotypes.Any())
            {
                scope.Line($"{this.infoVariableName}.DisallowedCocotypes = new HashSet<Dtmi>() {{ {string.Join(", ", this.discotypes.Select(c => $"new Dtmi(\"{c}\")"))} }};");
            }

            foreach (SupplementalSiblingConstraint siblingConstraint in this.siblingConstraints)
            {
                siblingConstraint.AddSiblingConstraint(scope, this.infoVariableName);
            }
        }

        /// <inheritdoc/>
        public override void AssignInfoVariable(CsScope scope, string dictionaryVariableName)
        {
            scope.Line($"{dictionaryVariableName}[{this.TypeVariableName}] = {this.infoVariableName};");
        }
    }
}
