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
    /// Indicates the kind of Entity, meaning the concrete DTDL type assigned to the corresponding element in the model.
    /// </summary>
    public enum DTEntityKind
    {
        /// <summary>The kind of the Entity is Array.</summary>
        Array,

        /// <summary>The kind of the Entity is Boolean.</summary>
        Boolean,

        /// <summary>The kind of the Entity is Command.</summary>
        Command,

        /// <summary>The kind of the Entity is CommandPayload.</summary>
        CommandPayload,

        /// <summary>The kind of the Entity is CommandRequest.</summary>
        CommandRequest,

        /// <summary>The kind of the Entity is CommandResponse.</summary>
        CommandResponse,

        /// <summary>The kind of the Entity is CommandType.</summary>
        CommandType,

        /// <summary>The kind of the Entity is Component.</summary>
        Component,

        /// <summary>The kind of the Entity is Date.</summary>
        Date,

        /// <summary>The kind of the Entity is DateTime.</summary>
        DateTime,

        /// <summary>The kind of the Entity is Double.</summary>
        Double,

        /// <summary>The kind of the Entity is Duration.</summary>
        Duration,

        /// <summary>The kind of the Entity is Enum.</summary>
        Enum,

        /// <summary>The kind of the Entity is EnumValue.</summary>
        EnumValue,

        /// <summary>The kind of the Entity is Field.</summary>
        Field,

        /// <summary>The kind of the Entity is Float.</summary>
        Float,

        /// <summary>The kind of the Entity is Integer.</summary>
        Integer,

        /// <summary>The kind of the Entity is Interface.</summary>
        Interface,

        /// <summary>The kind of the Entity is LatentType.</summary>
        LatentType,

        /// <summary>The kind of the Entity is Long.</summary>
        Long,

        /// <summary>The kind of the Entity is Map.</summary>
        Map,

        /// <summary>The kind of the Entity is MapKey.</summary>
        MapKey,

        /// <summary>The kind of the Entity is MapValue.</summary>
        MapValue,

        /// <summary>The kind of the Entity is NamedLatentType.</summary>
        NamedLatentType,

        /// <summary>The kind of the Entity is Object.</summary>
        Object,

        /// <summary>The kind of the Entity is Property.</summary>
        Property,

        /// <summary>The kind of the Entity is Reference.</summary>
        Reference,

        /// <summary>The kind of the Entity is Relationship.</summary>
        Relationship,

        /// <summary>The kind of the Entity is String.</summary>
        String,

        /// <summary>The kind of the Entity is Telemetry.</summary>
        Telemetry,

        /// <summary>The kind of the Entity is Time.</summary>
        Time,

        /// <summary>The kind of the Entity is Unit.</summary>
        Unit,

        /// <summary>The kind of the Entity is UnitAttribute.</summary>
        UnitAttribute,
    }
}
