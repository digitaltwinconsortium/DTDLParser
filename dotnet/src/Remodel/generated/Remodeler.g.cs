/* This is an auto-generated file.  Do not modify. */

namespace DTDLParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    using DTDLParser.Models;

    /// <summary>
    /// Partial class that performs the main operations of upgrading a model to the latest version of DTDL.
    /// </summary>
    public partial class Remodeler
    {
        /// <summary>
        /// The version of DTDL to which models will be upgraded.
        /// </summary>
        public const int TargetDtdlVersion = 3;

        private IReadOnlyDictionary<Dtmi, DTEntityInfo> model;

        static Remodeler()
        {
            termIriRegexes = new List<string>();
            termIriRegexes.Add(@"dtmi:standard:class:(\w*);2");
            termIriRegexes.Add(@"dtmi:standard:unit:(\w*);2");
            termIriRegexes.Add(@"dtmi:dtdl:class:(\w*);2");
            termIriRegexes.Add(@"dtmi:dtdl:instance:CommandType:(\w*);2");
            termIriRegexes.Add(@"dtmi:standard:unitprefix:(\w*);2");
            termIriRegexes.Add(@"dtmi:dtdl:property:(\w*);2");
            termIriRegexes.Add(@"dtmi:dtdl:instance:Schema:(\w*);2");
            termIriRegexes.Add(@"dtmi:standard:schema:geospatial:(\w*);2");
            termIriRegexes.Add(@"dtmi:dtdl:class:(\w*);3");
            termIriRegexes.Add(@"dtmi:dtdl:property:(\w*);3");
            termIriRegexes.Add(@"dtmi:dtdl:instance:CommandType:(\w*);3");
            termIriRegexes.Add(@"dtmi:dtdl:instance:Schema:(\w*);3");
            termIriRegexes.Add(@"dtmi:dtdl:meta:(\w*);3");
            termIriRegexes.Add(@"dtmi:standard:schema:geospatial:(\w*);3");
            termIriRegexes.Add(@"dtmi:dtdl:extension:annotation:v1:ValueAnnotation:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:annotation:v1:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:historization:v1:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:overriding:v1:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:overriding:v1:Override:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:quantitativeTypes:v1:class:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:quantitativeTypes:v1:enum:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:quantitativeTypes:v1:unit:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:quantitativeTypes:v1:unitprefix:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:quantitativeTypes:v1:property:(\w*)");
            termIriRegexes.Add(@"dtmi:dtdl:extension:quantitativeTypes:v1:enum:PowerUnit");
            termIriRegexes.Add(@"dtmi:iotcentral:class:(\w*);2");
            termIriRegexes.Add(@"dtmi:iotcentral:schema:(\w*);2");

            reservedPrefixes = new List<string>();
            reservedPrefixes.Add("dtmi:dtdl:");
            reservedPrefixes.Add("dtmi:standard:");
            reservedPrefixes.Add("dtmi:iotcentral:");

            abstractPropertyConcreteTypeMap = new Dictionary<string, Dictionary<string, string>>();

            Dictionary<string, string> commandPayloadTypeMap = new Dictionary<string, string>();
            commandPayloadTypeMap["request"] = "CommandRequest";
            commandPayloadTypeMap["response"] = "CommandResponse";
            abstractPropertyConcreteTypeMap["CommandPayload"] = commandPayloadTypeMap;

            nonDependentRefPropertyNames = new HashSet<string>();
            nonDependentRefPropertyNames.Add("target");

            areKeywordsRestricted = true;

            featureSplitoutTypeMap = new Dictionary<string, HashSet<Dtmi>>();
            HashSet<Dtmi> quantitativeTypesTypeMap = new HashSet<Dtmi>();
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Acceleration;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Angle;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:AngularAcceleration;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:AngularVelocity;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Area;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:BinaryUnit;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Capacitance;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Current;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:DataRate;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:DataSize;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:DecimalUnit;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Density;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Distance;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:ElectricCharge;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Energy;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Force;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Frequency;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Humidity;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Illuminance;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Inductance;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Latitude;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Length;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Longitude;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Luminance;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Luminosity;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:LuminousFlux;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:LuminousIntensity;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:MagneticFlux;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:MagneticInduction;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Mass;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:MassFlowRate;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Power;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Pressure;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:QuantitativeType;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:RatioUnit;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:RelativeHumidity;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Resistance;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:SoundPressure;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Temperature;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Thrust;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:TimeSpan;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Torque;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Velocity;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Voltage;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:Volume;2"));
            quantitativeTypesTypeMap.Add(new Dtmi("dtmi:standard:class:VolumeFlowRate;2"));
            featureSplitoutTypeMap["dtmi:dtdl:extension:quantitativeTypes;1"] = quantitativeTypesTypeMap;

            partnerMaxVersions = new Dictionary<string, int>();
            partnerMaxVersions["iotcentral"] = 2;
        }

        private bool TryRemoveImproperKeyword(JProperty keywordProperty, string discriminantPropName)
        {
            switch (discriminantPropName)
            {
                case "description":
                case "displayName":
                    if (keywordProperty.Name == "@type")
                    {
                        keywordProperty.Remove();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case "comment":
                case "languageMajorVersion":
                case "enumValue":
                case "name":
                case "writable":
                case "maxMultiplicity":
                case "minMultiplicity":
                case "symbol":
                    if (keywordProperty.Name == "@language")
                    {
                        keywordProperty.Remove();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    if (keywordProperty.Name == "@value" || keywordProperty.Name == "@language")
                    {
                        keywordProperty.Remove();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }

        private bool TryValidateModel(out IReadOnlyDictionary<Dtmi, DTEntityInfo> model, out IList<ParsingError> parsingErrors, out IList<Dtmi> undefinedIdentifiers, bool allowUndefinedExtensions = false, int maxDtdlVersion = 0)
        {
            var parsingOptions = new ParsingOptions() { AllowUndefinedExtensions = allowUndefinedExtensions };
            if (maxDtdlVersion > 0)
            {
                parsingOptions.MaxDtdlVersion = maxDtdlVersion;
            }

            ModelParser parser = new ModelParser(parsingOptions);
            try
            {
                model = parser.Parse(modelComponents.Select(mc => mc.JsonText).Concat(skipComponents.Select(sc => sc.JsonText)));
                parsingErrors = null;
                undefinedIdentifiers = null;
                return true;
            }
            catch (ParsingException pe)
            {
                Console.WriteLine();
                Console.WriteLine($"MODEL INVALID -- {pe.Errors.Count} errors in model, including:");
                Console.WriteLine(pe.Errors.First().Message);
                model = null;
                parsingErrors = pe.Errors;
                undefinedIdentifiers = new List<Dtmi>();
                return false;
            }
            catch (ResolutionException re)
            {
                Console.WriteLine();
                Console.WriteLine($"MODEL INCOMPLETE -- {re.UndefinedIdentifiers.Count} undefined identifiers in model, including: {re.UndefinedIdentifiers.First()}");
                model = null;
                parsingErrors = new List<ParsingError>();
                undefinedIdentifiers = re.UndefinedIdentifiers;
                return false;
            }
        }
    }
}
