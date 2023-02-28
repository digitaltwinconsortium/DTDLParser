/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
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
    using DTDLParser.Models;

    /// <summary>
    /// A collection of values of standard elements from the DTDL metamodel.
    /// </summary>
    internal partial class StandardElementCollection
    {
        private static void PopulateEndogenousAliases()
        {
            Dictionary<Dtmi, string> aliasTypePropertyMap = new Dictionary<Dtmi, string>();
            aliasTypePropertyMap[new Dtmi("dtmi:dtdl:class:Alias;3")] = "dtmi:dtdl:property:aliasFor;3";

            Dictionary<Dtmi, string> aliasElementPropertyMap = new Dictionary<Dtmi, string>();
            foreach (var kvp in EndogenousStandardModel.Dict)
            {
                foreach (Dtmi supplementalTypeId in kvp.Value.SupplementalTypes)
                {
                    if (aliasTypePropertyMap.TryGetValue(supplementalTypeId, out string propName))
                    {
                        aliasElementPropertyMap[kvp.Key] = propName;
                        break;
                    }
                }
            }

            foreach (KeyValuePair<Dtmi, string> kvp in aliasElementPropertyMap)
            {
                List<Dtmi> equivalentIds = new List<Dtmi>();
                Dtmi fromId = kvp.Key;
                Dtmi toId = null;
                while (aliasElementPropertyMap.TryGetValue(fromId, out string propName) && !EndogenousAliases.TryGetValue(fromId, out toId))
                {
                    equivalentIds.Add(fromId);
                    fromId = ((DTEntityInfo)EndogenousStandardModel.Dict[fromId].SupplementalProperties[propName]).Id;
                }

                foreach (Dtmi equivalentId in equivalentIds)
                {
                    EndogenousAliases[equivalentId] = toId ?? fromId;
                }
            }
        }
    }
}
