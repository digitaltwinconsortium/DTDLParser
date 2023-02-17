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
    /// Class <c>DTDateInfo</c> corresponds to an element of type Date in a DTDL model.
    /// </summary>
    public class DTDateInfo : DTTemporalSchemaInfo, ITypeChecker, IConstrainer, IEquatable<DTDateInfo>
    {
        private static readonly Dictionary<int, HashSet<DTEntityKind>> ConcreteKinds = new Dictionary<int, HashSet<DTEntityKind>>();

        private static readonly Dictionary<int, string> BadTypeActionFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeCauseFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeLocatedCauseFormat = new Dictionary<int, string>();

        private static readonly HashSet<string> PropertyNames = new HashSet<string>();

        private static readonly HashSet<string> VersionlessTypes = new HashSet<string>();

        private static readonly Regex ElementValueInstanceRegexPatternV2 = new Regex(@"^[0-9]{4}-[0-9]{2}-[0-9]{2}$", RegexOptions.Compiled);

        private static readonly Regex ElementValueInstanceRegexPatternV3 = new Regex(@"^[0-9]{4}-[0-9]{2}-[0-9]{2}$", RegexOptions.Compiled);

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

        private int countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowValue = 0;

        private int countOfExtendsNarrowValue = 0;

        private List<DTSupplementalTypeInfo> supplementalTypes;

        private List<ValueConstraint> commentValueConstraints = null;

        private List<ValueConstraint> descriptionValueConstraints = null;

        private List<ValueConstraint> displayNameValueConstraints = null;

        private string commentPropertyLayer = null;

        private TraversalStatus countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowStatus = TraversalStatus.NotStarted;

        private TraversalStatus countOfExtendsNarrowStatus = TraversalStatus.NotStarted;

        private Type checkedDescendantEnumValueDatatype = null;

        static DTDateInfo()
        {
            VersionlessTypes.Add("dtmi:dtdl:class:Date");
            VersionlessTypes.Add("dtmi:dtdl:class:Entity");
            VersionlessTypes.Add("dtmi:dtdl:class:PrimitiveSchema");
            VersionlessTypes.Add("dtmi:dtdl:class:Schema");
            VersionlessTypes.Add("dtmi:dtdl:class:TemporalSchema");

            PropertyNames.Add("comment");
            PropertyNames.Add("description");
            PropertyNames.Add("displayName");
            PropertyNames.Add("languageMajorVersion");

            ConcreteKinds[2] = new HashSet<DTEntityKind>();
            ConcreteKinds[2].Add(DTEntityKind.Date);

            ConcreteKinds[3] = new HashSet<DTEntityKind>();
            ConcreteKinds[3].Add(DTEntityKind.Date);

            BadTypeActionFormat[2] = "Use this standard value for property '{property}'{line1}: date.";
            BadTypeActionFormat[3] = "Use this standard value for property '{property}'{line1}: date.";

            BadTypeCauseFormat[2] = "{layer}{primaryId:p} property '{property}' has value{secondaryId:e} that is not a standard value for this property.";
            BadTypeCauseFormat[3] = "{layer}{primaryId:p} property '{property}' has value{secondaryId:e} that is not a standard value for this property.";

            BadTypeLocatedCauseFormat[2] = "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e}{line2} that is not a standard value for this property.";
            BadTypeLocatedCauseFormat[3] = "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e}{line2} that is not a standard value for this property.";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTDateInfo"/> class.
        /// </summary>
        /// <param name="dtdlVersion">Version of DTDL used to define the Date.</param>
        /// <param name="id">Identifier for the Date.</param>
        /// <param name="childOf">Identifier of the parent element in which this Date is defined.</param>
        /// <param name="myPropertyName">Name of the property by which the parent DTDL element refers to this Date.</param>
        /// <param name="definedIn">Identifier of the partition in which this Date is defined.</param>
        internal DTDateInfo(int dtdlVersion, Dtmi id, Dtmi childOf, string myPropertyName, Dtmi definedIn)
            : base(dtdlVersion, id, childOf, myPropertyName, definedIn, DTEntityKind.Date)
        {
            this.supplementalTypeIds = new HashSet<Dtmi>();
            this.supplementalProperties = new Dictionary<string, object>();
            this.supplementalTypes = new List<DTSupplementalTypeInfo>();

            this.IsPartition = false;

            this.MaybePartial = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTDateInfo"/> class.
        /// </summary>
        /// <param name="dtdlVersion">Version of DTDL used to define the Date.</param>
        /// <param name="id">Identifier for the Date.</param>
        /// <param name="childOf">Identifier of the parent element in which this Date is defined.</param>
        /// <param name="myPropertyName">Name of the property by which the parent DTDL element refers to this Date.</param>
        /// <param name="definedIn">Identifier of the partition in which this Date is defined.</param>
        /// <param name="entityKind">The kind of Entity, which may be other than Date if this constructor is called from a derived class.</param>
        internal DTDateInfo(int dtdlVersion, Dtmi id, Dtmi childOf, string myPropertyName, Dtmi definedIn, DTEntityKind entityKind)
            : base(dtdlVersion, id, childOf, myPropertyName, definedIn, entityKind)
        {
            this.supplementalTypeIds = new HashSet<Dtmi>();
            this.supplementalProperties = new Dictionary<string, object>();
            this.supplementalTypes = new List<DTSupplementalTypeInfo>();

            this.IsPartition = false;

            this.MaybePartial = false;
        }

        /// <summary>
        /// Get the DTMI that identifies type Date in the version of DTDL used to define this element.
        /// </summary>
        /// <value>The DTMI for the DTDL type Date.</value>
        public override Dtmi ClassId
        {
            get
            {
                return new Dtmi($"dtmi:dtdl:class:Date;{this.LanguageMajorVersion}");
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
        /// Determines whether two <c>DTDateInfo</c> objects are not equal.
        /// </summary>
        /// <param name="x">One <c>DTDateInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTDateInfo</c> object to compare to the first.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(DTDateInfo x, DTDateInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return !ReferenceEquals(null, y);
            }

            return !x.Equals(y);
        }

        /// <summary>
        /// Determines whether two <c>DTDateInfo</c> objects are equal.
        /// </summary>
        /// <param name="x">One <c>DTDateInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTDateInfo</c> object to compare to the first.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(DTDateInfo x, DTDateInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return ReferenceEquals(null, y);
            }

            return x.Equals(y);
        }

        /// <inheritdoc/>
        public override bool DeepEquals(DTSchemaInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.DeepEquals((DTDateInfo)other);
        }

        /// <summary>
        /// Compares to another <c>DTDateInfo</c> object, recursing through the entire subtree of object properties.
        /// </summary>
        /// <param name="other">The other <c>DTDateInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool DeepEquals(DTDateInfo other)
        {
            return base.DeepEquals(other)
                && Helpers.AreDictionariesDeepOrLiteralEqual(this.supplementalProperties, other.supplementalProperties)
                && this.supplementalTypeIds.SetEquals(other.supplementalTypeIds)
                ;
        }

        /// <inheritdoc/>
        public override bool DeepEquals(DTEntityInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.DeepEquals((DTDateInfo)other);
        }

        /// <inheritdoc/>
        public override bool DeepEquals(DTTemporalSchemaInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.DeepEquals((DTDateInfo)other);
        }

        /// <inheritdoc/>
        public override bool DeepEquals(DTPrimitiveSchemaInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.DeepEquals((DTDateInfo)other);
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object otherObj)
        {
            return otherObj is DTDateInfo other && this.Equals(other);
        }

        /// <summary>
        /// Compares to another <c>DTDateInfo</c> object.
        /// </summary>
        /// <param name="other">The other <c>DTDateInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool Equals(DTDateInfo other)
        {
            return base.Equals(other)
                && Helpers.AreDictionariesIdOrLiteralEqual(this.supplementalProperties, other.supplementalProperties)
                && this.supplementalTypeIds.SetEquals(other.supplementalTypeIds)
                ;
        }

        /// <inheritdoc/>
        public override bool Equals(DTEntityInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.Equals((DTDateInfo)other);
        }

        /// <inheritdoc/>
        public override bool Equals(DTSchemaInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.Equals((DTDateInfo)other);
        }

        /// <inheritdoc/>
        public override bool Equals(DTPrimitiveSchemaInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.Equals((DTDateInfo)other);
        }

        /// <inheritdoc/>
        public override bool Equals(DTTemporalSchemaInfo other)
        {
            return this.EntityKind == other?.EntityKind && this.Equals((DTDateInfo)other);
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
                hashCode = (hashCode * 131) + Helpers.GetDictionaryIdOrLiteralHashCode(this.supplementalProperties);
                hashCode = (hashCode * 131) + Helpers.GetSetLiteralHashCode(this.supplementalTypeIds);
            }

            return hashCode;
        }

        /// <summary>
        /// Validate a <c>JsonElement</c> to determine whether it is a string value that conforms to the RFC 3339 definition of 'date'.
        /// </summary>
        /// <param name="instanceElt">The <c>JsonElement</c> to validate.</param>
        /// <returns>A list of strings that each indicate a validation failure; the list is empty if the <c>JsonElement</c> conforms.</returns>
        public override IReadOnlyCollection<string> ValidateInstance(JsonElement instanceElt)
        {
            List<string> violations = new List<string>();
            this.ValidateInstance(instanceElt, null, violations);
            return violations;
        }

        /// <inheritdoc/>
        void IConstrainer.AddPropertyValueConstraint(string propertyName, ValueConstraint valueConstraint)
        {
            switch (propertyName)
            {
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

        /// <summary>
        /// Parse an element encoded in a <see cref="JsonLdElement"/> into an object that is a subclass of type <c>DTDateInfo</c>.
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
        /// <param name="allowIdReferenceSyntax">Allow an object reference to be made using an object containing nothing but an @id property.</param>
        /// <param name="ignoreElementsWithAutoIDsAndDuplicateNames">Ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.</param>
        /// <param name="allowedVersions">A set of allowed versions for the element.</param>
        /// <param name="inferredType">The type name to infer if no @type specified on element.</param>
        /// <returns>True if the <see cref="JsonLdElement"/> parses correctly as an appropriate element.</returns>
        internal static new bool TryParseElement(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, string dtmiSeg, string keyProp, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames, HashSet<int> allowedVersions, string inferredType)
        {
            AggregateContext childAggregateContext = aggregateContext.GetChildContext(elt, parsingErrorCollection);

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

            Dtmi elementId = IdValidator.ParseIdProperty(childAggregateContext, elt, childAggregateContext.MergeDefinitions ? layer : null, parentId, propName, dtmiSeg, idRequired, allowReservedIds, parsingErrorCollection);

            Dtmi baseElementId = childAggregateContext.MergeDefinitions || elementId.Tail == string.Empty ? elementId.Fragmentless : elementId;
            string elementLayer = childAggregateContext.MergeDefinitions ? elementId.Tail : string.Empty;

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

            DTDateInfo elementInfo = (DTDateInfo)baseElement;
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
                    elementInfo.ParsePropertiesV2(model, objectPropertyInfoList, elementPropertyConstraints, childAggregateContext, immediateSupplementalTypeIds, immediateUndefinedTypes, parsingErrorCollection, elt, elementLayer, definedIn, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames);
                    break;
                case 3:
                    elementInfo.ParsePropertiesV3(model, objectPropertyInfoList, elementPropertyConstraints, childAggregateContext, immediateSupplementalTypeIds, immediateUndefinedTypes, parsingErrorCollection, elt, elementLayer, definedIn, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames);
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
        /// Parse a string property value as an identifier for type <c>DTDateInfo</c>.
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
        /// <param name="elementInfo">The <see cref="DTDateInfo"/> created.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="immediateSupplementalTypeIds">A set of supplemental type IDs from the type array.</param>
        /// <param name="immediateUndefinedTypes">A list of undefind type strings from the type array.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if no errors detected in type array.</returns>
        internal static bool TryParseTypeStrings(List<string> typeStrings, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, ref DTDateInfo elementInfo, AggregateContext aggregateContext, out HashSet<Dtmi> immediateSupplementalTypeIds, out List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection)
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
        internal static bool TryParseTypeStringV2(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTDateInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
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
        internal static bool TryParseTypeStringV3(string typeString, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, HashSet<DTEntityKind> materialKinds, HashSet<Dtmi> supplementalTypeIds, ref DTDateInfo elementInfo, ref List<string> undefinedTypes, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
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
        /// Parse elements encoded in a <see cref="JsonLdValueCollection"/> into objects of subclasses of type <c>DTDateInfo</c>.
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
        /// <param name="allowIdReferenceSyntax">Allow an object reference to be made using an object containing nothing but an @id property.</param>
        /// <param name="ignoreElementsWithAutoIDsAndDuplicateNames">Ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.</param>
        /// <param name="allowedVersions">A set of allowed versions for the element.</param>
        internal static new void ParseValueCollection(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, List<ValueConstraint> valueConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdProperty valueCollectionProp, string layer, Dtmi parentId, Dtmi definedIn, string propName, string dtmiSeg, string keyProp, int minCount, bool isPlural, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames, HashSet<int> allowedVersions)
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
                        if (TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, valueConstraints, aggregateContext, parsingErrorCollection, value.ElementValue, layer, parentId, definedIn, propName, valueCollectionProp, dtmiSeg, keyProp, idRequired, typeRequired, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, allowedVersions, "Date"))
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
            tooDeepElementId = null;
            tooDeepElts = this.JsonLdElements;
            return true;
        }

        /// <summary>
        /// Determine whether a <c>JsonElement</c> matches this DTDL element.
        /// </summary>
        /// <param name="instanceElt">The <c>JsonElement</c> to match.</param>
        /// <param name="instanceName">If the instance is a property in a JSON object, the name corresponding to <paramref name="instanceElt"/>; otherwise, null.</param>
        /// <returns>True if the <c>JsonElement</c> matches; false otherwise.</returns>
        internal override bool DoesInstanceMatch(JsonElement instanceElt, string instanceName)
        {
            switch (this.DtdlVersion)
            {
                case 2:
                    return this.DoesInstanceMatchV2(instanceElt, instanceName);
                case 3:
                    return this.DoesInstanceMatchV3(instanceElt, instanceName);
            }

            return false;
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

        /// <summary>
        /// Validate a <c>JsonElement</c> to determine whether it is a string value that conforms to the RFC 3339 definition of 'date'.
        /// </summary>
        /// <param name="instanceElt">The <c>JsonElement</c> to validate.</param>
        /// <param name="instanceName">If the instance is a property in a JSON object, the name corresponding to <paramref name="instanceElt"/>; otherwise, null.</param>
        /// <param name="violations">A list of strings to which to add any validation failures.</param>
        internal override bool ValidateInstance(JsonElement instanceElt, string instanceName, List<string> violations)
        {
            switch (this.DtdlVersion)
            {
                case 2:
                    return this.ValidateInstanceV2(instanceElt, instanceName, violations);
                case 3:
                    return this.ValidateInstanceV3(instanceElt, instanceName, violations);
            }

            return true;
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
            }
        }

        /// <inheritdoc/>
        internal override void CheckDescendantEnumValueDatatype(Dtmi ancestorId, Dictionary<string, JsonLdElement> ancestorElts, Type datatype, ParsingErrorCollection parsingErrorCollection)
        {
            if (this.checkedDescendantEnumValueDatatype != datatype)
            {
                this.checkedDescendantEnumValueDatatype = datatype;
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
            }
        }

        /// <summary>
        /// Parse the properties in a JSON Date object.
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
        /// <param name="allowIdReferenceSyntax">Allow an object reference to be made using an object containing nothing but an @id property.</param>
        /// <param name="ignoreElementsWithAutoIDsAndDuplicateNames">Ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.</param>
        internal override void ParsePropertiesV2(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames)
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

                if (this.TryParseSupplementalProperty(model, immediateSupplementalTypeIds, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, layer, definedIn, parsingErrorCollection, prop.Name, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, prop, supplementalJsonLdProperties))
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
                            referenceType: "Date",
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

            foreach (Dtmi supplementalTypeId in immediateSupplementalTypeIds)
            {
                if (aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo))
                {
                    supplementalTypeInfo.CheckForRequiredProperties(parsingErrorCollection, this.Id, elt, supplementalJsonLdProperties);
                }
            }
        }

        /// <summary>
        /// Parse the properties in a JSON Date object.
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
        /// <param name="allowIdReferenceSyntax">Allow an object reference to be made using an object containing nothing but an @id property.</param>
        /// <param name="ignoreElementsWithAutoIDsAndDuplicateNames">Ignore any duplicate names and accept the first one in the list, unless the element has a user-assigned ID.</param>
        internal override void ParsePropertiesV3(Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, HashSet<Dtmi> immediateSupplementalTypeIds, List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi definedIn, bool globalize, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames)
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

                if (this.TryParseSupplementalProperty(model, immediateSupplementalTypeIds, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, layer, definedIn, parsingErrorCollection, prop.Name, globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, prop, supplementalJsonLdProperties))
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
                            referenceType: "Date",
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
                jsonWriter.WriteString("ClassId", $"dtmi:dtdl:class:Date;{this.LanguageMajorVersion}");
            }
        }

        private bool DoesInstanceMatchV2(JsonElement instanceElt, string instanceName)
        {
            return true;
        }

        private bool DoesInstanceMatchV3(JsonElement instanceElt, string instanceName)
        {
            return true;
        }

        private bool TryParseSupplementalProperty(Model model, HashSet<Dtmi> immediateSupplementalTypeIds, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, string layer, Dtmi definedIn, ParsingErrorCollection parsingErrorCollection, string propName, bool globalize, bool allowReservedIds, bool allowIdReferenceSyntax, bool ignoreElementsWithAutoIDsAndDuplicateNames, JsonLdProperty valueCollectionProp, Dictionary<string, JsonLdProperty> supplementalJsonLdProperties)
        {
            if (!aggregateContext.TryCreateDtmi(propName, out Dtmi propDtmi))
            {
                return false;
            }

            foreach (Dtmi supplementalTypeId in immediateSupplementalTypeIds)
            {
                if (aggregateContext.SupplementalTypeCollection.TryGetSupplementalTypeInfo(supplementalTypeId, out DTSupplementalTypeInfo supplementalTypeInfo) && supplementalTypeInfo.TryParseProperty(model, objectPropertyInfoList, elementPropertyConstraints, aggregateContext, parsingErrorCollection, layer, this.Id, definedIn, propDtmi.ToString(), globalize, allowReservedIds, allowIdReferenceSyntax, ignoreElementsWithAutoIDsAndDuplicateNames, valueCollectionProp, ref this.supplementalProperties, supplementalJsonLdProperties, this.supplementalSingularPropertyLayers, this.JsonLdElements))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ValidateInstanceV2(JsonElement instanceElt, string instanceName, List<string> violations)
        {
            if (instanceElt.ValueKind != JsonValueKind.String)
            {
                violations.Add($">>{instanceElt.GetRawText()}<< is not a string value");
                return false;
            }

            if (!DateTime.TryParse(instanceElt.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out DateTime _))
            {
                violations.Add($"\"{instanceElt.GetString()}\" does not conform to the RFC 3339 definition of 'date'");
                return false;
            }

            if (!ElementValueInstanceRegexPatternV2.IsMatch(instanceElt.GetString()))
            {
                violations.Add($"\"{instanceElt.GetString()}\" does not conform to the RFC 3339 definition of 'date'");
                return false;
            }

            return true;
        }

        private bool ValidateInstanceV3(JsonElement instanceElt, string instanceName, List<string> violations)
        {
            if (instanceElt.ValueKind != JsonValueKind.String)
            {
                violations.Add($">>{instanceElt.GetRawText()}<< is not a string value");
                return false;
            }

            if (!DateTime.TryParse(instanceElt.GetString(), CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out DateTime _))
            {
                violations.Add($"\"{instanceElt.GetString()}\" does not conform to the RFC 3339 definition of 'date'");
                return false;
            }

            if (!ElementValueInstanceRegexPatternV3.IsMatch(instanceElt.GetString()))
            {
                violations.Add($"\"{instanceElt.GetString()}\" does not conform to the RFC 3339 definition of 'date'");
                return false;
            }

            return true;
        }

        private void ApplyTransformationsV2(Model model, ParsingErrorCollection parsingErrorCollection)
        {
        }

        private void ApplyTransformationsV3(Model model, ParsingErrorCollection parsingErrorCollection)
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
    }
}
