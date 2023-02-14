namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class MetamodelDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetamodelDigest"/> class.
        /// </summary>
        /// <param name="digestText">JSON text of the metamodel digest from the metaparser.</param>
        public MetamodelDigest(string digestText)
        {
            JObject digest = (JObject)JToken.Parse(digestText);

            this.DtdlVersions = ((JArray)digest["dtdlVersions"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.Contexts = ((JObject)digest["contexts"]).Properties().ToDictionary(c => c.Name, c => ((JObject)c.Value).Properties().ToDictionary(d => d.Name, d => ((JValue)d.Value).Value<string>()));

            this.BaseClass = ((JValue)digest["baseClass"]).Value<string>();

            this.PartitionClasses = ((JArray)digest["partitionClasses"]).Select(t => ((JValue)t).Value<string>()).ToList();

            this.RootableClasses = ((JObject)digest["rootableClasses"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList());

            this.Aliases = ((JObject)digest["aliases"]).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<string>());

            this.IdentifierDefinitionRestrictions = ((JObject)digest["identifierDefinition"]).Properties().Where(p1 => int.TryParse(p1.Name, out int _)).ToDictionary(p2 => int.Parse(p2.Name), p2 => new StringRestriction((JObject)p2.Value));

            this.IdentifierReferenceRestrictions = ((JObject)digest["identifierReference"]).Properties().ToDictionary(p => int.Parse(p.Name), p => new StringRestriction((JObject)p.Value));

            this.ReservedIdPrefixes = ((JObject)digest["reservedIdPrefixes"]).Properties().ToDictionary(p => p.Name, p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList());

            this.PartitionRestrictions = ((JObject)digest["partitions"]).Properties().ToDictionary(p => int.Parse(p.Name), p => new PartitionRestriction((JObject)p.Value));

            this.DtdlVersionsAllowingLocalTerms = ((JArray)digest["dtdlVersionsAllowingLocalTerms"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.DtdlVersionsAllowingDynamicExtensions = ((JArray)digest["dtdlVersionsAllowingDynamicExtensions"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.DtdlVersionsRestrictingKeywords = ((JArray)digest["dtdlVersionsRestrictingKeywords"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.DtdlVersionsWithApocryphalPropertyCotypeDependency = ((JArray)digest["dtdlVersionsWithApocryphalPropertyCotypeDependency"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.Apocrypha = ((JObject)digest["apocrypha"]).Properties().ToDictionary(p => int.Parse(p.Name), p => new ApocryphaDigest((JObject)p.Value));

            this.IsLayeringSupported = ((JValue)digest["layeringSupported"]).Value<bool>();

            this.ContextsMergeDefinitions = ((JArray)digest["contextsMergeDefinitions"]).Select(t => ((JValue)t).Value<string>()).ToList();

            this.ExtensionDocumentationDigests = ((JObject)digest["extensionDocumentation"]).Properties().ToDictionary(p => p.Name, p => new ExtensionDocumentationDigest((JObject)p.Value));

            this.AffiliateContextsImplicitDtdlVersions = ((JObject)digest["affiliateContextsImplicitDtdlVersions"]).Properties().ToDictionary(p => p.Name, p => ((JValue)p.Value).Value<int>());

            this.ClassIdentifierDefinitionRestrictions = ((JObject)digest["identifierDefinition"]).Properties().Where(p1 => !int.TryParse(p1.Name, out int _)).ToDictionary(p2 => p2.Name, p2 => ((JObject)p2.Value).Properties().ToDictionary(p3 => int.Parse(p3.Name), p3 => new StringRestriction((JObject)p3.Value)));

            this.ExtensionKinds = ((JArray)digest["extensionKinds"]).Select(t => ((JValue)t).Value<string>()).ToList();

            this.ExtensibleMaterialClasses = ((JObject)digest["extensibleMaterialClasses"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList());

            this.MaterialClasses = ((JObject)digest["materialClasses"]).Properties().ToDictionary(p => p.Name, p => new MaterialClassDigest((JObject)p.Value));

            this.DescendantControls = ((JArray)digest["descendantControls"]).Select(t => new DescendantControlDigest((JObject)t)).ToList();

            this.SupplementalTypes = ((JObject)digest["supplementalTypes"]).Properties().ToDictionary(p => p.Name, p => new SupplementalTypeDigest((JObject)p.Value));

            this.QuantitativeTypes = ((JObject)digest["quantitativeTypes"]).Properties().ToDictionary(c => c.Name, c => ((JObject)c.Value).Properties().ToDictionary(d => d.Name, d => ((JValue)d.Value).Value<string>()));

            this.Units = ((JObject)digest["units"]).Properties().ToDictionary(c => c.Name, c => ((JObject)c.Value).Properties().ToDictionary(d => d.Name, d => ((JArray)d.Value).Select(e => ((JValue)e).Value<string>()).ToList()));

            this.Families = ((JObject)digest["families"]).Properties().ToDictionary(p => p.Name, p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList());

            this.ElementsJsonText = digest["elements"].ToString();
        }

        /// <summary>
        /// Gets a list of DTDL (major) version numbers defined in the metamodel digest.
        /// </summary>
        public List<int> DtdlVersions { get; }

        /// <summary>
        /// Gets a dictionary that maps from a context ID to a dictionary of term definitions.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> Contexts { get; }

        /// <summary>
        /// Gets the name of the base class defined in the metamodel digest.
        /// </summary>
        public string BaseClass { get; }

        /// <summary>
        /// Gets a list of partition classes defined in the metamodel digest.
        /// </summary>
        public List<string> PartitionClasses { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of rootable classes defined in the metamodel digest.
        /// </summary>
        public Dictionary<int, List<string>> RootableClasses { get; }

        /// <summary>
        /// Gets a dictionary that maps from alias class URI to the alias property URI for the class.
        /// </summary>
        public Dictionary<string, string> Aliases { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a <see cref="StringRestriction"/> object that describes restrictions on identifiers used in element definitions.
        /// </summary>
        public Dictionary<int, StringRestriction> IdentifierDefinitionRestrictions { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a <see cref="StringRestriction"/> object that describes restrictions on identifiers used in element references.
        /// </summary>
        public Dictionary<int, StringRestriction> IdentifierReferenceRestrictions { get; }

        /// <summary>
        /// Gets a dicictionary that maps from context ID to a list of identifier prefixes that are reserved for this context.
        /// </summary>
        public Dictionary<string, List<string>> ReservedIdPrefixes { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a <see cref="PartitionRestriction"/> object that describes restrictions on partition classes.
        /// </summary>
        public Dictionary<int, PartitionRestriction> PartitionRestrictions { get; }

        /// <summary>
        /// Gets a list of DTDL versions that allow local term definitions in context blocks.
        /// </summary>
        public List<int> DtdlVersionsAllowingLocalTerms { get; }

        /// <summary>
        /// Gets a list of DTDL versions that allow dynamic extensions to the DTDL language.
        /// </summary>
        public List<int> DtdlVersionsAllowingDynamicExtensions { get; }

        /// <summary>
        /// Gets a list of DTDL versions that restrict the use of unsupported JSON-LD keywords in models.
        /// </summary>
        public List<int> DtdlVersionsRestrictingKeywords { get; }

        /// <summary>
        /// Gets a list of DTDL versions for which apocryphal properties are dependent on an apocryphal cotype.
        /// </summary>
        public List<int> DtdlVersionsWithApocryphalPropertyCotypeDependency { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to an <see cref="ApocryphaDigest"/> object that describes the conditions for allowing apocryphal terms and IRIs.
        /// </summary>
        public Dictionary<int, ApocryphaDigest> Apocrypha { get; }

        /// <summary>
        /// Gets a value indicating whether multiple model layers are supported by any recognized language version or extension.
        /// </summary>
        public bool IsLayeringSupported { get; }

        /// <summary>
        /// Gets a list of context specifiers for which definitions whose identifiers contain IRI fragments should be merged.
        /// </summary>
        public List<string> ContextsMergeDefinitions { get; }

        /// <summary>
        /// Gets a dictionary that maps from affiliate context specifier to a <see cref="ExtensionDocumentationDigest"/>.
        /// </summary>
        public Dictionary<string, ExtensionDocumentationDigest> ExtensionDocumentationDigests { get; }

        /// <summary>
        /// Gets a dictionary that maps from affiliate context specifiers to implicit DTDL versions, for those affiliate contexts that are permitted to precede a DTDL context for back-compat reasons.
        /// </summary>
        public Dictionary<string, int> AffiliateContextsImplicitDtdlVersions { get; }

        /// <summary>
        /// Gets a dictionary that maps from class name to a dictionary that maps from DTDL version to a <see cref="StringRestriction"/> object that describes restrictions on identifiers used in specific classes of definitions.
        /// </summary>
        public Dictionary<string, Dictionary<int, StringRestriction>> ClassIdentifierDefinitionRestrictions { get; }

        /// <summary>
        /// Gets a list of strings that indicate the extension points defined in the metamodel digest.
        /// </summary>
        public List<string> ExtensionKinds { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of strings that each indicate a material class that is extensible.
        /// </summary>
        public Dictionary<int, List<string>> ExtensibleMaterialClasses { get; }

        /// <summary>
        /// Gets a dictionary that maps from class name to a <see cref="MaterialClassDigest"/> object providing details about the named material class.
        /// </summary>
        public Dictionary<string, MaterialClassDigest> MaterialClasses { get; }

        /// <summary>
        /// Gets a list of <see cref="DescendantControlDigest"/> objects that each describe a descendant control defined in the metamodel digest.
        /// </summary>
        public List<DescendantControlDigest> DescendantControls { get; }

        /// <summary>
        /// Gets a dictionary that maps from type URI to a <see cref="SupplementalTypeDigest"/> object providing details about the identified supplemental type.
        /// </summary>
        public Dictionary<string, SupplementalTypeDigest> SupplementalTypes { get; }

        /// <summary>
        /// Gets a dictionary that maps from a context specifier to a dictionary that maps from a quantitative type to a unit type.
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> QuantitativeTypes { get; }

        /// <summary>
        /// Gets a dictionary that maps from a context specifier to a dictionary that maps from a unit type to a list of unit values.
        /// </summary>
        public Dictionary<string, Dictionary<string, List<string>>> Units { get; }

        /// <summary>
        /// Gets a dictionary that maps from parent URI to a list of children URIs from the structural elements in DTDL affiliate models.
        /// </summary>
        public Dictionary<string, List<string>> Families { get; }

        /// <summary>
        /// Gets the JSON text of the 'elements' property in the metamodel digest.
        /// </summary>
        public string ElementsJsonText { get; }
    }
}
