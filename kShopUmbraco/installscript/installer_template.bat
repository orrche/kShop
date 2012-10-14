

set TARGETDIR=%~1

REM xcopy /Y /M "%TargetDir%App_Code\*cshtml" "%UMBRACOPATH%App_Code\"

REM xcopy /Y /M "%TargetDir%scripts\*js" "%UMBRACOPATH%scripts\"
REM xcopy /Y /M "%TargetDir%images\*" "%UMBRACOPATH%images\"

xcopy /Y /M "%TargetDir%bin\Debug\*dll" "%UMBRACOPATH%bin\"
xcopy /Y /M "%TargetDir%macroScripts\*cshtml" "%UMBRACOPATH%macroScripts\"
