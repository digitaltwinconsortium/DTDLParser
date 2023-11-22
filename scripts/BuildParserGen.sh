#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildParserGen $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/gen/ParserGenerator
#:: in dll MetamodelDigest.dll
#:: in dll CodeGenerator.dll
#:: out exe ParserGenerator.exe

dotnet build dotnet/gen/ParserGenerator --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildParserGen $DtdlModelParserBuildConfig
