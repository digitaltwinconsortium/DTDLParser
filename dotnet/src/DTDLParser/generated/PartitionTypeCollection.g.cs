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
            PartitionTypeStrings.Add("dtmi:dtdl:class:Interface;4");

            PartitionMaxBytes = new Dictionary<int, Dictionary<string, int>>();
            PartitionMaxBytes[3] = new Dictionary<string, int>
            {
                { "", 1048576 },
            };

            PartitionMaxBytes[4] = new Dictionary<string, int>
            {
                { "", 1048576 },
                { "aio_1", 1048576 },
                { "onvif_1", 1048576 },
            };
        }
    }
}
