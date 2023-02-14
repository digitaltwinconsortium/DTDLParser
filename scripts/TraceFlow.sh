#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh TraceFlow $DtdlModelParserBuildConfig

[[ -d images/input/generated ]] && rm -r images/input/generated
mkdir images/input/generated

[[ -d images/generated ]] && rm -r images/generated
mkdir images/generated

ErrLev=0

dotnet run --project dotnet/gen/FlowTracer --configuration $DtdlModelParserBuildConfig scripts images/input/generated/DevelopFlowDiagram.dot
if [ $? -ne 0 ]; then
  bash scripts/Failure.sh TraceFlow $DtdlModelParserBuildConfig
  exit 1
fi

dot -Tsvg images/input/generated/DevelopFlowDiagram.dot -o images/generated/DevelopFlowDiagram.svg
test $? -eq 0 || ErrLev=1

dot -Tsvg images/input/FlowDiagramShapeKey.dot -o images/generated/FlowDiagramShapeKey.svg
test $? -eq 0 || ErrLev=1

dot -Tsvg images/input/FlowDiagramColorKey.dot -o images/generated/FlowDiagramColorKey.svg
test $? -eq 0 || ErrLev=1

test $ErrLev -eq 0 || bash scripts/Failure.sh TraceFlow $DtdlModelParserBuildConfig
