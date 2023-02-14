namespace DTDLParser
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts a class and property extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class ClassPropertyPair
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassPropertyPair"/> class.
        /// </summary>
        /// <param name="classPropertyPairObj">A <c>JObject</c> from the metamodel digest containing information about a class and property.</param>
        public ClassPropertyPair(JObject classPropertyPairObj)
        {
            this.ClassName = ((JValue)classPropertyPairObj["class"]).Value<string>();
            this.PropertyName = ((JValue)classPropertyPairObj["property"]).Value<string>();
        }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName { get; }
    }
}
