#!/bin/bash

FinalStep=23

if [ "${1,,}" == "d" ] || [ "${1,,}" == "debug" ]; then
  DtdlModelParserBuildConfig=Debug
  DtdlModelParserLangConfig=dtdl
elif [ "${1,,}" == "nd" ] || [ "${1,,}" == "nextdebug" ]; then
  DtdlModelParserBuildConfig=Debug
  DtdlModelParserLangConfig=dtdlVNext
elif [ "${1,,}" == "r" ] || [ "${1,,}" == "release" ]; then
  DtdlModelParserBuildConfig=Release
  DtdlModelParserLangConfig=dtdl
elif [ "${1,,}" == "nr" ] || [ "${1,,}" == "nextrelease" ]; then
  DtdlModelParserBuildConfig=Release
  DtdlModelParserLangConfig=dtdlVNext
else
  echo ""
  echo " Usage:  Develop config [firstStep [lastStep]]"
  echo ""
  echo "   config:"
  echo "     D  | Debug"
  echo "     R  | Release"
  echo "     ND | NextDebug"
  echo "     NR | NextRelease"
  echo ""
  echo "   steps:"
  echo "      0 | C   | Clean"
  echo "      1 | BMD | BuildMetaDigest"
  echo "      2 | BCG | BuildCodeGenerator"
  echo "      3 | BPG | BuildParserGen"
  echo "      4 | GP  | GenerateParser"
  echo "      5 | BP  | BuildParser"
  echo "      6 | PP  | PackParser"
  echo "      7 | DPS | DeriveParserSbom"
  echo "      8 | BJI | BuildJsInterop"
  echo "      9 | BTG | BuildTestGen"
  echo "     10 | GPT | GenerateParserTests"
  echo "     11 | BPT | BuildParserTests"
  echo "     12 | TP  | TestParser"
  echo "     13 | BRG | BuildRemodelGen"
  echo "     14 | GRT | GenerateRemodelTool"
  echo "     15 | BRT | BuildRemodelTool"
  echo "     16 | PRT | PackRemodelTool"
  echo "     17 | BTE | BuildTutorialExtractor"
  echo "     18 | ET  | ExtractTutorials"
  echo "     19 | BT  | BuildTutorials"
  echo "     20 | TT  | TestTutorials"
  echo "     21 | DF  | DocFx"
  echo "     22 | BFT | BuildFlowTracer"
  echo "     23 | TF  | TraceFlow"
  echo ""
  echo " Examples:"
  echo ""
  echo "   Develop Debug        ... execute all (steps 0-$FinalStep) for Debug config per dtdl\dtdl_digest.json"
  echo "   Develop NextDebug    ... execute all (steps 0-$FinalStep) for Debug config per dtdl\dtdlVNext_digest.json"
  echo "   Develop Release BRG  ... execute BuildRemodelGen (step 13) for Release config"
  echo "   Develop R BRG PRT    ... execute BuildRemodelGen through PackRemodelTool (steps 13-16) for Release config"
  echo "   Develop D 5 12       ... execute BuildParser through TestParser (steps 5-12) for Debug config"
  exit 1
fi

DtdlModelParserVersion=$(nbgv get-version -v SimpleVersion)

if [ -z "$2" ]; then
  firstStep=0
  lastStep=$FinalStep
else
  case ${2,,} in
    0 | c | clean )
      firstStep=0
      ;;
    1 | bmd | buildmetadigest )
      firstStep=1
      ;;
    2 | bcg | buildcodegenerator )
      firstStep=2
      ;;
    3 | bpg | buildparsergen )
      firstStep=3
      ;;
    4 | gp | generateparser )
      firstStep=4
      ;;
    5 | bp | buildparser )
      firstStep=5
      ;;
    6 | pp | packparser )
      firstStep=6
      ;;
    7 | dps | deriveparsersbom )
      firstStep=7
      ;;
    8 | bji | buildjsinterop )
      firstStep=8
      ;;
    9 | btg | buildtestgen )
      firstStep=9
      ;;
    10 | gpt | generateparsertests )
      firstStep=10
      ;;
    11 | bpt | buildparsertests )
      firstStep=11
      ;;
    12 | tp | testparser )
      firstStep=12
      ;;
    13 | brg | buildremodelgen )
      firstStep=13
      ;;
    14 | grt | generateremodeltool )
      firstStep=14
      ;;
    15 | brt | buildremodeltool )
      firstStep=15
      ;;
    16 | prt | packremodeltool )
      firstStep=16
      ;;
    17 | bte | buildtutorialextractor )
      firstStep=17
      ;;
    18 | et | extracttutorials )
      firstStep=18
      ;;
    19 | bt | buildtutorials )
      firstStep=19
      ;;
    20 | tt | testtutorials )
      firstStep=20
      ;;
    21 | df | docfx )
      firstStep=21
      ;;
    22 | bft | buildflowtracer )
      firstStep=22
      ;;
    23 | tf | traceflow )
      firstStep=23
      ;;
  esac

  if [ -z "$3" ]; then
    lastStep=$firstStep
  else
    case ${3,,} in
      0 | c | clean )
        lastStep=0
        ;;
      1 | bmd | buildmetadigest )
        lastStep=1
        ;;
      2 | bcg | buildcodegenerator )
        lastStep=2
        ;;
      3 | bpg | buildparsergen )
        lastStep=3
        ;;
      4 | gp | generateparser )
        lastStep=4
        ;;
      5 | bp | buildparser )
        lastStep=5
        ;;
      6 | pp | packparser )
        lastStep=6
        ;;
      7 | dps | deriveparsersbom )
        lastStep=7
        ;;
      8 | bji | buildjsinterop )
        lastStep=8
        ;;
      9 | btg | buildtestgen )
        lastStep=9
        ;;
      10 | gpt | generateparsertests )
        lastStep=10
        ;;
      11 | bpt | buildparsertests )
        lastStep=11
        ;;
      12 | tp | testparser )
        lastStep=12
        ;;
      13 | brg | buildremodelgen )
        lastStep=13
        ;;
      14 | grt | generateremodeltool )
        lastStep=14
        ;;
      15 | brt | buildremodeltool )
        lastStep=15
        ;;
      16 | prt | packremodeltool )
        lastStep=16
        ;;
      17 | bte | buildtutorialextractor )
        lastStep=17
        ;;
      18 | et | extracttutorials )
        lastStep=18
        ;;
      19 | bt | buildtutorials )
        lastStep=19
        ;;
      20 | tt | testtutorials )
        lastStep=20
        ;;
      21 | df | docfx )
        lastStep=21
        ;;
      22 | bft | buildflowtracer )
        lastStep=22
        ;;
      23 | tf | traceflow )
        lastStep=23
        ;;
    esac
  fi
fi

if [ -z "$firstStep" ]; then
  echo \"$2\" is not a valid step
  exit 1
fi

if [ -z "$lastStep" ]; then
  echo \"$3\" is not a valid step
  exit 1
fi

if [ $firstStep -le 0 ] && [ $lastStep -ge 0 ]; then
  scripts/Clean.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 1 ] && [ $lastStep -ge 1 ]; then
  scripts/BuildMetaDigest.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 2 ] && [ $lastStep -ge 2 ]; then
  scripts/BuildCodeGenerator.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 3 ] && [ $lastStep -ge 3 ]; then
  scripts/BuildParserGen.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 4 ] && [ $lastStep -ge 4 ]; then
  scripts/GenerateParser.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 5 ] && [ $lastStep -ge 5 ]; then
  scripts/BuildParser.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 6 ] && [ $lastStep -ge 6 ]; then
  scripts/PackParser.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 7 ] && [ $lastStep -ge 7 ]; then
  scripts/DeriveParserSbom.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig $DtdlModelParserVersion
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 8 ] && [ $lastStep -ge 8 ]; then
  scripts/BuildJsInterop.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 9 ] && [ $lastStep -ge 9 ]; then
  scripts/BuildTestGen.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 10 ] && [ $lastStep -ge 10 ]; then
  scripts/GenerateParserTests.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 11 ] && [ $lastStep -ge 11 ]; then
  scripts/BuildParserTests.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 12 ] && [ $lastStep -ge 12 ]; then
  scripts/TestParser.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 13 ] && [ $lastStep -ge 13 ]; then
  scripts/BuildRemodelGen.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 14 ] && [ $lastStep -ge 14 ]; then
  scripts/GenerateRemodelTool.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 15 ] && [ $lastStep -ge 15 ]; then
  scripts/BuildRemodelTool.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 16 ] && [ $lastStep -ge 16 ]; then
  scripts/PackRemodelTool.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 17 ] && [ $lastStep -ge 17 ]; then
  scripts/BuildTutorialExtractor.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 18 ] && [ $lastStep -ge 18 ]; then
  scripts/ExtractTutorials.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig $DtdlModelParserVersion
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 19 ] && [ $lastStep -ge 19 ]; then
  scripts/BuildTutorials.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig $DtdlModelParserVersion
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 20 ] && [ $lastStep -ge 20 ]; then
  scripts/TestTutorials.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 21 ] && [ $lastStep -ge 21 ]; then
  scripts/RunDocFx.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  unix2dos api-docs/dotnet/_site/api/* 2>/dev/null
  unix2dos api-docs/dotnet/_site/fonts/* 2>/dev/null
  unix2dos api-docs/dotnet/_site/styles/* 2>/dev/null
  unix2dos api-docs/dotnet/_site/*.svg 2>/dev/null
  unix2dos api-docs/dotnet/_site/*.json 2>/dev/null
  unix2dos api-docs/dotnet/_site/*.yml 2>/dev/null
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 22 ] && [ $lastStep -ge 22 ]; then
  scripts/BuildFlowTracer.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi

if [ $firstStep -le 23 ] && [ $lastStep -ge 23 ]; then
  scripts/TraceFlow.sh $DtdlModelParserBuildConfig $DtdlModelParserLangConfig
  test $? -eq 0 || exit $?
fi
