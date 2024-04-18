@echo off
echo.  
echo === These examples are for Windows in a DOS command prompt. For other environmennts some  
echo === adaptions are needed.  
  
echo === Assume set PATH=%%PATH%%;[Path-to-executable] 
echo === For example:  
echo === set PATH=%%PATH%%;"C:\Users\%UserName%\Documents\Visual Studio 2022\Projects\xecrets\xecrets-file-cli\src\Xecrets.Cli\bin\Debug\net8.0\"  
echo.  
echo === Ensure there is a file thfg.mp4 and a folder Photos with files 1.jpg ... 5.jpg  
echo === in the current folder, named win-x64.  
if not exist thfg.mp4 call GetThfg.cmd
echo.  
echo === Make a working copy of thfg.mp4
echo copy /b /y thfg.mp4 thfg(1).mp4  
copy /b /y thfg.mp4 thfg(1).mp4  
if errorlevel 1 goto error  
echo.    
echo === Encrypt a file thfg(1).mp4 using just a password to thfg.axx, and then wipe the original.  
rem --echo echoes the command line, facilitates reading output and may be useful for debugging.  
rem The actual --echo option is not echoed.
XecretsCli --echo --password fileSecret --encrypt-to thfg(1).mp4 thfg.axx --wipe thfg(1).mp4  
if errorlevel 1 goto error  
echo.  
echo === Decrypt a file to it's original file name, in this case thfg(1).mp4, in it's original folder  
cd ..  
XecretsCli --echo --password fileSecret --decrypt-to-folder win-x64/thfg.axx --wipe win-x64/thfg.axx  
if errorlevel 1 goto error  
echo.   
echo === Create a private key pair file xecrets@example.org-private.axx with password 'masterSecret'  
cd win-x64  
XecretsCli --echo --password masterSecret --create-key-pair xecrets@example.org xecrets@example.org-private.axx  
if errorlevel 1 goto error  
echo.  
echo === Use an environment variable to hold the master key  
echo set XFKEY=--password masterSecret  
set XFKEY=--password masterSecret  
if errorlevel 1 goto error  
echo.  
echo === Export the non-secret public key from the private key pair to xecrets@example.org-public.txt  
XecretsCli --echo --environment XFKEY --use-key-pair xecrets@example.org-private.axx --export-public-key xecrets@example.org xecrets@example.org-public.txt  
if errorlevel 1 goto error  
echo.  
echo === Encrypt the file thfg.mp4 using a password and also with a public key for xecrets@example.org  
XecretsCli --echo --password fileSecret --load-public-key xecrets@example.org-public.txt --use-public-key xecrets@example.org --encrypt-to thfg(1).mp4 thfg.axx  
if errorlevel 1 goto error  
echo.  
echo === Decrypt the file thfg.axx using the private key from the secret key pair instead of the password  
XecretsCli --echo --password masterSecret --use-key-pair xecrets@example.org-private.axx --decrypt-to thfg.axx thfg(1).mp4  
if errorlevel 1 goto error  
echo.  
echo === Compare the decrypted with the original to ensure we got back what we had  
echo fc /b thfg.mp4 thfg(1).mp4  
fc /b thfg.mp4 thfg(1).mp4  
if errorlevel 1 goto error  
echo.  
echo === Securely wipe intermediate files
XecretsCli --echo --wipe thfg(1).mp4 thfg.axx xecrets@example.org-public.txt xecrets@example.org-private.axx  
if errorlevel 1 goto error  
  
exit /b  
  
:error  
echo.  
echo ***** FAILED with error code %errorlevel%  
exit /b %errorlevel%  