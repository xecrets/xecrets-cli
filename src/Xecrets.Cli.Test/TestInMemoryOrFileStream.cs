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

using System.Text;

using NUnit.Framework;

using Xecrets.Cli.Run;

namespace Xecrets.Cli.Test;

[TestFixture]
internal class TestInMemoryOrFileStream
{
    [Test]
    public void SmallTextIsKeptInMemory()
    {
        FakeWriteFile file = new();
        using InMemoryOrFileStream stream = new("notes.txt", file, new Dictionary<InMemoryType, long> { { InMemoryType.Text, 100 } });

        stream.Write(Encoding.UTF8.GetBytes("Some text"));

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.EqualTo(InMemoryType.Text));
        Assert.That(file.IsOpened, Is.False);
        Assert.That(stream.Data.ToArray(), Is.EqualTo(Encoding.UTF8.GetBytes("Some text")));
    }

    [Test]
    public void TextWithoutExtensionIsKeptInMemory()
    {
        FakeWriteFile file = new();
        using InMemoryOrFileStream stream = new("README", file, new Dictionary<InMemoryType, long> { { InMemoryType.Text, 100 } });

        stream.Write(Encoding.UTF8.GetBytes("Some text"));

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.EqualTo(InMemoryType.Text));
    }

    [Test]
    public void TextOverTheLimitIsWrittenCompletelyToTheFile()
    {
        FakeWriteFile file = new();
        byte[] text = Encoding.UTF8.GetBytes(new string('a', 10000));
        using InMemoryOrFileStream stream = new("notes.txt", file, new Dictionary<InMemoryType, long> { { InMemoryType.Text, 5000 } });

        stream.Write(text.AsSpan(0, 3000));
        stream.Write(text.AsSpan(3000));

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.Null);
        Assert.That(file.ReadAllBytes(), Is.EqualTo(text));
    }

    [Test]
    public void TypeThatIsNotRequestedIsWrittenToTheFile()
    {
        FakeWriteFile file = new();
        using InMemoryOrFileStream stream = new("document.pdf", file, new Dictionary<InMemoryType, long> { { InMemoryType.Text, 100 } });

        stream.Write(Encoding.ASCII.GetBytes("%PDF-1.4"));

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.Null);
        Assert.That(file.ReadAllBytes(), Is.EqualTo(Encoding.ASCII.GetBytes("%PDF-1.4")));
    }

    [Test]
    public void WithoutInMemoryTypesEverythingGoesDirectlyToTheFile()
    {
        FakeWriteFile file = new();
        using InMemoryOrFileStream stream = new("notes.txt", file, new Dictionary<InMemoryType, long>());

        stream.Write(Encoding.UTF8.GetBytes("Some"));

        Assert.That(file.IsOpened, Is.True);

        stream.Write(Encoding.UTF8.GetBytes(" text"));

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.Null);
        Assert.That(file.ReadAllBytes(), Is.EqualTo(Encoding.UTF8.GetBytes("Some text")));
    }

    [Test]
    public void WithoutInMemoryTypesEmptyDataStillCreatesTheFile()
    {
        FakeWriteFile file = new();
        using InMemoryOrFileStream stream = new("notes.txt", file, new Dictionary<InMemoryType, long>());

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.Null);
        Assert.That(file.IsOpened, Is.True);
        Assert.That(file.ReadAllBytes(), Is.Empty);
    }

    [Test]
    public void BinaryAcceptsAnything()
    {
        FakeWriteFile file = new();
        byte[] data = [0x00, 0xFF, 0xFE, 0x01];
        using InMemoryOrFileStream stream = new("data.bin", file, new Dictionary<InMemoryType, long> { { InMemoryType.Text, 100 }, { InMemoryType.Binary, 100 } });

        stream.Write(data);

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.EqualTo(InMemoryType.Binary));
        Assert.That(stream.Data.ToArray(), Is.EqualTo(data));
    }

    [TestCase("utf-16")]
    [TestCase("utf-16BE")]
    [TestCase("utf-32")]
    [TestCase("utf-32BE")]
    [TestCase("utf-8")]
    public void TextIsConvertedToUtf8WithoutByteOrderMark(string encodingName)
    {
        Encoding encoding = Encoding.GetEncoding(encodingName);
        const string text = "Räksmörgås – 日本語";
        FakeWriteFile file = new();
        using InMemoryOrFileStream stream = new("notes.txt", file, new Dictionary<InMemoryType, long> { { InMemoryType.Text, 1000 } });

        stream.Write([.. encoding.Preamble, .. encoding.GetBytes(text)]);

        stream.Commit();
        Assert.That(stream.InMemoryType, Is.EqualTo(InMemoryType.Text));
        Assert.That(stream.Data.ToArray(), Is.EqualTo(new UTF8Encoding(false).GetBytes(text)));
    }
}
