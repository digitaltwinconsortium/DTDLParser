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
    /// Class <c>ParsingOptions</c> defines properties that can be passed into the <see cref="ModelParser"/> constructor to configure its behavior.
    /// </summary>
    public partial class ParsingOptions
    {
        /// <summary>
        /// The highest version of DTDL understood by this version of <see cref="ModelParser"/>.
        /// </summary>
        public const int MaxKnownDtdlVersion = 3;

        /// <summary>
        /// Gets or sets an integer value that restricts the highest DTDL version the parser should accept; if a higher version model is submitted, a <see cref="ParsingException"/> will be thrown with a <see cref="ParsingError"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:disallowedContextVersion.
        /// </summary>
        /// <remarks>
        /// The default value is 3, because this is the highest version of DTDL understood by this version of <see cref="ModelParser"/>.
        /// </remarks>
        public int MaxDtdlVersion { get; set; } = MaxKnownDtdlVersion;
    }
}
