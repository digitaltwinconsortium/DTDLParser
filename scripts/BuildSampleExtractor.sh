#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildSampleExtractor $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/gen/SampleExtractor
#:: out exe SampleExtractor.exe

dotnet build dotnet/gen/SampleExtractor --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildSampleExtractor $DtdlModelParserBuildConfig
