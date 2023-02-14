@echo off
setlocal

goto begin

:config
  echo.
  echo Error:  DTDLPATH environment variable not set.  Set DTDLPATH to root of DTDL git repository.

exit /b 1

:begin

if [%DTDLPATH%]==[] goto config

set targetFolder=%CD%

cd %DTDLPATH%
call SyncDtdl.cmd %targetFolder%

echo.
echo Synced state of generated DTDL files from %DTDLPATH%
