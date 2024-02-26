namespace DTDLParser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a property on a class that is materialized in the parser object model.
    /// </summary>
    public abstract class MaterialProperty
    {
        /// <summary>
        /// The suffix applied to generate a field name for a regex pattern string.
        /// </summary>
        protected const string RegexPatternFieldSuffix = "PropertyRegexPatternV";

        private const string PropertyLayerFieldSuffix = "PropertyLayer";
        private const string ValueConstraintsFieldSuffix = "ValueConstraints";

        private Dictionary<int, List<IPropertyRestriction>> propertyRestrictions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialProperty"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property in DTDL.</param>
        /// <param name="obversePropertyName">The name of the property in the C# object model.</param>
        /// <param name="propertyNameUris">A dictionary that maps from DTDL version to the URI of the property name.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="propertyRestrictions">A list of objects that implement the <see cref="IPropertyRestriction"/> interface.</param>
        public MaterialProperty(string propertyName, string obversePropertyName, Dictionary<int, string> propertyNameUris, MaterialPropertyDigest propertyDigest, ObjectModelCustomizer objectModelCustomizer, Dictionary<int, List<IPropertyRestriction>> propertyRestrictions)
        {
            if (objectModelCustomizer != null)
            {
                this.PropertyType = objectModelCustomizer.GetPropertyType("CS", propertyDigest);
            }

            this.PropertyName = propertyName;
            this.ObversePropertyName = obversePropertyName;
            this.PropertyNameUris = propertyNameUris;
            this.PropertyDigest = propertyDigest;

            this.propertyRestrictions = propertyRestrictions;

            if (propertyName != null)
            {
                this.PropertyLayerFieldName = $"{propertyName}{PropertyLayerFieldSuffix}";
                this.ValueConstraintsField = $"{propertyName}{ValueConstraintsFieldSuffix}";
                this.ExtantPropVarName = $"{NameFormatter.FormatNameAsVariable(propertyName)}Property";
            }

            this.ShadowExpression = propertyDigest.IsShadowed ? $"(this.{ParserGeneratorValues.ShadowPropertyPrefix}{obversePropertyName} ?? this.{obversePropertyName})" : $"this.{obversePropertyName}";
        }

        /// <summary>
        /// Gets the kind of property.
        /// </summary>
        public abstract PropertyKind PropertyKind { get; }

        /// <summary>
        /// Gets the form in which the property is represented.
        /// </summary>
        public abstract PropertyRepresentation Representation { get; }

        /// <summary>
        /// Gets or sets the type of the property.
        /// </summary>
        public string PropertyType { get; protected set; }

        /// <summary>
        /// Gets the concrete type of the property.
        /// </summary>
        public abstract string ConcretePropertyType { get; }

        /// <summary>
        /// Gets the name of the key property if the property represenatation is a dictionary, or null if it is not.
        /// </summary>
        public virtual string KeyProperty
        {
            get => null;
        }

        /// <summary>
        /// Gets the name of the property in DTDL.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the name of the property in the C# object model.
        /// </summary>
        public string ObversePropertyName { get; }

        /// <summary>
        /// Gets dictionary that maps from DTDL version to the URI of the property name.
        /// </summary>
        protected Dictionary<int, string> PropertyNameUris { get; }

        /// <summary>
        /// Gets a <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.
        /// </summary>
        protected MaterialPropertyDigest PropertyDigest { get; }

        /// <summary>
        /// Gets the name of a field that indicates the layer in which the property is defined in the model.
        /// </summary>
        protected string PropertyLayerFieldName { get; }

        /// <summary>
        /// Gets the name of a field that holds a list of ValueConstraint objects for the property.
        /// </summary>
        protected string ValueConstraintsField { get; }

        /// <summary>
        /// Gets the name of a variable used within the ParseProperties method for recording extant <c>JsonLdProperty</c> values.
        /// </summary>
        protected string ExtantPropVarName { get; }

        /// <summary>
        /// Gets the name of an expression that provides a shadowed value of the property if shadowed or the obverse name if not.
        /// </summary>
        protected string ShadowExpression { get; }

        /// <summary>
        /// Indicates whether the property's value can be established by the content of a model.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version that defines the property.</param>
        /// <returns>True if the property's value can be established by the content of a model.</returns>
        public abstract bool IsParseable(int dtdlVersion);

        /// <summary>
        /// Generate code for the constructor of the material class that has this property.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code.</param>
        public abstract void GenerateConstructorCode(CsSorted sorted);

        /// <summary>
        /// Generate code for the Equals method of the material class that has this property.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code line.</param>
        public abstract void AddEqualsLine(CsSorted sorted);

        /// <summary>
        /// Generate code for the DeepEquals method of the material class that has this property.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code line.</param>
        public abstract void AddDeepEqualsLine(CsSorted sorted);

        /// <summary>
        /// Generate code for the GetHashCode method of the material class that has this property.
        /// </summary>
        /// <param name="sorted">A <see cref="CsSorted"/> object to which to add the code line.</param>
        public abstract void AddHashLine(CsSorted sorted);

        /// <summary>
        /// Generate code for the WriteToJson method of the material class that has this property.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add the code line.</param>
        public abstract void AddJsonWritingCode(CsScope scope);

        /// <summary>
        /// Generate a TypeScript property type line for this property.
        /// </summary>
        /// <param name="indentedTextWriter">An <see cref="IndentedTextWriter"/> object to which to add the type line.</param>
        public abstract void AddTypeScriptType(IndentedTextWriter indentedTextWriter);

        /// <summary>
        /// Generate appropriate members for the material class that has this property.
        /// </summary>
        /// <param name="dtdlVersions">A list of DTDL major version numbers to generate members for.</param>
        /// <param name="obverseClass">A <see cref="CsClass"/> object to which to add the members.</param>
        /// <param name="classIsAugmentable">True if the material class that has the property is augmentable.</param>
        public virtual void AddMembers(List<int> dtdlVersions, CsClass obverseClass, bool classIsAugmentable)
        {
            if (!this.PropertyDigest.IsInherited)
            {
                string valueString = this.PropertyDigest.IsPlural ? "values" : "value";
                CsProperty property = obverseClass.Property(Access.Public, Novelty.Normal, this.PropertyType, this.ObversePropertyName);
                property.Summary($"Gets the {valueString} of the '{this.PropertyName}' property of the DTDL element that corresponds to this object.");
                property.Value($"The '{this.PropertyName}' property of the DTDL element.");
                property.Get().Set(Access.Internal);
            }

            if (this.PropertyDigest.PropertyVersions.Any(v => v.Value.IsAllowed) && classIsAugmentable)
            {
                obverseClass.Field(Access.Private, "List<ValueConstraint>", this.ValueConstraintsField, "null");
            }
        }

        /// <summary>
        /// Generate code to iterate through all elements of the property and assign each one to a variable.
        /// </summary>
        /// <param name="outerScope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="varName">The name of the varibale to which each element is to be assigned.</param>
        /// <returns>A <see cref="CsScope"/> to which additional code can be added by the caller.</returns>
        public abstract CsScope Iterate(CsScope outerScope, ref string varName);

        /// <summary>
        /// Generate code to select one element of the property and assign it to a variable.
        /// </summary>
        /// <param name="outerScope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="varName">The name of the varibale to which each element is to be assigned.</param>
        /// <returns>A <see cref="CsScope"/> to which additional code can be added by the caller.</returns>
        public abstract CsScope One(CsScope outerScope, ref string varName);

        /// <summary>
        /// Generate code to determine whether the property has at least one value.
        /// </summary>
        /// <param name="outerScope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <returns>A <see cref="CsScope"/> to which additional code can be added by the caller.</returns>
        public abstract CsScope CheckPresence(CsScope outerScope);

        /// <summary>
        /// Generate code to set the defined value, if any, to the property.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version that defines the value for the property.</param>
        /// <param name="scope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="infoVar">Name of the variable that holds the entity info object.</param>
        public virtual void SetValue(int dtdlVersion, CsScope scope, string infoVar)
        {
            if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.Value != null)
            {
                scope.Line($"{infoVar}.{NameFormatter.FormatNameAsProperty(this.PropertyName)} = {propertyVersionDigest.Value};");
            }
        }

        /// <summary>
        /// Generate code that precedes the parse-properties switch statement.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version that defines the value for the property.</param>
        /// <param name="scope">The <see cref="CsScope"/> to which to add the code.</param>
        public virtual void AddPreludeToParseSwitch(int dtdlVersion, CsScope scope)
        {
            if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed)
            {
                scope.Line($"JsonLdProperty {NameFormatter.FormatNameAsVariable(this.PropertyName)}Property = null;");
            }
        }

        /// <summary>
        /// Generate code for the property's case within the parse-properties switch statement.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version that determines the parsing logic for the property.</param>
        /// <param name="switchOnProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        /// <param name="propVar">Name of the variable that holds the <c>"JsonLdProperty"/></c> for the switch.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        /// <param name="classIsPartition">True if the material class is a partition.</param>
        /// <param name="layerVar">Name of the variable that holds the name of the layer currently being parsed.</param>
        /// <param name="valueCountVar">Name of the variable that holds the count of values found by the parse.</param>
        /// <param name="definedInVar">Name of the variable that holds the identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="aggregateContextVar">Name of the variable that holds an <c>AggregateContext</c> for use in parsing identifiers.</param>
        public void AddCaseToParseSwitch(int dtdlVersion, CsSwitch switchOnProperty, string propVar, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar)
        {
            if (this.PropertyDigest.PropertyVersions.TryGetValue(dtdlVersion, out PropertyVersionDigest propertyVersionDigest) && propertyVersionDigest.IsAllowed)
            {
                switchOnProperty.Case($"\"{this.PropertyName}\"");
                switchOnProperty.Case($"\"{this.PropertyNameUris[dtdlVersion]}\"");

                CsIf ifAlreadyDefined = switchOnProperty.If($"{this.ExtantPropVarName} != null");

                ifAlreadyDefined
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"duplicatePropertyName\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line($"propertyName: \"{this.PropertyName}\",")
                        .Line($"incidentProperty: {propVar},")
                        .Line($"extantProperty: {this.ExtantPropVarName},")
                        .Line($"layer: {layerVar});");

                CsElse elseNotAlreadyDefined = ifAlreadyDefined.Else();
                elseNotAlreadyDefined.Line($"{this.ExtantPropVarName} = {propVar};");

                this.AddDetailToParseSwitchCase(dtdlVersion, propVar, propertyVersionDigest, elseNotAlreadyDefined, classIsAugmentable, classIsPartition, layerVar, valueCountVar, definedInVar, aggregateContextVar);

                switchOnProperty.Line("continue;");
            }
        }

        /// <summary>
        /// Generate code to check for a required property.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version that determines whether the property is required.</param>
        /// <param name="scope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="jsonLdEltVar">Name of the variable that holds the <c>JsonLdElement</c> being parsed.</param>
        public abstract void AddCheckForRequiredProperty(int dtdlVersion, CsScope scope, string jsonLdEltVar);

        /// <summary>
        /// Generate code to add the property to a <c>JArray</c> of object properties.
        /// </summary>
        /// <param name="scope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="arrayVariable">Name of a <c>JArray</c> variable to add the property to.</param>
        /// <param name="referencesVariable">Name of a variable to add any object references to.</param>
        public abstract void AddObjectPropertiesToArray(CsScope scope, string arrayVariable, string referencesVariable);

        /// <summary>
        /// Generate code to add the property to a <c>JArray</c> of literal properties.
        /// </summary>
        /// <param name="scope">The <see cref="CsScope"/> to which to add the code.</param>
        /// <param name="arrayVariable">Name of a <c>JArray</c> variable to add the property to.</param>
        public abstract void AddLiteralPropertiesToArray(CsScope scope, string arrayVariable);

        /// <summary>
        /// Generate code for the property's case within the TrySetObjectProperty method's switch statement.
        /// </summary>
        /// <param name="switchOnProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        /// <param name="layerVar">Name of the variable that holds the layer of the property.</param>
        /// <param name="jsonLdPropVar">Name of the variable that holds the <c>JsonLdProperty</c> representing the source of the property by which the parent refers to the referenced element.</param>
        /// <param name="elementVar">Name of the variable that holds the referenced element to set.</param>
        /// <param name="keyPropVar">Name of the variable that holds the property name of a dictionary key.</param>
        /// <param name="keyValueVar">Name of the variable that holds the value of a dictionary key.</param>
        /// <param name="parsingErrorCollectionVar">Name of the variable that holds a <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        public abstract void AddCaseToTrySetObjectPropertySwitch(CsSwitch switchOnProperty, string layerVar, string jsonLdPropVar, string elementVar, string keyPropVar, string keyValueVar, string parsingErrorCollectionVar);

        /// <summary>
        /// Generate code for the property's case within the get children switch statement.
        /// </summary>
        /// <param name="switchOnChildrenProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        /// <returns>True if case added.</returns>
        public virtual bool TryAddCaseToGetChildrenSwitch(CsSwitch switchOnChildrenProperty)
        {
            return false;
        }

        /// <summary>
        /// Generate code for the property's case within the get child switch statement.
        /// </summary>
        /// <param name="switchOnChildrenProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        /// <param name="keyPropertyNameVar">Name of the variable that holds the name of a string property on the child element that uniquely identifies the child.</param>
        /// <param name="keyPropertyValueVar">Name of the variable that holds the value of the unique string property on the child element that indicates which child to get.</param>
        /// <param name="childVar">Name of the variable that holds the identified child.</param>
        /// <returns>True if case added.</returns>
        public virtual bool TryAddCaseToGetChildSwitch(CsSwitch switchOnChildrenProperty, string keyPropertyNameVar, string keyPropertyValueVar, string childVar)
        {
            return false;
        }

        /// <summary>
        /// Generate code for the property's case within the property dictionary switch statement.
        /// </summary>
        /// <param name="switchOnProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        public virtual void AddCaseToDictionaryKeySwitch(CsSwitch switchOnProperty)
        {
        }

        /// <summary>
        /// Generate code for the property's case within the add value constraint switch statement.
        /// </summary>
        /// <param name="switchOnProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        /// <param name="constraintVariable">Name of a <c>ValueConstraint</c> variable to add to the field.</param>
        public void AddCaseForValueConstraintSwitch(CsSwitch switchOnProperty, string constraintVariable)
        {
            if (this.PropertyDigest.PropertyVersions.Any(v => v.Value.IsAllowed))
            {
                switchOnProperty.Case($"\"{this.PropertyName}\"");

                switchOnProperty.If($"this.{this.ValueConstraintsField} == null")
                    .Line($"this.{this.ValueConstraintsField} = new List<ValueConstraint>();");

                switchOnProperty.Line($"this.{this.ValueConstraintsField}.Add({constraintVariable});");
                switchOnProperty.Line("break;");
            }
        }

        /// <summary>
        /// Generate code for the property's case within the add sibling constraint switch statement.
        /// </summary>
        /// <param name="switchOnProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        /// <param name="constraintVariable">Name of a <c>SiblingConstraint</c> variable to add to the field.</param>
        public virtual void AddCaseForSiblingConstraintSwitch(CsSwitch switchOnProperty, string constraintVariable)
        {
        }

        /// <summary>
        /// Generate code for the property's case within the add instance property switch statement.
        /// </summary>
        /// <param name="switchOnProperty">The <see cref="CsSwitch"/> to which to add the code.</param>
        /// <param name="instancePropVariable">Name of a string variable to add to the field.</param>
        public virtual void AddCaseForInstancePropertySwitch(CsSwitch switchOnProperty, string instancePropVariable)
        {
        }

        /// <summary>
        /// Add code to the CheckRestrictions method in the material class that has this property.
        /// </summary>
        /// <param name="checkRestrictionsMethodBody">A <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="dtdlVersion">The DTDL version that specifies the restrictions.</param>
        /// <param name="typeName">The type name (DTDL term) corresponding to the material class.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        public virtual void AddRestrictions(CsScope checkRestrictionsMethodBody, int dtdlVersion, string typeName, bool classIsAugmentable)
        {
            if (this.propertyRestrictions != null && this.propertyRestrictions.TryGetValue(dtdlVersion, out List<IPropertyRestriction> restrictions))
            {
                foreach (IPropertyRestriction propertyRestriction in restrictions)
                {
                    propertyRestriction.AddRestriction(checkRestrictionsMethodBody, typeName, this);
                }
            }
        }

        /// <summary>
        /// Add type-specific code tho the property's case within the parse-properties switch statement.
        /// </summary>
        /// <param name="dtdlVersion">The DTDL version that determines the parsing logic for the property.</param>
        /// <param name="propVar">Name of the variable that holds the <c>"JsonLdProperty"/></c> for the switch.</param>
        /// <param name="propertyVersionDigest">A <see cref="PropertyVersionDigest"/> object containing version-specific property information extracted from the metamodel.</param>
        /// <param name="scope">The <see cref="CsScope"/> object to which to add the code.</param>
        /// <param name="classIsAugmentable">True if the material class is augmentable.</param>
        /// <param name="classIsPartition">True if the material class is a partition.</param>
        /// <param name="layerVar">Name of the variable that holds the name of the layer currently being parsed.</param>
        /// <param name="valueCountVar">Name of the variable that holds the count of values found by the parse.</param>
        /// <param name="definedInVar">Name of the variable that holds the identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="aggregateContextVar">Name of the variable that holds an <c>AggregateContext</c> for use in parsing identifiers.</param>
        protected abstract void AddDetailToParseSwitchCase(int dtdlVersion, string propVar, PropertyVersionDigest propertyVersionDigest, CsScope scope, bool classIsAugmentable, bool classIsPartition, string layerVar, string valueCountVar, string definedInVar, string aggregateContextVar);
    }
}
