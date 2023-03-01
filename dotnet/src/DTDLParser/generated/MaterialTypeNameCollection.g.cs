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
    /// A collection of all material type names.
    /// </summary>
    internal static class MaterialTypeNameCollection
    {
        private static readonly HashSet<string> TypeNames = new HashSet<string>();

        static MaterialTypeNameCollection()
        {
            TypeNames.Add("Array");
            TypeNames.Add("dtmi:dtdl:class:Array;2");
            TypeNames.Add("dtmi:dtdl:class:Array;3");
            TypeNames.Add("Boolean");
            TypeNames.Add("dtmi:dtdl:class:Boolean;2");
            TypeNames.Add("dtmi:dtdl:class:Boolean;3");
            TypeNames.Add("Command");
            TypeNames.Add("dtmi:dtdl:class:Command;2");
            TypeNames.Add("dtmi:dtdl:class:Command;3");
            TypeNames.Add("CommandPayload");
            TypeNames.Add("dtmi:dtdl:class:CommandPayload;2");
            TypeNames.Add("dtmi:dtdl:class:CommandPayload;3");
            TypeNames.Add("CommandRequest");
            TypeNames.Add("dtmi:dtdl:class:CommandRequest;3");
            TypeNames.Add("CommandResponse");
            TypeNames.Add("dtmi:dtdl:class:CommandResponse;3");
            TypeNames.Add("CommandType");
            TypeNames.Add("dtmi:dtdl:class:CommandType;2");
            TypeNames.Add("dtmi:dtdl:class:CommandType;3");
            TypeNames.Add("ComplexSchema");
            TypeNames.Add("dtmi:dtdl:class:ComplexSchema;2");
            TypeNames.Add("dtmi:dtdl:class:ComplexSchema;3");
            TypeNames.Add("Component");
            TypeNames.Add("dtmi:dtdl:class:Component;2");
            TypeNames.Add("dtmi:dtdl:class:Component;3");
            TypeNames.Add("Content");
            TypeNames.Add("dtmi:dtdl:class:Content;2");
            TypeNames.Add("dtmi:dtdl:class:Content;3");
            TypeNames.Add("Date");
            TypeNames.Add("dtmi:dtdl:class:Date;2");
            TypeNames.Add("dtmi:dtdl:class:Date;3");
            TypeNames.Add("DateTime");
            TypeNames.Add("dtmi:dtdl:class:DateTime;2");
            TypeNames.Add("dtmi:dtdl:class:DateTime;3");
            TypeNames.Add("Double");
            TypeNames.Add("dtmi:dtdl:class:Double;2");
            TypeNames.Add("dtmi:dtdl:class:Double;3");
            TypeNames.Add("Duration");
            TypeNames.Add("dtmi:dtdl:class:Duration;2");
            TypeNames.Add("dtmi:dtdl:class:Duration;3");
            TypeNames.Add("Entity");
            TypeNames.Add("dtmi:dtdl:class:Entity;2");
            TypeNames.Add("dtmi:dtdl:class:Entity;3");
            TypeNames.Add("Enum");
            TypeNames.Add("dtmi:dtdl:class:Enum;2");
            TypeNames.Add("dtmi:dtdl:class:Enum;3");
            TypeNames.Add("EnumValue");
            TypeNames.Add("dtmi:dtdl:class:EnumValue;2");
            TypeNames.Add("dtmi:dtdl:class:EnumValue;3");
            TypeNames.Add("Field");
            TypeNames.Add("dtmi:dtdl:class:Field;2");
            TypeNames.Add("dtmi:dtdl:class:Field;3");
            TypeNames.Add("Float");
            TypeNames.Add("dtmi:dtdl:class:Float;2");
            TypeNames.Add("dtmi:dtdl:class:Float;3");
            TypeNames.Add("Integer");
            TypeNames.Add("dtmi:dtdl:class:Integer;2");
            TypeNames.Add("dtmi:dtdl:class:Integer;3");
            TypeNames.Add("Interface");
            TypeNames.Add("dtmi:dtdl:class:Interface;2");
            TypeNames.Add("dtmi:dtdl:class:Interface;3");
            TypeNames.Add("LatentType");
            TypeNames.Add("dtmi:dtdl:class:LatentType;3");
            TypeNames.Add("Long");
            TypeNames.Add("dtmi:dtdl:class:Long;2");
            TypeNames.Add("dtmi:dtdl:class:Long;3");
            TypeNames.Add("Map");
            TypeNames.Add("dtmi:dtdl:class:Map;2");
            TypeNames.Add("dtmi:dtdl:class:Map;3");
            TypeNames.Add("MapKey");
            TypeNames.Add("dtmi:dtdl:class:MapKey;2");
            TypeNames.Add("dtmi:dtdl:class:MapKey;3");
            TypeNames.Add("MapValue");
            TypeNames.Add("dtmi:dtdl:class:MapValue;2");
            TypeNames.Add("dtmi:dtdl:class:MapValue;3");
            TypeNames.Add("NamedEntity");
            TypeNames.Add("dtmi:dtdl:class:NamedEntity;2");
            TypeNames.Add("dtmi:dtdl:class:NamedEntity;3");
            TypeNames.Add("NamedLatentType");
            TypeNames.Add("dtmi:dtdl:class:NamedLatentType;3");
            TypeNames.Add("NumericSchema");
            TypeNames.Add("dtmi:dtdl:class:NumericSchema;2");
            TypeNames.Add("dtmi:dtdl:class:NumericSchema;3");
            TypeNames.Add("Object");
            TypeNames.Add("dtmi:dtdl:class:Object;2");
            TypeNames.Add("dtmi:dtdl:class:Object;3");
            TypeNames.Add("PrimitiveSchema");
            TypeNames.Add("dtmi:dtdl:class:PrimitiveSchema;2");
            TypeNames.Add("dtmi:dtdl:class:PrimitiveSchema;3");
            TypeNames.Add("Property");
            TypeNames.Add("dtmi:dtdl:class:Property;2");
            TypeNames.Add("dtmi:dtdl:class:Property;3");
            TypeNames.Add("Relationship");
            TypeNames.Add("dtmi:dtdl:class:Relationship;2");
            TypeNames.Add("dtmi:dtdl:class:Relationship;3");
            TypeNames.Add("Schema");
            TypeNames.Add("dtmi:dtdl:class:Schema;2");
            TypeNames.Add("dtmi:dtdl:class:Schema;3");
            TypeNames.Add("SchemaField");
            TypeNames.Add("dtmi:dtdl:class:SchemaField;2");
            TypeNames.Add("dtmi:dtdl:class:SchemaField;3");
            TypeNames.Add("String");
            TypeNames.Add("dtmi:dtdl:class:String;2");
            TypeNames.Add("dtmi:dtdl:class:String;3");
            TypeNames.Add("Telemetry");
            TypeNames.Add("dtmi:dtdl:class:Telemetry;2");
            TypeNames.Add("dtmi:dtdl:class:Telemetry;3");
            TypeNames.Add("TemporalSchema");
            TypeNames.Add("dtmi:dtdl:class:TemporalSchema;2");
            TypeNames.Add("dtmi:dtdl:class:TemporalSchema;3");
            TypeNames.Add("Time");
            TypeNames.Add("dtmi:dtdl:class:Time;2");
            TypeNames.Add("dtmi:dtdl:class:Time;3");
            TypeNames.Add("Unit");
            TypeNames.Add("dtmi:dtdl:class:Unit;2");
            TypeNames.Add("dtmi:dtdl:class:Unit;3");
            TypeNames.Add("UnitAttribute");
            TypeNames.Add("dtmi:dtdl:class:UnitAttribute;2");
            TypeNames.Add("dtmi:dtdl:class:UnitAttribute;3");
        }

        /// <summary>
        /// Indicates whether a given type is material or supplemental.
        /// </summary>
        /// <param name="typeString">The type to check.</param>
        /// <returns>True if the type is material.</returns>
        internal static bool IsMaterialType(string typeString)
        {
            return TypeNames.Contains(typeString);
        }
    }
}
