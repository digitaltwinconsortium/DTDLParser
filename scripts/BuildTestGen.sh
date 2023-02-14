#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildTestGen $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/gen/UnitTestGenerator
#:: in dll MetamodelDigest.dll
#:: in dll CodeGenerator.dll
#:: out exe UnitTestGenerator.exe

dotnet build dotnet/gen/UnitTestGenerator --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildTestGen $DtdlModelParserBuildConfig
