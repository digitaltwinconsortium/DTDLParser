#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh PackJsInterop $DtdlModelParserBuildConfig

#:: job npm
#:: in dotnet/src/JsInterop/bin/[Release or Debug]/net7.0/browser-wasm/AppBundle
#:: out tgz dotnet/src/JsInterop/bin/$DtdlModelParserBuildConfig/net7.0/browser-wasm/AppBundle/dtdl-parser-interop-x.x.x.tgz

AppBundleDir=dotnet/src/JsInterop/bin/$DtdlModelParserBuildConfig/net7.0/browser-wasm/AppBundle

npm pack $AppBundleDir --pack-destination $AppBundleDir

test $? -eq 0 || bash scripts/Failure.sh PackJsInterop $DtdlModelParserBuildConfig