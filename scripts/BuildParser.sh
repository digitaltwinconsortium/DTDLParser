#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildParser $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/src/DTDLParser
#:: in dotnet dotnet/src/DTDLParser/generated
#:: out dll DTDLParser.dll

dotnet build dotnet/src/DTDLParser --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildParser $DtdlModelParserBuildConfig
