#!/bin/bash

echo $1

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildCodeGenerator $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/gen/CodeGenerator
#:: out dll CodeGenerator.dll

dotnet build dotnet/gen/CodeGenerator --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildCodeGenerator $DtdlModelParserBuildConfig
