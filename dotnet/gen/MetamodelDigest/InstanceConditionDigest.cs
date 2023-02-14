namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Datatype that abstracts instance validation conditions extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class InstanceConditionDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceConditionDigest"/> class.
        /// </summary>
        /// <param name="instanceConditionObj">A <c>JObject</c> from the metamodel digest containing conditions that must apply to an instance of the model element.</param>
        public InstanceConditionDigest(JObject instanceConditionObj)
        {
            this.JsonType = instanceConditionObj.TryGetValue("jsonType", out JToken jsonType) ? ((JValue)jsonType).Value<string>() : null;

            this.Datatype = instanceConditionObj.TryGetValue("datatype", out JToken datatype) ? ((JValue)datatype).Value<string>() : null;

            this.InstanceProperty = instanceConditionObj.TryGetValue("instanceProperty", out JToken instanceProperty) ? ((JArray)instanceProperty).Select(v => ((JValue)v).Value<string>()).ToList() : null;

            this.Pattern = instanceConditionObj.TryGetValue("pattern", out JToken pattern) ? ((JValue)pattern).Value<string>() : null;

            this.HasValue = instanceConditionObj.TryGetValue("hasValue", out JToken hasValue) ? ((JValue)hasValue).Value<string>() : null;

            this.NamePattern = instanceConditionObj.TryGetValue("namePattern", out JToken namePattern) ? ((JValue)namePattern).Value<string>() : null;

            this.NameHasValue = instanceConditionObj.TryGetValue("nameHasValue", out JToken nameHasValue) ? ((JValue)nameHasValue).Value<string>() : null;
        }

        /// <summary>
        /// Gets a string indicating the type of JSON element for the instance, or null if no element type constraint.
        /// </summary>
        public string JsonType { get; }

        /// <summary>
        /// Gets a string indicating the datatype of the instance, or null if no datatype constraint.
        /// </summary>
        public string Datatype { get; }

        /// <summary>
        /// Gets a list of property names whose values' validation criteria must be satisfied by the instance, or null if no such constraint.
        /// </summary>
        public List<string> InstanceProperty { get; }

        /// <summary>
        /// Gets a regex that constrains string instances, or null if no pattern constraint.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Gets the name of a literal property whose value must match the instance value, or null if no such constraint.
        /// </summary>
        public string HasValue { get; }

        /// <summary>
        /// Gets a regex that constrains the JSON property name, or null if not a property or no such constraint.
        /// </summary>
        public string NamePattern { get; }

        /// <summary>
        /// Gets the name of a literal property whose value must match the JSON property name, or null if not a property or no such constraint.
        /// </summary>
        public string NameHasValue { get; }
    }
}
