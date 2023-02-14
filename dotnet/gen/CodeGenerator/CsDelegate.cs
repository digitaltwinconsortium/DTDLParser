namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Generator for C# delegate.
    /// </summary>
    public class CsDelegate : CsDeclaration, ICsFile
    {
        private List<TypeParameter> typeParameters;
        private List<Parameter> parameters;
        private string returnDesc;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsDelegate"/> class.
        /// </summary>
        /// <param name="access">Access level of delegate.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of delegate.</param>
        /// <param name="name">Name of delegate.</param>
        /// <param name="subNamespace">A sub-namespace in which to declare the delefate.</param>
        public CsDelegate(Access access, Novelty novelty, string type, string name, string subNamespace = null)
            : base(access, novelty, $"delegate {type}", name)
        {
            this.typeParameters = new List<TypeParameter>();
            this.parameters = new List<Parameter>();
            this.returnDesc = null;

            this.TypeName = name;
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
        /// Add a type parameter to the delegate.
        /// </summary>
        /// <param name="name">Name of type parameter.</param>
        /// <param name="description">Optional text description of type parameter.</param>
        /// <returns>A reference to this object, for chaining delegate invocations.</returns>
        public CsDelegate TypeParam(string name, string description = null)
        {
            if (name[0] != 'T')
            {
                throw new StyleException($"Type parameter name '{name}' of delegate '{this.Name}' must begin with a 'T' -- SA1314.");
            }

            if (description != null && !description.EndsWith("."))
            {
                throw new StyleException($"Documentation text of delegate '{this.Name}' must end with a period -- SA1629.");
            }

            this.typeParameters.Add(new TypeParameter() { Name = name, Description = description });
            return this;
        }

        /// <summary>
        /// Add a parameter to the delegate.
        /// </summary>
        /// <param name="type">Type of parameter.</param>
        /// <param name="name">Name of parameter.</param>
        /// <param name="description">Optional text description of parameter.</param>
        /// <param name="defaultVal">Optional default value.</param>
        /// <param name="passage">Means for passing parameter.</param>
        /// <returns>A reference to this object, for chaining delegate invocations.</returns>
        public CsDelegate Param(string type, string name, string description = null, string defaultVal = null, Passage passage = Passage.In)
        {
            if (description != null && !description.EndsWith("."))
            {
                throw new StyleException($"Documentation text of delegate '{this.Name}' must end with a period -- SA1629.");
            }

            this.parameters.Add(new Parameter() { Type = type, Name = name, Description = description, DefaultVal = defaultVal, Passage = passage });
            return this;
        }

        /// <summary>
        /// Describe the return value of the delegate.
        /// </summary>
        /// <param name="returnDesc">Description of the return vaule.</param>
        /// <returns>A reference to this object, for chaining delegate invocations.</returns>
        public CsDelegate Returns(string returnDesc)
        {
            if (this.Type == "void")
            {
                throw new StyleException($"Void return value of delegate '{this.Name}' must not be documented -- SA1617.");
            }

            if (!returnDesc.EndsWith("."))
            {
                throw new StyleException($"Documentation text of delegate '{this.Name}' must end with a period -- SA1629.");
            }

            this.returnDesc = returnDesc;
            return this;
        }

        /// <inheritdoc/>
        public override void GenerateCode(CodeWriter codeWriter)
        {
            this.WriteSummary(codeWriter);

            foreach (TypeParameter typeParam in this.typeParameters)
            {
                if (typeParam.Description != null)
                {
                    codeWriter.WriteLine($"/// <typeparam name=\"{typeParam.Name}\">{typeParam.Description}</typeparam>");
                }
            }

            foreach (Parameter param in this.parameters)
            {
                if (param.Description != null)
                {
                    codeWriter.WriteLine($"/// <param name=\"{param.Name}\">{param.Description}</param>");
                }
            }

            if (this.returnDesc != null)
            {
                codeWriter.WriteLine($"/// <returns>{this.returnDesc}</returns>");
            }

            this.WriteRemarks(codeWriter);

            this.WriteDisables(codeWriter);
            this.WriteAttributes(codeWriter);

            string typeParams = this.typeParameters.Any() ? $"<{string.Join(", ", this.typeParameters.Select(p => p.Name))}>" : string.Empty;
            string paramList = string.Join(", ", this.parameters.Select(p => $"{GetPassageString(p.Passage)}{p.Type} {p.Name}{GetDefaultString(p.DefaultVal)}"));
            codeWriter.WriteLine($"{this.DecoratedName}{typeParams}({paramList});");
            this.WriteRestores(codeWriter);
            codeWriter.Break();
        }

        private static string GetPassageString(Passage passage)
        {
            switch (passage)
            {
                case Passage.In:
                    return string.Empty;
                case Passage.Out:
                    return "out ";
                case Passage.Reference:
                    return "ref ";
                default:
                    return null;
            }
        }

        private static string GetDefaultString(string defaultVal)
        {
            return defaultVal != null ? $" = {defaultVal}" : string.Empty;
        }

        private struct TypeParameter
        {
            public string Name;
            public string Description;
        }

        private struct Parameter
        {
            public string Type;
            public string Name;
            public string Description;
            public string DefaultVal;
            public Passage Passage;
        }
    }
}
