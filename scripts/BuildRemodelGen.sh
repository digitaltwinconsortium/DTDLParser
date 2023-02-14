#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildRemodelGen $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/gen/RemodelGenerator
#:: in dll MetamodelDigest.dll
#:: in dll CodeGenerator.dll
#:: out exe RemodelGenerator.exe

dotnet build dotnet/gen/RemodelGenerator --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildRemodelGen $DtdlModelParserBuildConfig
