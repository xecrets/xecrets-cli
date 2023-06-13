# README #

Xecrets File Cli 2.3.x - Cross Platform Open Source Encryption in C# for .NET
7+

Xecrets File Cli is built on top of AxCrypt 2.1.x as released under GNU GPL
version 3 or later by AxCrypt AB.

Our forked repository of AxCrypt 2.1.x, xecrets-net, must be checked out
side-by-side with the Xecrets File Cli repository xecrets-file-cli, since the
solution expects to find the source code for the original, slightly modified,
AxCrypt there.

As we're using the original code from AxCrypt, Xecrets File Cli is 100%
compatible with AxCrypt. The only modifications we've done are to update the
code to work with .NET 7 and compile with Visual Studio 2022, a very few minor
bugfixes and finally another very few minor changes to be able to work well with
the command line interface code. No changes to the core cryptography has been
made.

The motivation for providing Xecrets File Cli is to once again provide
the community with a true free open source encryption and decryption command
line tool exposing the functionality in a way easily consumable by scripts, code
as well as humans. We also believe it's time to get back to basics, and remove a
lot of the complexity present in AxCrypt with it's server integration,
commercial license handling, and an overly ambitious user interface.

Starting with Xecrets File Cli, we're planning to add a nuget SDK for direct
integration with .NET code, as well as radically simplified desktop application
Xecrets File Ez (albeit with less functionality).

Xecrets File Cli does not in any way communicate with any server or
other infrastructure over the Internet. It is entirely run and executed locally
in your system.

## Support Development ##

Please purchase your ready-built download from us at https://www.axantum.com/ !

Xecrets File Cli is free software, licensed under the GNU GPL Version 3 or
later license. This means you can use it anywhere and any way you like for free,
and you are also free to modify it as you wish as long as you do not
redistribute it. If you do redistribute it, please check with the Free Software
Foundation how this works, https://www.gnu.org/licenses/ .

Of course this also applies to us, so we're not requiring you to pay anything to
use Xecrets File Cli.

However, we do spend time and money to develop, maintain and distribute the
software for you, so there *is* a charge for downloading the ready-built
software from https://www.axantum.com/ .

Please support our time and effort by purchasing a download!

Of course, nothing prevents you from downloading the source code from github
where we keep the code, and building it yourself. It's maybe even a good idea to
try it out. But in the long run, by getting the ready-built software from us
you're spared all the time to keep your tooling updated, building, updating the
source code etc. We believe we provide a low cost service that is worth it!

### Xecrets File Cli 2.3.x ###

Although the software is in release status, API:s may break at any moment. Use
with care.

### How To Build? ###

Download the xecrets-file-cli and xecrets-net repositories side by side and open
the Xecrets File Cli solution in Visual Studio or the workspace in Visual Studio
Code. There are no external dependencies that are not resolved with Nuget.

Unit tests require a NUnit-compatible unit test runner.

### How to Contribute ###

Talk to us. Due to the nature of the application, pull requests are audited very
carefully. Before requesting a pull it's best if we discuss things.

Minimum requirements is that Code Analysis passes without warnings, as well as
compilation.

### Contact ###

Contact us via support@axantum.com or through github .