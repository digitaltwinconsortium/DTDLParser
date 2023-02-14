namespace DTDLParser
{
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that provides object-model class and property information based on language-specific conventions.
    /// </summary>
    public class ObjectModelCustomizer
    {
        private readonly Dictionary<string, string> classNamePrefixes;
        private readonly Dictionary<string, string> classNameSuffixes;
        private readonly Dictionary<string, bool> capitalizeClassName;

        private readonly Dictionary<string, string> propertyNamePrefixes;
        private readonly Dictionary<string, string> propertyNameSuffixes;
        private readonly Dictionary<string, bool> capitalizePropertyName;

        private readonly Dictionary<string, JObject> propertyTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectModelCustomizer"/> class.
        /// </summary>
        /// <param name="conventionsText">JSON text of the object-model conventions specification.</param>
        public ObjectModelCustomizer(string conventionsText)
        {
            this.ProgrammingLanguages = new Dictionary<string, string>();

            JObject conventions = (JObject)JToken.Parse(conventionsText);

            this.classNamePrefixes = new Dictionary<string, string>();
            this.classNameSuffixes = new Dictionary<string, string>();
            this.capitalizeClassName = new Dictionary<string, bool>();

            this.propertyNamePrefixes = new Dictionary<string, string>();
            this.propertyNameSuffixes = new Dictionary<string, string>();
            this.capitalizePropertyName = new Dictionary<string, bool>();

            this.propertyTypes = new Dictionary<string, JObject>();

            foreach (KeyValuePair<string, JToken> progLangPair in conventions)
            {
                JObject progLangObj = (JObject)progLangPair.Value;

                this.ProgrammingLanguages[progLangPair.Key] = ((JValue)progLangObj["language"]).Value<string>();

                JObject classNameObj = (JObject)progLangObj["className"];
                this.classNamePrefixes[progLangPair.Key] = ((JValue)classNameObj["prefix"]).Value<string>();
                this.classNameSuffixes[progLangPair.Key] = ((JValue)classNameObj["suffix"]).Value<string>();
                this.capitalizeClassName[progLangPair.Key] = ((JValue)classNameObj["capitalize"]).Value<bool>();

                JObject propertyNameObj = (JObject)progLangObj["propertyName"];
                this.propertyNamePrefixes[progLangPair.Key] = ((JValue)propertyNameObj["prefix"]).Value<string>();
                this.propertyNameSuffixes[progLangPair.Key] = ((JValue)propertyNameObj["suffix"]).Value<string>();
                this.capitalizePropertyName[progLangPair.Key] = ((JValue)propertyNameObj["capitalize"]).Value<bool>();

                this.propertyTypes[progLangPair.Key] = (JObject)progLangObj["propertyType"];
            }
        }

        /// <summary>
        /// Gets a dictionary that maps from programming language code to the name of the language.
        /// </summary>
        /// <value>Dictionary mapping programming language code to programming language name.</value>
        public Dictionary<string, string> ProgrammingLanguages { get; }

        /// <summary>
        /// Get the programming-language-specific class name for a DTDL type.
        /// </summary>
        /// <param name="progLang">The abbreviation for the programming language.</param>
        /// <param name="typeName">The DTDL type name.</param>
        /// <returns>A class name in the indicated programming language.</returns>
        public string GetClassName(string progLang, string typeName)
        {
            string prefix = this.classNamePrefixes[progLang];
            string suffix = this.classNameSuffixes[progLang];
            bool capitalize = this.capitalizeClassName[progLang];

            char firstChar = capitalize ? char.ToUpperInvariant(typeName[0]) : char.ToLowerInvariant(typeName[0]);

            return prefix + firstChar + typeName.Substring(1) + suffix;
        }

        /// <summary>
        /// Get the programming-language-specific property name for a DTDL property.
        /// </summary>
        /// <param name="progLang">The abbreviation for the programming language.</param>
        /// <param name="propertyName">The DTDL property name.</param>
        /// <returns>A property name in the indicated programming language.</returns>
        public string GetPropertyName(string progLang, string propertyName)
        {
            string prefix = this.propertyNamePrefixes[progLang];
            string suffix = this.propertyNameSuffixes[progLang];
            bool capitalize = this.capitalizePropertyName[progLang];

            char firstChar = capitalize ? char.ToUpperInvariant(propertyName[0]) : char.ToLowerInvariant(propertyName[0]);

            return prefix + firstChar + propertyName.Substring(1) + suffix;
        }

        /// <summary>
        /// Get the programming-language-specific property type for a DTDL property.
        /// </summary>
        /// <param name="progLang">The abbreviation for the programming language.</param>
        /// <param name="propertyDigest">A <see cref="MaterialPropertyDigest"/> object containing material property information extracted from the metamodel.</param>
        /// <returns>A property type in the indicated programming language.</returns>
        public string GetPropertyType(string progLang, MaterialPropertyDigest propertyDigest)
        {
            JObject propertyTypeObj = this.propertyTypes[progLang];

            if (propertyDigest.IsLiteral)
            {
                JObject literalObj = (JObject)propertyTypeObj["literal"];
                return this.GetSpecificType((JObject)literalObj[propertyDigest.Datatype ?? "untyped"], propertyDigest);
            }
            else if (propertyDigest.Class != null)
            {
                return this.GetSpecificType((JObject)propertyTypeObj["object"], propertyDigest).Replace("$", this.GetClassName(progLang, propertyDigest.Class));
            }
            else
            {
                return this.GetSpecificType((JObject)propertyTypeObj["identifier"], propertyDigest);
            }
        }

        private string GetSpecificType(JObject obj, MaterialPropertyDigest propertyDigest)
        {
            JObject optionalOrRequiredObj = (JObject)obj[propertyDigest.IsOptional ? "optional" : "required"];
            return ((JValue)optionalOrRequiredObj[propertyDigest.DictionaryKey != null ? "dictionary" : (propertyDigest.IsPlural ? "plural" : "singular")]).Value<string>();
        }
    }
}
