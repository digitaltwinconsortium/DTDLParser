namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Generator for a C# class or struct.
    /// </summary>
    public abstract class CsType : CsDeclaration, ICsFile
    {
        private string exports;
        private List<CsField> fields;
        private List<CsConstructor> constructors;
        private List<CsFinalizer> finalizers;
        private List<CsDelegate> delegates;
        private List<CsEnum> enums;
        private List<CsProperty> properties;
        private List<CsMethod> methods;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsType"/> class.
        /// </summary>
        /// <param name="typeType">Value or Reference.</param>
        /// <param name="access">Access level of class or struct.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="typeName">The name of the class or struct being declared.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="completeness">Partial or Full.</param>
        /// <param name="exports">Base type and/or interfaces exported by this type.</param>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsType(TypeType typeType, Access access, Novelty novelty, string typeName, Multiplicity multiplicity = Multiplicity.Instance, Completeness completeness = Completeness.Full, string exports = null, string subNamespace = null)
            : base(
                  access,
                  novelty,
                  typeType == TypeType.Value ? "struct" : "class",
                  typeName,
                  multiplicity: multiplicity,
                  completeness: completeness)
        {
            this.exports = exports;
            this.fields = new List<CsField>();
            this.constructors = new List<CsConstructor>();
            this.finalizers = new List<CsFinalizer>();
            this.delegates = new List<CsDelegate>();
            this.enums = new List<CsEnum>();
            this.properties = new List<CsProperty>();
            this.methods = new List<CsMethod>();

            this.TypeName = typeName;
            this.SubNamespace = subNamespace;
        }

        /// <summary>
        /// Gets the name of the type being declared.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Gets an additional component to be added to the library namespace, or null if there is none.
        /// </summary>
        public string SubNamespace { get; }

        /// <summary>
        /// Gets a value indicating whether the type may have non-private fields that are not static or not readonly.
        /// </summary>
        protected abstract bool AllowNonPrivateFields { get; }

        /// <summary>
        /// Add a C# const to the class or struct.
        /// </summary>
        /// <param name="access">Access level of const value.</param>
        /// <param name="type">Type of const value.</param>
        /// <param name="name">Name of const value.</param>
        /// <param name="value">Value of const value.</param>
        /// <param name="description">Optional text description of constant.</param>
        public void Constant(Access access, string type, string name, string value, string description = null)
        {
            if (name[0] < 'A' || name[0] > 'Z')
            {
                throw new StyleException($"Const field name '{name}' must begin with an uppercase letter -- SA1303.");
            }

            if (description != null && !description.EndsWith("."))
            {
                throw new StyleException($"Documentation text of const field '{name}' must end with a period -- SA1629.");
            }

            CsConst constant = new CsConst(access, type, name, value);
            if (description != null)
            {
                constant.Summary(description);
            }

            this.fields.Add(constant);
        }

        /// <summary>
        /// Add a C# field to the class or struct.
        /// </summary>
        /// <param name="access">Access level of field.</param>
        /// <param name="type">Type of field.</param>
        /// <param name="name">Name of field.</param>
        /// <param name="value">Optional value for field.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="mutability">Constant, ReadOnly, or Mutable.</param>
        /// <param name="description">Optional text description of constant.</param>
        public void Field(Access access, string type, string name, string value = null, Multiplicity multiplicity = Multiplicity.Instance, Mutability mutability = Mutability.Mutable, string description = null)
        {
            if (!this.AllowNonPrivateFields && access != Access.Private && (multiplicity != Multiplicity.Static || mutability != Mutability.ReadOnly))
            {
                throw new StyleException($"Field '{name}' must be private unless static and readonly -- SA1401.");
            }

            if (multiplicity == Multiplicity.Static && mutability == Mutability.ReadOnly)
            {
                if (name[0] < 'A' || name[0] > 'Z')
                {
                    throw new StyleException($"Static readonly field name '{name}' must begin with an uppercase letter -- SA1311.");
                }
            }
            else if (access == Access.Public)
            {
                if (name[0] < 'A' || name[0] > 'Z')
                {
                    throw new StyleException($"Public field name '{name}' must begin with an uppercase letter -- SA1311.");
                }
            }
            else
            {
                if (name[0] < 'a' || name[0] > 'z')
                {
                    throw new StyleException($"Field name '{name}' must begin with a lowercase letter -- SA1306.");
                }
            }

            if (description != null && !description.EndsWith("."))
            {
                throw new StyleException($"Documentation text of field '{name}' must end with a period -- SA1629.");
            }

            CsField field = new CsField(access, type, name, value, multiplicity, mutability);
            if (description != null)
            {
                field.Summary(description);
            }

            this.fields.Add(field);
        }

        /// <summary>
        /// Add a C# constructor to the class or struct.
        /// </summary>
        /// <param name="access">Access level of constructor.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <returns>The <see cref="CsConstructor"/> object added.</returns>
        public CsConstructor Constructor(Access access, Multiplicity multiplicity = Multiplicity.Instance)
        {
            CsConstructor csConstructor = new CsConstructor(access, this.TypeName, multiplicity);
            this.constructors.Add(csConstructor);
            return csConstructor;
        }

        /// <summary>
        /// Add a C# finalizer to the class or struct.
        /// </summary>
        /// <returns>The <see cref="CsFinalizer"/> object added.</returns>
        public CsFinalizer Finalizer()
        {
            CsFinalizer csFinalizer = new CsFinalizer(this.TypeName);
            this.finalizers.Add(csFinalizer);
            return csFinalizer;
        }

        /// <summary>
        /// Add a C# delegate to the class or struct.
        /// </summary>
        /// <param name="access">Access level of delegate.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of delegate.</param>
        /// <param name="name">Name of delegate.</param>
        /// <returns>The <see cref="CsMethod"/> object added.</returns>
        public CsDelegate Delegate(Access access, Novelty novelty, string type, string name)
        {
            CsDelegate csDelegate = new CsDelegate(access, novelty, type, name);
            this.delegates.Add(csDelegate);
            return csDelegate;
        }

        /// <summary>
        /// Add a C# enum to the class or struct.
        /// </summary>
        /// <param name="access">Access level of enum.</param>
        /// <param name="typeName">The name of the enum being declared.</param>
        /// <param name="sorted">True if the enum values should be sorted by name.</param>
        /// <returns>The <see cref="CsEnum"/> object added.</returns>
        public CsEnum Enum(Access access, string typeName, bool sorted = false)
        {
            CsEnum csEnum = new CsEnum(access, typeName, sorted);
            this.enums.Add(csEnum);
            return csEnum;
        }

        /// <summary>
        /// Add a C# property to the class or struct.
        /// </summary>
        /// <param name="access">Access level of property.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of property.</param>
        /// <param name="name">Name of property.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <returns>The <see cref="CsProperty"/> object added.</returns>
        public CsProperty Property(Access access, Novelty novelty, string type, string name, Multiplicity multiplicity = Multiplicity.Instance)
        {
            CsProperty csProperty = new CsProperty(access, novelty, type, name, multiplicity);
            this.properties.Add(csProperty);
            return csProperty;
        }

        /// <summary>
        /// Add a C# method to the class or struct.
        /// </summary>
        /// <param name="access">Access level of method.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of method.</param>
        /// <param name="name">Name of method.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="asynchrony">Aync or Sync.</param>
        /// <param name="completeness">Partial or Full.</param>
        /// <returns>The <see cref="CsMethod"/> object added.</returns>
        public CsMethod Method(Access access, Novelty novelty, string type, string name, Multiplicity multiplicity = Multiplicity.Instance, Asynchrony asynchrony = Asynchrony.Sync, Completeness completeness = Completeness.Full)
        {
            CsMethod csMethod = new CsMethod(hasBody: novelty != Novelty.Abstract, access, novelty, type, name, multiplicity, asynchrony, completeness);
            this.methods.Add(csMethod);
            return csMethod;
        }

        /// <summary>
        /// Determine whether a field with the given <paramref name="name"/> has been added to the class or struct.
        /// </summary>
        /// <param name="name">The name of the field to check for.</param>
        /// <returns>True if a field with the name is present.</returns>
        public bool HasField(string name)
        {
            return this.fields.Any(m => m.Name == name);
        }

        /// <summary>
        /// Determine whether a property with the given <paramref name="name"/> has been added to the class or struct.
        /// </summary>
        /// <param name="name">The name of the property to check for.</param>
        /// <returns>True if a property with the name is present.</returns>
        public bool HasProperty(string name)
        {
            return this.properties.Any(m => m.Name == name);
        }

        /// <summary>
        /// Determine whether a method with the given <paramref name="name"/> has been added to the class or struct.
        /// </summary>
        /// <param name="name">The name of the method to check for.</param>
        /// <returns>True if a method with the name is present.</returns>
        public bool HasMethod(string name)
        {
            return this.methods.Any(m => m.Name == name);
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            this.WriteSummary(codeWriter);
            this.WriteRemarks(codeWriter);

            this.WriteDisables(codeWriter);
            this.WriteAttributes(codeWriter);

            if (this.exports != null)
            {
                codeWriter.WriteLine($"{this.DecoratedName} : {this.exports}");
            }
            else
            {
                codeWriter.WriteLine(this.DecoratedName);
            }

            codeWriter.OpenScope();

            this.fields.Sort();
            foreach (CsField csField in this.fields)
            {
                csField.GenerateCode(codeWriter);
            }

            codeWriter.Break();

            this.constructors.Sort();
            foreach (CsConstructor csConstructor in this.constructors)
            {
                csConstructor.GenerateCode(codeWriter);
            }

            this.finalizers.Sort();
            foreach (CsFinalizer csFinalizer in this.finalizers)
            {
                csFinalizer.GenerateCode(codeWriter);
            }

            this.delegates.Sort();
            foreach (CsDelegate csDelegate in this.delegates)
            {
                csDelegate.GenerateCode(codeWriter);
            }

            this.enums.Sort();
            foreach (CsEnum csEnum in this.enums)
            {
                csEnum.GenerateCode(codeWriter);
            }

            this.properties.Sort();
            foreach (CsProperty csProperty in this.properties)
            {
                csProperty.GenerateCode(codeWriter);
            }

            this.methods.Sort();
            foreach (CsMethod csMethod in this.methods)
            {
                csMethod.GenerateCode(codeWriter);
            }

            codeWriter.CloseScope();
            this.WriteRestores(codeWriter);
        }
    }
}
