#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

DtdlModelParserVersion=$2

bash scripts/Title.sh DeriveParserSbom $DtdlModelParserBuildConfig

[[ -d dotnet/src/DTDLParser/_manifest ]] && rm -r dotnet/src/DTDLParser/_manifest

#:: job dotnet
#:: in dll DTDLParser.dll
#:: in dotnet dotnet/src/DTDLParser
#:: out json manifest.spdx

sbom-tool Generate -b dotnet/src/DTDLParser/bin/$DtdlModelParserBuildConfig/ -bc dotnet/src/DTDLParser -pn DTDLParser -pv ${DtdlModelParserVersion} -ps DigitalTwinsConsortium -nsb https://www.digitaltwinconsortium.org/DTDL -m dotnet/src/DTDLParser

test $? -eq 0 || bash scripts/Failure.sh DeriveParserSbom $DtdlModelParserBuildConfig
