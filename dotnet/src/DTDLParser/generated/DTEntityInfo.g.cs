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
    /// Class <c>DTEntityInfo</c> corresponds to an element of type Entity in a DTDL model.
    /// </summary>
    /// <remarks>
    /// This is the base class for all classes that correspond to elements in DTDL models.
    /// </remarks>
    public abstract class DTEntityInfo : ITypeChecker, IEquatable<DTEntityInfo>
    {
        private static readonly Dictionary<int, HashSet<DTEntityKind>> ConcreteKinds = new Dictionary<int, HashSet<DTEntityKind>>();

        private static readonly Dictionary<int, string> BadTypeActionFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeCauseFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeLocatedCauseFormat = new Dictionary<int, string>();

        private static readonly HashSet<string> PropertyNames = new HashSet<string>();

        private static readonly HashSet<string> VersionlessTypes = new HashSet<string>();

        private Dictionary<string, string> descriptionPropertyLayer = null;

        private Dictionary<string, string> displayNamePropertyLayer = null;

        private string commentPropertyLayer = null;

        static DTEntityInfo()
        {
            VersionlessTypes.Add("dtmi:dtdl:class:Entity");

            PropertyNames.Add("comment");
            PropertyNames.Add("description");
            PropertyNames.Add("displayName");
            PropertyNames.Add("languageMajorVersion");

            ConcreteKinds[2] = new HashSet<DTEntityKind>();
            ConcreteKinds[2].Add(DTEntityKind.Array);
            ConcreteKinds[2].Add(DTEntityKind.Boolean);
            ConcreteKinds[2].Add(DTEntityKind.Command);
            ConcreteKinds[2].Add(DTEntityKind.CommandPayload);
            ConcreteKinds[2].Add(DTEntityKind.CommandType);
            ConcreteKinds[2].Add(DTEntityKind.Component);
            ConcreteKinds[2].Add(DTEntityKind.Date);
            ConcreteKinds[2].Add(DTEntityKind.DateTime);
            ConcreteKinds[2].Add(DTEntityKind.Double);
            ConcreteKinds[2].Add(DTEntityKind.Duration);
            ConcreteKinds[2].Add(DTEntityKind.Enum);
            ConcreteKinds[2].Add(DTEntityKind.EnumValue);
            ConcreteKinds[2].Add(DTEntityKind.Field);
            ConcreteKinds[2].Add(DTEntityKind.Float);
            ConcreteKinds[2].Add(DTEntityKind.Integer);
            ConcreteKinds[2].Add(DTEntityKind.Interface);
            ConcreteKinds[2].Add(DTEntityKind.Long);
            ConcreteKinds[2].Add(DTEntityKind.Map);
            ConcreteKinds[2].Add(DTEntityKind.MapKey);
            ConcreteKinds[2].Add(DTEntityKind.MapValue);
            ConcreteKinds[2].Add(DTEntityKind.Object);
            ConcreteKinds[2].Add(DTEntityKind.Property);
            ConcreteKinds[2].Add(DTEntityKind.Relationship);
            ConcreteKinds[2].Add(DTEntityKind.String);
            ConcreteKinds[2].Add(DTEntityKind.Telemetry);
            ConcreteKinds[2].Add(DTEntityKind.Time);

            ConcreteKinds[3] = new HashSet<DTEntityKind>();
            ConcreteKinds[3].Add(DTEntityKind.Array);
            ConcreteKinds[3].Add(DTEntityKind.Boolean);
            ConcreteKinds[3].Add(DTEntityKind.Command);
            ConcreteKinds[3].Add(DTEntityKind.CommandRequest);
            ConcreteKinds[3].Add(DTEntityKind.CommandResponse);
            ConcreteKinds[3].Add(DTEntityKind.CommandType);
            ConcreteKinds[3].Add(DTEntityKind.Component);
            ConcreteKinds[3].Add(DTEntityKind.Date);
            ConcreteKinds[3].Add(DTEntityKind.DateTime);
            ConcreteKinds[3].Add(DTEntityKind.Double);
            ConcreteKinds[3].Add(DTEntityKind.Duration);
            ConcreteKinds[3].Add(DTEntityKind.Enum);
            ConcreteKinds[3].Add(DTEntityKind.EnumValue);
            ConcreteKinds[3].Add(DTEntityKind.Field);
            ConcreteKinds[3].Add(DTEntityKind.Float);
            ConcreteKinds[3].Add(DTEntityKind.Integer);
            ConcreteKinds[3].Add(DTEntityKind.Interface);
            ConcreteKinds[3].Add(DTEntityKind.Long);
            ConcreteKinds[3].Add(DTEntityKind.Map);
            ConcreteKinds[3].Add(DTEntityKind.MapKey);
            ConcreteKinds[3].Add(DTEntityKind.MapValue);
            ConcreteKinds[3].Add(DTEntityKind.Object);
            ConcreteKinds[3].Add(DTEntityKind.Property);
            ConcreteKinds[3].Add(DTEntityKind.Relationship);
            ConcreteKinds[3].Add(DTEntityKind.String);
            ConcreteKinds[3].Add(DTEntityKind.Telemetry);
            ConcreteKinds[3].Add(DTEntityKind.Time);

            ConcreteKinds[4] = new HashSet<DTEntityKind>();
            ConcreteKinds[4].Add(DTEntityKind.Array);
            ConcreteKinds[4].Add(DTEntityKind.Boolean);
            ConcreteKinds[4].Add(DTEntityKind.Command);
            ConcreteKinds[4].Add(DTEntityKind.CommandRequest);
            ConcreteKinds[4].Add(DTEntityKind.CommandResponse);
            ConcreteKinds[4].Add(DTEntityKind.CommandType);
            ConcreteKinds[4].Add(DTEntityKind.Component);
            ConcreteKinds[4].Add(DTEntityKind.Date);
            ConcreteKinds[4].Add(DTEntityKind.DateTime);
            ConcreteKinds[4].Add(DTEntityKind.Double);
            ConcreteKinds[4].Add(DTEntityKind.Duration);
            ConcreteKinds[4].Add(DTEntityKind.Enum);
            ConcreteKinds[4].Add(DTEntityKind.EnumValue);
            ConcreteKinds[4].Add(DTEntityKind.Field);
            ConcreteKinds[4].Add(DTEntityKind.Float);
            ConcreteKinds[4].Add(DTEntityKind.Integer);
            ConcreteKinds[4].Add(DTEntityKind.Interface);
            ConcreteKinds[4].Add(DTEntityKind.Long);
            ConcreteKinds[4].Add(DTEntityKind.Map);
            ConcreteKinds[4].Add(DTEntityKind.MapKey);
            ConcreteKinds[4].Add(DTEntityKind.MapValue);
            ConcreteKinds[4].Add(DTEntityKind.Object);
            ConcreteKinds[4].Add(DTEntityKind.Property);
            ConcreteKinds[4].Add(DTEntityKind.Relationship);
            ConcreteKinds[4].Add(DTEntityKind.String);
            ConcreteKinds[4].Add(DTEntityKind.Telemetry);
            ConcreteKinds[4].Add(DTEntityKind.Time);

            BadTypeActionFormat[2] = "Provide a @type{line3} in the set of allowable types.";
            BadTypeActionFormat[3] = "Provide a @type{line3} in the set of allowable types.";
            BadTypeActionFormat[4] = "Provide a @type{line3} in the set of allowable types.";

            BadTypeCauseFormat[2] = "Top-level element{secondaryId:e} does not have @type of Array, Command, CommandPayload, Component, Enum, EnumValue, Field, Interface, Map, MapKey, MapValue, Object, Property, Relationship, or Telemetry.";
            BadTypeCauseFormat[3] = "Top-level element{secondaryId:e} does not have @type of Array, Command, CommandRequest, CommandResponse, Component, Enum, EnumValue, Field, Interface, Map, MapKey, MapValue, Object, Property, Relationship, or Telemetry.";
            BadTypeCauseFormat[4] = "Top-level element{secondaryId:e} does not have @type of Array, Command, CommandRequest, CommandResponse, Component, Enum, EnumValue, Field, Interface, Map, MapKey, MapValue, Object, Property, Relationship, or Telemetry.";

            BadTypeLocatedCauseFormat[2] = "In {sourceName2}, element{line2} does not have @type of Array, Command, CommandPayload, Component, Enum, EnumValue, Field, Interface, Map, MapKey, MapValue, Object, Property, Relationship, or Telemetry.";
            BadTypeLocatedCauseFormat[3] = "In {sourceName2}, element{line2} does not have @type of Array, Command, CommandRequest, CommandResponse, Component, Enum, EnumValue, Field, Interface, Map, MapKey, MapValue, Object, Property, Relationship, or Telemetry.";
            BadTypeLocatedCauseFormat[4] = "In {sourceName2}, element{line2} does not have @type of Array, Command, CommandRequest, CommandResponse, Component, Enum, EnumValue, Field, Interface, Map, MapKey, MapValue, Object, Property, Relationship, or Telemetry.";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTEntityInfo"/> class.
        /// </summary>
        /// <param name="dtdlVersion">Version of DTDL used to define the Entity.</param>
        /// <param name="id">Identifier for the Entity.</param>
        /// <param name="childOf">Identifier of the parent element in which this Entity is defined.</param>
        /// <param name="myPropertyName">Name of the property by which the parent DTDL element refers to this Entity.</param>
        /// <param name="definedIn">Identifier of the partition in which this Entity is defined.</param>
        /// <param name="entityKind">The kind of Entity, which may be other than Entity if this constructor is called from a derived class.</param>
        internal DTEntityInfo(int dtdlVersion, Dtmi id, Dtmi childOf, string myPropertyName, Dtmi definedIn, DTEntityKind entityKind)
        {
            this.ChildOf = childOf;
            this.Comment = (string)null;
            this.DefinedIn = definedIn;
            this.Description = new Dictionary<string, string>();
            this.DisplayName = new Dictionary<string, string>();
            this.DtdlVersion = dtdlVersion;
            this.EntityKind = entityKind;
            this.Id = id;
            this.LanguageMajorVersion = 0;
            this.MyPropertyName = myPropertyName;
            this.ParentReferences = new HashSet<ParentReference>();
            this.StringProperties = new Dictionary<string, string>();

            if (childOf != null)
            {
                this.ParentReferences.Add(new ParentReference() { ParentId = childOf, PropertyName = myPropertyName });
            }

            this.IsPartition = false;

            this.UndefinedTypeStrings = new HashSet<string>();
            this.UndefinedPropertyDictionary = new Dictionary<string, JsonElement>();
            this.JsonLdElements = new Dictionary<string, JsonLdElement>();
            this.SiblingConstraints = null;

            this.MaybePartial = false;
        }

        /// <summary>
        /// Gets the kind of Entity, meaning the concrete DTDL type assigned to the corresponding element in the model.
        /// </summary>
        /// <value>The kind of Entity.</value>
        public DTEntityKind EntityKind { get; }

        /// <summary>
        /// Gets the identifier of the parent DTDL element in which this element is defined.
        /// </summary>
        /// <value>Identifier of the parent DTDL element.</value>
        public Dtmi ChildOf { get; }

        /// <summary>
        /// Get the DTMI that identifies type Entity in the version of DTDL used to define this element.
        /// </summary>
        /// <value>The DTMI for the DTDL type Entity.</value>
        public virtual Dtmi ClassId
        {
            get
            {
                return new Dtmi($"dtmi:dtdl:class:Entity;{this.LanguageMajorVersion}");
            }
        }

        /// <summary>
        /// Gets the identifier of the partition DTDL element in which this element is defined.
        /// </summary>
        /// <value>Identifier of the partition DTDL element.</value>
        public Dtmi DefinedIn { get; }

        /// <summary>
        /// Gets the identifier of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>Identifier of the DTDL element.</value>
        public Dtmi Id { get; }

        /// <summary>
        /// Gets any undefined properties of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>A dictionary that maps each string-valued property name to an object that holds the value of the property with the given name.</value>
        /// <remarks>
        /// An undefined property is any JSON-LD term used as a property name that is not defined in the DTDL language and not defined by any DTDL extension included via the "@context" property.
        /// </remarks>
        public IDictionary<string, JsonElement> UndefinedProperties
        {
            get
            {
                return this.UndefinedPropertyDictionary;
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
        public virtual IDictionary<string, object> SupplementalProperties
        {
            get
            {
                return new Dictionary<string, object>();
            }
        }

        /// <summary>
        /// Gets the value of the 'languageMajorVersion' property of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>The 'languageMajorVersion' property of the DTDL element.</value>
        public int LanguageMajorVersion { get; internal set; }

        /// <summary>
        /// Gets a collection of identifiers, each of which is a <c>Dtmi</c> that indicates a supplemental type that applies to the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>A collection of DTMIs indicating the supplemental types that apply to the DTDL element.</value>
        public virtual IReadOnlyCollection<Dtmi> SupplementalTypes
        {
            get
            {
                return new List<Dtmi>();
            }
        }

        /// <summary>
        /// Gets a collection of strings, each of which is an undefined type that applies to the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>A collection of undefined type strings.</value>
        /// <remarks>
        /// An undefined type is any JSON-LD term used as a value of "@type" that is not defined in the DTDL language and not defined by any DTDL extension included via the "@context" property.
        /// The DTDL language permits undefined types to be co-typed on any DTDL element regardless of its primary type.
        /// The DTDL language permits undefined propertyes to be added to any DTDL element that has an undefined co-type.
        /// </remarks>
        public IReadOnlyCollection<string> UndefinedTypes
        {
            get
            {
                return this.UndefinedTypeStrings;
            }
        }

        /// <summary>
        /// Gets the values of the 'description' property of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>The 'description' property of the DTDL element.</value>
        public IReadOnlyDictionary<string, string> Description { get; internal set; }

        /// <summary>
        /// Gets the values of the 'displayName' property of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>The 'displayName' property of the DTDL element.</value>
        public IReadOnlyDictionary<string, string> DisplayName { get; internal set; }

        /// <summary>
        /// Gets the value of the 'comment' property of the DTDL element that corresponds to this object.
        /// </summary>
        /// <value>The 'comment' property of the DTDL element.</value>
        public string Comment { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this class is a partition point for the object model.
        /// </summary>
        internal virtual bool IsPartition { get; }

        /// <summary>
        /// Gets a value indicating whether the class is an allowed cotype for a supplemental class that is designated as mergeable.
        /// </summary>
        /// <value>True if it is possible for the class to be mergeable.</value>
        internal bool MaybePartial { get; set; }

        /// <summary>
        /// Gets or sets a dictionary that maps from an undefined property name to a <c>JsonElement</c> representation of the property value.
        /// </summary>
        internal Dictionary<string, JsonElement> UndefinedPropertyDictionary { get; set; }

        /// <summary>
        /// Gets a dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the element.
        /// </summary>
        internal Dictionary<string, JsonLdElement> JsonLdElements { get; private set; }

        /// <summary>
        /// Gets a set of references to parent DTDL elements.
        /// </summary>
        /// <value>A set of parent references</value>
        internal HashSet<ParentReference> ParentReferences { get; }

        /// <summary>
        /// Gets material properties allowed by the type of DTDL element.
        /// </summary>
        internal virtual HashSet<string> MaterialProperties
        {
            get
            {
                return PropertyNames;
            }
        }

        /// <summary>
        /// Gets or sets a list of undefined type strings applied to the element.
        /// </summary>
        internal HashSet<string> UndefinedTypeStrings { get; set; }

        /// <summary>
        /// Gets the DTDL version in which this element is defined.
        /// </summary>
        /// <value>The DTDL version.</value>
        internal int DtdlVersion { get; }

        /// <summary>
        /// Gets a dictionary of singular string literal properties and their values.
        /// </summary>
        /// <value>Dictionary mappyng string literal properties to values.</value>
        internal IReadOnlyDictionary<string, string> StringProperties { get; set; }

        internal List<SiblingConstraint> SiblingConstraints { get; set; }

        /// <summary>
        /// Gets the name of the property by which the parent DTDL element refers to this element.
        /// </summary>
        /// <value>The referenced property name.</value>
        internal string MyPropertyName { get; }

        /// <summary>
        /// Determines whether two <c>DTEntityInfo</c> objects are not equal.
        /// </summary>
        /// <param name="x">One <c>DTEntityInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTEntityInfo</c> object to compare to the first.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(DTEntityInfo x, DTEntityInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return !ReferenceEquals(null, y);
            }

            return !x.Equals(y);
        }

        /// <summary>
        /// Determines whether two <c>DTEntityInfo</c> objects are equal.
        /// </summary>
        /// <param name="x">One <c>DTEntityInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTEntityInfo</c> object to compare to the first.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(DTEntityInfo x, DTEntityInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return ReferenceEquals(null, y);
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Compares to another <c>DTEntityInfo</c> object, recursing through the entire subtree of object properties.
        /// </summary>
        /// <param name="other">The other <c>DTEntityInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool DeepEquals(DTEntityInfo other)
        {
            return !ReferenceEquals(null, other)
                && Helpers.AreDictionariesLiteralEqual(this.Description, other.Description)
                && Helpers.AreDictionariesLiteralEqual(this.DisplayName, other.DisplayName)
                && this.Comment == other.Comment
                && this.EntityKind == other.EntityKind
                && this.LanguageMajorVersion == other.LanguageMajorVersion
                ;
        }

        /// <summary>
        /// Compares to another <c>DTEntityInfo</c> object.
        /// </summary>
        /// <param name="other">The other <c>DTEntityInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool Equals(DTEntityInfo other)
        {
            return !ReferenceEquals(null, other)
                && Helpers.AreDictionariesLiteralEqual(this.Description, other.Description)
                && Helpers.AreDictionariesLiteralEqual(this.DisplayName, other.DisplayName)
                && this.Comment == other.Comment
                && this.EntityKind == other.EntityKind
                && this.LanguageMajorVersion == other.LanguageMajorVersion
                ;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object otherObj)
        {
            return otherObj is DTEntityInfo other && this.Equals(other);
        }

        /// <inheritdoc/>
        bool ITypeChecker.DoesHaveType(Dtmi typeId)
        {
            return VersionlessTypes.Contains(typeId.Versionless)
                ;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            int hashCode = 0;

            unchecked
            {
                hashCode = (hashCode * 131) + (this.Comment?.GetHashCode() ?? 0);
                hashCode = (hashCode * 131) + Helpers.GetDictionaryLiteralHashCode(this.Description);
                hashCode = (hashCode * 131) + Helpers.GetDictionaryLiteralHashCode(this.DisplayName);
                hashCode = (hashCode * 131) + this.EntityKind.GetHashCode();
                hashCode = (hashCode * 131) + this.LanguageMajorVersion.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Validate a <c>JsonElement</c> to determine whether it conforms to the definition in the DTDL language (for primitive schema types) or to the specific element defined in the model (for complex schema types).
        /// </summary>
        /// <param name="instanceElt">The <c>JsonElement</c> to validate.</param>
        /// <returns>A list of strings that each indicate a validation failure; the list is empty if the <c>JsonElement</c> conforms.</returns>
        /// <remarks>
        /// This method will throw a <c>ValidationException</c> for subclasses that have no instance validator defined.
        /// Conformance is meaningfull only for schema types, so only these classes can validate an instance.
        /// </remarks>
        public virtual IReadOnlyCollection<string> ValidateInstance(JsonElement instanceElt)
        {
            throw new ValidationException(this.EntityKind.ToString());
        }

        /// <summary>
        /// Validate JSON text to determine whether it conforms to the definition in the DTDL language (for primitive schema types) or to the specific element defined in the model (for complex schema types).
        /// </summary>
        /// <param name="instanceText">The JSON text to validate.</param>
        /// <returns>A list of strings that each indicate a validation failure; the list is empty if the text is valid JSON and conforms.</returns>
        /// <remarks>
        /// Most subclasses will throw a <c>ValidationException</c> if this method is called, because they have no instance validator defined.
        /// Conformance is meaningfull only for schema types, so only these classes can validate an instance.
        /// </remarks>
        public IReadOnlyCollection<string> ValidateInstance(string instanceText)
        {
            JsonDocument instanceDoc = null;
            JsonElement instanceElt;
            try
            {
                instanceDoc = JsonDocument.Parse(instanceText);
                instanceElt = instanceDoc.RootElement.Clone();
            }
            catch (JsonException)
            {
                return new List<string>() { $">>{instanceText}<< is not valid JSON text" };
            }
            finally
            {
                instanceDoc?.Dispose();
            }

            return this.ValidateInstance(instanceElt);
        }

        /// <summary>
        /// Parse an element encoded in a <see cref="JsonLdElement"/> into an object of type <c>DTEntityInfo</c>.
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
        internal static bool TryParseElement(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, string dtmiSeg, string keyProp, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool tolerateSolecisms, HashSet<int> allowedVersions, string inferredType)
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
                if (elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
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

            DTEntityInfo elementInfo = (DTEntityInfo)baseElement;
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
        /// Parse a string property value as an identifier for a subclass of type <c>DTEntityInfo</c>.
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
        internal static bool TryParseIdString(List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, string idString, string layer, Dtmi parentId, string propName, JsonLdProperty propProp, string keyProp, HashSet<int> allowedVersions)
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
        /// <param name="elementInfo">The <see cref="DTEntityInfo"/> created.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="immediateSupplementalTypeIds">A set of supplemental type IDs from the type array.</param>
        /// <param name="immediateUndefinedTypes">A list of undefind type strings from the type array.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if no errors detected in type array.</returns>
        internal static bool TryParseTypeStrings(List<string> typeStrings, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, ref DTEntityInfo elementInfo, AggregateContext aggregateContext, out HashSet<Dtmi> immediateSupplementalTypeIds, out List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection)
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
                if (elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
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
        internal static bool TryParseTypeStringV2(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTEntityInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            switch (typeString)
            {
                case "Array":
                case "dtmi:dtdl:class:Array;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTArrayInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Array);
                    return true;
                case "Boolean":
                case "dtmi:dtdl:class:Boolean;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTBooleanInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Boolean);
                    return true;
                case "Command":
                case "dtmi:dtdl:class:Command;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Command);
                    return true;
                case "CommandPayload":
                case "dtmi:dtdl:class:CommandPayload;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandPayloadInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandPayload);
                    return true;
                case "CommandType":
                case "dtmi:dtdl:class:CommandType;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandTypeInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandType);
                    return true;
                case "Component":
                case "dtmi:dtdl:class:Component;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTComponentInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Component);
                    return true;
                case "Date":
                case "dtmi:dtdl:class:Date;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDateInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Date);
                    return true;
                case "DateTime":
                case "dtmi:dtdl:class:DateTime;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDateTimeInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.DateTime);
                    return true;
                case "Double":
                case "dtmi:dtdl:class:Double;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDoubleInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Double);
                    return true;
                case "Duration":
                case "dtmi:dtdl:class:Duration;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDurationInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Duration);
                    return true;
                case "Enum":
                case "dtmi:dtdl:class:Enum;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTEnumInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Enum);
                    return true;
                case "EnumValue":
                case "dtmi:dtdl:class:EnumValue;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTEnumValueInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.EnumValue);
                    return true;
                case "Field":
                case "dtmi:dtdl:class:Field;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTFieldInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Field);
                    return true;
                case "Float":
                case "dtmi:dtdl:class:Float;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTFloatInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Float);
                    return true;
                case "Integer":
                case "dtmi:dtdl:class:Integer;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTIntegerInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Integer);
                    return true;
                case "Interface":
                case "dtmi:dtdl:class:Interface;2":
                    if (elementId.AbsoluteUri.Length > 128)
                    {
                        parsingErrorCollection.Notify(
                            "idTooLongForType",
                            identifier: elementId.ToString(),
                            elementType: "Interface",
                            expectedCount: 128,
                            element: elt);
                    }

                    if (elementInfo == null)
                    {
                        elementInfo = new DTInterfaceInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Interface);
                    return true;
                case "Long":
                case "dtmi:dtdl:class:Long;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTLongInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Long);
                    return true;
                case "Map":
                case "dtmi:dtdl:class:Map;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Map);
                    return true;
                case "MapKey":
                case "dtmi:dtdl:class:MapKey;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapKeyInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.MapKey);
                    return true;
                case "MapValue":
                case "dtmi:dtdl:class:MapValue;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapValueInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.MapValue);
                    return true;
                case "Object":
                case "dtmi:dtdl:class:Object;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTObjectInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Object);
                    return true;
                case "Property":
                case "dtmi:dtdl:class:Property;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTPropertyInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Property);
                    return true;
                case "Relationship":
                case "dtmi:dtdl:class:Relationship;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTRelationshipInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Relationship);
                    return true;
                case "String":
                case "dtmi:dtdl:class:String;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTStringInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.String);
                    return true;
                case "Telemetry":
                case "dtmi:dtdl:class:Telemetry;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTTelemetryInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Telemetry);
                    return true;
                case "Time":
                case "dtmi:dtdl:class:Time;2":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTTimeInfo(2, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Time);
                    return true;
            }

            if (MaterialTypeNameCollection.IsMaterialType(typeString))
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
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
                    if (elementInfo == null)
                    {
                        elementInfo = new DTUnitInfo(2, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.Unit);
                    return true;
                case DTExtensionKind.UnitAttribute:
                    if (elementInfo == null)
                    {
                        elementInfo = new DTUnitAttributeInfo(2, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.UnitAttribute);
                    return true;
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
        internal static bool TryParseTypeStringV3(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTEntityInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            switch (typeString)
            {
                case "Array":
                case "dtmi:dtdl:class:Array;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTArrayInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Array);
                    return true;
                case "Boolean":
                case "dtmi:dtdl:class:Boolean;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTBooleanInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Boolean);
                    return true;
                case "Command":
                case "dtmi:dtdl:class:Command;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Command);
                    return true;
                case "CommandRequest":
                case "dtmi:dtdl:class:CommandRequest;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandRequestInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandRequest);
                    return true;
                case "CommandResponse":
                case "dtmi:dtdl:class:CommandResponse;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandResponseInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandResponse);
                    return true;
                case "CommandType":
                case "dtmi:dtdl:class:CommandType;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandTypeInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandType);
                    return true;
                case "Component":
                case "dtmi:dtdl:class:Component;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTComponentInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Component);
                    return true;
                case "Date":
                case "dtmi:dtdl:class:Date;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDateInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Date);
                    return true;
                case "DateTime":
                case "dtmi:dtdl:class:DateTime;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDateTimeInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.DateTime);
                    return true;
                case "Double":
                case "dtmi:dtdl:class:Double;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDoubleInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Double);
                    return true;
                case "Duration":
                case "dtmi:dtdl:class:Duration;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDurationInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Duration);
                    return true;
                case "Enum":
                case "dtmi:dtdl:class:Enum;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTEnumInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Enum);
                    return true;
                case "EnumValue":
                case "dtmi:dtdl:class:EnumValue;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTEnumValueInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.EnumValue);
                    return true;
                case "Field":
                case "dtmi:dtdl:class:Field;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTFieldInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Field);
                    return true;
                case "Float":
                case "dtmi:dtdl:class:Float;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTFloatInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Float);
                    return true;
                case "Integer":
                case "dtmi:dtdl:class:Integer;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTIntegerInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Integer);
                    return true;
                case "Interface":
                case "dtmi:dtdl:class:Interface;3":
                    if (elementId.AbsoluteUri.Length > 128)
                    {
                        parsingErrorCollection.Notify(
                            "idTooLongForType",
                            identifier: elementId.ToString(),
                            elementType: "Interface",
                            expectedCount: 128,
                            element: elt);
                    }

                    if (elementInfo == null)
                    {
                        elementInfo = new DTInterfaceInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Interface);
                    return true;
                case "Long":
                case "dtmi:dtdl:class:Long;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTLongInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Long);
                    return true;
                case "Map":
                case "dtmi:dtdl:class:Map;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Map);
                    return true;
                case "MapKey":
                case "dtmi:dtdl:class:MapKey;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapKeyInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.MapKey);
                    return true;
                case "MapValue":
                case "dtmi:dtdl:class:MapValue;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapValueInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.MapValue);
                    return true;
                case "Object":
                case "dtmi:dtdl:class:Object;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTObjectInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Object);
                    return true;
                case "Property":
                case "dtmi:dtdl:class:Property;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTPropertyInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Property);
                    return true;
                case "Relationship":
                case "dtmi:dtdl:class:Relationship;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTRelationshipInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Relationship);
                    return true;
                case "String":
                case "dtmi:dtdl:class:String;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTStringInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.String);
                    return true;
                case "Telemetry":
                case "dtmi:dtdl:class:Telemetry;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTTelemetryInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Telemetry);
                    return true;
                case "Time":
                case "dtmi:dtdl:class:Time;3":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTTimeInfo(3, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Time);
                    return true;
            }

            if (MaterialTypeNameCollection.IsMaterialType(typeString))
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
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
                    if (elementInfo == null)
                    {
                        elementInfo = new DTLatentTypeInfo(3, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.LatentType);
                    return true;
                case DTExtensionKind.NamedLatentType:
                    if (elementInfo == null)
                    {
                        elementInfo = new DTNamedLatentTypeInfo(3, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.NamedLatentType);
                    return true;
                case DTExtensionKind.Unit:
                    if (elementInfo == null)
                    {
                        elementInfo = new DTUnitInfo(3, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.Unit);
                    return true;
                case DTExtensionKind.UnitAttribute:
                    if (elementInfo == null)
                    {
                        elementInfo = new DTUnitAttributeInfo(3, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.UnitAttribute);
                    return true;
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
        internal static bool TryParseTypeStringV4(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTEntityInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            switch (typeString)
            {
                case "Array":
                case "dtmi:dtdl:class:Array;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTArrayInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Array);
                    return true;
                case "Boolean":
                case "dtmi:dtdl:class:Boolean;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTBooleanInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Boolean);
                    return true;
                case "Command":
                case "dtmi:dtdl:class:Command;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Command);
                    return true;
                case "CommandRequest":
                case "dtmi:dtdl:class:CommandRequest;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandRequestInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandRequest);
                    return true;
                case "CommandResponse":
                case "dtmi:dtdl:class:CommandResponse;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandResponseInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandResponse);
                    return true;
                case "CommandType":
                case "dtmi:dtdl:class:CommandType;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTCommandTypeInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.CommandType);
                    return true;
                case "Component":
                case "dtmi:dtdl:class:Component;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTComponentInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Component);
                    return true;
                case "Date":
                case "dtmi:dtdl:class:Date;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDateInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Date);
                    return true;
                case "DateTime":
                case "dtmi:dtdl:class:DateTime;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDateTimeInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.DateTime);
                    return true;
                case "Double":
                case "dtmi:dtdl:class:Double;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDoubleInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Double);
                    return true;
                case "Duration":
                case "dtmi:dtdl:class:Duration;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTDurationInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Duration);
                    return true;
                case "Enum":
                case "dtmi:dtdl:class:Enum;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTEnumInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Enum);
                    return true;
                case "EnumValue":
                case "dtmi:dtdl:class:EnumValue;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTEnumValueInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.EnumValue);
                    return true;
                case "Field":
                case "dtmi:dtdl:class:Field;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTFieldInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Field);
                    return true;
                case "Float":
                case "dtmi:dtdl:class:Float;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTFloatInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Float);
                    return true;
                case "Integer":
                case "dtmi:dtdl:class:Integer;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTIntegerInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Integer);
                    return true;
                case "Interface":
                case "dtmi:dtdl:class:Interface;4":
                    if (elementId.AbsoluteUri.Length > 128)
                    {
                        parsingErrorCollection.Notify(
                            "idTooLongForType",
                            identifier: elementId.ToString(),
                            elementType: "Interface",
                            expectedCount: 128,
                            element: elt);
                    }

                    if (elementInfo == null)
                    {
                        elementInfo = new DTInterfaceInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Interface);
                    return true;
                case "Long":
                case "dtmi:dtdl:class:Long;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTLongInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Long);
                    return true;
                case "Map":
                case "dtmi:dtdl:class:Map;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Map);
                    return true;
                case "MapKey":
                case "dtmi:dtdl:class:MapKey;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapKeyInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.MapKey);
                    return true;
                case "MapValue":
                case "dtmi:dtdl:class:MapValue;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTMapValueInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.MapValue);
                    return true;
                case "Object":
                case "dtmi:dtdl:class:Object;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTObjectInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Object);
                    return true;
                case "Property":
                case "dtmi:dtdl:class:Property;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTPropertyInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Property);
                    return true;
                case "Relationship":
                case "dtmi:dtdl:class:Relationship;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTRelationshipInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Relationship);
                    return true;
                case "String":
                case "dtmi:dtdl:class:String;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTStringInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.String);
                    return true;
                case "Telemetry":
                case "dtmi:dtdl:class:Telemetry;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTTelemetryInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Telemetry);
                    return true;
                case "Time":
                case "dtmi:dtdl:class:Time;4":
                    if (elementInfo == null)
                    {
                        elementInfo = new DTTimeInfo(4, elementId, parentId, propName, definedIn);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    materialKinds.Add(DTEntityKind.Time);
                    return true;
            }

            if (MaterialTypeNameCollection.IsMaterialType(typeString))
            {
                string sourceName1 = null;
                int sourceLine1 = 0;
                if (elt.TryGetSourceLocation(out string sourceName2, out int startLine2, out int endLine2))
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
                    if (elementInfo == null)
                    {
                        elementInfo = new DTLatentTypeInfo(4, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.LatentType);
                    return true;
                case DTExtensionKind.NamedLatentType:
                    if (elementInfo == null)
                    {
                        elementInfo = new DTNamedLatentTypeInfo(4, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.NamedLatentType);
                    return true;
                case DTExtensionKind.Unit:
                    if (elementInfo == null)
                    {
                        elementInfo = new DTUnitInfo(4, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.Unit);
                    return true;
                case DTExtensionKind.UnitAttribute:
                    if (elementInfo == null)
                    {
                        elementInfo = new DTUnitAttributeInfo(4, elementId, parentId, propName, definedIn, supplementalTypeId);
                    }

                    elementInfo.JsonLdElements[string.Empty] = elt;
                    elementInfo.AddType(supplementalTypeId, supplementalTypeInfo, parsingErrorCollection);
                    materialKinds.Add(DTEntityKind.UnitAttribute);
                    return true;
            }

            supplementalTypeIds.Add(supplementalTypeId);

            return true;
        }

        /// <summary>
        /// Parse elements encoded in a <see cref="JsonLdValueCollection"/> into objects of type <c>DTEntityInfo</c>.
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
        internal static void ParseValueCollection(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdProperty valueCollectionProp, string layer, Dtmi parentId, Dtmi definedIn, string propName, string dtmiSeg, string keyProp, int minCount, bool isPlural, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool tolerateSolecisms, HashSet<int> allowedVersions)
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
                                    layer: layer);
                            }
                        }

                        break;
                    case JsonLdValueType.Element:
                        if (TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, valueConstraints, aggregateContext, parsingErrorCollection, value.ElementValue, layer, parentId, definedIn, propName, valueCollectionProp, dtmiSeg, keyProp, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, allowedVersions, "Entity"))
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

        /// <summary>
        /// Check the nesting depth of all descendant elementSchema or schema properties.
        /// </summary>
        /// <param name="depth">The depth from the root to this element.</param>
        /// <param name="depthLimit">The allowed limit on the depth.</param>
        /// <param name="tooDeepElementId">An out parameter for the ID of the first element that exceeds the depth.</param>
        /// <param name="tooDeepElts">An out parameter providing a dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the first element that exceeds the depth.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if the depth is within the limit.</returns>
        internal abstract bool CheckDepthOfElementSchemaOrSchema(int depth, int depthLimit, out Dtmi tooDeepElementId, out Dictionary<string, JsonLdElement> tooDeepElts, ParsingErrorCollection parsingErrorCollection);

        /// <summary>
        /// Determine whether a <c>JsonElement</c> matches this DTDL element.
        /// </summary>
        /// <param name="instanceElt">The <c>JsonElement</c> to match.</param>
        /// <param name="instanceName">If the instance is a property in a JSON object, the name corresponding to <paramref name="instanceElt"/>; otherwise, null.</param>
        /// <returns>True if the <c>JsonElement</c> matches; false otherwise.</returns>
        internal virtual bool DoesInstanceMatch(JsonElement instanceElt, string instanceName)
        {
            return false;
        }

        /// <summary>
        /// Indicate whether the property with a given <paramref name="propertyName"/> is a dictionary that holds a given <paramref name="key"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <param name="key">The key for the dictionary property.</param>
        /// <returns>True if the property is present, is a dictionary, and holds the given key.</returns>
        internal virtual bool DoesPropertyDictContainKey(string propertyName, string key)
        {
            switch (propertyName)
            {
                default:
                    return false;
            }
        }

        /// <summary>
        /// From the property given by <paramref name="childrenPropertyName"/>, get the element with a property given by <paramref name="keyPropertyName"/> whose value is given by <paramref name="keyPropertyValue"/>.
        /// </summary>
        /// <param name="childrenPropertyName">The name of the plural object property that contains the child to get.</param>
        /// <param name="keyPropertyName">The name of a string property on the child element that uniquely identifies the child.</param>
        /// <param name="keyPropertyValue">The value of the unique string property on the child element that indicates which child to get.</param>
        /// <param name="child">The identified child.</param>
        /// <returns>True if the indicated child is found.</returns>
        internal abstract bool TryGetChild(string childrenPropertyName, string keyPropertyName, string keyPropertyValue, out DTEntityInfo child);

        /// <summary>
        /// Get the ID of any Array element of a descendant schema property, if such exists.
        /// </summary>
        /// <param name="elementId">An out parameter providing the element ID.</param>
        /// <param name="excludedElts">An out parameter providing a dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the element.</param>
        /// <returns>True if the type is found among the relevant descendant properties.</returns>
        internal abstract bool TryGetDescendantSchemaArray(out Dtmi elementId, out Dictionary<string, JsonLdElement> excludedElts);

        /// <summary>
        /// Get the ID of any Component element of a descendant schema or contents property, if such exists.
        /// </summary>
        /// <param name="elementId">An out parameter providing the element ID.</param>
        /// <param name="excludedElts">An out parameter providing a dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the element.</param>
        /// <returns>True if the type is found among the relevant descendant properties.</returns>
        internal abstract bool TryGetDescendantSchemaOrContentsComponentNarrow(out Dtmi elementId, out Dictionary<string, JsonLdElement> excludedElts);

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
        internal virtual bool TrySetObjectProperty(string propertyName, string layer, JsonLdProperty propProp, DTEntityInfo element, string keyProp, string keyValue, ParsingErrorCollection parsingErrorCollection)
        {
            switch (propertyName)
            {
                default:
                    break;
            }

            return false;
        }

        /// <summary>
        /// Validate a <c>JsonElement</c> to determine whether it conforms to the definition in the DTDL language (for primitive schema types) or to the specific element defined in the model (for complex schema types).
        /// </summary>
        /// <param name="instanceElt">The <c>JsonElement</c> to validate.</param>
        /// <param name="instanceName">If the instance is a property in a JSON object, the name corresponding to <paramref name="instanceElt"/>; otherwise, null.</param>
        /// <param name="violations">A list of strings to which to add any validation failures.</param>
        internal virtual bool ValidateInstance(JsonElement instanceElt, string instanceName, List<string> violations)
        {
            return true;
        }

        /// <summary>
        /// Get the transitive closure of the IDs of all descendant extends properties, and also check the nesting depth.
        /// </summary>
        /// <param name="depth">The depth from the root to this element.</param>
        /// <param name="depthLimit">The allowed limit on the depth.</param>
        /// <param name="tooDeepElementId">An out parameter for the ID of the first element that exceeds the depth.</param>
        /// <param name="tooDeepElts">An out parameter providing a dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the first element that exceeds the depth.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>A <c>HashSet</c> of the IDs, or null if the depth exceeds the limit.</returns>
        internal abstract HashSet<Dtmi> GetTransitiveExtendsNarrow(int depth, int depthLimit, out Dtmi tooDeepElementId, out Dictionary<string, JsonLdElement> tooDeepElts, ParsingErrorCollection parsingErrorCollection);

        /// <summary>
        /// Get an enumeration of elements from the property given by <paramref name="childrenPropertyName"/>.
        /// </summary>
        /// <param name="childrenPropertyName">The name of the plural object property that contains the children to get.</param>
        /// <returns>An enumeration of DTEntityInfo.</returns>
        internal abstract IEnumerable<DTEntityInfo> GetChildren(string childrenPropertyName);

        /// <summary>
        /// Get the count of all descendant contents or fields or enumValues or request or response or properties or schema or elementSchema or mapValue properties.
        /// </summary>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>The count of relevant property values.</returns>
        internal abstract int GetCountOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrow(ParsingErrorCollection parsingErrorCollection);

        /// <summary>
        /// Get the count of all descendant extends properties.
        /// </summary>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>The count of relevant property values.</returns>
        internal abstract int GetCountOfExtendsNarrow(ParsingErrorCollection parsingErrorCollection);

        /// <summary>
        /// Add a supplemental type.
        /// </summary>
        /// <param name="id">Identifier for the supplemental type to add.</param>
        /// <param name="supplementalType"><c>DTSupplementalTypeInfo</c> for the supplemental type.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        internal virtual void AddType(Dtmi id, DTSupplementalTypeInfo supplementalType, ParsingErrorCollection parsingErrorCollection)
        {
            throw new Exception($"attempt to add type {id} to non-augmentable type DTEntityInfo");
        }

        /// <summary>
        /// Apply any transformations that can only be performed after object properties have been assigned.
        /// </summary>
        /// <param name="model">The model to transform.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        internal abstract void ApplyTransformations(Model model, ParsingErrorCollection parsingErrorCollection);

        /// <summary>
        /// Check that the datatype of every descendant enumValue property matches <paramref name="datatype"/>.
        /// </summary>
        /// <param name="ancestorId">The identifier of the ancestor element that specifies the required datatype.</param>
        /// <param name="ancestorElts">An out parameter providing a dictionary that maps from layer name to the <see cref="JsonLdElement"/> that defines the layer of the ancestor element.</param>
        /// <param name="datatype">The required datatype.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        internal abstract void CheckDescendantEnumValueDatatype(Dtmi ancestorId, Dictionary<string, JsonLdElement> ancestorElts, Type datatype, ParsingErrorCollection parsingErrorCollection);

        /// <summary>
        /// Check whether the element has any pairs of cotypes that are mutually exclusive.
        /// </summary>
        /// <param name="supplementalTypeIds">A set of identifiers, each indicating a supplemental type applied to the element.</param>
        /// <param name="supplementalTypes">A list of <see cref="DTSupplementalTypeInfo"/> objects, each describing a supplemental type applied to the element.</param>
        /// <param name="parsingErrorCollection">A <see cref="ParsingErrorCollection"/> to which any parsing errors are added.</param>
        internal void CheckForDisallowedCocotypes(HashSet<Dtmi> supplementalTypeIds, List<DTSupplementalTypeInfo> supplementalTypes, ParsingErrorCollection parsingErrorCollection)
        {
            foreach (DTSupplementalTypeInfo supplementalTypeInfo in supplementalTypes)
            {
                foreach (Dtmi disallowedCocotype in supplementalTypeInfo.DisallowedCocotypes)
                {
                    if (supplementalTypeIds.Contains(disallowedCocotype))
                    {
                        string supplementalTypeTerm = ContextCollection.GetTermOrUri(supplementalTypeInfo.Type);
                        string disallowedCocotypeTerm = ContextCollection.GetTermOrUri(disallowedCocotype);
                        JsonLdElement elt = this.JsonLdElements.FirstOrDefault(e => (e.Value.Types.Contains(supplementalTypeTerm) || e.Value.Types.Contains(supplementalTypeInfo.Type.ToString())) && (e.Value.Types.Contains(disallowedCocotypeTerm) || e.Value.Types.Contains(disallowedCocotype.ToString()))).Value;
                        parsingErrorCollection.Notify(
                            "disallowedCocotype",
                            elementId: this.Id,
                            elementType: supplementalTypeTerm,
                            cotype: disallowedCocotypeTerm,
                            element: elt);
                    }
                }
            }
        }

        /// <summary>
        /// Check whether the element obeys restrictions that can only be assessed after object properties have been assigned.
        /// </summary>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        internal abstract void CheckRestrictions(ParsingErrorCollection parsingErrorCollection);

        /// <summary>
        /// Parse the properties in a JSON Entity object.
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
        internal virtual void ParsePropertiesV2(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool tolerateSolecisms)
        {
            this.LanguageMajorVersion = 2;

            JsonLdProperty commentProperty = null;
            JsonLdProperty descriptionProperty = null;
            JsonLdProperty displayNameProperty = null;
            Dictionary<string, JsonLdProperty> supplementalJsonLdProperties = new Dictionary<string, JsonLdProperty>();

            foreach (JsonLdProperty prop in elt.Properties)
            {
                switch (prop.Name)
                {
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
                            string newComment = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "comment", prop.Values, 512, null, layer, parsingErrorCollection, isOptional: true);
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
                            Dictionary<string, string> newDescription = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "description", prop.Values, "en", 512, null, layer, parsingErrorCollection);
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
                            Dictionary<string, string> newDisplayName = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "displayName", prop.Values, "en", 64, null, layer, parsingErrorCollection);
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
                            referenceType: "Entity",
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
        }

        /// <summary>
        /// Parse the properties in a JSON Entity object.
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
        internal virtual void ParsePropertiesV3(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool tolerateSolecisms)
        {
            this.LanguageMajorVersion = 3;

            JsonLdProperty commentProperty = null;
            JsonLdProperty descriptionProperty = null;
            JsonLdProperty displayNameProperty = null;
            Dictionary<string, JsonLdProperty> supplementalJsonLdProperties = new Dictionary<string, JsonLdProperty>();

            foreach (JsonLdProperty prop in elt.Properties)
            {
                switch (prop.Name)
                {
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
                            string newComment = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "comment", prop.Values, 512, null, layer, parsingErrorCollection, isOptional: true);
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
                            Dictionary<string, string> newDescription = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "description", prop.Values, "en", 512, null, layer, parsingErrorCollection);
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
                            Dictionary<string, string> newDisplayName = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "displayName", prop.Values, "en", 512, null, layer, parsingErrorCollection);
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
                            referenceType: "Entity",
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
        }

        /// <summary>
        /// Parse the properties in a JSON Entity object.
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
        internal virtual void ParsePropertiesV4(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool tolerateSolecisms)
        {
            this.LanguageMajorVersion = 3;

            JsonLdProperty commentProperty = null;
            JsonLdProperty descriptionProperty = null;
            JsonLdProperty displayNameProperty = null;
            Dictionary<string, JsonLdProperty> supplementalJsonLdProperties = new Dictionary<string, JsonLdProperty>();

            foreach (JsonLdProperty prop in elt.Properties)
            {
                switch (prop.Name)
                {
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
                            string newComment = ValueParser.ParseSingularStringValueCollection(aggregateContext, this.Id, "comment", prop.Values, 512, null, layer, parsingErrorCollection, isOptional: true);
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
                            Dictionary<string, string> newDescription = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "description", prop.Values, "en", 512, null, layer, parsingErrorCollection);
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
                            Dictionary<string, string> newDisplayName = ValueParser.ParseLangStringValueCollection(aggregateContext, this.Id, "displayName", prop.Values, "en", 512, null, layer, parsingErrorCollection);
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
                            referenceType: "Entity",
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
        internal virtual void RecordSourceAsAppropriate(string layer, JsonLdElement elt, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, bool atRoot, bool globalized)
        {
        }

        /// <summary>
        /// Write a JSON representation of the DTDL element represented by an object of this class.
        /// </summary>
        /// <param name="jsonWriter">A <c>Utf8JsonWriter</c> object with which to write the JSON representation.</param>
        /// <param name="includeClassId">True if the mothed should add a ClassId property to the JSON object.</param>
        internal virtual void WriteToJson(Utf8JsonWriter jsonWriter, bool includeClassId)
        {
            jsonWriter.WritePropertyName("SupplementalTypes");
            jsonWriter.WriteStartArray();
            foreach (Dtmi supplementalType in SupplementalTypes)
            {
                jsonWriter.WriteStringValue(supplementalType.ToString());
            }

            jsonWriter.WriteEndArray();

            jsonWriter.WritePropertyName("SupplementalProperties");
            jsonWriter.WriteStartObject();
            foreach (KeyValuePair<string, object> supplementalProperty in SupplementalProperties)
            {
                jsonWriter.WritePropertyName(supplementalProperty.Key);
                Helpers.WriteToJson(jsonWriter, supplementalProperty.Value);
            }

            jsonWriter.WriteEndObject();

            jsonWriter.WritePropertyName("UndefinedTypes");
            jsonWriter.WriteStartArray();
            foreach (string undefinedType in UndefinedTypes)
            {
                jsonWriter.WriteStringValue(undefinedType);
            }

            jsonWriter.WriteEndArray();

            jsonWriter.WritePropertyName("UndefinedProperties");
            jsonWriter.WriteStartObject();
            foreach (KeyValuePair<string, JsonElement> undefinedProperty in UndefinedProperties)
            {
                jsonWriter.WritePropertyName(undefinedProperty.Key);
                undefinedProperty.Value.WriteTo(jsonWriter);
            }

            jsonWriter.WriteEndObject();

            if (includeClassId)
            {
                jsonWriter.WriteString("ClassId", $"dtmi:dtdl:class:Entity;{this.LanguageMajorVersion}");
            }

            jsonWriter.WriteString("comment", this.Comment);

            jsonWriter.WritePropertyName("description");
            jsonWriter.WriteStartObject();

            foreach (KeyValuePair<string, string> descriptionPair in this.Description)
            {
                jsonWriter.WriteString(descriptionPair.Key, descriptionPair.Value);
            }

            jsonWriter.WriteEndObject();

            jsonWriter.WritePropertyName("displayName");
            jsonWriter.WriteStartObject();

            foreach (KeyValuePair<string, string> displayNamePair in this.DisplayName)
            {
                jsonWriter.WriteString(displayNamePair.Key, displayNamePair.Value);
            }

            jsonWriter.WriteEndObject();

            jsonWriter.WriteNumber("languageMajorVersion", this.LanguageMajorVersion);

            jsonWriter.WriteString("Id", this.Id.ToString());

            jsonWriter.WriteString("ChildOf", this.ChildOf?.ToString());

            jsonWriter.WriteString("DefinedIn", this.DefinedIn?.ToString());

            jsonWriter.WriteString("EntityKind", this.EntityKind.ToString());
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
        }

        /// <inheritdoc/>
        private void CheckRestrictionsV3(ParsingErrorCollection parsingErrorCollection)
        {
        }

        /// <inheritdoc/>
        private void CheckRestrictionsV4(ParsingErrorCollection parsingErrorCollection)
        {
        }
    }
}
