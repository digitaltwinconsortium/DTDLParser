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
    /// Class containing information about an object property being parsed.
    /// </summary>
    internal partial class ParsedObjectPropertyInfo
    {
        /// <summary>
        /// Gets or sets a collection of <c>DTEntityKind</c> indicating the expected kinds for this property.
        /// </summary>
        internal HashSet<DTEntityKind> ExpectedKinds { get; set; }
    }
}
