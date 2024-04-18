#region Coypright and GPL License

/*
 * Xecrets Cli - Copyright © 2022-2023, Svante Seleborg, All Rights Reserved.
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

#endregion Coypright and GPL License

using AxCrypt.Abstractions;
using AxCrypt.Core.IO;
using AxCrypt.Core.Runtime;
using AxCrypt.Mono;
using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Implementation
{
    internal class StandardIoDataStore : IStandardIoDataStore
    {
        private readonly DataStore _wrapped;

        public bool IsStdin { get; }

        public bool IsStdout { get; }

        public bool IsStdIo => IsStdin || IsStdout;

        public bool IsNamedStdIo => IsStdIo && _aliasName.Length > 0;

        private readonly string _aliasName = string.Empty;

        public string AliasName => _aliasName.Length > 0 ? _aliasName : Name;

        public StandardIoDataStore(string path)
        {
            if (path.Length == 0)
            {
                throw new ArgumentException("Path is empty", nameof(path));
            }

            IsStdin = path == XfNameOf.StdinAlias || path.StartsWith(XfNameOf.StdinAlias + XfNameOf.StdIoNameSeparator);
            IsStdout = path == XfNameOf.StdoutAlias || path.StartsWith(XfNameOf.StdoutAlias + XfNameOf.StdIoNameSeparator);

            string[] nameAndAlias = path.Split(XfNameOf.StdinAlias + XfNameOf.StdIoNameSeparator);
            if (nameAndAlias.Length != 2)
            {
                nameAndAlias = path.Split(XfNameOf.StdoutAlias + XfNameOf.StdIoNameSeparator);
            }

            string adjustedPath = path;
            if (nameAndAlias.Length == 2)
            {
                adjustedPath = nameAndAlias[0];
                _aliasName = nameAndAlias[1];
            }

            if (IsStdIo)
            {
                adjustedPath = path.Substring(0, 1);
            }

            static void ValidatePath(string path)
            {
                if (Path.GetFileName(path).Any(Path.GetInvalidFileNameChars().Contains))
                {
                    throw new ArgumentException("{0} is not a valid filename.".Format(Path.GetFileName(path)));
                }

                if (path.Any(Path.GetInvalidPathChars().Contains))
                {
                    throw new ArgumentException("{0} is not a valid path.".Format(path));
                }
            }

            ValidatePath(adjustedPath);
            _wrapped = new DataStore(adjustedPath);

            ValidatePath(AliasName);
        }

        public bool IsWriteProtected
        {
            get
            {
                return IsStdin || !IsStdout && _wrapped.IsWriteProtected;
            }
            set
            {
                if (!IsStdIo)
                {
                    _wrapped.IsWriteProtected = value;
                }
            }
        }

        public bool IsEncryptable => IsStdin || _wrapped.IsEncryptable;

        private readonly DateTime _utcNow = New<INow>().Utc;

        public DateTime CreationTimeUtc
        {
            get
            {
                return IsStdIo ? _utcNow : _wrapped.CreationTimeUtc;
            }
            set
            {
                if (!IsStdIo)
                {
                    _wrapped.CreationTimeUtc = value;
                }
            }
        }

        public DateTime LastAccessTimeUtc
        {
            get
            {
                return IsStdIo ? _utcNow : _wrapped.LastAccessTimeUtc;
            }
            set
            {
                if (!IsStdIo)
                {
                    _wrapped.LastAccessTimeUtc = value;
                }
            }
        }

        public DateTime LastWriteTimeUtc
        {
            get
            {
                return IsStdIo ? _utcNow : _wrapped.LastWriteTimeUtc;
            }
            set
            {
                if (!IsStdIo)
                {
                    _wrapped.LastWriteTimeUtc = value;
                }
            }
        }

        public IDataContainer Container => !IsStdIo ? _wrapped.Container : throw new FileOperationException("Cannot get the parent container of a standard IO stream.", _wrapped.Name, ErrorStatus.InvalidPath);

        public bool IsAvailable => IsStdIo || _wrapped.IsAvailable;

        public bool IsFile => true;

        public bool IsFolder => false;

        public string Name => _wrapped.Name;

        public string FullName => _wrapped.FullName;

        public bool IsNetworkPath => !IsStdIo && _wrapped.IsNetworkPath;

        public void Delete()
        {
            if (!IsStdIo)
            {
                _wrapped.Delete();
            }
        }

        public bool IsLocked()
        {
            return !IsStdIo && _wrapped.IsLocked();
        }

        public long Length()
        {
            return IsStdIo ? 0 : _wrapped.Length();
        }

        public void MoveTo(string destinationFileName)
        {
            if (IsStdIo)
            {
                throw new FileOperationException("Cannot move a standard IO stream.", _wrapped.Name, ErrorStatus.InvalidPath);
            }
        }

        public Stream OpenRead()
        {
            if (IsStdout)
            {
                throw new FileOperationException("Cannot read the standard output stream.", _wrapped.Name, ErrorStatus.InvalidPath);
            }
            return IsStdin ? New<RewindableStdinStream>() : _wrapped.OpenRead();
        }

        public Stream OpenUpdate()
        {
            if (IsStdIo)
            {
                throw new FileOperationException("Cannot read/write a standard IO stream.", _wrapped.Name, ErrorStatus.InvalidPath);
            }
            return _wrapped.OpenUpdate();
        }

        public Stream OpenWrite()
        {
            if (IsStdin)
            {
                throw new FileOperationException("Cannot write the standard input stream.", _wrapped.Name, ErrorStatus.InvalidPath);
            }
            return IsStdout ? Console.OpenStandardOutput() : _wrapped.OpenWrite();
        }

        public void SetFileTimes(DateTime creationTimeUtc, DateTime lastAccessTimeUtc, DateTime lastWriteTimeUtc)
        {
            if (!IsStdIo)
            {
                _wrapped.SetFileTimes(creationTimeUtc, lastAccessTimeUtc, lastWriteTimeUtc);
            }
        }
    }
}
