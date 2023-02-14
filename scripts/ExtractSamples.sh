#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

if [ "$2" == "dtdlVNext" ]; then
  SamplesConfig="pattern:samples/Sample??_*.md"
else
  SamplesConfig="manifest:samples/README.md"
fi

bash scripts/Title.sh ExtractSamples $DtdlModelParserBuildConfig

for d in samples/projects/Sample* ; do
  [[ -d "$d" ]] && rm -r "$d"
done

[[ -d samples/projects/Samples.sln ]] && rm -r samples/projects/Samples.sln

#:: job dotnet
#:: in exe SampleExtractor.exe
#:: in json dtdl.json
#:: in md samples
#:: out dotnet samples/projects

dotnet run --project dotnet/gen/SampleExtractor --configuration $DtdlModelParserBuildConfig samples/projects ${SamplesConfig}

test $? -eq 0 || bash scripts/Failure.sh ExtractSamples $DtdlModelParserBuildConfig
