#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh GenerateRemodelTool $DtdlModelParserBuildConfig

[[ -d dotnet/src/Remodel/generated ]] && rm -r dotnet/src/Remodel/generated

#:: job dotnet
#:: in exe RemodelGenerator.exe
#:: in json dtdl.json
#:: in json dtdl_digest.json
#:: in json ObjectModelConventions.json
#:: out dotnet dotnet/src/Remodel/generated

dotnet run --project dotnet/gen/RemodelGenerator --configuration $DtdlModelParserBuildConfig dotnet/src/Remodel/generated dtdl/dtdl_digest.json dtdl/support/ObjectModelConventions.json dtdl/dtdl.json

test $? -eq 0 || bash scripts/Failure.sh GenerateRemodelTool $DtdlModelParserBuildConfig
