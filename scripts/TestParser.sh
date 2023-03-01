#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh TestParser $DtdlModelParserBuildConfig

#:: job dotnet

ErrLev=0

#:: in dll ParserUnitTest.dll
#:: in json test-cases/generated
#:: in json test-cases/doc-examples
#:: in json test-cases/specification

dotnet test dotnet/tests/ParserUnitTest --configuration $DtdlModelParserBuildConfig
test $? -eq 0 || ErrLev=1

test $ErrLev -eq 0 || bash scripts/Failure.sh TestParser $DtdlModelParserBuildConfig
