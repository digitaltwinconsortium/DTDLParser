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
        /// The lowest version of DTDL understood by this version of <see cref="ModelParser"/>.
        /// </summary>
        public const int MinKnownDtdlVersion = 2;

        /// <summary>
        /// Gets or sets an integer value that restricts the highest DTDL version the parser should accept; if a higher version model is submitted, a <see cref="ParsingException"/> will be thrown with a <see cref="ParsingError"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:disallowedContextVersion.
        /// </summary>
        /// <remarks>
        /// The default value is 3, because this is the highest version of DTDL understood by this version of <see cref="ModelParser"/>.
        /// </remarks>
        public int MaxDtdlVersion { get; set; } = MaxKnownDtdlVersion;

        /// <summary>
        /// Gets or sets a value indicating whether and when the parser should continue parsing if it encounters a reference to an extension that cannot be resolved.
        /// If this property is <see cref="WhenToAllow.Never"/>, an undefined extension context in a model will result in a <see cref="ParsingException"/> with a <see cref="ParsingError"/> indicating a <c>ValidationID</c> of dtmi:dtdl:parsingError:unresolvableContextSpecifier or dtmi:dtdl:parsingError:unresolvableContextVersion.
        /// If this property is <see cref="WhenToAllow.Always"/>, an undefined extension context in a model will not interrupt parsing, and furthermore the presence of this undefined context will allow the model to use undefined co-types and to use undefined properties in elements that have undefined co-types.
        /// If this property is not set or is set to <see cref="WhenToAllow.PerDefault"/>, the parsing behavior is determined according to the version of the DTDL context specified by the model.
        /// </summary>
        /// <remarks>
        /// For DTDL v2, the default behavior is to allow undefined extensions.
        /// For DTDL v3, the default behavior is to disallow undefined extensions.
        /// </remarks>
        public WhenToAllow AllowUndefinedExtensions { get; set; } = WhenToAllow.PerDefault;
    }
}
