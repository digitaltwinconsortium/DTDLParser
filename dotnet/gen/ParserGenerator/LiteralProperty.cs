namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a literal property on a class that is materialized in the parser object model.
    /// </summary>
    public abstract class LiteralProperty : MaterialProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        public LiteralProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
        }

        /// <inheritdoc/>
        public override PropertyKind PropertyKind
        {
            get => PropertyKind.Literal;
        }

        /// <inheritdoc/>
        public override bool IsParseable(int dtdlVersion)
        {
            return this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed;
        }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);
        }

        /// <inheritdoc/>
        public override void AddCheckForRequiredProperty(int dtdlVersion, CsScope scope, string jsonLdEltVar)
        {
            if (!this.PropertyDigest.IsOptional && this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed)
            {
                scope.If($"{this.ExtantPropVarName} == null")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"missingLiteralProperty\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyName: \"{this.PropertyName}\",")
                        .Line($"element: {jsonLdEltVar});");
            }
        }

        /// <inheritdoc/>
        public override void AddObjectPropertiesToArray(CsScope scope, string arrayVariable, string referencesVariable)
        {
        }

        /// <inheritdoc/>
        public override void AddCaseToTrySetObjectPropertySwitch(CsSwitch switchOnProperty, string layerVar, string jsonLdPropVar, string elementVar, string keyPropVar, string keyValueVar, string parsingErrorCollectionVar)
        {
        }
    }
}
