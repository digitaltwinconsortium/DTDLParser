#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildTutorials $DtdlModelParserBuildConfig

ErrLev=0

#:: job dotnet
#:: in nupkg DTDLParser.*.nupkg
#:: in dotnet tutorials/projects
#:: out exe Tutorial*.exe

dotnet build tutorials/projects/Tutorials.sln --configuration $DtdlModelParserBuildConfig
test $? -eq 0 || ErrLev=1

test $ErrLev -eq 0 || bash scripts/Failure.sh BuildTutorials $DtdlModelParserBuildConfig
