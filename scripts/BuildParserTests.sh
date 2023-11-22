#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildParserTests $DtdlModelParserBuildConfig

#:: job dotnet

ErrLev=0

#:: in dotnet dotnet/tests/ParserUnitTest
#:: in dotnet dotnet/tests/ParserUnitTest/generated
#:: in dll DTDLParser.dll
#:: out dll ParserUnitTest.dll

dotnet build dotnet/tests/ParserUnitTest --configuration $DtdlModelParserBuildConfig
test $? -eq 0 || ErrLev=1

test $ErrLev -eq 0 || bash scripts/Failure.sh BuildParserTests $DtdlModelParserBuildConfig
