/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using DTDLParser.Models;

    /// <summary>
    /// Class for parsing and storing information from JSON-LD context blocks.
    /// </summary>
    internal partial class AggregateContext
    {
        /// <summary>
        /// Get a list of mergeable cotypes that can be applied to an element of a given kind.
        /// </summary>
        /// <param name="entityKind">The kind of element.</param>
        /// <returns>A list of strings, each of which is a term that maps to an appropriate mergeable cotype.</returns>
        internal List<string> GetMergeableCotypesForKind(DTEntityKind entityKind)
        {
            List<string> mergeableCotypes = new List<string>();
            foreach (Dtmi cotypeId in this.activeDtdlContext.MergeableTypeIds)
            {
                if (this.SupplementalTypeCollection.TryGetSupplementalTypeInfo(cotypeId, out DTSupplementalTypeInfo supplementalTypeInfo) && supplementalTypeInfo.AllowedCotypeKinds.Contains(entityKind))
                {
                    if (this.activeDtdlContext.TryGetTerm(cotypeId, out string term))
                    {
                        mergeableCotypes.Add(term);
                    }
                }
            }

            foreach (VersionedContext affiliateContext in this.activeAffiliateContexts.Values)
            {
                foreach (Dtmi cotypeId in affiliateContext.MergeableTypeIds)
                {
                    if (this.SupplementalTypeCollection.TryGetSupplementalTypeInfo(cotypeId, out DTSupplementalTypeInfo supplementalTypeInfo) && supplementalTypeInfo.AllowedCotypeKinds.Contains(entityKind))
                    {
                        if (affiliateContext.TryGetTerm(cotypeId, out string term))
                        {
                            mergeableCotypes.Add(term);
                        }
                    }
                }
            }

            return mergeableCotypes;
        }
    }
}
