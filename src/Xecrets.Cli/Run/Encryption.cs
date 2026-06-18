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

namespace Xecrets.Cli.Run;

internal sealed class Encryption : IDisposable
{
    private readonly IFile _fromFile;

    private readonly EncryptRequest _request;

    private readonly ICoreServices _coreServices;

    private Encryption(IFile fromFile, EncryptRequest request, ICoreServices coreServices)
    {
        _fromFile = fromFile;
        _request = request;
        _coreServices = coreServices;
    }

    public static Task<Encryption> CreateAsync(IFile fromFile, Parameters parameters)
    {
        string passphrase;
        IReadOnlyList<PublicKey> recipients;
        IReadOnlyList<PublicKey> masterKeys;
        if (parameters.EncryptedWithParameters == EncryptedWithParameters.Empty)
        {
            passphrase = parameters.Identities.First(id => id.Passphrase.Length > 0).Passphrase;
            recipients = [.. parameters.PublicKeys.Where(pk => parameters.SharingEmails.Contains(pk.Email))];
            masterKeys = [];
        }
        else
        {
            EncryptedWithParameters encryptedWith = parameters.EncryptedWithParameters;
            passphrase = encryptedWith.Passphrase.Length > 0
                ? encryptedWith.Passphrase
                : parameters.Identities.First(id => id.Passphrase.Length > 0).Passphrase;
            recipients = [.. encryptedWith.Recipients.Concat(parameters.PublicKeys.Where(pk => parameters.SharingEmails.Contains(pk.Email)))];
            masterKeys = encryptedWith.MasterKeys;
            parameters.EncryptedWithParameters = EncryptedWithParameters.Empty;
        }

        EncryptRequest request = new(
            passphrase,
            recipients,
            masterKeys,
            string.Empty,
            fromFile.CreationTimeUtc,
            fromFile.LastAccessTimeUtc,
            fromFile.LastWriteTimeUtc,
            Compress: true,
            Progress: parameters.Progress);
        return Task.FromResult(new Encryption(fromFile, request, parameters.CoreServices));
    }

    public async Task EncryptToAsync(Stream toStream, string originalFileName, bool compress)
    {
        await using Stream fromStream = _fromFile.OpenRead();

        EncryptRequest request = _request with
        {
            OriginalFileName = originalFileName,
            Compress = compress,
        };
        await _coreServices.EncryptAsync(fromStream, toStream, request);
    }

    public void Dispose()
    {
    }
}
