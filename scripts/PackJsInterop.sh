#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh PackJsInterop $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dll DTDLJsInterop.dll
#:: out nupkg DTDLJsInterop.*.nupkg

dotnet pack dotnet/src/JsInterop --configuration $DtdlModelParserBuildConfig -p:PublicRelease=true

test $? -eq 0 || bash scripts/Failure.sh PackJsInterop $DtdlModelParserBuildConfig
