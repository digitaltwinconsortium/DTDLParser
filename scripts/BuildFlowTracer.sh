#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildFlowTracer $DtdlModelParserBuildConfig

dotnet build dotnet/gen/FlowTracer --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildFlowTracer $DtdlModelParserBuildConfig
