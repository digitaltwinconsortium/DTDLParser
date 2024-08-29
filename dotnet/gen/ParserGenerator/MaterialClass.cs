namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a class that is materialized in the parser object model.
    /// </summary>
    public class MaterialClass : ITypeGenerator
    {
        private string typeName;
        private string parentTypeName;
        private string baseTypeName;
        private string kindEnum;
        private string kindProperty;
        private string className;
        private string baseClassName;
        private string kindValue;
        private Dictionary<int, List<ExtensibleMaterialClass>> extensibleMaterialClasses;
        private Dictionary<string, InstanceValidationDigest> classInstanceValidationDigests;
        private Dictionary<string, string> parentMap;
        private List<IDescendantControl> descendantControls;
        private List<int> dtdlVersionsWithApocryphalPropertyCotypeDependency;
        private Dictionary<int, ApocryphaDigest> apocryphaDigests;
        private Access access;
        private bool isLayeringSupported;

        private MaterialClassDigest materialClassDigest;
        private bool isAbstract;
        private bool isOvert;
        private bool isAugmentable;
        private bool isPartition;
        private bool isRootable;
        private bool maybePartial;
        private string parentClass;
        private List<string> typeIds;
        private Dictionary<int, List<string>> extensibleMaterialSubtypes;
        private List<string> standardElementIds;

        private Dictionary<int, List<ConcreteSubclass>> concreteSubclasses;
        private Dictionary<int, List<ConcreteSubclass>> elementalSubclasses;
        private Dictionary<int, List<ConcreteSubclass>> specializableSubclasses;

        private List<MaterialProperty> properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialClass"/> class.
        /// </summary>
        /// <param name="typeName">The name of the class.</param>
        /// <param name="parentTypeName">The name of the parent type.</param>
        /// <param name="baseTypeName">The name of the base type of all entities.</param>
        /// <param name="materialClassDigest">A <see cref="MaterialClassDigest"/> object containing digested information about the material class.</param>
        /// <param name="objectModelCustomizer">An <see cref="ObjectModelCustomizer"/> for generating material property types.</param>
        /// <param name="contexts">A dictionary that maps from a context ID to a dictionary of term definitions.</param>
        /// <param name="classIdentifierDefinitionRestrictions">A dictionary that maps from class name to a dictionary that maps from DTDL version to a <see cref="StringRestriction"/> object that restricts the identifiers for the class.</param>
        /// <param name="extensibleMaterialClasses">A map from DTDL version to a list of <see cref="ExtensibleMaterialClass"/> objects.</param>
        /// <param name="classInstanceValidationDigests">A map from DTDL type name to an <see cref="InstanceValidationDigest"/> object providing instance validation criteria for the DTDL type.</param>
        /// <param name="parentMap">A dictionary that maps from class type to parent class type.</param>
        /// <param name="descendantControls">A list of objects that implement the <see cref="IDescendantControl"/> interface.</param>
        /// <param name="dtdlVersionsWithApocryphalPropertyCotypeDependency">A list of DTDL versions for which apocryphal properties are dependent on an apocryphal cotype.</param>
        /// <param name="apocryphaDigests">A dictionary that maps from DTDL version to an <see cref="ApocryphaDigest"/> object that describes the conditions for allowing apocryphal terms and IRIs.</param>
        /// <param name="access">Access level for the class declaration.</param>
        /// <param name="isLayeringSupported">True if multiple model layers are supported by any recognized language version or extension.</param>
        public MaterialClass(
            string typeName,
            string parentTypeName,
            string baseTypeName,
            MaterialClassDigest materialClassDigest,
            ObjectModelCustomizer objectModelCustomizer,
            Dictionary<string, Dictionary<string, string>> contexts,
            Dictionary<string, Dictionary<int, StringRestriction>> classIdentifierDefinitionRestrictions,
            Dictionary<int, List<ExtensibleMaterialClass>> extensibleMaterialClasses,
            Dictionary<string, InstanceValidationDigest> classInstanceValidationDigests,
            Dictionary<string, string> parentMap,
            List<IDescendantControl> descendantControls,
            List<int> dtdlVersionsWithApocryphalPropertyCotypeDependency,
            Dictionary<int, ApocryphaDigest> apocryphaDigests,
            Access access,
            bool isLayeringSupported)
        {
            this.typeName = typeName;
            this.parentTypeName = parentTypeName;
            this.baseTypeName = baseTypeName;
            this.kindEnum = NameFormatter.FormatNameAsEnum(baseTypeName);
            this.kindProperty = NameFormatter.FormatNameAsEnumProperty(baseTypeName);
            this.className = NameFormatter.FormatNameAsClass(typeName);
            this.baseClassName = NameFormatter.FormatNameAsClass(baseTypeName);
            this.kindValue = $"{this.kindEnum}.{NameFormatter.FormatNameAsEnumValue(typeName)}";
            this.extensibleMaterialClasses = extensibleMaterialClasses;
            this.classInstanceValidationDigests = classInstanceValidationDigests;
            this.parentMap = parentMap;
            this.descendantControls = descendantControls;
            this.dtdlVersionsWithApocryphalPropertyCotypeDependency = dtdlVersionsWithApocryphalPropertyCotypeDependency;
            this.apocryphaDigests = apocryphaDigests;
            this.access = access;
            this.isLayeringSupported = isLayeringSupported;

            this.materialClassDigest = materialClassDigest;
            this.isAbstract = materialClassDigest.IsAbstract;
            this.isOvert = materialClassDigest.IsOvert;
            this.isAugmentable = materialClassDigest.IsAugmentable;
            this.isPartition = materialClassDigest.IsPartition;
            this.isRootable = materialClassDigest.IsRootable;
            this.maybePartial = materialClassDigest.MaybePartial;
            this.parentClass = parentTypeName != null ? NameFormatter.FormatNameAsClass(parentTypeName) : null;
            this.typeIds = materialClassDigest.TypeIds;
            this.extensibleMaterialSubtypes = this.materialClassDigest.DtdlVersions.ToDictionary(v => v, v => materialClassDigest.ExtensibleMaterialSubclasses.TryGetValue(v, out List<string> subtypes) ? subtypes : new List<string>());
            this.standardElementIds = materialClassDigest.StandardElementIds.ContainsKey(2) ? materialClassDigest.StandardElementIds[2] : null;

            this.concreteSubclasses = new Dictionary<int, List<ConcreteSubclass>>();
            this.elementalSubclasses = new Dictionary<int, List<ConcreteSubclass>>();
            this.specializableSubclasses = new Dictionary<int, List<ConcreteSubclass>>();
            foreach (int dtdlVersion in this.materialClassDigest.DtdlVersions)
            {
                List<ConcreteSubclass> concreteSubclasses = new List<ConcreteSubclass>();
                List<ConcreteSubclass> elementalSubclasses = new List<ConcreteSubclass>();
                List<ConcreteSubclass> specializableSubclasses = new List<ConcreteSubclass>();

                if (materialClassDigest.ConcreteSubclasses.ContainsKey(dtdlVersion))
                {
                    List<string> elementalSubclassNames = materialClassDigest.ElementalSubclasses[dtdlVersion];
                    foreach (string subclassName in materialClassDigest.ConcreteSubclasses[dtdlVersion])
                    {
                        ConcreteSubclass concreteSubclass = new ConcreteSubclass(dtdlVersion, subclassName, this.kindEnum, contexts, classIdentifierDefinitionRestrictions);
                        concreteSubclasses.Add(concreteSubclass);
                        if (elementalSubclassNames.Contains(subclassName))
                        {
                            elementalSubclasses.Add(concreteSubclass);
                        }
                        else
                        {
                            specializableSubclasses.Add(concreteSubclass);
                        }
                    }
                }

                this.concreteSubclasses[dtdlVersion] = concreteSubclasses;
                this.elementalSubclasses[dtdlVersion] = elementalSubclasses;
                this.specializableSubclasses[dtdlVersion] = specializableSubclasses;
            }

            MaterialPropertyFactory materialPropertyFactory = new MaterialPropertyFactory(this.materialClassDigest.DtdlVersions, contexts, this.baseClassName, this.kindEnum, this.kindProperty, objectModelCustomizer, this.isLayeringSupported);
            this.properties = new List<MaterialProperty>();
            foreach (KeyValuePair<string, MaterialPropertyDigest> kvp in materialClassDigest.Properties)
            {
                this.properties.Add(materialPropertyFactory.Create(kvp.Key, kvp.Value));
            }

            this.properties.Sort((x, y) => x.PropertyName.CompareTo(y.PropertyName));

            if (this.parentClass == null)
            {
                this.properties.Add(new InternalProperty(
                    ParserGeneratorValues.IdentifierType,
                    ParserGeneratorValues.IdentifierType,
                    ParserGeneratorValues.IdentifierName,
                    Access.Public,
                    NameFormatter.FormatNameAsParameter(ParserGeneratorValues.IdentifierName),
                    "Gets the identifier of the DTDL element that corresponds to this object.",
                    "Identifier of the DTDL element.",
                    isRelevantToIdentity: false,
                    isSettable: false,
                    isNullable: false));

                this.properties.Add(new InternalProperty(
                    ParserGeneratorValues.IdentifierType,
                    ParserGeneratorValues.IdentifierType,
                    ParserGeneratorValues.DefiningParentName,
                    Access.Public,
                    NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningParentName),
                    "Gets the identifier of the parent DTDL element in which this element is defined.",
                    "Identifier of the parent DTDL element.",
                    isRelevantToIdentity: false,
                    isSettable: false,
                    isNullable: true));

                this.properties.Add(new InternalProperty(
                    ParserGeneratorValues.IdentifierType,
                    ParserGeneratorValues.IdentifierType,
                    ParserGeneratorValues.DefiningPartitionName,
                    Access.Public,
                    NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningPartitionName),
                    "Gets the identifier of the partition DTDL element in which this element is defined.",
                    "Identifier of the partition DTDL element.",
                    isRelevantToIdentity: false,
                    isSettable: false,
                    isNullable: true));

                this.properties.Add(new InternalProperty(
                    NameFormatter.FormatNameAsEnum(this.baseTypeName),
                    NameFormatter.FormatNameAsEnum(this.baseTypeName),
                    NameFormatter.FormatNameAsEnumProperty(this.baseTypeName),
                    Access.Public,
                    NameFormatter.FormatNameAsEnumParameter(this.baseTypeName),
                    $"Gets the kind of {this.baseTypeName}, meaning the concrete DTDL type assigned to the corresponding element in the model.",
                    $"The kind of {this.baseTypeName}.",
                    isRelevantToIdentity: true,
                    isSettable: false,
                    isNullable: false));

                if (this.isLayeringSupported)
                {
                    this.properties.Add(new InternalProperty(
                        $"IReadOnlyList<{ParserGeneratorValues.ObverseTypeString}>",
                        $"List<{ParserGeneratorValues.ObverseTypeString}>",
                        ParserGeneratorValues.LayersPropertyName,
                        Access.Public,
                        $"new List<{ParserGeneratorValues.ObverseTypeString}>()",
                        "Gets a list of names of layers in which this element is defined.",
                        "A list of layer names.",
                        isRelevantToIdentity: false,
                        isSettable: true,
                        isNullable: false));
                }

                this.properties.Add(new InternalProperty(
                    ParserGeneratorValues.ObverseTypeInteger,
                    ParserGeneratorValues.ObverseTypeInteger,
                    ParserGeneratorValues.DtdlVersionPropertyName,
                    Access.Internal,
                    NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DtdlVersionPropertyName),
                    "Gets the DTDL version in which this element is defined.",
                    "The DTDL version.",
                    isRelevantToIdentity: false,
                    isSettable: false,
                    isNullable: false));

                this.properties.Add(new InternalProperty(
                    ParserGeneratorValues.ObverseTypeString,
                    ParserGeneratorValues.ObverseTypeString,
                    ParserGeneratorValues.DefiningPropertyName,
                    Access.Internal,
                    NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningPropertyName),
                    "Gets the name of the property by which the parent DTDL element refers to this element.",
                    "The referenced property name.",
                    isRelevantToIdentity: false,
                    isSettable: false,
                    isNullable: true));

                this.properties.Add(new InternalProperty(
                    "string",
                    "string",
                    ParserGeneratorValues.LimitSpecifierPropertyName,
                    Access.Internal,
                    "string.Empty",
                    "Gets the limit specifier for the context that was active when the instance was defined.",
                    "A limit specifier string",
                    isRelevantToIdentity: false,
                    isSettable: true,
                    isNullable: false));

                this.properties.Add(new InternalProperty(
                    "HashSet<ParentReference>",
                    "HashSet<ParentReference>",
                    ParserGeneratorValues.ParentReferencesName,
                    Access.Internal,
                    $"new HashSet<ParentReference>()",
                    "Gets a set of references to parent DTDL elements.",
                    "A set of parent references",
                    isRelevantToIdentity: false,
                    isSettable: false,
                    isNullable: false));

                this.properties.Add(new InternalProperty(
                    $"IReadOnlyDictionary<string, {ParserGeneratorValues.ObverseTypeString}>",
                    $"Dictionary<string, {ParserGeneratorValues.ObverseTypeString}>",
                    ParserGeneratorValues.StringPropertiesPropertyName,
                    Access.Internal,
                    $"new Dictionary<string, {ParserGeneratorValues.ObverseTypeString}>()",
                    "Gets a dictionary of singular string literal properties and their values.",
                    "Dictionary mappyng string literal properties to values.",
                    isRelevantToIdentity: false,
                    isSettable: true,
                    isNullable: false));

                this.properties.Add(new InternalProperty(
                    ParserGeneratorValues.ObverseTypeBoolean,
                    ParserGeneratorValues.ObverseTypeBoolean,
                    ParserGeneratorValues.MaybePartialPropertyName,
                    Access.Internal,
                    null,
                    "Gets a value indicating whether the class is an allowed cotype for a supplemental class that is designated as mergeable.",
                    "True if it is possible for the class to be mergeable.",
                    isRelevantToIdentity: false,
                    isSettable: true,
                    isNullable: false));
            }
        }

        /// <inheritdoc/>
        public void GenerateCode(CsLibrary parserLibrary)
        {
            bool anyObjectProperties = this.properties.Any(p => p.PropertyKind == PropertyKind.Object);

            CsClass obverseClass = parserLibrary.Class(this.access, this.isAbstract ? Novelty.Abstract : Novelty.Normal, this.className, exports: this.GetExports(), subNamespace: ParserGeneratorValues.ElementSubNamespace);
            obverseClass.Summary($"Class <c>{this.className}</c> corresponds to an element of type {this.typeName} in a DTDL model.");
            if (this.parentClass == null)
            {
                obverseClass.Remarks("This is the base class for all classes that correspond to elements in DTDL models.");
            }

            CsConstructor staticConstructor = obverseClass.Constructor(Access.Implicit, Multiplicity.Static);
            this.GenerateVersionlessTypes(obverseClass, staticConstructor.Body);
            this.GenerateMaterialPropertyNames(obverseClass, staticConstructor.Body);
            this.GenerateConcreteKinds(obverseClass, staticConstructor.Body);
            this.GenerateBadTypeFormatStrings(obverseClass, staticConstructor.Body);

            MaterialClassEqualizer.AddMembers(obverseClass, this.className, this.kindProperty, this.parentClass == null, this.isAugmentable, this.properties, this.parentMap);

            if (!this.isAbstract)
            {
                this.GenerateConstructor(obverseClass, isConcrete: true);
            }

            this.GenerateConstructor(obverseClass, isConcrete: false);

            foreach (KeyValuePair<int, List<ConcreteSubclass>> kvp in this.concreteSubclasses)
            {
                foreach (ConcreteSubclass concreteSubclass in kvp.Value)
                {
                    concreteSubclass.AddMembers(obverseClass, this.typeName);
                }
            }

            foreach (MaterialProperty materialProperty in this.properties)
            {
                materialProperty.AddMembers(this.materialClassDigest.DtdlVersions, obverseClass, this.isAugmentable);
            }

            MaterialClassAugmentor.AddMembers(obverseClass, this.typeName, this.isOvert, this.isAugmentable, this.parentClass == null, anyObjectProperties);

            MaterialClassPartitioner.AddMembers(obverseClass, this.typeName, this.isPartition, this.parentClass == null);

            MaterialClassPresenter.AddMembers(obverseClass, this.typeName, this.isPartition, this.isRootable, this.parentClass == null, this.isLayeringSupported);

            MaterialClassJsonizer.AddMembers(obverseClass, this.typeName, this.parentClass == null, this.properties);

            MaterialClassParser.AddMembers(
                this.materialClassDigest.DtdlVersions,
                obverseClass,
                this.typeName,
                this.baseClassName,
                this.kindEnum,
                this.kindProperty,
                this.parentClass == null,
                this.isAbstract,
                this.isAugmentable,
                this.isPartition,
                this.isLayeringSupported,
                this.concreteSubclasses,
                this.specializableSubclasses,
                this.extensibleMaterialClasses,
                this.extensibleMaterialSubtypes,
                this.properties,
                this.dtdlVersionsWithApocryphalPropertyCotypeDependency,
                this.apocryphaDigests);

            MaterialClassValidator.AddMembers(
                this.materialClassDigest.DtdlVersions,
                obverseClass,
                this.kindProperty,
                this.parentClass == null,
                this.isAbstract,
                this.materialClassDigest.Instance,
                this.materialClassDigest.Properties,
                this.classInstanceValidationDigests);

            this.GenerateClassIdProperty(obverseClass);
            this.GenerateApplyTransformationsMethods(obverseClass);
            this.GenerateCheckRestrictionsMethods(obverseClass, this.isAugmentable);
            this.GenerateCheckForDisallowedCocotypesMethod(obverseClass);

            this.GenerateTrySetObjectPropertyMethod(obverseClass);

            foreach (IDescendantControl descendantControl in this.descendantControls)
            {
                descendantControl.AddMembers(obverseClass, this.typeName, this.parentClass == null, this.isAbstract, this.properties);
            }
        }

        /// <summary>
        /// Add TypeScript info for the class to a <c>MaterialClassJsonizer</c>.
        /// </summary>
        /// <param name="jsonizer">The <c>MaterialClassJsonizer</c> to use for generating and outputting the TypeScript info.</param>
        public void AddTypeScriptTypeInfo(MaterialClassJsonizer jsonizer)
        {
            jsonizer.AddTypeInfo(this.typeName, this.parentTypeName, this.materialClassDigest, this.properties);
        }

        private string GetExports()
        {
            StringBuilder exportsBuilder = new StringBuilder();

            if (this.parentClass != null)
            {
                exportsBuilder.Append($"{this.parentClass}, ");
            }

            exportsBuilder.Append("ITypeChecker, ");

            if (this.isAugmentable)
            {
                exportsBuilder.Append("IConstrainer, ");

                if (this.properties.Any(p => p.PropertyKind == PropertyKind.Object))
                {
                    exportsBuilder.Append("IPropertyInstanceBinder, ");
                }
            }

            if (this.isRootable)
            {
                exportsBuilder.Append("IRootable, ");
            }

            exportsBuilder.Append($"IEquatable<{this.className}>");

            return exportsBuilder.ToString();
        }

        private void GenerateConstructor(CsClass obverseClass, bool isConcrete)
        {
            string dtdlVersionParamName = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DtdlVersionPropertyName);
            string idParamName = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.IdentifierName);
            string definingParentParamName = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningParentName);
            string definingPropertyParamName = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningPropertyName);
            string definingPartitionParamName = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningPartitionName);
            string kindParamName = NameFormatter.FormatNameAsEnumParameter(this.baseTypeName);

            CsConstructor constructor = obverseClass.Constructor(Access.Internal);
            constructor.Param(ParserGeneratorValues.ObverseTypeInteger, dtdlVersionParamName, $"Version of DTDL used to define the {this.typeName}.");
            constructor.Param(ParserGeneratorValues.IdentifierType, idParamName, $"Identifier for the {this.typeName}.");
            constructor.Param(ParserGeneratorValues.IdentifierType, definingParentParamName, $"Identifier of the parent element in which this {this.typeName} is defined.");
            constructor.Param(ParserGeneratorValues.ObverseTypeString, definingPropertyParamName, $"Name of the property by which the parent DTDL element refers to this {this.typeName}.");
            constructor.Param(ParserGeneratorValues.IdentifierType, definingPartitionParamName, $"Identifier of the partition in which this {this.typeName} is defined.");

            if (this.isAugmentable && !this.isOvert)
            {
                constructor.Param(ParserGeneratorValues.IdentifierType, "implicitSupplementalTypeId", "Identifier for an implicit supplemantal type.");
            }

            if (isConcrete)
            {
                if (this.parentClass != null)
                {
                    constructor.Preamble($": base({dtdlVersionParamName}, {idParamName}, {definingParentParamName}, {definingPropertyParamName}, {definingPartitionParamName}, {this.kindValue})");
                }
            }
            else
            {
                constructor.Param(this.kindEnum, kindParamName, $"The kind of {this.baseTypeName}, which may be other than {this.typeName} if this constructor is called from a derived class.");
                if (this.parentClass != null)
                {
                    constructor.Preamble($": base({dtdlVersionParamName}, {idParamName}, {definingParentParamName}, {definingPropertyParamName}, {definingPartitionParamName}, {kindParamName})");
                }
            }

            CsSorted sorted = constructor.Body.Sorted();
            foreach (MaterialProperty materialProperty in this.properties)
            {
                materialProperty.GenerateConstructorCode(sorted);
            }

            if (this.parentClass == null)
            {
                string definingParentParam = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningParentName);
                string definingPropNameParam = NameFormatter.FormatNameAsParameter(ParserGeneratorValues.DefiningPropertyName);

                constructor.Body
                    .Break()
                    .If($"{definingParentParam} != null")
                    .Line($"this.{ParserGeneratorValues.ParentReferencesName}.Add(new ParentReference() {{ ParentId = {definingParentParam}, PropertyName = {definingPropNameParam} }});");
            }

            MaterialClassAugmentor.GenerateConstructorCode(constructor.Body, this.isOvert, this.isAugmentable, "implicitSupplementalTypeId");

            MaterialClassPartitioner.GenerateConstructorCode(constructor.Body, this.isPartition);

            MaterialClassPresenter.GenerateConstructorCode(constructor.Body, this.isRootable);

            MaterialClassParser.GenerateConstructorCode(constructor.Body, this.parentClass == null);

            constructor.Body.Break();
            constructor.Body.Line($"this.{ParserGeneratorValues.MaybePartialPropertyName} = {ParserGeneratorValues.GetBooleanLiteral(this.maybePartial)};");
        }

        private void GenerateVersionlessTypes(CsClass obverseClass, CsScope staticConstructorBody)
        {
            obverseClass.Field(Access.Private, "HashSet<string>", "VersionlessTypes", "new HashSet<string>()", Multiplicity.Static, Mutability.ReadOnly);

            foreach (string typeId in this.typeIds)
            {
                staticConstructorBody.Line($"VersionlessTypes.Add(\"{typeId}\");");
            }

            staticConstructorBody.Break();
        }

        private void GenerateMaterialPropertyNames(CsClass obverseClass, CsScope staticConstructorBody)
        {
            obverseClass.Field(Access.Private, "HashSet<string>", "PropertyNames", "new HashSet<string>()", Multiplicity.Static, Mutability.ReadOnly);

            foreach (MaterialProperty materialProperty in this.properties)
            {
                if (materialProperty.PropertyName != null)
                {
                    staticConstructorBody.Line($"PropertyNames.Add(\"{materialProperty.PropertyName}\");");
                }
            }

            staticConstructorBody.Break();

            CsProperty materialPropertiesAccessor = obverseClass.Property(Access.Internal, this.parentClass != null ? Novelty.Override : Novelty.Virtual, "HashSet<string>", "MaterialProperties");
            materialPropertiesAccessor.Summary("Gets material properties allowed by the type of DTDL element.");
            materialPropertiesAccessor.Body().Get()
                .Line($"return PropertyNames;");
        }

        private void GenerateConcreteKinds(CsClass obverseClass, CsScope staticConstructorBody)
        {
            obverseClass.Field(Access.Private, $"Dictionary<int, HashSet<{this.kindEnum}>>", "ConcreteKinds", $"new Dictionary<int, HashSet<{this.kindEnum}>>()", Multiplicity.Static, Mutability.ReadOnly);

            foreach (int dtdlVersion in this.materialClassDigest.DtdlVersions)
            {
                staticConstructorBody.Line($"ConcreteKinds[{dtdlVersion}] = new HashSet<{this.kindEnum}>();");

                if (this.concreteSubclasses.TryGetValue(dtdlVersion, out List<ConcreteSubclass> concreteSubclasses))
                {
                    CsSorted sorted = staticConstructorBody.Sorted();
                    foreach (ConcreteSubclass concreteSubclass in concreteSubclasses)
                    {
                        concreteSubclass.AddEnumValue(sorted, $"ConcreteKinds[{dtdlVersion}]");
                    }
                }

                staticConstructorBody.Break();
            }
        }

        private void GenerateBadTypeFormatStrings(CsClass obverseClass, CsScope staticConstructorBody)
        {
            obverseClass.Field(Access.Private, "Dictionary<int, string>", "BadTypeActionFormat", "new Dictionary<int, string>()", Multiplicity.Static, Mutability.ReadOnly);
            obverseClass.Field(Access.Private, "Dictionary<int, string>", "BadTypeCauseFormat", "new Dictionary<int, string>()", Multiplicity.Static, Mutability.ReadOnly);
            obverseClass.Field(Access.Private, "Dictionary<int, string>", "BadTypeLocatedCauseFormat", "new Dictionary<int, string>()", Multiplicity.Static, Mutability.ReadOnly);

            foreach (KeyValuePair<int, string> kvp in this.materialClassDigest.BadTypeActionFormat)
            {
                staticConstructorBody.Line($"BadTypeActionFormat[{kvp.Key}] = \"{kvp.Value}\";");
            }

            staticConstructorBody.Break();

            foreach (KeyValuePair<int, string> kvp in this.materialClassDigest.BadTypeCauseFormat)
            {
                staticConstructorBody.Line($"BadTypeCauseFormat[{kvp.Key}] = \"{kvp.Value}\";");
            }

            staticConstructorBody.Break();

            foreach (KeyValuePair<int, string> kvp in this.materialClassDigest.BadTypeLocatedCauseFormat)
            {
                staticConstructorBody.Line($"BadTypeLocatedCauseFormat[{kvp.Key}] = \"{kvp.Value}\";");
            }

            staticConstructorBody.Break();
        }

        private void GenerateClassIdProperty(CsClass obverseClass)
        {
            CsProperty prop = obverseClass.Property(Access.Public, this.parentClass == null ? Novelty.Virtual : Novelty.Override, ParserGeneratorValues.IdentifierType, ParserGeneratorValues.ClassIdName);
            prop.Summary($"Get the DTMI that identifies type {this.typeName} in the version of DTDL used to define this element.");
            prop.Value($"The DTMI for the DTDL type {this.typeName}.");
            prop.Body().Get().Line($"return new {ParserGeneratorValues.IdentifierType}($\"dtmi:dtdl:class:{this.typeName};{{this.LanguageMajorVersion}}\");");
        }

        private void GenerateApplyTransformationsMethods(CsClass obverseClass)
        {
            if (this.parentClass == null)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Abstract, "void", "ApplyTransformations");
                baseClassMethod.Summary("Apply any transformations that can only be performed after object properties have been assigned.");
                baseClassMethod.Param("Model", "model", "The model to transform.");
                baseClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            }
            else if (!this.isAbstract)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, "void", "ApplyTransformations");
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param("Model", "model");
                concreteClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                if (this.materialClassDigest.DtdlVersions.Any())
                {
                    CsSwitch switchOnDtdlVersion = concreteClassMethod.Body.Switch($"this.{ParserGeneratorValues.DtdlVersionPropertyName}");

                    foreach (int dtdlVersion in this.materialClassDigest.DtdlVersions)
                    {
                        switchOnDtdlVersion.Case(dtdlVersion.ToString());
                        switchOnDtdlVersion.Line($"this.ApplyTransformationsV{dtdlVersion}(model, parsingErrorCollection);");
                        switchOnDtdlVersion.Line("break;");
                    }
                }

                foreach (KeyValuePair<string, MaterialPropertyDigest> kvp in this.materialClassDigest.Properties)
                {
                    if (!kvp.Value.IsInherited)
                    {
                        if (kvp.Value.Breakout.Any())
                        {
                            concreteClassMethod.Body.Line($"this.BreakOut{NameFormatter.FormatNameAsProperty(kvp.Key)}();");
                        }

                        if (kvp.Value.ReverseAs != null)
                        {
                            concreteClassMethod.Body.Line($"this.Reverse{NameFormatter.FormatNameAsProperty(kvp.Key)}();");
                        }
                    }
                }
            }

            foreach (int dtdlVersion in this.materialClassDigest.DtdlVersions)
            {
                CsMethod versionSpecificClassMethod = obverseClass.Method(Access.Private, Novelty.Normal, "void", $"ApplyTransformationsV{dtdlVersion}");
                versionSpecificClassMethod.Param("Model", "model");
                versionSpecificClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                foreach (IDescendantControl descendantControl in this.descendantControls)
                {
                    descendantControl.AddTransformation(versionSpecificClassMethod.Body, dtdlVersion, this.typeName, this.properties);
                }
            }
        }

        private void GenerateCheckRestrictionsMethods(CsClass obverseClass, bool classIsAugmentable)
        {
            if (this.parentClass == null)
            {
                CsMethod baseClassMethod = obverseClass.Method(Access.Internal, Novelty.Abstract, "void", "CheckRestrictions");
                baseClassMethod.Summary("Check whether the element obeys restrictions that can only be assessed after object properties have been assigned.");
                baseClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            }
            else if (!this.isAbstract)
            {
                CsMethod concreteClassMethod = obverseClass.Method(Access.Internal, Novelty.Override, "void", "CheckRestrictions");
                concreteClassMethod.InheritDoc();
                concreteClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                MaterialClassAugmentor.AddChecksForRestrictions(concreteClassMethod.Body, classIsAugmentable);

                if (this.materialClassDigest.DtdlVersions.Any())
                {
                    CsSwitch switchOnDtdlVersion = concreteClassMethod.Body.Switch($"this.{ParserGeneratorValues.DtdlVersionPropertyName}");

                    foreach (int dtdlVersion in this.materialClassDigest.DtdlVersions)
                    {
                        switchOnDtdlVersion.Case(dtdlVersion.ToString());
                        switchOnDtdlVersion.Line($"this.CheckRestrictionsV{dtdlVersion}(parsingErrorCollection);");
                        switchOnDtdlVersion.Line("break;");
                    }
                }
            }

            foreach (int dtdlVersion in this.materialClassDigest.DtdlVersions)
            {
                CsMethod versionSpecificClassMethod = obverseClass.Method(Access.Private, Novelty.Normal, "void", $"CheckRestrictionsV{dtdlVersion}");
                versionSpecificClassMethod.InheritDoc();
                versionSpecificClassMethod.Param("ParsingErrorCollection", "parsingErrorCollection");

                foreach (MaterialProperty materialProperty in this.properties)
                {
                    materialProperty.AddRestrictions(versionSpecificClassMethod.Body, dtdlVersion, this.typeName, classIsAugmentable);
                }

                foreach (IDescendantControl descendantControl in this.descendantControls)
                {
                    descendantControl.AddRestriction(versionSpecificClassMethod.Body, dtdlVersion, this.typeName);
                }
            }
        }

        private void GenerateCheckForDisallowedCocotypesMethod(CsClass obverseClass)
        {
            if (this.parentClass == null)
            {
                CsMethod method = obverseClass.Method(Access.Internal, Novelty.Normal, "void", "CheckForDisallowedCocotypes");
                method.Summary("Check whether the element has any pairs of cotypes that are mutually exclusive.");
                method.Param($"HashSet<{ParserGeneratorValues.IdentifierType}>", "supplementalTypeIds", "A set of identifiers, each indicating a supplemental type applied to the element.");
                method.Param("List<DTSupplementalTypeInfo>", "supplementalTypes", "A list of <see cref=\"DTSupplementalTypeInfo\"/> objects, each describing a supplemental type applied to the element.");
                method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <see cref=\"ParsingErrorCollection\"/> to which any parsing errors are added.");

                CsForEach forEachTypeInfo = method.Body.ForEach("DTSupplementalTypeInfo supplementalTypeInfo in supplementalTypes");
                CsForEach forEachDisallowedType = forEachTypeInfo.ForEach($"{ParserGeneratorValues.IdentifierType} disallowedCocotype in supplementalTypeInfo.DisallowedCocotypes");
                CsIf ifDisallowed = forEachDisallowedType.If("supplementalTypeIds.Contains(disallowedCocotype)");

                ifDisallowed
                    .Line("string supplementalTypeTerm = ContextCollection.GetTermOrUri(supplementalTypeInfo.Type);")
                    .Line("string disallowedCocotypeTerm = ContextCollection.GetTermOrUri(disallowedCocotype);")
                    .Line("JsonLdElement elt = this.JsonLdElements.FirstOrDefault(e => (e.Value.Types.Contains(supplementalTypeTerm) || e.Value.Types.Contains(supplementalTypeInfo.Type.ToString())) && (e.Value.Types.Contains(disallowedCocotypeTerm) || e.Value.Types.Contains(disallowedCocotype.ToString()))).Value;")
                    .MultiLine("parsingErrorCollection.Notify(")
                        .Line("\"disallowedCocotype\",")
                        .Line($"elementId: this.{ParserGeneratorValues.IdentifierName},")
                        .Line("elementType: supplementalTypeTerm,")
                        .Line("cotype: disallowedCocotypeTerm,")
                        .Line("element: elt);");
            }
        }

        private void GenerateTrySetObjectPropertyMethod(CsClass obverseClass)
        {
            CsMethod method = obverseClass.Method(Access.Internal, this.parentClass != null ? Novelty.Override : Novelty.Virtual, ParserGeneratorValues.ObverseTypeBoolean, "TrySetObjectProperty");
            method.Summary("Try to set an object property with a given <paramref name=\"propertyName\"/>.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "propertyName", "The name of the property whose element value to set if the property is recognized.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "layer", "The key property for dictionary properties.");
            method.Param("JsonLdProperty", "propProp", "The <see cref=\"JsonLdProperty\"/> representing the source of the property by which the parent refers to this element.");
            method.Param(this.baseClassName, "element", "The reference element to set.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "keyProp", "The key property for dictionary properties.");
            method.Param(ParserGeneratorValues.ObverseTypeString, "keyValue", "The key value for dictionary properties.");
            method.Param("ParsingErrorCollection", "parsingErrorCollection", "A <c>ParsingErrorCollection</c> to which any parsing errors are added.");
            method.Returns("True if the property name is recognized.");

            if (this.parentClass != null)
            {
                method.Body.If("base.TrySetObjectProperty(propertyName, layer, propProp, element, keyProp, keyValue, parsingErrorCollection)")
                    .Line("return true;");
            }

            CsSwitch switchOnPropertyName = method.Body.Switch("propertyName");

            foreach (MaterialProperty materialProperty in this.properties)
            {
                materialProperty.AddCaseToTrySetObjectPropertySwitch(switchOnPropertyName, "layer", "propProp", "element", "keyProp", "keyValue", "parsingErrorCollection");
            }

            switchOnPropertyName.Default()
                .Line("break;");

            MaterialClassAugmentor.AddTrySetObjectProperties(method.Body, "propertyName", "element", "keyValue", "layer", "propProp", this.isAugmentable);

            method.Body.Line("return false;");
        }
    }
}
