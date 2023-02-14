#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

DtdlModelParserVersion=$3

bash scripts/Title.sh DeriveParserSbom $DtdlModelParserBuildConfig

[[ -d dotnet/src/Parser/_manifest ]] && rm -r dotnet/src/Parser/_manifest

#:: job dotnet
#:: in dll DTDLParser.dll
#:: in dotnet dotnet/src/Parser
#:: out json manifest.spdx

sbom-tool Generate -b dotnet/src/Parser/bin/$DtdlModelParserBuildConfig/ -bc dotnet/src/Parser -pn DTDLParser -pv ${DtdlModelParserVersion} -ps DigitalTwinsConsortium -nsb https://www.digitaltwinconsortium.org/DTDL -m dotnet/src/Parser

test $? -eq 0 || bash scripts/Failure.sh DeriveParserSbom $DtdlModelParserBuildConfig
