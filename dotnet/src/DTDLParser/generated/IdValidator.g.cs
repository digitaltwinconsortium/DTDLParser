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
    /// A static class for determining whether a string is a valid identifier.
    /// </summary>
    internal static partial class IdValidator
    {
        static IdValidator()
        {
            IdDefinitionMaxLengths[2] = new Dictionary<string, int>
            {
                { "", 2048 },
            };

            IdDefinitionRegexPatterns[2] = new Regex(@"^dtmi:[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?(?::[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?)*;[1-9][0-9]{0,8}$", RegexOptions.Compiled);
            IdDefinitionMaxLengths[3] = new Dictionary<string, int>
            {
                { "", 2048 },
            };

            IdDefinitionRegexPatterns[3] = new Regex(@"^dtmi:[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?(?::[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?)*(?:;[1-9][0-9]{0,8}(?:\.[1-9][0-9]{0,5})?)?$", RegexOptions.Compiled);
            IdDefinitionMaxLengths[4] = new Dictionary<string, int>
            {
                { "", 2048 },
                { "aio_1", 512 },
                { "onvif_1", 512 },
            };

            IdDefinitionRegexPatterns[4] = new Regex(@"^dtmi:[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?(?::[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?)*(?:;[1-9][0-9]{0,8}(?:\.[1-9][0-9]{0,5})?)?$", RegexOptions.Compiled);

            IdReferenceMaxLengths[2] = new Dictionary<string, int>
            {
                { "", 2048 },
            };

            IdReferenceRegexPatterns[2] = new Regex(@"^dtmi:[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?(?::[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?)*;[1-9][0-9]{0,8}$", RegexOptions.Compiled);
            IdReferenceMaxLengths[3] = new Dictionary<string, int>
            {
                { "", 2048 },
            };

            IdReferenceRegexPatterns[3] = new Regex(@"^dtmi:[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?(?::[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?)*(?:;[1-9][0-9]{0,8}(?:\.[1-9][0-9]{0,5})?)?$", RegexOptions.Compiled);
            IdReferenceMaxLengths[4] = new Dictionary<string, int>
            {
                { "", 2048 },
                { "aio_1", 512 },
                { "onvif_1", 512 },
            };

            IdReferenceRegexPatterns[4] = new Regex(@"^dtmi:[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?(?::[A-Za-z](?:[A-Za-z0-9_]*[A-Za-z0-9])?)*(?:;[1-9][0-9]{0,8}(?:\.[1-9][0-9]{0,5})?)?$", RegexOptions.Compiled);
        }
    }
}
