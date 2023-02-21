#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildJsInterop $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/src/JsInterop
#:: in js javascript/index.js
#:: in ts javascript/index.d.ts
#:: in ts javascript/DtdlErr.d.ts
#:: in ts javascript/generated/DtdlOm.d.ts
#:: in json javascript/package.json
#:: out dotnet/src/JsInterop/bin/[Release or Debug]/net7.0/browser-wasm/AppBundle

dotnet publish dotnet/src/JsInterop --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildJsInterop $DtdlModelParserBuildConfig