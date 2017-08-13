@echo off

:: 设置环境变量
set _devenv="%VS140COMNTOOLS%..\..\Common7\IDE\devenv.com"
if not exist %_devenv% set _devenv="%VS120COMNTOOLS%..\..\Common7\IDE\devenv.com"

cd %~dp0%

set shlib="..\SHLib\SHLib.sln"
set shlibPro="..\SHLib\SHLib\SHLib.csproj"

%_devenv% /rebuild Debug %cd%%shlib% /project %cd%%shlibPro% /projectconfig Debug
::copy %nutshell_project_dll% %projectRef%\NutShell.dll


