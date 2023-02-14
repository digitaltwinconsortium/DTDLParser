namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Code generator for instances of standard supplemental base types, which are the extensible material classes.
    /// </summary>
    public class SupplementalType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalType"/> class.
        /// </summary>
        /// <param name="typeUri">The URI of the supplemental base type.</param>
        public SupplementalType(string typeUri)
        {
            this.TypeUri = typeUri;
            this.TypeVariableName = GetTypeVariableName(typeUri);
        }

        /// <summary>
        /// Gets or sets the URI of the supplemental base type.
        /// </summary>
        public string TypeUri { get; set; }

        /// <summary>
        /// Gets or sets the varible name for the supplemental type identifier value.
        /// </summary>
        public string TypeVariableName { get; set; }

        /// <summary>
        /// Define a variable for the supplemental type identifier value.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        public void DefineIdVariable(CsScope scope)
        {
            scope.Line($"{ParserGeneratorValues.IdentifierType} {this.TypeVariableName} = new {ParserGeneratorValues.IdentifierType}(\"{this.TypeUri}\");");
        }

        /// <summary>
        /// Define a variable for the supplemental type information object.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        /// <param name="contextIdVariables">A <c>Dictionary</c> mapping context IDs to variables that hold the context ID values.</param>
        public virtual void DefineInfoVariable(CsScope scope, Dictionary<string, string> contextIdVariables)
        {
        }

        /// <summary>
        /// Assign the value of the information variable to a dictionary value whose key is the identifier value.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        /// <param name="dictionaryVariableName">Name of the dictionary variable to which to assign the info variable.</param>
        public virtual void AssignInfoVariable(CsScope scope, string dictionaryVariableName)
        {
        }

        /// <summary>
        /// Get a variable name for the supplemental type identifier value.
        /// </summary>
        /// <param name="typeUri">The URI of the supplemental type.</param>
        /// <returns>A string representation of the variable name.</returns>
        protected static string GetTypeVariableName(string typeUri)
        {
            return typeUri != null ? $"{GetVariableRoot(typeUri)}TypeId{GetVariableSuffix(typeUri)}" : "null";
        }

        /// <summary>
        /// Get a variable name for the supplemental type information object.
        /// </summary>
        /// <param name="typeUri">The URI of the supplemental type.</param>
        /// <returns>A string representation of the variable name.</returns>
        protected static string GetInfoVariableName(string typeUri)
        {
            return typeUri != null ? $"{GetVariableRoot(typeUri)}Info{GetVariableSuffix(typeUri)}" : "null";
        }

        private static string GetVariableRoot(string typeUri)
        {
            int lastLabelStart = typeUri.LastIndexOf(':') + 1;
            int versionStart = typeUri.IndexOf(';');
            int lastLabelLength = (versionStart > 0 ? versionStart : typeUri.Length) - lastLabelStart;
            string lastLabel = typeUri.Substring(lastLabelStart, lastLabelLength);
            return char.ToLowerInvariant(lastLabel[0]) + lastLabel.Substring(1);
        }

        private static string GetVariableSuffix(string typeUri)
        {
            int versionSuffixStart = typeUri.IndexOf(';') + 1;
            if (versionSuffixStart > 0)
            {
                return $"V{typeUri.Substring(versionSuffixStart).Replace('.', '_')}";
            }
            else
            {
                return $"V{Regex.Match(typeUri, @":[Vv](\d+):").Groups[1].Captures[0].Value}";
            }
        }
    }
}
