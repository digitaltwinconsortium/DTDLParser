namespace DTDLParser
{
    /// <summary>
    /// Generator for C# property.
    /// </summary>
    public class CsProperty : CsDeclaration
    {
        private string valueDesc;
        private CsDeclaration get;
        private CsDeclaration set;
        private CsDeclaration init;
        private string defaultVal;
        private CsPropertyBody body;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsProperty"/> class.
        /// </summary>
        /// <param name="access">Access level of property.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of property.</param>
        /// <param name="name">Name of property.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        public CsProperty(Access access, Novelty novelty, string type, string name, Multiplicity multiplicity = Multiplicity.Instance)
            : base(access, novelty, type, name, multiplicity: multiplicity)
        {
            this.valueDesc = null;
            this.get = null;
            this.set = null;
            this.init = null;
            this.defaultVal = null;
            this.body = null;
        }

        /// <summary>
        /// Describe the value of the property.
        /// </summary>
        /// <param name="valueDesc">Description of the vaule.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsProperty Value(string valueDesc)
        {
            this.valueDesc = valueDesc;
            return this;
        }

        /// <summary>
        /// Add an implicit get accessor to the property.
        /// </summary>
        /// <param name="access">Access level of accessor.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsProperty Get(Access access = Access.Implicit)
        {
            this.get = new CsDeclaration(access, Novelty.Normal, null, "get");
            return this;
        }

        /// <summary>
        /// Add an implicit set accessor to the property.
        /// </summary>
        /// <param name="access">Access level of accessor.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsProperty Set(Access access = Access.Implicit)
        {
            this.set = new CsDeclaration(access, Novelty.Normal, null, "set");
            return this;
        }

        /// <summary>
        /// Add an implicit init accessor to the property.
        /// </summary>
        /// <param name="access">Access level of accessor.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsProperty Init(Access access = Access.Implicit)
        {
            this.init = new CsDeclaration(access, Novelty.Normal, null, "init");
            return this;
        }

        /// <summary>
        /// Add a default value to the property.
        /// </summary>
        /// <param name="defaultVal">Default value for the property.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsProperty Default(string defaultVal)
        {
            this.defaultVal = defaultVal;
            return this;
        }

        /// <summary>
        /// Adds a <see cref="CsPropertyBody"/> to the property.
        /// </summary>
        /// <returns>The <see cref="CsPropertyBody"/> object added.</returns>
        public CsPropertyBody Body()
        {
            this.body = new CsPropertyBody();
            return this.body;
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            this.WriteSummary(codeWriter);

            if (this.valueDesc != null)
            {
                codeWriter.WriteLine($"/// <value>{this.valueDesc}</value>");
            }

            this.WriteRemarks(codeWriter);

            this.WriteDisables(codeWriter);
            this.WriteAttributes(codeWriter);

            if (this.body == null)
            {
                string getText = this.get != null ? $" {this.get.DecoratedName};" : string.Empty;
                string setText = this.set != null ? $" {this.set.DecoratedName};" : string.Empty;
                string initText = this.init != null ? $" {this.init.DecoratedName};" : string.Empty;
                string defaultValText = this.defaultVal != null ? $" = {this.defaultVal};" : string.Empty;
                codeWriter.WriteLine($"{this.DecoratedName} {{{getText}{setText}{initText} }}{defaultValText}");
                codeWriter.Break();
            }
            else
            {
                codeWriter.WriteLine(this.DecoratedName);
                this.body.GenerateCode(codeWriter);
            }

            this.WriteRestores(codeWriter);
        }
    }
}
