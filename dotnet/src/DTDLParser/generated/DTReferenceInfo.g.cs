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
    /// Class <c>DTReferenceInfo</c> corresponds to an element of type Reference in a DTDL model.
    /// </summary>
    internal class DTReferenceInfo : DTEntityInfo, ITypeChecker, IEquatable<DTReferenceInfo>
    {
        private static readonly Dictionary<int, HashSet<DTEntityKind>> ConcreteKinds = new Dictionary<int, HashSet<DTEntityKind>>();

        private static readonly Dictionary<int, string> BadTypeActionFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeCauseFormat = new Dictionary<int, string>();

        private static readonly Dictionary<int, string> BadTypeLocatedCauseFormat = new Dictionary<int, string>();

        private static readonly HashSet<string> PropertyNames = new HashSet<string>();

        private static readonly HashSet<string> VersionlessTypes = new HashSet<string>();

        private bool checkedForDescendantSchemaArray = false;

        private bool checkedForDescendantSchemaOrContentsComponentNarrow = false;

        private Dictionary<string, JsonLdElement> eltsOfDescendantSchemaArray = null;

        private Dictionary<string, JsonLdElement> eltsOfDescendantSchemaOrContentsComponentNarrow = null;

        private Dtmi idOfDescendantSchemaArray = null;

        private Dtmi idOfDescendantSchemaOrContentsComponentNarrow = null;

        private int countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowValue = 0;

        private int countOfExtendsNarrowValue = 0;

        private TraversalStatus countOfContentsOrFieldsOrEnumValuesOrRequestOrResponseOrPropertiesOrSchemaOrElementSchemaOrMapValueNarrowStatus = TraversalStatus.NotStarted;

        private TraversalStatus countOfExtendsNarrowStatus = TraversalStatus.NotStarted;

        private Type checkedDescendantEnumValueDatatype = null;

        static DTReferenceInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTReferenceInfo"/> class.
        /// </summary>
        /// <param name="dtdlVersion">Version of DTDL used to define the Reference.</param>
        /// <param name="id">Identifier for the Reference.</param>
        /// <param name="childOf">Identifier of the parent element in which this Reference is defined.</param>
        /// <param name="myPropertyName">Name of the property by which the parent DTDL element refers to this Reference.</param>
        /// <param name="definedIn">Identifier of the partition in which this Reference is defined.</param>
        internal DTReferenceInfo(int dtdlVersion, Dtmi id, Dtmi childOf, string myPropertyName, Dtmi definedIn)
            : base(dtdlVersion, id, childOf, myPropertyName, definedIn, DTEntityKind.Reference)
        {
            this.IsPartition = false;

            this.MaybePartial = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DTReferenceInfo"/> class.
        /// </summary>
        /// <param name="dtdlVersion">Version of DTDL used to define the Reference.</param>
        /// <param name="id">Identifier for the Reference.</param>
        /// <param name="childOf">Identifier of the parent element in which this Reference is defined.</param>
        /// <param name="myPropertyName">Name of the property by which the parent DTDL element refers to this Reference.</param>
        /// <param name="definedIn">Identifier of the partition in which this Reference is defined.</param>
        /// <param name="entityKind">The kind of Entity, which may be other than Reference if this constructor is called from a derived class.</param>
        internal DTReferenceInfo(int dtdlVersion, Dtmi id, Dtmi childOf, string myPropertyName, Dtmi definedIn, DTEntityKind entityKind)
            : base(dtdlVersion, id, childOf, myPropertyName, definedIn, entityKind)
        {
            this.IsPartition = false;

            this.MaybePartial = false;
        }

        /// <summary>
        /// Get the DTMI that identifies type Reference in the version of DTDL used to define this element.
        /// </summary>
        /// <value>The DTMI for the DTDL type Reference.</value>
        public override Dtmi ClassId
        {
            get
            {
                return new Dtmi($"dtmi:dtdl:class:Reference;{this.LanguageMajorVersion}");
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
                return new Dictionary<string, object>();
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
                return new List<Dtmi>();
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
        /// Determines whether two <c>DTReferenceInfo</c> objects are not equal.
        /// </summary>
        /// <param name="x">One <c>DTReferenceInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTReferenceInfo</c> object to compare to the first.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(DTReferenceInfo x, DTReferenceInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return !ReferenceEquals(null, y);
            }

            return !x.Equals(y);
        }

        /// <summary>
        /// Determines whether two <c>DTReferenceInfo</c> objects are equal.
        /// </summary>
        /// <param name="x">One <c>DTReferenceInfo</c> object to compare.</param>
        /// <param name="y">Another <c>DTReferenceInfo</c> object to compare to the first.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(DTReferenceInfo x, DTReferenceInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return ReferenceEquals(null, y);
            }

            return x.Equals(y);
        }

        /// <summary>
        /// Compares to another <c>DTReferenceInfo</c> object, recursing through the entire subtree of object properties.
        /// </summary>
        /// <param name="other">The other <c>DTReferenceInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool DeepEquals(DTReferenceInfo other)
        {
            return base.DeepEquals(other)
                ;
        }

        /// <summary>
        /// Compares to another <c>DTReferenceInfo</c> object.
        /// </summary>
        /// <param name="other">The other <c>DTReferenceInfo</c> object to compare to.</param>
        /// <returns>True if equal.</returns>
        public virtual bool Equals(DTReferenceInfo other)
        {
            return base.Equals(other)
                ;
        }

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object otherObj)
        {
            return otherObj is DTReferenceInfo other && this.Equals(other);
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
            int hashCode = base.GetHashCode();

            unchecked
            {
            }

            return hashCode;
        }

        /// <summary>
        /// Parse an element encoded in a <see cref="JsonLdElement"/> into an object that is a subclass of type <c>DTReferenceInfo</c>.
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

            DTReferenceInfo elementInfo = (DTReferenceInfo)baseElement;
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
        /// Parse a string property value as an identifier for type <c>DTReferenceInfo</c>.
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
        /// <param name="elementInfo">The <see cref="DTReferenceInfo"/> created.</param>
        /// <param name="aggregateContext">An <c>AggregateContext</c> object representing information retrieved from JSON-LD context blocks.</param>
        /// <param name="immediateSupplementalTypeIds">A set of supplemental type IDs from the type array.</param>
        /// <param name="immediateUndefinedTypes">A list of undefind type strings from the type array.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing errors are added.</param>
        /// <returns>True if no errors detected in type array.</returns>
        internal static bool TryParseTypeStrings(List<string> typeStrings, Dtmi elementId, string layer, JsonLdElement elt, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, ref DTReferenceInfo elementInfo, AggregateContext aggregateContext, out HashSet<Dtmi> immediateSupplementalTypeIds, out List<string> immediateUndefinedTypes, ParsingErrorCollection parsingErrorCollection)
        {
            immediateSupplementalTypeIds = new HashSet<Dtmi>();
            immediateUndefinedTypes = new List<string>();

            HashSet<DTEntityKind> materialKinds = new HashSet<DTEntityKind>();

            bool anyFailures = false;
            foreach (string typeString in typeStrings)
            {
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
        /// Parse elements encoded in a <see cref="JsonLdValueCollection"/> into objects of subclasses of type <c>DTReferenceInfo</c>.
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
                                    layer: layer);
                            }
                        }

                        break;
                    case JsonLdValueType.Element:
                        if (TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, valueConstraints, aggregateContext, parsingErrorCollection, value.ElementValue, layer, parentId, definedIn, propName, valueCollectionProp, dtmiSeg, keyProp, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, allowedVersions, "Reference"))
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
            throw new Exception($"attempt to add type {id} to non-augmentable type DTReferenceInfo");
        }

        /// <inheritdoc/>
        internal override void ApplyTransformations(Model model, ParsingErrorCollection parsingErrorCollection)
        {
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
                jsonWriter.WriteString("ClassId", $"dtmi:dtdl:class:Reference;{this.LanguageMajorVersion}");
            }
        }
    }
}
