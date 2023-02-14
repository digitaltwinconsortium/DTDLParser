namespace DTDLParser
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Generator for a C# interface.
    /// </summary>
    public class CsInterface : CsDeclaration, ICsFile
    {
        private string exports;
        private List<CsProperty> properties;
        private List<CsMethod> methods;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsInterface"/> class.
        /// </summary>
        /// <param name="access">Access level of interface.</param>
        /// <param name="typeName">The name of the interface being declared.</param>
        /// <param name="exports">Interfaces exported by this interface.</param>
        /// <param name="subNamespace">A sub-namespace in which to declare the enum.</param>
        public CsInterface(Access access, string typeName, string exports = null, string subNamespace = null)
            : base(access, Novelty.Normal, "interface", typeName)
        {
            if (typeName[0] != 'I')
            {
                throw new Exception($"Interface name '{typeName}' must begin with an 'I' -- SA1302.");
            }

            this.exports = exports;
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
        /// Add a C# property to the interface.
        /// </summary>
        /// <param name="access">Access level of property.</param>
        /// <param name="type">Type of property.</param>
        /// <param name="name">Name of property.</param>
        /// <returns>The <see cref="CsProperty"/> object added.</returns>
        public CsProperty Property(Access access, string type, string name)
        {
            CsProperty csProperty = new CsProperty(access, Novelty.Normal, type, name);
            this.properties.Add(csProperty);
            return csProperty;
        }

        /// <summary>
        /// Add a C# method to the interface.
        /// </summary>
        /// <param name="access">Access level of method.</param>
        /// <param name="type">Type of method.</param>
        /// <param name="name">Name of method.</param>
        /// <returns>The <see cref="CsMethod"/> object added.</returns>
        public CsMethod Method(Access access, string type, string name)
        {
            CsMethod csMethod = new CsMethod(hasBody: false, access, Novelty.Normal, type, name);
            this.methods.Add(csMethod);
            return csMethod;
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
