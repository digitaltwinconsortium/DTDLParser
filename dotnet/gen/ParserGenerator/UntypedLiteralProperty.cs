namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a untyped literal property on a class that is materialized in the parser object model.
    /// </summary>
    public abstract class UntypedLiteralProperty : LiteralProperty
    {
        private const string DatatypeFieldSuffix = "Datatype";

        /// <summary>
        /// Initializes a new instance of the <see cref="UntypedLiteralProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        public UntypedLiteralProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.DatatypeField = $"{propertyName}{DatatypeFieldSuffix}";
        }

        /// <summary>
        /// Gets the name of a field that stores the datatype for this property.
        /// </summary>
        protected string DatatypeField { get; }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);

            if (!this.PropertyDigest.IsInherited)
            {
                obverseClass.Field(Access.Private, ParserGeneratorValues.ObverseTypeString, this.DatatypeField, "string.Empty");
            }
        }

        /// <inheritdoc/>
        public override void AddLiteralPropertiesToArray(CsScope scope, string arrayVariable)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                string varName = "item";
                CsScope iterationScope = this.Iterate(scope, ref varName);
                iterationScope.Line($"{arrayVariable}.Add(new JArray() {{ \"{this.PropertyName}\", {varName}.ToString(), string.Empty, this.{this.DatatypeField} }});");
            }
        }
    }
}
