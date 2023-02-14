namespace DTDLParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a singular typed literal property on a class that is materialized in the parser object model.
    /// </summary>
    public class InternalProperty : MaterialProperty
    {
        private Access access;
        private string value;
        private string description;
        private string valueDesc;
        private bool isRelevantToIdentity;
        private bool isSettable;

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalProperty"/> class.
        /// </summary>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="concretePropertyType">The concrete type of the property.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="access">The <see cref="Access"/> for the property.</param>
        /// <param name="value">The value for the property.</param>
        /// <param name="description">Text description of property.</param>
        /// <param name="valueDesc">Text description of property value.</param>
        /// <param name="isRelevantToIdentity">True if the property factors into equivalence and hash calculation.</param>
        /// <param name="isSettable">True if the property can be set outside the constructor.</param>
        public InternalProperty(string propertyType, string concretePropertyType, string obversePropertyName, Access access, string value, string description, string valueDesc, bool isRelevantToIdentity, bool isSettable)
            : base(null, obversePropertyName, new Dictionary<int, string>(), new MaterialPropertyDigest(), null, new Dictionary<int, List<IPropertyRestriction>>())
        {
            this.PropertyType = propertyType;
            this.ConcretePropertyType = concretePropertyType;

            this.access = access;
            this.value = value;
            this.description = description;
            this.valueDesc = valueDesc;
            this.isRelevantToIdentity = isRelevantToIdentity;
            this.isSettable = isSettable;
        }

        /// <inheritdoc/>
        public override PropertyKind PropertyKind
        {
            get => PropertyKind.Internal;
        }

        /// <inheritdoc/>
        public override PropertyRepresentation Representation
        {
            get => PropertyRepresentation.Item;
        }

        /// <inheritdoc/>
        public override string ConcretePropertyType { get; }

        /// <inheritdoc/>
        public override bool IsParseable(int dtdlVersion)
        {
            return false;
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            if (this.value != null)
            {
                sorted.Line($"this.{this.ObversePropertyName} = {this.value};");
            }
        }

        /// <inheritdoc/>
        public override void AddEqualsLine(CsSorted sorted)
        {
            if (this.isRelevantToIdentity)
            {
                sorted.Line($"&& this.{this.ObversePropertyName} == other.{this.ObversePropertyName}");
            }
        }

        /// <inheritdoc/>
        public override void AddDeepEqualsLine(CsSorted sorted)
        {
            if (this.isRelevantToIdentity)
            {
                sorted.Line($"&& this.{this.ObversePropertyName} == other.{this.ObversePropertyName}");
            }
        }

        /// <inheritdoc/>
        public override void AddHashLine(CsSorted sorted)
        {
            if (this.isRelevantToIdentity)
            {
                sorted.Line($"hashCode = (hashCode * 131) + this.{this.ObversePropertyName}.GetHashCode();");
            }
        }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                CsProperty property = obverseClass.Property(this.access, Novelty.Normal, this.PropertyType, this.ObversePropertyName);
                property.Summary(this.description);
                property.Value(this.valueDesc);
                property.Get();
                if (this.isSettable)
                {
                    property.Set(this.access == Access.Public ? Access.Internal : Access.Implicit);
                }
            }
        }

        /// <inheritdoc/>
        public override CsScope Iterate(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}";
            return outerScope;
        }

        /// <inheritdoc/>
        public override CsScope One(CsScope outerScope, ref string varName)
        {
            varName = $"this.{this.ObversePropertyName}";
            return outerScope;
        }

        /// <inheritdoc/>
        public override CsScope CheckPresence(CsScope outerScope)
        {
            return outerScope;
        }

        /// <inheritdoc/>
        public override void SetValue(int dtdlVersion, CsScope scope, string infoVar)
        {
        }

        /// <inheritdoc/>
        public override void AddCheckForRequiredProperty(int dtdlVersion, CsScope scope, string jsonLdEltVar)
        {
        }

        /// <inheritdoc/>
        public override void AddObjectPropertiesToArray(CsScope scope, string arrayVariable, string referencesVariable)
        {
        }

        /// <inheritdoc/>
        public override void AddLiteralPropertiesToArray(CsScope scope, string arrayVariable)
        {
        }

        /// <inheritdoc/>
        public override void AddCaseToTrySetObjectPropertySwitch(CsSwitch switchOnProperty, string layerVar, string jsonLdPropVar, string elementVar, string keyPropVar, string keyValueVar, string parsingErrorCollectionVar)
        {
        }

        /// <inheritdoc/>
        public override void AddRestrictions(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName, bool classIsAugmentable)
        {
        }

        /// <inheritdoc/>
        protected override void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
        }
    }
}
