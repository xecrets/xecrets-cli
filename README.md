# README

Xecrets Cli 2.x - A Cross Platform AxCrypt compatible Open Source Encryption command line
tool in C# for .NET 8+

Xecrets Cli (aka "the command line", "the command line tool", or "the CLI") is built on top of
[AxCrypt 2.1.x](https://github.com/axantum/xecrets-net) as released under GNU GPL version 3 or later
by AxCrypt AB.

As we're using the original code from AxCrypt, Xecrets Cli is 100% compatible with
AxCrypt.

The motivation for providing Xecrets Cli is to once again provide the community with a free
truly open source encryption and decryption command line tool exposing the functionality in a way
easily consumable by scripts and code as well as humans. We also believe it's time to get back to
basics, and remove a lot of the complexity present in AxCrypt with it's server integration, business
license handling, and an overly ambitious user interface.

Xecrets Cli does not in any way communicate with any server or other infrastructure over the
Internet. It is entirely run and executed locally on your system.

Starting with Xecrets Cli, we've since released a [nuget SDK
package](https://www.nuget.org/packages/Xecrets.Sdk/) for direct integration with .NET code, as well
as radically simplified desktop application [Xecrets Ez](https://www.axantum.com/xecrets-ez).

## Command Line Arguments

Xecrets Cli is intended to be called from the command line by humans, scripts or code. It has a
large number of options, making it a very powerful toolbox, but the basic usage is still very
simple. Check out the [documentation](docs/index.md "Command Line Tool Options") for details and
examples.

## Quick Start

To encrypt a file:

`XecretsCli --password xecret --encrypt-to Document.txt Document-txt.axx`

To decrypt a file:

`XecretsCli --password xecret --decrypt-to Document-txt.axx Document.txt`

There are numerous other options and features such as generating and using public key pairs, JSON
output logging for programmatic use, wiping files, passing options from files or via environment
variables and more. See the full help with:

`XecretsCli --stdout --help --help --help | more`

## Maintenance Subscription for Axantum Builds

If you're using a build published by us, there are a few restrictions unless you buy a maintenance
subscription. Most features are free even there, but some features intended for programmatic use
such as JSON logging and taking options from files and environment variables will restrict the size
of files possible to encrypt to 1 MB if you don't have a maintenance subscription valid for the
build. You can always decrypt any size of file.

If you're doing your own GPL build from the sources, no restrictions apply.

## Software Development Kit

To call Xecrets Cli from a .NET application as a .NET library, use the SDK which is available
as [nuget package Xecrets.Sdk](https://www.nuget.org/packages/Xecrets.Sdk). It comes with
[intellisense documentation](sdk-docs/index.md "The SDK API").

## Support Development

If you represent a business, please purchase your maintenance subscription for the build from us at
https://www.axantum.com/ or if you're a private individual, get and build it from source but if you'd
like to support us, do purchase a premium subscription for the desktop app. It's also very useful.

Xecrets Cli is free software, licensed under the GNU GPL Version 3 or later license. This means
you can use it anywhere and any way you like for free, and you are also free to modify it as you
wish as long as you do not redistribute it. If you do redistribute it, please check with the Free
Software Foundation how this works, https://www.gnu.org/licenses/ .

Naturally this also applies to us, so we're not requiring you to pay anything to use the Xecrets Cli
software.

However, we do spend time and money to develop, maintain and distribute the software for you, so if
you are a business and are using the business-oriented features for programmatic integration of the
software, there _is_ a maintenance subscription required for use of the ready-built and tested
software from https://www.axantum.com/ .

Nothing prevents you from downloading [the source code](https://github.com/xecrets/xecrets-cli)
from github where we keep the code, and building it yourself. It's maybe even a good idea to try it
out. But in the long run, by getting the ready-built software from us your project is spared all the
time to keep your tooling updated, building, updating the source code etc. We believe we provide a
low cost service that is worth it for any business using our software!

### Xecrets Cli 2.x status

The software is still in beta, API:s may break and options change at any moment. Use with care.

### The AxCrypt fork xecrets-net

The only modifications we've done are to update the code to work with .NET 8 and compile with Visual
Studio 2022, keep dependencies updated, a few minor bugfixes and finally another very few minor
changes to be able to work well with the command line tool code. No changes to the core cryptography
has been made.

### How To Build?

Download the [xecrets-cli](https://github.com/xecrets/xecrets-cli) and
[xecrets-net](https://github.com/axantum/xecrets-net) repositories side by side. The solution
expects to find the source code for the original, slightly modified, AxCrypt there. Open the Xecrets
Cli solution in Visual Studio or the workspace in Visual Studio Code and build. There are no
external dependencies that are not resolved with Nuget.

Unit tests require a NUnit-compatible unit test runner.

### How to Contribute

Talk to us. Due to the nature of the application, pull requests are audited very
carefully. Before requesting a pull it's best if we discuss things.

Minimum requirement is that there are no compiler warnings.

### Contact

Contact us via our [support](https://www.axantum.com/support "Xecrets Support
Site") or through github .
