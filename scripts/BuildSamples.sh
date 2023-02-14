#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildSamples $DtdlModelParserBuildConfig

ErrLev=0

#:: job dotnet
#:: in nupkg DTDLParser.*.nupkg
#:: in dotnet samples/projects
#:: out exe Sample*.exe

dotnet build samples/projects/Samples.sln --configuration $DtdlModelParserBuildConfig
test $? -eq 0 || ErrLev=1

test $ErrLev -eq 0 || bash scripts/Failure.sh BuildSamples $DtdlModelParserBuildConfig
