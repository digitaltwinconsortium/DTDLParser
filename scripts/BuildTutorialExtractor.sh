#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildTutorialExtractor $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/gen/TutorialExtractor
#:: out exe TutorialExtractor.exe

dotnet build dotnet/gen/TutorialExtractor --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildTutorialExtractor $DtdlModelParserBuildConfig
