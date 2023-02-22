#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildJsInterop $DtdlModelParserBuildConfig

dotnet publish javascript --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildJsInterop $DtdlModelParserBuildConfig
