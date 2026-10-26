#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2025 Svante Seleborg, All Rights Reserved.
 *
 * This code file is part of Xecrets Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 *
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets Cli is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets Cli is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Copyright and GPL License

using AxCrypt.Abstractions;
using AxCrypt.Core.Runtime;
using AxCrypt.Fake;

using NUnit.Framework;

using Xecrets.Cli.Implementation;
using Xecrets.Cli.Log;
using Xecrets.Cli.Operation;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Test;

[TestFixture]
internal class TestInMemoryOperation
{
    [SetUp]
    public void SetUp()
    {
        TypeMap.Register.Singleton<IRuntimeEnvironment>(() => new FakeRuntimeEnvironment());
    }

    [Test]
    public async Task ValidOptionsAreAdded()
    {
        using CancelSignal cancelSignal = new();
        using Parameters parameters = CreateParameters("XecretsCli --in-memory=text:100 --in-memory=binary:200", cancelSignal);
        InMemoryOperation operation = new();

        foreach (ParsedOp op in parameters.Parser.ParsedOps)
        {
            parameters.CurrentOp = op;
            Assert.That((await operation.DryAsync(parameters)).IsSuccess);
        }

        Assert.That(parameters.InMemory, Is.EquivalentTo(new Dictionary<InMemoryType, long> { { InMemoryType.Text, 100 }, { InMemoryType.Binary, 200 } }));
    }

    [TestCase("text")]
    [TestCase("text:")]
    [TestCase("text:-1")]
    [TestCase("text:1:2")]
    [TestCase("Text:100")]
    [TestCase("audio:100")]
    [TestCase("1:100")]
    public async Task InvalidOptionsAreRejected(string option)
    {
        using CancelSignal cancelSignal = new();
        using Parameters parameters = CreateParameters($"XecretsCli --in-memory={option}", cancelSignal);
        parameters.CurrentOp = parameters.Parser.ParsedOps.Single();

        Status status = await new InMemoryOperation().DryAsync(parameters);

        Assert.That(status.StatusCode, Is.EqualTo(XfStatusCode.InvalidOption));
        Assert.That(parameters.InMemory, Is.Empty);
    }

    private static Parameters CreateParameters(string commandLine, CancelSignal cancelSignal)
    {
        CliServices cliServices = new(
            coreServices: null!,
            timeProvider: TimeProvider.System,
            cancelSignal: cancelSignal,
            consoleOut: new ConsoleOut(TextWriter.Null),
            splash: null!,
            buildUtc: null!,
            license: null!,
            licenseCandidates: null!,
            licenseLocator: null!,
            desktopServices: null!,
            inUseBy: null!,
            shamirsSecretSharing: null!);
        return new Parameters(new OptionsParser(commandLine), cliServices);
    }
}
