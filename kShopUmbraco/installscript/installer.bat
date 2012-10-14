@echo off

set INSTALLERPATH=%~1
set BINPATH=%~2

IF EXIST "%INSTALLERPATH%installscript\settings.bat" GOTO INSTALL
echo You haven't configured your settings.bat file for your system. Copy form sample
exit

:INSTALL 
copy "%INSTALLERPATH%installscript\settings.bat" + "%INSTALLERPATH%installscript\installer_template.bat" "%INSTALLERPATH%installscript\installer_run.bat" > "%INSTALLERPATH%installscript\null.txt"
"%INSTALLERPATH%installscript\installer_run.bat" "%BINPATH%"