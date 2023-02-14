namespace DTDLParser
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a property on supplemental type.
    /// </summary>
    public class SupplementalProperty
    {
        private string propertyName;
        private string typeUri;
        private string maxCount;
        private string minCount;
        private string regex;
        private string isPlural;
        private string isOptional;
        private string defaultLanguage;
        private string dtmiSeg;
        private string dictionaryKey;
        private string idRequired;
        private string typeRequired;
        private string childOf;
        private string instanceProperty;
        private string requiredValues;
        private string requiredValuesString;
        private string requiredLiteral;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalProperty"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the supplemental property.</param>
        /// <param name="supplementalPropertyDigest">A <see cref="SupplementalPropertyDigest"/> that provides supplemental property information extracted from the metamodel.</param>
        public SupplementalProperty(string propertyName, SupplementalPropertyDigest supplementalPropertyDigest)
        {
            this.propertyName = propertyName;
            this.typeUri = supplementalPropertyDigest.TypeUri;
            this.maxCount = supplementalPropertyDigest.MaxCount != null ? supplementalPropertyDigest.MaxCount.ToString() : "null";
            this.minCount = supplementalPropertyDigest.MinCount != null ? supplementalPropertyDigest.MinCount.ToString() : "null";
            this.regex = $"regex: {(supplementalPropertyDigest.Pattern != null ? $"new Regex(@\"{supplementalPropertyDigest.Pattern.Replace("\"", "\\\"")}\")" : "null")}";
            this.isPlural = $"isPlural: {ParserGeneratorValues.GetBooleanLiteral(supplementalPropertyDigest.IsPlural)}";
            this.isOptional = $"isOptional: {ParserGeneratorValues.GetBooleanLiteral(supplementalPropertyDigest.IsOptional)}";
            this.defaultLanguage = $"defaultLanguage: {(supplementalPropertyDigest.DefaultLanguage != null ? $"\"{supplementalPropertyDigest.DefaultLanguage}\"" : "null")}";
            this.dtmiSeg = $"dtmiSeg: {(supplementalPropertyDigest.DtmiSegment != null ? $"\"{supplementalPropertyDigest.DtmiSegment}\"" : "null")}";
            this.dictionaryKey = $"dictionaryKey: {(supplementalPropertyDigest.DictionaryKey != null ? $"\"{supplementalPropertyDigest.DictionaryKey}\"" : "null")}";
            this.idRequired = $"idRequired: {ParserGeneratorValues.GetBooleanLiteral(supplementalPropertyDigest.IdRequired)}";
            this.typeRequired = $"typeRequired: {ParserGeneratorValues.GetBooleanLiteral(supplementalPropertyDigest.TypeRequired)}";
            this.childOf = $"childOf: {(supplementalPropertyDigest.ChildOf != null ? $"new Dtmi(\"{supplementalPropertyDigest.ChildOf}\")" : "null")}";
            this.instanceProperty = $"instanceProperty: {(supplementalPropertyDigest.InstanceProperty != null ? $"\"{supplementalPropertyDigest.InstanceProperty}\"" : "null")}";
            this.requiredValues = $"requiredValues: {(supplementalPropertyDigest.RequiredValues != null ? $"new List<{ParserGeneratorValues.IdentifierType}>() {{ {string.Join(", ", supplementalPropertyDigest.RequiredValues.Select(s => $"new {ParserGeneratorValues.IdentifierType}(\"{s}\")"))} }}" : "null")}";
            this.requiredValuesString = $"requiredValuesString: {(supplementalPropertyDigest.RequiredValuesString != null ? $"\"{supplementalPropertyDigest.RequiredValuesString}\"" : "null")}";
            this.requiredLiteral = $"requiredLiteral: {(supplementalPropertyDigest.RequiredLiteral != null ? ToJsonLiteral(supplementalPropertyDigest.RequiredLiteral, escapeQuotes: false) : "null")}";
        }

        /// <summary>
        /// Add the property to a supplemental type instance.
        /// </summary>
        /// <param name="scope">A <see cref="CsScope"/> object to which to add generated code.</param>
        /// <param name="infoVariableName">Name of the supplementaly type info variable to which to add the cotype.</param>
        public void AddProperty(CsScope scope, string infoVariableName)
        {
            string typeUriString = this.typeUri != null ? $"new {(this.typeUri.StartsWith("dtmi:") ? "Dtmi" : "Uri")}(\"{this.typeUri}\")" : "null";

            scope.Line($"{infoVariableName}.AddProperty(\"{this.propertyName}\", {typeUriString}, {this.maxCount}, {this.minCount}, {this.regex}, {this.isPlural}, {this.isOptional}, {this.defaultLanguage}, {this.dtmiSeg}, {this.dictionaryKey}, {this.idRequired}, {this.typeRequired}, {this.childOf}, {this.instanceProperty}, {this.requiredValues}, {this.requiredValuesString}, {this.requiredLiteral});");
        }

        private static string ToJsonLiteral(object value, bool escapeQuotes)
        {
            Type valueType = value.GetType();
            if (valueType == typeof(string))
            {
                return escapeQuotes ? $"\\\"{(string)value}\\\"" : $"\"{(string)value}\"";
            }
            else if (valueType == typeof(bool))
            {
                return ParserGeneratorValues.GetBooleanLiteral((bool)value);
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
