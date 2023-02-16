#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildJsInterop $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/src/JsInterop
#:: out dll DTDLJsInterop.dll

dotnet build dotnet/src/JsInterop --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildJsInterop $DtdlModelParserBuildConfig
