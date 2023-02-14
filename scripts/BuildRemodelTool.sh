#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildRemodelTool $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/src/Remodel
#:: in dotnet dotnet/src/Remodel/generated
#:: in dll DTDLParser.dll
#:: out exe DTDLRemodel.exe

dotnet build dotnet/src/Remodel --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildRemodelTool $DtdlModelParserBuildConfig
