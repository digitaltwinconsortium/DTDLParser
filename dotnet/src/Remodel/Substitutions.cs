namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that provides IRI/term substitutions for contexts, types, and properties.
    /// </summary>
    public class Substitutions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Substitutions"/> class.
        /// </summary>
        /// <param name="subsFile">A <c>FileInfo</c> object representing a JSON file containing IRI/term substitutions for contexts, types, and properties.</param>
        public Substitutions(FileInfo subsFile)
        {
            Console.WriteLine("Reading substitutions file");

            if (!subsFile.Exists)
            {
                Console.WriteLine($"Substitutions file {subsFile.FullName} not found");
                return;
            }

            FileStream readStream = subsFile.OpenRead();
            StreamReader streamReader = new StreamReader(readStream);
            string jsonText = streamReader.ReadToEnd();

            JToken subsToken = JToken.Parse(jsonText);

            if (subsToken.Type != JTokenType.Object)
            {
                Console.WriteLine("Substitutions file text is not a JSON object");
                return;
            }

            JObject subsObj = (JObject)subsToken;

            this.ContextAddition = this.GetString("contextAddition", subsObj);
            this.ContextSubstitutions = this.GetDictionary("contextSubstitutions", subsObj);
            this.TypeSubstitutions = this.GetDictionary("typeSubstitutions", subsObj);
            this.PropertySubstitutions = this.GetDictionary("propertySubstitutions", subsObj);
        }

        /// <summary>
        /// Gets a string indicating a context specifier that should be added when needed to permit apocryphal types and properties in a model component.
        /// </summary>
        public string ContextAddition { get; }

        /// <summary>
        /// Gets a dictionary that maps context specifiers in the source model to substitute context specifiers for the converted model.
        /// </summary>
        public Dictionary<string, string> ContextSubstitutions { get; }

        /// <summary>
        /// Gets a dictionary that maps type names in the source model to substitute type names for the converted model.
        /// </summary>
        public Dictionary<string, string> TypeSubstitutions { get; }

        /// <summary>
        /// Gets a dictionary that maps property names in the source model to substitute property names for the converted model.
        /// </summary>
        public Dictionary<string, string> PropertySubstitutions { get; }

        /// <summary>
        /// Try to confirm that the configured substitution values are valid.
        /// </summary>
        /// <returns>True if all configured subsitutions are valid.</returns>
        public bool TryValidate()
        {
            Console.WriteLine("Validating substitutions");

            return
                this.ContextAddition != null && this.TryValidateDtmi(this.ContextAddition, requireVersion: true) &&
                this.ContextSubstitutions != null && this.ContextSubstitutions.Values.All(c => this.TryValidateDtmi(c, requireVersion: true)) &&
                this.TypeSubstitutions != null && this.TypeSubstitutions.Values.All(t => this.TryValidateDtmi(t, allowTerm: true)) &&
                this.PropertySubstitutions != null && this.PropertySubstitutions.Values.All(p => this.TryValidateDtmi(p, allowTerm: true));
        }

        private bool TryValidateDtmi(string dtmiString, bool requireVersion = true, bool allowTerm = false)
        {
            if (dtmiString == null)
            {
                return true;
            }

            if (!Dtmi.TryCreateDtmi(dtmiString, out Dtmi dtmi) && !(allowTerm && !dtmiString.StartsWith('@') && !dtmiString.Contains(':')))
            {
                Console.WriteLine($"value \"{dtmiString}\" is not a valid DTMI" + (allowTerm ? " or term" : string.Empty));
                return false;
            }

            if (requireVersion && (dtmi?.MajorVersion ?? 1) == 0)
            {
                Console.WriteLine($"DTMI \"{dtmiString}\" does not have a version suffix");
                return false;
            }

            return true;
        }

        private string GetString(string memberName, JObject subsObj)
        {
            if (!subsObj.TryGetValue(memberName, out JToken token))
            {
                Console.WriteLine($"Substitutions file contains no \"{memberName}\" member");
                return null;
            }

            if (token.Type != JTokenType.String)
            {
                Console.WriteLine($"Substitutions file \"{memberName}\" member does not have a string value");
                return null;
            }

            return ((JValue)token).Value<string>();
        }

        private Dictionary<string, string> GetDictionary(string memberName, JObject subsObj)
        {
            if (!subsObj.TryGetValue(memberName, out JToken token))
            {
                Console.WriteLine($"Substitutions file contains no \"{memberName}\" member");
                return null;
            }

            if (token.Type != JTokenType.Object)
            {
                Console.WriteLine($"Substitutions file \"{memberName}\" member does not have a JSON object value");
                return null;
            }

            return ((JObject)token).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>());
        }
    }
}
