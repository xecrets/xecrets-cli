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

using Xecrets.Core.Desktop.Abstractions;

namespace Xecrets.Cli.Test;

internal sealed class FakeWriteFile : IFile
{
    public MemoryStream Written { get; } = new();

    public bool IsOpened { get; private set; }

    public bool IsStdout => false;

    public bool IsStdin => false;

    public bool IsStdIo => false;

    public bool IsNamedStdIo => false;

    public bool IsEncryptable => false;

    public bool IsAvailable => IsOpened;

    public bool IsSymbolicLink => false;

    public string AliasName => Name;

    public string Name => "fake.txt";

    public string FullName => Name;

    public DateTime CreationTimeUtc => throw new NotSupportedException();

    public DateTime LastAccessTimeUtc => throw new NotSupportedException();

    public DateTime LastWriteTimeUtc => throw new NotSupportedException();

    public IFolder ParentFolder => throw new NotSupportedException();

    public long Length => Written.Length;

    public Stream OpenRead() => throw new NotSupportedException();

    public Stream OpenWrite()
    {
        IsOpened = true;
        return Written;
    }

    public void SetFileTimes(DateTime creationTimeUtc, DateTime lastAccessTimeUtc, DateTime lastWriteTimeUtc) => throw new NotSupportedException();

    public void DeleteIfAvailable() => throw new NotSupportedException();

    public byte[] ReadAllBytes() => Written.ToArray();
}
