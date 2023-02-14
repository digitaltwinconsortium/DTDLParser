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
    /// A collection of partition DTDL types, which are the types that are outlined from a DTDL model.
    /// </summary>
    internal static partial class PartitionTypeCollection
    {
        static PartitionTypeCollection()
        {
            PartitionTypeStrings = new HashSet<string>();
            PartitionTypeStrings.Add("Interface");

            PartitionTypeDescription = string.Join(" or ", PartitionTypeStrings.Select(t => $"'{t}'"));

            PartitionTypeStrings.Add("dtmi:dtdl:class:Interface;2");
            PartitionTypeStrings.Add("dtmi:dtdl:class:Interface;3");

            PartitionMaxBytes = new Dictionary<int, int>();
            PartitionMaxBytes[3] = 1048576;
        }
    }
}
