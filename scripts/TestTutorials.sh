#!/bin/bash

if [ "$1" == "Release" ]; then
  DtdlModelParserBuildConfig=Release
else
  DtdlModelParserBuildConfig=Debug
fi

bash scripts/Title.sh TestTutorials $DtdlModelParserBuildConfig

ErrLev=0

#:: job dotnet
#:: in exe Tutorial*.exe
#:: in dotnet tutorials/projects

for d in tutorials/projects/Tutorial* ; do
  [[ -f $d/output.txt ]] && rm $d/output.txt
done

for d in tutorials/projects/Tutorial* ; do
  if [ -d $d ]; then
    echo testing $d ;
    dotnet run --project $d --configuration $DtdlModelParserBuildConfig | grep -v "^##vso" > $d/output.txt ;
    diff --strip-trailing-cr $d/expect.txt $d/output.txt ;
    test $? -eq 0 || ErrLev=1
  fi
done

if [ $ErrLev -eq 0 ]; then
  for d in tutorials/projects/Tutorial* ; do
    [[ -f $d/output.txt ]] && rm $d/output.txt
  done
else
  bash scripts/Failure.sh TestTutorials $DtdlModelParserBuildConfig
fi

exit $ErrLev
