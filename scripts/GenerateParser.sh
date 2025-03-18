#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh GenerateParser $DtdlModelParserBuildConfig

[[ -d dotnet/src/DTDLParser/generated ]] && rm -r dotnet/src/DTDLParser/generated

[[ -d javascript/generated ]] && rm -r javascript/generated
mkdir javascript/generated

#:: job dotnet
#:: in exe ParserGenerator.exe
#:: in json dtdl_digest.json
#:: in json ObjectModelConventions.json
#:: in json ParsingErrorMessages.json
#:: out dotnet dotnet/src/DTDLParser/generated

dotnet run --project dotnet/gen/ParserGenerator --no-launch-profile --configuration $DtdlModelParserBuildConfig dotnet/src/DTDLParser/generated SupportedExtensions.g.md javascript/generated/DtdlOm.d.ts dtdl/dtdl_digest.json dtdl/support/ObjectModelConventions.json dtdl/dtdl.json dtdl/support/ParsingErrorMessages.json

test $? -eq 0 || bash scripts/Failure.sh GenerateParser $DtdlModelParserBuildConfig
