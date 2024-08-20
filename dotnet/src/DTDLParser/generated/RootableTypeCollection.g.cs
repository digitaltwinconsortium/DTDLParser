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
    /// A collection of rootable DTDL types, which are the types permitted at the top level of a DTDL model.
    /// </summary>
    internal static partial class RootableTypeCollection
    {
        static RootableTypeCollection()
        {
            RootableTypeStrings = new Dictionary<int, HashSet<string>>();

            RootableTypeStrings[2] = new HashSet<string>();
            RootableTypeStrings[2].Add("Interface");

            RootableTypeStrings[3] = new HashSet<string>();
            RootableTypeStrings[3].Add("Interface");

            RootableTypeStrings[4] = new HashSet<string>();
            RootableTypeStrings[4].Add("Interface");

            RootableTypeDescriptions = new Dictionary<int, string>();
            RootableTypeDescriptions[2] = string.Join(" or ", RootableTypeStrings[2].Select(t => $"'{t}'"));
            RootableTypeDescriptions[3] = string.Join(" or ", RootableTypeStrings[3].Select(t => $"'{t}'"));
            RootableTypeDescriptions[4] = string.Join(" or ", RootableTypeStrings[4].Select(t => $"'{t}'"));

            RootableTypeStrings[2].Add("dtmi:dtdl:class:Interface;2");

            RootableTypeStrings[3].Add("dtmi:dtdl:class:Interface;3");

            RootableTypeStrings[4].Add("dtmi:dtdl:class:Interface;4");
        }
    }
}
