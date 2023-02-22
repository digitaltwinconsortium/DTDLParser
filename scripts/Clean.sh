#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh Clean $DtdlModelParserBuildConfig

[[ -d dotnet/gen/MetamodelDigest/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/MetamodelDigest/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/gen/MetamodelDigest/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/MetamodelDigest/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/gen/MetamodelDigest/obj/project.assets.json ]] && rm dotnet/gen/MetamodelDigest/obj/project.assets.json
[[ -d dotnet/gen/CodeGenerator/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/CodeGenerator/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/gen/CodeGenerator/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/CodeGenerator/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/gen/CodeGenerator/obj/project.assets.json ]] && rm dotnet/gen/CodeGenerator/obj/project.assets.json
[[ -d dotnet/gen/ParserGenerator/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/ParserGenerator/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/gen/ParserGenerator/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/ParserGenerator/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/gen/ParserGenerator/obj/project.assets.json ]] && rm dotnet/gen/ParserGenerator/obj/project.assets.json
[[ -d dotnet/gen/UnitTestGenerator/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/UnitTestGenerator/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/gen/UnitTestGenerator/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/UnitTestGenerator/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/gen/UnitTestGenerator/obj/project.assets.json ]] && rm dotnet/gen/UnitTestGenerator/obj/project.assets.json
[[ -d dotnet/gen/RemodelGenerator/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/RemodelGenerator/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/gen/RemodelGenerator/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/RemodelGenerator/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/gen/RemodelGenerator/obj/project.assets.json ]] && rm dotnet/gen/RemodelGenerator/obj/project.assets.json
[[ -d dotnet/gen/TutorialExtractor/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/TutorialExtractor/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/gen/TutorialExtractor/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/TutorialExtractor/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/gen/TutorialExtractor/obj/project.assets.json ]] && rm dotnet/gen/TutorialExtractor/obj/project.assets.json
[[ -d dotnet/gen/FlowTracer/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/FlowTracer/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/gen/FlowTracer/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/gen/FlowTracer/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/gen/FlowTracer/obj/project.assets.json ]] && rm dotnet/gen/FlowTracer/obj/project.assets.json

[[ -d dotnet/src/Parser/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/src/Parser/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/src/Parser/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/src/Parser/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/src/Parser/obj/project.assets.json ]] && rm dotnet/src/Parser/obj/project.assets.json
[[ -d dotnet/src/Remodel/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/src/Remodel/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/src/Remodel/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/src/Remodel/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/src/Remodel/obj/project.assets.json ]] && rm dotnet/src/Remodel/obj/project.assets.json

[[ -d dotnet/tests/DtmiUnitTest/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/DtmiUnitTest/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/tests/DtmiUnitTest/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/DtmiUnitTest/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/tests/DtmiUnitTest/obj/project.assets.json ]] && rm dotnet/tests/DtmiUnitTest/obj/project.assets.json
[[ -d dotnet/tests/ResultFormatterUnitTest/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/ResultFormatterUnitTest/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/tests/ResultFormatterUnitTest/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/ResultFormatterUnitTest/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/tests/ResultFormatterUnitTest/obj/project.assets.json ]] && rm dotnet/tests/ResultFormatterUnitTest/obj/project.assets.json
[[ -d dotnet/tests/ParserUnitTest/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/ParserUnitTest/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/tests/ParserUnitTest/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/ParserUnitTest/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/tests/ParserUnitTest/obj/project.assets.json ]] && rm dotnet/tests/ParserUnitTest/obj/project.assets.json
[[ -d dotnet/tests/ParserSyncTest/bin/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/ParserSyncTest/bin/$DtdlModelParserBuildConfig
[[ -d dotnet/tests/ParserSyncTest/obj/$DtdlModelParserBuildConfig ]] && rm -r dotnet/tests/ParserSyncTest/obj/$DtdlModelParserBuildConfig
[[ -f dotnet/tests/ParserSyncTest/obj/project.assets.json ]] && rm dotnet/tests/ParserSyncTest/obj/project.assets.json

[[ -d TestResults ]] && rm -r TestResults

for d in tutorials/projects/Tutorial* ; do
  [[ -d $d/bin/$DtdlModelParserBuildConfig ]] && rm -r $d/bin/$DtdlModelParserBuildConfig
  [[ -d $d/obj/$DtdlModelParserBuildConfig ]] && rm -r $d/obj/$DtdlModelParserBuildConfig
  [[ -f $d/obj/project.assets.json ]] && rm $d/obj/project.assets.json
done

exit 0
