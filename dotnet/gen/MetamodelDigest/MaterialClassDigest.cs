namespace DTDLParser
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class that abstracts material class information extracted from the metamodel digest provided by the metaparser.
    /// </summary>
    public class MaterialClassDigest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialClassDigest"/> class.
        /// </summary>
        public MaterialClassDigest()
        {
            this.DtdlVersions = new List<int>();
            this.IsAbstract = false;
            this.IsOvert = false;
            this.IsAugmentable = false;
            this.IsPartition = false;
            this.IsRootable = false;
            this.MaybePartial = false;
            this.ParentClass = null;
            this.TypeOptionalVersions = new List<int>();
            this.IdRequiredVersions = new List<int>();
            this.IdRequiredWhen = new Dictionary<int, List<ClassPropertyPair>>();
            this.TypeIds = new List<string>();
            this.ConcreteSubclasses = new Dictionary<int, List<string>>();
            this.ElementalSubclasses = new Dictionary<int, List<string>>();
            this.ExtensibleMaterialSubclasses = new Dictionary<int, List<string>>();
            this.StandardElementIds = new Dictionary<int, List<string>>();
            this.BadTypeCauseFormat = new Dictionary<int, string>();
            this.BadTypeLocatedCauseFormat = new Dictionary<int, string>();
            this.BadTypeActionFormat = new Dictionary<int, string>();
            this.Properties = new Dictionary<string, MaterialPropertyDigest>();
            this.Instance = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialClassDigest"/> class.
        /// </summary>
        /// <param name="materialClassObj">A <c>JObject</c> from the metamodel digest containing information about a material class.</param>
        public MaterialClassDigest(JObject materialClassObj)
        {
            this.DtdlVersions = ((JArray)materialClassObj["dtdlVersions"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.IsAbstract = ((JValue)materialClassObj["abstract"]).Value<bool>();

            this.IsOvert = ((JValue)materialClassObj["overt"]).Value<bool>();

            this.IsAugmentable = !this.IsAbstract;

            this.IsPartition = ((JValue)materialClassObj["partition"]).Value<bool>();

            this.IsRootable = ((JValue)materialClassObj["rootable"]).Value<bool>();

            this.MaybePartial = ((JValue)materialClassObj["maybePartial"]).Value<bool>();

            this.ParentClass = ((JValue)materialClassObj["parentClass"]).Value<string>();

            this.TypeOptionalVersions = ((JArray)materialClassObj["typeOptionalVersions"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.IdRequiredVersions = ((JArray)materialClassObj["idRequiredVersions"]).Select(t => ((JValue)t).Value<int>()).ToList();

            this.IdRequiredWhen = ((JObject)materialClassObj["idRequiredWhen"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => new ClassPropertyPair((JObject)t)).ToList());

            this.TypeIds = ((JArray)materialClassObj["typeIds"]).Select(t => ((JValue)t).Value<string>()).ToList();

            this.ConcreteSubclasses = ((JObject)materialClassObj["concreteSubclasses"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList());

            this.ElementalSubclasses = ((JObject)materialClassObj["elementalSubclasses"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList());

            this.Elements = ((JObject)materialClassObj["elements"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => new StandardElementDigest((JObject)t)).ToList());

            this.ExtensibleMaterialSubclasses = ((JObject)materialClassObj["extensibleMaterialSubclasses"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList());

            this.StandardElementIds = materialClassObj.TryGetValue("standardElementIds", out JToken standardElementIds) ? ((JObject)standardElementIds).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JArray)p.Value).Select(t => ((JValue)t).Value<string>()).ToList()) : new Dictionary<int, List<string>>();

            this.BadTypeCauseFormat = ((JObject)materialClassObj["badTypeCauseFormat"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JValue)p.Value).Value<string>());

            this.BadTypeLocatedCauseFormat = ((JObject)materialClassObj["badTypeLocatedCauseFormat"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JValue)p.Value).Value<string>());

            this.BadTypeActionFormat = ((JObject)materialClassObj["badTypeActionFormat"]).Properties().ToDictionary(p => int.Parse(p.Name), p => ((JValue)p.Value).Value<string>());

            this.Properties = ((JObject)materialClassObj["properties"]).Properties().ToDictionary(p => p.Name, p => new MaterialPropertyDigest((JObject)p.Value));

            this.Instance = materialClassObj.TryGetValue("instance", out JToken instance) ? new InstanceValidationDigest((JObject)instance) : null;
        }

        /// <summary>
        /// Gets a list of DTDL versions in which the class has been defined.
        /// </summary>
        public List<int> DtdlVersions { get; }

        /// <summary>
        /// Gets a value indicating whether the obverse class is to be abstract according to the metamodel.
        /// </summary>
        public bool IsAbstract { get; }

        /// <summary>
        /// Gets a value indicating whether the DTDL type is permitted to be used in a DTDL model.
        /// </summary>
        public bool IsOvert { get; }

        /// <summary>
        /// Gets a value indicating whether the DTDL type is augmentable.
        /// </summary>
        public bool IsAugmentable { get; }

        /// <summary>
        /// Gets a value indicating whether the class is designated as a partition type in the metamodel.
        /// </summary>
        public bool IsPartition { get; }

        /// <summary>
        /// Gets a value indicating whether the class is a subclass of a class that is designated as a rootable type or partition type in the metamodel.
        /// </summary>
        public bool IsRootable { get; }

        /// <summary>
        /// Gets a value indicating whether the class is an allowed cotype for a supplemental class that is designated as mergeable.
        /// </summary>
        public bool MaybePartial { get; }

        /// <summary>
        /// Gets the parent class of the class.
        /// </summary>
        public string ParentClass { get; }

        /// <summary>
        /// Gets a list of DTDL versions for which the @type property is optional.
        /// </summary>
        public List<int> TypeOptionalVersions { get; }

        /// <summary>
        /// Gets a list of DTDL versions for which the @id property is required.
        /// </summary>
        public List<int> IdRequiredVersions { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of <see cref="ClassPropertyPair"/> objects that indicate when an @id is required.
        /// </summary>
        public Dictionary<int, List<ClassPropertyPair>> IdRequiredWhen { get; }

        /// <summary>
        /// Gets a list of type URIs of which the class is a subtype.
        /// </summary>
        public List<string> TypeIds { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of type URIs that are subclasses of the class.
        /// </summary>
        public Dictionary<int, List<string>> ConcreteSubclasses { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of type URIs that are subclasses of the class and which have any instances that are standard elements.
        /// </summary>
        public Dictionary<int, List<string>> ElementalSubclasses { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of standard element IDs.
        /// </summary>
        public Dictionary<int, List<StandardElementDigest>> Elements { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of type URIs that are subclasses of the class and which are both extensible and material.
        /// </summary>
        public Dictionary<int, List<string>> ExtensibleMaterialSubclasses { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a list of IDs of standard elements that are instances of the class.
        /// </summary>
        public Dictionary<int, List<string>> StandardElementIds { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a string that describes the cause of a bad type error.
        /// </summary>
        public Dictionary<int, string> BadTypeCauseFormat { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a string that describes the cause of a bad type error, with source location information.
        /// </summary>
        public Dictionary<int, string> BadTypeLocatedCauseFormat { get; }

        /// <summary>
        /// Gets a dictionary that maps from DTDL version to a string that describes the action that will resolve a bad type error.
        /// </summary>
        public Dictionary<int, string> BadTypeActionFormat { get; }

        /// <summary>
        /// Gets a dictionary that maps from property name to a <see cref="MaterialPropertyDigest"/> object providing details about the property.
        /// </summary>
        public Dictionary<string, MaterialPropertyDigest> Properties { get; }

        /// <summary>
        /// Gets an <see cref="InstanceValidationDigest"/> providing instance validation criteria for the DTDL type.
        /// </summary>
        public InstanceValidationDigest Instance { get; }
    }
}
