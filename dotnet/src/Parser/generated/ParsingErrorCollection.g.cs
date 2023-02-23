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
    /// A collection of <c>ParsingError</c>.
    /// </summary>
    internal partial class ParsingErrorCollection
    {
        private static readonly Uri AbstractSupplementalTypeValidationId = new Uri("dtmi:dtdl:parsingError:abstractSupplementalType");

        private static readonly Uri BadDtmiOrTermValidationId = new Uri("dtmi:dtdl:parsingError:badDtmiOrTerm");

        private static readonly Uri BooleanCountBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:booleanCountBelowMin");

        private static readonly Uri BooleanMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:booleanMultipleValues");

        private static readonly Uri BooleanNotBooleanValidationId = new Uri("dtmi:dtdl:parsingError:booleanNotBoolean");

        private static readonly Uri BooleanObjectNoValueValidationId = new Uri("dtmi:dtdl:parsingError:booleanObjectNoValue");

        private static readonly Uri BooleanTypeNotBooleanValidationId = new Uri("dtmi:dtdl:parsingError:booleanTypeNotBoolean");

        private static readonly Uri BooleanValueNotBooleanValidationId = new Uri("dtmi:dtdl:parsingError:booleanValueNotBoolean");

        private static readonly Uri ChildOfNotRecognizedValidationId = new Uri("dtmi:dtdl:parsingError:childOfNotRecognized");

        private static readonly Uri ClassNotRecognizedValidationId = new Uri("dtmi:dtdl:parsingError:classNotRecognized");

        private static readonly Uri ClassNotStringValidationId = new Uri("dtmi:dtdl:parsingError:classNotString");

        private static readonly Uri ConflictingSupplementalTypesValidationId = new Uri("dtmi:dtdl:parsingError:conflictingSupplementalTypes");

        private static readonly Uri ConstraintMissingClassValidationId = new Uri("dtmi:dtdl:parsingError:constraintMissingClass");

        private static readonly Uri ConstraintNotObjectValidationId = new Uri("dtmi:dtdl:parsingError:constraintNotObject");

        private static readonly Uri CotypeNotConcreteMaterialValidationId = new Uri("dtmi:dtdl:parsingError:cotypeNotConcreteMaterial");

        private static readonly Uri CrossPartitionReferenceValidationId = new Uri("dtmi:dtdl:parsingError:crossPartitionReference");

        private static readonly Uri DisallowedCocotypeValidationId = new Uri("dtmi:dtdl:parsingError:disallowedCocotype");

        private static readonly Uri DisallowedContextVersionValidationId = new Uri("dtmi:dtdl:parsingError:disallowedContextVersion");

        private static readonly Uri DisallowedIdFragmentValidationId = new Uri("dtmi:dtdl:parsingError:disallowedIdFragment");

        private static readonly Uri DisallowedLocalContextValidationId = new Uri("dtmi:dtdl:parsingError:disallowedLocalContext");

        private static readonly Uri DisallowedTypeValidationId = new Uri("dtmi:dtdl:parsingError:disallowedType");

        private static readonly Uri DisallowedVersionDefinitionValidationId = new Uri("dtmi:dtdl:parsingError:disallowedVersionDefinition");

        private static readonly Uri DisallowedVersionReferenceValidationId = new Uri("dtmi:dtdl:parsingError:disallowedVersionReference");

        private static readonly Uri DtdlContextFollowsAffiliateValidationId = new Uri("dtmi:dtdl:parsingError:dtdlContextFollowsAffiliate");

        private static readonly Uri DtmiSegPropertyNotStringOrNumberValidationId = new Uri("dtmi:dtdl:parsingError:dtmiSegPropertyNotStringOrNumber");

        private static readonly Uri DuplicateDefinitionValidationId = new Uri("dtmi:dtdl:parsingError:duplicateDefinition");

        private static readonly Uri DuplicatePropertyNameValidationId = new Uri("dtmi:dtdl:parsingError:duplicatePropertyName");

        private static readonly Uri EmptyContextValidationId = new Uri("dtmi:dtdl:parsingError:emptyContext");

        private static readonly Uri EmptyExtensionContextValidationId = new Uri("dtmi:dtdl:parsingError:emptyExtensionContext");

        private static readonly Uri ExcessiveCountValidationId = new Uri("dtmi:dtdl:parsingError:excessiveCount");

        private static readonly Uri ExcessiveDepthNarrowValidationId = new Uri("dtmi:dtdl:parsingError:excessiveDepthNarrow");

        private static readonly Uri ExcessiveDepthWideValidationId = new Uri("dtmi:dtdl:parsingError:excessiveDepthWide");

        private static readonly Uri ExcludedTypeValidationId = new Uri("dtmi:dtdl:parsingError:excludedType");

        private static readonly Uri ExtensionNotAllowedValidationId = new Uri("dtmi:dtdl:parsingError:extensionNotAllowed");

        private static readonly Uri ExtensionNotJsonObjectValidationId = new Uri("dtmi:dtdl:parsingError:extensionNotJsonObject");

        private static readonly Uri ExtensionTermDefinitionInvalidDtmiValidationId = new Uri("dtmi:dtdl:parsingError:extensionTermDefinitionInvalidDtmi");

        private static readonly Uri ExtensionTermDefinitionInvalidUriValidationId = new Uri("dtmi:dtdl:parsingError:extensionTermDefinitionInvalidUri");

        private static readonly Uri ExtensionTermEmptyValidationId = new Uri("dtmi:dtdl:parsingError:extensionTermEmpty");

        private static readonly Uri ExtensionTermInvalidValidationId = new Uri("dtmi:dtdl:parsingError:extensionTermInvalid");

        private static readonly Uri ExtensionTermReservedValidationId = new Uri("dtmi:dtdl:parsingError:extensionTermReserved");

        private static readonly Uri ExtensionTermSchemePrefixValidationId = new Uri("dtmi:dtdl:parsingError:extensionTermSchemePrefix");

        private static readonly Uri GraphDisallowedValidationId = new Uri("dtmi:dtdl:parsingError:graphDisallowed");

        private static readonly Uri IdentifierCountAboveMaxValidationId = new Uri("dtmi:dtdl:parsingError:identifierCountAboveMax");

        private static readonly Uri IdentifierCountBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:identifierCountBelowMin");

        private static readonly Uri IdentifierInvalidValidationId = new Uri("dtmi:dtdl:parsingError:identifierInvalid");

        private static readonly Uri IdentifierMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:identifierMultipleValues");

        private static readonly Uri IdentifierNotDtmiValidationId = new Uri("dtmi:dtdl:parsingError:identifierNotDtmi");

        private static readonly Uri IdentifierNotStringValidationId = new Uri("dtmi:dtdl:parsingError:identifierNotString");

        private static readonly Uri IdentifierNotUriValidationId = new Uri("dtmi:dtdl:parsingError:identifierNotUri");

        private static readonly Uri IdentifierTooLongValidationId = new Uri("dtmi:dtdl:parsingError:identifierTooLong");

        private static readonly Uri IdNotStringValidationId = new Uri("dtmi:dtdl:parsingError:idNotString");

        private static readonly Uri IdRefBadDtmiOrTermValidationId = new Uri("dtmi:dtdl:parsingError:idRefBadDtmiOrTerm");

        private static readonly Uri IdReferenceValidationId = new Uri("dtmi:dtdl:parsingError:idReference");

        private static readonly Uri IdTooLongForTypeValidationId = new Uri("dtmi:dtdl:parsingError:idTooLongForType");

        private static readonly Uri IdTooLongValidationId = new Uri("dtmi:dtdl:parsingError:idTooLong");

        private static readonly Uri IncompatibleTypeValidationId = new Uri("dtmi:dtdl:parsingError:incompatibleType");

        private static readonly Uri InconsistentBooleanValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentBooleanValues");

        private static readonly Uri InconsistentContextValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentContext");

        private static readonly Uri InconsistentIdentifierValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentIdentifierValues");

        private static readonly Uri InconsistentIntegerValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentIntegerValues");

        private static readonly Uri InconsistentJsonValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentJsonValues");

        private static readonly Uri InconsistentLangStringValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentLangStringValues");

        private static readonly Uri InconsistentLiteralValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentLiteralValues");

        private static readonly Uri InconsistentNumericValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentNumericValues");

        private static readonly Uri InconsistentObjectValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentObjectValues");

        private static readonly Uri InconsistentParentValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentParent");

        private static readonly Uri InconsistentPartitionValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentPartition");

        private static readonly Uri InconsistentStringValuesValidationId = new Uri("dtmi:dtdl:parsingError:inconsistentStringValues");

        private static readonly Uri IncorrectExtensionTypeValidationId = new Uri("dtmi:dtdl:parsingError:incorrectExtensionType");

        private static readonly Uri IncorrectPropertyValueValidationId = new Uri("dtmi:dtdl:parsingError:incorrectPropertyValue");

        private static readonly Uri InferredTypeDoesNotAllowValidationId = new Uri("dtmi:dtdl:parsingError:inferredTypeDoesNotAllow");

        private static readonly Uri IntConstraintMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:intConstraintMultipleValues");

        private static readonly Uri IntConstraintNotIntegerValidationId = new Uri("dtmi:dtdl:parsingError:intConstraintNotInteger");

        private static readonly Uri IntConstraintNoValueValidationId = new Uri("dtmi:dtdl:parsingError:intConstraintNoValue");

        private static readonly Uri IntegerCountBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:integerCountBelowMin");

        private static readonly Uri IntegerMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:integerMultipleValues");

        private static readonly Uri IntegerNotIntegerValidationId = new Uri("dtmi:dtdl:parsingError:integerNotInteger");

        private static readonly Uri IntegerObjectNoValueValidationId = new Uri("dtmi:dtdl:parsingError:integerObjectNoValue");

        private static readonly Uri IntegerTypeNotIntegerValidationId = new Uri("dtmi:dtdl:parsingError:integerTypeNotInteger");

        private static readonly Uri IntegerValueNotIntegerValidationId = new Uri("dtmi:dtdl:parsingError:integerValueNotInteger");

        private static readonly Uri InvalidContextSpecifierForVersionValidationId = new Uri("dtmi:dtdl:parsingError:invalidContextSpecifierForVersion");

        private static readonly Uri InvalidContextSpecifierValidationId = new Uri("dtmi:dtdl:parsingError:invalidContextSpecifier");

        private static readonly Uri InvalidCotypeValidationId = new Uri("dtmi:dtdl:parsingError:invalidCotype");

        private static readonly Uri InvalidCotypeVersionValidationId = new Uri("dtmi:dtdl:parsingError:invalidCotypeVersion");

        private static readonly Uri InvalidExtensionIdElementValidationId = new Uri("dtmi:dtdl:parsingError:invalidExtensionIdElement");

        private static readonly Uri InvalidExtensionSpecifierValidationId = new Uri("dtmi:dtdl:parsingError:invalidExtensionSpecifier");

        private static readonly Uri InvalidIdForTypeValidationId = new Uri("dtmi:dtdl:parsingError:invalidIdForType");

        private static readonly Uri InvalidIdValidationId = new Uri("dtmi:dtdl:parsingError:invalidId");

        private static readonly Uri KeywordDisallowedValidationId = new Uri("dtmi:dtdl:parsingError:keywordDisallowed");

        private static readonly Uri LangMapObjKeywordValidationId = new Uri("dtmi:dtdl:parsingError:langMapObjKeyword");

        private static readonly Uri LangStringCodeNotUniqueValidationId = new Uri("dtmi:dtdl:parsingError:langStringCodeNotUnique");

        private static readonly Uri LangStringElementCodeNotStringValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementCodeNotString");

        private static readonly Uri LangStringElementCodeNotUniqueValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementCodeNotUnique");

        private static readonly Uri LangStringElementContextValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementContext");

        private static readonly Uri LangStringElementGraphValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementGraph");

        private static readonly Uri LangStringElementIdValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementId");

        private static readonly Uri LangStringElementInvalidCodeValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementInvalidCode");

        private static readonly Uri LangStringElementInvalidPropValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementInvalidProp");

        private static readonly Uri LangStringElementKeywordValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementKeyword");

        private static readonly Uri LangStringElementNotStringOrObjectValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementNotStringOrObject");

        private static readonly Uri LangStringElementNoValueValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementNoValue");

        private static readonly Uri LangStringElementTypeValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementType");

        private static readonly Uri LangStringElementValueNotStringValidationId = new Uri("dtmi:dtdl:parsingError:langStringElementValueNotString");

        private static readonly Uri LangStringInvalidCodeValidationId = new Uri("dtmi:dtdl:parsingError:langStringInvalidCode");

        private static readonly Uri LangStringValueInvalidValidationId = new Uri("dtmi:dtdl:parsingError:langStringValueInvalid");

        private static readonly Uri LangStringValueNotStringValidationId = new Uri("dtmi:dtdl:parsingError:langStringValueNotString");

        private static readonly Uri LangStringValueTooLongValidationId = new Uri("dtmi:dtdl:parsingError:langStringValueTooLong");

        private static readonly Uri LangTagValueInLangMapValidationId = new Uri("dtmi:dtdl:parsingError:langTagValueInLangMap");

        private static readonly Uri LayerMissingMaterialTypeValidationId = new Uri("dtmi:dtdl:parsingError:layerMissingMaterialType");

        private static readonly Uri LiteralCountBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:literalCountBelowMin");

        private static readonly Uri LiteralMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:literalMultipleValues");

        private static readonly Uri LiteralNotValidValidationId = new Uri("dtmi:dtdl:parsingError:literalNotValid");

        private static readonly Uri LiteralObjectNoValueValidationId = new Uri("dtmi:dtdl:parsingError:literalObjectNoValue");

        private static readonly Uri LiteralTypeNotSingularValidationId = new Uri("dtmi:dtdl:parsingError:literalTypeNotSingular");

        private static readonly Uri LocalContextNotLastValidationId = new Uri("dtmi:dtdl:parsingError:localContextNotLast");

        private static readonly Uri LocalTermDefinitionInvalidDtmiValidationId = new Uri("dtmi:dtdl:parsingError:localTermDefinitionInvalidDtmi");

        private static readonly Uri LocalTermDefinitionInvalidUriValidationId = new Uri("dtmi:dtdl:parsingError:localTermDefinitionInvalidUri");

        private static readonly Uri LocalTermEmptyValidationId = new Uri("dtmi:dtdl:parsingError:localTermEmpty");

        private static readonly Uri LocalTermInvalidValidationId = new Uri("dtmi:dtdl:parsingError:localTermInvalid");

        private static readonly Uri LocalTermReservedValidationId = new Uri("dtmi:dtdl:parsingError:localTermReserved");

        private static readonly Uri LocalTermSchemePrefixValidationId = new Uri("dtmi:dtdl:parsingError:localTermSchemePrefix");

        private static readonly Uri MatchingPropertyNotSupplementalValidationId = new Uri("dtmi:dtdl:parsingError:matchingPropertyNotSupplemental");

        private static readonly Uri MaxCountNotPositiveValidationId = new Uri("dtmi:dtdl:parsingError:maxCountNotPositive");

        private static readonly Uri MetamodelMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:metamodelMultipleValues");

        private static readonly Uri MetamodelNoGraphValidationId = new Uri("dtmi:dtdl:parsingError:metamodelNoGraph");

        private static readonly Uri MetamodelNotObjectValidationId = new Uri("dtmi:dtdl:parsingError:metamodelNotObject");

        private static readonly Uri MetamodelNoValueValidationId = new Uri("dtmi:dtdl:parsingError:metamodelNoValue");

        private static readonly Uri MinCountExceedsMaxCountValidationId = new Uri("dtmi:dtdl:parsingError:minCountExceedsMaxCount");

        private static readonly Uri MinCountNegativeValidationId = new Uri("dtmi:dtdl:parsingError:minCountNegative");

        private static readonly Uri MismatchedLayersValidationId = new Uri("dtmi:dtdl:parsingError:mismatchedLayers");

        private static readonly Uri MissingCocotypeValidationId = new Uri("dtmi:dtdl:parsingError:missingCocotype");

        private static readonly Uri MissingContextValidationId = new Uri("dtmi:dtdl:parsingError:missingContext");

        private static readonly Uri MissingContextVersionValidationId = new Uri("dtmi:dtdl:parsingError:missingContextVersion");

        private static readonly Uri MissingDictKeyPropertyValueValidationId = new Uri("dtmi:dtdl:parsingError:missingDictKeyPropertyValue");

        private static readonly Uri MissingDtdlContextValidationId = new Uri("dtmi:dtdl:parsingError:missingDtdlContext");

        private static readonly Uri MissingDtmiSegPropertyValueValidationId = new Uri("dtmi:dtdl:parsingError:missingDtmiSegPropertyValue");

        private static readonly Uri MissingEssentialTypesValidationId = new Uri("dtmi:dtdl:parsingError:missingEssentialTypes");

        private static readonly Uri MissingExtensionContextValidationId = new Uri("dtmi:dtdl:parsingError:missingExtensionContext");

        private static readonly Uri MissingExtensionIdValidationId = new Uri("dtmi:dtdl:parsingError:missingExtensionId");

        private static readonly Uri MissingExtensionTypeValidationId = new Uri("dtmi:dtdl:parsingError:missingExtensionType");

        private static readonly Uri MissingExtensionVersionValidationId = new Uri("dtmi:dtdl:parsingError:missingExtensionVersion");

        private static readonly Uri MissingIdentifierPropertyValidationId = new Uri("dtmi:dtdl:parsingError:missingIdentifierProperty");

        private static readonly Uri MissingLiteralPropertyValidationId = new Uri("dtmi:dtdl:parsingError:missingLiteralProperty");

        private static readonly Uri MissingObjectPropertyValidationId = new Uri("dtmi:dtdl:parsingError:missingObjectProperty");

        private static readonly Uri MissingRequiredIdValidationId = new Uri("dtmi:dtdl:parsingError:missingRequiredId");

        private static readonly Uri MissingSubClassOfValidationId = new Uri("dtmi:dtdl:parsingError:missingSubClassOf");

        private static readonly Uri MissingSupplementalIdentifierPropertyValidationId = new Uri("dtmi:dtdl:parsingError:missingSupplementalIdentifierProperty");

        private static readonly Uri MissingSupplementalLiteralPropertyValidationId = new Uri("dtmi:dtdl:parsingError:missingSupplementalLiteralProperty");

        private static readonly Uri MissingSupplementalObjectPropertyValidationId = new Uri("dtmi:dtdl:parsingError:missingSupplementalObjectProperty");

        private static readonly Uri MissingTopLevelIdValidationId = new Uri("dtmi:dtdl:parsingError:missingTopLevelId");

        private static readonly Uri MissingTypeInfoValidationId = new Uri("dtmi:dtdl:parsingError:missingTypeInfo");

        private static readonly Uri ModelMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:modelMultipleValues");

        private static readonly Uri ModelNoGraphValidationId = new Uri("dtmi:dtdl:parsingError:modelNoGraph");

        private static readonly Uri ModelNotObjectValidationId = new Uri("dtmi:dtdl:parsingError:modelNotObject");

        private static readonly Uri ModelNoValueValidationId = new Uri("dtmi:dtdl:parsingError:modelNoValue");

        private static readonly Uri MultipleMaterialTypesValidationId = new Uri("dtmi:dtdl:parsingError:multipleMaterialTypes");

        private static readonly Uri MultipleReferenceValidationId = new Uri("dtmi:dtdl:parsingError:multipleReference");

        private static readonly Uri NoMatchingSupplementalPropertyValidationId = new Uri("dtmi:dtdl:parsingError:noMatchingSupplementalProperty");

        private static readonly Uri NonConformantDatatypeValidationId = new Uri("dtmi:dtdl:parsingError:nonConformantDatatype");

        private static readonly Uri NonConformantPropertyValueValidationId = new Uri("dtmi:dtdl:parsingError:nonConformantPropertyValue");

        private static readonly Uri NonDtmiContextSpecifierValidationId = new Uri("dtmi:dtdl:parsingError:nonDtmiContextSpecifier");

        private static readonly Uri NonUniqueImportedPropertyValueValidationId = new Uri("dtmi:dtdl:parsingError:nonUniqueImportedPropertyValue");

        private static readonly Uri NonUniquePropertyValueValidationId = new Uri("dtmi:dtdl:parsingError:nonUniquePropertyValue");

        private static readonly Uri NotJsonObjectValidationId = new Uri("dtmi:dtdl:parsingError:notJsonObject");

        private static readonly Uri NotRequiredBooleanValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredBooleanValue");

        private static readonly Uri NotRequiredIdentifierValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredIdentifierValue");

        private static readonly Uri NotRequiredIntegerValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredIntegerValue");

        private static readonly Uri NotRequiredJsonValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredJsonValue");

        private static readonly Uri NotRequiredLangStringValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredLangStringValue");

        private static readonly Uri NotRequiredLiteralValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredLiteralValue");

        private static readonly Uri NotRequiredNumericValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredNumericValue");

        private static readonly Uri NotRequiredStringValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredStringValue");

        private static readonly Uri NotRequiredTypeValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredType");

        private static readonly Uri NotRequiredValueValidationId = new Uri("dtmi:dtdl:parsingError:notRequiredValue");

        private static readonly Uri NotSiblingRequiredTypeValidationId = new Uri("dtmi:dtdl:parsingError:notSiblingRequiredType");

        private static readonly Uri NoTypeThatAllowsValidationId = new Uri("dtmi:dtdl:parsingError:noTypeThatAllows");

        private static readonly Uri NumericCountBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:numericCountBelowMin");

        private static readonly Uri NumericMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:numericMultipleValues");

        private static readonly Uri NumericNotNumericValidationId = new Uri("dtmi:dtdl:parsingError:numericNotNumeric");

        private static readonly Uri NumericObjectNoValueValidationId = new Uri("dtmi:dtdl:parsingError:numericObjectNoValue");

        private static readonly Uri NumericTypeNotNumericValidationId = new Uri("dtmi:dtdl:parsingError:numericTypeNotNumeric");

        private static readonly Uri NumericValueNotNumericValidationId = new Uri("dtmi:dtdl:parsingError:numericValueNotNumeric");

        private static readonly Uri ObjectCountAboveMaxValidationId = new Uri("dtmi:dtdl:parsingError:objectCountAboveMax");

        private static readonly Uri ObjectCountBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:objectCountBelowMin");

        private static readonly Uri ObjectMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:objectMultipleValues");

        private static readonly Uri PartitionTooLargeValidationId = new Uri("dtmi:dtdl:parsingError:partitionTooLarge");

        private static readonly Uri PathConstraintNoValueValidationId = new Uri("dtmi:dtdl:parsingError:pathConstraintNoValue");

        private static readonly Uri PathNotRecognizedValidationId = new Uri("dtmi:dtdl:parsingError:pathNotRecognized");

        private static readonly Uri PathNotStringValidationId = new Uri("dtmi:dtdl:parsingError:pathNotString");

        private static readonly Uri PathNotUniqueValidationId = new Uri("dtmi:dtdl:parsingError:pathNotUnique");

        private static readonly Uri PatternInvalidValidationId = new Uri("dtmi:dtdl:parsingError:patternInvalid");

        private static readonly Uri PropertyClassNotRecognizedValidationId = new Uri("dtmi:dtdl:parsingError:propertyClassNotRecognized");

        private static readonly Uri PropertyElementNotObjectValidationId = new Uri("dtmi:dtdl:parsingError:propertyElementNotObject");

        private static readonly Uri PropertyInvalidDtmiValidationId = new Uri("dtmi:dtdl:parsingError:propertyInvalidDtmi");

        private static readonly Uri PropertyIrrelevantDtmiOrTermValidationId = new Uri("dtmi:dtdl:parsingError:propertyIrrelevantDtmiOrTerm");

        private static readonly Uri PropertyNotDtmiNorTermValidationId = new Uri("dtmi:dtdl:parsingError:propertyNotDtmiNorTerm");

        private static readonly Uri PropertyUndefinedTermValidationId = new Uri("dtmi:dtdl:parsingError:propertyUndefinedTerm");

        private static readonly Uri RecursiveStructureNarrowValidationId = new Uri("dtmi:dtdl:parsingError:recursiveStructureNarrow");

        private static readonly Uri RecursiveStructureWideValidationId = new Uri("dtmi:dtdl:parsingError:recursiveStructureWide");

        private static readonly Uri RefNotStringOrObjectValidationId = new Uri("dtmi:dtdl:parsingError:refNotStringOrObject");

        private static readonly Uri RefObjectNotAllowedValidationId = new Uri("dtmi:dtdl:parsingError:refObjectNotAllowed");

        private static readonly Uri ReservedIdValidationId = new Uri("dtmi:dtdl:parsingError:reservedId");

        private static readonly Uri SiblingPropertyMismatchValidationId = new Uri("dtmi:dtdl:parsingError:siblingPropertyMismatch");

        private static readonly Uri StringConstraintMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:stringConstraintMultipleValues");

        private static readonly Uri StringConstraintNotStringValidationId = new Uri("dtmi:dtdl:parsingError:stringConstraintNotString");

        private static readonly Uri StringConstraintNoValueValidationId = new Uri("dtmi:dtdl:parsingError:stringConstraintNoValue");

        private static readonly Uri StringCountBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:stringCountBelowMin");

        private static readonly Uri StringInvalidValidationId = new Uri("dtmi:dtdl:parsingError:stringInvalid");

        private static readonly Uri StringMultipleValuesValidationId = new Uri("dtmi:dtdl:parsingError:stringMultipleValues");

        private static readonly Uri StringNotStringValidationId = new Uri("dtmi:dtdl:parsingError:stringNotString");

        private static readonly Uri StringObjectNoValueValidationId = new Uri("dtmi:dtdl:parsingError:stringObjectNoValue");

        private static readonly Uri StringTooLongValidationId = new Uri("dtmi:dtdl:parsingError:stringTooLong");

        private static readonly Uri StringTypeNotStringValidationId = new Uri("dtmi:dtdl:parsingError:stringTypeNotString");

        private static readonly Uri StringValueNotStringValidationId = new Uri("dtmi:dtdl:parsingError:stringValueNotString");

        private static readonly Uri SubClassOfNotExtensibleValidationId = new Uri("dtmi:dtdl:parsingError:subClassOfNotExtensible");

        private static readonly Uri SubClassOfNotRecognizedValidationId = new Uri("dtmi:dtdl:parsingError:subClassOfNotRecognized");

        private static readonly Uri TopLevelGraphDisallowedValidationId = new Uri("dtmi:dtdl:parsingError:topLevelGraphDisallowed");

        private static readonly Uri TopLevelKeywordDisallowedValidationId = new Uri("dtmi:dtdl:parsingError:topLevelKeywordDisallowed");

        private static readonly Uri TypeInvalidDtmiValidationId = new Uri("dtmi:dtdl:parsingError:typeInvalidDtmi");

        private static readonly Uri TypeIrrelevantDtmiOrTermValidationId = new Uri("dtmi:dtdl:parsingError:typeIrrelevantDtmiOrTerm");

        private static readonly Uri TypeNotDtmiNorTermValidationId = new Uri("dtmi:dtdl:parsingError:typeNotDtmiNorTerm");

        private static readonly Uri TypeNotRootableValidationId = new Uri("dtmi:dtdl:parsingError:typeNotRootable");

        private static readonly Uri TypeUndefinedTermValidationId = new Uri("dtmi:dtdl:parsingError:typeUndefinedTerm");

        private static readonly Uri UndefinedReservedIdValidationId = new Uri("dtmi:dtdl:parsingError:undefinedReservedId");

        private static readonly Uri UnrecognizedContextVersionValidationId = new Uri("dtmi:dtdl:parsingError:unrecognizedContextVersion");

        private static readonly Uri UnresolvableContextSpecifierValidationId = new Uri("dtmi:dtdl:parsingError:unresolvableContextSpecifier");

        private static readonly Uri UnresolvableContextVersionValidationId = new Uri("dtmi:dtdl:parsingError:unresolvableContextVersion");

        private static readonly Uri ValueAboveMaxValidationId = new Uri("dtmi:dtdl:parsingError:valueAboveMax");

        private static readonly Uri ValueAboveRangeValidationId = new Uri("dtmi:dtdl:parsingError:valueAboveRange");

        private static readonly Uri ValueBelowMinValidationId = new Uri("dtmi:dtdl:parsingError:valueBelowMin");

        private static readonly Uri ValueBelowRangeValidationId = new Uri("dtmi:dtdl:parsingError:valueBelowRange");

        private static readonly Uri ValueNotExactValidationId = new Uri("dtmi:dtdl:parsingError:valueNotExact");

        private static readonly Uri ValueObjectContextValidationId = new Uri("dtmi:dtdl:parsingError:valueObjectContext");

        private static readonly Uri ValueObjectGraphValidationId = new Uri("dtmi:dtdl:parsingError:valueObjectGraph");

        private static readonly Uri ValueObjectIdValidationId = new Uri("dtmi:dtdl:parsingError:valueObjectId");

        private static readonly Uri ValueObjectInvalidPropValidationId = new Uri("dtmi:dtdl:parsingError:valueObjectInvalidProp");

        private static readonly Uri ValueObjectKeywordValidationId = new Uri("dtmi:dtdl:parsingError:valueObjectKeyword");

        private static readonly Uri ValueObjectLanguageValidationId = new Uri("dtmi:dtdl:parsingError:valueObjectLanguage");

        private static readonly Uri WrongParentValidationId = new Uri("dtmi:dtdl:parsingError:wrongParent");

        /// <summary>
        /// Generate an appropriate parsing error and add it to the collection.
        /// </summary>
        /// <param name="validationCode">A string uniquely identifying the validation condition that identifies the error.</param>
        /// <param name="contextId">A context identifer relevant to the error.</param>
        /// <param name="elementId">A URI that identifies the element in which the error was found.</param>
        /// <param name="referenceId">A URI that is referenced and thereby causes or contributes to the error.</param>
        /// <param name="governingId">A URI of an element that governs the value in which the error was found.</param>
        /// <param name="typeId">A URI that identifies a type relevant to the error.</param>
        /// <param name="identifier">A string that represents an identifier that is not necessarily valid.</param>
        /// <param name="cotype">A string representing a required or prohibited co-type on the element in which the error was found.</param>
        /// <param name="relation">A string describing a relationship to another element that is required or prohibited.</param>
        /// <param name="referenceType">A string describing a type that is directly or indirectly referenced and thereby causes or contributes to the error.</param>
        /// <param name="propertyName">A string representing the name of the property among whose values the error was found.</param>
        /// <param name="propertyConjunction">A string describing a conjunction of property names among whose values the error was found.</param>
        /// <param name="propertyDisjunction">A string describing a disjunction of property names among whose values the error was found.</param>
        /// <param name="governingPropertyName">A string representing the name of a property that governs the value in which the error was found.</param>
        /// <param name="constraintName">A string representing the name of a constraint relevant to the error.</param>
        /// <param name="nestedName">A string indicating a property name within an element that is a value of the incident property.</param>
        /// <param name="literalPropertyName">A string indicating a property on a JSON object value that is a localized string or representational literal.</param>
        /// <param name="refPropertyName">A string representing the name of a property that references another element that has a property that contributes to the error.</param>
        /// <param name="propertyValue">A string indicating a value of the property that is erroneous.</param>
        /// <param name="constraintValue">A string indicating a constraint that identifies or relates to the error.</param>
        /// <param name="contextValue">A string indicating the value of a context specifier.</param>
        /// <param name="termValue">A string indicating the value of a term definition in a local context block.</param>
        /// <param name="nestedValue">A string indicating the value of a property within an element that is a value of the incident property.</param>
        /// <param name="refValue">A string indicating the value of a referenced property value.</param>
        /// <param name="valueConjunction">A string indicating a conjunction of property values.</param>
        /// <param name="nameValuePair">A string describing a name-value pair relevant to the error.</param>
        /// <param name="typeRestriction">A string representing a required or prohibited type of the erroneous value.</param>
        /// <param name="valueRestriction">A string representing a required or prohibited value for the erroneous value.</param>
        /// <param name="relationRestriction">A string representing a requirement or prohibition on a relationship to another elemnt.</param>
        /// <param name="versionRestriction">A string describing a the required or prohibited versions for a value.</param>
        /// <param name="langCode">A string representing a language code.</param>
        /// <param name="keyword">A string indicating a JSON-LD keyword relevant to the error.</param>
        /// <param name="pattern">A string representing a regular expression pattern.</param>
        /// <param name="limit">A string describing a numeric limit.</param>
        /// <param name="range">A string describing a range of values.</param>
        /// <param name="datatype">A string describing a literal datatype.</param>
        /// <param name="elementType">A string describing a DTDL element type.</param>
        /// <param name="term">A string indicating a JSON-LD term relevant to the error.</param>
        /// <param name="layer">The name of the layer in which the error was found.</param>
        /// <param name="version">A version number related to the error.</param>
        /// <param name="partition">A string indicating the partition in which the error was found.</param>
        /// <param name="refPartition">A string indicating a partition that is referenced by the erroneous property.</param>
        /// <param name="violations">A collection of strings that each indicate a validation failure, if there are any.</param>
        /// <param name="observedCount">A numerical value for the observed count of some item.</param>
        /// <param name="expectedCount">A numerical value for the expected count of some item.</param>
        /// <param name="violationCount">A numerical value for the count of validation failures.</param>
        /// <param name="contextComponent">A <see cref="JsonLdContextComponent"/> in which the error was found.</param>
        /// <param name="element">A <see cref="JsonLdElement"/> in which the error was found.</param>
        /// <param name="extantElement">A <see cref="JsonLdElement"/> that is extant.</param>
        /// <param name="siblingElement">A <see cref="JsonLdElement"/> that is a sibling of <paramref name="element"/>.</param>
        /// <param name="ancestorElement">Used in conjunction with <paramref name="descendantElement"/>, a <see cref="JsonLdElement"/> higher in the hierarchy that contains the error.</param>
        /// <param name="descendantElement">Used in conjunction with <paramref name="ancestorElement"/>, a <see cref="JsonLdElement"/> lower in the hierarchy that contains the error.</param>
        /// <param name="incidentProperty">A <see cref="JsonLdProperty"/> in which the error was found.</param>
        /// <param name="extantProperty">A <see cref="JsonLdProperty"/> for an extant property related to the error.</param>
        /// <param name="governingProperty">A <see cref="JsonLdProperty"/> for a governing property related to the error.</param>
        /// <param name="refProperty">A <see cref="JsonLdProperty"/> for a property that references another element that has a property that contributes to the error.</param>
        /// <param name="incidentValues">A <see cref="JsonLdValueCollection"/> that holds values of the erroneous property.</param>
        /// <param name="governingValues">A <see cref="JsonLdValueCollection"/> that holds values of a governing property.</param>
        /// <param name="incidentValue">A <see cref="JsonLdValue"/> that holds the erroneous property value.</param>
        /// <param name="extantValue">A <see cref="JsonLdValue"/> that holds an extant value related to the error.</param>
        /// <param name="extensionProperty">A <see cref="DtdlExtensionProperty"/> that holds information about a property defined by an extension.</param>
        internal void Notify(string validationCode, Uri contextId = null, Uri elementId = null, Uri referenceId = null, Uri governingId = null, Uri typeId = null, string identifier = null, string cotype = null, string relation = null, string referenceType = null, string propertyName = null, string propertyConjunction = null, string propertyDisjunction = null, string governingPropertyName = null, string constraintName = null, string nestedName = null, string literalPropertyName = null, string refPropertyName = null, string propertyValue = null, string constraintValue = null, string contextValue = null, string termValue = null, string nestedValue = null, string refValue = null, string valueConjunction = null, string nameValuePair = null, string typeRestriction = null, string valueRestriction = null, string relationRestriction = null, string versionRestriction = null, string langCode = null, string keyword = null, string pattern = null, string limit = null, string range = null, string datatype = null, string elementType = null, string term = null, string layer = null, string version = null, string partition = null, string refPartition = null, IReadOnlyCollection<string> violations = null, int? observedCount = null, int? expectedCount = null, int? violationCount = null, JsonLdContextComponent contextComponent = null, JsonLdElement element = null, JsonLdElement extantElement = null, JsonLdElement siblingElement = null, JsonLdElement ancestorElement = null, JsonLdElement descendantElement = null, JsonLdProperty incidentProperty = null, JsonLdProperty extantProperty = null, JsonLdProperty governingProperty = null, JsonLdProperty refProperty = null, JsonLdValueCollection incidentValues = null, JsonLdValueCollection governingValues = null, JsonLdValue incidentValue = null, JsonLdValue extantValue = null, DtdlExtensionProperty extensionProperty = null)
        {
            string sourceName1 = string.Empty;
            int startLine1 = 0;
            int endLine1 = 0;

            string sourceName2 = string.Empty;
            int startLine2 = 0;
            int endLine2 = 0;

            switch (validationCode)
            {
                case "abstractSupplementalType":
                    if (elementId == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or cotype when generating abstractSupplementalType ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            AbstractSupplementalTypeValidationId,
                            "In {sourceName1}, @type{line1} specifies supplemental type {type}, which is abstract.",
                            "Remove @type {type} or replace it with a concrete subtype of {type}.",
                            primaryId: elementId,
                            type: cotype,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            AbstractSupplementalTypeValidationId,
                            "{layer}{primaryId:n} has @type that specifies supplemental type {type}, which is abstract.",
                            "Remove @type {type} or replace it with a concrete subtype of {type}.",
                            primaryId: elementId,
                            type: cotype,
                            layer: layer);
                    }

                    return;
                case "badDtmiOrTerm":
                    if (elementId == null || propertyName == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue when generating badDtmiOrTerm ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            BadDtmiOrTermValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value '{value}'{line2} that is neither a valid DTMI reference nor a DTDL term.",
                            "Replace the value of property '{property}' with a valid DTMI reference or a term defined by DTDL -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            BadDtmiOrTermValidationId,
                            "{layer}{primaryId:p} property '{property}' has value '{value}' that is neither a valid DTMI reference nor a DTDL term.",
                            "Replace the value of property '{property}' with a valid DTMI reference or a term defined by DTDL -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer);
                    }

                    return;
                case "booleanCountBelowMin":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating booleanCountBelowMin ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} boolean {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            BooleanCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} boolean {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            BooleanCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "booleanMultipleValues":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating booleanMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            BooleanMultipleValuesValidationId,
                            "In {sourceName1},{line1}, property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            BooleanMultipleValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "booleanNotBoolean":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating booleanNotBoolean ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            BooleanNotBooleanValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is not a JSON boolean.",
                            "Change the value of '{property}' to one of the JSON boolean values true or false.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            BooleanNotBooleanValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is not a JSON boolean.",
                            "Change the value of '{property}' to one of the JSON boolean values true or false.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "booleanObjectNoValue":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating booleanObjectNoValue ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            BooleanObjectNoValueValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a boolean value to the object, or replace the object with a JSON boolean.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            BooleanObjectNoValueValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a boolean value to the object, or replace the object with a JSON boolean.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "booleanTypeNotBoolean":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating booleanTypeNotBoolean ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            BooleanTypeNotBooleanValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:boolean'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:boolean'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            BooleanTypeNotBooleanValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:boolean'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:boolean'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "booleanValueNotBoolean":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating booleanValueNotBoolean ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            BooleanValueNotBooleanValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@value' is not a JSON boolean.",
                            "Change the value of the '@value' property of '{property}' to one of the JSON boolean values true or false.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            BooleanValueNotBooleanValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@value' is not a JSON boolean.",
                            "Change the value of the '@value' property of '{property}' to one of the JSON boolean values true or false.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "childOfNotRecognized":
                    if (contextId == null || elementId == null || constraintName == null || constraintValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName or constraintValue when generating childOfNotRecognized ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("dtmm:childOf", out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ChildOfNotRecognizedValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'dtmm:childOf' property '{value}'{line1} is not a recognized DTDL element name.",
                            "Remove 'dtmm:childOf' property or change its value to a JSON string that indicates a DTDL element name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            value: constraintValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ChildOfNotRecognizedValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'dtmm:childOf' property '{value}' is not a recognized DTDL element name.",
                            "Remove 'dtmm:childOf' property or change its value to a JSON string that indicates a DTDL element name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            value: constraintValue);
                    }

                    return;
                case "classNotRecognized":
                    if (contextId == null || elementId == null || constraintName == null || constraintValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName or constraintValue when generating classNotRecognized ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ClassNotRecognizedValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has '{property}' value whose 'sh:class' property '{value}'{line1} is not a recognized DTDL type name.",
                            "Remove 'sh:class' property or change its value to a JSON string that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            value: constraintValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ClassNotRecognizedValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has '{property}' value whose 'sh:class' property '{value}' is not a recognized DTDL type name.",
                            "Remove 'sh:class' property or change its value to a JSON string that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            value: constraintValue);
                    }

                    return;
                case "classNotString":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating classNotString ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ClassNotStringValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has '{property}' value whose 'sh:class' property{line1} has no value or is not a string.",
                            "Change the value of 'sh:class' property to a JSON string that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ClassNotStringValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has '{property}' value whose 'sh:class' property has no value or is not a string.",
                            "Change the value of 'sh:class' property to a JSON string that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "conflictingSupplementalTypes":
                    if (elementId == null || elementType == null || referenceType == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or elementType or referenceType or propertyName when generating conflictingSupplementalTypes ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            ConflictingSupplementalTypesValidationId,
                            "In {sourceName1}, '@type'{line1} includes '{type}' and '{restriction}', both of which add property '{property}' to element.",
                            "Remove either '{type}' or '{restriction}' from set of @type values.",
                            primaryId: elementId,
                            type: elementType,
                            restriction: referenceType,
                            property: propertyName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ConflictingSupplementalTypesValidationId,
                            "{primaryId:n} has supplemental types '{type}' and '{restriction}' that both add property '{property}' to element.",
                            "Remove either '{type}' or '{restriction}' from set of @type values.",
                            primaryId: elementId,
                            type: elementType,
                            restriction: referenceType,
                            property: propertyName);
                    }

                    return;
                case "constraintMissingClass":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating constraintMissingClass ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ConstraintMissingClassValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has value that is missing 'sh:class' property.",
                            "Add a 'sh:class' property with a JSON string value that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ConstraintMissingClassValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has value that is missing 'sh:class' property.",
                            "Add a 'sh:class' property with a JSON string value that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "constraintNotObject":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating constraintNotObject ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ConstraintNotObjectValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has value that is not a JSON object.",
                            "Change the value of '{property}' to a JSON array of JSON objects that each expresses a class constraint on the extension type.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ConstraintNotObjectValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has value that is not a JSON object.",
                            "Change the value of '{property}' to a JSON array of JSON objects that each expresses a class constraint on the extension type.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "cotypeNotConcreteMaterial":
                    if (contextId == null || elementId == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or cotype when generating cotypeNotConcreteMaterial ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            CotypeNotConcreteMaterialValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:or'/'sh:class' cotype constraint '{type}'{line1} that is not a DTDL concrete material class.",
                            "Change the value of 'sh:class' to the name of a DTDL concrete material class.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            type: cotype,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            CotypeNotConcreteMaterialValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:or'/'sh:class' cotype constraint '{type}' that is not a DTDL concrete material class.",
                            "Change the value of 'sh:class' to the name of a DTDL concrete material class.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            type: cotype);
                    }

                    return;
                case "crossPartitionReference":
                    if (elementId == null || propertyName == null || referenceId == null || refPartition == null || partition == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or refPartition or partition when generating crossPartitionReference ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && element != null && element.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            CrossPartitionReferenceValidationId,
                            "Illegal cross-partion reference -- in {sourceName1}, property '{property}'{line1} refers to {secondaryId} whose definition{line2}{source2} is nested within {value}.",
                            "Create a copy of {secondaryId} within {restriction}, give it a unique @id value, and refer to it instead of {secondaryId}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: refPartition,
                            restriction: partition,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            CrossPartitionReferenceValidationId,
                            "Illegal cross-partion reference -- {primaryId:p} property '{property}' refers to {secondaryId}, whose definition is nested within {value}.",
                            "Create a copy of {secondaryId} within {restriction}, give it a unique @id value, and refer to it instead of {secondaryId}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: refPartition,
                            restriction: partition);
                    }

                    return;
                case "disallowedCocotype":
                    if (elementId == null || elementType == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or elementType or cotype when generating disallowedCocotype ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            DisallowedCocotypeValidationId,
                            "In {sourceName1}, @type{line1} specifies {type}, but this may not be co-typed with {restriction}.",
                            "Remove @type {type} or {restriction} from element.",
                            primaryId: elementId,
                            type: elementType,
                            restriction: cotype,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            DisallowedCocotypeValidationId,
                            "{primaryId:n} has @type with value {type} that may not be co-typed on elements that are also co-typed with {restriction}.",
                            "Remove @type {type} or {restriction} from element.",
                            primaryId: elementId,
                            type: elementType,
                            restriction: cotype);
                    }

                    return;
                case "disallowedContextVersion":
                    if (contextValue == null || version == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue or version when generating disallowedContextVersion ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            DisallowedContextVersionValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1}, which specifies a DTDL version that exceeds the configured max version of {restriction}.",
                            "Modify @context specifier to indicate a DTDL major version no greater than {restriction}.",
                            value: contextValue,
                            restriction: version,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            DisallowedContextVersionValidationId,
                            "@context specifier has value '{value}', which specifies a DTDL version that exceeds the configured max version of {restriction}.",
                            "Modify @context specifier to indicate a DTDL major version no greater than {restriction}.",
                            value: contextValue,
                            restriction: version);
                    }

                    return;
                case "disallowedIdFragment":
                    if (elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId when generating disallowedIdFragment ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            DisallowedIdFragmentValidationId,
                            "In {sourceName1}, identifier '{primaryId}'{line1} includes a fragment suffix, which is not permitted.",
                            "Remove fragment suffix from identifier.",
                            primaryId: elementId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            DisallowedIdFragmentValidationId,
                            "Identifier '{primaryId}' includes a fragment suffix, which is not permitted.",
                            "Remove fragment suffix from identifier.",
                            primaryId: elementId);
                    }

                    return;
                case "disallowedLocalContext":
                    if (version == null)
                    {
                        throw new ArgumentException("Missing required parameter version when generating disallowedLocalContext ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            DisallowedLocalContextValidationId,
                            "In {sourceName1}, @context value contains a block of local context definitions{line1}, which are not allowed in DTDL version {restriction}.",
                            "Remove the local context object, or perhaps try specifiying a different version of DTDL.",
                            restriction: version,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            DisallowedLocalContextValidationId,
                            "@context value contains a block of local context definitions, which are not allowed in DTDL version {restriction}.",
                            "Remove the local context object, or perhaps try specifiying a different version of DTDL.",
                            restriction: version);
                    }

                    return;
                case "disallowedType":
                    if (elementId == null || propertyName == null || referenceId == null || refPropertyName == null || refValue == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or refPropertyName or refValue or typeRestriction when generating disallowedType ParsingError.");
                    }

                    if (refProperty != null && refProperty.TryGetSourceLocation(out sourceName1, out startLine1) && siblingElement != null && siblingElement.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            DisallowedTypeValidationId,
                            "In {sourceName1}, property '{property}'{line1} refers to element{line2}{source2} that has disallowed @type of {type}.",
                            "Either remove @type {type} from the currently referenced element, or provide a value for property '{property}' that refers to a different sibling element.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            type: typeRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            DisallowedTypeValidationId,
                            "{primaryId:p} property '{property}' refers to sibling with {auxProperty} '{value}', which has disallowed @type of {type}.",
                            "Either remove @type {type} from the currently referenced element, or provide a value for property '{property}' that refers to a different sibling element.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            type: typeRestriction);
                    }

                    return;
                case "disallowedVersionDefinition":
                    if (elementId == null || propertyName == null || version == null || versionRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or version or versionRestriction when generating disallowedVersionDefinition ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            DisallowedVersionDefinitionValidationId,
                            "In {sourceName1}, property '{property}' has a value{line1} that specifies DTDL context version {value}, which is not allowed for this property.",
                            "Change the DTDL context version of property '{property}' to one of the following: {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: version,
                            restriction: versionRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            DisallowedVersionDefinitionValidationId,
                            "{layer}{primaryId:p} property '{property}' has a value that specifies DTDL context version {value}, which is not allowed for this property.",
                            "Change the DTDL context version of property '{property}' to one of the following: {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: version,
                            restriction: versionRestriction);
                    }

                    return;
                case "disallowedVersionReference":
                    if (elementId == null || propertyName == null || referenceId == null || version == null || versionRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or version or versionRestriction when generating disallowedVersionReference ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && element != null && element.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            DisallowedVersionReferenceValidationId,
                            "In {sourceName1}, property '{property}'{line1} refers to {secondaryId}, which is defined{line2}{source2} using DTDL version {value}, which is not allowed for this property.",
                            "Change the value of property '{property}' to an element defined using one of the following DTDL versions: {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: version,
                            restriction: versionRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            DisallowedVersionReferenceValidationId,
                            "{primaryId:p} property '{property}' refers to {secondaryId}, which is defined using DTDL version {value}, which is not allowed for this property.",
                            "Change the value of property '{property}' to an element defined using one of the following DTDL versions: {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: version,
                            restriction: versionRestriction);
                    }

                    return;
                case "dtdlContextFollowsAffiliate":
                    if (contextValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue when generating dtdlContextFollowsAffiliate ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            DtdlContextFollowsAffiliateValidationId,
                            "In {sourceName1}, @context array contains DTDL context specifier '{value}'{line1} after an affiliate context specifier.",
                            "Rearrange context specifiers so that all DTDL context specifiers are at the beginning of @context array.",
                            value: contextValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            DtdlContextFollowsAffiliateValidationId,
                            "@context array contains DTDL context specifier '{value}' after an affiliate context specifier.",
                            "Rearrange context specifiers so that all DTDL context specifiers are at the beginning of @context array.",
                            value: contextValue);
                    }

                    return;
                case "dtmiSegPropertyNotStringOrNumber":
                    if (elementId == null || propertyName == null || nestedName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or nestedName when generating dtmiSegPropertyNotStringOrNumber ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            DtmiSegPropertyNotStringOrNumberValidationId,
                            "In {sourceName1}, element{line1} has property '{value}'{line2} whose value is not a string or number.",
                            "Replace '{value}' property with an appropriate value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: nestedName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            DtmiSegPropertyNotStringOrNumberValidationId,
                            "{layer}{primaryId:p} property '{property}' has property '{value}' whose value is not a string or number.",
                            "Replace '{value}' property with an appropriate value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: nestedName,
                            layer: layer);
                    }

                    return;
                case "duplicateDefinition":
                    if (elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId when generating duplicateDefinition ParsingError.");
                    }

                    if (extantElement != null && extantElement.TryGetSourceLocationForId(out sourceName1, out startLine1) && element != null && element.TryGetSourceLocationForId(out sourceName2, out startLine2))
                    {
                        this.Add(
                            DuplicateDefinitionValidationId,
                            "In {sourceName2}, identifier {primaryId} is the value of '@id' property{line2}, but this identifier is already used{source1} as the value of '@id' property{line1}.",
                            "Remove all but one JSON object containing '@id' property with value {primaryId}, or change the '@id' values so there are no duplicates.",
                            primaryId: elementId,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            DuplicateDefinitionValidationId,
                            "{layer}{primaryId} has more than one definition.",
                            "Remove all but one JSON object containing '@id' property with value {primaryId}, or change the '@id' values so there are no duplicates.",
                            primaryId: elementId,
                            layer: layer);
                    }

                    return;
                case "duplicatePropertyName":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating duplicatePropertyName ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            DuplicatePropertyNameValidationId,
                            "In {sourceName1}, property '{property}'{line1} already defined{line2}.",
                            "Remove duplicate uses of property '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            DuplicatePropertyNameValidationId,
                            "{primaryId:p} property '{property}' has multiple definitions.",
                            "Remove duplicate uses of property '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "emptyContext":
                    if (element != null && element.TryGetSourceLocationForContext(out sourceName1, out startLine1))
                    {
                        this.Add(
                            EmptyContextValidationId,
                            "In {sourceName1}, top-level JSON object has a @context specifier{line1} that is empty.",
                            "To the '@context' property, add a string whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            EmptyContextValidationId,
                            "Top-level JSON object has a @context specifier that is empty.",
                            "To the '@context' property, add a string whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.");
                    }

                    return;
                case "emptyExtensionContext":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating emptyExtensionContext ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForContext(out sourceName1, out startLine1))
                    {
                        this.Add(
                            EmptyExtensionContextValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has a @context specifier{line1} that is empty.",
                            "To the '@context' property, add a string whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            EmptyExtensionContextValidationId,
                            "DTDL language extension {primaryId} has a @context specifier that is empty.",
                            "To the '@context' property, add a string whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.",
                            primaryId: contextId);
                    }

                    return;
                case "excessiveCount":
                    if (elementId == null || propertyDisjunction == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyDisjunction or observedCount or expectedCount when generating excessiveCount ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("{primaryId:n} is at the root of a hierarchy that contains {count1} {property} properties, but the allowed maximum count is {count2}.");
                        SetCount(causeBuilder, 1, (int)observedCount);
                        SetCount(causeBuilder, 2, (int)expectedCount);

                        this.Add(
                            ExcessiveCountValidationId,
                            causeBuilder.ToString(),
                            "Remove one or more {property} property values to reduce the total count.",
                            primaryId: elementId,
                            property: propertyDisjunction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{primaryId:n} is at the root of a hierarchy that contains {count1} {property} properties, but the allowed maximum count is {count2}.");
                        SetCount(causeBuilder, 1, (int)observedCount);
                        SetCount(causeBuilder, 2, (int)expectedCount);

                        this.Add(
                            ExcessiveCountValidationId,
                            causeBuilder.ToString(),
                            "Remove one or more {property} property values to reduce the total count.",
                            primaryId: elementId,
                            property: propertyDisjunction);
                    }

                    return;
                case "excessiveDepthNarrow":
                    if (elementId == null || propertyDisjunction == null || referenceId == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyDisjunction or referenceId or observedCount or expectedCount when generating excessiveDepthNarrow ParsingError.");
                    }

                    if (ancestorElement != null && ancestorElement.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && descendantElement != null && descendantElement.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, element{line1} is at the root of a chain of {property} properties that exceeds {count2} levels -- element{line2}{source2} is at level {count1}.");
                        SetCount(causeBuilder, 1, (int)observedCount);
                        SetCount(causeBuilder, 2, (int)expectedCount);

                        this.Add(
                            ExcessiveDepthNarrowValidationId,
                            causeBuilder.ToString(),
                            "Change the value of one or more {property} properties in the hierarchy to reduce the nesting depth.",
                            primaryId: elementId,
                            property: propertyDisjunction,
                            secondaryId: referenceId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{primaryId:n} is at the root of a chain of {property} properties that exceeds {count2} levels -- element{secondaryId:e} is at level {count1}.");
                        SetCount(causeBuilder, 1, (int)observedCount);
                        SetCount(causeBuilder, 2, (int)expectedCount);

                        this.Add(
                            ExcessiveDepthNarrowValidationId,
                            causeBuilder.ToString(),
                            "Change the value of one or more {property} properties in the hierarchy to reduce the nesting depth.",
                            primaryId: elementId,
                            property: propertyDisjunction,
                            secondaryId: referenceId);
                    }

                    return;
                case "excessiveDepthWide":
                    if (elementId == null || referenceId == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or referenceId or observedCount or expectedCount when generating excessiveDepthWide ParsingError.");
                    }

                    if (ancestorElement != null && ancestorElement.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && descendantElement != null && descendantElement.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, element{line1} is at the root of a hierarchy that exceeds {count2} levels -- element{line2}{source2} is at level {count1}.");
                        SetCount(causeBuilder, 1, (int)observedCount);
                        SetCount(causeBuilder, 2, (int)expectedCount);

                        this.Add(
                            ExcessiveDepthWideValidationId,
                            causeBuilder.ToString(),
                            "Change the value of one or more properties of elements in the hierarchy to reduce the nesting depth.",
                            primaryId: elementId,
                            secondaryId: referenceId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{primaryId:n} is at the root of a hierarchy that exceeds {count2} levels -- element{secondaryId:e} is at level {count1}.");
                        SetCount(causeBuilder, 1, (int)observedCount);
                        SetCount(causeBuilder, 2, (int)expectedCount);

                        this.Add(
                            ExcessiveDepthWideValidationId,
                            causeBuilder.ToString(),
                            "Change the value of one or more properties of elements in the hierarchy to reduce the nesting depth.",
                            primaryId: elementId,
                            secondaryId: referenceId);
                    }

                    return;
                case "excludedType":
                    if (elementId == null || propertyDisjunction == null || referenceId == null || referenceType == null || elementType == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyDisjunction or referenceId or referenceType or elementType when generating excludedType ParsingError.");
                    }

                    if (ancestorElement != null && ancestorElement.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && descendantElement != null && descendantElement.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            ExcludedTypeValidationId,
                            "In {sourceName1}, element{line1} has type {type}, which is not allowed to contain the {property} value of type {restriction}{line2}{source2}.",
                            "Remove all elements of type {restriction} from {property} properties under the {type} element.",
                            primaryId: elementId,
                            property: propertyDisjunction,
                            secondaryId: referenceId,
                            restriction: referenceType,
                            type: elementType,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            ExcludedTypeValidationId,
                            "{primaryId:n} contains {restriction}{secondaryId:e}, which is not allowed in {property} properties under elements of type {type}.",
                            "Remove all elements of type {restriction} from {property} properties under the {type} element.",
                            primaryId: elementId,
                            property: propertyDisjunction,
                            secondaryId: referenceId,
                            restriction: referenceType,
                            type: elementType);
                    }

                    return;
                case "extensionNotAllowed":
                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ExtensionNotAllowedValidationId,
                            "In DTDL extension {sourceName1}, '@context'{line1} specifies a version that does not permit DTDL language extensions.",
                            "Use a later DTDL context version if possible.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionNotAllowedValidationId,
                            "In DTDL extension, '@context' specifies a version that does not permit DTDL language extensions.",
                            "Use a later DTDL context version if possible.");
                    }

                    return;
                case "extensionNotJsonObject":
                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ExtensionNotJsonObjectValidationId,
                            "In DTDL extension {sourceName1}, top-level JSON element{line1} is neither a JSON object nor a JSON array of JSON objects.",
                            "Update your model to follow the examples.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionNotJsonObjectValidationId,
                            "In DTDL extension, top-level JSON element is neither a JSON object nor a JSON array of JSON objects.",
                            "Update your model to follow the examples.");
                    }

                    return;
                case "extensionTermDefinitionInvalidDtmi":
                    if (contextId == null || term == null || identifier == null || version == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or term or identifier or version when generating extensionTermDefinitionInvalidDtmi ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            ExtensionTermDefinitionInvalidDtmiValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} context contains a definition for term '{property}'{line1} whose value '{value}' starts with 'dtmi:' but is not a valid DTMI or DTMI prefix for DTDL version {restriction}.",
                            "Change the value of term '{property}' either to a URI or URI prefix with a different scheme or to a valid DTMI or DTMI prefix -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: contextId,
                            property: term,
                            value: identifier,
                            restriction: version,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionTermDefinitionInvalidDtmiValidationId,
                            "DTDL language extension {primaryId} context contains a definition for term '{property}' whose value '{value}' starts with 'dtmi:' but is not a valid DTMI or DTMI prefix for DTDL version {restriction}.",
                            "Change the value of term '{property}' either to a URI or URI prefix with a different scheme or to a valid DTMI or DTMI prefix -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: contextId,
                            property: term,
                            value: identifier,
                            restriction: version);
                    }

                    return;
                case "extensionTermDefinitionInvalidUri":
                    if (contextId == null || term == null || identifier == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or term or identifier when generating extensionTermDefinitionInvalidUri ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            ExtensionTermDefinitionInvalidUriValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} context contains a definition for term '{property}'{line1} whose value '{value}' is not a valid URI or URI prefix.",
                            "Change the value of term '{property}' to a valid URI or URI prefix.",
                            primaryId: contextId,
                            property: term,
                            value: identifier,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionTermDefinitionInvalidUriValidationId,
                            "DTDL language extension {primaryId} context contains a definition for term '{property}' whose value '{value}' is not a valid URI or URI prefix.",
                            "Change the value of term '{property}' to a valid URI or URI prefix.",
                            primaryId: contextId,
                            property: term,
                            value: identifier);
                    }

                    return;
                case "extensionTermEmpty":
                    if (contextId == null || term == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or term when generating extensionTermEmpty ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            ExtensionTermEmptyValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} context defines a term {line1} that is an empty string.",
                            "Use a non-empty string of characters for the term.",
                            primaryId: contextId,
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionTermEmptyValidationId,
                            "DTDL language extension {primaryId} context defines a term that is an empty string.",
                            "Use a non-empty string of characters for the term.",
                            primaryId: contextId,
                            property: term);
                    }

                    return;
                case "extensionTermInvalid":
                    if (contextId == null || term == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or term when generating extensionTermInvalid ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            ExtensionTermInvalidValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} context defines a term '{property}'{line1} that contains invalid characters.",
                            "Use a different term that does not begin with '@' and that contains only letters, digits, and the characters '@', '-', '.', '_', '~', '!', '$', '&', ''', '(', ')', '*', '+', ',', ';', '='.",
                            primaryId: contextId,
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionTermInvalidValidationId,
                            "DTDL language extension {primaryId} context defines a term '{property}' that contains invalid characters.",
                            "Use a different term that does not begin with '@' and that contains only letters, digits, and the characters '@', '-', '.', '_', '~', '!', '$', '&', ''', '(', ')', '*', '+', ',', ';', '='.",
                            primaryId: contextId,
                            property: term);
                    }

                    return;
                case "extensionTermReserved":
                    if (contextId == null || term == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or term when generating extensionTermReserved ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            ExtensionTermReservedValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} context defines a term '{property}'{line1} that is defined by the DTDL context.",
                            "Use a different term that is not a DTDL reserved word.",
                            primaryId: contextId,
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionTermReservedValidationId,
                            "DTDL language extension {primaryId} context defines a term '{property}' that is defined by the DTDL context.",
                            "Use a different term that is not a DTDL reserved word.",
                            primaryId: contextId,
                            property: term);
                    }

                    return;
                case "extensionTermSchemePrefix":
                    if (contextId == null || term == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or term when generating extensionTermSchemePrefix ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            ExtensionTermSchemePrefixValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} context defines a term '{property}'{line1} which is reserved as the scheme prefix for DTDL identifiers.",
                            "Use a different term other than '{property}'.",
                            primaryId: contextId,
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ExtensionTermSchemePrefixValidationId,
                            "DTDL language extension {primaryId} context defines a term '{property}' which is reserved as the scheme prefix for DTDL identifiers.",
                            "Use a different term other than '{property}'.",
                            primaryId: contextId,
                            property: term);
                    }

                    return;
                case "graphDisallowed":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating graphDisallowed ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForGraph(out sourceName1, out startLine1))
                    {
                        this.Add(
                            GraphDisallowedValidationId,
                            "In {sourceName1}, property '{property}' has value that contains '@graph' property{line1}, which is not allowed.",
                            "Remove the '@graph' property, and elevate the value of this property to the top level of the JSON document.",
                            primaryId: elementId,
                            property: propertyName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            GraphDisallowedValidationId,
                            "{primaryId:p} property '{property}' has value that contains '@graph' property, which is not allowed.",
                            "Remove the '@graph' property, and elevate the value of this property to the top level of the JSON document.",
                            primaryId: elementId,
                            property: propertyName);
                    }

                    return;
                case "identifierCountAboveMax":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating identifierCountAboveMax ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}{line1}, property '{property}' has {count1} {item1} but no more than {count2} {item2} {verb2} allowed.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            IdentifierCountAboveMaxValidationId,
                            causeBuilder.ToString(),
                            "Remove one or more '{property}' property values from the object until the maximum count is not exceeded.",
                            primaryId: elementId,
                            property: propertyName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{primaryId:p} property '{property}' has {count1} {item1} but no more than {count2} {item2} {verb2} allowed.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            IdentifierCountAboveMaxValidationId,
                            causeBuilder.ToString(),
                            "Remove one or more '{property}' property values from the object until the maximum count is not exceeded.",
                            primaryId: elementId,
                            property: propertyName);
                    }

                    return;
                case "identifierCountBelowMin":
                    if (elementId == null || propertyName == null || typeRestriction == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or typeRestriction or observedCount or expectedCount when generating identifierCountBelowMin ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} string {item2} for '{property}', each of which is a valid {type}.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            IdentifierCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} string {item2} for '{property}', each of which is a valid {type}.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            IdentifierCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            layer: layer);
                    }

                    return;
                case "identifierInvalid":
                    if (elementId == null || propertyName == null || propertyValue == null || pattern == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or pattern when generating identifierInvalid ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IdentifierInvalidValidationId,
                            "In {sourceName1}, property '{property}' has value '{value}'{line1}, which is not valid for this property.",
                            "Modify the value of '{property}' to make it match the regular expression '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: pattern,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdentifierInvalidValidationId,
                            "{layer}{primaryId:p} property '{property}' has value '{value}', which is not valid for this property.",
                            "Modify the value of '{property}' to make it match the regular expression '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: pattern,
                            layer: layer);
                    }

                    return;
                case "identifierMultipleValues":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating identifierMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IdentifierMultipleValuesValidationId,
                            "In {sourceName1},{line1}, property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdentifierMultipleValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "identifierNotDtmi":
                    if (elementId == null || propertyName == null || typeRestriction == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or typeRestriction or propertyValue when generating identifierNotDtmi ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IdentifierNotDtmiValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value '{value}', which is neither a valid DTMI nor a term defined in an active context.",
                            "Change the value of '{property}' to a valid DTMI string or to a term defined in one of the active contexts.",
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            value: propertyValue,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdentifierNotDtmiValidationId,
                            "{layer}{primaryId:p} property '{property}' has value '{value}', which is neither a valid DTMI nor a term defined in an active context.",
                            "Change the value of '{property}' to a valid DTMI string or to a term defined in one of the active contexts.",
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            value: propertyValue,
                            layer: layer);
                    }

                    return;
                case "identifierNotString":
                    if (elementId == null || propertyName == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or typeRestriction when generating identifierNotString ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IdentifierNotStringValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is not a JSON string.",
                            "Change the value of '{property}' to a JSON string that is a valid {type}.",
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdentifierNotStringValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is not a JSON string.",
                            "Change the value of '{property}' to a JSON string that is a valid {type}.",
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            layer: layer);
                    }

                    return;
                case "identifierNotUri":
                    if (elementId == null || propertyName == null || typeRestriction == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or typeRestriction or propertyValue when generating identifierNotUri ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IdentifierNotUriValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value '{value}', which is neither a valid URI nor a term defined in an active context.",
                            "Change the value of '{property}' to a valid URI string or to a term defined in one of the active contexts.",
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            value: propertyValue,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdentifierNotUriValidationId,
                            "{layer}{primaryId:p} property '{property}' has value '{value}', which is neither a valid URI nor a term defined in an active context.",
                            "Change the value of '{property}' to a valid URI string or to a term defined in one of the active contexts.",
                            primaryId: elementId,
                            property: propertyName,
                            type: typeRestriction,
                            value: propertyValue,
                            layer: layer);
                    }

                    return;
                case "identifierTooLong":
                    if (elementId == null || propertyName == null || propertyValue == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or expectedCount when generating identifierTooLong ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, property '{property}' has value '{value}'{line1}, which is too long -- length limit is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for '{property}' or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            IdentifierTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has value '{value}', which is too long -- length limit is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for '{property}' or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            IdentifierTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer);
                    }

                    return;
                case "idNotString":
                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            IdNotStringValidationId,
                            "In {sourceName1}, '@id' property{line1} has a value that is empty or not a string.",
                            "Replace the value with a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdNotStringValidationId,
                            "'@id' property has a value that is empty or not a string.",
                            "Replace the value with a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.");
                    }

                    return;
                case "idRefBadDtmiOrTerm":
                    if (elementId == null || propertyName == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue when generating idRefBadDtmiOrTerm ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            IdRefBadDtmiOrTermValidationId,
                            "In {sourceName1}, '@id'{line1} has value '{value}' that is neither a valid DTMI reference nor a DTDL term.",
                            "Replace the value of '@id' with a valid DTMI reference or a term defined by DTDL -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdRefBadDtmiOrTermValidationId,
                            "{layer}{primaryId:n} has '{property}' value whose '@id' value '{value}' is neither a valid DTMI reference nor a DTDL term.",
                            "Replace the value of '@id' with a valid DTMI reference or a term defined by DTDL -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer);
                    }

                    return;
                case "idReference":
                    if (elementId == null || propertyName == null || identifier == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or identifier when generating idReference ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IdReferenceValidationId,
                            "In {sourceName1}, property '{property}' has an inline definition{line1} containing nothing but an '@id' property.",
                            "Replace the inline definition with a string value of '{value}', or provide a complete inline definition for the value of property '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: identifier,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IdReferenceValidationId,
                            "{layer}{primaryId:p} property '{property}' has an inline definition containing nothing but an '@id' property.",
                            "Replace the inline definition with a string value of '{value}', or provide a complete inline definition for the value of property '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: identifier,
                            layer: layer);
                    }

                    return;
                case "idTooLong":
                    if (identifier == null || expectedCount == null || version == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier or expectedCount or version when generating idTooLong ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, identifier '{value}'{line1} is too long -- length limit is {count1} {item1} for DTDL version {restriction}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for the identifier or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            IdTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            value: identifier,
                            restriction: version,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("Identifier '{value}' is too long -- length limit is {count1} {item1} for DTDL version {restriction}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for the identifier or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            IdTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            value: identifier,
                            restriction: version);
                    }

                    return;
                case "idTooLongForType":
                    if (identifier == null || elementType == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier or elementType or expectedCount when generating idTooLongForType ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, identifier '{value}'{line1} is too long for an element with @type {type} -- length limit for this type is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for the identifier or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            IdTooLongForTypeValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            value: identifier,
                            type: elementType,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("Identifier '{value}' is too long for an element with @type {type} -- length limit for this type is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for the identifier or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            IdTooLongForTypeValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            value: identifier,
                            type: elementType);
                    }

                    return;
                case "incompatibleType":
                    if (elementId == null || propertyName == null || referenceId == null || elementType == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or elementType or typeRestriction when generating incompatibleType ParsingError.");
                    }
                    {
                        this.Add(
                            IncompatibleTypeValidationId,
                            "{layer}{primaryId:p} property '{property}' has value{secondaryId:e} which is incompatible because it has @type {type} in other layers.",
                            "For the value of property '{property}', choose a new unique @id or choose an element whose @type is {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            type: elementType,
                            restriction: typeRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentBooleanValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentBooleanValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentBooleanValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has boolean value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentBooleanValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has boolean value {value}, but it already has value {restriction}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentContext":
                    if (elementId == null || version == null || versionRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or version or versionRestriction when generating inconsistentContext ParsingError.");
                    }
                    {
                        this.Add(
                            InconsistentContextValidationId,
                            "{layer}{primaryId:n} is defined using DTDL context version {value}, but it is defined using DTDL context version {restriction} in other layers.",
                            "Change the context specifiers so that all layers of any given identifer are defined using the same DTDL context version.",
                            primaryId: elementId,
                            value: version,
                            restriction: versionRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentIdentifierValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentIdentifierValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentIdentifierValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has identifier value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentIdentifierValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has identifier value '{value}', but it already has value '{restriction}'.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentIntegerValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentIntegerValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentIntegerValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has integer value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentIntegerValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has integer value {value}, but it already has value {restriction}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentJsonValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentJsonValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentJsonValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has JSON value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentJsonValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has JSON value {value}, but it already has value {restriction}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentLangStringValues":
                    if (elementId == null || propertyName == null || langCode == null || observedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or langCode or observedCount when generating inconsistentLangStringValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentLangStringValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has {item1} {value} than{line2}{source2}.",
                            "Remove redundant language codes from '{property}' properties, or modify the properties so that the value for each language code is consistent across all layers.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentLangStringValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has {item1} {value} than those defined in other layers.",
                            "Remove redundant language codes from '{property}' properties, or modify the properties so that the value for each language code is consistent across all layers.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer);
                    }

                    return;
                case "inconsistentLiteralValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentLiteralValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentLiteralValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has literal value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentLiteralValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has literal value {value}, but it already has value {restriction}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentNumericValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentNumericValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentNumericValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has numeric value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentNumericValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has numeric value {value}, but it already has value {restriction}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentObjectValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentObjectValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentObjectValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentObjectValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has identifier value '{value}', but it already has value '{restriction}'.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "inconsistentParent":
                    if (elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId when generating inconsistentParent ParsingError.");
                    }
                    {
                        this.Add(
                            InconsistentParentValidationId,
                            "{layer}{primaryId:n} has a different parent than in other layers.",
                            "Relocate one or more element layer definitions so that all layers of each element are defined at the top level or have a common parent element.",
                            primaryId: elementId,
                            layer: layer);
                    }

                    return;
                case "inconsistentPartition":
                    if (elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId when generating inconsistentPartition ParsingError.");
                    }
                    {
                        this.Add(
                            InconsistentPartitionValidationId,
                            "{layer}{primaryId:n} is defined in a different partition than in other layers.",
                            "Relocate one or more element layer definitions so that all layers of each element are defined at the top level or under a common top-level element.",
                            primaryId: elementId,
                            layer: layer);
                    }

                    return;
                case "inconsistentStringValues":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating inconsistentStringValues ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            InconsistentStringValuesValidationId,
                            "In {sourceName1}, property '{property}'{line1} has string value '{value}', but it has value '{restriction}'{line2}{source2}.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InconsistentStringValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has string value '{value}', but it already has value '{restriction}'.",
                            "Remove redundant instances of property '{property}', or change one or more '{property}' properties so that all values match.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "incorrectExtensionType":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating incorrectExtensionType ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            IncorrectExtensionTypeValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has '@type' property{line1} whose value is not 'DtdlExtension'.",
                            "Replace the value of property '@type' with the string value 'DtdlExtension'.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IncorrectExtensionTypeValidationId,
                            "DTDL language extension {primaryId} has '@type' property whose value is not 'DtdlExtension'.",
                            "Replace the value of property '@type' with the string value 'DtdlExtension'.",
                            primaryId: contextId);
                    }

                    return;
                case "incorrectPropertyValue":
                    if (elementId == null || propertyName == null || propertyValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or valueRestriction when generating incorrectPropertyValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            IncorrectPropertyValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value '{value}', but the value must be {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IncorrectPropertyValueValidationId,
                            "{primaryId:p} property '{property}' has value '{value}', but the value must be {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: valueRestriction);
                    }

                    return;
                case "inferredTypeDoesNotAllow":
                    if (elementId == null || referenceType == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or referenceType or propertyName when generating inferredTypeDoesNotAllow ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && element != null && element.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            InferredTypeDoesNotAllowValidationId,
                            "In {sourceName1}, element{line2} has inferred type {type}, which does not allow property '{property}'{line1}.",
                            "Remove property '{property}' or correct if misspelled.",
                            primaryId: elementId,
                            type: referenceType,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            InferredTypeDoesNotAllowValidationId,
                            "{layer}{primaryId:n} has inferred type {type}, which does not allow property '{property}'.",
                            "Remove property '{property}' or correct if misspelled.",
                            primaryId: elementId,
                            type: referenceType,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "intConstraintMultipleValues":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating intConstraintMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntConstraintMultipleValuesValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntConstraintMultipleValuesValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "intConstraintNotInteger":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating intConstraintNotInteger ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntConstraintNotIntegerValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' value is not a JSON integer.",
                            "Change the value of property '{property}' to a JSON integer.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntConstraintNotIntegerValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' value is not a JSON integer.",
                            "Change the value of property '{property}' to a JSON integer.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "intConstraintNoValue":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating intConstraintNoValue ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntConstraintNoValueValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' is empty.",
                            "Provide a single integer value for property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntConstraintNoValueValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' is empty.",
                            "Provide a single integer value for property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "integerCountBelowMin":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating integerCountBelowMin ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} integer {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            IntegerCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} integer {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            IntegerCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "integerMultipleValues":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating integerMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntegerMultipleValuesValidationId,
                            "In {sourceName1},{line1}, property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntegerMultipleValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "integerNotInteger":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating integerNotInteger ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntegerNotIntegerValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is not a JSON integer.",
                            "Change the value of '{property}' to a JSON integer.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntegerNotIntegerValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is not a JSON integer.",
                            "Change the value of '{property}' to a JSON integer.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "integerObjectNoValue":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating integerObjectNoValue ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntegerObjectNoValueValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a integer value to the object, or replace the object with a JSON integer.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntegerObjectNoValueValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a integer value to the object, or replace the object with a JSON integer.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "integerTypeNotInteger":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating integerTypeNotInteger ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntegerTypeNotIntegerValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:integer'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:integer'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntegerTypeNotIntegerValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:integer'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:integer'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "integerValueNotInteger":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating integerValueNotInteger ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            IntegerValueNotIntegerValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@value' is not a JSON integer.",
                            "Change the value of the '@value' property of '{property}' to a JSON integer.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            IntegerValueNotIntegerValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@value' is not a JSON integer.",
                            "Change the value of the '@value' property of '{property}' to a JSON integer.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "invalidContextSpecifier":
                    if (contextValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue when generating invalidContextSpecifier ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            InvalidContextSpecifierValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1} that is not a legal DTMI.",
                            "Replace the @context specifier with a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: contextValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidContextSpecifierValidationId,
                            "@context specifier has value '{value}' that is not a legal DTMI.",
                            "Replace the @context specifier with a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: contextValue);
                    }

                    return;
                case "invalidContextSpecifierForVersion":
                    if (contextValue == null || version == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue or version when generating invalidContextSpecifierForVersion ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            InvalidContextSpecifierForVersionValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1}, which is not a valid DTMI for DTDL version {restriction}.",
                            "Change @context specifier to a valid DTMI for DTDL version {restriction} -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: contextValue,
                            restriction: version,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidContextSpecifierForVersionValidationId,
                            "@context specifier has value '{value}', which is not a valid DTMI for DTDL version {restriction}.",
                            "Change @context specifier to a valid DTMI for DTDL version {restriction} -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: contextValue,
                            restriction: version);
                    }

                    return;
                case "invalidCotype":
                    if (elementId == null || cotype == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or cotype or typeRestriction when generating invalidCotype ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            InvalidCotypeValidationId,
                            "In {sourceName1}, element has @type{line1} with value {type} that can only be applied to elements of @type {restriction}.",
                            "Remove @type {type} from element.",
                            primaryId: elementId,
                            type: cotype,
                            restriction: typeRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidCotypeValidationId,
                            "{layer}{primaryId:n} has @type with value {type} that can only be applied to elements of @type {restriction}.",
                            "Remove @type {type} from element.",
                            primaryId: elementId,
                            type: cotype,
                            restriction: typeRestriction);
                    }

                    return;
                case "invalidCotypeVersion":
                    if (elementId == null || cotype == null || versionRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or cotype or versionRestriction when generating invalidCotypeVersion ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            InvalidCotypeVersionValidationId,
                            "In {sourceName1}, element has @type{line1} with value {type} that can only be applied to elements defined in DTDL version {restriction}.",
                            "Remove @type {type} from element.",
                            primaryId: elementId,
                            type: cotype,
                            restriction: versionRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidCotypeVersionValidationId,
                            "{layer}{primaryId:n} has @type with value {type} that can only be applied to elements defined in DTDL version {restriction}.",
                            "Remove @type {type} from element.",
                            primaryId: elementId,
                            type: cotype,
                            restriction: versionRestriction);
                    }

                    return;
                case "invalidExtensionIdElement":
                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            InvalidExtensionIdElementValidationId,
                            "In {sourceName1}, DTDL language extension '@id' property{line1} is not a non-empty JSON string.",
                            "Replace the value of property '@id' with a string that is a valid DTMI.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidExtensionIdElementValidationId,
                            "DTDL language extension '@id' property is not a non-empty JSON string.",
                            "Replace the value of property '@id' with a string that is a valid DTMI.");
                    }

                    return;
                case "invalidExtensionSpecifier":
                    if (identifier == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier when generating invalidExtensionSpecifier ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            InvalidExtensionSpecifierValidationId,
                            "In {sourceName1}, DTDL language extension has '@id' property{line1} with value '{value}' that is not a legal DTMI.",
                            "Replace the value of property '@id' with a string that that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: identifier,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidExtensionSpecifierValidationId,
                            "DTDL language extension has '@id' property with value '{value}' that is not a legal DTMI.",
                            "Replace the value of property '@id' with a string that that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: identifier);
                    }

                    return;
                case "invalidId":
                    if (identifier == null || version == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier or version when generating invalidId ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            InvalidIdValidationId,
                            "In {sourceName1}, identifier '{value}'{line1} is invalid for DTDL version {restriction}.",
                            "Replace the identifier with a string that conforms to the DTMI syntax for DTDL version {restriction} -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: identifier,
                            restriction: version,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidIdValidationId,
                            "Identifier '{value}' is invalid for DTDL version {restriction}.",
                            "Replace the identifier with a string that conforms to the DTMI syntax for DTDL version {restriction} -- see https://github.com/Azure/digital-twin-model-identifier.",
                            value: identifier,
                            restriction: version);
                    }

                    return;
                case "invalidIdForType":
                    if (identifier == null || elementType == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier or elementType when generating invalidIdForType ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            InvalidIdForTypeValidationId,
                            "In {sourceName1}, identifier '{value}'{line1} is invalid for an element with @type {type}",
                            "Replace the identifier with a string that conforms to the DTMI syntax allowed for elements of type {type}  -- see https://aka.ms/dtdl.",
                            value: identifier,
                            type: elementType,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            InvalidIdForTypeValidationId,
                            "Identifier '{value}' is invalid for an element with @type {type}.",
                            "Replace the identifier with a string that conforms to the DTMI syntax allowed for elements of type {type}  -- see https://aka.ms/dtdl.",
                            value: identifier,
                            type: elementType);
                    }

                    return;
                case "keywordDisallowed":
                    if (elementId == null || propertyName == null || keyword == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or keyword when generating keywordDisallowed ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForKeyword(keyword, out sourceName1, out startLine1))
                    {
                        this.Add(
                            KeywordDisallowedValidationId,
                            "In {sourceName1}, property '{property}' has value that contains '{value}' property{line1}, which is not allowed.",
                            "Remove the '{value}' property.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            KeywordDisallowedValidationId,
                            "{primaryId:p} property '{property}' has value that contains '{value}' property, which is not allowed.",
                            "Remove the '{value}' property.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword);
                    }

                    return;
                case "langMapObjKeyword":
                    if (elementId == null || propertyName == null || keyword == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or keyword when generating langMapObjKeyword ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForKeyword(keyword, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangMapObjKeywordValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '{value}' property, which does not belong in a language map object.",
                            "Remove '{value}' property, and ensure object contains only properties that map from ISO 639 language codes to string values.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangMapObjKeywordValidationId,
                            "{layer}{primaryId:p} property '{property}' has '{value}' property, which does not belong in a language map object.",
                            "Remove '{value}' property, and ensure object contains only properties that map from ISO 639 language codes to string values.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword,
                            layer: layer);
                    }

                    return;
                case "langStringCodeNotUnique":
                    if (elementId == null || propertyName == null || langCode == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or langCode when generating langStringCodeNotUnique ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && extantValue != null && extantValue.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            LangStringCodeNotUniqueValidationId,
                            "In {sourceName1}, property '{property}' has value for language code '{value}'{line1}, but language code '{value}' already has a value defined{line2}.",
                            "Modify the language map for property '{property}' so that it contains only one value for language code '{value}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            LangStringCodeNotUniqueValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values for language code '{value}'.",
                            "Modify the language map for property '{property}' so that it contains only one value for language code '{value}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer);
                    }

                    return;
                case "langStringElementCodeNotString":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementCodeNotString ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForLanguage(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementCodeNotStringValidationId,
                            "In {sourceName1}, '@language' property{line1} has value that is empty or not a string.",
                            "Replace the value with a string that is a valid ISO 639 language code -- see https://www.iso.org/iso-639-language-codes.html.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementCodeNotStringValidationId,
                            "{layer}{primaryId:p} property '{property}' array has element with '@language' property whose value is not a JSON string.",
                            "Change the value of '{property}' array element property '@language' to a JSON string that is a valid ISO 639 language code -- see https://www.iso.org/iso-639-language-codes.html.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementCodeNotUnique":
                    if (elementId == null || propertyName == null || langCode == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or langCode when generating langStringElementCodeNotUnique ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && extantValue != null && extantValue.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            LangStringElementCodeNotUniqueValidationId,
                            "In {sourceName1}, property '{property}' has value for language code '{value}'{line1}, but language code '{value}' already has a value defined{line2}.",
                            "Modify the language map for property '{property}' so that it contains only one value for language code '{value}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementCodeNotUniqueValidationId,
                            "{layer}{primaryId:p} property '{property}' is array in which language code '{value}' is duplicated.",
                            "Remove redundant instances of language code '{value}' from '{property}' array.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer);
                    }

                    return;
                case "langStringElementContext":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementContext ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForContext(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementContextValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@context' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementContextValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@context' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementGraph":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementGraph ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForGraph(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementGraphValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@graph' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementGraphValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@graph' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementId":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementId ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementIdValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@id' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementIdValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@id' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementInvalidCode":
                    if (elementId == null || propertyName == null || langCode == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or langCode when generating langStringElementInvalidCode ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForLanguage(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementInvalidCodeValidationId,
                            "In {sourceName1}, '@language' property{line1} has value '{value}', which is not valid according to ISO 639.",
                            "Change the language code '{value}' to a valid ISO 639 language code -- see https://www.iso.org/iso-639-language-codes.html.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementInvalidCodeValidationId,
                            "{layer}{primaryId:p} property '{property}' array has element with language code '{value}', which is not vlid according to ISO 639.",
                            "Change the language code '{value}' to a valid ISO 639 language code -- see https://www.iso.org/iso-639-language-codes.html.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer);
                    }

                    return;
                case "langStringElementInvalidProp":
                    if (elementId == null || propertyName == null || literalPropertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or literalPropertyName when generating langStringElementInvalidProp ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementInvalidPropValidationId,
                            "In {sourceName1},{line1}, property '{property}' has property '{value}', which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: literalPropertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementInvalidPropValidationId,
                            "{layer}{primaryId:p} property '{property}' has property '{value}', which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: literalPropertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementKeyword":
                    if (elementId == null || propertyName == null || keyword == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or keyword when generating langStringElementKeyword ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForKeyword(keyword, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementKeywordValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '{value}' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementKeywordValidationId,
                            "{layer}{primaryId:p} property '{property}' has '{value}' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword,
                            layer: layer);
                    }

                    return;
                case "langStringElementNotStringOrObject":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementNotStringOrObject ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LangStringElementNotStringOrObjectValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is not a JSON string or object.",
                            "Change all values of '{property}' to JSON objects, with the possible exception of one JSON string for the default language.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementNotStringOrObjectValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is not a JSON string or object.",
                            "Change all values of '{property}' to JSON objects, with the possible exception of one JSON string for the default language.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementNoValue":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementNoValue ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LangStringElementNoValueValidationId,
                            "In {sourceName1}, property '{property}' array has element{line1} that does not contain a '@value' property.",
                            "Add a '@value' property to the element with a string value that is the desired text for property '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementNoValueValidationId,
                            "{layer}{primaryId:p} property '{property}' array has element that does not contain a '@value' property.",
                            "Ensure every object in property '{property}' array has a '@value' property that is a string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementType":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementType ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementTypeValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@type' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementTypeValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@type' property, which is not allowed in a language-tagged value object.",
                            "Remove all properties from object other than '@value' and '@language'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringElementValueNotString":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating langStringElementValueNotString ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForValue(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringElementValueNotStringValidationId,
                            "In {sourceName1}, '@value' property{line1} has value that is not a string.",
                            "Replace the value with a string that is the desired text for property '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringElementValueNotStringValidationId,
                            "{layer}{primaryId:p} property '{property}' array has element with '@value' value that is not a JSON string.",
                            "Change the value of '{property}' array element property '@value' to a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "langStringInvalidCode":
                    if (elementId == null || propertyName == null || langCode == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or langCode when generating langStringInvalidCode ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringInvalidCodeValidationId,
                            "In {sourceName1},{line1}, property '{property}' has language code '{value}', which is not valid according to ISO 639.",
                            "Change the language code '{value}' to a valid ISO 639 language code -- see https://www.iso.org/iso-639-language-codes.html.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringInvalidCodeValidationId,
                            "{layer}{primaryId:p} property '{property}' has language code '{value}', which is not valid according to ISO 639.",
                            "Change the language code '{value}' to a valid ISO 639 language code -- see https://www.iso.org/iso-639-language-codes.html.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer);
                    }

                    return;
                case "langStringValueInvalid":
                    if (elementId == null || propertyName == null || propertyValue == null || pattern == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or pattern when generating langStringValueInvalid ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LangStringValueInvalidValidationId,
                            "In {sourceName1}, property '{property}' has value '{value}'{line1}, which is invalid.",
                            "Modify the value of '{property}' to make it match the regular expression '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: pattern,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringValueInvalidValidationId,
                            "{layer}{primaryId:p} property '{property}' has value '{value}', which is invalid.",
                            "Modify the value of '{property}' to make it match the regular expression '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: pattern,
                            layer: layer);
                    }

                    return;
                case "langStringValueNotString":
                    if (elementId == null || propertyName == null || langCode == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or langCode when generating langStringValueNotString ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LangStringValueNotStringValidationId,
                            "In {sourceName1},{line1}, property '{property}' with language code '{value}' has value that is not a JSON string.",
                            "Change the value of '{property}' with language code '{value}' to a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangStringValueNotStringValidationId,
                            "{layer}{primaryId:p} property '{property}' with language code '{value}' has value that is not a JSON string.",
                            "Change the value of '{property}' with language code '{value}' to a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            value: langCode,
                            layer: layer);
                    }

                    return;
                case "langStringValueTooLong":
                    if (elementId == null || propertyName == null || propertyValue == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or expectedCount when generating langStringValueTooLong ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, property '{property}' has value '{value}'{line1}, which is too long -- length limit is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for '{property}' or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            LangStringValueTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has value '{value}', which is too long -- length limit is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for '{property}' or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            LangStringValueTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer);
                    }

                    return;
                case "langTagValueInLangMap":
                    if (elementId == null || propertyName == null || nameValuePair == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or nameValuePair when generating langTagValueInLangMap ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LangTagValueInLangMapValidationId,
                            "In {sourceName1},{line1}, property '{property}' appears to be a language-tagged value object, but a language map object is expected when the object is not in an array.",
                            "Either nest the object in an array, or replace the '@value' and '@language' properties so the object looks like {value}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: nameValuePair,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LangTagValueInLangMapValidationId,
                            "{layer}{primaryId:p} property '{property}' appears to be a language-tagged value object, but a language map object is expected when the object is not in an array.",
                            "Either nest the object in an array, or replace the '@value' and '@language' properties so the object looks like {value}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: nameValuePair,
                            layer: layer);
                    }

                    return;
                case "layerMissingMaterialType":
                    if (elementId == null || elementType == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or elementType when generating layerMissingMaterialType ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            LayerMissingMaterialTypeValidationId,
                            "In {sourceName1}, element has @type{line1} that specifies no material type.",
                            "Add a value of '{type}' to the element's @type.",
                            primaryId: elementId,
                            type: elementType,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LayerMissingMaterialTypeValidationId,
                            "{layer}{primaryId:n} has @type that specifies no material type.",
                            "Add a value of '{type}' to the element's @type.",
                            primaryId: elementId,
                            type: elementType,
                            layer: layer);
                    }

                    return;
                case "literalCountBelowMin":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating literalCountBelowMin ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} string, integer, or boolean {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            LiteralCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} string, integer, or boolean {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            LiteralCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "literalMultipleValues":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating literalMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LiteralMultipleValuesValidationId,
                            "In {sourceName1},{line1}, property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LiteralMultipleValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "literalNotValid":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating literalNotValid ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LiteralNotValidValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is not a JSON string, integer, or boolean.",
                            "Change the value of '{property}' to a JSON string, integer, or boolean.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LiteralNotValidValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is not a JSON string, integer, or boolean.",
                            "Change the value of '{property}' to a JSON string, integer, or boolean.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "literalObjectNoValue":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating literalObjectNoValue ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LiteralObjectNoValueValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a literal value to the object, or replace the object with a JSON literal.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LiteralObjectNoValueValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a literal value to the object, or replace the object with a JSON literal.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "literalTypeNotSingular":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating literalTypeNotSingular ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LiteralTypeNotSingularValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@type' does not specify a single type'.",
                            "Remove the '@type' property of '{property}' or give it a single value that is appropriate for the value type.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LiteralTypeNotSingularValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@type' does not specify a single type.",
                            "Remove the '@type' property of '{property}' or give it a single value that is appropriate for the value type.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "localContextNotLast":
                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            LocalContextNotLastValidationId,
                            "In {sourceName1}, @context array contains a local context object{line1} that is not the last element in the array.",
                            "Merge all local context definitions into a single object and locate it at the end of the @context array.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LocalContextNotLastValidationId,
                            "@context array contains a local context object that is not the last element in the array.",
                            "Merge all local context definitions into a single object and locate it at the end of the @context array.");
                    }

                    return;
                case "localTermDefinitionInvalidDtmi":
                    if (term == null || identifier == null || version == null)
                    {
                        throw new ArgumentException("Missing required parameter term or identifier or version when generating localTermDefinitionInvalidDtmi ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LocalTermDefinitionInvalidDtmiValidationId,
                            "In {sourceName1}, @context contains a local definition for term '{property}'{line1} whose value {value} starts with 'dtmi:' but is not a valid DTMI or DTMI prefix for DTDL version {restriction}.",
                            "Change the value of term '{property}' either to a URI or URI prefix with a different scheme or to a valid DTMI or DTMI prefix -- see https://github.com/Azure/digital-twin-model-identifier.",
                            property: term,
                            value: identifier,
                            restriction: version,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LocalTermDefinitionInvalidDtmiValidationId,
                            "@context contains a local definition for term '{property}' whose value {value} starts with 'dtmi:' but is not a valid DTMI or DTMI prefix for DTDL version {restriction}.",
                            "Change the value of term '{property}' either to a URI or URI prefix with a different scheme or to a valid DTMI or DTMI prefix -- see https://github.com/Azure/digital-twin-model-identifier.",
                            property: term,
                            value: identifier,
                            restriction: version);
                    }

                    return;
                case "localTermDefinitionInvalidUri":
                    if (term == null || identifier == null)
                    {
                        throw new ArgumentException("Missing required parameter term or identifier when generating localTermDefinitionInvalidUri ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LocalTermDefinitionInvalidUriValidationId,
                            "In {sourceName1}, @context contains a local definition for term '{property}'{line1} whose value {value} is not a valid URI or URI prefix.",
                            "Change the value of term '{property}' to a valid URI or URI prefix.",
                            property: term,
                            value: identifier,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LocalTermDefinitionInvalidUriValidationId,
                            "@context contains a local definition for term '{property}' whose value {value} is not a valid URI or URI prefix.",
                            "Change the value of term '{property}' to a valid URI or URI prefix.",
                            property: term,
                            value: identifier);
                    }

                    return;
                case "localTermEmpty":
                    if (term == null)
                    {
                        throw new ArgumentException("Missing required parameter term when generating localTermEmpty ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LocalTermEmptyValidationId,
                            "In {sourceName1}, @context defines a local term {line1} that is an empty string.",
                            "Use a non-empty string of characters for the term.",
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LocalTermEmptyValidationId,
                            "@context defines a local term that is an empty string.",
                            "Use a non-empty string of characters for the term.",
                            property: term);
                    }

                    return;
                case "localTermInvalid":
                    if (term == null)
                    {
                        throw new ArgumentException("Missing required parameter term when generating localTermInvalid ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LocalTermInvalidValidationId,
                            "In {sourceName1}, @context defines a local term '{property}'{line1} that contains invalid characters.",
                            "Use a different term that does not begin with '@' and that contains only letters, digits, and the characters '@', '-', '.', '_', '~', '!', '$', '&', ''', '(', ')', '*', '+', ',', ';', '='.",
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LocalTermInvalidValidationId,
                            "@context defines a local term '{property}' that contains invalid characters.",
                            "Use a different term that does not begin with '@' and that contains only letters, digits, and the characters '@', '-', '.', '_', '~', '!', '$', '&', ''', '(', ')', '*', '+', ',', ';', '='.",
                            property: term);
                    }

                    return;
                case "localTermReserved":
                    if (term == null)
                    {
                        throw new ArgumentException("Missing required parameter term when generating localTermReserved ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LocalTermReservedValidationId,
                            "In {sourceName1}, @context defines a local term '{property}'{line1} that is defined by the DTDL context.",
                            "Use a different term that is not a DTDL reserved word.",
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LocalTermReservedValidationId,
                            "@context defines a local term '{property}' that is defined by the DTDL context.",
                            "Use a different term that is not a DTDL reserved word.",
                            property: term);
                    }

                    return;
                case "localTermSchemePrefix":
                    if (term == null)
                    {
                        throw new ArgumentException("Missing required parameter term when generating localTermSchemePrefix ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocationForTerm(term, out sourceName1, out startLine1))
                    {
                        this.Add(
                            LocalTermSchemePrefixValidationId,
                            "In {sourceName1}, @context defines a local term '{property}'{line1} which is reserved as the scheme prefix for DTDL identifiers.",
                            "Use a different term other than '{property}'.",
                            property: term,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            LocalTermSchemePrefixValidationId,
                            "@context defines a local term '{property}' which is reserved as the scheme prefix for DTDL identifiers.",
                            "Use a different term other than '{property}'.",
                            property: term);
                    }

                    return;
                case "matchingPropertyNotSupplemental":
                    if (elementId == null || propertyName == null || referenceId == null || refPropertyName == null || refValue == null || nestedName == null || literalPropertyName == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or refPropertyName or refValue or nestedName or literalPropertyName or typeRestriction when generating matchingPropertyNotSupplemental ParsingError.");
                    }

                    if (refProperty != null && refProperty.TryGetSourceLocation(out sourceName1, out startLine1) && siblingElement != null && siblingElement.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            MatchingPropertyNotSupplementalValidationId,
                            "In {sourceName1}, property '{property}'{line1} refers to element{line2}{source2} whose property '{restriction}' is associated with its material type '{type}' and cannot be overridden.",
                            "Either change the value of property '{transformation}' to indicate a different property not associated with @type '{type}', or provide a value for property '{property}' that refers to a different sibling element.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            restriction: nestedName,
                            transformation: literalPropertyName,
                            type: typeRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            MatchingPropertyNotSupplementalValidationId,
                            "{primaryId:p} property '{property}' refers to sibling with {auxProperty} '{value}', but this sibling's property '{restriction}' is associated with the element's material type '{type}' and cannot be overridden.",
                            "Either change the value of property '{transformation}' to indicate a different property not associated with @type '{type}', or provide a value for property '{property}' that refers to a different sibling element.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            restriction: nestedName,
                            transformation: literalPropertyName,
                            type: typeRestriction);
                    }

                    return;
                case "maxCountNotPositive":
                    if (contextId == null || elementId == null || propertyName == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or propertyName or propertyValue when generating maxCountNotPositive ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:maxCount", out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MaxCountNotPositiveValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:maxCount' property {value}{line1} is not positive.",
                            "Remove 'sh:maxCount' property or change its value to a positive integer.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MaxCountNotPositiveValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:maxCount' property {value} is not positive.",
                            "Remove 'sh:maxCount' property or change its value to a positive integer.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue);
                    }

                    return;
                case "metamodelMultipleValues":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating metamodelMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MetamodelMultipleValuesValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has multiple 'metamodel' values{line1} but only one value is allowed.",
                            "The '@graph' property in the 'metamodel' object is permitted to have multiple values; if appropriate, restructure the metamodel definitions so any multiplicity is in '@graph' values rather than 'metamodel' values.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MetamodelMultipleValuesValidationId,
                            "DTDL language extension {primaryId} has multiple 'metamodel' values but only one value is allowed.",
                            "The '@graph' property in the 'metamodel' object is permitted to have multiple values; if appropriate, restructure the metamodel definitions so any multiplicity is in '@graph' values rather than 'metamodel' values.",
                            primaryId: contextId);
                    }

                    return;
                case "metamodelNoGraph":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating metamodelNoGraph ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MetamodelNoGraphValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has 'metamodel' object{line1} that lacks a '@graph' property.",
                            "Add a '@graph' property to 'metamodel' object with a value that is a metamodel definition.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MetamodelNoGraphValidationId,
                            "DTDL language extension {primaryId} has 'metamodel' object that lacks a '@graph' property.",
                            "Add a '@graph' property to 'metamodel' object with a value that is a metamodel definition.",
                            primaryId: contextId);
                    }

                    return;
                case "metamodelNotObject":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating metamodelNotObject ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MetamodelNotObjectValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has 'metamodel' value{line1} that is not a JSON object.",
                            "Change all values of property 'metamodel' to JSON objects, each containing a '@graph' property whose value is a metamodel definition.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MetamodelNotObjectValidationId,
                            "DTDL language extension {primaryId} has 'metamodel' value that is not a JSON object.",
                            "Change all values of property 'metamodel' to JSON objects, each containing a '@graph' property whose value is a metamodel definition.",
                            primaryId: contextId);
                    }

                    return;
                case "metamodelNoValue":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating metamodelNoValue ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MetamodelNoValueValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has 'metamodel' value{line1} that is empty.",
                            "To the values of property 'metamodel', add a JSON object containing a '@graph' property whose value is a metamodel definition.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MetamodelNoValueValidationId,
                            "DTDL language extension {primaryId} has 'metamodel' value that is empty.",
                            "To the values of property 'metamodel', add a JSON object containing a '@graph' property whose value is a metamodel definition.",
                            primaryId: contextId);
                    }

                    return;
                case "minCountExceedsMaxCount":
                    if (contextId == null || elementId == null || propertyName == null || propertyValue == null || refValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or propertyName or propertyValue or refValue when generating minCountExceedsMaxCount ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:minCount", out sourceName1, out startLine1, out endLine1) && extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:maxCount", out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            MinCountExceedsMaxCountValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:minCount' property {value}{line1} exceeds 'sh:maxCount' property {restriction}{line2}.",
                            "Remove 'sh:minCount' property or 'sh:maxCount' property, or change one or both values so that the minimum is less than or equal to the maximum.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: refValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            MinCountExceedsMaxCountValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:minCount' property {value} exceeds 'sh:maxCount' property {restriction}.",
                            "Remove 'sh:minCount' property or 'sh:maxCount' property, or change one or both values so that the minimum is less than or equal to the maximum.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: refValue);
                    }

                    return;
                case "minCountNegative":
                    if (contextId == null || elementId == null || propertyName == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or propertyName or propertyValue when generating minCountNegative ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:minCount", out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MinCountNegativeValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:minCount' property {value}{line1} is negative.",
                            "Remove 'sh:minCount' property or change its value to a non-negative integer.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MinCountNegativeValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:minCount' property {value} is negative.",
                            "Remove 'sh:minCount' property or change its value to a non-negative integer.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue);
                    }

                    return;
                case "mismatchedLayers":
                    if (elementId == null || propertyName == null || layer == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or layer when generating mismatchedLayers ParsingError.");
                    }
                    {
                        this.Add(
                            MismatchedLayersValidationId,
                            "{layer}{primaryId:p} property '{property}' adds context that introduces nested layer '{value}'.",
                            "Relocate the layering context to the top-level definition.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "missingCocotype":
                    if (elementId == null || elementType == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or elementType or cotype when generating missingCocotype ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            MissingCocotypeValidationId,
                            "In {sourceName1}, element has @type{line1} with value {type} that can only be co-typed on elements that are also co-typed with {restriction}.",
                            "Remove @type {type} from element, or add @type {restriction} to element.",
                            primaryId: elementId,
                            type: elementType,
                            restriction: cotype,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingCocotypeValidationId,
                            "{primaryId:n} has @type with value {type} that can only be co-typed on elements that are also co-typed with {restriction}.",
                            "Remove @type {type} from element, or add @type {restriction} to element.",
                            primaryId: elementId,
                            type: elementType,
                            restriction: cotype);
                    }

                    return;
                case "missingContext":
                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingContextValidationId,
                            "In {sourceName1}, top-level JSON object{line1} has no @context specifier.",
                            "Add a '@context' property whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingContextValidationId,
                            "Top-level JSON object has no @context specifier.",
                            "Add a '@context' property whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.");
                    }

                    return;
                case "missingContextVersion":
                    if (contextValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue when generating missingContextVersion ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingContextVersionValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1}, which is invalid because it lacks a version number.",
                            "Modify @context specifier so that it ends with a ';' followed by a version number.",
                            value: contextValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingContextVersionValidationId,
                            "@context specifier has value '{value}', which is invalid because it lacks a version number.",
                            "Modify @context specifier so that it ends with a ';' followed by a version number.",
                            value: contextValue);
                    }

                    return;
                case "missingDictKeyPropertyValue":
                    if (elementId == null || propertyName == null || referenceId == null || nestedName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or nestedName when generating missingDictKeyPropertyValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && element != null && element.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            MissingDictKeyPropertyValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e} whose definition{line2}{source2} is missing required property '{value}'.",
                            "Add '{value}' property with a string value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: nestedName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            MissingDictKeyPropertyValueValidationId,
                            "{primaryId:p} property '{property}' has value{secondaryId:e} that requires property '{value}' to be specified but it is not.",
                            "Add '{value}' property with a string value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: nestedName);
                    }

                    return;
                case "missingDtdlContext":
                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingDtdlContextValidationId,
                            "In {sourceName1}, @context specifier in top-level JSON object has value{line1} that does not have a DTDL context specifier as its first or only value.",
                            "Set the value of the '@context' property either to a string or to an array whose first value is a string, wherein the string is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingDtdlContextValidationId,
                            "@context specifier in top-level JSON object has value that does not have a DTDL context specifier as its first or only value.",
                            "Set the value of the '@context' property either to a string or to an array whose first value is a string, wherein the string is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.");
                    }

                    return;
                case "missingDtmiSegPropertyValue":
                    if (elementId == null || propertyName == null || nestedName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or nestedName when generating missingDtmiSegPropertyValue ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingDtmiSegPropertyValueValidationId,
                            "In {sourceName1}, element{line1} requires property '{value}' to be specified but it is not.",
                            "Add '{value}' property with a value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: nestedName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingDtmiSegPropertyValueValidationId,
                            "{layer}{primaryId:p} property '{property}' requires property '{value}' to be specified but it is not.",
                            "Add '{value}' property with a value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: nestedName,
                            layer: layer);
                    }

                    return;
                case "missingEssentialTypes":
                    if (contextId == null || elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId when generating missingEssentialTypes ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            MissingEssentialTypesValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId:n} has '@type' property{line1} that lacks value(s) 'rdfs:Class' and/or 'sh:NodeShape'.",
                            "Update '@type' property to include at least the values 'rdfs:Class' and 'sh:NodeShape'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingEssentialTypesValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:n} has '@type' property that lacks value(s) 'rdfs:Class' and/or 'sh:NodeShape'.",
                            "Update '@type' property to include at least the values 'rdfs:Class' and 'sh:NodeShape'.",
                            primaryId: contextId,
                            secondaryId: elementId);
                    }

                    return;
                case "missingExtensionContext":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating missingExtensionContext ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingExtensionContextValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId}{line1} has no @context specifier.",
                            "Add a '@context' property whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingExtensionContextValidationId,
                            "DTDL language extension {primaryId} has no @context specifier.",
                            "Add a '@context' property whose value is a valid DTDL context specifier, such as 'dtmi:dtdl:context;2'.",
                            primaryId: contextId);
                    }

                    return;
                case "missingExtensionId":
                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingExtensionIdValidationId,
                            "In {sourceName1}, DTDL language extension{line1} is missing '@id' property.",
                            "Add a JSON property '@id' whose value is a valid DTMI.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingExtensionIdValidationId,
                            "DTDL language extension is missing '@id' property.",
                            "Add a JSON property '@id' whose value is a valid DTMI.");
                    }

                    return;
                case "missingExtensionType":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating missingExtensionType ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingExtensionTypeValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId}{line1} has no @type specifier.",
                            "Add a '@type' property with a string value of 'DtdlExtension'.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingExtensionTypeValidationId,
                            "DTDL language extension {primaryId} has no @type specifier.",
                            "Add a '@type' property with a string value of 'DtdlExtension'.",
                            primaryId: contextId);
                    }

                    return;
                case "missingExtensionVersion":
                    if (identifier == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier when generating missingExtensionVersion ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            MissingExtensionVersionValidationId,
                            "In {sourceName1}, DTDL language extension has '@id' property{line1} with value '{value}' that is invalid because it lacks a version number.",
                            "Modify the identifier so that it ends with a ';' followed by a version number.",
                            value: identifier,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingExtensionVersionValidationId,
                            "DTDL language extension has '@id' property with value '{value}' that is invalid because it lacks a version number.",
                            "Modify the identifier so that it ends with a ';' followed by a version number.",
                            value: identifier);
                    }

                    return;
                case "missingIdentifierProperty":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating missingIdentifierProperty ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingIdentifierPropertyValidationId,
                            "In {sourceName1}, element{line1} requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element.",
                            primaryId: elementId,
                            property: propertyName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingIdentifierPropertyValidationId,
                            "{primaryId:n} requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element.",
                            primaryId: elementId,
                            property: propertyName);
                    }

                    return;
                case "missingLiteralProperty":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating missingLiteralProperty ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingLiteralPropertyValidationId,
                            "In {sourceName1}, element{line1} requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element.",
                            primaryId: elementId,
                            property: propertyName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingLiteralPropertyValidationId,
                            "{primaryId:n} requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element.",
                            primaryId: elementId,
                            property: propertyName);
                    }

                    return;
                case "missingObjectProperty":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating missingObjectProperty ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingObjectPropertyValidationId,
                            "In {sourceName1}, element{line1} requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element.",
                            primaryId: elementId,
                            property: propertyName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingObjectPropertyValidationId,
                            "{primaryId:n} requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element.",
                            primaryId: elementId,
                            property: propertyName);
                    }

                    return;
                case "missingRequiredId":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating missingRequiredId ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingRequiredIdValidationId,
                            "In {sourceName1}, property '{property}' element{line1} has no '@id' property.",
                            "Add an '@id' property whose value is a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingRequiredIdValidationId,
                            "{layer}{primaryId:p} property '{property}' element has no '@id' property.",
                            "Add an '@id' property whose value is a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "missingSubClassOf":
                    if (contextId == null || elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId when generating missingSubClassOf ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingSubClassOfValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} property 'rdfs:subClassOf'{line1} is missing or not a string.",
                            "Add or modify 'rdfs:subClassOf' property so that it has a JSON string value that indicates an extensible DTDL type or a subclass thereof.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingSubClassOfValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} property 'rdfs:subClassOf' is missing or not a string.",
                            "Add or modify 'rdfs:subClassOf' property so that it has a JSON string value that indicates an extensible DTDL type or a subclass thereof.",
                            primaryId: contextId,
                            secondaryId: elementId);
                    }

                    return;
                case "missingSupplementalIdentifierProperty":
                    if (elementId == null || propertyName == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or cotype when generating missingSupplementalIdentifierProperty ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingSupplementalIdentifierPropertyValidationId,
                            "In {sourceName1}, element{line1} has co-type {type}, which requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element, or remove co-type {type} from the element.",
                            primaryId: elementId,
                            property: propertyName,
                            type: cotype,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingSupplementalIdentifierPropertyValidationId,
                            "{primaryId:n} has co-type {type}, which requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element, or remove co-type {type} from the element.",
                            primaryId: elementId,
                            property: propertyName,
                            type: cotype);
                    }

                    return;
                case "missingSupplementalLiteralProperty":
                    if (elementId == null || propertyName == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or cotype when generating missingSupplementalLiteralProperty ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingSupplementalLiteralPropertyValidationId,
                            "In {sourceName1}, element{line1} has co-type {type}, which requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element, or remove co-type {type} from the element.",
                            primaryId: elementId,
                            property: propertyName,
                            type: cotype,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingSupplementalLiteralPropertyValidationId,
                            "{primaryId:n} has co-type {type}, which requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element, or remove co-type {type} from the element.",
                            primaryId: elementId,
                            property: propertyName,
                            type: cotype);
                    }

                    return;
                case "missingSupplementalObjectProperty":
                    if (elementId == null || propertyName == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or cotype when generating missingSupplementalObjectProperty ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingSupplementalObjectPropertyValidationId,
                            "In {sourceName1}, element{line1} has co-type {type}, which requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element, or remove co-type {type} from the element.",
                            primaryId: elementId,
                            property: propertyName,
                            type: cotype,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingSupplementalObjectPropertyValidationId,
                            "{primaryId:n} has co-type {type}, which requires property '{property}'; however, this property is not present.",
                            "Add a property '{property}' to the element, or remove co-type {type} from the element.",
                            primaryId: elementId,
                            property: propertyName,
                            type: cotype);
                    }

                    return;
                case "missingTopLevelId":
                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingTopLevelIdValidationId,
                            "In {sourceName1}, top-level element{line1} has no '@id' property.",
                            "Add an '@id' property whose value is a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingTopLevelIdValidationId,
                            "{layer}Top-level element has no '@id' property.",
                            "Add an '@id' property whose value is a string that conforms to the DTMI syntax -- see https://github.com/Azure/digital-twin-model-identifier.",
                            layer: layer);
                    }

                    return;
                case "missingTypeInfo":
                    if (contextId == null || elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId when generating missingTypeInfo ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            MissingTypeInfoValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:n} is missing '@type' property.",
                            "Add a '@type' property whose value is a JSON array that includes at least the values 'rdfs:Class' and 'sh:NodeShape'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MissingTypeInfoValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:n} is missing '@type' property.",
                            "Add a '@type' property whose value is a JSON array that includes at least the values 'rdfs:Class' and 'sh:NodeShape'.",
                            primaryId: contextId,
                            secondaryId: elementId);
                    }

                    return;
                case "modelMultipleValues":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating modelMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ModelMultipleValuesValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has multiple 'model' values{line1} but only one value is allowed.",
                            "The '@graph' property in the 'model' object is permitted to have multiple values; if appropriate, restructure the model definitions so any multiplicity is in '@graph' values rather than 'model' values.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ModelMultipleValuesValidationId,
                            "DTDL language extension {primaryId} has multiple 'model' values but only one value is allowed.",
                            "The '@graph' property in the 'model' object is permitted to have multiple values; if appropriate, restructure the model definitions so any multiplicity is in '@graph' values rather than 'model' values.",
                            primaryId: contextId);
                    }

                    return;
                case "modelNoGraph":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating modelNoGraph ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ModelNoGraphValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has 'model' object{line1} that lacks a '@graph' property.",
                            "Add a '@graph' property to 'model' object with a value that is a model definition.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ModelNoGraphValidationId,
                            "DTDL language extension {primaryId} has 'model' object that lacks a '@graph' property.",
                            "Add a '@graph' property to 'model' object with a value that is a model definition.",
                            primaryId: contextId);
                    }

                    return;
                case "modelNotObject":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating modelNotObject ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ModelNotObjectValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has 'model' value{line1} that is not a JSON object.",
                            "Change all values of property 'model' to JSON objects, each containing a '@graph' property whose value is a model definition.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ModelNotObjectValidationId,
                            "DTDL language extension {primaryId} has 'model' value that is not a JSON object.",
                            "Change all values of property 'model' to JSON objects, each containing a '@graph' property whose value is a model definition.",
                            primaryId: contextId);
                    }

                    return;
                case "modelNoValue":
                    if (contextId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId when generating modelNoValue ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ModelNoValueValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} has 'model' value{line1} that is empty.",
                            "To the values of property 'model', add a JSON object containing a '@graph' property whose value is a model definition.",
                            primaryId: contextId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ModelNoValueValidationId,
                            "DTDL language extension {primaryId} has 'model' value that is empty.",
                            "To the values of property 'model', add a JSON object containing a '@graph' property whose value is a model definition.",
                            primaryId: contextId);
                    }

                    return;
                case "multipleMaterialTypes":
                    if (elementId == null || valueConjunction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or valueConjunction when generating multipleMaterialTypes ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            MultipleMaterialTypesValidationId,
                            "In {sourceName1}, element has @type{line1} that specifies multiple material types: {value}.",
                            "Remove excess @type values so that only one material type remains.",
                            primaryId: elementId,
                            value: valueConjunction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            MultipleMaterialTypesValidationId,
                            "{layer}{primaryId:n} has @type that specifies multiple material types: {value}.",
                            "Remove excess @type values so that only one material type remains.",
                            primaryId: elementId,
                            value: valueConjunction,
                            layer: layer);
                    }

                    return;
                case "multipleReference":
                    if (elementId == null || propertyName == null || referenceId == null || propertyConjunction == null || valueConjunction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or propertyConjunction or valueConjunction when generating multipleReference ParsingError.");
                    }

                    if (siblingElement != null && siblingElement.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && element != null && element.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            MultipleReferenceValidationId,
                            "The two elements{line1}{source1} and{line2} in {sourceName2} both have properties {auxProperty} with respective values {value}.",
                            "Remove or modify one or both elements so that the combination of {auxProperty} values is unique across all '{property}' elements.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: propertyConjunction,
                            value: valueConjunction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            MultipleReferenceValidationId,
                            "{primaryId:p} has more than one '{property}' value in which properties {auxProperty} have respective values {value}.",
                            "Remove or modify one or both elements so that the combination of {auxProperty} values is unique across all '{property}' elements.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: propertyConjunction,
                            value: valueConjunction);
                    }

                    return;
                case "noMatchingSupplementalProperty":
                    if (elementId == null || propertyName == null || referenceId == null || refPropertyName == null || refValue == null || nestedName == null || literalPropertyName == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or refPropertyName or refValue or nestedName or literalPropertyName or typeRestriction when generating noMatchingSupplementalProperty ParsingError.");
                    }

                    if (refProperty != null && refProperty.TryGetSourceLocation(out sourceName1, out startLine1) && siblingElement != null && siblingElement.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            NoMatchingSupplementalPropertyValidationId,
                            "In {sourceName1}, property '{property}'{line1} refers to element{line2}{source2} that has no supplemental @type with property '{restriction}'.",
                            "Either add a supplemental @type that has property '{restriction}' to the currently referenced element, or change the value of property '{transformation}' to indicate a different property on the currently referenced element, or provide a value for property '{property}' that refers to a different sibling element.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            restriction: nestedName,
                            transformation: literalPropertyName,
                            type: typeRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            NoMatchingSupplementalPropertyValidationId,
                            "{primaryId:p} property '{property}' refers to sibling with {auxProperty} '{value}', but this sibling has no supplemental @type with property '{restriction}'.",
                            "Either add a supplemental @type that has property '{restriction}' to the currently referenced element, or change the value of property '{transformation}' to indicate a different property on the currently referenced element, or provide a value for property '{property}' that refers to a different sibling element.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            restriction: nestedName,
                            transformation: literalPropertyName,
                            type: typeRestriction);
                    }

                    return;
                case "nonConformantDatatype":
                    if (elementId == null || propertyName == null || governingId == null || governingPropertyName == null || propertyValue == null || datatype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or governingId or governingPropertyName or propertyValue or datatype when generating nonConformantDatatype ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && governingProperty != null && governingProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            NonConformantDatatypeValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value '{value}', which does not have datatype {type} as required by property '{restriction}'{line2}{source2}.",
                            "Change the value of property '{property}' to a value whose datatype is {type}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: governingId,
                            restriction: governingPropertyName,
                            value: propertyValue,
                            type: datatype,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            NonConformantDatatypeValidationId,
                            "{primaryId:p} property '{property}' has value '{value}'; however, {secondaryId:n} specifies that the datatype of all descendant '{property}' properties must be {type}.",
                            "Change the value of property '{property}' to a value whose datatype is {type}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: governingId,
                            restriction: governingPropertyName,
                            value: propertyValue,
                            type: datatype);
                    }

                    return;
                case "nonConformantPropertyValue":
                    if (elementId == null || propertyName == null || governingPropertyName == null || violations == null || violationCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or governingPropertyName or violations or violationCount when generating nonConformantPropertyValue ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && governingValues != null && governingValues.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, '{property}' value{line1} does not conform to '{restriction}' value{line2}{source2} due to {count1} {item1}: {firstViolation}.");
                        SetCount(causeBuilder, 1, (int)violationCount, "violation", "violations including");

                        this.Add(
                            NonConformantPropertyValueValidationId,
                            causeBuilder.ToString(),
                            "Change the value of property '{property}' so that it conforms to '{restriction}' value.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: governingPropertyName,
                            violations: violations,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{primaryId:p} property '{property}' has value that does not conform to '{restriction}' value due to {count1} {item1}: {firstViolation}.");
                        SetCount(causeBuilder, 1, (int)violationCount, "violation", "violations including");

                        this.Add(
                            NonConformantPropertyValueValidationId,
                            causeBuilder.ToString(),
                            "Change the value of property '{property}' so that it conforms to '{restriction}' value.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: governingPropertyName,
                            violations: violations);
                    }

                    return;
                case "nonDtmiContextSpecifier":
                    if (contextValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue when generating nonDtmiContextSpecifier ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            NonDtmiContextSpecifierValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1}, which is not a DTMI.",
                            "Remove '{value}' @context specifier{line1}.",
                            value: contextValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NonDtmiContextSpecifierValidationId,
                            "@context specifier has value '{value}', which is not a DTMI.",
                            "Remove '{value}' @context specifier.",
                            value: contextValue);
                    }

                    return;
                case "nonUniqueImportedPropertyValue":
                    if (elementId == null || referenceId == null || propertyName == null || refPropertyName == null || nestedName == null || nestedValue == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or referenceId or propertyName or refPropertyName or nestedName or nestedValue when generating nonUniqueImportedPropertyValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            NonUniqueImportedPropertyValueValidationId,
                            "Property '{auxProperty}' has value '{value}'{line1}{source1} and{line2} in {sourceName2}, which is a uniqueness violation because {primaryId} transitively {transformation} {secondaryId}.",
                            "Either change the value of property '{auxProperty}' to a value that is unique across all values of '{property}', or remove one or more {transformation} properties so that '{property}' will not be imported.",
                            primaryId: elementId,
                            secondaryId: referenceId,
                            property: propertyName,
                            transformation: refPropertyName,
                            auxProperty: nestedName,
                            value: nestedValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            NonUniqueImportedPropertyValueValidationId,
                            "{primaryId:n}, because it transitively {transformation} {secondaryId}, has property '{property}' that contains more than one element for which property '{auxProperty}' has value '{value}'.",
                            "Either change the value of property '{auxProperty}' to a value that is unique across all values of '{property}', or remove one or more {transformation} properties so that '{property}' will not be imported.",
                            primaryId: elementId,
                            secondaryId: referenceId,
                            property: propertyName,
                            transformation: refPropertyName,
                            auxProperty: nestedName,
                            value: nestedValue);
                    }

                    return;
                case "nonUniquePropertyValue":
                    if (elementId == null || propertyName == null || nestedName == null || nestedValue == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or nestedName or nestedValue when generating nonUniquePropertyValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && extantProperty != null && extantProperty.TryGetSourceLocation(out sourceName2, out startLine2))
                    {
                        this.Add(
                            NonUniquePropertyValueValidationId,
                            "In {sourceName2},{line2}, property '{auxProperty}' has value '{value}' which is already used{line1}{source1}.",
                            "Change the value of property '{auxProperty}' to a value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            auxProperty: nestedName,
                            value: nestedValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            NonUniquePropertyValueValidationId,
                            "{primaryId:p} property '{property}' contains more than one element whose property '{auxProperty}' has value '{value}'.",
                            "Change the value of property '{auxProperty}' to a value that is unique across all values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            auxProperty: nestedName,
                            value: nestedValue);
                    }

                    return;
                case "notJsonObject":
                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            NotJsonObjectValidationId,
                            "In {sourceName1}, top-level JSON element{line1} is neither a JSON object nor a JSON array of JSON objects.",
                            "Update your model to follow the examples in aka.ms/dtdl.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotJsonObjectValidationId,
                            "Top-level JSON element is neither a JSON object nor a JSON array of JSON objects.",
                            "Update your model to follow the examples in aka.ms/dtdl.");
                    }

                    return;
                case "notRequiredBooleanValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredBooleanValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredBooleanValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have boolean value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredBooleanValueValidationId,
                            "{primaryId:p} property '{property}' does not have boolean value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredIdentifierValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredIdentifierValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredIdentifierValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have identifier value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredIdentifierValueValidationId,
                            "{primaryId:p} property '{property}' does not have identifier value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredIntegerValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredIntegerValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredIntegerValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have integer value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredIntegerValueValidationId,
                            "{primaryId:p} property '{property}' does not have integer value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredJsonValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredJsonValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredJsonValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have JSON value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredJsonValueValidationId,
                            "{primaryId:p} property '{property}' does not have JSON value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredLangStringValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredLangStringValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredLangStringValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have language-map value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredLangStringValueValidationId,
                            "{primaryId:p} property '{property}' does not have language-map value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredLiteralValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredLiteralValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredLiteralValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have literal value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredLiteralValueValidationId,
                            "{primaryId:p} property '{property}' does not have literal value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredNumericValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredNumericValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredNumericValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have numeric value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredNumericValueValidationId,
                            "{primaryId:p} property '{property}' does not have numeric value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredStringValue":
                    if (elementId == null || propertyName == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or valueRestriction when generating notRequiredStringValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredStringValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} does not have string value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredStringValueValidationId,
                            "{primaryId:p} property '{property}' does not have string value {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            restriction: valueRestriction,
                            layer: layer);
                    }

                    return;
                case "notRequiredType":
                    if (elementId == null || propertyName == null || referenceId == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or typeRestriction when generating notRequiredType ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredTypeValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e} that does not have @type of {type}.",
                            "Provide a value for property '{property}' that has a @type of {type} or a subtype thereof.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            type: typeRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredTypeValidationId,
                            "{primaryId:p} property '{property}' has value{secondaryId:e} that does not have @type of {type}.",
                            "Provide a value for property '{property}' that has a @type of {type} or a subtype thereof.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            type: typeRestriction);
                    }

                    return;
                case "notRequiredValue":
                    if (elementId == null || propertyName == null || referenceId == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or valueRestriction when generating notRequiredValue ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            NotRequiredValueValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e} that is not {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            restriction: valueRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NotRequiredValueValidationId,
                            "{primaryId:p} property '{property}' has value{secondaryId:e} that is not {restriction}.",
                            "Change the value of property '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            restriction: valueRestriction);
                    }

                    return;
                case "notSiblingRequiredType":
                    if (elementId == null || propertyName == null || referenceId == null || refPropertyName == null || refValue == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or refPropertyName or refValue or typeRestriction when generating notSiblingRequiredType ParsingError.");
                    }

                    if (refProperty != null && refProperty.TryGetSourceLocation(out sourceName1, out startLine1) && siblingElement != null && siblingElement.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            NotSiblingRequiredTypeValidationId,
                            "In {sourceName1}, property '{property}'{line1} refers to element{line2}{source2} that does not have @type of {type}.",
                            "Provide a value for property '{property}' that refers to a sibling that has a @type of {type} or a subtype thereof.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            type: typeRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            NotSiblingRequiredTypeValidationId,
                            "{primaryId:p} property '{property}' refers to sibling with {auxProperty} '{value}', but this sibling does not have @type of {type}.",
                            "Provide a value for property '{property}' that refers to a sibling that has a @type of {type} or a subtype thereof.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            auxProperty: refPropertyName,
                            value: refValue,
                            type: typeRestriction);
                    }

                    return;
                case "noTypeThatAllows":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating noTypeThatAllows ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && element != null && element.TryGetSourceLocationForType(out sourceName2, out startLine2))
                    {
                        this.Add(
                            NoTypeThatAllowsValidationId,
                            "In {sourceName1}, no type specified in @type{line2} allows property '{property}'{line1}.",
                            "Remove property '{property}' or correct if misspelled.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            NoTypeThatAllowsValidationId,
                            "{layer}{primaryId:n} does not have a @type that allows property '{property}'.",
                            "Remove property '{property}' or correct if misspelled.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "numericCountBelowMin":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating numericCountBelowMin ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} numeric {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            NumericCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} numeric {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            NumericCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "numericMultipleValues":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating numericMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            NumericMultipleValuesValidationId,
                            "In {sourceName1},{line1}, property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NumericMultipleValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "numericNotNumeric":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating numericNotNumeric ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            NumericNotNumericValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is not a JSON number.",
                            "Change the value of '{property}' to a JSON number.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NumericNotNumericValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is not a JSON number.",
                            "Change the value of '{property}' to a JSON number.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "numericObjectNoValue":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating numericObjectNoValue ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            NumericObjectNoValueValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a numeric value to the object, or replace the object with a JSON number.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NumericObjectNoValueValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a numeric value to the object, or replace the object with a JSON number.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "numericTypeNotNumeric":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating numericTypeNotNumeric ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            NumericTypeNotNumericValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:decimal'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:decimal'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NumericTypeNotNumericValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:decimal'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:decimal'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "numericValueNotNumeric":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating numericValueNotNumeric ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            NumericValueNotNumericValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@value' is not a JSON number.",
                            "Change the value of the '@value' property of '{property}' to a JSON number.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            NumericValueNotNumericValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@value' is not a JSON number.",
                            "Change the value of the '@value' property of '{property}' to a JSON number.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "objectCountAboveMax":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating objectCountAboveMax ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}{line1}, property '{property}' has {count1} {item1} but no more than {count2} {item2} {verb2} allowed.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            ObjectCountAboveMaxValidationId,
                            causeBuilder.ToString(),
                            "Remove one or more '{property}' property values from the object until the maximum count is not exceeded.",
                            primaryId: elementId,
                            property: propertyName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{primaryId:p} property '{property}' has {count1} {item1} but no more than {count2} {item2} {verb2} allowed.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            ObjectCountAboveMaxValidationId,
                            causeBuilder.ToString(),
                            "Remove one or more '{property}' property values from the object until the maximum count is not exceeded.",
                            primaryId: elementId,
                            property: propertyName);
                    }

                    return;
                case "objectCountBelowMin":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating objectCountBelowMin ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has {count1} valid {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} {item2} for '{property}', each of which is an element definition, a DTMI reference, or DTDL term.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            ObjectCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has {count1} valid {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} {item2} for '{property}', each of which is an element definition, a DTMI reference, or DTDL term.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            ObjectCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "objectMultipleValues":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating objectMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ObjectMultipleValuesValidationId,
                            "In {sourceName1},{line1}, property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ObjectMultipleValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "partitionTooLarge":
                    if (elementId == null || partition == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or partition or observedCount or expectedCount when generating partitionTooLarge ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1}, JSON text of element{line1} is {count1} {item1} in length, but the largest permissible size is {count2} {item2}.");
                        SetCount(causeBuilder, 1, (int)observedCount, "byte", "bytes");
                        SetCount(causeBuilder, 2, (int)expectedCount, "byte", "bytes");

                        StringBuilder actionBuilder = new StringBuilder("Refactor model so that the size of each {value} is no greater than {count2} {item2}.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "byte", "bytes");

                        this.Add(
                            PartitionTooLargeValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            value: partition,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("JSON text of {primaryId} is {count1} {item1} in length, but the largest permissible size is {count2} {item2}.");
                        SetCount(causeBuilder, 1, (int)observedCount, "byte", "bytes");
                        SetCount(causeBuilder, 2, (int)expectedCount, "byte", "bytes");

                        StringBuilder actionBuilder = new StringBuilder("Refactor model so that the size of each {value} is no greater than {count2} {item2}.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "byte", "bytes");

                        this.Add(
                            PartitionTooLargeValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            value: partition);
                    }

                    return;
                case "pathConstraintNoValue":
                    if (contextId == null || typeId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or typeId when generating pathConstraintNoValue ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:path", out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            PathConstraintNoValueValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value whose 'sh:path' property{line1} has no value.",
                            "Add a JSON string value to the 'sh:path' property that indicates a DTDL property name.",
                            primaryId: contextId,
                            secondaryId: typeId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PathConstraintNoValueValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value whose 'sh:path' property has no value.",
                            "Add a JSON string value to the 'sh:path' property that indicates a DTDL property name.",
                            primaryId: contextId,
                            secondaryId: typeId);
                    }

                    return;
                case "pathNotRecognized":
                    if (contextId == null || typeId == null || constraintValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or typeId or constraintValue when generating pathNotRecognized ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:path", out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            PathNotRecognizedValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value whose 'sh:path' property '{value}'{line1} is not a recognized DTDL property name.",
                            "Change the value of 'sh:path' property to a JSON string that indicates a DTDL property name.",
                            primaryId: contextId,
                            secondaryId: typeId,
                            value: constraintValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PathNotRecognizedValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value whose 'sh:path' property '{value}' is not a recognized DTDL property name.",
                            "Change the value of 'sh:path' property to a JSON string that indicates a DTDL property name.",
                            primaryId: contextId,
                            secondaryId: typeId,
                            value: constraintValue);
                    }

                    return;
                case "pathNotString":
                    if (contextId == null || elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId when generating pathNotString ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            PathNotStringValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property 'sh:property' has value that is missing 'sh:path' property or value is not a string.",
                            "Add a 'sh:path' property with a JSON string value that indicates a DTDL property name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PathNotStringValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property 'sh:property' has value that is missing 'sh:path' property or value is not a string.",
                            "Add a 'sh:path' property with a JSON string value that indicates a DTDL property name.",
                            primaryId: contextId,
                            secondaryId: elementId);
                    }

                    return;
                case "pathNotUnique":
                    if (contextId == null || elementId == null || constraintValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintValue when generating pathNotUnique ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1) && extantValue != null && extantValue.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            PathNotUniqueValidationId,
                            "In {sourceName}, DTDL language extension {primaryId} metamodel element {secondaryId:n} has 'sh:property' value with 'sh:path' value '{value}'{line1} but this value is already used{line2}.",
                            "Remove or combine elements in the 'sh:property' collection so that only one element identifies 'sh:path' value '{value}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            value: constraintValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            PathNotUniqueValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:n} has 'sh:property' with multiple values that duplicate the value '{value}' for the 'sh:path' property.",
                            "Remove or combine elements in the 'sh:property' collection so that only one element identifies 'sh:path' value '{value}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            value: constraintValue);
                    }

                    return;
                case "patternInvalid":
                    if (contextId == null || elementId == null || propertyName == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or propertyName or propertyValue when generating patternInvalid ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:pattern", out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            PatternInvalidValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:pattern' property '{value}'{line1} is not a valid regular expression.",
                            "Remove 'sh:pattern' property or change its value to a JSON string that specifies a valid regular expression.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PatternInvalidValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:pattern' property '{value}' is not a valid regular expression.",
                            "Remove 'sh:pattern' property or change its value to a JSON string that specifies a valid regular expression.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue);
                    }

                    return;
                case "propertyClassNotRecognized":
                    if (contextId == null || elementId == null || propertyName == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or propertyName or propertyValue when generating propertyClassNotRecognized ParsingError.");
                    }

                    if (extensionProperty != null && extensionProperty.TryGetSourceLocationForConstraint("sh:class", out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            PropertyClassNotRecognizedValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:class' property '{value}'{line1} is not a recognized DTDL type name.",
                            "Change the value of 'sh:class' property to a JSON string that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PropertyClassNotRecognizedValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'sh:property' value for path '{property}' whose 'sh:class' property '{value}' is not a recognized DTDL type name.",
                            "Change the value of 'sh:class' property to a JSON string that indicates a DTDL type name.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: propertyName,
                            value: propertyValue);
                    }

                    return;
                case "propertyElementNotObject":
                    if (contextId == null || elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId when generating propertyElementNotObject ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            PropertyElementNotObjectValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property 'sh:property' has value that is not a JSON object.",
                            "Change the value of 'sh:property' to a JSON array of JSON objects that each defines a property of the extension type.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PropertyElementNotObjectValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property 'sh:property' has value that is not a JSON object.",
                            "Change the value of 'sh:property' to a JSON array of JSON objects that each defines a property of the extension type.",
                            primaryId: contextId,
                            secondaryId: elementId);
                    }

                    return;
                case "propertyInvalidDtmi":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating propertyInvalidDtmi ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            PropertyInvalidDtmiValidationId,
                            "In {sourceName1}, property '{property}'{line1} is an invalid DTMI.",
                            "Replace property '{property}' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PropertyInvalidDtmiValidationId,
                            "{layer}{primaryId:p} property '{property}' is an invalid DTMI.",
                            "Replace property '{property}' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "propertyIrrelevantDtmiOrTerm":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating propertyIrrelevantDtmiOrTerm ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && element != null && element.TryGetSourceLocationForType(out sourceName2, out startLine2))
                    {
                        this.Add(
                            PropertyIrrelevantDtmiOrTermValidationId,
                            "In {sourceName1}, property '{property}'{line1} is not a relevant property for any type specified in @type{line2}.",
                            "Remove property '{property}' or correct if misspelled.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            PropertyIrrelevantDtmiOrTermValidationId,
                            "{layer}{primaryId:p} property '{property}' is not a relevant property for any specified @type on the element.",
                            "Remove property '{property}' or correct if misspelled.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "propertyNotDtmiNorTerm":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating propertyNotDtmiNorTerm ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            PropertyNotDtmiNorTermValidationId,
                            "In {sourceName1}, property '{property}'{line1} is an IRI that is not a DTMI.",
                            "Replace property '{property}' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PropertyNotDtmiNorTermValidationId,
                            "{layer}{primaryId:p} property '{property}' is an IRI that is not a DTMI.",
                            "Replace property '{property}' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "propertyUndefinedTerm":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating propertyUndefinedTerm ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            PropertyUndefinedTermValidationId,
                            "In {sourceName1}, property '{property}'{line1} is an undefined term.",
                            "Replace property '{property}' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            PropertyUndefinedTermValidationId,
                            "{layer}{primaryId:p} property '{property}' is an undefined term.",
                            "Replace property '{property}' with a string that is either a defined term or a valid DTMI -- see https://github.com/Azure/digital-twin-model-identifier.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "recursiveStructureNarrow":
                    if (elementId == null || propertyDisjunction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyDisjunction when generating recursiveStructureNarrow ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            RecursiveStructureNarrowValidationId,
                            "In {sourceName1}, element{line1} is at the root of a chain of {property} properties that includes itself.",
                            "Change the value of one or more {property} properties in the hierarchy to remeve the recursion.",
                            primaryId: elementId,
                            property: propertyDisjunction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            RecursiveStructureNarrowValidationId,
                            "{primaryId:n} is at the root of a chain of {property} properties that includes itself.",
                            "Change the value of one or more {property} properties in the hierarchy to remeve the recursion.",
                            primaryId: elementId,
                            property: propertyDisjunction);
                    }

                    return;
                case "recursiveStructureWide":
                    if (elementId == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId when generating recursiveStructureWide ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            RecursiveStructureWideValidationId,
                            "In {sourceName1}, element{line1} is at the root of a hierarchy that includes itself.",
                            "Change the value of one or more properties of elements in the hierarchy to remeve the recursion.",
                            primaryId: elementId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            RecursiveStructureWideValidationId,
                            "{primaryId:n} is at the root of a hierarchy that includes itself.",
                            "Change the value of one or more properties of elements in the hierarchy to remeve the recursion.",
                            primaryId: elementId);
                    }

                    return;
                case "refNotStringOrObject":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating refNotStringOrObject ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            RefNotStringOrObjectValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value{line2} that is neither a JSON object nor a JSON string representing a valid DTMI reference or DTDL term.",
                            "Replace the value of property '{property}' with either a DTDL element defined in a JSON object or a DTMI reference or term represented in a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            RefNotStringOrObjectValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is neither a JSON object nor a JSON string representing a valid DTMI reference or DTDL term.",
                            "Replace the value of property '{property}' with either a DTDL element defined in a JSON object or a DTMI reference or term represented in a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "refObjectNotAllowed":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating refObjectNotAllowed ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            RefObjectNotAllowedValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value{line2} that is a JSON object, but value must be a JSON string representing a valid DTMI reference or DTDL term.",
                            "Replace the value of property '{property}' with a DTMI reference or term represented in a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            RefObjectNotAllowedValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object, but value must be a JSON string representing a valid DTMI reference or DTDL term.",
                            "Replace the value of property '{property}' with a DTMI reference or term represented in a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "reservedId":
                    if (identifier == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier or valueRestriction when generating reservedId ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            ReservedIdValidationId,
                            "In {sourceName1}, identifier '{value}'{line1} begins with a reserved prefix.",
                            "Modify the identifier so that it does not begin with any of these reserved prefixes: {restriction}",
                            value: identifier,
                            restriction: valueRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ReservedIdValidationId,
                            "Identifier '{value}' begins with a reserved prefix.",
                            "Modify the identifier so that it does not begin with any of these reserved prefixes: {restriction}",
                            value: identifier,
                            restriction: valueRestriction);
                    }

                    return;
                case "siblingPropertyMismatch":
                    if (elementId == null || propertyName == null || refValue == null || valueRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or refValue or valueRestriction when generating siblingPropertyMismatch ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            SiblingPropertyMismatchValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value '{value}' instead of '{restriction}'.",
                            "Change the value of property '{property}' to '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: refValue,
                            restriction: valueRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            SiblingPropertyMismatchValidationId,
                            "{primaryId:p} property '{property}' has value '{value}' instead of '{restriction}'.",
                            "Change the value of property '{property}' to '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: refValue,
                            restriction: valueRestriction);
                    }

                    return;
                case "stringConstraintMultipleValues":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating stringConstraintMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringConstraintMultipleValuesValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringConstraintMultipleValuesValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "stringConstraintNotString":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating stringConstraintNotString ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringConstraintNotStringValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' value is not a JSON string.",
                            "Change the value of property '{property}' to a JSON string.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringConstraintNotStringValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' value is not a JSON string.",
                            "Change the value of property '{property}' to a JSON string.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "stringConstraintNoValue":
                    if (contextId == null || elementId == null || constraintName == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or constraintName when generating stringConstraintNoValue ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringConstraintNoValueValidationId,
                            "In {sourceName1}{line1}, DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' is empty.",
                            "Provide a single string value for property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringConstraintNoValueValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId:p} property '{property}' is empty.",
                            "Provide a single string value for property '{property}'.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            property: constraintName);
                    }

                    return;
                case "stringCountBelowMin":
                    if (elementId == null || propertyName == null || observedCount == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or observedCount or expectedCount when generating stringCountBelowMin ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} string {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            StringCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has {count1} {item1} but {count2} {item2} {verb2} required.");
                        SetCount(causeBuilder, 1, (int)observedCount, "value", "values");
                        SetCount(causeBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        StringBuilder actionBuilder = new StringBuilder("Provide at least {count2} string {item2} for '{property}'.");
                        SetCount(actionBuilder, 2, (int)expectedCount, "value", "values", "is", "are");

                        this.Add(
                            StringCountBelowMinValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "stringInvalid":
                    if (elementId == null || propertyName == null || propertyValue == null || pattern == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or pattern when generating stringInvalid ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringInvalidValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value '{value}', which is invalid.",
                            "Modify the value of '{property}' to make it match the regular expression '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: pattern,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringInvalidValidationId,
                            "{layer}{primaryId:p} property '{property}' has value '{value}', which is invalid.",
                            "Modify the value of '{property}' to make it match the regular expression '{restriction}'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: pattern,
                            layer: layer);
                    }

                    return;
                case "stringMultipleValues":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating stringMultipleValues ParsingError.");
                    }

                    if (incidentValues != null && incidentValues.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringMultipleValuesValidationId,
                            "In {sourceName1},{line1}, property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringMultipleValuesValidationId,
                            "{layer}{primaryId:p} property '{property}' has multiple values but only one value is allowed.",
                            "Remove all but one of the values of '{property}'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "stringNotString":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating stringNotString ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringNotStringValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is not a JSON string.",
                            "Change the value of '{property}' to a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringNotStringValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is not a JSON string.",
                            "Change the value of '{property}' to a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "stringObjectNoValue":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating stringObjectNoValue ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringObjectNoValueValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a string value to the object, or replace the object with a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringObjectNoValueValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object with no '@value' property.",
                            "Add a '@value' property with a string value to the object, or replace the object with a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "stringTooLong":
                    if (elementId == null || propertyName == null || propertyValue == null || expectedCount == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or expectedCount when generating stringTooLong ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        StringBuilder causeBuilder = new StringBuilder("In {sourceName1},{line1}, property '{property}' has value '{value}', which is too long -- length limit is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for '{property}' or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            StringTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        StringBuilder causeBuilder = new StringBuilder("{layer}{primaryId:p} property '{property}' has value '{value}', which is too long -- length limit is {count1} {item1}.");
                        SetCount(causeBuilder, 1, (int)expectedCount, "character", "characters");

                        StringBuilder actionBuilder = new StringBuilder("Select a shorter value for '{property}' or trim current value to no more than {count1} {item1}.");
                        SetCount(actionBuilder, 1, (int)expectedCount, "character", "characters");

                        this.Add(
                            StringTooLongValidationId,
                            causeBuilder.ToString(),
                            actionBuilder.ToString(),
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            layer: layer);
                    }

                    return;
                case "stringTypeNotString":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating stringTypeNotString ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringTypeNotStringValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:string'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:string'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringTypeNotStringValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@type' does not specify 'xsd:string'.",
                            "Remove the '@type' property of '{property}' or change its value to 'xsd:string'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "stringValueNotString":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating stringValueNotString ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            StringValueNotStringValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value that is a JSON object whose '@value' is not a JSON string.",
                            "Change the value of the '@value' property of '{property}' to a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            StringValueNotStringValidationId,
                            "{layer}{primaryId:p} property '{property}' has value that is a JSON object whose '@value' is not a JSON string.",
                            "Change the value of the '@value' property of '{property}' to a JSON string.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "subClassOfNotExtensible":
                    if (contextId == null || elementId == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or propertyValue when generating subClassOfNotExtensible ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            SubClassOfNotExtensibleValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'rdfs:subClassOf' property whose value '{value}'{line1} does not indicate an extensible DTDL class.",
                            "Change the string value of 'rdfs:subClassOf' to an extensible DTDL type or a subclass thereof.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            value: propertyValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            SubClassOfNotExtensibleValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'rdfs:subClassOf' property whose value '{value}' does not indicate an extensible DTDL class.",
                            "Change the string value of 'rdfs:subClassOf' to an extensible DTDL type or a subclass thereof.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            value: propertyValue);
                    }

                    return;
                case "subClassOfNotRecognized":
                    if (contextId == null || elementId == null || propertyValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextId or elementId or propertyValue when generating subClassOfNotRecognized ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            SubClassOfNotRecognizedValidationId,
                            "In {sourceName1}, DTDL language extension {primaryId} metamodel element {secondaryId} has 'rdfs:subClassOf' property whose value '{value}'{line1} is not recognized.",
                            "Change the string value of 'rdfs:subClassOf' to an extensible DTDL type or a subclass thereof.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            value: propertyValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            SubClassOfNotRecognizedValidationId,
                            "DTDL language extension {primaryId} metamodel element {secondaryId} has 'rdfs:subClassOf' property whose value '{value}' is not recognized.",
                            "Change the string value of 'rdfs:subClassOf' to an extensible DTDL type or a subclass thereof.",
                            primaryId: contextId,
                            secondaryId: elementId,
                            value: propertyValue);
                    }

                    return;
                case "topLevelGraphDisallowed":
                    if (element != null && element.TryGetSourceLocationForGraph(out sourceName1, out startLine1))
                    {
                        this.Add(
                            TopLevelGraphDisallowedValidationId,
                            "In {sourceName1}, top-level JSON object contains '@graph' property{line1}, which is not allowed.",
                            "Remove the '@graph' property, and elevate the value of this property to the top level of the JSON document.",
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            TopLevelGraphDisallowedValidationId,
                            "Top-level JSON object contains '@graph' property, which is not allowed.",
                            "Remove the '@graph' property, and elevate the value of this property to the top level of the JSON document.");
                    }

                    return;
                case "topLevelKeywordDisallowed":
                    if (keyword == null)
                    {
                        throw new ArgumentException("Missing required parameter keyword when generating topLevelKeywordDisallowed ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForKeyword(keyword, out sourceName1, out startLine1))
                    {
                        this.Add(
                            TopLevelKeywordDisallowedValidationId,
                            "In {sourceName1}, top-level JSON object contains '{value}' property{line1}, which is not allowed.",
                            "Remove the '{value}' property.",
                            value: keyword,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            TopLevelKeywordDisallowedValidationId,
                            "Top-level JSON object contains '{value}' property, which is not allowed.",
                            "Remove the '{value}' property.",
                            value: keyword);
                    }

                    return;
                case "typeInvalidDtmi":
                    if (elementId == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or cotype when generating typeInvalidDtmi ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            TypeInvalidDtmiValidationId,
                            "In {sourceName1}, element has @type{line1} that specifies type {value} that is an invaild DTMI.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            TypeInvalidDtmiValidationId,
                            "{layer}{primaryId:n} has @type that specifies type {value} that is an invaild DTMI.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer);
                    }

                    return;
                case "typeIrrelevantDtmiOrTerm":
                    if (elementId == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or cotype when generating typeIrrelevantDtmiOrTerm ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            TypeIrrelevantDtmiOrTermValidationId,
                            "In {sourceName1}, element has @type{line1} that specifies type {value} that is not a DTDL material type or supplemental type.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            TypeIrrelevantDtmiOrTermValidationId,
                            "{layer}{primaryId:n} has @type that specifies type {value} that is not a DTDL material type or supplemental type.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer);
                    }

                    return;
                case "typeNotDtmiNorTerm":
                    if (elementId == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or cotype when generating typeNotDtmiNorTerm ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            TypeNotDtmiNorTermValidationId,
                            "In {sourceName1}, element has @type{line1} that specifies type {value} that is an IRI but not a DTMI.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            TypeNotDtmiNorTermValidationId,
                            "{layer}{primaryId:n} has @type that specifies type {value} that is an IRI but not a DTMI.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer);
                    }

                    return;
                case "typeNotRootable":
                    if (identifier == null || typeRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter identifier or typeRestriction when generating typeNotRootable ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            TypeNotRootableValidationId,
                            "In {sourceName1}, top-level JSON element {value}{line1} does not have @type of {type}.",
                            "Provide a '@type' value in the set of allowable types.",
                            value: identifier,
                            type: typeRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            TypeNotRootableValidationId,
                            "Top-level JSON element {value} does not have @type of {type}.",
                            "Provide a '@type' value in the set of allowable types.",
                            value: identifier,
                            type: typeRestriction);
                    }

                    return;
                case "typeUndefinedTerm":
                    if (elementId == null || cotype == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or cotype when generating typeUndefinedTerm ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForType(out sourceName1, out startLine1))
                    {
                        this.Add(
                            TypeUndefinedTermValidationId,
                            "In {sourceName1}, element has @type{line1} that specifies type {value} that is an undefined term.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            TypeUndefinedTermValidationId,
                            "{layer}{primaryId:n} has @type that specifies type {value} that is an undefined term.",
                            "Remove @type {value} or replace with an appropriate DTDL type -- see aka.ms/dtdl.",
                            primaryId: elementId,
                            value: cotype,
                            layer: layer);
                    }

                    return;
                case "undefinedReservedId":
                    if (elementId == null || propertyName == null || referenceId == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId when generating undefinedReservedId ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1) && incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName2, out startLine2, out endLine2))
                    {
                        this.Add(
                            UndefinedReservedIdValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value{secondaryId:e}{line2} that has no known definition despite having a reserved prefix.",
                            "Revise the value of property '{property}'{line1} to refer to a valid DTDL element or to an element defined in the model.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1,
                            sourceName2: sourceName2,
                            startLine2: startLine2,
                            endLine2: endLine2);
                    }
                    else
                    {
                        this.Add(
                            UndefinedReservedIdValidationId,
                            "{primaryId:p} property '{property}' has value{secondaryId:e} that has no known definition despite having a reserved prefix.",
                            "Revise the value of property '{property}' to refer to a valid DTDL element or to an element defined in the model.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId);
                    }

                    return;
                case "unrecognizedContextVersion":
                    if (contextValue == null || versionRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue or versionRestriction when generating unrecognizedContextVersion ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            UnrecognizedContextVersionValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1}, which specifies a DTDL version that is not recognized.",
                            "Modify @context specifier to indicate one of the following DTDL versions: {restriction}.",
                            value: contextValue,
                            restriction: versionRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            UnrecognizedContextVersionValidationId,
                            "@context specifier has value '{value}', which specifies a DTDL version that is not recognized.",
                            "Modify @context specifier to indicate one of the following DTDL versions: {restriction}.",
                            value: contextValue,
                            restriction: versionRestriction);
                    }

                    return;
                case "unresolvableContextSpecifier":
                    if (contextValue == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue when generating unresolvableContextSpecifier ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            UnresolvableContextSpecifierValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1}, which is unrecognized.",
                            "Remove '{value}' @context specifier{line1}.",
                            value: contextValue,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            UnresolvableContextSpecifierValidationId,
                            "@context specifier has value '{value}', which is unrecognized.",
                            "Remove '{value}' @context specifier.",
                            value: contextValue);
                    }

                    return;
                case "unresolvableContextVersion":
                    if (contextValue == null || versionRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter contextValue or versionRestriction when generating unresolvableContextVersion ParsingError.");
                    }

                    if (contextComponent != null && contextComponent.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            UnresolvableContextVersionValidationId,
                            "In {sourceName1}, @context specifier has value '{value}'{line1}, which specifies a context version that is not recognized.",
                            "Modify @context specifier to indicate one of the following versions: {restriction}.",
                            value: contextValue,
                            restriction: versionRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            UnresolvableContextVersionValidationId,
                            "@context specifier has value '{value}', which specifies a context version that is not recognized.",
                            "Modify @context specifier to indicate one of the following versions: {restriction}.",
                            value: contextValue,
                            restriction: versionRestriction);
                    }

                    return;
                case "valueAboveMax":
                    if (elementId == null || propertyName == null || propertyValue == null || limit == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or limit when generating valueAboveMax ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ValueAboveMaxValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value {value}, which is greater than the allowed maximum of {restriction}.",
                            "Reduce the value of '{property}' to a value less than or equal to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: limit,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueAboveMaxValidationId,
                            "{layer}{primaryId:p} property '{property}' has value {value}, which is greater than the allowed maximum of {restriction}.",
                            "Reduce the value of '{property}' to a value less than or equal to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: limit,
                            layer: layer);
                    }

                    return;
                case "valueAboveRange":
                    if (elementId == null || propertyName == null || propertyValue == null || range == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or range when generating valueAboveRange ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ValueAboveRangeValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value {value}, which is above the allowed range of {restriction}.",
                            "Reduce the value of '{property}' to a value in the range {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: range,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueAboveRangeValidationId,
                            "{layer}{primaryId:p} property '{property}' has value {value}, which is above the allowed range of {restriction}.",
                            "Reduce the value of '{property}' to a value in the range {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: range,
                            layer: layer);
                    }

                    return;
                case "valueBelowMin":
                    if (elementId == null || propertyName == null || propertyValue == null || limit == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or limit when generating valueBelowMin ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ValueBelowMinValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value {value}, which is less than the allowed minimum of {restriction}.",
                            "Increase the value of '{property}' to a value greater than or equal to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: limit,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueBelowMinValidationId,
                            "{layer}{primaryId:p} property '{property}' has value {value}, which is less than the allowed minimum of {restriction}.",
                            "Increase the value of '{property}' to a value greater than or equal to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: limit,
                            layer: layer);
                    }

                    return;
                case "valueBelowRange":
                    if (elementId == null || propertyName == null || propertyValue == null || range == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or range when generating valueBelowRange ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ValueBelowRangeValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value {value}, which is below the allowed range of {restriction}.",
                            "Increase the value of '{property}' to a value in the range {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: range,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueBelowRangeValidationId,
                            "{layer}{primaryId:p} property '{property}' has value {value}, which is below the allowed range of {restriction}.",
                            "Increase the value of '{property}' to a value in the range {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: range,
                            layer: layer);
                    }

                    return;
                case "valueNotExact":
                    if (elementId == null || propertyName == null || propertyValue == null || limit == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or propertyValue or limit when generating valueNotExact ParsingError.");
                    }

                    if (incidentValue != null && incidentValue.TryGetSourceLocation(out sourceName1, out startLine1, out endLine1))
                    {
                        this.Add(
                            ValueNotExactValidationId,
                            "In {sourceName1},{line1}, property '{property}' has value {value} but the only allowed value is {restriction}.",
                            "Change the value of '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: limit,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueNotExactValidationId,
                            "{layer}{primaryId:p} property '{property}' has value {value} but the only allowed value is {restriction}.",
                            "Change the value of '{property}' to {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            value: propertyValue,
                            restriction: limit,
                            layer: layer);
                    }

                    return;
                case "valueObjectContext":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating valueObjectContext ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForContext(out sourceName1, out startLine1))
                    {
                        this.Add(
                            ValueObjectContextValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@context' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueObjectContextValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@context' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "valueObjectGraph":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating valueObjectGraph ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForGraph(out sourceName1, out startLine1))
                    {
                        this.Add(
                            ValueObjectGraphValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@graph' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueObjectGraphValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@graph' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "valueObjectId":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating valueObjectId ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForId(out sourceName1, out startLine1))
                    {
                        this.Add(
                            ValueObjectIdValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@id' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueObjectIdValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@id' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "valueObjectInvalidProp":
                    if (elementId == null || propertyName == null || literalPropertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or literalPropertyName when generating valueObjectInvalidProp ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            ValueObjectInvalidPropValidationId,
                            "In {sourceName1},{line1}, property '{property}' has property '{value}', which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: literalPropertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueObjectInvalidPropValidationId,
                            "{layer}{primaryId:p} property '{property}' has property '{value}', which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: literalPropertyName,
                            layer: layer);
                    }

                    return;
                case "valueObjectKeyword":
                    if (elementId == null || propertyName == null || keyword == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or keyword when generating valueObjectKeyword ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForKeyword(keyword, out sourceName1, out startLine1))
                    {
                        this.Add(
                            ValueObjectKeywordValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '{value}' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueObjectKeywordValidationId,
                            "{layer}{primaryId:p} property '{property}' has '{value}' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            value: keyword,
                            layer: layer);
                    }

                    return;
                case "valueObjectLanguage":
                    if (elementId == null || propertyName == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName when generating valueObjectLanguage ParsingError.");
                    }

                    if (element != null && element.TryGetSourceLocationForLanguage(out sourceName1, out startLine1))
                    {
                        this.Add(
                            ValueObjectLanguageValidationId,
                            "In {sourceName1},{line1}, property '{property}' has '@language' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            ValueObjectLanguageValidationId,
                            "{layer}{primaryId:p} property '{property}' has '@language' property, which is not allowed in a value object.",
                            "Remove all properties from object other than '@value' and '@type'.",
                            primaryId: elementId,
                            property: propertyName,
                            layer: layer);
                    }

                    return;
                case "wrongParent":
                    if (elementId == null || propertyName == null || referenceId == null || relation == null || relationRestriction == null)
                    {
                        throw new ArgumentException("Missing required parameter elementId or propertyName or referenceId or relation or relationRestriction when generating wrongParent ParsingError.");
                    }

                    if (incidentProperty != null && incidentProperty.TryGetSourceLocation(out sourceName1, out startLine1))
                    {
                        this.Add(
                            WrongParentValidationId,
                            "In {sourceName1}, property '{property}'{line1} has value {secondaryId}, which is a {value} but must be a child of {restriction}.",
                            "Change the value of property '{property}' to an element that is a child of {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: relation,
                            restriction: relationRestriction,
                            sourceName1: sourceName1,
                            startLine1: startLine1,
                            endLine1: endLine1);
                    }
                    else
                    {
                        this.Add(
                            WrongParentValidationId,
                            "{primaryId:p} property '{property}' has value {secondaryId}, which is a {value} but must be a child of {restriction}.",
                            "Change the value of property '{property}' to an element that is a child of {restriction}.",
                            primaryId: elementId,
                            property: propertyName,
                            secondaryId: referenceId,
                            value: relation,
                            restriction: relationRestriction);
                    }

                    return;
                default:
                    throw new InvalidOperationException($"Missing error-generation case for validation code {validationCode}.");
            }
        }

        /// <summary>
        /// Generate an appropriate parsing error, add it to the collection, and throw a <c>ParsingException</c>.
        /// </summary>
        /// <param name="validationCode">A string uniquely identifying the validation condition that identifies the error.</param>
        /// <param name="contextId">A context identifer relevant to the error.</param>
        /// <param name="elementId">A URI that identifies the element in which the error was found.</param>
        /// <param name="referenceId">A URI that is referenced and thereby causes or contributes to the error.</param>
        /// <param name="governingId">A URI of an element that governs the value in which the error was found.</param>
        /// <param name="typeId">A URI that identifies a type relevant to the error.</param>
        /// <param name="identifier">A string that represents an identifier that is not necessarily valid.</param>
        /// <param name="cotype">A string representing a required or prohibited co-type on the element in which the error was found.</param>
        /// <param name="relation">A string describing a relationship to another element that is required or prohibited.</param>
        /// <param name="referenceType">A string describing a type that is directly or indirectly referenced and thereby causes or contributes to the error.</param>
        /// <param name="propertyName">A string representing the name of the property among whose values the error was found.</param>
        /// <param name="propertyConjunction">A string describing a conjunction of property names among whose values the error was found.</param>
        /// <param name="propertyDisjunction">A string describing a disjunction of property names among whose values the error was found.</param>
        /// <param name="governingPropertyName">A string representing the name of a property that governs the value in which the error was found.</param>
        /// <param name="constraintName">A string representing the name of a constraint relevant to the error.</param>
        /// <param name="nestedName">A string indicating a property name within an element that is a value of the incident property.</param>
        /// <param name="literalPropertyName">A string indicating a property on a JSON object value that is a localized string or representational literal.</param>
        /// <param name="refPropertyName">A string representing the name of a property that references another element that has a property that contributes to the error.</param>
        /// <param name="propertyValue">A string indicating a value of the property that is erroneous.</param>
        /// <param name="constraintValue">A string indicating a constraint that identifies or relates to the error.</param>
        /// <param name="contextValue">A string indicating the value of a context specifier.</param>
        /// <param name="termValue">A string indicating the value of a term definition in a local context block.</param>
        /// <param name="nestedValue">A string indicating the value of a property within an element that is a value of the incident property.</param>
        /// <param name="refValue">A string indicating the value of a referenced property value.</param>
        /// <param name="valueConjunction">A string indicating a conjunction of property values.</param>
        /// <param name="nameValuePair">A string describing a name-value pair relevant to the error.</param>
        /// <param name="typeRestriction">A string representing a required or prohibited type of the erroneous value.</param>
        /// <param name="valueRestriction">A string representing a required or prohibited value for the erroneous value.</param>
        /// <param name="relationRestriction">A string representing a requirement or prohibition on a relationship to another elemnt.</param>
        /// <param name="versionRestriction">A string describing a the required or prohibited versions for a value.</param>
        /// <param name="langCode">A string representing a language code.</param>
        /// <param name="keyword">A string indicating a JSON-LD keyword relevant to the error.</param>
        /// <param name="pattern">A string representing a regular expression pattern.</param>
        /// <param name="limit">A string describing a numeric limit.</param>
        /// <param name="range">A string describing a range of values.</param>
        /// <param name="datatype">A string describing a literal datatype.</param>
        /// <param name="elementType">A string describing a DTDL element type.</param>
        /// <param name="term">A string indicating a JSON-LD term relevant to the error.</param>
        /// <param name="layer">The name of the layer in which the error was found.</param>
        /// <param name="version">A version number related to the error.</param>
        /// <param name="partition">A string indicating the partition in which the error was found.</param>
        /// <param name="refPartition">A string indicating a partition that is referenced by the erroneous property.</param>
        /// <param name="violations">A collection of strings that each indicate a validation failure, if there are any.</param>
        /// <param name="observedCount">A numerical value for the observed count of some item.</param>
        /// <param name="expectedCount">A numerical value for the expected count of some item.</param>
        /// <param name="violationCount">A numerical value for the count of validation failures.</param>
        /// <param name="contextComponent">A <see cref="JsonLdContextComponent"/> in which the error was found.</param>
        /// <param name="element">A <see cref="JsonLdElement"/> in which the error was found.</param>
        /// <param name="extantElement">A <see cref="JsonLdElement"/> that is extant.</param>
        /// <param name="siblingElement">A <see cref="JsonLdElement"/> that is a sibling of <paramref name="element"/>.</param>
        /// <param name="ancestorElement">Used in conjunction with <paramref name="descendantElement"/>, a <see cref="JsonLdElement"/> higher in the hierarchy that contains the error.</param>
        /// <param name="descendantElement">Used in conjunction with <paramref name="ancestorElement"/>, a <see cref="JsonLdElement"/> lower in the hierarchy that contains the error.</param>
        /// <param name="incidentProperty">A <see cref="JsonLdProperty"/> in which the error was found.</param>
        /// <param name="extantProperty">A <see cref="JsonLdProperty"/> for an extant property related to the error.</param>
        /// <param name="governingProperty">A <see cref="JsonLdProperty"/> for a governing property related to the error.</param>
        /// <param name="refProperty">A <see cref="JsonLdProperty"/> for a property that references another element that has a property that contributes to the error.</param>
        /// <param name="incidentValues">A <see cref="JsonLdValueCollection"/> that holds values of the erroneous property.</param>
        /// <param name="governingValues">A <see cref="JsonLdValueCollection"/> that holds values of a governing property.</param>
        /// <param name="incidentValue">A <see cref="JsonLdValue"/> that holds the erroneous property value.</param>
        /// <param name="extantValue">A <see cref="JsonLdValue"/> that holds an extant value related to the error.</param>
        /// <param name="extensionProperty">A <see cref="DtdlExtensionProperty"/> that holds information about a property defined by an extension.</param>
        internal void Quit(string validationCode, Uri contextId = null, Uri elementId = null, Uri referenceId = null, Uri governingId = null, Uri typeId = null, string identifier = null, string cotype = null, string relation = null, string referenceType = null, string propertyName = null, string propertyConjunction = null, string propertyDisjunction = null, string governingPropertyName = null, string constraintName = null, string nestedName = null, string literalPropertyName = null, string refPropertyName = null, string propertyValue = null, string constraintValue = null, string contextValue = null, string termValue = null, string nestedValue = null, string refValue = null, string valueConjunction = null, string nameValuePair = null, string typeRestriction = null, string valueRestriction = null, string relationRestriction = null, string versionRestriction = null, string langCode = null, string keyword = null, string pattern = null, string limit = null, string range = null, string datatype = null, string elementType = null, string term = null, string layer = null, string version = null, string partition = null, string refPartition = null, IReadOnlyCollection<string> violations = null, int? observedCount = null, int? expectedCount = null, int? violationCount = null, JsonLdContextComponent contextComponent = null, JsonLdElement element = null, JsonLdElement extantElement = null, JsonLdElement siblingElement = null, JsonLdElement ancestorElement = null, JsonLdElement descendantElement = null, JsonLdProperty incidentProperty = null, JsonLdProperty extantProperty = null, JsonLdProperty governingProperty = null, JsonLdProperty refProperty = null, JsonLdValueCollection incidentValues = null, JsonLdValueCollection governingValues = null, JsonLdValue incidentValue = null, JsonLdValue extantValue = null, DtdlExtensionProperty extensionProperty = null)
        {
            this.Notify(validationCode, contextId, elementId, referenceId, governingId, typeId, identifier, cotype, relation, referenceType, propertyName, propertyConjunction, propertyDisjunction, governingPropertyName, constraintName, nestedName, literalPropertyName, refPropertyName, propertyValue, constraintValue, contextValue, termValue, nestedValue, refValue, valueConjunction, nameValuePair, typeRestriction, valueRestriction, relationRestriction, versionRestriction, langCode, keyword, pattern, limit, range, datatype, elementType, term, layer, version, partition, refPartition, violations, observedCount, expectedCount, violationCount, contextComponent, element, extantElement, siblingElement, ancestorElement, descendantElement, incidentProperty, extantProperty, governingProperty, refProperty, incidentValues, governingValues, incidentValue, extantValue, extensionProperty);
            throw new ParsingException(this.parsingErrors);
        }
    }
}
