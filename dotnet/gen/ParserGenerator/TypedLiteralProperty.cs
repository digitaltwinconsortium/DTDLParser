namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a typed literal property on a class that is materialized in the parser object model.
    /// </summary>
    public abstract class TypedLiteralProperty : LiteralProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedLiteralProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        /// <param name="datatype">A string representation of the datatype of the literal property.</param>
        /// <param name="literalType">An object that exports the <see cref="ILiteralType"/> interface to support declaring and parsing a specific literal type.</param>
        public TypedLiteralProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions, string datatype, ILiteralType literalType)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.Datatype = datatype;
            this.LiteralType = literalType;
        }

        /// <summary>
        /// Gets a string representation of the datatype of the literal property.
        /// </summary>
        protected string Datatype { get; }

        /// <summary>
        /// Gets an object that exports the <see cref="ILiteralType"/> interface to support declaring and parsing a specific literal type.
        /// </summary>
        protected ILiteralType LiteralType { get; }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);

            if (!this.PropertyDigest.IsInherited)
            {
                foreach (int dtdlVersion in dtdlVersions)
                {
                    if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest digest) && digest.Pattern != null)
                    {
                        obverseClass.Field(Access.Internal, "Regex", $"{this.ObversePropertyName}{RegexPatternFieldSuffix}{dtdlVersion}", $"new Regex(@\"{digest.Pattern}\", RegexOptions.Compiled)", Multiplicity.Static, Mutability.ReadOnly, $"Regular expression pattern for values of property '{this.ObversePropertyName}' for DTDLv{dtdlVersion}.");
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override void AddLiteralPropertiesToArray(CsScope scope, string arrayVariable)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                string varName = "item";
                CsScope iterationScope = this.Iterate(scope, ref varName);
                iterationScope.Line($"{arrayVariable}.Add(new JArray() {{ \"{this.PropertyName}\", {varName}.ToString(), string.Empty, \"#{this.Datatype}\" }});");
            }
        }
    }
}
