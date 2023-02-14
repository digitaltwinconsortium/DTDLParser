namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an identifier property on a class that is materialized in the parser object model.
    /// </summary>
    public abstract class IdentifierProperty : MaterialProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="baseClassName">The C# name of the DTDL base class.</param>
        public IdentifierProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, string baseClassName)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.BaseClassName = baseClassName;
        }

        /// <inheritdoc/>
        public override PropertyKind PropertyKind
        {
            get => PropertyKind.Identifier;
        }

        /// <summary>
        /// Gets the C# name of the DTDL base class.
        /// </summary>
        protected string BaseClassName { get; }

        /// <inheritdoc/>
        public override bool IsParseable(int dtdlVersion)
        {
            return this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed;
        }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);

            if (!this.PropertyDigest.IsInherited)
            {
                foreach (int dtdlVersion in dtdlVersions)
                {
                    if (this.PropertyDigest.PropertyVersions[dtdlVersion].Pattern != null)
                    {
                        obverseClass.Field(Access.Internal, "Regex", $"{this.ObversePropertyName}{RegexPatternFieldSuffix}{dtdlVersion}", $"new Regex(@\"{this.PropertyDigest.PropertyVersions[dtdlVersion].Pattern}\", RegexOptions.Compiled)", Multiplicity.Static, Mutability.ReadOnly, $"Regular expression pattern for values of property '{this.ObversePropertyName}' for DTDLv{dtdlVersion}.");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override void AddCheckForRequiredProperty(int dtdlVersion, CsScope scope, string jsonLdEltVar)
        {
            if (!this.PropertyDigest.IsOptional && this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed)
            {
                scope.If($"{this.ExtantPropVarName} == null")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"missingIdentifierProperty\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyName: \"{this.PropertyName}\",")
                        .Line($"element: {jsonLdEltVar});");
            }
        }

        /// <inheritdoc/>
        public override void AddObjectPropertiesToArray(CsScope scope, string arrayVariable, string referencesVariable)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                string varName = "item";
                CsScope iterationScope = this.Iterate(scope, ref varName);
                iterationScope.Line($"{arrayVariable}.Add(new JArray() {{ \"{this.PropertyName}\", {varName}.ToString(), string.Empty }});");
            }
        }

        /// <inheritdoc/>
        public override void AddLiteralPropertiesToArray(CsScope scope, string arrayVariable)
        {
        }
    }
}
