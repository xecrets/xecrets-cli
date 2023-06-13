XecretsFileCli - Xecrets File Command Line Documentation

NAME
====

XecretsFileCli - Cross Platform AxCrypt Compatible Command Line Tool 

SYNOPSIS
========

XecretsFileCli [--use-public-key _email(s)_] [--create-key-pair _email_
_encrypted_] [--decrypt-to _encrypted_ _clear_] [--encrypt-to _clear_
_encrypted_] [--file _name_] [--gpl-license] [--help] [--id] [--json-log]
[--use-key-pair _encrypted_] [--decrypt-to-folder _encrypted_ [_folder_]]
[--environment _variable_] [--progress] [--password _secret_] [--quiet]
[--dryrun] [--stdout] [--text-log] [--load-public-key _file(s)_] [--overwrite]
[--wipe _file_] [--export-public-key _email_ _file_] [--platform] [--echo]
[--debug-break] [--debug-break-parse] [--argument-markdown]
[--cli-options-code-export] [--cli-version] [--internal] [--jwt-audience
_audience_] [--jwt-claims _expiration_ _claims_] [--jwt-create-key-pair
_private-pem_ _public-pem_] [--jwt-issuer _issuer_] [--jwt-sign _signed-jwt_]
[--jwt-private-key _private-pem_] [--jwt-verify _public-pem_ _signed-jwt_]
[--license _jwt-license_]

DESCRIPTION
===========

Encrypts, decrypts, creates public key pairs, imports key pairs and public keys and securely wipes files.

Options
-------
-b|--use-public-key _email(s)_
:       Use selected loaded public key(s) for encryption.
-c|--create-key-pair _email_ _encrypted_
:       Create a key pair for an email moniker, in an encrypted file.
-d|--decrypt-to _encrypted_ _clear_
:       Decrypt a file to the given file path.
-e|--encrypt-to _clear_ _encrypted_
:       Encrypt a file to the given file path.
-f|--file _name_
:       Take options from a file.
-g|--gpl-license 
:       Display the full GNU GPL license.
-h|-?|--help 
:       Display this helpful help message, use again for details.
-i|--id 
:       Arbitrary id which is returned in JSON-logging.
-j|--json-log 
:       Enable JSON-based logging.
-k|--use-key-pair _encrypted_
:       Use a key-pair, from an encrypted file path. Password is required.
-l|--decrypt-to-folder _encrypted_ [_folder_]
:       Decrypt a file path with it's original name to a destination folder.
        If _folder_ is not provided, the _encrypted_'s folder will be used.
-n|--environment _variable_
:       Take options from environment variable.
-o|--progress 
:       Continuously log progress.
-p|--password _secret_
:       A password to use for encryption or decryption.
-q|--quiet 
:       Do not display any messages or progress (global).
-r|--dryrun 
:       Only perform a dry run without actually modifying anything (global).
-s|--stdout 
:       Write log output to stdout instead of stderr (global).
-t|--text-log 
:       Enable text-based console log.
-u|--load-public-key _file(s)_
:       Load public key(s) from file(s).
-v|--overwrite 
:       Overwrite files instead of using alternate target name (global).
-w|--wipe _file_
:       Securely wipe and delete a file.
-x|--export-public-key _email_ _file_
:       Export a public key.
--platform 
:       Display the platform the program is running on.
--echo 
:       Display the command line received by the program.
--debug-break 
:       Break into debugger when executing this argument.
--debug-break-parse 
:       Break into debugger when parsing this argument.
--argument-markdown 
:       Display help texts as markdown.
--cli-options-code-export 
:       Display C# source code for options.
--cli-version 
:       Display the CLI API version.
--internal 
:       Display help for internal use commands and disable splash (global).
--jwt-audience _audience_
:       Set audience email for JWT.
--jwt-claims _expiration_ _claims_
:       Set days until expiration and claims JSON.
--jwt-create-key-pair _private-pem_ _public-pem_
:       Create JWT keypair as PEM files.
--jwt-issuer _issuer_
:       Set issuer email for JWT.
--jwt-sign _signed-jwt_
:       Sign and write JWT to file.
--jwt-private-key _private-pem_
:       Use a private key PEM file for signing.
--jwt-verify _public-pem_ _signed-jwt_
:       Verify a signed JWT file using a public PEM file.
--license _jwt-license_
:       Use this license. Overrides any others found (global).

Options are processed in order and may appear multiple times, except when
'global'. Arguments such as _email_ are required placeholders. Flags may be
negated by suffixing with '-'.; Single letter flags may be bundled together.
'-', '--' or '/' are allowed as option prefixes. Quote special characters with a
single backslash (\\), and enclose single values that contain spaces with quotes
(").

Since options are accepted from the command line, from environment variables,
and from files, as well as in different operating systems the native command
line parsing is not used, instead the command line is taken as a whole and
parsed internally into discrete arguments. Although the command line treatment
of arguments is similar across Windows, Linux and OS X, there are various
differences. Therefore it is parsed internally with consistent rules.
Essentially only double quotes to contain spaces in an argument, and escape with
a backslash is supported. When reading arguments from a source other than the
command line, command line features such as environment variable expansion and
other injections of data into the command are not supported. Options specified
in environment variables and files will behave consistently across all supported
platforms.

COMPATIBILITY
=============

This software is backwards and forward compatible with AxCrypt 2.x, at the time
of publication. If AxCrypt implements some breaking changes in the future, this
may no longer hold true - but in this case, all older versions of AxCrypt before
the breaking change suffer the same problem.

Xecrets File Command Line encrypts data with AxCrypt version 2 format, using
AES-256 encryption always. It can still decrypt AxCrypt version 1 files, as well
as AES-128 encrypted version 2 files.

FILES
=====

*Encrypted Files* - *.axx

:   Encrypted files are files that have been encryped using one or more
passwords and optionally one or more public keys. They traditionally have a
".axx" file extension, but this is not a requirement.

*Private key pair files* - email@example.org-private.axx

:   Key pair files are encrypted files that contain both a private key which is
secret, and the matching public key. A private key can be used to decrypt files
encrypted with the public key. A private key pair file is essentially a text
file that is encrypted, and thus typically have a ".axx" extension. We recommend
naming them as "someone@example.org-private.axx".

*Public key files* - email@example.org-public.txt

:   Public key files are non-secret text files that can be sent openly to
anyone, and then used to encrypt files that the owner of the corresponding
private key can open. We recommend naming them as
"someone@example.org-public.txt".

FILE NAMES
==========

Encrypted files carry the original file name with them inside the encrypted
container, with the intention that when decryption is done, the original name
can be restored without depending on the name of the encrypted file. This also
allows the protection by encryption of the original name, since the encrypted
file can be named something that does not give away any information which the
original name often does.

This can cause some problems when moving files between operating system, where
different name rules apply, and also some characters are very difficult to use
in some operating systems.

Our recommendation is therefore to name files safely, which means avoiding most
special characters. To be really safe, use only: 0-9 a-z A-Z - (dash) . (dot) _
(underscore) . Other characters will work in most cases, but your mileage may
vary. Do try it out in your intended environments before wide scale use.

Standard IO Aliases
-------------------

Xecrets File supports special aliases for the standard input and standard output
stream respectively where a path to a file may occur.

Standard Input: - (dash)
Standard Output: + (plus)

Using one of these aliases in place of a path will cause Xecrets File to read or
write to a standard IO stream instead.

A special situation arises when encrypting from standard input, as an encrypted file
always contains the orginal file name in encrypted form in the file. To specify
the name to use in this case, use the following syntax:

-:_FileName_

I.e. the dash for standard input, followed by a : (colon), followed by the name
to use.

BASIC CONCEPTS
==============

Xecrets File Command Line encrypts files using at least a password, and
optionally with one or more public keys.

With Xecrets File, a key pair can be created with --create-key-pair, and the
public part can be extracted and shared with other people, for example it can be
published on a web site, or distributed in any other way. It is a clear text
plain text file that is not secret.

The public key is typically used to encrypt data for someone else without
sharing any secret information. All that is required is that the recipient of
the encrypted data provides the sender with their public key in such a file.

It can also be used as a key recovery mechanism, if all encryption is performed
using such a public key, the holder of the private key can always decrypt the
file without known the password used by the encrypting party. This is how
AxCrypt implements key recovery.

Finally, the recommendation is that encryption is never done only with a
password, but always with the encrypting partys personal public key. If the
personal key pair is kept safe, and accessible, the files can then be decrypted
using the private key part of the key pair.

LOGGING AND MESSAGES
====================

Progress, informative messages and error messages may be written in either text
format for humans, or in JSON to be interpreted by scripts or software.

Use --text-log or --json-log respectivey. If none is provided, very little
output is written. To make the software entirely silent, use --quiet.

All messages are always written to the stderr console output stream, unless
explicitly redirected to standard output with --stdout .

ENCRYPTION
==========

To encrypt a file, a --password must be supplied. If more are given, only the
first is used for encryption, although the others may be used to attempt
decryption.

If a key pair is supplied via --use-key-pair, the file is also encrypted using
that key pairs public key.

If a list of public keys is given with --load-public-key, and --use-public-key
with an email contained in the list is also given, the file will be encrypted
using that public key as well.

PASSWORD BASED ENCRYPTION
=========================

Password based encryption is performed using the PBKDF2-HMACS-HA512 key
derivation function with 1,000 iterations. This derived key is then used
in the NIST AES Key Wrap algorithm to actually protect the file master encryption key.

The advantage of password based encryption is that it's the only thing that
needs to be kept secret, and it can be kept in memory, a piece of paper or
preferrably a password manager. As long as you know the password, and have
access to or are able to create appropriate software then the original data is
recoverable.

It's also more complicated to use when sharing encrypted files with others, as a
secure channel is need to send the shared secret password and also there's a
need then to keep track of multiple passwords.

For more details, see AxCryptVersion2AlgorithmsandFileFormat.pdf .

PUBLIC KEY BASED ENCRYPTION
===========================

Public key based encryption is done using RSA-4096, where the encryption key and
the decryption key is split into two parts. The encryption, or public, key does
not need to be secret and can be sent to anyone or even published. The
decryption, or private, key needs to be kept secret, and this is typically done
by encrypting it in turn as Xecrets File-encrypted file with a password.

For more details, see AxCryptVersion2AlgorithmsandFileFormat.pdf .

ENVIRONMENT
===========

_ANY VARIABLE_

:   Any environment variable can contain options, which is typically used to
provide passwords to the command line without exposing them visibly or in logs.
Use --environment _variable_ to read options from an environment variable.

EXAMPLES
========

    @echo off
    echo.  
    echo === These examples are for Windows in a DOS command prompt. For other environmennts some  
    echo === adaptions are needed.  
      
    echo === Assume set PATH=%%PATH%%;[Path-to-executable] 
    echo === For example:  
    echo === set PATH=%%PATH%%;"C:\Users\%UserName%\Documents\Visual Studio 2022\Projects\xecrets-file-cli\src\Xecrets.File.Cli\bin\Debug\net7.0\"  
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
    XecretsFileCli --echo --password fileSecret --encrypt-to thfg(1).mp4 thfg.axx --wipe thfg(1).mp4  
    if errorlevel 1 goto error  
    echo.  
    echo === Decrypt a file to it's original file name, in this case thfg(1).mp4, in it's original folder  
    cd ..  
    XecretsFileCli --echo --password fileSecret --decrypt-to-folder win-x64/thfg.axx --wipe win-x64/thfg.axx  
    if errorlevel 1 goto error  
    echo.   
    echo === Create a private key pair file xecrets@example.org-private.axx with password 'masterSecret'  
    cd win-x64  
    XecretsFileCli --echo --password masterSecret --create-key-pair xecrets@example.org xecrets@example.org-private.axx  
    if errorlevel 1 goto error  
    echo.  
    echo === Use an environment variable to hold the master key  
    echo set XFKEY=--password masterSecret  
    set XFKEY=--password masterSecret  
    if errorlevel 1 goto error  
    echo.  
    echo === Export the non-secret public key from the private key pair to xecrets@example.org-public.txt  
    XecretsFileCli --echo --environment XFKEY --use-key-pair xecrets@example.org-private.axx --export-public-key xecrets@example.org xecrets@example.org-public.txt  
    if errorlevel 1 goto error  
    echo.  
    echo === Encrypt the file thfg.mp4 using a password and also with a public key for xecrets@example.org  
    XecretsFileCli --echo --password fileSecret --load-public-key xecrets@example.org-public.txt --use-public-key xecrets@example.org --encrypt-to thfg(1).mp4 thfg.axx  
    if errorlevel 1 goto error  
    echo.  
    echo === Decrypt the file thfg.axx using the private key from the secret key pair instead of the password  
    XecretsFileCli --echo --password masterSecret --use-key-pair xecrets@example.org-private.axx --decrypt-to thfg.axx thfg(1).mp4  
    if errorlevel 1 goto error  
    echo.  
    echo === Compare the decrypted with the original to ensure we got back what we had  
    echo fc /b thfg.mp4 thfg(1).mp4  
    fc /b thfg.mp4 thfg(1).mp4  
    if errorlevel 1 goto error  
    echo.  
    echo === Securely wipe intermediate files
    XecretsFileCli --echo --wipe thfg(1).mp4 thfg.axx xecrets@example.org-public.txt xecrets@example.org-private.axx  
    if errorlevel 1 goto error  
      
    exit /b  
      
    :error  
    echo.  
    echo ***** FAILED with error code %errorlevel%  
    exit /b %errorlevel%


BUGS
====

See GitHub Issues: <https://github.com/xecrets/xecrets-file-cli/issues>

ACKNOWLEDGEMENTS
================

This program uses code from AxCrypt to perform high level cryptographic
operations on streams and files, and is licensed under GNU GPL version 3. See
https://www.axcrypt.net/ for details.

This program uses code from the Legion of the Bouncy Castle, Copyright (c) 2000
\- 2017 The Legion of the Bouncy Castle Inc. (http://www.bouncycastle.org), to
perform low level public key cryptography and is licensed under an adaptation of
the MIT X11 License, see https://www.bouncycastle.org/csharp/licence.html for
details.

This program uses code from NDeks.Options to parse the command line and is
licensed under the MIT/X11 license, see http://www.ndesk.org/Options for
details.

AxCrypt is a registered trademark of AxCrypt AB, and is only used for
informational purposes.

AUTHOR
======

Svante Seleborg / Axantum Software AB <support@axantum.com> . Note that the code
used from AxCrypt is with minor exceptions also originally written by Svante
Seleborg / Axantum Software AB.

SEE ALSO
========

https://www.axantum.com https://www.axcrypt.net

TIP
===

Write to stdout, paginate with 'more': --stdout --help --help --help | more