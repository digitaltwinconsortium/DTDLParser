namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Generator for C# method.
    /// </summary>
    public class CsMethod : CsDeclaration
    {
        private List<TypeParameter> typeParameters;
        private List<Parameter> parameters;
        private string returnDesc;
        private List<string> preambleTexts;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsMethod"/> class.
        /// </summary>
        /// <param name="hasBody">True if the method has a body, false for a declaration in aan interface.</param>
        /// <param name="access">Access level of method.</param>
        /// <param name="novelty">None, Abstract, Virtual, Override, or New.</param>
        /// <param name="type">Type of method.</param>
        /// <param name="name">Name of method.</param>
        /// <param name="multiplicity">Static or Instance.</param>
        /// <param name="asynchrony">Aync or Sync.</param>
        /// <param name="completeness">Partial or Full.</param>
        public CsMethod(bool hasBody, Access access, Novelty novelty, string type, string name, Multiplicity multiplicity = Multiplicity.Instance, Asynchrony asynchrony = Asynchrony.Sync, Completeness completeness = Completeness.Full)
            : base(access, novelty, type, name, multiplicity: multiplicity, asynchrony: asynchrony, completeness: completeness)
        {
            this.typeParameters = new List<TypeParameter>();
            this.parameters = new List<Parameter>();
            this.returnDesc = null;
            this.preambleTexts = new List<string>();

            this.Body = hasBody ? new CsScope(null) : null;
        }

        /// <summary>
        /// Gets a <see cref="CsScope"/> that is the body of the method.
        /// </summary>
        public CsScope Body { get; }

        /// <summary>
        /// Gets an enumeration of method parameter names.
        /// </summary>
        public IEnumerable<string> ParamNames { get => this.parameters.Select(p => p.Name); }

        /// <summary>
        /// Add any lines that precede the method body.
        /// </summary>
        /// <param name="text">A line of code text.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsMethod Preamble(string text)
        {
            this.preambleTexts.Add(text);
            return this;
        }

        /// <summary>
        /// Add a type parameter to the method.
        /// </summary>
        /// <param name="name">Name of type parameter.</param>
        /// <param name="description">Optional text description of type parameter.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsMethod TypeParam(string name, string description = null)
        {
            if (name[0] != 'T')
            {
                throw new StyleException($"Type parameter name '{name}' of method '{this.Name}' must begin with a 'T' -- SA1314.");
            }

            if (description != null && !description.EndsWith("."))
            {
                throw new StyleException($"Documentation text of method '{this.Name}' must end with a period -- SA1629.");
            }

            this.typeParameters.Add(new TypeParameter() { Name = name, Description = description });
            return this;
        }

        /// <summary>
        /// Add a parameter to the method.
        /// </summary>
        /// <param name="type">Type of parameter.</param>
        /// <param name="name">Name of parameter.</param>
        /// <param name="description">Optional text description of parameter.</param>
        /// <param name="defaultVal">Optional default value.</param>
        /// <param name="passage">Means for passing parameter.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsMethod Param(string type, string name, string description = null, string defaultVal = null, Passage passage = Passage.In)
        {
            if (description != null && !description.EndsWith("."))
            {
                throw new StyleException($"Documentation text of method '{this.Name}' must end with a period -- SA1629.");
            }

            this.parameters.Add(new Parameter() { Type = type, Name = name, Description = description, DefaultVal = defaultVal, Passage = passage });
            return this;
        }

        /// <summary>
        /// Describe the return value of the method.
        /// </summary>
        /// <param name="returnDesc">Description of the return vaule.</param>
        /// <returns>A reference to this object, for chaining method invocations.</returns>
        public CsMethod Returns(string returnDesc)
        {
            if (this.Type == "void")
            {
                throw new StyleException($"Void return value of method '{this.Name}' must not be documented -- SA1617.");
            }

            if (!returnDesc.EndsWith("."))
            {
                throw new StyleException($"Documentation text of method '{this.Name}' must end with a period -- SA1629.");
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
            string terminator = this.Body == null ? ";" : string.Empty;
            codeWriter.WriteLine($"{this.DecoratedName}{typeParams}({paramList}){terminator}");

            if (this.preambleTexts.Any())
            {
                codeWriter.IncreaseIndent();

                foreach (string text in this.preambleTexts)
                {
                    codeWriter.WriteLine(text);
                }

                codeWriter.DecreaseIndent();
            }

            if (this.Body != null)
            {
                this.Body.GenerateCode(codeWriter);
            }
            else
            {
                codeWriter.Break();
            }

            this.WriteRestores(codeWriter);
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
