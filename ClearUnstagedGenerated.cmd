@echo off
setlocal

set YN=N
set /P YN=Clear all unstaged changes and additions to generated files (Y/[N])?
if /I "%YN%" NEQ "Y" exit /b

git clean -f dotnet\tests\ParserUnitTest\generated
git restore dotnet\tests\ParserUnitTest\generated

git clean -f dotnet\src\Remodel\generated
git restore dotnet\src\Remodel\generated

git clean -f dotnet\src\Parser\generated
git restore dotnet\src\Parser\generated

git clean -f samples\projects
git restore samples\projects

git clean -f images\input\generated
git restore images\input\generated

git clean -f images\generated
git restore images\generated

git clean -f api-docs\dotnet\_site
git restore api-docs\dotnet\_site
