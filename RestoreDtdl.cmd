@echo off
setlocal

goto begin

:config
  echo.
  echo Error:  DTDLPATH environment variable not set.  Set DTDLPATH to root of DTDL git repository.

exit /b 1

:begin

if [%DTDLPATH%]==[] goto config

call %DTDLPATH%\RestoreDtdl.cmd

echo.
echo Restored state of generated DTDL files
