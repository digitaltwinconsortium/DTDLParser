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
    /// Class that provides information about a type that is not materialized as a C# class.
    /// </summary>
    public partial class DTSupplementalTypeInfo : IEquatable<DTSupplementalTypeInfo>, ITypeChecker
    {
        private DTSupplementalTypeInfo()
        {
            this.AllowedCotypeKinds = new HashSet<DTEntityKind>();
        }

        /// <summary>
        /// Gets or sets a collection of kinds of allowed cotypes for this supplemental type.
        /// </summary>
        internal HashSet<DTEntityKind> AllowedCotypeKinds { get; set; }

        private static bool TryParseExtensionElement(DTExtensionKind extensionKind, Model model, List<ParsedObjectPropertyInfo> objectPropertyInfoList, List<ElementPropertyConstraint> elementPropertyConstraints, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection, JsonLdElement elt, string layer, Dtmi parentId, Dtmi definedIn, string propName, JsonLdProperty propProp, string dtmiSeg, bool idRequired, bool typeRequired, bool globalize, bool allowReservedIds, bool tolerateSolecisms, string inferredType)
        {
            switch (extensionKind)
            {
                case DTExtensionKind.Unit:
                    return DTUnitInfo.TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, null, aggregateContext, parsingErrorCollection, elt, layer, parentId, definedIn, propName, propProp, dtmiSeg, null, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, null, inferredType);
                case DTExtensionKind.UnitAttribute:
                    return DTUnitAttributeInfo.TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, null, aggregateContext, parsingErrorCollection, elt, layer, parentId, definedIn, propName, propProp, dtmiSeg, null, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, null, inferredType);
                case DTExtensionKind.LatentType:
                    return DTLatentTypeInfo.TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, null, aggregateContext, parsingErrorCollection, elt, layer, parentId, definedIn, propName, propProp, dtmiSeg, null, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, null, inferredType);
                case DTExtensionKind.NamedLatentType:
                    return DTNamedLatentTypeInfo.TryParseElement(model, objectPropertyInfoList, elementPropertyConstraints, null, aggregateContext, parsingErrorCollection, elt, layer, parentId, definedIn, propName, propProp, dtmiSeg, null, idRequired, typeRequired, globalize, allowReservedIds, tolerateSolecisms, null, inferredType);
                default:
                    return false;
            }
        }
    }
}
