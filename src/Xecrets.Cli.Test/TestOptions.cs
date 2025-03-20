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

using Xecrets.Cli.Public;

namespace Xecrets.Cli.Test;

[TestFixture]
internal class TestOptions
{
    [SetUp]
    public void SetUp()
    {
        TypeMap.Register.Singleton<IRuntimeEnvironment>(() => new FakeRuntimeEnvironment());
    }

    [Test]
    public void TestPasswordWithEncryptOption()
    {
        var parser = new OptionsParser("XecretsCli --password secretStuff --encrypt-file-to from.txt to.axx");
        Assert.That(parser.ParseStatus, Is.EqualTo(Status.Success));
        Assert.That(parser.ParseStatus.IsSuccess);

        ParsedOp[] allParsed = parser.ParsedOps.ToArray();

        Assert.That(allParsed.Length, Is.EqualTo(2));

        Assert.That(allParsed[0].OpCode, Is.EqualTo(XfOpCode.Password));
        Assert.That(allParsed[0].Arguments.Count, Is.EqualTo(1));
        Assert.That(allParsed[0].Arguments[0], Is.EqualTo("secretStuff"));

        Assert.That(allParsed[1].OpCode, Is.EqualTo(XfOpCode.EncryptTo));
        Assert.That(allParsed[1].Arguments.Count, Is.EqualTo(2));
        Assert.That(allParsed[1].Arguments[0], Is.EqualTo("from.txt"));
        Assert.That(allParsed[1].Arguments[1], Is.EqualTo("to.axx"));
    }

    [Test]
    public void TestUnknownArgumentTreatedAsExtra()
    {
        var parser = new OptionsParser("XecretsCli --unknown blabla");
        Assert.That(parser.ParseStatus.StatusCode, Is.EqualTo(XfStatusCode.ExtraArguments));
    }

    [Test]
    public void TestTooManyPasswordsOption()
    {
        var parser = new OptionsParser("XecretsCli --password secretStuff moreStuff --encrypt-file-to from.txt to.axx");
        Assert.That(parser.ParseStatus.StatusCode, Is.EqualTo(XfStatusCode.ExtraArguments));
    }

    [Test]
    public void TestQuiet()
    {
        OptionsParser parser;
        
        parser = new OptionsParser("XecretsCli --quiet");
        Assert.That(parser.ParseStatus.IsSuccess);

        Assert.That(parser.IsQuiet, Is.True);

        parser = new OptionsParser("XecretsCli --quiet+");
        Assert.That(parser.IsQuiet, Is.True);

        Assert.That(parser.IsQuiet, Is.True);

        parser = new OptionsParser("XecretsCli --quiet-");
        Assert.That(parser.IsQuiet, Is.False);
    }

    [Test]
    public void TestUnknownOption()
    {
        // --password a --text-progress --encrypt-as big.aax big.axx
        OptionsParser parser;

        parser = new OptionsParser("XecretsCli --password a --text-log --environment AVALUE big.axx");
        Assert.That(parser.ParseStatus.IsSuccess, Is.False);

        Assert.That(parser.Extra.Count, Is.EqualTo(1));
    }
}
