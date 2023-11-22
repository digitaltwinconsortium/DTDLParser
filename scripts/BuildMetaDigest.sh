#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh BuildMetaDigest $DtdlModelParserBuildConfig

#:: job dotnet
#:: in dotnet dotnet/gen/MetamodelDigest
#:: out dll MetamodelDigest.dll

dotnet build dotnet/gen/MetamodelDigest --configuration $DtdlModelParserBuildConfig

test $? -eq 0 || bash scripts/Failure.sh BuildMetaDigest $DtdlModelParserBuildConfig
