namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an object property on a class that is materialized in the parser object model.
    /// </summary>
    public abstract class ObjectProperty : MaterialProperty
    {
        private const string InstancePropertiesFieldSuffix = "InstanceProperties";
        private const string AllowedVersionsFieldSuffix = "AllowedVersions";

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        public ObjectProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions)
            : base(propertyName, obversePropertyName, propertyNameUris, propertyDigest, objectModelCustomizer, propertyRestrictions)
        {
            this.ClassName = NameFormatter.FormatNameAsClass(this.PropertyDigest.Class);
            this.VersionedClassName = this.PropertyDigest.PropertyVersions.ToDictionary(p => p.Key, p => NameFormatter.FormatNameAsClass(p.Value.Class));
            this.InstancePropertiesField = $"{propertyName}{InstancePropertiesFieldSuffix}";
            this.AllowedVersionsField = $"{propertyName}{AllowedVersionsFieldSuffix}";
        }

        /// <inheritdoc/>
        public override PropertyKind PropertyKind
        {
            get => PropertyKind.Object;
        }

        /// <summary>
        /// Gets the obverse class name of the object property value.
        /// </summary>
        protected string ClassName { get; }

        /// <summary>
        /// Gets the obverse class name of the object property value for each DTDL version.
        /// </summary>
        protected Dictionary<int, string> VersionedClassName { get; }

        /// <summary>
        /// Gets the name of a field that holds a list of instance property names for the object property.
        /// </summary>
        protected string InstancePropertiesField { get; }

        /// <summary>
        /// Gets the name of a field that holds a list of allowed version numbers for the object property.
        /// </summary>
        protected string AllowedVersionsField { get; }

        /// <inheritdoc/>
        public override bool IsParseable(int dtdlVersion)
        {
            return this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed;
        }

        /// <inheritdoc/>
        public override void GenerateConstructorCode(CsSorted sorted)
        {
            if (!this.PropertyDigest.IsInherited && this.PropertyDigest.ReverseAs != null)
            {
                string reverseProperty = NameFormatter.FormatNameAsProperty(this.PropertyDigest.ReverseAs);
                sorted.Line($"this.{reverseProperty} = new List<{this.ClassName}>();");
            }
        }

        /// <inheritdoc/>
        public override void AddJsonWritingCode(CsScope scope)
        {
            if (!this.PropertyDigest.IsInherited && this.PropertyDigest.ReverseAs != null)
            {
                string reverseProperty = NameFormatter.FormatNameAsProperty(this.PropertyDigest.ReverseAs);
                string varName = $"{reverseProperty}Elt";
                scope.Line($"jsonWriter.WritePropertyName(\"{this.PropertyDigest.ReverseAs}\");");
                scope.Line("jsonWriter.WriteStartArray();");
                scope.Break();
                scope.ForEach($"{this.ClassName} {varName} in this.{reverseProperty}")
                    .Line($"jsonWriter.WriteStringValue({varName}.{ParserGeneratorValues.IdentifierName}.ToString());");
                scope.Break();
                scope.Line("jsonWriter.WriteEndArray();");
                scope.Break();
            }
        }

        /// <inheritdoc/>
        public override void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            base.AddMembers(dtdlVersions, obverseClass, classIsAugmentable);

            if (this.PropertyDigest.PropertyVersions.Any(v => v.Value.IsAllowed) && classIsAugmentable)
            {
                obverseClass.Field(Access.Private, "List<string>", this.InstancePropertiesField, "null");
            }

            foreach (int dtdlVersion in dtdlVersions)
            {
                if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest))
                {
                    obverseClass.Field(Access.Private, "HashSet<int>", $"{this.AllowedVersionsField}V{dtdlVersion}", $"new HashSet<int>() {{ {string.Join(", ", propertyVersionDigest.ClassVersions)} }}");
                }
            }

            if (!this.PropertyDigest.IsInherited && this.PropertyDigest.ReverseAs != null)
            {
                string reverseProperty = NameFormatter.FormatNameAsProperty(this.PropertyDigest.ReverseAs);

                CsProperty property = obverseClass.Property(Access.Public, Novelty.Normal, $"IReadOnlyList<{this.ClassName}>", reverseProperty);
                property.Summary($"Gets a list of {this.ClassName} objects whose '{this.PropertyName}' property identifies the DTDL element corresponding to this object.");
                property.Value($"The {this.ClassName} objects whose '{this.PropertyName}' property refers to this object.");
                property.Get().Set(Access.Internal);

                CsMethod reverseMethod = obverseClass.Method(Access.Private, Novelty.Normal, "void", $"Reverse{this.ObversePropertyName}");

                string varName = "item";
                CsScope iterationScope = this.Iterate(reverseMethod.Body, ref varName);
                iterationScope.Line($"((List<{this.ClassName}>){varName}.{reverseProperty}).Add(this);");
            }
        }

        /// <inheritdoc/>
        public override void AddCheckForRequiredProperty(int dtdlVersion, CsScope scope, string jsonLdEltVar)
        {
            if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed && propertyVersionDigest.MinCount != null && propertyVersionDigest.MinCount > 0)
            {
                scope.If($"{this.ExtantPropVarName} == null")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"missingObjectProperty\",")
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
                iterationScope.Line($"{arrayVariable}.Add(new JArray() {{ \"{this.PropertyName}\", {varName}.{ParserGeneratorValues.IdentifierName}.ToString(), string.Empty }});");
                iterationScope.Line($"{referencesVariable}.Add({varName}.{ParserGeneratorValues.IdentifierName});");
            }
        }

        /// <inheritdoc/>
        public override void AddLiteralPropertiesToArray(CsScope scope, string arrayVariable)
        {
        }

        /// <inheritdoc/>
        public override void AddCaseForInstancePropertySwitch(CsSwitch switchOnProperty, string instancePropVariable)
        {
            if (this.PropertyDigest.PropertyVersions.Any(v => v.Value.IsAllowed))
            {
                switchOnProperty.Case($"\"{this.PropertyName}\"");

                switchOnProperty.If($"this.{this.InstancePropertiesField} == null")
                    .Line($"this.{this.InstancePropertiesField} = new List<string>();");

                switchOnProperty.Line($"this.{this.InstancePropertiesField}.Add({instancePropVariable});");
                switchOnProperty.Line("break;");
            }
        }
    }
}
