#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh RunDocFx $DtdlModelParserBuildConfig

[[ -d api-docs/dotnet/_site ]] && rm -r api-docs/dotnet/_site
[[ -d api-docs/dotnet/api ]] && rm -r api-docs/dotnet/api

if [ -z "$3" ]; then
  where -q docfx.exe
  if [ $? -ne 0 ]; then
    echo The DocFx application docfx.exe is not on your PATH.
    echo Without this, the API documentation cannot be generated.
    echo It is not necessary to generate this locally; the pipeline will generate it for you when you submit a PR.
    echo If you want to generate the API documentation locally, you can install DocFx by running the following line from an elevated command prompt:
    echo   choco install docfx -y
    echo Then ensure the full path to docfx.exe is on your PATH.
    bash scripts/Failure.sh RunDocFx $DtdlModelParserBuildConfig
    exit 1
  fi
fi

#:: job dotnet
#:: in dotnet dotnet/src/Parser
#:: in dotnet dotnet/src/Parser/generated
#:: in json docfx.json

${3}docfx.exe api-docs/dotnet/docfx.json

test $? -eq 0 || bash scripts/Failure.sh RunDocFx $DtdlModelParserBuildConfig
