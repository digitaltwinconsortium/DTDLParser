#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh GenerateParser $DtdlModelParserBuildConfig

[[ -d dotnet/src/Parser/generated ]] && rm -r dotnet/src/Parser/generated

[[ -d javascript/generated ]] && rm -r javascript/generated
mkdir javascript/generated

#:: job dotnet
#:: in exe ParserGenerator.exe
#:: in json dtdl_digest.json
#:: in json ObjectModelConventions.json
#:: in json ParsingErrorMessages.json
#:: out dotnet dotnet/src/Parser/generated

dotnet run --project dotnet/gen/ParserGenerator --configuration $DtdlModelParserBuildConfig dotnet/src/Parser/generated SupportedExtensions.g.md javascript/generated/DtdlOm.d.ts dtdl/dtdl_digest.json dtdl/support/ObjectModelConventions.json dtdl/dtdl.json dtdl/support/ParsingErrorMessages.json

test $? -eq 0 || bash scripts/Failure.sh GenerateParser $DtdlModelParserBuildConfig
