﻿#region Copyright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2024, Svante Seleborg, All Rights Reserved.
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

using AxCrypt.Core.IO;

using Xecrets.Cli.Abstractions;

namespace Xecrets.Cli.Implementation;

internal class AsciiArmorDataStore(IStandardIoDataStore wrapped) : IStandardIoDataStore
{
    public bool IsStdin => wrapped.IsStdin;

    public bool IsStdout => wrapped.IsStdout;

    public bool IsStdIo => wrapped.IsStdIo;

    public bool IsNamedStdIo => wrapped.IsNamedStdIo;

    public string AliasName => wrapped.AliasName;

    public bool IsWriteProtected { get => wrapped.IsWriteProtected; set => wrapped.IsWriteProtected = value; }

    public bool IsEncryptable => wrapped.IsEncryptable;

    public DateTime CreationTimeUtc { get => wrapped.CreationTimeUtc; set => wrapped.CreationTimeUtc = value; }

    public DateTime LastAccessTimeUtc { get => wrapped.LastAccessTimeUtc; set => wrapped.LastAccessTimeUtc = value; }

    public DateTime LastWriteTimeUtc { get => wrapped.LastWriteTimeUtc; set => wrapped.LastWriteTimeUtc = value; }

    public IDataContainer Container => wrapped.Container;

    public bool IsAvailable => wrapped.IsAvailable;

    public bool IsFile => wrapped.IsFile;

    public bool IsFolder => wrapped.IsFolder;

    public string Name => wrapped.Name;

    public string FullName => wrapped.FullName;

    public bool IsNetworkPath => wrapped.IsNetworkPath;

    public void Delete()
    {
        wrapped.Delete();
    }

    public bool IsLocked()
    {
        return wrapped.IsLocked();
    }

    public long Length()
    {
        return wrapped.Length();
    }

    public void MoveTo(string destinationFileName)
    {
        wrapped.MoveTo(destinationFileName);
    }

    public Stream OpenRead()
    {
        return new AsciiArmorStream(wrapped.OpenRead());
    }

    public Stream OpenUpdate()
    {
        return wrapped.OpenUpdate();
    }

    public Stream OpenWrite()
    {
        return new AsciiArmorStream(wrapped.OpenWrite());
    }

    public void SetFileTimes(DateTime creationTimeUtc, DateTime lastAccessTimeUtc, DateTime lastWriteTimeUtc)
    {
        wrapped.SetFileTimes(creationTimeUtc, lastAccessTimeUtc, lastWriteTimeUtc);
    }
}