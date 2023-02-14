namespace DTDLParser
{
    /// <summary>
    /// Static class that holds values used by the Metaparser.
    /// </summary>
    internal static class ParserGeneratorValues
    {
        /// <summary>
        /// Sub-namespace for DTDL obverse classes and supplemental classes.
        /// </summary>
        public const string ElementSubNamespace = "Models";

        /// <summary>
        /// Type of the identifier for referencing model elements.
        /// </summary>
        public const string IdentifierType = "Dtmi";

        /// <summary>
        /// Name to use for the class identifier property on a material class.
        /// </summary>
        public const string ClassIdName = "ClassId";

        /// <summary>
        /// Name to use for the identifier property on a material class.
        /// </summary>
        public const string IdentifierName = "Id";

        /// <summary>
        /// Name to use for the defining parent property on a material class.
        /// </summary>
        public const string DefiningParentName = "ChildOf";

        /// <summary>
        /// Name to use for the property on a material class that indicates the property by which the parent is linked to the element.
        /// </summary>
        public const string DefiningPropertyName = "MyPropertyName";

        /// <summary>
        /// Name to use for the defining partition property on a material class.
        /// </summary>
        public const string DefiningPartitionName = "DefinedIn";

        /// <summary>
        /// Name to use for the property on a material class that provides a set of references to parent DTDL elements.
        /// </summary>
        public const string ParentReferencesName = "ParentReferences";

        /// <summary>
        /// Name of the internal property in obverse classes that holds a list of names of layers in which the class instance has been defined.
        /// </summary>
        public const string LayersPropertyName = "Layers";

        /// <summary>
        /// Name of the internal property in obverse classes that holds the DTDL version in which an instance of the class is defined.
        /// </summary>
        public const string DtdlVersionPropertyName = "DtdlVersion";

        /// <summary>
        /// Name of the SupplementalTypes property to generate in augmentable obverse classes.
        /// </summary>
        public const string SupplementalTypesPropertyName = "SupplementalTypes";

        /// <summary>
        /// Name of the SupplementalProperties property to generate in augmentable obverse classes.
        /// </summary>
        public const string SupplementalPropertiesPropertyName = "SupplementalProperties";

        /// <summary>
        /// Name of the UndefinedTypes property to generate in obverse classes.
        /// </summary>
        public const string UndefinedTypesPropertyName = "UndefinedTypes";

        /// <summary>
        /// Name of the UndefinedProperties property to generate in obverse classes.
        /// </summary>
        public const string UndefinedPropertiesPropertyName = "UndefinedProperties";

        /// <summary>
        /// Name of the property to generate on a material class to indicate whether it is a partition.
        /// </summary>
        public const string IsPartitionPropertyName = "IsPartition";

        /// <summary>
        /// Name of the property to generate on a material class to provide a dictionary of singular string literal properties and their values.
        /// </summary>
        public const string StringPropertiesPropertyName = "StringProperties";

        /// <summary>
        /// Name of the property to generate on a material class to indicate whether the class is an allowed cotype for a supplemental class that is designated as mergeable.
        /// </summary>
        public const string MaybePartialPropertyName = "MaybePartial";

        /// <summary>
        /// Name of the dictionary property to generate on a material class that maps from layer name to the <c>JsonLdElement</c> that defines the layer of the element.
        /// </summary>
        public const string JsonLdElementsPropertyName = "JsonLdElements";

        /// <summary>
        /// Name of the ValidateInstance method to generate in validating obverse classes.
        /// </summary>
        public const string ValidateInstanceMethodName = "ValidateInstance";

        /// <summary>
        /// Name of the GetJsonLdText method to generate in partition obverse classes.
        /// </summary>
        public const string GetJsonLdTextMethodName = "GetJsonLdText";

        /// <summary>
        /// Name of the GetJsonLd method to generate in partition obverse classes.
        /// </summary>
        public const string GetJsonLdMethodName = "GetJsonLd";

        /// <summary>
        /// Name of the pseudo-obverse class used for object property values that indicate obverse references.
        /// </summary>
        public const string ReferenceObverseName = "Reference";

        /// <summary>
        /// Prefex for generating field names of properties that shadow other properties when importing via descendant control.
        /// </summary>
        public const string ShadowPropertyPrefix = "original";

        /// <summary>
        /// The type of boolean values in the C# language.
        /// </summary>
        public const string ObverseTypeBoolean = "bool";

        /// <summary>
        /// The type of integer values in the C# language.
        /// </summary>
        public const string ObverseTypeInteger = "int";

        /// <summary>
        /// The type of string values in the C# language.
        /// </summary>
        public const string ObverseTypeString = "string";

        /// <summary>
        /// The keyword for the boolean literal value true in the C# language.
        /// </summary>
        public const string ObverseValueTrue = "true";

        /// <summary>
        /// The keyword for the boolean literal value false in the C# language.
        /// </summary>
        public const string ObverseValueFalse = "false";

        /// <summary>
        /// String form of the URI prefix for DTDL context versions greater than 1.
        /// </summary>
        public const string DtdlContextPrefix = "dtmi:dtdl:context;";

        /// <summary>
        /// Filename of the DTDL language configuration JSON file.
        /// </summary>
        public const string LanguageConfigurationFileName = "dtdl.json";

        /// <summary>
        /// Name of the model elements file to generate from the configured source element files.
        /// </summary>
        public const string ElementsFileName = "ModelElements.g.json";

        /// <summary>
        /// Gets the context ID for a given DTDL language version.
        /// </summary>
        /// <param name="dtdlVersion">The version of the DTDL language.</param>
        /// <returns>A string representation of the DTMI for the DTDL context.</returns>
        public static string GetDtdlContextIdString(int dtdlVersion)
        {
            return DtdlContextPrefix + dtdlVersion;
        }

        /// <summary>
        /// Gets a boolean literal value in the C# language.
        /// </summary>
        /// <param name="value">The value of the boolean literal to get.</param>
        /// <returns>A string representation of the C# keyword.</returns>
        public static string GetBooleanLiteral(bool value)
        {
            return value ? ParserGeneratorValues.ObverseValueTrue : ParserGeneratorValues.ObverseValueFalse;
        }
    }
}
