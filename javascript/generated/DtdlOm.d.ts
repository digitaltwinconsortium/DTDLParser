export type DtdlObjectModel = { [entityId: string]: EntityType };

export interface ArrayInfo extends ComplexSchemaInfo {
  EntityKind: 'Array';
  elementSchema: string;
}

export type ArrayType = ArrayInfo;

export interface BooleanInfo extends PrimitiveSchemaInfo {
  EntityKind: 'Boolean';
}

export type BooleanType = BooleanInfo;

export interface ByteInfo extends NumericSchemaInfo {
  EntityKind: 'Byte';
}

export type ByteType = ByteInfo;

export interface BytesInfo extends PrimitiveSchemaInfo {
  EntityKind: 'Bytes';
}

export type BytesType = BytesInfo;

export interface CommandInfo extends ContentInfo {
  EntityKind: 'Command';
  commandType?: string;
  request?: string;
  response?: string;
}

export type CommandType = CommandInfo;

export interface CommandPayloadInfo extends SchemaFieldInfo {
  EntityKind: 'CommandPayload' | 'CommandRequest' | 'CommandResponse';
  nullable: boolean;
}

export type CommandPayloadType = CommandPayloadInfo | CommandRequestType | CommandResponseType;

export interface CommandRequestInfo extends CommandPayloadInfo {
  EntityKind: 'CommandRequest';
}

export type CommandRequestType = CommandRequestInfo;

export interface CommandResponseInfo extends CommandPayloadInfo {
  EntityKind: 'CommandResponse';
}

export type CommandResponseType = CommandResponseInfo;

export interface CommandTypeInfo extends EntityInfo {
  EntityKind: 'CommandType';
}

export type CommandTypeType = CommandTypeInfo;

export interface ComplexSchemaInfo extends SchemaInfo {
  EntityKind: 'Array' | 'Enum' | 'Map' | 'Object';
}

export type ComplexSchemaType = ComplexSchemaInfo | ArrayType | EnumType | MapType | ObjectType;

export interface ComponentInfo extends ContentInfo {
  EntityKind: 'Component';
  schema: string;
}

export type ComponentType = ComponentInfo;

export interface ContentInfo extends NamedEntityInfo {
  EntityKind: 'Command' | 'Component' | 'Property' | 'Relationship' | 'Telemetry';
}

export type ContentType = ContentInfo | CommandType | ComponentType | PropertyType | RelationshipType | TelemetryType;

export interface DateInfo extends TemporalSchemaInfo {
  EntityKind: 'Date';
}

export type DateType = DateInfo;

export interface DateTimeInfo extends TemporalSchemaInfo {
  EntityKind: 'DateTime';
}

export type DateTimeType = DateTimeInfo;

export interface DoubleInfo extends NumericSchemaInfo {
  EntityKind: 'Double';
}

export type DoubleType = DoubleInfo;

export interface DurationInfo extends TemporalSchemaInfo {
  EntityKind: 'Duration';
}

export type DurationType = DurationInfo;

export interface EntityInfo {
  EntityKind: 'Array' | 'Boolean' | 'Command' | 'CommandPayload' | 'CommandType' | 'Component' | 'Date' | 'DateTime' | 'Double' | 'Duration' | 'Enum' | 'EnumValue' | 'Field' | 'Float' | 'Integer' | 'Interface' | 'Long' | 'Map' | 'MapKey' | 'MapValue' | 'Object' | 'Property' | 'Relationship' | 'String' | 'Telemetry' | 'Time' | 'CommandRequest' | 'CommandResponse' | 'Byte' | 'Bytes' | 'Short' | 'UnsignedByte' | 'UnsignedInteger' | 'UnsignedLong' | 'UnsignedShort' | 'Uuid' | 'Unit' | 'UnitAttribute' | 'LatentType' | 'NamedLatentType';
  SupplementalTypes: string[];
  SupplementalProperties: { [property: string]: any };
  UndefinedTypes: string[];
  UndefinedProperties: { [property: string]: any };
  ClassId: string;
  comment?: string;
  description: { [languageCode: string]: string };
  displayName: { [languageCode: string]: string };
  languageMajorVersion: number;
  Id: string;
  ChildOf?: string;
  DefinedIn?: string;
}

export type EntityType = EntityInfo | CommandTypeType | InterfaceType | LatentTypeType | NamedEntityType | SchemaType | UnitType;

export interface EnumInfo extends ComplexSchemaInfo {
  EntityKind: 'Enum';
  enumValues: string[];
  valueSchema: string;
}

export type EnumType = EnumInfo;

export interface EnumValueInfo extends NamedEntityInfo {
  EntityKind: 'EnumValue';
  enumValue: string | number | boolean;
}

export type EnumValueType = EnumValueInfo;

export interface FieldInfo extends SchemaFieldInfo {
  EntityKind: 'Field';
}

export type FieldType = FieldInfo;

export interface FloatInfo extends NumericSchemaInfo {
  EntityKind: 'Float';
}

export type FloatType = FloatInfo;

export interface IntegerInfo extends NumericSchemaInfo {
  EntityKind: 'Integer';
}

export type IntegerType = IntegerInfo;

export interface InterfaceInfo extends EntityInfo {
  EntityKind: 'Interface';
  contents: { [name: string]: string };
  commands: { [name: string]: string };
  components: { [name: string]: string };
  properties: { [name: string]: string };
  relationships: { [name: string]: string };
  telemetries: { [name: string]: string };
  extends: string[];
  extendedBy: string[];
  schemas: string[];
}

export type InterfaceType = InterfaceInfo;

export interface LatentTypeInfo extends EntityInfo {
  EntityKind: 'LatentType';
}

export type LatentTypeType = LatentTypeInfo;

export interface LongInfo extends NumericSchemaInfo {
  EntityKind: 'Long';
}

export type LongType = LongInfo;

export interface MapInfo extends ComplexSchemaInfo {
  EntityKind: 'Map';
  mapKey: string;
  mapValue: string;
}

export type MapType = MapInfo;

export interface MapKeyInfo extends NamedEntityInfo {
  EntityKind: 'MapKey';
  schema: string;
}

export type MapKeyType = MapKeyInfo;

export interface MapValueInfo extends SchemaFieldInfo {
  EntityKind: 'MapValue';
}

export type MapValueType = MapValueInfo;

export interface NamedEntityInfo extends EntityInfo {
  EntityKind: 'Command' | 'CommandPayload' | 'Component' | 'EnumValue' | 'Field' | 'MapKey' | 'MapValue' | 'Property' | 'Relationship' | 'Telemetry' | 'CommandRequest' | 'CommandResponse' | 'UnitAttribute' | 'NamedLatentType';
  name: string;
}

export type NamedEntityType = NamedEntityInfo | ContentType | EnumValueType | MapKeyType | NamedLatentTypeType | SchemaFieldType | UnitAttributeType;

export interface NamedLatentTypeInfo extends NamedEntityInfo {
  EntityKind: 'NamedLatentType';
}

export type NamedLatentTypeType = NamedLatentTypeInfo;

export interface NumericSchemaInfo extends PrimitiveSchemaInfo {
  EntityKind: 'Double' | 'Float' | 'Integer' | 'Long' | 'Byte' | 'Short' | 'UnsignedByte' | 'UnsignedInteger' | 'UnsignedLong' | 'UnsignedShort';
}

export type NumericSchemaType = NumericSchemaInfo | ByteType | DoubleType | FloatType | IntegerType | LongType | ShortType | UnsignedByteType | UnsignedIntegerType | UnsignedLongType | UnsignedShortType;

export interface ObjectInfo extends ComplexSchemaInfo {
  EntityKind: 'Object';
  fields: string[];
}

export type ObjectType = ObjectInfo;

export interface PrimitiveSchemaInfo extends SchemaInfo {
  EntityKind: 'Boolean' | 'Date' | 'DateTime' | 'Double' | 'Duration' | 'Float' | 'Integer' | 'Long' | 'String' | 'Time' | 'Byte' | 'Bytes' | 'Short' | 'UnsignedByte' | 'UnsignedInteger' | 'UnsignedLong' | 'UnsignedShort' | 'Uuid';
}

export type PrimitiveSchemaType = PrimitiveSchemaInfo | BooleanType | BytesType | NumericSchemaType | StringType | TemporalSchemaType | UuidType;

export interface PropertyInfo extends ContentInfo {
  EntityKind: 'Property';
  schema: string;
  writable: boolean;
}

export type PropertyType = PropertyInfo;

export interface RelationshipInfo extends ContentInfo {
  EntityKind: 'Relationship';
  maxMultiplicity?: number;
  minMultiplicity?: number;
  properties: string[];
  target?: string;
  writable: boolean;
}

export type RelationshipType = RelationshipInfo;

export interface SchemaInfo extends EntityInfo {
  EntityKind: 'Array' | 'Boolean' | 'Date' | 'DateTime' | 'Double' | 'Duration' | 'Enum' | 'Float' | 'Integer' | 'Long' | 'Map' | 'Object' | 'String' | 'Time' | 'Byte' | 'Bytes' | 'Short' | 'UnsignedByte' | 'UnsignedInteger' | 'UnsignedLong' | 'UnsignedShort' | 'Uuid';
}

export type SchemaType = SchemaInfo | ComplexSchemaType | PrimitiveSchemaType;

export interface SchemaFieldInfo extends NamedEntityInfo {
  EntityKind: 'CommandPayload' | 'Field' | 'MapValue' | 'CommandRequest' | 'CommandResponse';
  schema: string;
}

export type SchemaFieldType = SchemaFieldInfo | CommandPayloadType | FieldType | MapValueType;

export interface ShortInfo extends NumericSchemaInfo {
  EntityKind: 'Short';
}

export type ShortType = ShortInfo;

export interface StringInfo extends PrimitiveSchemaInfo {
  EntityKind: 'String';
}

export type StringType = StringInfo;

export interface TelemetryInfo extends ContentInfo {
  EntityKind: 'Telemetry';
  schema: string;
}

export type TelemetryType = TelemetryInfo;

export interface TemporalSchemaInfo extends PrimitiveSchemaInfo {
  EntityKind: 'Date' | 'DateTime' | 'Duration' | 'Time';
}

export type TemporalSchemaType = TemporalSchemaInfo | DateType | DateTimeType | DurationType | TimeType;

export interface TimeInfo extends TemporalSchemaInfo {
  EntityKind: 'Time';
}

export type TimeType = TimeInfo;

export interface UnitInfo extends EntityInfo {
  EntityKind: 'Unit';
  symbol?: string;
}

export type UnitType = UnitInfo;

export interface UnitAttributeInfo extends NamedEntityInfo {
  EntityKind: 'UnitAttribute';
}

export type UnitAttributeType = UnitAttributeInfo;

export interface UnsignedByteInfo extends NumericSchemaInfo {
  EntityKind: 'UnsignedByte';
}

export type UnsignedByteType = UnsignedByteInfo;

export interface UnsignedIntegerInfo extends NumericSchemaInfo {
  EntityKind: 'UnsignedInteger';
}

export type UnsignedIntegerType = UnsignedIntegerInfo;

export interface UnsignedLongInfo extends NumericSchemaInfo {
  EntityKind: 'UnsignedLong';
}

export type UnsignedLongType = UnsignedLongInfo;

export interface UnsignedShortInfo extends NumericSchemaInfo {
  EntityKind: 'UnsignedShort';
}

export type UnsignedShortType = UnsignedShortInfo;

export interface UuidInfo extends PrimitiveSchemaInfo {
  EntityKind: 'Uuid';
}

export type UuidType = UuidInfo;
