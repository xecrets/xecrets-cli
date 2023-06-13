@echo off
pushd %~dp0
if not exist Photos mkdir Photos
if not exist Photos\1.jpg copy ..\Photos\1.jpg Photos\1.jpg
if not exist Photos\2.jpg copy ..\Photos\2.jpg Photos\2.jpg
if not exist Photos\3.jpg copy ..\Photos\3.jpg Photos\3.jpg
if not exist Photos\4.jpg copy ..\Photos\4.jpg Photos\4.jpg
if not exist Photos\5.jpg copy ..\Photos\5.jpg Photos\5.jpg
if not exist thfg.mp4 call GetThfg.cmd
popd
"%~dp0\..\..\bin\Release\net6.0\publish\win-x64\XecretsFileCli.exe" %*