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
    /// A collection of DTDL types that are not materialized as C# classes.
    /// </summary>
    internal partial class SupplementalTypeCollection
    {
        static SupplementalTypeCollection()
        {
            EndogenousSupplementalTypes = new Dictionary<Dtmi, DTSupplementalTypeInfo>();

            Dtmi dtdlContextIdV2 = new Dtmi("dtmi:dtdl:context;2");
            Dtmi dtdlContextIdV3 = new Dtmi("dtmi:dtdl:context;3");
            Dtmi dtdlExtensionAnnotationContextIdV1 = new Dtmi("dtmi:dtdl:extension:annotation;1");
            Dtmi dtdlExtensionHistorizationContextIdV1 = new Dtmi("dtmi:dtdl:extension:historization;1");
            Dtmi dtdlExtensionMqttContextIdV1 = new Dtmi("dtmi:dtdl:extension:mqtt;1");
            Dtmi dtdlExtensionOverridingContextIdV1 = new Dtmi("dtmi:dtdl:extension:overriding;1");
            Dtmi dtdlExtensionQuantitativeTypesContextIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes;1");
            Dtmi iotcentralContextIdV2 = new Dtmi("dtmi:iotcentral:context;2");

            Dtmi adjunctTypeTypeIdV3 = new Dtmi("dtmi:dtdl:class:AdjunctType;3");
            Dtmi aliasTypeIdV3 = new Dtmi("dtmi:dtdl:class:Alias;3");
            Dtmi latentTypeTypeIdV3 = new Dtmi("dtmi:dtdl:class:LatentType;3");
            Dtmi namedLatentTypeTypeIdV3 = new Dtmi("dtmi:dtdl:class:NamedLatentType;3");
            Dtmi semanticTypeTypeIdV2 = new Dtmi("dtmi:dtdl:class:SemanticType;2");
            Dtmi semanticTypeTypeIdV3 = new Dtmi("dtmi:dtdl:class:SemanticType;3");
            Dtmi semanticUnitTypeIdV2 = new Dtmi("dtmi:dtdl:class:SemanticUnit;2");
            Dtmi semanticUnitTypeIdV3 = new Dtmi("dtmi:dtdl:class:SemanticUnit;3");
            Dtmi unitTypeIdV2 = new Dtmi("dtmi:dtdl:class:Unit;2");
            Dtmi unitTypeIdV3 = new Dtmi("dtmi:dtdl:class:Unit;3");
            Dtmi unitAttributeTypeIdV2 = new Dtmi("dtmi:dtdl:class:UnitAttribute;2");
            Dtmi unitAttributeTypeIdV3 = new Dtmi("dtmi:dtdl:class:UnitAttribute;3");
            Dtmi valueAnnotationTypeIdV1 = new Dtmi("dtmi:dtdl:extension:annotation:v1:ValueAnnotation");
            Dtmi historizedTypeIdV1 = new Dtmi("dtmi:dtdl:extension:historization:v1:Historized");
            Dtmi cacheableTypeIdV1 = new Dtmi("dtmi:dtdl:extension:mqtt:v1:Cacheable");
            Dtmi idempotentTypeIdV1 = new Dtmi("dtmi:dtdl:extension:mqtt:v1:Idempotent");
            Dtmi indexedTypeIdV1 = new Dtmi("dtmi:dtdl:extension:mqtt:v1:Indexed");
            Dtmi mqttTypeIdV1 = new Dtmi("dtmi:dtdl:extension:mqtt:v1:Mqtt");
            Dtmi overrideTypeIdV1 = new Dtmi("dtmi:dtdl:extension:overriding:v1:Override");
            Dtmi accelerationTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Acceleration");
            Dtmi angleTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Angle");
            Dtmi angularAccelerationTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:AngularAcceleration");
            Dtmi angularVelocityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:AngularVelocity");
            Dtmi apparentEnergyTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:ApparentEnergy");
            Dtmi apparentPowerTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:ApparentPower");
            Dtmi areaTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Area");
            Dtmi binaryUnitTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:BinaryUnit");
            Dtmi capacitanceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Capacitance");
            Dtmi concentrationTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Concentration");
            Dtmi currentTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Current");
            Dtmi dataRateTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:DataRate");
            Dtmi dataSizeTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:DataSize");
            Dtmi decimalUnitTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:DecimalUnit");
            Dtmi densityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Density");
            Dtmi distanceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Distance");
            Dtmi electricChargeTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:ElectricCharge");
            Dtmi energyTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Energy");
            Dtmi energyRateTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:EnergyRate");
            Dtmi forceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Force");
            Dtmi frequencyTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Frequency");
            Dtmi humidityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Humidity");
            Dtmi illuminanceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Illuminance");
            Dtmi inductanceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Inductance");
            Dtmi ionizingRadiationDoseTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:IonizingRadiationDose");
            Dtmi irradianceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Irradiance");
            Dtmi latitudeTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Latitude");
            Dtmi lengthTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Length");
            Dtmi longitudeTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Longitude");
            Dtmi luminanceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Luminance");
            Dtmi luminosityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Luminosity");
            Dtmi luminousFluxTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:LuminousFlux");
            Dtmi luminousIntensityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:LuminousIntensity");
            Dtmi magneticFluxTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:MagneticFlux");
            Dtmi magneticInductionTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:MagneticInduction");
            Dtmi massTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Mass");
            Dtmi massFlowRateTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:MassFlowRate");
            Dtmi powerTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Power");
            Dtmi pressureTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Pressure");
            Dtmi quantitativeTypeTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:QuantitativeType");
            Dtmi radioactivityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Radioactivity");
            Dtmi ratioUnitTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:RatioUnit");
            Dtmi reactiveEnergyTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:ReactiveEnergy");
            Dtmi reactivePowerTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:ReactivePower");
            Dtmi relativeDensityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:RelativeDensity");
            Dtmi relativeHumidityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:RelativeHumidity");
            Dtmi resistanceTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Resistance");
            Dtmi soundPressureTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:SoundPressure");
            Dtmi symbolicUnitTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:SymbolicUnit");
            Dtmi temperatureTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Temperature");
            Dtmi thrustTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Thrust");
            Dtmi timeSpanTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:TimeSpan");
            Dtmi torqueTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Torque");
            Dtmi unitPrefixTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:UnitPrefix");
            Dtmi velocityTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Velocity");
            Dtmi voltageTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Voltage");
            Dtmi volumeTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:Volume");
            Dtmi volumeFlowRateTypeIdV1 = new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:class:VolumeFlowRate");
            Dtmi accelerationVectorTypeIdV2 = new Dtmi("dtmi:iotcentral:class:AccelerationVector;2");
            Dtmi eventTypeIdV2 = new Dtmi("dtmi:iotcentral:class:Event;2");
            Dtmi locationTypeIdV2 = new Dtmi("dtmi:iotcentral:class:Location;2");
            Dtmi stateTypeIdV2 = new Dtmi("dtmi:iotcentral:class:State;2");
            Dtmi velocityVectorTypeIdV2 = new Dtmi("dtmi:iotcentral:class:VelocityVector;2");
            Dtmi accelerationTypeIdV2 = new Dtmi("dtmi:standard:class:Acceleration;2");
            Dtmi accelerationUnitTypeIdV2 = new Dtmi("dtmi:standard:class:AccelerationUnit;2");
            Dtmi angleTypeIdV2 = new Dtmi("dtmi:standard:class:Angle;2");
            Dtmi angleUnitTypeIdV2 = new Dtmi("dtmi:standard:class:AngleUnit;2");
            Dtmi angularAccelerationTypeIdV2 = new Dtmi("dtmi:standard:class:AngularAcceleration;2");
            Dtmi angularAccelerationUnitTypeIdV2 = new Dtmi("dtmi:standard:class:AngularAccelerationUnit;2");
            Dtmi angularVelocityTypeIdV2 = new Dtmi("dtmi:standard:class:AngularVelocity;2");
            Dtmi angularVelocityUnitTypeIdV2 = new Dtmi("dtmi:standard:class:AngularVelocityUnit;2");
            Dtmi areaTypeIdV2 = new Dtmi("dtmi:standard:class:Area;2");
            Dtmi areaUnitTypeIdV2 = new Dtmi("dtmi:standard:class:AreaUnit;2");
            Dtmi binaryPrefixTypeIdV2 = new Dtmi("dtmi:standard:class:BinaryPrefix;2");
            Dtmi binaryUnitTypeIdV2 = new Dtmi("dtmi:standard:class:BinaryUnit;2");
            Dtmi capacitanceTypeIdV2 = new Dtmi("dtmi:standard:class:Capacitance;2");
            Dtmi capacitanceUnitTypeIdV2 = new Dtmi("dtmi:standard:class:CapacitanceUnit;2");
            Dtmi chargeUnitTypeIdV2 = new Dtmi("dtmi:standard:class:ChargeUnit;2");
            Dtmi currentTypeIdV2 = new Dtmi("dtmi:standard:class:Current;2");
            Dtmi currentUnitTypeIdV2 = new Dtmi("dtmi:standard:class:CurrentUnit;2");
            Dtmi dataRateTypeIdV2 = new Dtmi("dtmi:standard:class:DataRate;2");
            Dtmi dataRateUnitTypeIdV2 = new Dtmi("dtmi:standard:class:DataRateUnit;2");
            Dtmi dataSizeTypeIdV2 = new Dtmi("dtmi:standard:class:DataSize;2");
            Dtmi dataSizeUnitTypeIdV2 = new Dtmi("dtmi:standard:class:DataSizeUnit;2");
            Dtmi decimalPrefixTypeIdV2 = new Dtmi("dtmi:standard:class:DecimalPrefix;2");
            Dtmi decimalUnitTypeIdV2 = new Dtmi("dtmi:standard:class:DecimalUnit;2");
            Dtmi densityTypeIdV2 = new Dtmi("dtmi:standard:class:Density;2");
            Dtmi densityUnitTypeIdV2 = new Dtmi("dtmi:standard:class:DensityUnit;2");
            Dtmi distanceTypeIdV2 = new Dtmi("dtmi:standard:class:Distance;2");
            Dtmi electricChargeTypeIdV2 = new Dtmi("dtmi:standard:class:ElectricCharge;2");
            Dtmi energyTypeIdV2 = new Dtmi("dtmi:standard:class:Energy;2");
            Dtmi energyUnitTypeIdV2 = new Dtmi("dtmi:standard:class:EnergyUnit;2");
            Dtmi forceTypeIdV2 = new Dtmi("dtmi:standard:class:Force;2");
            Dtmi forceUnitTypeIdV2 = new Dtmi("dtmi:standard:class:ForceUnit;2");
            Dtmi frequencyTypeIdV2 = new Dtmi("dtmi:standard:class:Frequency;2");
            Dtmi frequencyUnitTypeIdV2 = new Dtmi("dtmi:standard:class:FrequencyUnit;2");
            Dtmi humidityTypeIdV2 = new Dtmi("dtmi:standard:class:Humidity;2");
            Dtmi illuminanceTypeIdV2 = new Dtmi("dtmi:standard:class:Illuminance;2");
            Dtmi illuminanceUnitTypeIdV2 = new Dtmi("dtmi:standard:class:IlluminanceUnit;2");
            Dtmi inductanceTypeIdV2 = new Dtmi("dtmi:standard:class:Inductance;2");
            Dtmi inductanceUnitTypeIdV2 = new Dtmi("dtmi:standard:class:InductanceUnit;2");
            Dtmi latitudeTypeIdV2 = new Dtmi("dtmi:standard:class:Latitude;2");
            Dtmi lengthTypeIdV2 = new Dtmi("dtmi:standard:class:Length;2");
            Dtmi lengthUnitTypeIdV2 = new Dtmi("dtmi:standard:class:LengthUnit;2");
            Dtmi longitudeTypeIdV2 = new Dtmi("dtmi:standard:class:Longitude;2");
            Dtmi luminanceTypeIdV2 = new Dtmi("dtmi:standard:class:Luminance;2");
            Dtmi luminanceUnitTypeIdV2 = new Dtmi("dtmi:standard:class:LuminanceUnit;2");
            Dtmi luminosityTypeIdV2 = new Dtmi("dtmi:standard:class:Luminosity;2");
            Dtmi luminousFluxTypeIdV2 = new Dtmi("dtmi:standard:class:LuminousFlux;2");
            Dtmi luminousFluxUnitTypeIdV2 = new Dtmi("dtmi:standard:class:LuminousFluxUnit;2");
            Dtmi luminousIntensityTypeIdV2 = new Dtmi("dtmi:standard:class:LuminousIntensity;2");
            Dtmi luminousIntensityUnitTypeIdV2 = new Dtmi("dtmi:standard:class:LuminousIntensityUnit;2");
            Dtmi magneticFluxTypeIdV2 = new Dtmi("dtmi:standard:class:MagneticFlux;2");
            Dtmi magneticFluxUnitTypeIdV2 = new Dtmi("dtmi:standard:class:MagneticFluxUnit;2");
            Dtmi magneticInductionTypeIdV2 = new Dtmi("dtmi:standard:class:MagneticInduction;2");
            Dtmi magneticInductionUnitTypeIdV2 = new Dtmi("dtmi:standard:class:MagneticInductionUnit;2");
            Dtmi massTypeIdV2 = new Dtmi("dtmi:standard:class:Mass;2");
            Dtmi massFlowRateTypeIdV2 = new Dtmi("dtmi:standard:class:MassFlowRate;2");
            Dtmi massFlowRateUnitTypeIdV2 = new Dtmi("dtmi:standard:class:MassFlowRateUnit;2");
            Dtmi massUnitTypeIdV2 = new Dtmi("dtmi:standard:class:MassUnit;2");
            Dtmi powerTypeIdV2 = new Dtmi("dtmi:standard:class:Power;2");
            Dtmi powerUnitTypeIdV2 = new Dtmi("dtmi:standard:class:PowerUnit;2");
            Dtmi pressureTypeIdV2 = new Dtmi("dtmi:standard:class:Pressure;2");
            Dtmi pressureUnitTypeIdV2 = new Dtmi("dtmi:standard:class:PressureUnit;2");
            Dtmi quantitativeTypeTypeIdV2 = new Dtmi("dtmi:standard:class:QuantitativeType;2");
            Dtmi ratioUnitTypeIdV2 = new Dtmi("dtmi:standard:class:RatioUnit;2");
            Dtmi relativeHumidityTypeIdV2 = new Dtmi("dtmi:standard:class:RelativeHumidity;2");
            Dtmi resistanceTypeIdV2 = new Dtmi("dtmi:standard:class:Resistance;2");
            Dtmi resistanceUnitTypeIdV2 = new Dtmi("dtmi:standard:class:ResistanceUnit;2");
            Dtmi soundPressureTypeIdV2 = new Dtmi("dtmi:standard:class:SoundPressure;2");
            Dtmi soundPressureUnitTypeIdV2 = new Dtmi("dtmi:standard:class:SoundPressureUnit;2");
            Dtmi temperatureTypeIdV2 = new Dtmi("dtmi:standard:class:Temperature;2");
            Dtmi temperatureUnitTypeIdV2 = new Dtmi("dtmi:standard:class:TemperatureUnit;2");
            Dtmi thrustTypeIdV2 = new Dtmi("dtmi:standard:class:Thrust;2");
            Dtmi timeSpanTypeIdV2 = new Dtmi("dtmi:standard:class:TimeSpan;2");
            Dtmi timeUnitTypeIdV2 = new Dtmi("dtmi:standard:class:TimeUnit;2");
            Dtmi torqueTypeIdV2 = new Dtmi("dtmi:standard:class:Torque;2");
            Dtmi torqueUnitTypeIdV2 = new Dtmi("dtmi:standard:class:TorqueUnit;2");
            Dtmi unitlessTypeIdV2 = new Dtmi("dtmi:standard:class:Unitless;2");
            Dtmi velocityTypeIdV2 = new Dtmi("dtmi:standard:class:Velocity;2");
            Dtmi velocityUnitTypeIdV2 = new Dtmi("dtmi:standard:class:VelocityUnit;2");
            Dtmi voltageTypeIdV2 = new Dtmi("dtmi:standard:class:Voltage;2");
            Dtmi voltageUnitTypeIdV2 = new Dtmi("dtmi:standard:class:VoltageUnit;2");
            Dtmi volumeTypeIdV2 = new Dtmi("dtmi:standard:class:Volume;2");
            Dtmi volumeFlowRateTypeIdV2 = new Dtmi("dtmi:standard:class:VolumeFlowRate;2");
            Dtmi volumeFlowRateUnitTypeIdV2 = new Dtmi("dtmi:standard:class:VolumeFlowRateUnit;2");
            Dtmi volumeUnitTypeIdV2 = new Dtmi("dtmi:standard:class:VolumeUnit;2");

            DTSupplementalTypeInfo adjunctTypeInfoV3 = new DTSupplementalTypeInfo(DTExtensionKind.None, dtdlContextIdV3, adjunctTypeTypeIdV3, isAbstract: true, isMergeable: false, null);
            adjunctTypeInfoV3.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Array, DTEntityKind.Boolean, DTEntityKind.Command, DTEntityKind.CommandPayload, DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.CommandType, DTEntityKind.Component, DTEntityKind.Date, DTEntityKind.DateTime, DTEntityKind.Double, DTEntityKind.Duration, DTEntityKind.Enum, DTEntityKind.EnumValue, DTEntityKind.Field, DTEntityKind.Float, DTEntityKind.Integer, DTEntityKind.Interface, DTEntityKind.LatentType, DTEntityKind.Long, DTEntityKind.Map, DTEntityKind.MapKey, DTEntityKind.MapValue, DTEntityKind.NamedLatentType, DTEntityKind.Object, DTEntityKind.Property, DTEntityKind.Relationship, DTEntityKind.String, DTEntityKind.Telemetry, DTEntityKind.Time, DTEntityKind.Unit, DTEntityKind.UnitAttribute };
            adjunctTypeInfoV3.AllowedCotypeVersions = new HashSet<int>() { 2, 3 };

            DTSupplementalTypeInfo aliasInfoV3 = new DTSupplementalTypeInfo(DTExtensionKind.None, dtdlContextIdV3, aliasTypeIdV3, isAbstract: false, isMergeable: false, null);
            aliasInfoV3.AddProperty("dtmi:dtdl:property:aliasFor;3", new Dtmi("dtmi:dtdl:class:Entity;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            aliasInfoV3.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Array, DTEntityKind.Boolean, DTEntityKind.Command, DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.CommandType, DTEntityKind.Component, DTEntityKind.Date, DTEntityKind.DateTime, DTEntityKind.Double, DTEntityKind.Duration, DTEntityKind.Enum, DTEntityKind.EnumValue, DTEntityKind.Field, DTEntityKind.Float, DTEntityKind.Integer, DTEntityKind.Interface, DTEntityKind.LatentType, DTEntityKind.Long, DTEntityKind.Map, DTEntityKind.MapKey, DTEntityKind.MapValue, DTEntityKind.NamedLatentType, DTEntityKind.Object, DTEntityKind.Property, DTEntityKind.Relationship, DTEntityKind.String, DTEntityKind.Telemetry, DTEntityKind.Time, DTEntityKind.Unit, DTEntityKind.UnitAttribute };
            aliasInfoV3.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo semanticTypeInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.None, dtdlContextIdV2, semanticTypeTypeIdV2, isAbstract: true, isMergeable: false, null);
            semanticTypeInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Relationship, DTEntityKind.Telemetry };
            semanticTypeInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo semanticTypeInfoV3 = new DTSupplementalTypeInfo(DTExtensionKind.None, dtdlContextIdV3, semanticTypeTypeIdV3, isAbstract: true, isMergeable: false, null);
            semanticTypeInfoV3.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Relationship, DTEntityKind.Telemetry };
            semanticTypeInfoV3.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo semanticUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.None, dtdlContextIdV2, semanticUnitTypeIdV2, isAbstract: true, isMergeable: false, null);
            semanticUnitInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Unit };
            semanticUnitInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo semanticUnitInfoV3 = new DTSupplementalTypeInfo(DTExtensionKind.None, dtdlContextIdV3, semanticUnitTypeIdV3, isAbstract: true, isMergeable: false, null);
            semanticUnitInfoV3.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Unit };
            semanticUnitInfoV3.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo valueAnnotationInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionAnnotationContextIdV1, valueAnnotationTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            valueAnnotationInfoV1.AddProperty("dtmi:dtdl:extension:annotation:v1:ValueAnnotation:annotates", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            valueAnnotationInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            valueAnnotationInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };
            valueAnnotationInfoV1.AddSiblingConstraint(new SiblingConstraint() { KeyPropertyName = "name", KeyrefPropertyId = new Dtmi("dtmi:dtdl:extension:annotation:v1:ValueAnnotation:annotates"), RequiredTypes = new List<Dtmi>() { new Dtmi("dtmi:dtdl:class:Property;3"), new Dtmi("dtmi:dtdl:class:Telemetry;3") }, RequiredTypesString = "Property or Telemetry", DisallowedType = new Dtmi("dtmi:dtdl:extension:annotation:v1:ValueAnnotation"), DisallowedTypeName = "ValueAnnotation" });

            DTSupplementalTypeInfo historizedInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionHistorizationContextIdV1, historizedTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            historizedInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            historizedInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo cacheableInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionMqttContextIdV1, cacheableTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            cacheableInfoV1.AddProperty("dtmi:dtdl:extension:mqtt:v1:Cacheable:ttl", new Uri("http://www.w3.org/2001/XMLSchema#duration"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            cacheableInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Command };
            cacheableInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };
            cacheableInfoV1.RequiredCocotypes = new HashSet<Dtmi>() { new Dtmi("dtmi:dtdl:extension:mqtt:v1:Idempotent") };

            DTSupplementalTypeInfo idempotentInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionMqttContextIdV1, idempotentTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            idempotentInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Command };
            idempotentInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo indexedInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionMqttContextIdV1, indexedTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            indexedInfoV1.AddProperty("dtmi:dtdl:extension:mqtt:v1:Indexed:index", new Uri("http://www.w3.org/2001/XMLSchema#integer"), 1, 1, null, 1, null, regex: null, hasUniqueValue: true, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            indexedInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.EnumValue, DTEntityKind.Field, DTEntityKind.Telemetry };
            indexedInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo mqttInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionMqttContextIdV1, mqttTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            mqttInfoV1.AddProperty("dtmi:dtdl:extension:mqtt:v1:Mqtt:commandTopic", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, null, null, null, null, regex: new Regex(@"^(?:(?:[!%-*,-.0-z|~][!$-*,-.0-z|~]*)|(?:{(?:[A-Za-z]+:)?[A-Za-z]+}))(?:\/(?:(?:[!$-*,-.0-z|~]+)|(?:{(?:[A-Za-z]+:)?[A-Za-z]+})))*$"), hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            mqttInfoV1.AddProperty("dtmi:dtdl:extension:mqtt:v1:Mqtt:payloadFormat", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            mqttInfoV1.AddProperty("dtmi:dtdl:extension:mqtt:v1:Mqtt:telemetryTopic", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, null, null, null, null, regex: new Regex(@"^(?:(?:[!%-*,-.0-z|~][!$-*,-.0-z|~]*)|(?:{(?:[A-Za-z]+:)?[A-Za-z]+}))(?:\/(?:(?:[!$-*,-.0-z|~]+)|(?:{(?:[A-Za-z]+:)?[A-Za-z]+})))*$"), hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            mqttInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Interface };
            mqttInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo overrideInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionOverridingContextIdV1, overrideTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            overrideInfoV1.AddProperty("dtmi:dtdl:extension:overriding:v1:Override:overrides", null, 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            overrideInfoV1.AddPropertyValueConstraint("schema", new ValueConstraint() { SiblingKeyPropertyName = "name", SiblingKeyrefPropertyId = new Dtmi("dtmi:dtdl:extension:annotation:v1:ValueAnnotation:annotates"), SiblingParentOfPropertyId = new Dtmi("dtmi:dtdl:extension:overriding:v1:Override:overrides") });
            overrideInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property };
            overrideInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };
            overrideInfoV1.RequiredCocotypes = new HashSet<Dtmi>() { new Dtmi("dtmi:dtdl:extension:annotation:v1:ValueAnnotation") };
            overrideInfoV1.DisallowedCocotypes = new HashSet<Dtmi>() { new Dtmi("dtmi:dtdl:extension:initialization:v1:Initialized") };
            overrideInfoV1.AddSiblingConstraint(new SiblingConstraint() { KeyPropertyName = "name", KeyrefPropertyId = new Dtmi("dtmi:dtdl:extension:annotation:v1:ValueAnnotation:annotates"), UniqueReference = new Dtmi("dtmi:dtdl:extension:overriding:v1:Override:overrides"), SupplementalPropertyPath = new Dtmi("dtmi:dtdl:extension:overriding:v1:Override:overrides") });
            overrideInfoV1.AddSiblingConstraint(new SiblingConstraint() { KeyPropertyName = "name", KeyrefPropertyId = new Dtmi("dtmi:dtdl:extension:annotation:v1:ValueAnnotation:annotates"), RequiredTypes = new List<Dtmi>() { new Dtmi("dtmi:dtdl:class:Property;3"), new Dtmi("dtmi:dtdl:class:Telemetry;3") }, RequiredTypesString = "Property or Telemetry" });

            DTSupplementalTypeInfo accelerationInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, accelerationTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            accelerationInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:AccelerationUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            accelerationInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            accelerationInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo angleInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, angleTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            angleInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:AngleUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            angleInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            angleInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo angularAccelerationInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, angularAccelerationTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            angularAccelerationInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:AngularAccelerationUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            angularAccelerationInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            angularAccelerationInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo angularVelocityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, angularVelocityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            angularVelocityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:AngularVelocityUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            angularVelocityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            angularVelocityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo apparentEnergyInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, apparentEnergyTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            apparentEnergyInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ApparentEnergyUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            apparentEnergyInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            apparentEnergyInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo apparentPowerInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, apparentPowerTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            apparentPowerInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ApparentPowerUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            apparentPowerInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            apparentPowerInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo areaInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, areaTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            areaInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:AreaUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            areaInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            areaInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo binaryUnitInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionQuantitativeTypesContextIdV1, binaryUnitTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            binaryUnitInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:baseUnit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            binaryUnitInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:prefix", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, null, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            binaryUnitInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Array, DTEntityKind.Boolean, DTEntityKind.Command, DTEntityKind.CommandPayload, DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.CommandType, DTEntityKind.Component, DTEntityKind.Date, DTEntityKind.DateTime, DTEntityKind.Double, DTEntityKind.Duration, DTEntityKind.Enum, DTEntityKind.EnumValue, DTEntityKind.Field, DTEntityKind.Float, DTEntityKind.Integer, DTEntityKind.Interface, DTEntityKind.LatentType, DTEntityKind.Long, DTEntityKind.Map, DTEntityKind.MapKey, DTEntityKind.MapValue, DTEntityKind.NamedLatentType, DTEntityKind.Object, DTEntityKind.Property, DTEntityKind.Relationship, DTEntityKind.String, DTEntityKind.Telemetry, DTEntityKind.Time, DTEntityKind.Unit, DTEntityKind.UnitAttribute };
            binaryUnitInfoV1.AllowedCotypeVersions = new HashSet<int>() { 2, 3 };

            DTSupplementalTypeInfo capacitanceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, capacitanceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            capacitanceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:CapacitanceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            capacitanceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            capacitanceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo concentrationInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, concentrationTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            concentrationInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:Unitless"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            concentrationInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            concentrationInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo currentInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, currentTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            currentInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:CurrentUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            currentInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            currentInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo dataRateInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, dataRateTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            dataRateInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:DataRateUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            dataRateInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            dataRateInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo dataSizeInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, dataSizeTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            dataSizeInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:DataSizeUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            dataSizeInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            dataSizeInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo decimalUnitInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionQuantitativeTypesContextIdV1, decimalUnitTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            decimalUnitInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:baseUnit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            decimalUnitInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:prefix", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, null, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            decimalUnitInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.EnumValue };
            decimalUnitInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo densityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, densityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            densityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:DensityUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            densityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            densityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo distanceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, distanceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            distanceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:LengthUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            distanceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            distanceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo electricChargeInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, electricChargeTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            electricChargeInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ChargeUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            electricChargeInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            electricChargeInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo energyInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, energyTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            energyInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:EnergyUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            energyInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            energyInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo energyRateInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, energyRateTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            energyRateInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:PowerUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            energyRateInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            energyRateInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo forceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, forceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            forceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ForceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            forceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            forceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo frequencyInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, frequencyTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            frequencyInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:FrequencyUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            frequencyInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            frequencyInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo humidityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, humidityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            humidityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:DensityUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            humidityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            humidityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo illuminanceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, illuminanceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            illuminanceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:IlluminanceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            illuminanceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            illuminanceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo inductanceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, inductanceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            inductanceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:InductanceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            inductanceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            inductanceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo ionizingRadiationDoseInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, ionizingRadiationDoseTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            ionizingRadiationDoseInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:IonizingRadiationDoseUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            ionizingRadiationDoseInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            ionizingRadiationDoseInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo irradianceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, irradianceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            irradianceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:IrradianceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            irradianceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            irradianceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo latitudeInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, latitudeTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            latitudeInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:AngleUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            latitudeInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            latitudeInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo lengthInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, lengthTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            lengthInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:LengthUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            lengthInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            lengthInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo longitudeInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, longitudeTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            longitudeInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:AngleUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            longitudeInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            longitudeInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo luminanceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, luminanceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            luminanceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:LuminanceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminanceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            luminanceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo luminosityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, luminosityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            luminosityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:PowerUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminosityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            luminosityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo luminousFluxInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, luminousFluxTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            luminousFluxInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:LuminousFluxUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminousFluxInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            luminousFluxInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo luminousIntensityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, luminousIntensityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            luminousIntensityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:LuminousIntensityUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminousIntensityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            luminousIntensityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo magneticFluxInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, magneticFluxTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            magneticFluxInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:MagneticFluxUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            magneticFluxInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            magneticFluxInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo magneticInductionInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, magneticInductionTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            magneticInductionInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:MagneticInductionUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            magneticInductionInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            magneticInductionInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo massInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, massTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            massInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:MassUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            massInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            massInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo massFlowRateInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, massFlowRateTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            massFlowRateInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:MassFlowRateUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            massFlowRateInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            massFlowRateInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo powerInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, powerTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            powerInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:PowerUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            powerInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            powerInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo pressureInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, pressureTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            pressureInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:PressureUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            pressureInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            pressureInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo quantitativeTypeInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, quantitativeTypeTypeIdV1, isAbstract: true, isMergeable: false, semanticTypeTypeIdV3);
            quantitativeTypeInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            quantitativeTypeInfoV1.AddPropertyValueConstraint("schema", new ValueConstraint() { RequiredTypes = new List<Dtmi>() { new Dtmi("dtmi:dtdl:class:NumericSchema;3") }, RequiredTypesString = "NumericSchema" });
            quantitativeTypeInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            quantitativeTypeInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo radioactivityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, radioactivityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            radioactivityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:RadioactivityUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            radioactivityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            radioactivityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo ratioUnitInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionQuantitativeTypesContextIdV1, ratioUnitTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            ratioUnitInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:bottomUnit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            ratioUnitInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:topUnit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            ratioUnitInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.EnumValue };
            ratioUnitInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo reactiveEnergyInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, reactiveEnergyTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            reactiveEnergyInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ReactiveEnergyUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            reactiveEnergyInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            reactiveEnergyInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo reactivePowerInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, reactivePowerTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            reactivePowerInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ReactivePowerUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            reactivePowerInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            reactivePowerInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo relativeDensityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, relativeDensityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            relativeDensityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:Unitless"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            relativeDensityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            relativeDensityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo relativeHumidityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, relativeHumidityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            relativeHumidityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:Unitless"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            relativeHumidityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            relativeHumidityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo resistanceInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, resistanceTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            resistanceInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ResistanceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            resistanceInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            resistanceInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo soundPressureInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, soundPressureTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            soundPressureInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:SoundPressureUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            soundPressureInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            soundPressureInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo symbolicUnitInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionQuantitativeTypesContextIdV1, symbolicUnitTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            symbolicUnitInfoV1.AddProperty("dtmi:dtdl:property:symbol;3", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            symbolicUnitInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.EnumValue };
            symbolicUnitInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo temperatureInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, temperatureTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            temperatureInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:TemperatureUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            temperatureInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            temperatureInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo thrustInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, thrustTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            thrustInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:ForceUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            thrustInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            thrustInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo timeSpanInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, timeSpanTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            timeSpanInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:TimeUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            timeSpanInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            timeSpanInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo torqueInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, torqueTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            torqueInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:TorqueUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            torqueInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            torqueInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo unitPrefixInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.AdjunctType, dtdlExtensionQuantitativeTypesContextIdV1, unitPrefixTypeIdV1, isAbstract: false, isMergeable: false, adjunctTypeTypeIdV3);
            unitPrefixInfoV1.AddProperty("dtmi:dtdl:property:exponent;3", new Uri("http://www.w3.org/2001/XMLSchema#integer"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            unitPrefixInfoV1.AddProperty("dtmi:dtdl:property:symbol;3", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            unitPrefixInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.EnumValue };
            unitPrefixInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo velocityInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, velocityTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            velocityInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:VelocityUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            velocityInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            velocityInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo voltageInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, voltageTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            voltageInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:VoltageUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            voltageInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            voltageInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo volumeInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, volumeTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            volumeInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:VolumeUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            volumeInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            volumeInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo volumeFlowRateInfoV1 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlExtensionQuantitativeTypesContextIdV1, volumeFlowRateTypeIdV1, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV1);
            volumeFlowRateInfoV1.AddProperty("dtmi:dtdl:extension:quantitativeTypes:v1:property:unit", new Dtmi("dtmi:dtdl:class:EnumValue;3"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: new Dtmi("dtmi:dtdl:extension:quantitativeTypes:v1:enum:VolumeFlowRateUnit"), instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            volumeFlowRateInfoV1.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.CommandRequest, DTEntityKind.CommandResponse, DTEntityKind.Field, DTEntityKind.MapValue, DTEntityKind.Property, DTEntityKind.Telemetry };
            volumeFlowRateInfoV1.AllowedCotypeVersions = new HashSet<int>() { 3 };

            DTSupplementalTypeInfo accelerationVectorInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, iotcentralContextIdV2, accelerationVectorTypeIdV2, isAbstract: false, isMergeable: false, semanticTypeTypeIdV2);
            accelerationVectorInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AccelerationUnit;2"), 1, null, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            accelerationVectorInfoV2.AddPropertyValueConstraint("schema", new ValueConstraint() { RequiredValues = new List<Dtmi>() { new Dtmi("dtmi:iotcentral:schema:vector;2") }, RequiredValuesString = "vector" });
            accelerationVectorInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            accelerationVectorInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo eventInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, iotcentralContextIdV2, eventTypeIdV2, isAbstract: false, isMergeable: false, semanticTypeTypeIdV2);
            eventInfoV2.AddPropertyValueConstraint("schema", new ValueConstraint() { RequiredTypes = new List<Dtmi>() { new Dtmi("dtmi:dtdl:class:NumericSchema;2"), new Dtmi("dtmi:dtdl:class:String;2") }, RequiredTypesString = "NumericSchema or String" });
            eventInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            eventInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo locationInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, iotcentralContextIdV2, locationTypeIdV2, isAbstract: false, isMergeable: false, semanticTypeTypeIdV2);
            locationInfoV2.AddPropertyValueConstraint("schema", new ValueConstraint() { RequiredValues = new List<Dtmi>() { new Dtmi("dtmi:standard:schema:geospatial:point;2"), new Dtmi("dtmi:standard:schema:geospatial:multiPoint;2"), new Dtmi("dtmi:standard:schema:geospatial:lineString;2"), new Dtmi("dtmi:standard:schema:geospatial:multiLineString;2"), new Dtmi("dtmi:standard:schema:geospatial:polygon;2"), new Dtmi("dtmi:standard:schema:geospatial:multiPolygon;2"), new Dtmi("dtmi:iotcentral:schema:geopoint;2") }, RequiredValuesString = "point or multiPoint or lineString or multiLineString or polygon or multiPolygon or geopoint" });
            locationInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            locationInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo stateInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, iotcentralContextIdV2, stateTypeIdV2, isAbstract: false, isMergeable: false, semanticTypeTypeIdV2);
            stateInfoV2.AddPropertyValueConstraint("schema", new ValueConstraint() { RequiredTypes = new List<Dtmi>() { new Dtmi("dtmi:dtdl:class:Enum;2") }, RequiredTypesString = "Enum" });
            stateInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            stateInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo velocityVectorInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, iotcentralContextIdV2, velocityVectorTypeIdV2, isAbstract: false, isMergeable: false, semanticTypeTypeIdV2);
            velocityVectorInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:VelocityUnit;2"), 1, null, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            velocityVectorInfoV2.AddPropertyValueConstraint("schema", new ValueConstraint() { RequiredValues = new List<Dtmi>() { new Dtmi("dtmi:iotcentral:schema:vector;2") }, RequiredValuesString = "vector" });
            velocityVectorInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            velocityVectorInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo accelerationInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, accelerationTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            accelerationInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AccelerationUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            accelerationInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            accelerationInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo accelerationUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, accelerationUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo angleInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, angleTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            angleInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AngleUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            angleInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            angleInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo angleUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, angleUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo angularAccelerationInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, angularAccelerationTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            angularAccelerationInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AngularAccelerationUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            angularAccelerationInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            angularAccelerationInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo angularAccelerationUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, angularAccelerationUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo angularVelocityInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, angularVelocityTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            angularVelocityInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AngularVelocityUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            angularVelocityInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            angularVelocityInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo angularVelocityUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, angularVelocityUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo areaInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, areaTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            areaInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AreaUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            areaInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            areaInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo areaUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, areaUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo binaryPrefixInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.UnitAttribute, dtdlContextIdV2, binaryPrefixTypeIdV2, isAbstract: false, isMergeable: false, unitAttributeTypeIdV2);
            binaryPrefixInfoV2.AddProperty("dtmi:dtdl:property:exponent;2", new Uri("http://www.w3.org/2001/XMLSchema#integer"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            binaryPrefixInfoV2.AddProperty("dtmi:dtdl:property:symbol;2", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);

            DTSupplementalTypeInfo binaryUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticUnit, dtdlContextIdV2, binaryUnitTypeIdV2, isAbstract: false, isMergeable: false, semanticUnitTypeIdV2);
            binaryUnitInfoV2.AddProperty("dtmi:dtdl:property:baseUnit;2", new Dtmi("dtmi:dtdl:class:Unit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            binaryUnitInfoV2.AddProperty("dtmi:dtdl:property:prefix;2", new Dtmi("dtmi:standard:class:BinaryPrefix;2"), 1, null, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            binaryUnitInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Unit };
            binaryUnitInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo capacitanceInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, capacitanceTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            capacitanceInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:CapacitanceUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            capacitanceInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            capacitanceInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo capacitanceUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, capacitanceUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo chargeUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, chargeUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo currentInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, currentTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            currentInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:CurrentUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            currentInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            currentInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo currentUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, currentUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo dataRateInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, dataRateTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            dataRateInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:DataRateUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            dataRateInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            dataRateInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo dataRateUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, dataRateUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo dataSizeInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, dataSizeTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            dataSizeInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:DataSizeUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            dataSizeInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            dataSizeInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo dataSizeUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, dataSizeUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo decimalPrefixInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.UnitAttribute, dtdlContextIdV2, decimalPrefixTypeIdV2, isAbstract: false, isMergeable: false, unitAttributeTypeIdV2);
            decimalPrefixInfoV2.AddProperty("dtmi:dtdl:property:exponent;2", new Uri("http://www.w3.org/2001/XMLSchema#integer"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            decimalPrefixInfoV2.AddProperty("dtmi:dtdl:property:symbol;2", new Uri("http://www.w3.org/2001/XMLSchema#string"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);

            DTSupplementalTypeInfo decimalUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticUnit, dtdlContextIdV2, decimalUnitTypeIdV2, isAbstract: false, isMergeable: false, semanticUnitTypeIdV2);
            decimalUnitInfoV2.AddProperty("dtmi:dtdl:property:baseUnit;2", new Dtmi("dtmi:dtdl:class:Unit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            decimalUnitInfoV2.AddProperty("dtmi:dtdl:property:prefix;2", new Dtmi("dtmi:standard:class:DecimalPrefix;2"), 1, null, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: true, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            decimalUnitInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Unit };
            decimalUnitInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo densityInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, densityTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            densityInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:DensityUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            densityInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            densityInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo densityUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, densityUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo distanceInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, distanceTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            distanceInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:LengthUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            distanceInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            distanceInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo electricChargeInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, electricChargeTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            electricChargeInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:ChargeUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            electricChargeInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            electricChargeInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo energyInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, energyTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            energyInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:EnergyUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            energyInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            energyInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo energyUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, energyUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo forceInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, forceTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            forceInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:ForceUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            forceInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            forceInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo forceUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, forceUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo frequencyInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, frequencyTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            frequencyInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:FrequencyUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            frequencyInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            frequencyInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo frequencyUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, frequencyUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo humidityInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, humidityTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            humidityInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:DensityUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            humidityInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            humidityInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo illuminanceInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, illuminanceTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            illuminanceInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:IlluminanceUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            illuminanceInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            illuminanceInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo illuminanceUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, illuminanceUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo inductanceInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, inductanceTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            inductanceInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:InductanceUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            inductanceInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            inductanceInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo inductanceUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, inductanceUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo latitudeInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, latitudeTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            latitudeInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AngleUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            latitudeInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            latitudeInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo lengthInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, lengthTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            lengthInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:LengthUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            lengthInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            lengthInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo lengthUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, lengthUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo longitudeInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, longitudeTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            longitudeInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:AngleUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            longitudeInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            longitudeInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo luminanceInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, luminanceTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            luminanceInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:LuminanceUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminanceInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            luminanceInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo luminanceUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, luminanceUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo luminosityInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, luminosityTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            luminosityInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:PowerUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminosityInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            luminosityInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo luminousFluxInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, luminousFluxTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            luminousFluxInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:LuminousFluxUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminousFluxInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            luminousFluxInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo luminousFluxUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, luminousFluxUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo luminousIntensityInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, luminousIntensityTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            luminousIntensityInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:LuminousIntensityUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            luminousIntensityInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            luminousIntensityInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo luminousIntensityUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, luminousIntensityUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo magneticFluxInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, magneticFluxTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            magneticFluxInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:MagneticFluxUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            magneticFluxInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            magneticFluxInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo magneticFluxUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, magneticFluxUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo magneticInductionInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, magneticInductionTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            magneticInductionInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:MagneticInductionUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            magneticInductionInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            magneticInductionInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo magneticInductionUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, magneticInductionUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo massInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, massTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            massInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:MassUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            massInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            massInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo massFlowRateInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, massFlowRateTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            massFlowRateInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:MassFlowRateUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            massFlowRateInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            massFlowRateInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo massFlowRateUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, massFlowRateUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo massUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, massUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo powerInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, powerTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            powerInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:PowerUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            powerInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            powerInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo powerUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, powerUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo pressureInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, pressureTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            pressureInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:PressureUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            pressureInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            pressureInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo pressureUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, pressureUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo quantitativeTypeInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, quantitativeTypeTypeIdV2, isAbstract: true, isMergeable: false, semanticTypeTypeIdV2);
            quantitativeTypeInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:dtdl:class:Unit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            quantitativeTypeInfoV2.AddPropertyValueConstraint("schema", new ValueConstraint() { RequiredTypes = new List<Dtmi>() { new Dtmi("dtmi:dtdl:class:NumericSchema;2") }, RequiredTypesString = "NumericSchema" });
            quantitativeTypeInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            quantitativeTypeInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo ratioUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticUnit, dtdlContextIdV2, ratioUnitTypeIdV2, isAbstract: false, isMergeable: false, semanticUnitTypeIdV2);
            ratioUnitInfoV2.AddProperty("dtmi:dtdl:property:bottomUnit;2", new Dtmi("dtmi:dtdl:class:Unit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            ratioUnitInfoV2.AddProperty("dtmi:dtdl:property:topUnit;2", new Dtmi("dtmi:dtdl:class:Unit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            ratioUnitInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Unit };
            ratioUnitInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo relativeHumidityInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, relativeHumidityTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            relativeHumidityInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:Unitless;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            relativeHumidityInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            relativeHumidityInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo resistanceInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, resistanceTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            resistanceInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:ResistanceUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            resistanceInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            resistanceInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo resistanceUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, resistanceUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo soundPressureInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, soundPressureTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            soundPressureInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:SoundPressureUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            soundPressureInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            soundPressureInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo soundPressureUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, soundPressureUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo temperatureInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, temperatureTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            temperatureInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:TemperatureUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            temperatureInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            temperatureInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo temperatureUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, temperatureUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo thrustInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, thrustTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            thrustInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:ForceUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            thrustInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            thrustInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo timeSpanInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, timeSpanTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            timeSpanInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:TimeUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            timeSpanInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            timeSpanInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo timeUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, timeUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo torqueInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, torqueTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            torqueInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:TorqueUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            torqueInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            torqueInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo torqueUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, torqueUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo unitlessInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, unitlessTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo velocityInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, velocityTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            velocityInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:VelocityUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            velocityInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            velocityInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo velocityUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, velocityUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo voltageInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, voltageTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            voltageInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:VoltageUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            voltageInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            voltageInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo voltageUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, voltageUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo volumeInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, volumeTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            volumeInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:VolumeUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            volumeInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            volumeInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo volumeFlowRateInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.SemanticType, dtdlContextIdV2, volumeFlowRateTypeIdV2, isAbstract: false, isMergeable: false, quantitativeTypeTypeIdV2);
            volumeFlowRateInfoV2.AddProperty("dtmi:dtdl:property:unit;2", new Dtmi("dtmi:standard:class:VolumeFlowRateUnit;2"), 1, 1, null, null, null, regex: null, hasUniqueValue: false, isPlural: false, isOptional: false, defaultLanguage: null, dtmiSeg: null, dictionaryKey: null, idRequired: false, typeRequired: true, childOf: null, instanceProperty: null, requiredValues: null, requiredValuesString: null, requiredLiteral: null);
            volumeFlowRateInfoV2.AllowedCotypeKinds = new HashSet<DTEntityKind>() { DTEntityKind.Property, DTEntityKind.Telemetry };
            volumeFlowRateInfoV2.AllowedCotypeVersions = new HashSet<int>() { 2 };

            DTSupplementalTypeInfo volumeFlowRateUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, volumeFlowRateUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            DTSupplementalTypeInfo volumeUnitInfoV2 = new DTSupplementalTypeInfo(DTExtensionKind.Unit, dtdlContextIdV2, volumeUnitTypeIdV2, isAbstract: false, isMergeable: false, unitTypeIdV2);

            EndogenousSupplementalTypes[adjunctTypeTypeIdV3] = adjunctTypeInfoV3;
            EndogenousSupplementalTypes[aliasTypeIdV3] = aliasInfoV3;
            EndogenousSupplementalTypes[semanticTypeTypeIdV2] = semanticTypeInfoV2;
            EndogenousSupplementalTypes[semanticTypeTypeIdV3] = semanticTypeInfoV3;
            EndogenousSupplementalTypes[semanticUnitTypeIdV2] = semanticUnitInfoV2;
            EndogenousSupplementalTypes[semanticUnitTypeIdV3] = semanticUnitInfoV3;
            EndogenousSupplementalTypes[valueAnnotationTypeIdV1] = valueAnnotationInfoV1;
            EndogenousSupplementalTypes[historizedTypeIdV1] = historizedInfoV1;
            EndogenousSupplementalTypes[cacheableTypeIdV1] = cacheableInfoV1;
            EndogenousSupplementalTypes[idempotentTypeIdV1] = idempotentInfoV1;
            EndogenousSupplementalTypes[indexedTypeIdV1] = indexedInfoV1;
            EndogenousSupplementalTypes[mqttTypeIdV1] = mqttInfoV1;
            EndogenousSupplementalTypes[overrideTypeIdV1] = overrideInfoV1;
            EndogenousSupplementalTypes[accelerationTypeIdV1] = accelerationInfoV1;
            EndogenousSupplementalTypes[angleTypeIdV1] = angleInfoV1;
            EndogenousSupplementalTypes[angularAccelerationTypeIdV1] = angularAccelerationInfoV1;
            EndogenousSupplementalTypes[angularVelocityTypeIdV1] = angularVelocityInfoV1;
            EndogenousSupplementalTypes[apparentEnergyTypeIdV1] = apparentEnergyInfoV1;
            EndogenousSupplementalTypes[apparentPowerTypeIdV1] = apparentPowerInfoV1;
            EndogenousSupplementalTypes[areaTypeIdV1] = areaInfoV1;
            EndogenousSupplementalTypes[binaryUnitTypeIdV1] = binaryUnitInfoV1;
            EndogenousSupplementalTypes[capacitanceTypeIdV1] = capacitanceInfoV1;
            EndogenousSupplementalTypes[concentrationTypeIdV1] = concentrationInfoV1;
            EndogenousSupplementalTypes[currentTypeIdV1] = currentInfoV1;
            EndogenousSupplementalTypes[dataRateTypeIdV1] = dataRateInfoV1;
            EndogenousSupplementalTypes[dataSizeTypeIdV1] = dataSizeInfoV1;
            EndogenousSupplementalTypes[decimalUnitTypeIdV1] = decimalUnitInfoV1;
            EndogenousSupplementalTypes[densityTypeIdV1] = densityInfoV1;
            EndogenousSupplementalTypes[distanceTypeIdV1] = distanceInfoV1;
            EndogenousSupplementalTypes[electricChargeTypeIdV1] = electricChargeInfoV1;
            EndogenousSupplementalTypes[energyTypeIdV1] = energyInfoV1;
            EndogenousSupplementalTypes[energyRateTypeIdV1] = energyRateInfoV1;
            EndogenousSupplementalTypes[forceTypeIdV1] = forceInfoV1;
            EndogenousSupplementalTypes[frequencyTypeIdV1] = frequencyInfoV1;
            EndogenousSupplementalTypes[humidityTypeIdV1] = humidityInfoV1;
            EndogenousSupplementalTypes[illuminanceTypeIdV1] = illuminanceInfoV1;
            EndogenousSupplementalTypes[inductanceTypeIdV1] = inductanceInfoV1;
            EndogenousSupplementalTypes[ionizingRadiationDoseTypeIdV1] = ionizingRadiationDoseInfoV1;
            EndogenousSupplementalTypes[irradianceTypeIdV1] = irradianceInfoV1;
            EndogenousSupplementalTypes[latitudeTypeIdV1] = latitudeInfoV1;
            EndogenousSupplementalTypes[lengthTypeIdV1] = lengthInfoV1;
            EndogenousSupplementalTypes[longitudeTypeIdV1] = longitudeInfoV1;
            EndogenousSupplementalTypes[luminanceTypeIdV1] = luminanceInfoV1;
            EndogenousSupplementalTypes[luminosityTypeIdV1] = luminosityInfoV1;
            EndogenousSupplementalTypes[luminousFluxTypeIdV1] = luminousFluxInfoV1;
            EndogenousSupplementalTypes[luminousIntensityTypeIdV1] = luminousIntensityInfoV1;
            EndogenousSupplementalTypes[magneticFluxTypeIdV1] = magneticFluxInfoV1;
            EndogenousSupplementalTypes[magneticInductionTypeIdV1] = magneticInductionInfoV1;
            EndogenousSupplementalTypes[massTypeIdV1] = massInfoV1;
            EndogenousSupplementalTypes[massFlowRateTypeIdV1] = massFlowRateInfoV1;
            EndogenousSupplementalTypes[powerTypeIdV1] = powerInfoV1;
            EndogenousSupplementalTypes[pressureTypeIdV1] = pressureInfoV1;
            EndogenousSupplementalTypes[quantitativeTypeTypeIdV1] = quantitativeTypeInfoV1;
            EndogenousSupplementalTypes[radioactivityTypeIdV1] = radioactivityInfoV1;
            EndogenousSupplementalTypes[ratioUnitTypeIdV1] = ratioUnitInfoV1;
            EndogenousSupplementalTypes[reactiveEnergyTypeIdV1] = reactiveEnergyInfoV1;
            EndogenousSupplementalTypes[reactivePowerTypeIdV1] = reactivePowerInfoV1;
            EndogenousSupplementalTypes[relativeDensityTypeIdV1] = relativeDensityInfoV1;
            EndogenousSupplementalTypes[relativeHumidityTypeIdV1] = relativeHumidityInfoV1;
            EndogenousSupplementalTypes[resistanceTypeIdV1] = resistanceInfoV1;
            EndogenousSupplementalTypes[soundPressureTypeIdV1] = soundPressureInfoV1;
            EndogenousSupplementalTypes[symbolicUnitTypeIdV1] = symbolicUnitInfoV1;
            EndogenousSupplementalTypes[temperatureTypeIdV1] = temperatureInfoV1;
            EndogenousSupplementalTypes[thrustTypeIdV1] = thrustInfoV1;
            EndogenousSupplementalTypes[timeSpanTypeIdV1] = timeSpanInfoV1;
            EndogenousSupplementalTypes[torqueTypeIdV1] = torqueInfoV1;
            EndogenousSupplementalTypes[unitPrefixTypeIdV1] = unitPrefixInfoV1;
            EndogenousSupplementalTypes[velocityTypeIdV1] = velocityInfoV1;
            EndogenousSupplementalTypes[voltageTypeIdV1] = voltageInfoV1;
            EndogenousSupplementalTypes[volumeTypeIdV1] = volumeInfoV1;
            EndogenousSupplementalTypes[volumeFlowRateTypeIdV1] = volumeFlowRateInfoV1;
            EndogenousSupplementalTypes[accelerationVectorTypeIdV2] = accelerationVectorInfoV2;
            EndogenousSupplementalTypes[eventTypeIdV2] = eventInfoV2;
            EndogenousSupplementalTypes[locationTypeIdV2] = locationInfoV2;
            EndogenousSupplementalTypes[stateTypeIdV2] = stateInfoV2;
            EndogenousSupplementalTypes[velocityVectorTypeIdV2] = velocityVectorInfoV2;
            EndogenousSupplementalTypes[accelerationTypeIdV2] = accelerationInfoV2;
            EndogenousSupplementalTypes[accelerationUnitTypeIdV2] = accelerationUnitInfoV2;
            EndogenousSupplementalTypes[angleTypeIdV2] = angleInfoV2;
            EndogenousSupplementalTypes[angleUnitTypeIdV2] = angleUnitInfoV2;
            EndogenousSupplementalTypes[angularAccelerationTypeIdV2] = angularAccelerationInfoV2;
            EndogenousSupplementalTypes[angularAccelerationUnitTypeIdV2] = angularAccelerationUnitInfoV2;
            EndogenousSupplementalTypes[angularVelocityTypeIdV2] = angularVelocityInfoV2;
            EndogenousSupplementalTypes[angularVelocityUnitTypeIdV2] = angularVelocityUnitInfoV2;
            EndogenousSupplementalTypes[areaTypeIdV2] = areaInfoV2;
            EndogenousSupplementalTypes[areaUnitTypeIdV2] = areaUnitInfoV2;
            EndogenousSupplementalTypes[binaryPrefixTypeIdV2] = binaryPrefixInfoV2;
            EndogenousSupplementalTypes[binaryUnitTypeIdV2] = binaryUnitInfoV2;
            EndogenousSupplementalTypes[capacitanceTypeIdV2] = capacitanceInfoV2;
            EndogenousSupplementalTypes[capacitanceUnitTypeIdV2] = capacitanceUnitInfoV2;
            EndogenousSupplementalTypes[chargeUnitTypeIdV2] = chargeUnitInfoV2;
            EndogenousSupplementalTypes[currentTypeIdV2] = currentInfoV2;
            EndogenousSupplementalTypes[currentUnitTypeIdV2] = currentUnitInfoV2;
            EndogenousSupplementalTypes[dataRateTypeIdV2] = dataRateInfoV2;
            EndogenousSupplementalTypes[dataRateUnitTypeIdV2] = dataRateUnitInfoV2;
            EndogenousSupplementalTypes[dataSizeTypeIdV2] = dataSizeInfoV2;
            EndogenousSupplementalTypes[dataSizeUnitTypeIdV2] = dataSizeUnitInfoV2;
            EndogenousSupplementalTypes[decimalPrefixTypeIdV2] = decimalPrefixInfoV2;
            EndogenousSupplementalTypes[decimalUnitTypeIdV2] = decimalUnitInfoV2;
            EndogenousSupplementalTypes[densityTypeIdV2] = densityInfoV2;
            EndogenousSupplementalTypes[densityUnitTypeIdV2] = densityUnitInfoV2;
            EndogenousSupplementalTypes[distanceTypeIdV2] = distanceInfoV2;
            EndogenousSupplementalTypes[electricChargeTypeIdV2] = electricChargeInfoV2;
            EndogenousSupplementalTypes[energyTypeIdV2] = energyInfoV2;
            EndogenousSupplementalTypes[energyUnitTypeIdV2] = energyUnitInfoV2;
            EndogenousSupplementalTypes[forceTypeIdV2] = forceInfoV2;
            EndogenousSupplementalTypes[forceUnitTypeIdV2] = forceUnitInfoV2;
            EndogenousSupplementalTypes[frequencyTypeIdV2] = frequencyInfoV2;
            EndogenousSupplementalTypes[frequencyUnitTypeIdV2] = frequencyUnitInfoV2;
            EndogenousSupplementalTypes[humidityTypeIdV2] = humidityInfoV2;
            EndogenousSupplementalTypes[illuminanceTypeIdV2] = illuminanceInfoV2;
            EndogenousSupplementalTypes[illuminanceUnitTypeIdV2] = illuminanceUnitInfoV2;
            EndogenousSupplementalTypes[inductanceTypeIdV2] = inductanceInfoV2;
            EndogenousSupplementalTypes[inductanceUnitTypeIdV2] = inductanceUnitInfoV2;
            EndogenousSupplementalTypes[latitudeTypeIdV2] = latitudeInfoV2;
            EndogenousSupplementalTypes[lengthTypeIdV2] = lengthInfoV2;
            EndogenousSupplementalTypes[lengthUnitTypeIdV2] = lengthUnitInfoV2;
            EndogenousSupplementalTypes[longitudeTypeIdV2] = longitudeInfoV2;
            EndogenousSupplementalTypes[luminanceTypeIdV2] = luminanceInfoV2;
            EndogenousSupplementalTypes[luminanceUnitTypeIdV2] = luminanceUnitInfoV2;
            EndogenousSupplementalTypes[luminosityTypeIdV2] = luminosityInfoV2;
            EndogenousSupplementalTypes[luminousFluxTypeIdV2] = luminousFluxInfoV2;
            EndogenousSupplementalTypes[luminousFluxUnitTypeIdV2] = luminousFluxUnitInfoV2;
            EndogenousSupplementalTypes[luminousIntensityTypeIdV2] = luminousIntensityInfoV2;
            EndogenousSupplementalTypes[luminousIntensityUnitTypeIdV2] = luminousIntensityUnitInfoV2;
            EndogenousSupplementalTypes[magneticFluxTypeIdV2] = magneticFluxInfoV2;
            EndogenousSupplementalTypes[magneticFluxUnitTypeIdV2] = magneticFluxUnitInfoV2;
            EndogenousSupplementalTypes[magneticInductionTypeIdV2] = magneticInductionInfoV2;
            EndogenousSupplementalTypes[magneticInductionUnitTypeIdV2] = magneticInductionUnitInfoV2;
            EndogenousSupplementalTypes[massTypeIdV2] = massInfoV2;
            EndogenousSupplementalTypes[massFlowRateTypeIdV2] = massFlowRateInfoV2;
            EndogenousSupplementalTypes[massFlowRateUnitTypeIdV2] = massFlowRateUnitInfoV2;
            EndogenousSupplementalTypes[massUnitTypeIdV2] = massUnitInfoV2;
            EndogenousSupplementalTypes[powerTypeIdV2] = powerInfoV2;
            EndogenousSupplementalTypes[powerUnitTypeIdV2] = powerUnitInfoV2;
            EndogenousSupplementalTypes[pressureTypeIdV2] = pressureInfoV2;
            EndogenousSupplementalTypes[pressureUnitTypeIdV2] = pressureUnitInfoV2;
            EndogenousSupplementalTypes[quantitativeTypeTypeIdV2] = quantitativeTypeInfoV2;
            EndogenousSupplementalTypes[ratioUnitTypeIdV2] = ratioUnitInfoV2;
            EndogenousSupplementalTypes[relativeHumidityTypeIdV2] = relativeHumidityInfoV2;
            EndogenousSupplementalTypes[resistanceTypeIdV2] = resistanceInfoV2;
            EndogenousSupplementalTypes[resistanceUnitTypeIdV2] = resistanceUnitInfoV2;
            EndogenousSupplementalTypes[soundPressureTypeIdV2] = soundPressureInfoV2;
            EndogenousSupplementalTypes[soundPressureUnitTypeIdV2] = soundPressureUnitInfoV2;
            EndogenousSupplementalTypes[temperatureTypeIdV2] = temperatureInfoV2;
            EndogenousSupplementalTypes[temperatureUnitTypeIdV2] = temperatureUnitInfoV2;
            EndogenousSupplementalTypes[thrustTypeIdV2] = thrustInfoV2;
            EndogenousSupplementalTypes[timeSpanTypeIdV2] = timeSpanInfoV2;
            EndogenousSupplementalTypes[timeUnitTypeIdV2] = timeUnitInfoV2;
            EndogenousSupplementalTypes[torqueTypeIdV2] = torqueInfoV2;
            EndogenousSupplementalTypes[torqueUnitTypeIdV2] = torqueUnitInfoV2;
            EndogenousSupplementalTypes[unitlessTypeIdV2] = unitlessInfoV2;
            EndogenousSupplementalTypes[velocityTypeIdV2] = velocityInfoV2;
            EndogenousSupplementalTypes[velocityUnitTypeIdV2] = velocityUnitInfoV2;
            EndogenousSupplementalTypes[voltageTypeIdV2] = voltageInfoV2;
            EndogenousSupplementalTypes[voltageUnitTypeIdV2] = voltageUnitInfoV2;
            EndogenousSupplementalTypes[volumeTypeIdV2] = volumeInfoV2;
            EndogenousSupplementalTypes[volumeFlowRateTypeIdV2] = volumeFlowRateInfoV2;
            EndogenousSupplementalTypes[volumeFlowRateUnitTypeIdV2] = volumeFlowRateUnitInfoV2;
            EndogenousSupplementalTypes[volumeUnitTypeIdV2] = volumeUnitInfoV2;

            ConnectEndogenousPropertySetters();
        }

        private static bool TryGetKindsAndVersions(Dtmi extensionId, Dtmi typeId, HashSet<Dtmi> cotypeIds, List<JsonLdValue> allowedCotypes, out HashSet<DTEntityKind> kinds, out HashSet<int> versions, ParsingErrorCollection parsingErrorCollection)
        {
            kinds = new HashSet<DTEntityKind>();
            versions = new HashSet<int>();

            foreach (Dtmi cotypeId in cotypeIds)
            {
                DTEntityKind kind;
                if (!Enum.TryParse(ContextCollection.GetTermOrUri(cotypeId), out kind))
                {
                    string cotypeIdTerm = ContextCollection.GetTermOrUri(cotypeId);
                    JsonLdValue allowedCotype = allowedCotypes.First(c => c.StringValue == cotypeIdTerm || c.StringValue == cotypeId.OriginalString);
                    parsingErrorCollection.Notify(
                        "cotypeNotConcreteMaterial",
                        contextId: extensionId,
                        elementId: typeId,
                        cotype: allowedCotype.StringValue,
                        incidentValue: allowedCotype);

                    return false;
                }

                kinds.Add(kind);
                versions.Add(cotypeId.MajorVersion);
            }

            return true;
        }

        private static bool TrySetAllowedCotypes(Dtmi extensionId, Dtmi typeId, List<JsonLdValue> allowedCotypes, DTSupplementalTypeInfo typeInfo, DTSupplementalTypeInfo parentTypeInfo, AggregateContext aggregateContext, ParsingErrorCollection parsingErrorCollection)
        {
            if (!TryGetClassConstraints(allowedCotypes, "sh:or", typeInfo, aggregateContext, out HashSet<Dtmi> cotypeIds, parsingErrorCollection))
            {
                return false;
            }

            if (cotypeIds != null)
            {
                if (!TryGetKindsAndVersions(extensionId, typeId, cotypeIds, allowedCotypes, out HashSet<DTEntityKind> kinds, out HashSet<int> versions, parsingErrorCollection))
                {
                    return false;
                }

                typeInfo.AllowedCotypeKinds = kinds;
                typeInfo.AllowedCotypeVersions = versions;
                if (parentTypeInfo != null)
                {
                    typeInfo.AllowedCotypeKinds.IntersectWith(parentTypeInfo.AllowedCotypeKinds);
                    typeInfo.AllowedCotypeVersions.IntersectWith(parentTypeInfo.AllowedCotypeVersions);
                }
            }
            else if (parentTypeInfo != null)
            {
                typeInfo.AllowedCotypeKinds = parentTypeInfo.AllowedCotypeKinds;
                typeInfo.AllowedCotypeVersions = parentTypeInfo.AllowedCotypeVersions;
            }

            return true;
        }
    }
}
