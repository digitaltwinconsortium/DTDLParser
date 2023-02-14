#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh TestSamples $DtdlModelParserBuildConfig

ErrLev=0

#:: job dotnet
#:: in exe Sample*.exe
#:: in dotnet samples/projects

for d in samples/projects/Sample* ; do
  [[ -f $d/output.txt ]] && rm $d/output.txt
done

for d in samples/projects/Sample* ; do
  if [ -d $d ]; then
    echo testing $d ;
    dotnet run --project $d --configuration $DtdlModelParserBuildConfig | grep -v "^##vso" > $d/output.txt ;
    diff $d/expect.txt $d/output.txt ;
    test $? -eq 0 || ErrLev=1
  fi
done

if [ $ErrLev -eq 0 ]; then
  for d in samples/projects/Sample* ; do
    [[ -f $d/output.txt ]] && rm $d/output.txt
  done
else
  bash scripts/Failure.sh TestSamples $DtdlModelParserBuildConfig
fi

exit $ErrLev
