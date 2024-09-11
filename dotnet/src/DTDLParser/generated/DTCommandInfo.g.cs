/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using DTDLParser;

    /// <summary>
    /// Class <c>DTCommandInfo</c> corresponds to an element of type Command in a DTDL model.
    /// </summary>
    public class DTCommandInfo : DTContentInfo, ITypeChecker, IConstrainer, IPropertyInstanceBinder, IEquatable<DTCommandInfo>
    {
        private static readonly Dictionary<int, HashSet<DTEntityKind>> ConcreteKinds = new Dictionary<int, HashSet<DTEntityKind>>();

        private static readonly Dictionary<int, string> BadTypeActionFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeCauseFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeLocatedCauseFormat = new Dictionary<int, string>();

        private static readonly HashSet<string> PropertyNames = new HashSet<string>();

        private static readonly HashSet<string> VersionlessTypes = new HashSet<string>();

        private bool checkedForDescendantSchemaArray = false;

        private bool checkedForDescendantSchemaOrContentsComponentNarrow = false;

        private Dictionary<string, Dtmi> supplementalPropertySources = new Dictionary<string, Dtmi>();

        private Dictionary<string, JsonLdElement> eltsOfDescendantSchemaArray = null;

        private Dictionary<string, JsonLdElement> eltsOfDescendantSchemaOrContentsComponentNarrow = null;

        private Dictionary<string, object> supplementalProperties;

        private Dictionary<string, string> descriptionPropertyLayer = null;

        private Dictionary<string, string> displayNamePropertyLayer = null;

        private Dictionary<string, string> supplementalSingularPropertyLayers = new Dictionary<string, string>();

        private Dtmi idOfDescendantSchemaArray = null;

        private Dtmi idOfDescendantSchemaOrContentsComponentNarrow = null;

        private HashSet<Dtmi> supplementalTypeIds;

        private HashSet<int> commandTypeAllowedVersionsV2 = new HashSet<int>() { 2 };

        private HashSet<int> commandTypeAllowedVersionsV3 = new HashSet<int>() { 3 };

        private HashSet<int> commandTypeAllowedVersionsV4 = new HashSet<int>() { 4 };

        private HashSet<int> requestAllowedVersionsV2 = new HashSet<int>() { 2 };

        private HashSet<int> requestAllowedVersionsV3 = new HashSet<int>() { 3 };

        private HashSet<int> requestAllowedVersionsV4 = new HashSet<int>() { 4 };

        private HashSet<int> responseAllowedVersionsV2 = new HashSet<int>() { 2 };

        private HashSet<int> responseAllowedVersionsV3 = new HashSet<int>() { 3 };

        private HashSet<int> responseAllowedVersionsV4 = new HashSet<int>() { 4 };

        private int countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowValue = 0;

        private int countOfExtendsNarrowValue = 0;

        private List<DTSupplementalTypeInfo> supplementalTypes;

        private List<string> commandTypeInstanceProperties = null;

        private List<string> requestInstanceProperties = null;

        private List<string> responseInstanceProperties = null;

        private List<ValueConstraint> commandTypeValueConstraints = null;

        private List<ValueConstraint> commentValueConstraints = null;

        private List<ValueConstraint> descriptionValueConstraints = null;

        private List<ValueConstraint> displayNameValueConstraints = null;

        private List<ValueConstraint> nameValueConstraints = null;

        private List<ValueConstraint> requestValueConstraints = null;

        private List<ValueConstraint> responseValueConstraints = null;

        private string commandTypePropertyLayer = null;

        private string commentPropertyLayer = null;

        private string namePropertyLayer = null;

        private string requestPropertyLayer = null;

        private string responsePropertyLayer = null;

        private TraversalStatus countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowStatus = TraversalStatus.NotStarted;

        private TraversalStatus countOfExtendsNarrowStatus = TraversalStatus.NotStarted;

        private Type checkedDescendantEnumValueDatatype = null;

        static DTCommandInfo()
        {
            VersionlessTypes.Add("dtmi:dtdl:class:Command");
            VersionlessTypes.Add("dtmi:dtdl:class:Content");
            VersionlessTypes.Add("dtmi:dtdl:class:Entity");
            VersionlessTypes.Add("dtmi:dtdl:class:NamedEntity");

            PropertyNames.Add("commandType");
            PropertyNames.Add("comment");
            PropertyNames.Add("description");
            PropertyNames.Add("displayName");
            PropertyNames.Add("languageMajorVersion");
            PropertyNames.Add("name");
            PropertyNames.Add("request");
            PropertyNames.Add("response");

            ConcreteKinds[2] = new HashSet<DTEntityKind>();
            ConcreteKinds[2].Add(DTEntityKind.Command);

            ConcreteKinds[3] = new HashSet<DTEntityKind>();
            ConcreteKinds[3].Add(DTEntityKind.Command);

            ConcreteKinds[4] = new HashSet<DTEntityKind>();
            ConcreteKinds[4].Add(DTEntityKind.Command);

            BadTypeActionFormat[2] = "Provide a @type{line3} of Command, or provide a value for property '{property}'{line1} with @type of Command.";
            BadTypeActionFormat[3] = "Provide a @type{line3} of Command, or provide a value for property '{property}'{line1} with @type of Command.";
            BadTypeActionFormat[4] = "Provide a @type{line3} of Command, or provide a value for property '{property}'{line1} with @type of Command.";

            BadTypeCauseFormat[2] = "{layer}{primaryId:p} property '{property}' has value{secondaryId:e} that does not have @type of Command.";
            BadTypeCauseFormat[3] = "{layer}{primaryId:p} property '{property}' has value{secondaryId:e} that does not have @type of Command.";
            BadTypeCauseFormat[4] = "{layer}{primaryId:p} property '{property}' has value{secondaryId:e} that does not have @type of Command.";

            BadTypeLocatedCauseFormat[2] = "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e}{line2} that does not have @type of Command.";
            BadTypeLocatedCauseFormat[3] = "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e}{line2} that does not have @type of Command.";
            BadTypeLocatedCauseFormat[4] = "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e}{line2} that does not have @type of Command.";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTCommandInfo"/> class.
        /// </summary>
        /// <param name="dtdlVersion">Version of DTDL used to define the Command.</param>
        /// <param name="id">Identifier for the Command.</param>
        /// <param name="childOf">Identifier of the parent element in which this Command is defined.</param>
        /// <param name="myPropertyName">Name of the property by which the parent DTDL element refers to this Command.</param>
        /// <param name="definedIn">Identifier of the partition in which this Command is defined.</param>
        internal DTCommandInfo(int dtdlVersion, Dtmi id, Dtmi childOf, string myPropertyName, Dtmi definedIn)
            : base(dtdlVersion, id, childOf, myPropertyName, definedIn, DTEntityKind.Command)
        {
            this.CommandType = null;
            this.Request = null;
            this.Response = null;

            this.supplementalTypeIds = new HashSet<Dtmi>();
            this.supplementalProperties = new Dictionary<string, object>();
            this.supplementalTypes = new List<DTSupplementalTypeInfo>();

            this.IsPartition = false;

            this.MaybePartial = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTCommandInfo"/> class.
        /// </summary>
        /// <param name="dtdlVersion">Version of DTDL used to define the Command.</param>
        /// <param name="id">Identifier for the Command.</param>
        /// <param name="childOf">Identifier of the parent element in which this Command is defined.</param>
        /// <param name="myPropertyName">Name of the property by which the parent DTDL element refers to this Command.</param>
        /// <param name="definedIn">Identifier of the partition in which this Command is defined.</param>
        /// <param name="entityKind">The kind of Entity, which may be other than Command if this constructor is called from a derived class.</param>
        internal DTCommandInfo(int dtdlVersion, Dtmi id, Dtmi childOf, string myPropertyName, Dtmi definedIn, DTEntityKind entityKind)
            : base(dtdlVersion, id, childOf, myPropertyName, definedIn, entityKind)
        {
            this.CommandType = null;
            this.Request = null;
            this.Response = null;

            this.supplementalTypeIds = new HashSet<Dtmi>();
            this.supplementalProperties = new Dictionary<string, object>();
            this.supplementalTypes = new List<DTSupplementalTypeInfo>();

            this.IsPartition = false;

            this.MaybePartial = false;
        }

        /// <summary>
        /// Gets the value of the 'request' property of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>The 'request' property of the DTDL element.</value>
        public DTCommandPayloadInfo Request { get; internal set; }

        /// <summary>
        /// Gets the value of the 'response' property of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>The 'response' property of the DTDL element.</value>
        public DTCommandPayloadInfo Response { get; internal set; }

        /// <summary>
        /// Gets the value of the 'commandType' property of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>The 'commandType' property of the DTDL element.</value>
        public DTCommandTypeInfo CommandType { get; internal set; }

        /// <summary>
        /// Get the DTMI that identifies type Command in the version of DTDL used to define this element.
        /// </summary>
        /// <value>The DTMI for the DTDL type Command.</value>
        public override Dtmi ClassId
        {
            get
            {
                return new Dtmi($"dtmi:dtdl:class:Command;{this.LanguageMajorVersion}");
            }
        }

        /// <summary>
        /// Gets the supplemantal properties of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>A dictionary that maps each string-valued property name to an object that holds the value of the property with the given name.</value>
        /// <remarks>
        /// If the property is a literal in the DTDL model, the object holds a literal value.
        /// If the property is another DTDL element in the model, the object is the C# object that corresponds to this element.
        /// </remarks>
        public override IDictionary<string, object> SupplementalProperties
        {
            get
            {
                return this.supplementalProperties;
            }
        }

        /// <summary>
        /// Gets a collection of identifiers, each of which is a <c>Dtmi</c> that indicates a supplemental type that applies to the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>A collection of DTMIs indicating the supplemental types that apply to the DTDL element.</value>
        public override IReadOnlyCollection<Dtmi> SupplementalTypes
        {
            get
            {
                return this.supplementalTypeIds;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this class is a partition point for the object model.
        /// </summary>
        internal override bool IsPartition { get; }

        /// <summary>
        /// Gets material properties allowed by the type of DTDL element.
        /// </summary>
        internal override HashSet<string> MaterialProperties
        {
            get
            {
                return PropertyNames;
            }
        }

        /// <summary>
        /// Determines whether two <c>DTCommandInfo</c> objects are not equal.
        /// </summary>
        /// <param name="x">One <c>DTCommandInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTCommandInfo</c> object to compare to the first.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(DTCommandInfo x, DTCommandInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return !ReferenceEquals(null, y);
            }

            return !x.Equals(y);
        }

        /// <summary>
        /// Determines whether two <c>DTCommandInfo</c> objects are equal.
        /// </summary>
        /// <param name="x">One <c>DTCommandInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTCommandInfo</c> object to compare to the first.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(DTCommandInfo x, DTCommandInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return ReferenceEquals(null, y);
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Compares to another <c>DTCommandInfo</c> object, recursing through the entire subtree of object properties.
        /// </summary>
        /// <param name="other">The other <c>DTCommandInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool DeepEquals(DTCommandInfo other)
        {
            return base.DeepEquals(other)
                && (this.CommandType?.DeepEquals(other.CommandType) ?? ReferenceEquals(null, other.CommandType))
                && (this.Request?.DeepEquals(other.Request) ?? ReferenceEquals(null, other.Request))
                && (this.Response?.DeepEquals(other.Response) ?? ReferenceEquals(null, other.Response))
                && Helpers.AreDictionariesDeepOrLiteralEqual(this.supplementalProperties, other.supplementalProperties)
                && this.supplementalTypeIds.SetEquals(other.supplementalTypeIds)
                ;
        }

        /// <inheritdoc/>
        public override bool DeepEquals(DTEntityInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.DeepEquals((DTCommandInfo)other);
        }

        /// <inheritdoc/>
        public override bool DeepEquals(DTContentInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.DeepEquals((DTCommandInfo)other);
        }

        /// <inheritdoc/>
        public override bool DeepEquals(DTNamedEntityInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.DeepEquals((DTCommandInfo)other);
        }

        /// <summary>
        /// Compares to another <c>DTCommandInfo</c> object.
        /// </summary>
        /// <param name="other">The other <c>DTCommandInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool Equals(DTCommandInfo other)
        {
            return base.Equals(other)
                && Helpers.AreDictionariesIdOrLiteralEqual(this.supplementalProperties, other.supplementalProperties)
                && this.CommandType?.Id == other.CommandType?.Id
                && this.Request?.Id == other.Request?.Id
                && this.Response?.Id == other.Response?.Id
                && this.supplementalTypeIds.SetEquals(other.supplementalTypeIds)
                ;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object otherObj)
        {
            return otherObj is DTCommandInfo other && this.Equals(other);
        }

        /// <inheritdoc/>
        public override bool Equals(DTNamedEntityInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.Equals((DTCommandInfo)other);
        }

        /// <inheritdoc/>
        public override bool Equals(DTContentInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.Equals((DTCommandInfo)other);
        }

        /// <inheritdoc/>
        public override bool Equals(DTEntityInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.Equals((DTCommandInfo)other);
        }

        /// <inheritdoc/>
        bool ITypeChecker.DoesHaveType(Dtmi typeId)
        {
            return VersionlessTypes.Contains(typeId.Versionless)
                || this.supplementalTypes.Any(ps => ((ITypeChecker)ps).DoesHaveType(typeId))
                ;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();

            unchecked
            {
                hashCode = (hashCode * 131) + (ReferenceEquals(null, this.CommandType) ? 0 : this.CommandType.GetHashCode());
                hashCode = (hashCode * 131) + (ReferenceEquals(null, this.Request) ? 0 : this.Request.GetHashCode());
                hashCode = (hashCode * 131) + (ReferenceEquals(null, this.Response) ? 0 : this.Response.GetHashCode());
                hashCode = (hashCode * 131) + Helpers.GetDictionaryIdOrLiteralHashCode(this.supplementalProperties);
                hashCode = (hashCode * 131) + Helpers.GetSetLiteralHashCode(this.supplementalTypeIds);
            }

            return hashCode;
        }

        /// <inheritdoc/>
        void IConstrainer.AddPropertyValueConstraint(string propertyName, ValueConstraint valueConstraint)
        {
            switch (propertyName)
            {
                case "commandType":
                    if (this.commandTypeValueConstraints == null)
                    {
                        this.commandTypeValueConstraints = new List<ValueConstraint>();
                    }

                    this.commandTypeValueConstraints.Add(valueConstraint);
                    break;
                case "comment":
                    if (this.commentValueConstraints == null)
                    {
                        this.commentValueConstraints = new List<ValueConstraint>();
                    }

                    this.commentValueConstraints.Add(valueConstraint);
                    break;
                case "description":
                    if (this.descriptionValueConstraints == null)
                    {
                        this.descriptionValueConstraints = new List<ValueConstraint>();
                    }

                    this.descriptionValueConstraints.Add(valueConstraint);
                    break;
                case "displayName":
                    if (this.displayNameValueConstraints == null)
                    {
                        this.displayNameValueConstraints = new List<ValueConstraint>();
                    }

                    this.displayNameValueConstraints.Add(valueConstraint);
                    break;
                case "name":
                    if (this.nameValueConstraints == null)
                    {
                        this.nameValueConstraints = new List<ValueConstraint>();
                    }

                    this.nameValueConstraints.Add(valueConstraint);
                    break;
                case "request":
                    if (this.requestValueConstraints == null)
                    {
                        this.requestValueConstraints = new List<ValueConstraint>();
                    }

                    this.requestValueConstraints.Add(valueConstraint);
                    break;
                case "response":
                    if (this.responseValueConstraints == null)
                    {
                        this.responseValueConstraints = new List<ValueConstraint>();
                    }

                    this.responseValueConstraints.Add(valueConstraint);
                    break;
            }
        }

        /// <inheritdoc/>
        void IConstrainer.AddSiblingConstraint(SiblingConstraint siblingConstraint)
        {
            if (this.SiblingConstraints == null)
            {
                this.SiblingConstraints = new List<SiblingConstraint>();
            }

            this.SiblingConstraints.Add(siblingConstraint);
        }

        /// <inheritdoc/>
        void IPropertyInstanceBinder.AddInstanceProperty(string propertyName, string instancePropertyName)
        {
            switch (propertyName)
            {
                case "commandType":
                    if (this.commandTypeInstanceProperties == null)
                    {
                        this.commandTypeInstanceProperties = new List<string>();
                    }

                    this.commandTypeInstanceProperties.Add(instancePropertyName);
                    break;
                case "request":
                    if (this.requestInstanceProperties == null)
                    {
                        this.requestInstanceProperties = new List<string>();
                    }

                    this.requestInstanceProperties.Add(instancePropertyName);
                    break;
                case "response":
                    if (this.responseInstanceProperties == null)
                    {
                        this.responseInstanceProperties = new List<string>();
                    }

                    this.responseInstanceProperties.Add(instancePropertyName);
                    break;
            }
        }

        /// <summary>
        /// Parse an element encoded in a <see cref="JsonLdElement"/> into an object that is a subclass of type <c>DTCommandInfo</c>.
        /// </summary>
        /// <param name="model">The model to add the element to.</param>
        /// <param name="objectPropertyInfoList">A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="valueConstraints">List of <see cref="ValueConstraint"/> to be added to <paramref name="elementPropertyConstraints"/>.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element, used for auto ID generation.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="dtmiSeg">A DTMI segment identifier, used for auto ID generation.</param>
        /// <param name="keyProp">A property used for the key if the parent exposes a collection of these elements as a dictionary.</param>
        /// <param name="idRequired">A boolean value indicating whether an @id must be present.</param>
        /// <param name="typeRequired">A boolean value indicating whether a @type must be present.</param>
        /// <param name="globalize">Treat all nested definitions as though defined globally.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="tolerateSolecisms">Tolerate specific minor invalidities to support backward compatibility.</param>
        /// <param name="allowedVersions">A set of allowed versions for the element.</param>
        /// <param name="inferredType">The type name to infer if no @type specified on element.</param>
        /// <returns>True if the <see cref="JsonLdElement"/> parses correctly as an appropriate element.</returns>
        internal static new bool TryParseElement(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, string dtmiSeg, string keyProp, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool tolerateSolecisms, HashSet<int> allowedVersions, string inferredType)
        {
            AggregateContext childAggregateContext = aggregateContext.GetChildContext(elt, parsingErrorCollection);

            bool allowIdReferenceSyntax = tolerateSolecisms && aggregateContext.DtdlVersion < 3;

            if (elt.PropertyCount == 1 && elt.Id != null)
            {
                if (allowIdReferenceSyntax)
                {
                    if (TryParseIdString(objectPropertyInfoList, elementPropertyConstraints, valueConstraints, childAggregateContext, parsingErrorCollection, elt.Id, layer, parentId, propName, propProp, keyProp, allowedVersions))
                    {
                        return true;
                    }
                    else
                    {
                        parsingErrorCollection.Notify(
                            "idRefBadDtmiOrTerm",
                            elementId: parentId,
                            propertyName: propName,
                            propertyValue: elt.Id,
                            element: elt,
                            version: aggregateContext.DtdlVersion.ToString(),
                            layer: layer);
                        return false;
                    }
                }
                else
                {
                    parsingErrorCollection.Notify(
                        "idReference",
                        elementId: parentId,
                        propertyName: propName,
                        identifier: elt.Id,
                        element: elt,
                        layer: layer);
                    return false;
                }
            }

            if (elt.Graph != null)
            {
                parsingErrorCollection.Notify(
                    "graphDisallowed",
                    elementId: parentId,
                    propertyName: propName,
                    element: elt);
            }

            if (childAggregateContext.RestrictKeywords)
            {
                if (elt.Language != null)
                {
                    parsingErrorCollection.Notify(
                        "keywordDisallowed",
                        elementId: parentId,
                        propertyName: propName,
                        keyword: "@language",
                        element: elt);
                }

                if (elt.Value != null)
                {
                    parsingErrorCollection.Notify(
                        "keywordDisallowed",
                        elementId: parentId,
                        propertyName: propName,
                        keyword: "@value",
                        element: elt);
                }

                foreach (string keyword in elt.Keywords)
                {
                    parsingErrorCollection.Notify(
                        "keywordDisallowed",
                        elementId: parentId,
                        propertyName: propName,
                        keyword: keyword,
                        element: elt);
                }
            }

            if (allowedVersions != null && !allowedVersions.Contains(childAggregateContext.DtdlVersion))
            {
                parsingErrorCollection.Notify(
                    "disallowedVersionDefinition",
                    elementId: parentId,
                    propertyName: propName,
                    version: childAggregateContext.DtdlVersion.ToString(),
                    versionRestriction: string.Join(" ,", allowedVersions),
                    contextComponent: childAggregateContext.DtdlContextComponent,
                    layer: layer);
            }

            bool tolerateReservedIds = tolerateSolecisms && aggregateContext.DtdlVersion < 3;

            Dtmi elementId = IdValidator.ParseIdProperty(childAggregateContext, elt, childAggregateContext.MergeDefinitions ? layer : null, parentId, propName, dtmiSeg, idRequired, allowReservedIds || tolerateReservedIds, parsingErrorCollection);

            Dtmi baseElementId = childAggregateContext.MergeDefinitions || elementId.Tail == string.Empty ? elementId.Fragmentless : elementId;
            string elementLayer = childAggregateContext.MergeDefinitions ? elementId.Tail : string.Empty;

            bool ignoreElementsWithAutoIDsAndDuplicateNames = tolerateSolecisms && aggregateContext.DtdlVersion < 3;

            if (model.Dict.TryGetValue(baseElementId, out DTEntityInfo baseElement))
            {
                if (baseElement.JsonLdElements.TryGetValue(elementLayer, out JsonLdElement duplicateElt))
                {
                    if (!baseElementId.IsReserved)
                    {
                        parsingErrorCollection.Notify(
                            "duplicateDefinition",
                            elementId: baseElementId,
                            element: elt,
                            extantElement: duplicateElt,
                            layer: layer);
                    }
                    else if (!ignoreElementsWithAutoIDsAndDuplicateNames && dtmiSeg != null)
                    {
                        JsonLdProperty dtmiSegProp = elt.Properties.First(p => p.Name == dtmiSeg || (Dtmi.TryCreateDtmi(p.Name, out Dtmi d) && ContextCollection.GetTermOrUri(d) == dtmiSeg));
                        JsonLdProperty dupSegProp = duplicateElt.Properties.First(p => p.Name == dtmiSeg || (Dtmi.TryCreateDtmi(p.Name, out Dtmi d) && ContextCollection.GetTermOrUri(d) == dtmiSeg));
                        parsingErrorCollection.Notify(
                            "nonUniquePropertyValue",
                            elementId: parentId,
                            propertyName: propName,
                            nestedName: dtmiSeg,
                            nestedValue: dtmiSegProp.Values.Values.First().StringValue,
                            incidentProperty: dupSegProp,
                            extantProperty: dtmiSegProp);
                    }

                    return false;
                }

                if (baseElement.DtdlVersion != childAggregateContext.DtdlVersion)
                {
                    parsingErrorCollection.Notify(
                        "inconsistentContext",
                        elementId: baseElementId,
                        version: childAggregateContext.DtdlVersion.ToString(),
                        versionRestriction: baseElement.DtdlVersion.ToString(),
                        layer: elementLayer);
                    return false;
                }
            }

            if (typeRequired && elt.Types == null)
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                {
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeLocatedCauseFormat[childAggregateContext.DtdlVersion],
                        BadTypeActionFormat[childAggregateContext.DtdlVersion],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: baseElementId,
                        layer: layer,
                        sourceName1: sourceName1,
                        startLine1: sourceLine1,
                        sourceName2: sourceName2,
                        startLine2: startLine2,
                        endLine2: endLine2);
                }
                else
                {
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeCauseFormat[childAggregateContext.DtdlVersion],
                        BadTypeActionFormat[childAggregateContext.DtdlVersion],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: baseElementId,
                        layer: layer);
                }

                return false;
            }

            DTCommandInfo elementInfo = (DTCommandInfo)baseElement;
            HashSet<Dtmi> immediateSupplementalTypeIds;
            List<string> immediateUndefinedTypes;
            bool typeArrayParsed;
            if (elt.Types == null)
            {
                typeArrayParsed = TryParseTypeStrings(new List<string>() { inferredType }, baseElementId, elementLayer, elt, parentId, definedIn, propName, propProp, ref elementInfo, childAggregateContext, out immediateSupplementalTypeIds, out immediateUndefinedTypes, parsingErrorCollection);
            }
            else
            {
                typeArrayParsed = TryParseTypeStrings(elt.Types, baseElementId, elementLayer, elt, parentId, definedIn, propName, propProp, ref elementInfo, childAggregateContext, out immediateSupplementalTypeIds, out immediateUndefinedTypes, parsingErrorCollection);
            }

            if (!typeArrayParsed || ReferenceEquals(null, elementInfo))
            {
                return false;
            }

            switch (childAggregateContext.DtdlVersion)
            {
                case 2:
                    elementInfo.ParsePropertiesV2(model, objectPropertyInfoList, elementPropertyConstraints, childAggregateContext, immediateSupplementalTypeIds, immediateUndefinedTypes, parsingErrorCollection, elt, elementLayer, definedIn, globalize, allowReservedIds, tolerateSolecisms);
                    break;
                case 3:
                    elementInfo.ParsePropertiesV3(model, objectPropertyInfoList, elementPropertyConstraints, childAggregateContext, immediateSupplementalTypeIds, immediateUndefinedTypes, parsingErrorCollection, elt, elementLayer, definedIn, globalize, allowReservedIds, tolerateSolecisms);
                    break;
                case 4:
                    elementInfo.ParsePropertiesV4(model, objectPropertyInfoList, elementPropertyConstraints, childAggregateContext, immediateSupplementalTypeIds, immediateUndefinedTypes, parsingErrorCollection, elt, elementLayer, definedIn, globalize, allowReservedIds, tolerateSolecisms);
                    break;
            }

            elementInfo.LimitSpecifier = aggregateContext.LimitSpecifier;

            elementInfo.RecordSourceAsAppropriate(elementLayer, elt, childAggregateContext, parsingErrorCollection, atRoot: parentId == null, globalized: globalize);

            model.Dict[baseElementId] = elementInfo;

            if (parentId != null)
            {
                objectPropertyInfoList.Add(new ParsedObjectPropertyInfo() { ElementId = parentId, PropertyName = propName, Layer = layer, JsonLdProperty = propProp, ReferencedElementId = baseElementId, KeyProperty = keyProp, ExpectedKinds = null, AllowedVersions = null, ChildOf = null, BadTypeCauseFormat = null, BadTypeLocatedCauseFormat = null, BadTypeActionFormat = null });

                if (valueConstraints != null && elementPropertyConstraints != null)
                {
                    foreach (ValueConstraint valueConstraint in valueConstraints)
                    {
                        elementPropertyConstraints.Add(new ElementPropertyConstraint() { ParentId = parentId, PropertyName = propName, ElementId = baseElementId, ValueConstraint = valueConstraint });
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Parse a string property value as an identifier for type <c>DTCommandInfo</c>.
        /// </summary>
        /// <param name="objectPropertyInfoList">A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="valueConstraints">List of <see cref="ValueConstraint"/> to be added to <paramref name="elementPropertyConstraints"/>.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="idString">The identifier string to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element, used for auto ID generation.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="keyProp">A property used for the key if the parent exposes a collection of these elements as a dictionary.</param>
        /// <param name="allowedVersions">A set of allowed versions for the element.</param>
        /// <returns>True if the string parses correctly as an identifier.</returns>
        internal static new bool TryParseIdString(List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, string idString, string layer, Dtmi parentId, string propName, JsonLdProperty propProp, string keyProp, HashSet<int> allowedVersions)
        {
            if (!aggregateContext.TryCreateDtmi(idString, out Dtmi elementId))
            {
                return false;
            }

            objectPropertyInfoList.Add(new ParsedObjectPropertyInfo() { ElementId = parentId, PropertyName = propName, Layer = layer, JsonLdProperty = propProp, ReferencedIdString = idString, ReferencedElementId = elementId, KeyProperty = keyProp, ExpectedKinds = ConcreteKinds[aggregateContext.DtdlVersion], AllowedVersions = allowedVersions, ChildOf = null, BadTypeCauseFormat = BadTypeCauseFormat[aggregateContext.DtdlVersion], BadTypeLocatedCauseFormat = BadTypeLocatedCauseFormat[aggregateContext.DtdlVersion], BadTypeActionFormat = BadTypeActionFormat[aggregateContext.DtdlVersion] });

            if (valueConstraints != null && elementPropertyConstraints != null)
            {
                foreach (ValueConstraint valueConstraint in valueConstraints)
                {
                    elementPropertyConstraints.Add(new ElementPropertyConstraint() { ParentId = parentId, PropertyName = propName, ElementId = elementId, ValueConstraint = valueConstraint });
                }
            }

            return true;
        }

        /// <summary>
        /// Parse a list of @type values containing material or supplemental types.
        /// </summary>
        /// <param name="typeStrings">The list of strings to parse.</param>
        /// <param name="elementId">The identifier of the element of this type to create.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element, used for auto ID generation.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="elementInfo">The <see cref="DTCommandInfo"/> created.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="immediateSupplementalTypeIds">A set of supplemental type IDs from the type array.</param>
        /// <param name="immediateUndefinedTypes">A list of undefind type strings from the type array.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if no errors detected in type array.</returns>
        internal static bool TryParseTypeStrings(List<string> typeStrings, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, ref DTCommandInfo elementInfo, AggregateContext aggregateContext, out HashSet<Dtmi> immediateSupplementalTypeIds, out List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection)
        {
            immediateSupplementalTypeIds = new HashSet<Dtmi>();
            immediateUndefinedTypes = new List<string>();

            HashSet<DTEntityKind> materialKinds = new HashSet<DTEntityKind>();

            bool anyFailures = false;
            foreach (string typeString in typeStrings)
            {
                switch (aggregateContext.DtdlVersion)
                {
                    case 2:
                        if (!TryParseTypeStringV2(typeString, elementId, layer, elt, parentId, definedIn, propName, propProp, materialKinds, immediateSupplementalTypeIds, ref elementInfo, ref immediateUndefinedTypes, aggregateContext, parsingErrorCollection))
                        {
                            anyFailures = true;
                        }

                        break;
                    case 3:
                        if (!TryParseTypeStringV3(typeString, elementId, layer, elt, parentId, definedIn, propName, propProp, materialKinds, immediateSupplementalTypeIds, ref elementInfo, ref immediateUndefinedTypes, aggregateContext, parsingErrorCollection))
                        {
                            anyFailures = true;
                        }

                        break;
                    case 4:
                        if (!TryParseTypeStringV4(typeString, elementId, layer, elt, parentId, definedIn, propName, propProp, materialKinds, immediateSupplementalTypeIds, ref elementInfo, ref immediateUndefinedTypes, aggregateContext, parsingErrorCollection))
                        {
                            anyFailures = true;
                        }

                        break;
                }
            }

            if (anyFailures)
            {
                return false;
            }

            if (ReferenceEquals(null, elementInfo))
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                {
                    elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeLocatedCauseFormat[aggregateContext.DtdlVersion],
                        BadTypeActionFormat[aggregateContext.DtdlVersion],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        layer: layer,
                        sourceName1: sourceName1,
                        startLine1: sourceLine1,
                        sourceName2: sourceName2,
                        startLine2: startLine2,
                        endLine2: endLine2,
                        startLine3: sourceLine3);
                }
                else
                {
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeCauseFormat[aggregateContext.DtdlVersion],
                        BadTypeActionFormat[aggregateContext.DtdlVersion],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        layer: layer);
                }

                return false;
            }

            if (!materialKinds.Any())
            {
                parsingErrorCollection.Notify(
                    "layerMissingMaterialType",
                    elementId: elementId,
                    elementType: elementInfo.EntityKind.ToString(),
                    element: elt,
                    layer: layer);
                return false;
            }

            materialKinds.Add(elementInfo.EntityKind);
            if (materialKinds.Count() > 1)
            {
                parsingErrorCollection.Notify(
                    "multipleMaterialTypes",
                    elementId: elementId,
                    valueConjunction: string.Join(" and ", materialKinds),
                    element: elt,
                    layer: layer);
                return false;
            }

            elementInfo.UndefinedTypeStrings.UnionWith(immediateUndefinedTypes);

            bool requiresMergeability = layer != string.Empty && !elementId.IsReserved;
            bool isMergeable = false;
            foreach (Dtmi supplementalTypeId in immediateSupplementalTypeIds)
            {
                if (aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
                {
                    if (!supplementalTypeInfo.AllowedCotypeKinds.Contains(elementInfo.EntityKind))
                    {
                        parsingErrorCollection.Notify(
                            "invalidCotype",
                            elementId: elementId,
                            cotype: ContextCollection.GetTermOrUri(supplementalTypeId),
                            typeRestriction: string.Join(" or ", supplementalTypeInfo.AllowedCotypeKinds),
                            element: elt,
                            layer: layer);
                    }
                    else if (!supplementalTypeInfo.AllowedCotypeVersions.Contains(elementInfo.DtdlVersion))
                    {
                        parsingErrorCollection.Notify(
                            "invalidCotypeVersion",
                            elementId: elementId,
                            cotype: ContextCollection.GetTermOrUri(supplementalTypeId),
                            versionRestriction: string.Join(" or ", supplementalTypeInfo.AllowedCotypeVersions),
                            element: elt,
                            layer: layer);
                    }
                    else
                    {
                        elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                        foreach (Dtmi requiredCocotype in supplementalTypeInfo.RequiredCocotypes)
                        {
                            if (!immediateSupplementalTypeIds.Contains(requiredCocotype))
                            {
                                string requiredCocotypeTerm = ContextCollection.GetTermOrUri(requiredCocotype);
                                parsingErrorCollection.Notify(
                                    "missingCocotype",
                                    elementId: elementId,
                                    elementType: ContextCollection.GetTermOrUri(supplementalTypeId),
                                    cotype: requiredCocotypeTerm,
                                    element: elt);
                            }
                        }
                    }

                    isMergeable = isMergeable || supplementalTypeInfo.IsMergeable;
                }
            }

            if (requiresMergeability && !isMergeable)
            {
                parsingErrorCollection.Notify(
                    "disallowedIdFragment",
                    elementId: elementId,
                    element: elt,
                    layer: layer);
            }

            return true;
        }

        /// <summary>
        /// Parse a string @type value, whether material or supplemental.
        /// </summary>
        /// <param name="typeString">The string value to parse.</param>
        /// <param name="elementId">The identifier of the element of this type to create.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="materialKinds">A set of material kinds to update with the material kind of the type, if any.</param>
        /// <param name="supplementalTypeIds">A set of supplemental type IDs to update with the supplemental type, if any.</param>
        /// <param name="elementInfo">The element created if the type is material.</param>
        /// <param name="undefinedTypes">A list of string values of any undefined supplemental types.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if the parse is sucessful.</returns>
        internal static bool TryParseTypeStringV2(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTCommandInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            switch (typeString)
            {
                case "Command":
                case "dtmi:dtdl:class:Command;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Command);
                    return true;
            }

            if (MaterialTypeNameCollection.IsMaterialType(typeString))
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                {
                    elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeLocatedCauseFormat[2],
                        BadTypeActionFormat[2],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        value: typeString,
                        layer: layer,
                        sourceName1: sourceName1,
                        startLine1: sourceLine1,
                        sourceName2: sourceName2,
                        startLine2: startLine2,
                        endLine2: endLine2,
                        startLine3: sourceLine3);
                }
                else
                {
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeCauseFormat[2],
                        BadTypeActionFormat[2],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        value: typeString,
                        layer: layer);
                }

                return false;
            }

            if (!aggregateContext.TryCreateDtmi(typeString, out Dtmi supplementalTypeId))
            {
                if (typeString.StartsWith("dtmi:"))
                {
                    parsingErrorCollection.Notify(
                        "typeInvalidDtmi",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        layer: layer);
                    return false;
                }
                else
                {
                    /* Non-DTMI IRI always valid as cotype   */
                    /* Undefined term always valid as cotype */
                }

                undefinedTypes.Add(typeString);
                return true;
            }

            if (!aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
            {
                if (aggregateContext.IsComplete)
                {
                    parsingErrorCollection.Notify(
                        "typeIrrelevantDtmiOrTerm",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        version: aggregateContext.DtdlVersion.ToString(),
                        layer: layer);
                    return false;
                }

                undefinedTypes.Add(typeString);
                return true;
            }

            if (supplementalTypeInfo.IsAbstract)
            {
                parsingErrorCollection.Notify(
                    "abstractSupplementalType",
                    elementId: elementId,
                    cotype: ContextCollection.GetTermOrUri(supplementalTypeId),
                    element: elt,
                    layer: layer);
                return false;
            }

            switch (supplementalTypeInfo.ExtensionKind)
            {
                case DTExtensionKind.Unit:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[2],
                                BadTypeActionFormat[2],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[2],
                                BadTypeActionFormat[2],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
                case DTExtensionKind.UnitAttribute:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[2],
                                BadTypeActionFormat[2],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[2],
                                BadTypeActionFormat[2],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
            }

            supplementalTypeIds.Add(supplementalTypeId);

            return true;
        }

        /// <summary>
        /// Parse a string @type value, whether material or supplemental.
        /// </summary>
        /// <param name="typeString">The string value to parse.</param>
        /// <param name="elementId">The identifier of the element of this type to create.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="materialKinds">A set of material kinds to update with the material kind of the type, if any.</param>
        /// <param name="supplementalTypeIds">A set of supplemental type IDs to update with the supplemental type, if any.</param>
        /// <param name="elementInfo">The element created if the type is material.</param>
        /// <param name="undefinedTypes">A list of string values of any undefined supplemental types.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if the parse is sucessful.</returns>
        internal static bool TryParseTypeStringV3(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTCommandInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            switch (typeString)
            {
                case "Command":
                case "dtmi:dtdl:class:Command;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Command);
                    return true;
            }

            if (MaterialTypeNameCollection.IsMaterialType(typeString))
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                {
                    elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeLocatedCauseFormat[3],
                        BadTypeActionFormat[3],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        value: typeString,
                        layer: layer,
                        sourceName1: sourceName1,
                        startLine1: sourceLine1,
                        sourceName2: sourceName2,
                        startLine2: startLine2,
                        endLine2: endLine2,
                        startLine3: sourceLine3);
                }
                else
                {
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeCauseFormat[3],
                        BadTypeActionFormat[3],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        value: typeString,
                        layer: layer);
                }

                return false;
            }

            if (!aggregateContext.TryCreateDtmi(typeString, out Dtmi supplementalTypeId))
            {
                if (typeString.StartsWith("dtmi:"))
                {
                    parsingErrorCollection.Notify(
                        "typeInvalidDtmi",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        layer: layer);
                    return false;
                }
                else if (typeString.Contains(":"))
                {
                    parsingErrorCollection.Notify(
                        "typeNotDtmiNorTerm",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        layer: layer);
                    return false;
                }
                else
                {
                    if (aggregateContext.IsComplete)
                    {
                        parsingErrorCollection.Notify(
                            "typeUndefinedTerm",
                            elementId: elementId,
                            cotype: typeString,
                            element: elt,
                            version: aggregateContext.DtdlVersion.ToString(),
                            layer: layer);
                        return false;
                    }
                }

                undefinedTypes.Add(typeString);
                return true;
            }

            if (!aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
            {
                if (aggregateContext.IsComplete)
                {
                    parsingErrorCollection.Notify(
                        "typeIrrelevantDtmiOrTerm",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        version: aggregateContext.DtdlVersion.ToString(),
                        layer: layer);
                    return false;
                }

                undefinedTypes.Add(typeString);
                return true;
            }

            if (supplementalTypeInfo.IsAbstract)
            {
                parsingErrorCollection.Notify(
                    "abstractSupplementalType",
                    elementId: elementId,
                    cotype: ContextCollection.GetTermOrUri(supplementalTypeId),
                    element: elt,
                    layer: layer);
                return false;
            }

            switch (supplementalTypeInfo.ExtensionKind)
            {
                case DTExtensionKind.LatentType:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
                case DTExtensionKind.NamedLatentType:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
                case DTExtensionKind.Unit:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
                case DTExtensionKind.UnitAttribute:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[3],
                                BadTypeActionFormat[3],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
            }

            supplementalTypeIds.Add(supplementalTypeId);

            return true;
        }

        /// <summary>
        /// Parse a string @type value, whether material or supplemental.
        /// </summary>
        /// <param name="typeString">The string value to parse.</param>
        /// <param name="elementId">The identifier of the element of this type to create.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="materialKinds">A set of material kinds to update with the material kind of the type, if any.</param>
        /// <param name="supplementalTypeIds">A set of supplemental type IDs to update with the supplemental type, if any.</param>
        /// <param name="elementInfo">The element created if the type is material.</param>
        /// <param name="undefinedTypes">A list of string values of any undefined supplemental types.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if the parse is sucessful.</returns>
        internal static bool TryParseTypeStringV4(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTCommandInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            switch (typeString)
            {
                case "Command":
                case "dtmi:dtdl:class:Command;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Command);
                    return true;
            }

            if (MaterialTypeNameCollection.IsMaterialType(typeString))
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                {
                    elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeLocatedCauseFormat[4],
                        BadTypeActionFormat[4],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        value: typeString,
                        layer: layer,
                        sourceName1: sourceName1,
                        startLine1: sourceLine1,
                        sourceName2: sourceName2,
                        startLine2: startLine2,
                        endLine2: endLine2,
                        startLine3: sourceLine3);
                }
                else
                {
                    parsingErrorCollection.Add(
                        new Uri("dtmi:dtdl:parsingError:badType"),
                        BadTypeCauseFormat[4],
                        BadTypeActionFormat[4],
                        primaryId: parentId,
                        property: propName,
                        secondaryId: elementId,
                        value: typeString,
                        layer: layer);
                }

                return false;
            }

            if (!aggregateContext.TryCreateDtmi(typeString, out Dtmi supplementalTypeId))
            {
                if (typeString.StartsWith("dtmi:"))
                {
                    parsingErrorCollection.Notify(
                        "typeInvalidDtmi",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        layer: layer);
                    return false;
                }
                else if (typeString.Contains(":"))
                {
                    parsingErrorCollection.Notify(
                        "typeNotDtmiNorTerm",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        layer: layer);
                    return false;
                }
                else
                {
                    if (aggregateContext.IsComplete)
                    {
                        parsingErrorCollection.Notify(
                            "typeUndefinedTerm",
                            elementId: elementId,
                            cotype: typeString,
                            element: elt,
                            version: aggregateContext.DtdlVersion.ToString(),
                            layer: layer);
                        return false;
                    }
                }

                undefinedTypes.Add(typeString);
                return true;
            }

            if (!aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
            {
                if (aggregateContext.IsComplete)
                {
                    parsingErrorCollection.Notify(
                        "typeIrrelevantDtmiOrTerm",
                        elementId: elementId,
                        cotype: typeString,
                        element: elt,
                        version: aggregateContext.DtdlVersion.ToString(),
                        layer: layer);
                    return false;
                }

                undefinedTypes.Add(typeString);
                return true;
            }

            if (supplementalTypeInfo.IsAbstract)
            {
                parsingErrorCollection.Notify(
                    "abstractSupplementalType",
                    elementId: elementId,
                    cotype: ContextCollection.GetTermOrUri(supplementalTypeId),
                    element: elt,
                    layer: layer);
                return false;
            }

            switch (supplementalTypeInfo.ExtensionKind)
            {
                case DTExtensionKind.LatentType:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
                case DTExtensionKind.NamedLatentType:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
                case DTExtensionKind.Unit:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
                case DTExtensionKind.UnitAttribute:
                    {
                        string sourceName1 = null;
                        int sourceLine1 = 0;
                        if (propProp != null && propProp.TryGetSourceLocation(out sourceName1, out sourceLine1) && elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
                        {
                            elt.TryGetSourceLocationForType(out _, out int sourceLine3);
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeLocatedCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer,
                                sourceName1: sourceName1,
                                startLine1: sourceLine1,
                                sourceName2: sourceName2,
                                startLine2: startLine2,
                                endLine2: endLine2,
                                startLine3: sourceLine3);
                        }
                        else
                        {
                            parsingErrorCollection.Add(
                                new Uri("dtmi:dtdl:parsingError:badType"),
                                BadTypeCauseFormat[4],
                                BadTypeActionFormat[4],
                                primaryId: parentId,
                                property: propName,
                                secondaryId: elementId,
                                value: typeString,
                                layer: layer);
                        }
                    }

                    return false;
            }

            supplementalTypeIds.Add(supplementalTypeId);

            return true;
        }

        /// <summary>
        /// Parse elements encoded in a <see cref="JsonLdValueCollection"/> into objects of subclasses of type <c>DTCommandInfo</c>.
        /// </summary>
        /// <param name="model">The model to add the element to.</param>
        /// <param name="objectPropertyInfoList">A list of <c>ParsedObjectPropertyInfo</c> to add any object properties, which will be assigned after all parsing has completed.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="valueConstraints">List of <see cref="ValueConstraint"/> to be added to <paramref name="elementPropertyConstraints"/>.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="valueCollectionProp">The <see cref="JsonLdProperty"/> holding the <see cref="JsonLdValueCollection"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of the element.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="propName">The name of the property by which the parent refers to this element, used for auto ID generation.</param>
        /// <param name="dtmiSeg">A DTMI segment identifier, used for auto ID generation.</param>
        /// <param name="keyProp">A property used for the key if the parent exposes a collection of these elements as a dictionary.</param>
        /// <param name="minCount">The minimum count of element values required.</param>
        /// <param name="isPlural">A boolean value indicating whether the property allows multiple values.</param>
        /// <param name="idRequired">A boolean value indicating whether an @id must be present.</param>
        /// <param name="typeRequired">A boolean value indicating whether a @type must be present.</param>
        /// <param name="globalize">Treat all nested definitions as though defined globally.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="tolerateSolecisms">Tolerate specific minor invalidities to support backward compatibility.</param>
        /// <param name="allowedVersions">A set of allowed versions for the element.</param>
        internal static new void ParseValueCollection(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdProperty valueCollectionProp, string layer, Dtmi parentId, Dtmi definedIn, string propName, string dtmiSeg, string keyProp, int minCount, bool isPlural, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool tolerateSolecisms, HashSet<int> allowedVersions)
        {
            int valueCount = 0;

            foreach (JsonLdValue value in valueCollectionProp.Values.Values)
            {
                if (!isPlural && valueCount > 0)
                {
                    parsingErrorCollection.Notify(
                        "objectMultipleValues",
                        elementId: parentId,
                        propertyName: propName,
                        incidentValues: valueCollectionProp.Values,
                        layer: layer);
                    return;
                }

                switch (value.ValueType)
                {
                    case JsonLdValueType.String:
                        if (parentId != null)
                        {
                            if (TryParseIdString(objectPropertyInfoList, elementPropertyConstraints, valueConstraints, aggregateContext, parsingErrorCollection, value.StringValue, layer, parentId, propName, valueCollectionProp, keyProp, allowedVersions))
                            {
                                ++valueCount;
                            }
                            else
                            {
                                parsingErrorCollection.Notify(
                                    "badDtmiOrTerm",
                                    elementId: parentId,
                                    propertyName: propName,
                                    propertyValue: value.StringValue,
                                    incidentProperty: valueCollectionProp,
                                    incidentValue: value,
                                    version: aggregateContext.DtdlVersion.ToString(),
                                    layer: layer);
                            }
                        }

                        break;
                    case JsonLdValueType.Element:
                        if (TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, valueConstraints, aggregateContext, parsingErrorCollection, value.ElementValue, layer, parentId, definedIn, propName, valueCollectionProp, dtmiSeg, keyProp, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, allowedVersions, "Command"))
                        {
                            ++valueCount;
                        }

                        break;
                    default:
                        parsingErrorCollection.Notify(
                            "refNotStringOrObject",
                            elementId: parentId,
                            propertyName: propName,
                            incidentProperty: valueCollectionProp,
                            incidentValue: value,
                            layer: layer);
                        break;
                }
            }

            if (valueCount < minCount)
            {
                parsingErrorCollection.Notify(
                    "objectCountBelowMin",
                    elementId: parentId,
                    propertyName: propName,
                    observedCount: valueCount,
                    expectedCount: minCount,
                    incidentProperty: valueCollectionProp,
                    layer: layer);
            }
        }

        /// <inheritdoc/>
        internal override bool CheckDepthOfElementSchemaOrSchema(int depth, int depthLimit, out Dtmi tooDeepElementId, out Dictionary<string, JsonLdElement> tooDeepElts, ParsingErrorCollection parsingErrorCollection)
        {
            if (this.CommandType != null)
            {
                if (!this.CommandType.CheckDepthOfElementSchemaOrSchema(depth, depthLimit, out tooDeepElementId, out tooDeepElts, parsingErrorCollection))
                {
                    if (tooDeepElementId == this.Id)
                    {
                        parsingErrorCollection.Notify(
                            "recursiveStructureWide",
                            elementId: this.Id,
                            propertyDisjunction: "'elementSchema' or 'schema'",
                            element: this.JsonLdElements.First().Value);
                        tooDeepElementId = null;
                    }

                    return false;
                }
            }

            if (this.Request != null)
            {
                if (!this.Request.CheckDepthOfElementSchemaOrSchema(depth, depthLimit, out tooDeepElementId, out tooDeepElts, parsingErrorCollection))
                {
                    if (tooDeepElementId == this.Id)
                    {
                        parsingErrorCollection.Notify(
                            "recursiveStructureWide",
                            elementId: this.Id,
                            propertyDisjunction: "'elementSchema' or 'schema'",
                            element: this.JsonLdElements.First().Value);
                        tooDeepElementId = null;
                    }

                    return false;
                }
            }

            if (this.Response != null)
            {
                if (!this.Response.CheckDepthOfElementSchemaOrSchema(depth, depthLimit, out tooDeepElementId, out tooDeepElts, parsingErrorCollection))
                {
                    if (tooDeepElementId == this.Id)
                    {
                        parsingErrorCollection.Notify(
                            "recursiveStructureWide",
                            elementId: this.Id,
                            propertyDisjunction: "'elementSchema' or 'schema'",
                            element: this.JsonLdElements.First().Value);
                        tooDeepElementId = null;
                    }

                    return false;
                }
            }

            tooDeepElementId = null;
            tooDeepElts = this.JsonLdElements;
            return true;
        }

        /// <summary>
        /// Indicate whether the property with a given <paramref name="propertyName"/> is a dictionary that holds a given <paramref name="key"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <param name="key">The key for the dictionary property.</param>
        /// <returns>True if the property is present, is a dictionary, and holds the given key.</returns>
        internal override bool DoesPropertyDictContainKey(string propertyName, string key)
        {
            if (base.DoesPropertyDictContainKey(propertyName, key))
            {
                return true;
            }

            switch (propertyName)
            {
                default:
                    return false;
            }
        }

        /// <inheritdoc/>
        internal override bool TryGetChild(string childrenPropertyName, string keyPropertyName, string keyPropertyValue, out DTEntityInfo child)
        {
            switch (childrenPropertyName)
            {
                default:
                    break;
            }

            child = null;
            return false;
        }

        /// <inheritdoc/>
        internal override bool TryGetDescendantSchemaArray(out Dtmi elementId, out Dictionary<string, JsonLdElement> excludedElts)
        {
            if (this.checkedForDescendantSchemaArray)
            {
                elementId = this.idOfDescendantSchemaArray;
                excludedElts = this.eltsOfDescendantSchemaArray;
                return this.idOfDescendantSchemaArray != null;
            }

            this.checkedForDescendantSchemaArray = true;

            if (this.CommandType != null)
            {
                if (this.CommandType.TryGetDescendantSchemaArray(out elementId, out excludedElts))
                {
                    this.idOfDescendantSchemaArray = elementId;
                    this.eltsOfDescendantSchemaArray = excludedElts;
                    return true;
                }
            }

            if (this.Request != null)
            {
                if (this.Request.TryGetDescendantSchemaArray(out elementId, out excludedElts))
                {
                    this.idOfDescendantSchemaArray = elementId;
                    this.eltsOfDescendantSchemaArray = excludedElts;
                    return true;
                }
            }

            if (this.Response != null)
            {
                if (this.Response.TryGetDescendantSchemaArray(out elementId, out excludedElts))
                {
                    this.idOfDescendantSchemaArray = elementId;
                    this.eltsOfDescendantSchemaArray = excludedElts;
                    return true;
                }
            }

            elementId = null;
            excludedElts = null;
            return false;
        }

        /// <inheritdoc/>
        internal override bool TryGetDescendantSchemaOrContentsComponentNarrow(out Dtmi elementId, out Dictionary<string, JsonLdElement> excludedElts)
        {
            if (this.checkedForDescendantSchemaOrContentsComponentNarrow)
            {
                elementId = this.idOfDescendantSchemaOrContentsComponentNarrow;
                excludedElts = this.eltsOfDescendantSchemaOrContentsComponentNarrow;
                return this.idOfDescendantSchemaOrContentsComponentNarrow != null;
            }

            this.checkedForDescendantSchemaOrContentsComponentNarrow = true;

            elementId = null;
            excludedElts = null;
            return false;
        }

        /// <summary>
        /// Try to set an object property with a given <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property whose element value to set if the property is recognized.</param>
        /// <param name="layer">The key property for dictionary properties.</param>
        /// <param name="propProp">The <see cref="JsonLdProperty"/> representing the source of the property by which the parent refers to this element.</param>
        /// <param name="element">The reference element to set.</param>
        /// <param name="keyProp">The key property for dictionary properties.</param>
        /// <param name="keyValue">The key value for dictionary properties.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if the property name is recognized.</returns>
        internal override bool TrySetObjectProperty(string propertyName, string layer, JsonLdProperty propProp, DTEntityInfo element, string keyProp, string keyValue, ParsingErrorCollection parsingErrorCollection)
        {
            if (base.TrySetObjectProperty(propertyName, layer, propProp, element, keyProp, keyValue, parsingErrorCollection))
            {
                return true;
            }

            switch (propertyName)
            {
                case "commandType":
                case "dtmi:dtdl:property:commandType;2":
                case "dtmi:dtdl:property:commandType;3":
                case "dtmi:dtdl:property:commandType;4":
                    if (this.CommandType != null)
                    {
                        if (this.CommandType.Id != element.Id)
                        {
                            JsonLdProperty extantProp = this.JsonLdElements[this.commandTypePropertyLayer].Properties.FirstOrDefault(p => p.Name == "commandType");
                            parsingErrorCollection.Notify(
                                "inconsistentObjectValues",
                                elementId: this.Id,
                                propertyName: "commandType",
                                propertyValue: element.Id.ToString(),
                                valueRestriction: this.CommandType.Id.ToString(),
                                incidentProperty: propProp,
                                extantProperty: extantProp,
                                layer: layer);
                        }
                    }
                    else
                    {
                        this.CommandType = (DTCommandTypeInfo)element;
                        this.commandTypePropertyLayer = layer;
                    }

                    return true;
                case "request":
                case "dtmi:dtdl:property:request;2":
                case "dtmi:dtdl:property:request;3":
                case "dtmi:dtdl:property:request;4":
                    if (this.Request != null)
                    {
                        if (this.Request.Id != element.Id)
                        {
                            JsonLdProperty extantProp = this.JsonLdElements[this.requestPropertyLayer].Properties.FirstOrDefault(p => p.Name == "request");
                            parsingErrorCollection.Notify(
                                "inconsistentObjectValues",
                                elementId: this.Id,
                                propertyName: "request",
                                propertyValue: element.Id.ToString(),
                                valueRestriction: this.Request.Id.ToString(),
                                incidentProperty: propProp,
                                extantProperty: extantProp,
                                layer: layer);
                        }
                    }
                    else
                    {
                        this.Request = (DTCommandPayloadInfo)element;
                        this.requestPropertyLayer = layer;
                    }

                    return true;
                case "response":
                case "dtmi:dtdl:property:response;2":
                case "dtmi:dtdl:property:response;3":
                case "dtmi:dtdl:property:response;4":
                    if (this.Response != null)
                    {
                        if (this.Response.Id != element.Id)
                        {
                            JsonLdProperty extantProp = this.JsonLdElements[this.responsePropertyLayer].Properties.FirstOrDefault(p => p.Name == "response");
                            parsingErrorCollection.Notify(
                                "inconsistentObjectValues",
                                elementId: this.Id,
                                propertyName: "response",
                                propertyValue: element.Id.ToString(),
                                valueRestriction: this.Response.Id.ToString(),
                                incidentProperty: propProp,
                                extantProperty: extantProp,
                                layer: layer);
                        }
                    }
                    else
                    {
                        this.Response = (DTCommandPayloadInfo)element;
                        this.responsePropertyLayer = layer;
                    }

                    return true;
                default:
                    break;
            }

            foreach (DTSupplementalTypeInfo supplementalType in this.supplementalTypes)
            {
                if (supplementalType.TrySetObjectProperty(this.Id, propertyName, element, keyValue, layer, propProp, ref this.supplementalProperties, this.supplementalSingularPropertyLayers, this.JsonLdElements, parsingErrorCollection))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        internal override HashSet<Dtmi> GetTransitiveExtendsNarrow(int depth, int depthLimit, out Dtmi tooDeepElementId, out Dictionary<string, JsonLdElement> tooDeepElts, ParsingErrorCollection parsingErrorCollection)
        {
            HashSet<Dtmi> closure = new HashSet<Dtmi>();

            tooDeepElementId = null;
            tooDeepElts = null;
            return closure;
        }

        /// <inheritdoc/>
        internal override IEnumerable<DTEntityInfo> GetChildren(string childrenPropertyName)
        {
            switch (childrenPropertyName)
            {
                default:
                    return new List<DTEntityInfo>();
            }
        }

        /// <inheritdoc/>
        internal override int GetCountOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrow(ParsingErrorCollection parsingErrorCollection)
        {
            if (this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowStatus == TraversalStatus.Complete)
            {
                return this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowValue;
            }

            if (this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowStatus == TraversalStatus.InProgress)
            {
                parsingErrorCollection.Notify(
                    "recursiveStructureNarrow",
                    elementId: this.Id,
                    propertyDisjunction: "'contents' or 'fields' or 'enumValues' or 'request' or 'response' or 'properties' or 'schema' or 'elementSchema' or 'mapValue'",
                    element: this.JsonLdElements.First().Value);
                return 0;
            }

            this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowStatus = TraversalStatus.InProgress;
            if (this.Request != null)
            {
                this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowValue += this.Request.GetCountOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrow(parsingErrorCollection) + 1;
            }

            if (this.Response != null)
            {
                this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowValue += this.Response.GetCountOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrow(parsingErrorCollection) + 1;
            }

            this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowStatus = TraversalStatus.Complete;
            return this.countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowValue;
        }

        /// <inheritdoc/>
        internal override int GetCountOfExtendsNarrow(ParsingErrorCollection parsingErrorCollection)
        {
            if (this.countOfExtendsNarrowStatus == TraversalStatus.Complete)
            {
                return this.countOfExtendsNarrowValue;
            }

            if (this.countOfExtendsNarrowStatus == TraversalStatus.InProgress)
            {
                parsingErrorCollection.Notify(
                    "recursiveStructureNarrow",
                    elementId: this.Id,
                    propertyDisjunction: "'extends'",
                    element: this.JsonLdElements.First().Value);
                return 0;
            }

            this.countOfExtendsNarrowStatus = TraversalStatus.InProgress;
            this.countOfExtendsNarrowStatus = TraversalStatus.Complete;
            return this.countOfExtendsNarrowValue;
        }

        /// <summary>
        /// Add a supplemental type.
        /// </summary>
        /// <param name="id">Identifier for the supplemental type to add.</param>
        /// <param name="supplementalType"><c>DTSupplementalTypeInfo</c> for the supplemental type.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        internal override void AddType(Dtmi id, DTSupplementalTypeInfo supplementalType, ParsingErrorCollection parsingErrorCollection)
        {
            if (this.supplementalTypeIds.Add(id))
            {
                this.supplementalTypes.Add(supplementalType);
                supplementalType.AttachConstraints(this);
                supplementalType.BindInstanceProperties(this);
                if (!supplementalType.TryRecordPropertySources(this.supplementalPropertySources, out string conflictingProperty, out Dtmi conflictingType))
                {
                    string typeString1 = ContextCollection.GetTermOrUri(conflictingType);
                    string typeString2 = ContextCollection.GetTermOrUri(id);
                    string propName = ContextCollection.GetTermOrUri(new Dtmi(conflictingProperty));
                    JsonLdElement elt = this.JsonLdElements.FirstOrDefault(e => e.Value.Types.Contains(typeString1) && e.Value.Types.Contains(typeString2)).Value;
                    parsingErrorCollection.Notify(
                        "conflictingSupplementalTypes",
                        elementId: this.Id,
                        elementType: typeString1,
                        referenceType: typeString2,
                        propertyName: propName,
                        element: elt);
                }
            }
        }

        /// <inheritdoc/>
        internal override void ApplyTransformations(Model model, ParsingErrorCollection parsingErrorCollection)
        {
            switch (this.DtdlVersion)
            {
                case 2:
                    this.ApplyTransformationsV2(model, parsingErrorCollection);
                    break;
                case 3:
                    this.ApplyTransformationsV3(model, parsingErrorCollection);
                    break;
                case 4:
                    this.ApplyTransformationsV4(model, parsingErrorCollection);
                    break;
            }
        }

        /// <inheritdoc/>
        internal override void CheckDescendantEnumValueDatatype(Dtmi ancestorId, Dictionary<string, JsonLdElement> ancestorElts, Type datatype, ParsingErrorCollection parsingErrorCollection)
        {
            if (this.checkedDescendantEnumValueDatatype != datatype)
            {
                this.checkedDescendantEnumValueDatatype = datatype;
                if (this.CommandType != null)
                {
                    this.CommandType.CheckDescendantEnumValueDatatype(ancestorId, ancestorElts, datatype, parsingErrorCollection);
                }

                if (this.Request != null)
                {
                    this.Request.CheckDescendantEnumValueDatatype(ancestorId, ancestorElts, datatype, parsingErrorCollection);
                }

                if (this.Response != null)
                {
                    this.Response.CheckDescendantEnumValueDatatype(ancestorId, ancestorElts, datatype, parsingErrorCollection);
                }
            }
        }

        /// <inheritdoc/>
        internal override void CheckRestrictions(ParsingErrorCollection parsingErrorCollection)
        {
            this.CheckForDisallowedCocotypes(this.supplementalTypeIds, this.supplementalTypes, parsingErrorCollection);

            foreach (DTSupplementalTypeInfo supplementalType in this.supplementalTypes)
            {
                supplementalType.CheckRestrictions(this.supplementalProperties, parsingErrorCollection, this.Id, this.JsonLdElements);
            }

            switch (this.DtdlVersion)
            {
                case 2:
                    this.CheckRestrictionsV2(parsingErrorCollection);
                    break;
                case 3:
                    this.CheckRestrictionsV3(parsingErrorCollection);
                    break;
                case 4:
                    this.CheckRestrictionsV4(parsingErrorCollection);
                    break;
            }
        }

        /// <summary>
        /// Parse the properties in a JSON Command object.
        /// </summary>
        /// <param name="model">Model to which to add object properties.</param>
        /// <param name="objectPropertyInfoList">List of object info structs for deferred assignments.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="immediateSupplementalTypeIds">A set of supplemental type IDs.</param>
        /// <param name="immediateUndefinedTypes">A list of undefind type strings.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="globalize">Treat all nested definitions as though defined globally.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="tolerateSolecisms">Tolerate specific minor invalidities to support backward compatibility.</param>
        internal override void ParsePropertiesV2(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool tolerateSolecisms)
        {
            this.LanguageMajorVersion = 2;

            JsonLdProperty commandTypeProperty = null;
            JsonLdProperty commentProperty = null;
            JsonLdProperty descriptionProperty = null;
            JsonLdProperty displayNameProperty = null;
            JsonLdProperty nameProperty = null;
            JsonLdProperty requestProperty = null;
            JsonLdProperty responseProperty = null;
            Dictionary<string, JsonLdProperty> supplementalJsonLdProperties = new Dictionary<string, JsonLdProperty>();

            foreach (JsonLdProperty prop in elt.Properties)
            {
                switch (prop.Name)
                {
                    case "commandType":
                    case "dtmi:dtdl:property:commandType;2":
                        if (commandTypeProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "commandType",
                                incidentProperty: prop,
                                extantProperty: commandTypeProperty,
                                layer: layer);
                        }
                        else
                        {
                            commandTypeProperty = prop;
                            DTCommandTypeInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.commandTypeValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "commandType", null, null, 0, isPlural: false, idRequired: true, typeRequired: true, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.commandTypeAllowedVersionsV2);
                        }

                        continue;
                    case "comment":
                    case "dtmi:dtdl:property:comment;2":
                        if (commentProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "comment",
                                incidentProperty: prop,
                                extantProperty: commentProperty,
                                layer: layer);
                        }
                        else
                        {
                            commentProperty = prop;
                            int? maxLength = 512;
                            string newComment = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "comment", prop.Values, maxLength, null, layer, parsingErrorCollection, isOptional: true);
                            if (this.commentPropertyLayer != null)
                            {
                                if (this.Comment != newComment)
                                {
                                    JsonLdProperty extantProp = this.JsonLdElements[this.commentPropertyLayer].Properties.FirstOrDefault(p => p.Name == "comment");
                                    parsingErrorCollection.Notify(
                                        "inconsistentStringValues",
                                        elementId: this.Id,
                                        propertyName: "comment",
                                        propertyValue: newComment.ToString(),
                                        valueRestriction: this.Comment.ToString(),
                                        incidentProperty: prop,
                                        extantProperty: extantProp,
                                        layer: layer);
                                }
                            }
                            else
                            {
                                this.Comment = newComment;
                                this.commentPropertyLayer = layer;

                                if (this.commentValueConstraints != null)
                                {
                                    foreach (ValueConstraint valueConstraint in this.commentValueConstraints)
                                    {
                                        if (valueConstraint.RequiredLiteral != null && !valueConstraint.RequiredLiteral.Equals(newComment))
                                        {
                                            parsingErrorCollection.Notify(
                                                "notRequiredStringValue",
                                                elementId: this.Id,
                                                propertyName: "comment",
                                                propertyValue: newComment.ToString(),
                                                valueRestriction: valueConstraint.RequiredLiteral.ToString(),
                                                incidentProperty: prop,
                                                layer: layer);
                                        }
                                    }
                                }

                                ((Dictionary<string, string>)this.StringProperties)["comment"] = newComment;
                            }
                        }

                        continue;
                    case "description":
                    case "dtmi:dtdl:property:description;2":
                        if (descriptionProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "description",
                                incidentProperty: prop,
                                extantProperty: descriptionProperty,
                                layer: layer);
                        }
                        else
                        {
                            descriptionProperty = prop;
                            int? maxLength = 512;
                            Dictionary<string, string> newDescription = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "description", prop.Values, "en", maxLength, null, layer, parsingErrorCollection);
                            List<string> descriptionCodes = Helpers.GetKeysWithDifferingLiteralValues(this.Description, newDescription);
                            if (descriptionCodes.Any())
                            {
                                JsonLdProperty extantProp = Helpers.TryGetSingleUniqueValue(descriptionCodes.Select(c => this.descriptionPropertyLayer[c]), out string uniqueCodeLayer) && this.JsonLdElements[uniqueCodeLayer].Properties.Any(p => p.Name == "description") ? this.JsonLdElements[uniqueCodeLayer].Properties.First(p => p.Name == "description") : null;
                                parsingErrorCollection.Notify(
                                    "inconsistentLangStringValues",
                                    elementId: this.Id,
                                    propertyName: "description",
                                    langCode: string.Join(" and ", descriptionCodes.Select(c => $"'{c}'")),
                                    observedCount: descriptionCodes.Count,
                                    incidentProperty: prop,
                                    extantProperty: extantProp,
                                    layer: layer);
                            }
                            else if (this.Description.Any())
                            {
                                foreach (KeyValuePair<string, string> kvp in newDescription)
                                {
                                    ((Dictionary<string, string>)this.Description)[kvp.Key] = kvp.Value;
                                    this.descriptionPropertyLayer[kvp.Key] = layer;
                                }
                            }
                            else
                            {
                                this.Description = newDescription;
                                this.descriptionPropertyLayer = newDescription.ToDictionary(e => e.Key, e => layer);
                            }
                        }

                        continue;
                    case "displayName":
                    case "dtmi:dtdl:property:displayName;2":
                        if (displayNameProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "displayName",
                                incidentProperty: prop,
                                extantProperty: displayNameProperty,
                                layer: layer);
                        }
                        else
                        {
                            displayNameProperty = prop;
                            int? maxLength = 64;
                            Dictionary<string, string> newDisplayName = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "displayName", prop.Values, "en", maxLength, null, layer, parsingErrorCollection);
                            List<string> displayNameCodes = Helpers.GetKeysWithDifferingLiteralValues(this.DisplayName, newDisplayName);
                            if (displayNameCodes.Any())
                            {
                                JsonLdProperty extantProp = Helpers.TryGetSingleUniqueValue(displayNameCodes.Select(c => this.displayNamePropertyLayer[c]), out string uniqueCodeLayer) && this.JsonLdElements[uniqueCodeLayer].Properties.Any(p => p.Name == "displayName") ? this.JsonLdElements[uniqueCodeLayer].Properties.First(p => p.Name == "displayName") : null;
                                parsingErrorCollection.Notify(
                                    "inconsistentLangStringValues",
                                    elementId: this.Id,
                                    propertyName: "displayName",
                                    langCode: string.Join(" and ", displayNameCodes.Select(c => $"'{c}'")),
                                    observedCount: displayNameCodes.Count,
                                    incidentProperty: prop,
                                    extantProperty: extantProp,
                                    layer: layer);
                            }
                            else if (this.DisplayName.Any())
                            {
                                foreach (KeyValuePair<string, string> kvp in newDisplayName)
                                {
                                    ((Dictionary<string, string>)this.DisplayName)[kvp.Key] = kvp.Value;
                                    this.displayNamePropertyLayer[kvp.Key] = layer;
                                }
                            }
                            else
                            {
                                this.DisplayName = newDisplayName;
                                this.displayNamePropertyLayer = newDisplayName.ToDictionary(e => e.Key, e => layer);
                            }
                        }

                        continue;
                    case "name":
                    case "dtmi:dtdl:property:name;2":
                        if (nameProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "name",
                                incidentProperty: prop,
                                extantProperty: nameProperty,
                                layer: layer);
                        }
                        else
                        {
                            nameProperty = prop;
                            int? maxLength = 64;
                            string newName = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "name", prop.Values, maxLength, NamePropertyRegexPatternV2, layer, parsingErrorCollection, isOptional: false);
                            if (this.namePropertyLayer != null)
                            {
                                if (this.Name != newName)
                                {
                                    JsonLdProperty extantProp = this.JsonLdElements[this.namePropertyLayer].Properties.FirstOrDefault(p => p.Name == "name");
                                    parsingErrorCollection.Notify(
                                        "inconsistentStringValues",
                                        elementId: this.Id,
                                        propertyName: "name",
                                        propertyValue: newName.ToString(),
                                        valueRestriction: this.Name.ToString(),
                                        incidentProperty: prop,
                                        extantProperty: extantProp,
                                        layer: layer);
                                }
                            }
                            else
                            {
                                this.Name = newName;
                                this.namePropertyLayer = layer;

                                if (this.nameValueConstraints != null)
                                {
                                    foreach (ValueConstraint valueConstraint in this.nameValueConstraints)
                                    {
                                        if (valueConstraint.RequiredLiteral != null && !valueConstraint.RequiredLiteral.Equals(newName))
                                        {
                                            parsingErrorCollection.Notify(
                                                "notRequiredStringValue",
                                                elementId: this.Id,
                                                propertyName: "name",
                                                propertyValue: newName.ToString(),
                                                valueRestriction: valueConstraint.RequiredLiteral.ToString(),
                                                incidentProperty: prop,
                                                layer: layer);
                                        }
                                    }
                                }

                                ((Dictionary<string, string>)this.StringProperties)["name"] = newName;
                            }
                        }

                        continue;
                    case "request":
                    case "dtmi:dtdl:property:request;2":
                        if (requestProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "request",
                                incidentProperty: prop,
                                extantProperty: requestProperty,
                                layer: layer);
                        }
                        else
                        {
                            requestProperty = prop;
                            DTCommandPayloadInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.requestValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "request", null, null, 0, isPlural: false, idRequired: false, typeRequired: false, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.requestAllowedVersionsV2);
                        }

                        continue;
                    case "response":
                    case "dtmi:dtdl:property:response;2":
                        if (responseProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "response",
                                incidentProperty: prop,
                                extantProperty: responseProperty,
                                layer: layer);
                        }
                        else
                        {
                            responseProperty = prop;
                            DTCommandPayloadInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.responseValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "response", null, null, 0, isPlural: false, idRequired: false, typeRequired: false, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.responseAllowedVersionsV2);
                        }

                        continue;
                }

                if (this.TryParseSupplementalProperty(model, immediateSupplementalTypeIds, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, layer, definedIn, parsingErrorCollection, prop.Name, globalize, allowReservedIds, tolerateSolecisms, prop, supplementalJsonLdProperties))
                {
                    continue;
                }

                /* Irrelevant DTMI or term always valid as property */
                /* Invalid DTMI always valid as property            */
                /* Non-DTMI IRI always valid as property            */
                /* Undefined term always valid as property          */

                if (!immediateUndefinedTypes.Any())
                {
                    if (elt.Types != null)
                    {
                        parsingErrorCollection.Notify(
                            "noTypeThatAllows",
                            elementId: this.Id,
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                    }
                    else
                    {
                        parsingErrorCollection.Notify(
                            "inferredTypeDoesNotAllow",
                            elementId: this.Id,
                            referenceType: "Command",
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                    }

                    continue;
                }

                using (JsonDocument propDoc = JsonDocument.Parse(prop.Values.GetJsonText()))
                {
                    this.UndefinedPropertyDictionary[prop.Name] = propDoc.RootElement.Clone();
                }
            }

            if (nameProperty == null)
            {
                parsingErrorCollection.Notify(
                    "missingLiteralProperty",
                    elementId: this.Id,
                    propertyName: "name",
                    element: elt);
            }

            foreach (Dtmi supplementalTypeId in immediateSupplementalTypeIds)
            {
                if (aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
                {
                    supplementalTypeInfo.CheckForRequiredProperties(parsingErrorCollection, this.Id, elt, supplementalJsonLdProperties);
                }
            }
        }

        /// <summary>
        /// Parse the properties in a JSON Command object.
        /// </summary>
        /// <param name="model">Model to which to add object properties.</param>
        /// <param name="objectPropertyInfoList">List of object info structs for deferred assignments.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="immediateSupplementalTypeIds">A set of supplemental type IDs.</param>
        /// <param name="immediateUndefinedTypes">A list of undefind type strings.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="globalize">Treat all nested definitions as though defined globally.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="tolerateSolecisms">Tolerate specific minor invalidities to support backward compatibility.</param>
        internal override void ParsePropertiesV3(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool tolerateSolecisms)
        {
            this.LanguageMajorVersion = 3;

            JsonLdProperty commandTypeProperty = null;
            JsonLdProperty commentProperty = null;
            JsonLdProperty descriptionProperty = null;
            JsonLdProperty displayNameProperty = null;
            JsonLdProperty nameProperty = null;
            JsonLdProperty requestProperty = null;
            JsonLdProperty responseProperty = null;
            Dictionary<string, JsonLdProperty> supplementalJsonLdProperties = new Dictionary<string, JsonLdProperty>();

            foreach (JsonLdProperty prop in elt.Properties)
            {
                switch (prop.Name)
                {
                    case "commandType":
                    case "dtmi:dtdl:property:commandType;3":
                        if (commandTypeProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "commandType",
                                incidentProperty: prop,
                                extantProperty: commandTypeProperty,
                                layer: layer);
                        }
                        else
                        {
                            commandTypeProperty = prop;
                            DTCommandTypeInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.commandTypeValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "commandType", null, null, 0, isPlural: false, idRequired: true, typeRequired: true, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.commandTypeAllowedVersionsV3);
                        }

                        continue;
                    case "comment":
                    case "dtmi:dtdl:property:comment;3":
                        if (commentProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "comment",
                                incidentProperty: prop,
                                extantProperty: commentProperty,
                                layer: layer);
                        }
                        else
                        {
                            commentProperty = prop;
                            int? maxLength = 512;
                            string newComment = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "comment", prop.Values, maxLength, null, layer, parsingErrorCollection, isOptional: true);
                            if (this.commentPropertyLayer != null)
                            {
                                if (this.Comment != newComment)
                                {
                                    JsonLdProperty extantProp = this.JsonLdElements[this.commentPropertyLayer].Properties.FirstOrDefault(p => p.Name == "comment");
                                    parsingErrorCollection.Notify(
                                        "inconsistentStringValues",
                                        elementId: this.Id,
                                        propertyName: "comment",
                                        propertyValue: newComment.ToString(),
                                        valueRestriction: this.Comment.ToString(),
                                        incidentProperty: prop,
                                        extantProperty: extantProp,
                                        layer: layer);
                                }
                            }
                            else
                            {
                                this.Comment = newComment;
                                this.commentPropertyLayer = layer;

                                if (this.commentValueConstraints != null)
                                {
                                    foreach (ValueConstraint valueConstraint in this.commentValueConstraints)
                                    {
                                        if (valueConstraint.RequiredLiteral != null && !valueConstraint.RequiredLiteral.Equals(newComment))
                                        {
                                            parsingErrorCollection.Notify(
                                                "notRequiredStringValue",
                                                elementId: this.Id,
                                                propertyName: "comment",
                                                propertyValue: newComment.ToString(),
                                                valueRestriction: valueConstraint.RequiredLiteral.ToString(),
                                                incidentProperty: prop,
                                                layer: layer);
                                        }
                                    }
                                }

                                ((Dictionary<string, string>)this.StringProperties)["comment"] = newComment;
                            }
                        }

                        continue;
                    case "description":
                    case "dtmi:dtdl:property:description;3":
                        if (descriptionProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "description",
                                incidentProperty: prop,
                                extantProperty: descriptionProperty,
                                layer: layer);
                        }
                        else
                        {
                            descriptionProperty = prop;
                            int? maxLength = 512;
                            Dictionary<string, string> newDescription = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "description", prop.Values, "en", maxLength, null, layer, parsingErrorCollection);
                            List<string> descriptionCodes = Helpers.GetKeysWithDifferingLiteralValues(this.Description, newDescription);
                            if (descriptionCodes.Any())
                            {
                                JsonLdProperty extantProp = Helpers.TryGetSingleUniqueValue(descriptionCodes.Select(c => this.descriptionPropertyLayer[c]), out string uniqueCodeLayer) && this.JsonLdElements[uniqueCodeLayer].Properties.Any(p => p.Name == "description") ? this.JsonLdElements[uniqueCodeLayer].Properties.First(p => p.Name == "description") : null;
                                parsingErrorCollection.Notify(
                                    "inconsistentLangStringValues",
                                    elementId: this.Id,
                                    propertyName: "description",
                                    langCode: string.Join(" and ", descriptionCodes.Select(c => $"'{c}'")),
                                    observedCount: descriptionCodes.Count,
                                    incidentProperty: prop,
                                    extantProperty: extantProp,
                                    layer: layer);
                            }
                            else if (this.Description.Any())
                            {
                                foreach (KeyValuePair<string, string> kvp in newDescription)
                                {
                                    ((Dictionary<string, string>)this.Description)[kvp.Key] = kvp.Value;
                                    this.descriptionPropertyLayer[kvp.Key] = layer;
                                }
                            }
                            else
                            {
                                this.Description = newDescription;
                                this.descriptionPropertyLayer = newDescription.ToDictionary(e => e.Key, e => layer);
                            }
                        }

                        continue;
                    case "displayName":
                    case "dtmi:dtdl:property:displayName;3":
                        if (displayNameProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "displayName",
                                incidentProperty: prop,
                                extantProperty: displayNameProperty,
                                layer: layer);
                        }
                        else
                        {
                            displayNameProperty = prop;
                            int? maxLength = 512;
                            Dictionary<string, string> newDisplayName = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "displayName", prop.Values, "en", maxLength, null, layer, parsingErrorCollection);
                            List<string> displayNameCodes = Helpers.GetKeysWithDifferingLiteralValues(this.DisplayName, newDisplayName);
                            if (displayNameCodes.Any())
                            {
                                JsonLdProperty extantProp = Helpers.TryGetSingleUniqueValue(displayNameCodes.Select(c => this.displayNamePropertyLayer[c]), out string uniqueCodeLayer) && this.JsonLdElements[uniqueCodeLayer].Properties.Any(p => p.Name == "displayName") ? this.JsonLdElements[uniqueCodeLayer].Properties.First(p => p.Name == "displayName") : null;
                                parsingErrorCollection.Notify(
                                    "inconsistentLangStringValues",
                                    elementId: this.Id,
                                    propertyName: "displayName",
                                    langCode: string.Join(" and ", displayNameCodes.Select(c => $"'{c}'")),
                                    observedCount: displayNameCodes.Count,
                                    incidentProperty: prop,
                                    extantProperty: extantProp,
                                    layer: layer);
                            }
                            else if (this.DisplayName.Any())
                            {
                                foreach (KeyValuePair<string, string> kvp in newDisplayName)
                                {
                                    ((Dictionary<string, string>)this.DisplayName)[kvp.Key] = kvp.Value;
                                    this.displayNamePropertyLayer[kvp.Key] = layer;
                                }
                            }
                            else
                            {
                                this.DisplayName = newDisplayName;
                                this.displayNamePropertyLayer = newDisplayName.ToDictionary(e => e.Key, e => layer);
                            }
                        }

                        continue;
                    case "name":
                    case "dtmi:dtdl:property:name;3":
                        if (nameProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "name",
                                incidentProperty: prop,
                                extantProperty: nameProperty,
                                layer: layer);
                        }
                        else
                        {
                            nameProperty = prop;
                            int? maxLength = 512;
                            string newName = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "name", prop.Values, maxLength, NamePropertyRegexPatternV3, layer, parsingErrorCollection, isOptional: false);
                            if (this.namePropertyLayer != null)
                            {
                                if (this.Name != newName)
                                {
                                    JsonLdProperty extantProp = this.JsonLdElements[this.namePropertyLayer].Properties.FirstOrDefault(p => p.Name == "name");
                                    parsingErrorCollection.Notify(
                                        "inconsistentStringValues",
                                        elementId: this.Id,
                                        propertyName: "name",
                                        propertyValue: newName.ToString(),
                                        valueRestriction: this.Name.ToString(),
                                        incidentProperty: prop,
                                        extantProperty: extantProp,
                                        layer: layer);
                                }
                            }
                            else
                            {
                                this.Name = newName;
                                this.namePropertyLayer = layer;

                                if (this.nameValueConstraints != null)
                                {
                                    foreach (ValueConstraint valueConstraint in this.nameValueConstraints)
                                    {
                                        if (valueConstraint.RequiredLiteral != null && !valueConstraint.RequiredLiteral.Equals(newName))
                                        {
                                            parsingErrorCollection.Notify(
                                                "notRequiredStringValue",
                                                elementId: this.Id,
                                                propertyName: "name",
                                                propertyValue: newName.ToString(),
                                                valueRestriction: valueConstraint.RequiredLiteral.ToString(),
                                                incidentProperty: prop,
                                                layer: layer);
                                        }
                                    }
                                }

                                ((Dictionary<string, string>)this.StringProperties)["name"] = newName;
                            }
                        }

                        continue;
                    case "request":
                    case "dtmi:dtdl:property:request;3":
                        if (requestProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "request",
                                incidentProperty: prop,
                                extantProperty: requestProperty,
                                layer: layer);
                        }
                        else
                        {
                            requestProperty = prop;
                            DTCommandRequestInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.requestValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "request", null, null, 0, isPlural: false, idRequired: false, typeRequired: false, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.requestAllowedVersionsV3);
                        }

                        continue;
                    case "response":
                    case "dtmi:dtdl:property:response;3":
                        if (responseProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "response",
                                incidentProperty: prop,
                                extantProperty: responseProperty,
                                layer: layer);
                        }
                        else
                        {
                            responseProperty = prop;
                            DTCommandResponseInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.responseValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "response", null, null, 0, isPlural: false, idRequired: false, typeRequired: false, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.responseAllowedVersionsV3);
                        }

                        continue;
                }

                if (this.TryParseSupplementalProperty(model, immediateSupplementalTypeIds, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, layer, definedIn, parsingErrorCollection, prop.Name, globalize, allowReservedIds, tolerateSolecisms, prop, supplementalJsonLdProperties))
                {
                    continue;
                }

                if (aggregateContext.TryCreateDtmi(prop.Name, out Dtmi _))
                {
                    if (aggregateContext.IsComplete)
                    {
                        parsingErrorCollection.Notify(
                            "propertyIrrelevantDtmiOrTerm",
                            elementId: this.Id,
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                        continue;
                    }
                }
                else if (prop.Name.StartsWith("dtmi:"))
                {
                    parsingErrorCollection.Notify(
                        "propertyInvalidDtmi",
                        elementId: this.Id,
                        propertyName: prop.Name,
                        incidentProperty: prop,
                        element: elt,
                        layer: layer);
                    continue;
                }
                else if (prop.Name.Contains(":"))
                {
                    parsingErrorCollection.Notify(
                        "propertyNotDtmiNorTerm",
                        elementId: this.Id,
                        propertyName: prop.Name,
                        incidentProperty: prop,
                        element: elt,
                        layer: layer);
                    continue;
                }
                else
                {
                    if (aggregateContext.IsComplete)
                    {
                        parsingErrorCollection.Notify(
                            "propertyUndefinedTerm",
                            elementId: this.Id,
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            version: aggregateContext.DtdlVersion.ToString(),
                            layer: layer);
                        continue;
                    }
                }

                if (!immediateUndefinedTypes.Any())
                {
                    if (elt.Types != null)
                    {
                        parsingErrorCollection.Notify(
                            "noTypeThatAllows",
                            elementId: this.Id,
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                    }
                    else
                    {
                        parsingErrorCollection.Notify(
                            "inferredTypeDoesNotAllow",
                            elementId: this.Id,
                            referenceType: "Command",
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                    }

                    continue;
                }

                using (JsonDocument propDoc = JsonDocument.Parse(prop.Values.GetJsonText()))
                {
                    this.UndefinedPropertyDictionary[prop.Name] = propDoc.RootElement.Clone();
                }
            }

            if (nameProperty == null)
            {
                parsingErrorCollection.Notify(
                    "missingLiteralProperty",
                    elementId: this.Id,
                    propertyName: "name",
                    element: elt);
            }

            foreach (Dtmi supplementalTypeId in immediateSupplementalTypeIds)
            {
                if (aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
                {
                    supplementalTypeInfo.CheckForRequiredProperties(parsingErrorCollection, this.Id, elt, supplementalJsonLdProperties);
                }
            }
        }

        /// <summary>
        /// Parse the properties in a JSON Command object.
        /// </summary>
        /// <param name="model">Model to which to add object properties.</param>
        /// <param name="objectPropertyInfoList">List of object info structs for deferred assignments.</param>
        /// <param name="elementPropertyConstraints">List of <c>ElementPropertyConstraint</c> to be checked after object property assignment.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="immediateSupplementalTypeIds">A set of supplemental type IDs.</param>
        /// <param name="immediateUndefinedTypes">A list of undefind type strings.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="definedIn">Identifier of the partition or top-level element under which this element is defined.</param>
        /// <param name="globalize">Treat all nested definitions as though defined globally.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="tolerateSolecisms">Tolerate specific minor invalidities to support backward compatibility.</param>
        internal override void ParsePropertiesV4(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool tolerateSolecisms)
        {
            this.LanguageMajorVersion = 4;

            JsonLdProperty commandTypeProperty = null;
            JsonLdProperty commentProperty = null;
            JsonLdProperty descriptionProperty = null;
            JsonLdProperty displayNameProperty = null;
            JsonLdProperty nameProperty = null;
            JsonLdProperty requestProperty = null;
            JsonLdProperty responseProperty = null;
            Dictionary<string, JsonLdProperty> supplementalJsonLdProperties = new Dictionary<string, JsonLdProperty>();

            foreach (JsonLdProperty prop in elt.Properties)
            {
                switch (prop.Name)
                {
                    case "commandType":
                    case "dtmi:dtdl:property:commandType;4":
                        if (commandTypeProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "commandType",
                                incidentProperty: prop,
                                extantProperty: commandTypeProperty,
                                layer: layer);
                        }
                        else
                        {
                            commandTypeProperty = prop;
                            DTCommandTypeInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.commandTypeValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "commandType", null, null, 0, isPlural: false, idRequired: true, typeRequired: true, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.commandTypeAllowedVersionsV4);
                        }

                        continue;
                    case "comment":
                    case "dtmi:dtdl:property:comment;4":
                        if (commentProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "comment",
                                incidentProperty: prop,
                                extantProperty: commentProperty,
                                layer: layer);
                        }
                        else
                        {
                            commentProperty = prop;
                            int? maxLength = aggregateContext.LimitSpecifier switch
                            {
                                "" => 512,
                                "onvif_1" => 515,
                                _ => null,
                            };

                            string newComment = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "comment", prop.Values, maxLength, null, layer, parsingErrorCollection, isOptional: true);
                            if (this.commentPropertyLayer != null)
                            {
                                if (this.Comment != newComment)
                                {
                                    JsonLdProperty extantProp = this.JsonLdElements[this.commentPropertyLayer].Properties.FirstOrDefault(p => p.Name == "comment");
                                    parsingErrorCollection.Notify(
                                        "inconsistentStringValues",
                                        elementId: this.Id,
                                        propertyName: "comment",
                                        propertyValue: newComment.ToString(),
                                        valueRestriction: this.Comment.ToString(),
                                        incidentProperty: prop,
                                        extantProperty: extantProp,
                                        layer: layer);
                                }
                            }
                            else
                            {
                                this.Comment = newComment;
                                this.commentPropertyLayer = layer;

                                if (this.commentValueConstraints != null)
                                {
                                    foreach (ValueConstraint valueConstraint in this.commentValueConstraints)
                                    {
                                        if (valueConstraint.RequiredLiteral != null && !valueConstraint.RequiredLiteral.Equals(newComment))
                                        {
                                            parsingErrorCollection.Notify(
                                                "notRequiredStringValue",
                                                elementId: this.Id,
                                                propertyName: "comment",
                                                propertyValue: newComment.ToString(),
                                                valueRestriction: valueConstraint.RequiredLiteral.ToString(),
                                                incidentProperty: prop,
                                                layer: layer);
                                        }
                                    }
                                }

                                ((Dictionary<string, string>)this.StringProperties)["comment"] = newComment;
                            }
                        }

                        continue;
                    case "description":
                    case "dtmi:dtdl:property:description;4":
                        if (descriptionProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "description",
                                incidentProperty: prop,
                                extantProperty: descriptionProperty,
                                layer: layer);
                        }
                        else
                        {
                            descriptionProperty = prop;
                            int? maxLength = aggregateContext.LimitSpecifier switch
                            {
                                "" => 512,
                                "onvif_1" => 514,
                                _ => null,
                            };

                            Dictionary<string, string> newDescription = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "description", prop.Values, "en", maxLength, null, layer, parsingErrorCollection);
                            List<string> descriptionCodes = Helpers.GetKeysWithDifferingLiteralValues(this.Description, newDescription);
                            if (descriptionCodes.Any())
                            {
                                JsonLdProperty extantProp = Helpers.TryGetSingleUniqueValue(descriptionCodes.Select(c => this.descriptionPropertyLayer[c]), out string uniqueCodeLayer) && this.JsonLdElements[uniqueCodeLayer].Properties.Any(p => p.Name == "description") ? this.JsonLdElements[uniqueCodeLayer].Properties.First(p => p.Name == "description") : null;
                                parsingErrorCollection.Notify(
                                    "inconsistentLangStringValues",
                                    elementId: this.Id,
                                    propertyName: "description",
                                    langCode: string.Join(" and ", descriptionCodes.Select(c => $"'{c}'")),
                                    observedCount: descriptionCodes.Count,
                                    incidentProperty: prop,
                                    extantProperty: extantProp,
                                    layer: layer);
                            }
                            else if (this.Description.Any())
                            {
                                foreach (KeyValuePair<string, string> kvp in newDescription)
                                {
                                    ((Dictionary<string, string>)this.Description)[kvp.Key] = kvp.Value;
                                    this.descriptionPropertyLayer[kvp.Key] = layer;
                                }
                            }
                            else
                            {
                                this.Description = newDescription;
                                this.descriptionPropertyLayer = newDescription.ToDictionary(e => e.Key, e => layer);
                            }
                        }

                        continue;
                    case "displayName":
                    case "dtmi:dtdl:property:displayName;4":
                        if (displayNameProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "displayName",
                                incidentProperty: prop,
                                extantProperty: displayNameProperty,
                                layer: layer);
                        }
                        else
                        {
                            displayNameProperty = prop;
                            int? maxLength = aggregateContext.LimitSpecifier switch
                            {
                                "" => 512,
                                "onvif_1" => 513,
                                _ => null,
                            };

                            Dictionary<string, string> newDisplayName = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "displayName", prop.Values, "en", maxLength, null, layer, parsingErrorCollection);
                            List<string> displayNameCodes = Helpers.GetKeysWithDifferingLiteralValues(this.DisplayName, newDisplayName);
                            if (displayNameCodes.Any())
                            {
                                JsonLdProperty extantProp = Helpers.TryGetSingleUniqueValue(displayNameCodes.Select(c => this.displayNamePropertyLayer[c]), out string uniqueCodeLayer) && this.JsonLdElements[uniqueCodeLayer].Properties.Any(p => p.Name == "displayName") ? this.JsonLdElements[uniqueCodeLayer].Properties.First(p => p.Name == "displayName") : null;
                                parsingErrorCollection.Notify(
                                    "inconsistentLangStringValues",
                                    elementId: this.Id,
                                    propertyName: "displayName",
                                    langCode: string.Join(" and ", displayNameCodes.Select(c => $"'{c}'")),
                                    observedCount: displayNameCodes.Count,
                                    incidentProperty: prop,
                                    extantProperty: extantProp,
                                    layer: layer);
                            }
                            else if (this.DisplayName.Any())
                            {
                                foreach (KeyValuePair<string, string> kvp in newDisplayName)
                                {
                                    ((Dictionary<string, string>)this.DisplayName)[kvp.Key] = kvp.Value;
                                    this.displayNamePropertyLayer[kvp.Key] = layer;
                                }
                            }
                            else
                            {
                                this.DisplayName = newDisplayName;
                                this.displayNamePropertyLayer = newDisplayName.ToDictionary(e => e.Key, e => layer);
                            }
                        }

                        continue;
                    case "name":
                    case "dtmi:dtdl:property:name;4":
                        if (nameProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "name",
                                incidentProperty: prop,
                                extantProperty: nameProperty,
                                layer: layer);
                        }
                        else
                        {
                            nameProperty = prop;
                            int? maxLength = 512;
                            string newName = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "name", prop.Values, maxLength, NamePropertyRegexPatternV4, layer, parsingErrorCollection, isOptional: false);
                            if (this.namePropertyLayer != null)
                            {
                                if (this.Name != newName)
                                {
                                    JsonLdProperty extantProp = this.JsonLdElements[this.namePropertyLayer].Properties.FirstOrDefault(p => p.Name == "name");
                                    parsingErrorCollection.Notify(
                                        "inconsistentStringValues",
                                        elementId: this.Id,
                                        propertyName: "name",
                                        propertyValue: newName.ToString(),
                                        valueRestriction: this.Name.ToString(),
                                        incidentProperty: prop,
                                        extantProperty: extantProp,
                                        layer: layer);
                                }
                            }
                            else
                            {
                                this.Name = newName;
                                this.namePropertyLayer = layer;

                                if (this.nameValueConstraints != null)
                                {
                                    foreach (ValueConstraint valueConstraint in this.nameValueConstraints)
                                    {
                                        if (valueConstraint.RequiredLiteral != null && !valueConstraint.RequiredLiteral.Equals(newName))
                                        {
                                            parsingErrorCollection.Notify(
                                                "notRequiredStringValue",
                                                elementId: this.Id,
                                                propertyName: "name",
                                                propertyValue: newName.ToString(),
                                                valueRestriction: valueConstraint.RequiredLiteral.ToString(),
                                                incidentProperty: prop,
                                                layer: layer);
                                        }
                                    }
                                }

                                ((Dictionary<string, string>)this.StringProperties)["name"] = newName;
                            }
                        }

                        continue;
                    case "request":
                    case "dtmi:dtdl:property:request;4":
                        if (requestProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "request",
                                incidentProperty: prop,
                                extantProperty: requestProperty,
                                layer: layer);
                        }
                        else
                        {
                            requestProperty = prop;
                            DTCommandRequestInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.requestValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "request", null, null, 0, isPlural: false, idRequired: false, typeRequired: false, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.requestAllowedVersionsV4);
                        }

                        continue;
                    case "response":
                    case "dtmi:dtdl:property:response;4":
                        if (responseProperty != null)
                        {
                            parsingErrorCollection.Notify(
                                "duplicatePropertyName",
                                elementId: this.Id,
                                propertyName: "response",
                                incidentProperty: prop,
                                extantProperty: responseProperty,
                                layer: layer);
                        }
                        else
                        {
                            responseProperty = prop;
                            DTCommandResponseInfo.ParseValueCollection(model, objectPropertyInfoList, elementPropertyConstraints, this.responseValueConstraints, aggregateContext, parsingErrorCollection, prop, layer, this.Id, globalize ? null : definedIn ?? this.Id, "response", null, null, 0, isPlural: false, idRequired: false, typeRequired: false, globalize: globalize, allowReservedIds: allowReservedIds, tolerateSolecisms: tolerateSolecisms, allowedVersions: this.responseAllowedVersionsV4);
                        }

                        continue;
                }

                if (this.TryParseSupplementalProperty(model, immediateSupplementalTypeIds, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, layer, definedIn, parsingErrorCollection, prop.Name, globalize, allowReservedIds, tolerateSolecisms, prop, supplementalJsonLdProperties))
                {
                    continue;
                }

                if (aggregateContext.TryCreateDtmi(prop.Name, out Dtmi _))
                {
                    if (aggregateContext.IsComplete)
                    {
                        parsingErrorCollection.Notify(
                            "propertyIrrelevantDtmiOrTerm",
                            elementId: this.Id,
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                        continue;
                    }
                }
                else if (prop.Name.StartsWith("dtmi:"))
                {
                    parsingErrorCollection.Notify(
                        "propertyInvalidDtmi",
                        elementId: this.Id,
                        propertyName: prop.Name,
                        incidentProperty: prop,
                        element: elt,
                        layer: layer);
                    continue;
                }
                else if (prop.Name.Contains(":"))
                {
                    parsingErrorCollection.Notify(
                        "propertyNotDtmiNorTerm",
                        elementId: this.Id,
                        propertyName: prop.Name,
                        incidentProperty: prop,
                        element: elt,
                        layer: layer);
                    continue;
                }
                else
                {
                    if (aggregateContext.IsComplete)
                    {
                        parsingErrorCollection.Notify(
                            "propertyUndefinedTerm",
                            elementId: this.Id,
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            version: aggregateContext.DtdlVersion.ToString(),
                            layer: layer);
                        continue;
                    }
                }

                if (!immediateUndefinedTypes.Any())
                {
                    if (elt.Types != null)
                    {
                        parsingErrorCollection.Notify(
                            "noTypeThatAllows",
                            elementId: this.Id,
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                    }
                    else
                    {
                        parsingErrorCollection.Notify(
                            "inferredTypeDoesNotAllow",
                            elementId: this.Id,
                            referenceType: "Command",
                            propertyName: prop.Name,
                            incidentProperty: prop,
                            element: elt,
                            layer: layer);
                    }

                    continue;
                }

                using (JsonDocument propDoc = JsonDocument.Parse(prop.Values.GetJsonText()))
                {
                    this.UndefinedPropertyDictionary[prop.Name] = propDoc.RootElement.Clone();
                }
            }

            if (nameProperty == null)
            {
                parsingErrorCollection.Notify(
                    "missingLiteralProperty",
                    elementId: this.Id,
                    propertyName: "name",
                    element: elt);
            }

            foreach (Dtmi supplementalTypeId in immediateSupplementalTypeIds)
            {
                if (aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
                {
                    supplementalTypeInfo.CheckForRequiredProperties(parsingErrorCollection, this.Id, elt, supplementalJsonLdProperties);
                }
            }
        }

        /// <summary>
        /// Record JSON-LD source as appropriate and check length of text against limit.
        /// </summary>
        /// <param name="layer">The name of the layer whose source to record.</param>
        /// <param name="elt">A <c>JsonLdElement</c> containing the JSON-LD source of the element.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <param name="atRoot">True if the element is at the document root.</param>
        /// <param name="globalized">True if the element has been globalized.</param>
        internal override void RecordSourceAsAppropriate(string layer, JsonLdElement elt, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, bool atRoot, bool globalized)
        {
        }

        /// <summary>
        /// Write a JSON representation of the DTDL element represented by an object of this class.
        /// </summary>
        /// <param name="jsonWriter">A <c>Utf8JsonWriter</c> object with which to write the JSON representation.</param>
        /// <param name="includeClassId">True if the mothed should add a ClassId property to the JSON object.</param>
        internal override void WriteToJson(Utf8JsonWriter jsonWriter, bool includeClassId)
        {
            base.WriteToJson(jsonWriter, includeClassId: false);

            if (includeClassId)
            {
                jsonWriter.WriteString("ClassId", $"dtmi:dtdl:class:Command;{this.LanguageMajorVersion}");
            }

            jsonWriter.WriteString("commandType", this.CommandType?.Id?.ToString());

            jsonWriter.WriteString("request", this.Request?.Id?.ToString());

            jsonWriter.WriteString("response", this.Response?.Id?.ToString());
        }

        private bool TryParseSupplementalProperty(Model model, HashSet<Dtmi> immediateSupplementalTypeIds, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, string layer, Dtmi definedIn, ParsingErrorCollection parsingErrorCollection, string propName, bool globalize, bool allowReservedIds, bool tolerateSolecisms, JsonLdProperty valueCollectionProp, Dictionary<string, JsonLdProperty> supplementalJsonLdProperties)
        {
            if (!aggregateContext.TryCreateDtmi(propName, out Dtmi propDtmi))
            {
                return false;
            }

            foreach (Dtmi supplementalTypeId in immediateSupplementalTypeIds)
            {
                if (aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo) && supplementalTypeInfo.TryParseProperty(model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, layer, this.Id, definedIn, propDtmi.ToString(), globalize, allowReservedIds, tolerateSolecisms, valueCollectionProp, ref this.supplementalProperties, supplementalJsonLdProperties, this.supplementalSingularPropertyLayers, this.JsonLdElements))
                {
                    return true;
                }
            }

            return false;
        }

        private void ApplyTransformationsV2(Model model, ParsingErrorCollection parsingErrorCollection)
        {
        }

        private void ApplyTransformationsV3(Model model, ParsingErrorCollection parsingErrorCollection)
        {
        }

        private void ApplyTransformationsV4(Model model, ParsingErrorCollection parsingErrorCollection)
        {
        }

        /// <inheritdoc/>
        private void CheckRestrictionsV2(ParsingErrorCollection parsingErrorCollection)
        {
            if (this.CommandType != null && this.CommandType.Id.AbsoluteUri != "dtmi:dtdl:instance:CommandType:asynchronous;2" && this.CommandType.Id.AbsoluteUri != "dtmi:dtdl:instance:CommandType:synchronous;2")
            {
                JsonLdProperty propProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "commandType")).Value?.Properties?.First();
                parsingErrorCollection.Notify(
                    "incorrectPropertyValue",
                    elementId: this.Id,
                    propertyName: "commandType",
                    propertyValue: ContextCollection.GetTermOrUri(this.CommandType.Id),
                    valueRestriction: "'asynchronous' or 'synchronous'",
                    incidentProperty: propProp);
            }

            if (this.commandTypeInstanceProperties != null)
            {
                foreach (string instanceProp in this.commandTypeInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.CommandType.ValidateInstance(jsonElement) : this.CommandType.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "commandType")).Value?.Properties?.First(p => p.Name == "commandType");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "commandType",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }

            if (this.requestInstanceProperties != null)
            {
                foreach (string instanceProp in this.requestInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.Request.ValidateInstance(jsonElement) : this.Request.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "request")).Value?.Properties?.First(p => p.Name == "request");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "request",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }

            if (this.responseInstanceProperties != null)
            {
                foreach (string instanceProp in this.responseInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.Response.ValidateInstance(jsonElement) : this.Response.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "response")).Value?.Properties?.First(p => p.Name == "response");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "response",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }
        }

        /// <inheritdoc/>
        private void CheckRestrictionsV3(ParsingErrorCollection parsingErrorCollection)
        {
            if (this.CommandType != null && this.CommandType.Id.AbsoluteUri != "dtmi:dtdl:instance:CommandType:asynchronous;3" && this.CommandType.Id.AbsoluteUri != "dtmi:dtdl:instance:CommandType:synchronous;3")
            {
                JsonLdProperty propProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "commandType")).Value?.Properties?.First();
                parsingErrorCollection.Notify(
                    "incorrectPropertyValue",
                    elementId: this.Id,
                    propertyName: "commandType",
                    propertyValue: ContextCollection.GetTermOrUri(this.CommandType.Id),
                    valueRestriction: "'asynchronous' or 'synchronous'",
                    incidentProperty: propProp);
            }

            if (this.commandTypeInstanceProperties != null)
            {
                foreach (string instanceProp in this.commandTypeInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.CommandType.ValidateInstance(jsonElement) : this.CommandType.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "commandType")).Value?.Properties?.First(p => p.Name == "commandType");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "commandType",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }

            if (this.requestInstanceProperties != null)
            {
                foreach (string instanceProp in this.requestInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.Request.ValidateInstance(jsonElement) : this.Request.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "request")).Value?.Properties?.First(p => p.Name == "request");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "request",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }

            if (this.responseInstanceProperties != null)
            {
                foreach (string instanceProp in this.responseInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.Response.ValidateInstance(jsonElement) : this.Response.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "response")).Value?.Properties?.First(p => p.Name == "response");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "response",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }
        }

        /// <inheritdoc/>
        private void CheckRestrictionsV4(ParsingErrorCollection parsingErrorCollection)
        {
            if (this.CommandType != null && this.CommandType.Id.AbsoluteUri != "dtmi:dtdl:instance:CommandType:asynchronous;4" && this.CommandType.Id.AbsoluteUri != "dtmi:dtdl:instance:CommandType:synchronous;4")
            {
                JsonLdProperty propProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "commandType")).Value?.Properties?.First();
                parsingErrorCollection.Notify(
                    "incorrectPropertyValue",
                    elementId: this.Id,
                    propertyName: "commandType",
                    propertyValue: ContextCollection.GetTermOrUri(this.CommandType.Id),
                    valueRestriction: "'asynchronous' or 'synchronous'",
                    incidentProperty: propProp);
            }

            if (this.commandTypeInstanceProperties != null)
            {
                foreach (string instanceProp in this.commandTypeInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.CommandType.ValidateInstance(jsonElement) : this.CommandType.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "commandType")).Value?.Properties?.First(p => p.Name == "commandType");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "commandType",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }

            if (this.requestInstanceProperties != null)
            {
                foreach (string instanceProp in this.requestInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.Request.ValidateInstance(jsonElement) : this.Request.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "request")).Value?.Properties?.First(p => p.Name == "request");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "request",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }

            if (this.responseInstanceProperties != null)
            {
                foreach (string instanceProp in this.responseInstanceProperties)
                {
                    object supplementalProperty = this.supplementalProperties[instanceProp];
                    IReadOnlyCollection<string> violations = supplementalProperty is JsonElement jsonElement ? this.Response.ValidateInstance(jsonElement) : this.Response.ValidateInstance(supplementalProperty.ToString());
                    if (violations.Any())
                    {
                        string violationPostscript = violations.Count > 1 ? $", plus {violations.Count - 1} other violation(s)" : string.Empty;
                        string instanceTerm = ContextCollection.GetTermOrUri(new Uri(instanceProp));
                        JsonLdProperty instancePropProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == instanceProp || p.Name == instanceTerm)).Value?.Properties?.First(p => p.Name == instanceProp || p.Name == instanceTerm);
                        JsonLdProperty conformanceProp = this.JsonLdElements.FirstOrDefault(e => e.Value.Properties.Any(p => p.Name == "response")).Value?.Properties?.First(p => p.Name == "response");

                        parsingErrorCollection.Notify(
                            "nonConformantPropertyValue",
                            elementId: this.Id,
                            propertyName: instanceTerm,
                            governingPropertyName: "response",
                            violations: violations,
                            violationCount: violations.Count,
                            incidentValues: instancePropProp.Values,
                            governingValues: conformanceProp.Values);
                    }
                }
            }
        }
    }
}
