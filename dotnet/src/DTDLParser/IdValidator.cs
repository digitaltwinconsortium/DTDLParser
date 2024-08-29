namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A static class for determining whether a string is a valid identifier.
    /// </summary>
    internal static partial class IdValidator
    {
        private static readonly Dictionary<int, Dictionary<string, int>> IdDefinitionMaxLengths = new Dictionary<int, Dictionary<string, int>>();
        private static readonly Dictionary<int, Dictionary<string, int>> IdReferenceMaxLengths = new Dictionary<int, Dictionary<string, int>>();
        private static readonly Dictionary<int, Regex> IdDefinitionRegexPatterns = new Dictionary<int, Regex>();
        private static readonly Dictionary<int, Regex> IdReferenceRegexPatterns = new Dictionary<int, Regex>();

        /// <summary>
        /// Parse the @id value from a <see cref="JsonLdElement"/> into a Dtmi, or auto-generate an identifier if none present.
        /// </summary>
        /// <param name="aggregateContext">An <see cref="AggregateContext"/> for parsing the model elements.</param>
        /// <param name="elt">The <see cref="JsonLdElement"/> to parse.</param>
        /// <param name="layer">Name of the layer currently being parsed.</param>
        /// <param name="parentId">The identifier of the parent of this object, used for auto ID generation.</param>
        /// <param name="propName">The name of the property by which the parent refers to this object, used for auto ID generation.</param>
        /// <param name="dtmiSeg">A DTMI segment identifier, used for auto ID generation.</param>
        /// <param name="idRequired">A boolean value indicating whether the @id must be present in the object.</param>
        /// <param name="allowReservedIds">Allow elements to define IDs that have reserved prefixes.</param>
        /// <param name="parsingErrorCollection">A <c>ParsingErrorCollection</c> to which any parsing error is added.</param>
        /// <returns>The DTMI extracted from the object or auto-generated.</returns>
        internal static Dtmi ParseIdProperty(AggregateContext aggregateContext, JsonLdElement elt, string layer, Dtmi parentId, string propName, string dtmiSeg, bool idRequired, bool allowReservedIds, ParsingErrorCollection parsingErrorCollection)
        {
            if (elt.Id != null)
            {
                if (elt.Id == string.Empty)
                {
                    parsingErrorCollection.Quit("idNotString", element: elt);
                }

                if (IdDefinitionMaxLengths.TryGetValue(aggregateContext.DtdlVersion, out Dictionary<string, int> specMaxLength) && specMaxLength.TryGetValue(aggregateContext.LimitSpecifier, out int maxLength) && elt.Id.Length > maxLength)
                {
                    parsingErrorCollection.Quit(
                        "idTooLong",
                        element: elt,
                        identifier: elt.Id,
                        expectedCount: maxLength,
                        version: aggregateContext.DtdlVersion.ToString(),
                        contextValue: aggregateContext.LimitContext);
                }

                if (IdDefinitionRegexPatterns.TryGetValue(aggregateContext.DtdlVersion, out Regex regex) && !regex.IsMatch(elt.Id))
                {
                    parsingErrorCollection.Quit(
                        "invalidId",
                        element: elt,
                        identifier: elt.Id,
                        version: aggregateContext.DtdlVersion.ToString());
                }

                if (!allowReservedIds && aggregateContext.IsIdentifierReserved(elt.Id))
                {
                    parsingErrorCollection.Quit(
                        "reservedId",
                        element: elt,
                        identifier: elt.Id,
                        valueRestriction: aggregateContext.GetReservedPrefixesString());
                }

                return new Dtmi(elt.Id, skipValidation: true);
            }
            else
            {
                if (idRequired)
                {
                    if (parentId == null)
                    {
                        parsingErrorCollection.Quit("missingTopLevelId", element: elt);
                    }
                    else
                    {
                        parsingErrorCollection.Quit(
                            "missingRequiredId",
                            element: elt,
                            elementId: parentId,
                            propertyName: propName);
                    }
                }

                string secondSeg = string.Empty;
                if (dtmiSeg != null)
                {
                    JsonLdProperty segProp = elt.Properties.FirstOrDefault(p => p.Name == dtmiSeg || (Dtmi.TryCreateDtmi(p.Name, out Dtmi d) && ContextCollection.GetTermOrUri(d) == dtmiSeg));
                    if (segProp == null)
                    {
                        parsingErrorCollection.Quit(
                            "missingDtmiSegPropertyValue",
                            element: elt,
                            elementId: parentId,
                            propertyName: propName,
                            nestedName: dtmiSeg,
                            layer: layer);
                    }

                    object dtmiSegValue = ValueParser.ParseSingularLiteralValueCollection(aggregateContext, null, dtmiSeg, segProp.Values, layer, parsingErrorCollection: null, isOptional: false, typeFragment: out string typeFragment);

                    if (typeFragment == "#string")
                    {
                        secondSeg = ":__" + (string)dtmiSegValue;
                    }
                    else if (typeFragment == "#integer")
                    {
                        secondSeg = ":__" + ((int)dtmiSegValue).ToString();
                    }
                    else if (typeFragment == "#decimal")
                    {
                        secondSeg = ":__" + ((double)dtmiSegValue).ToString().Replace('.', '_');
                    }
                    else
                    {
                        parsingErrorCollection.Quit(
                            "dtmiSegPropertyNotStringOrNumber",
                            element: elt,
                            incidentProperty: segProp,
                            elementId: parentId,
                            propertyName: propName,
                            nestedName: dtmiSeg,
                            layer: layer);
                    }
                }

                if (layer == null)
                {
                    return new Dtmi(GetFragmentlessChildId(parentId, propName, secondSeg) + parentId.Fragment, skipValidation: true);
                }
                else if (parentId.Tail == string.Empty)
                {
                    return new Dtmi(GetFragmentlessChildId(parentId, propName, secondSeg) + (layer == string.Empty ? string.Empty : $"#{layer}"), skipValidation: true);
                }
                else
                {
                    parsingErrorCollection.Quit(
                        "mismatchedLayers",
                        elementId: parentId,
                        propertyName: propName,
                        layer: layer);
                    return null;
                }
            }
        }

        /// <summary>
        /// Determine whether a string represents a valid DTMI when used as an element reference, for a given version of DTDL.
        /// </summary>
        /// <param name="idString">The string putatively representing a DTMI.</param>
        /// <param name="dtdlVersion">The version of DTDL whose identifier syntax applies.</param>
        /// <param name="limitSpecifier">A limit specifier to indicate a customized limit.</param>
        /// <returns>True if the string represents a valid DTMI for an element reference.</returns>
        internal static bool IsIdReferenceValid(string idString, int dtdlVersion, string limitSpecifier)
        {
            if (IdReferenceMaxLengths.TryGetValue(dtdlVersion, out Dictionary<string, int> specMaxLength) && specMaxLength.TryGetValue(limitSpecifier, out int maxLength) && idString.Length > maxLength)
            {
                return false;
            }

            if (IdReferenceRegexPatterns.TryGetValue(dtdlVersion, out Regex regex) && !regex.IsMatch(idString))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// If a string represents a valid DTMI when used as an element reference, for a given version of DTDL, create a <see cref="Dtmi"/> object and return it as a <c>Uri</c>.
        /// </summary>
        /// <param name="idString">The string putatively representing a DTMI.</param>
        /// <param name="dtdlVersion">The version of DTDL whose identifier syntax applies.</param>
        /// <param name="limitSpecifier">A limit specifier to indicate a customized limit.</param>
        /// <param name="uri">A <c>Uri</c> object to receive the created DTMI reference.</param>
        /// <returns>True if the string represents a valid DTMI for an element reference.</returns>
        internal static bool TryCreateIdReference(string idString, int dtdlVersion, string limitSpecifier, out Uri uri)
        {
            if (IsIdReferenceValid(idString, dtdlVersion, limitSpecifier))
            {
                uri = new Dtmi(idString, skipValidation: true);
                return true;
            }
            else
            {
                uri = null;
                return false;
            }
        }

        private static string GetFragmentlessChildId(Dtmi parentId, string propName, string secondSeg)
        {
            string firstSeg = propName.Contains(';') ? propName.Substring(0, propName.IndexOf(';')) : propName;

            int versionPos = parentId.AbsoluteUri.IndexOf(';');
            int fragmentPos = parentId.AbsoluteUri.IndexOf('#');

            if (versionPos < 0)
            {
                if (fragmentPos < 0)
                {
                    return parentId.AbsoluteUri + ":_" + firstSeg + secondSeg;
                }
                else
                {
                    return parentId.AbsoluteUri.Substring(0, fragmentPos) + ":_" + firstSeg + secondSeg;
                }
            }
            else
            {
                if (fragmentPos < 0)
                {
                    return parentId.AbsoluteUri.Substring(0, versionPos) + ":_" + firstSeg + secondSeg + parentId.AbsoluteUri.Substring(versionPos);
                }
                else
                {
                    return parentId.AbsoluteUri.Substring(0, versionPos) + ":_" + firstSeg + secondSeg + parentId.AbsoluteUri.Substring(versionPos, fragmentPos - versionPos);
                }
            }
        }
    }
}
