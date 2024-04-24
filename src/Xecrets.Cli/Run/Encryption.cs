#region Coypright and GPL License

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

#endregion Coypright and GPL License

using AxCrypt.Abstractions;
using AxCrypt.Core;
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.IO;
using AxCrypt.Core.UI;

using Xecrets.Cli.Abstractions;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Run
{
    internal sealed class Encryption : IDisposable
    {
        private IAxCryptDocument _document;

        private Stream _fromStream;

        public Encryption(Stream fromStream, IEnumerable<LogOnIdentity> identities, IEnumerable<UserPublicKey> publicKeys, IProgressContext progress)
        {
            var encryptionParameters = new EncryptionParameters(new V2Aes256CryptoFactory().CryptoId, identities.First());
            encryptionParameters.AddOrReplace(publicKeys);
            _document = New<AxCryptFactory>().CreateDocument(encryptionParameters);
            _fromStream = new ProgressStream(fromStream, progress);
        }

        public void EncryptTo(IStandardIoDataStore toStore, string originalFileName)
        {
            using (_fromStream)
            {
                using var toStream = toStore.OpenWrite();

                _document.FileName = originalFileName;
                _document.CreationTimeUtc = New<INow>().Utc;
                _document.LastAccessTimeUtc = _document.CreationTimeUtc;
                _document.LastWriteTimeUtc = _document.CreationTimeUtc;

                _document.EncryptTo(_fromStream, toStream, AxCryptOptions.EncryptWithCompression);
            }
        }

        public void Dispose()
        {
            if (_document != null)
            {
                _document.Dispose();
                _document = null!;
            }

            if (_fromStream != null)
            {
                _fromStream.Dispose();
                _fromStream = null!;
            }
        }
    }
}
