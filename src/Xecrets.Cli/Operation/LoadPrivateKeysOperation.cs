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

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class LoadPrivateKeysOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        IFile fileStore = parameters.DesktopServices.StandardIoFile(parameters.Arg1);
        if (!parameters.DesktopServices.CanReadFromFile(fileStore, out string? reason))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, parameters,
                $"Can't read private keys from file '{fileStore.Name}'. [{reason}]"));
        }

        if (parameters.Arg2.Length == 0)
        {
            return Task.FromResult(Status.Success);
        }

        fileStore = parameters.DesktopServices.StandardIoFile(parameters.Arg2);
        if (!parameters.DesktopServices.CanWriteToFile(fileStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotWrite, parameters,
                $"Can't write to file '{fileStore.Name}'."));
        }
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        return Task.FromResult(RealAsyncInternal(parameters));
    }

    private static Status RealAsyncInternal(Parameters parameters)
    {
        IFile store = parameters.DesktopServices.StandardIoFile(parameters.Arg1);
        string json;
        using (StreamReader reader = new StreamReader(store.OpenRead()))
        {
            json = reader.ReadToEnd();
        }

        PrivateKeyImportResult result;
        try
        {
            string? reEncryptPassphrase = parameters.Identities.FirstOrDefault(i => i.Passphrase.Length > 0)?.Passphrase;
            result = parameters.CoreServices.ImportPrivateKeys(json, new PrivateKeyImportRequest(
                [.. parameters.Identities.Where(i => i.Passphrase.Length > 0).Select(i => i.Passphrase)],
                reEncryptPassphrase,
                null));
        }
        catch (Exception ex)
        {
            return new Status(XfStatusCode.DeserializeError, parameters,
                $"Deserialization error with '{store.Name}'. {ex.Message}");
        }

        if (result.LoadedKeyPairs.Count != 0)
        {
            parameters.Identities.Add(new Identity(string.Empty, result.LoadedKeyPairs));
        }

        if (parameters.Arg2.Length > 0 && result.ReEncryptedAccountsJson != null)
        {
            store = parameters.DesktopServices.StandardIoFile(parameters.Arg2);
            using StreamWriter writer = new(store.OpenWrite());
            writer.Write(result.ReEncryptedAccountsJson);
        }
        return Status.Success;
    }
}
