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

internal sealed class Decryption : IDisposable
{
    private readonly IDecryptionSession _session;

    private Decryption(IDecryptionSession session)
    {
        _session = session;
    }

    public bool IsDecryptable => _session.IsDecryptable;

    public string OriginalFileName => _session.OriginalFileName;

    public static async Task<Decryption> CreateAsync(Stream fromStream, IEnumerable<Identity> identities, IProgress<Progress> progress, ICoreServices coreServices)
    {
        DecryptRequest request = new([.. identities], progress);
        IDecryptionSession session = await coreServices.OpenDecryptionAsync(fromStream, request);
        return new Decryption(session);
    }

    public async Task DecryptToAsync(IFile toFile, IDesktopServices desktopServices)
    {
        try
        {
            await using Stream toStream = toFile.OpenWrite();
            await _session.DecryptAsync(toStream);
            if (!_session.IsDecryptable)
            {
                return;
            }

            if (!toFile.IsStdIo)
            {
                toFile.SetFileTimes(_session.CreationTimeUtc, _session.LastAccessTimeUtc, _session.LastWriteTimeUtc);
            }
        }
        catch (Exception)
        {
            if (toFile is { IsStdIo: false, IsAvailable: true })
            {
                await desktopServices.WipeAsync(toFile.FullName, new Progress<Progress>());
            }
            throw;
        }
    }

    public EncryptedWithParameters EncryptedWithParameters => _session.EncryptedWithParameters;

    public void Dispose()
    {
        _session.Dispose();
    }
}
