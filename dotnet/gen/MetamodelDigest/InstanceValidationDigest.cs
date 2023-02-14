namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts material property information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class InstanceValidationDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceValidationDigest"/> class.
        /// </summary>
        public InstanceValidationDigest()
        {
            this.CriteriaText = null;
            this.ElementConditions = new Dictionary<int, InstanceConditionDigest>();
            this.ChildConditions = new Dictionary<int, InstanceConditionDigest>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceValidationDigest"/> class.
        /// </summary>
        /// <param name="instanceValidationObj">A <c>JObject</c> from the metamodel digest containing information about validating instances of the model element.</param>
        public InstanceValidationDigest(JObject instanceValidationObj)
        {
            this.TypeText = instanceValidationObj.TryGetValue("typeText", out JToken typeText) ? ((JValue)typeText).Value<string>() : null;

            this.ConformanceText = instanceValidationObj.TryGetValue("conformanceText", out JToken conformanceText) ? ((JValue)conformanceText).Value<string>() : null;

            this.CriteriaText = ((JValue)instanceValidationObj["criteriaText"]).Value<string>();

            this.ElementConditions = instanceValidationObj.Properties().Where(p1 => int.TryParse(p1.Name, out int _)).ToDictionary(p2 => int.Parse(p2.Name), p2 => new InstanceConditionDigest((JObject)((JObject)p2.Value)["element"]));

            this.ChildConditions = instanceValidationObj.Properties().Where(p1 => int.TryParse(p1.Name, out int _)).ToDictionary(p2 => int.Parse(p2.Name), p2 => new InstanceConditionDigest((JObject)((JObject)p2.Value)["eachChild"]));
        }

        /// <summary>
        /// Gets a textual description of the type criteria for an instance of the element.
        /// </summary>
        public string TypeText { get; }

        /// <summary>
        /// Gets a textual description of the conformance criteria for an instance of the element.
        /// </summary>
        public string ConformanceText { get; }

        /// <summary>
        /// Gets a textual description of the validation criteria for an instance of the element.
        /// </summary>
        public string CriteriaText { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a <see cref="InstanceConditionDigest"/> object providing version-specific validation conditions on the element.
        /// </summary>
        public Dictionary<int, InstanceConditionDigest> ElementConditions { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a <see cref="InstanceConditionDigest"/> object providing version-specific validation conditions on each child of the element.
        /// </summary>
        public Dictionary<int, InstanceConditionDigest> ChildConditions { get; }
    }
}
