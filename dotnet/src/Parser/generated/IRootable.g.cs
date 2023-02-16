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
    /// Interface for additional methods on elements that are allowed at the root of a model file.
    /// </summary>
    public interface IRootable
    {
        /// <summary>
        /// Gets a <c>JsonElement</c> that holds the portion of the DTDL model that defines this rootable element.
        /// </summary>
        /// <returns>A <c>JsonElement</c> object containing the JSON structure.</returns>
        JsonElement GetJsonLd();

        /// <summary>
        /// Gets a JSON string that holds the portion of the DTDL model that defines this rootable element.
        /// </summary>
        /// <returns>A string containing the JSON text.</returns>
        string GetJsonLdText();
    }
}
