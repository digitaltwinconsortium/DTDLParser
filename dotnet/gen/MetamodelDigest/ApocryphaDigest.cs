namespace DTDLParser
{
    using System;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts apocrypha information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class ApocryphaDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApocryphaDigest"/> class.
        /// </summary>
        /// <param name="apocryphonObj">A <c>JObject</c> from the metamodel digest containing information about apocrypha for a given version of DTDL.</param>
        public ApocryphaDigest(JObject apocryphonObj)
        {
            this.UndefinedTermAsCotype = ToleranceStringToEnum(((JValue)apocryphonObj["undefinedTermAsCotype"]).Value<string>());

            this.IrrelevantDtmiOrTermAsCotype = ToleranceStringToEnum(((JValue)apocryphonObj["irrelevantDtmiOrTermAsCotype"]).Value<string>());

            this.NotDtmiNorTermAsCotype = ToleranceStringToEnum(((JValue)apocryphonObj["notDtmiNorTermAsCotype"]).Value<string>());

            this.InvalidDtmiAsCotype = ToleranceStringToEnum(((JValue)apocryphonObj["invalidDtmiAsCotype"]).Value<string>());

            this.UndefinedTermAsProperty = ToleranceStringToEnum(((JValue)apocryphonObj["undefinedTermAsProperty"]).Value<string>());

            this.IrrelevantDtmiOrTermAsProperty = ToleranceStringToEnum(((JValue)apocryphonObj["irrelevantDtmiOrTermAsProperty"]).Value<string>());

            this.NotDtmiNorTermAsProperty = ToleranceStringToEnum(((JValue)apocryphonObj["notDtmiNorTermAsProperty"]).Value<string>());

            this.InvalidDtmiAsProperty = ToleranceStringToEnum(((JValue)apocryphonObj["invalidDtmiAsProperty"]).Value<string>());
        }

        /// <summary>
        /// Gets the condition under which an undefined term may be used as a cotype.
        /// </summary>
        public ApocryphaTolerance UndefinedTermAsCotype { get; }

        /// <summary>
        /// Gets the condition under which an irrelevant DTMI or term may be used as a cotype.
        /// </summary>
        public ApocryphaTolerance IrrelevantDtmiOrTermAsCotype { get; }

        /// <summary>
        /// Gets the condition under which a non-DTMI IRI may be used as a cotype.
        /// </summary>
        public ApocryphaTolerance NotDtmiNorTermAsCotype { get; }

        /// <summary>
        /// Gets the condition under which an invalid DTMI may be used as a cotype.
        /// </summary>
        public ApocryphaTolerance InvalidDtmiAsCotype { get; }

        /// <summary>
        /// Gets the condition under which an undefined term may be used as a property.
        /// </summary>
        public ApocryphaTolerance UndefinedTermAsProperty { get; }

        /// <summary>
        /// Gets the condition under which an irrelevant DTMI or term may be used as a property.
        /// </summary>
        public ApocryphaTolerance IrrelevantDtmiOrTermAsProperty { get; }

        /// <summary>
        /// Gets the condition under which a non-DTMI IRI may be used as a property.
        /// </summary>
        public ApocryphaTolerance NotDtmiNorTermAsProperty { get; }

        /// <summary>
        /// Gets the condition under which an invalid DTMI may be used as a property.
        /// </summary>
        public ApocryphaTolerance InvalidDtmiAsProperty { get; }

        private static ApocryphaTolerance ToleranceStringToEnum(string toleranceString)
        {
            switch (toleranceString)
            {
                case "valid":
                    return ApocryphaTolerance.Valid;
                case "invalid":
                    return ApocryphaTolerance.Invalid;
                case "tenable":
                    return ApocryphaTolerance.Tenable;
                default:
                    throw new Exception($"string \"{toleranceString}\" is not a valid tolerance string.");
            }
        }
    }
}
