#region Copyright and GPL License

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

using AxCrypt.Core;
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.Header;
using AxCrypt.Core.IO;
using AxCrypt.Core.Reader;
using AxCrypt.Core.UI;

using Xecrets.Cli.Abstractions;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Run;

internal sealed class Decryption(Stream fromStream, IEnumerable<LogOnIdentity> identities, IProgressContext progress) : IDisposable
{
    private IAxCryptDocument _document = CreateDocument(identities, new ProgressStream(fromStream, progress));

    public bool HasValidPassphrase => _document.PassphraseIsValid;

    public string OriginalFileName => _document.FileName;

    public void DecryptTo(IStandardIoDataStore toStore)
    {
        try
        {
            using var toStream = toStore.OpenWrite();
            _document.DecryptTo(toStream);
        }
        catch (Exception)
        {
            if (!toStore.IsStdIo && toStore.IsAvailable)
            {
                using var destinationLock = New<FileLocker>().Acquire(toStore);
                new AxCryptFile().Wipe(destinationLock, new ProgressContext());
            }
            throw;
        }

        if (!toStore.IsStdIo)
        {
            toStore.SetFileTimes(_document.CreationTimeUtc, _document.LastAccessTimeUtc, _document.LastWriteTimeUtc);
        }
    }

    private static IAxCryptDocument CreateDocument(IEnumerable<LogOnIdentity> identities, Stream fromStream)
    {
        var headers = new Headers();
        AxCryptReaderBase reader = headers.CreateReader(new LookAheadStream(fromStream));
        var isLegacyV1 = reader is V1AxCryptReader;

        var document = AxCryptReaderBase.Document(reader);

        foreach (var decryptionParameter in DecryptionParameters(identities, isLegacyV1: isLegacyV1))
        {
            if (decryptionParameter.Passphrase != null)
            {
                if (document.Load(decryptionParameter.Passphrase, decryptionParameter.CryptoId, headers))
                {
                    document.DecryptionParameter = decryptionParameter;
                    return document;
                }
            }
            if (decryptionParameter.PrivateKey != null)
            {
                if (document.Load(decryptionParameter.PrivateKey, decryptionParameter.CryptoId, headers))
                {
                    document.DecryptionParameter = decryptionParameter;
                    return document;
                }
            }
        }
        return document;
    }

    private static List<DecryptionParameter> DecryptionParameters(IEnumerable<LogOnIdentity> identities, bool isLegacyV1)
    {
        var decryptionParameters = new List<DecryptionParameter>();
        foreach (var identity in identities)
        {
            decryptionParameters.AddRange(DecryptionParameters(isLegacyV1: isLegacyV1, identity.Passphrase, identity.PrivateKeys));
        }

        return decryptionParameters;
    }

    private static IEnumerable<DecryptionParameter> DecryptionParameters(bool isLegacyV1, Passphrase passphrase, IEnumerable<IAsymmetricPrivateKey?> privateKeys)
    {
        var cryptoIds =
            isLegacyV1
            ? [new V1Aes128CryptoFactory().CryptoId]
            : Resolve.CryptoFactory.OrderedIds.Where(id => id != new V1Aes128CryptoFactory().CryptoId);

        return DecryptionParameter.CreateAll([passphrase], privateKeys, cryptoIds);
    }

    public void Dispose()
    {
        if (_document != null)
        {
            _document.Dispose();
            _document = null!;
        }
    }
}
