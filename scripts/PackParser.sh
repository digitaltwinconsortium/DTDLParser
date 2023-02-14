#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh PackParser $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dll DTDLParser.dll
#:: out nupkg DTDLParser.*.nupkg

dotnet pack dotnet/src/Parser --configuration $DtdlModelParserBuildConfig -p:PublicRelease=true

test $? -eq 0 || bash scripts/Failure.sh PackParser $DtdlModelParserBuildConfig
