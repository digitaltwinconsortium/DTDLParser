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
    /// Indicates the kind of extension.
    /// </summary>
    /// <remarks>
    /// DTDL has a limited number of types that can be subtyped by DTDL language extensions.
    /// Values of this enum are returned by the <see cref="DTSupplementalTypeInfo.ExtensionKind"/> property to indicate the extensible DTDL type that is an ancestor of the supplemental type.
    /// </remarks>
    public enum DTExtensionKind
    {
        /// <summary>The kind of extension is None.</summary>
        None,

        /// <summary>The kind of extension is SemanticType.</summary>
        SemanticType,

        /// <summary>The kind of extension is SemanticUnit.</summary>
        SemanticUnit,

        /// <summary>The kind of extension is Unit.</summary>
        Unit,

        /// <summary>The kind of extension is UnitAttribute.</summary>
        UnitAttribute,

        /// <summary>The kind of extension is AdjunctType.</summary>
        AdjunctType,

        /// <summary>The kind of extension is LatentType.</summary>
        LatentType,

        /// <summary>The kind of extension is NamedLatentType.</summary>
        NamedLatentType,
    }
}
