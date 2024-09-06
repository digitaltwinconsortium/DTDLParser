namespace DTDLParser
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts limit context information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class LimitContextDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LimitContextDigest"/> class.
        /// </summary>
        public LimitContextDigest()
        {
            this.DtdlVersion = 0;
            this.LimitSpec = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LimitContextDigest"/> class.
        /// </summary>
        /// <param name="limitContextObj">A <c>JObject</c> from the metamodel digest containing information about limits specified by a given extension context.</param>
        public LimitContextDigest(JObject limitContextObj)
        {
            this.DtdlVersion = ((JValue)limitContextObj["dtdlVersion"]).Value<int>();

            this.LimitSpec = ((JValue)limitContextObj["spec"]).Value<string>();
        }

        /// <summary>
        /// Gets the version of DTDL for which this extension defines limits.
        /// </summary>
        public int DtdlVersion { get; }

        /// <summary>
        /// Gets a string to use for referencing the limits defined by this extension.
        /// </summary>
        public string LimitSpec { get; }
    }
}
