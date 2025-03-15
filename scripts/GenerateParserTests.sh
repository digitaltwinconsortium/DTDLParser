#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh GenerateParserTests $DtdlModelParserBuildConfig

[[ -d dotnet/tests/ParserUnitTest/generated ]] && rm -r dotnet/tests/ParserUnitTest/generated
mkdir dotnet/tests/ParserUnitTest/generated

#:: job dotnet
#:: in exe UnitTestGenerator.exe
#:: in json dtdl_digest.json
#:: in json ObjectModelConventions.json
#:: in json test-cases/generated
#:: in json test-cases/solecisms
#:: in json test-cases/doc-examples
#:: in json test-cases/specification
#:: out dotnet dotnet/tests/ParserUnitTest/generated

dotnet run --project dotnet/gen/UnitTestGenerator --no-launch-profile --configuration $DtdlModelParserBuildConfig dotnet/tests/ParserUnitTest/generated test-cases/generated test-cases/solecisms test-cases/doc-examples test-cases/specification dtdl/dtdl_digest.json dtdl/support/ObjectModelConventions.json

test $? -eq 0 || bash scripts/Failure.sh GenerateParserTests $DtdlModelParserBuildConfig
