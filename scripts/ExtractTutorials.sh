#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

TutorialsConfig="manifest:tutorials/README.md"

bash scripts/Title.sh ExtractTutorials $DtdlModelParserBuildConfig

for d in tutorials/projects/Tutorial* ; do
  [[ -d "$d" ]] && rm -r "$d"
done

[[ -d tutorials/projects/Tutorials.sln ]] && rm -r tutorials/projects/Tutorials.sln

#:: job dotnet
#:: in exe TutorialExtractor.exe
#:: in json dtdl.json
#:: in md tutorials
#:: out dotnet tutorials/projects

dotnet run --project dotnet/gen/TutorialExtractor --no-launch-profile --configuration $DtdlModelParserBuildConfig tutorials/projects ${TutorialsConfig}

test $? -eq 0 || bash scripts/Failure.sh ExtractTutorials $DtdlModelParserBuildConfig
