#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh PackRemodelTool $DtdlModelParserBuildConfig

#:: job dotnet
#:: in exe DTDLRemodel.exe
#:: out nupkg DTDLRemodel.*.nupkg

dotnet pack dotnet/src/Remodel --configuration $DtdlModelParserBuildConfig -p:PublicRelease=true

test $? -eq 0 || bash scripts/Failure.sh PackRemodelTool $DtdlModelParserBuildConfig
