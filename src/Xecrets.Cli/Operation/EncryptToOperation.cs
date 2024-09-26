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

using AxCrypt.Abstractions;
using AxCrypt.Core;
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Implementation;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;
using Xecrets.Licensing.Abstractions;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class EncryptToOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Where(id => id.Passphrase != Passphrase.Empty).Any())
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, "A password must be provided to encrypt files."));
        }

        IStandardIoDataStore toFreeStore = parameters.Arg2.FindFree(parameters);
        if (!toFreeStore.VerifyCanWrite(parameters, out Status status))
        {
            return Task.FromResult(status);
        }

        IStandardIoDataStore fromStore = New<IStandardIoDataStore>(parameters.Arg1);
        if (fromStore.IsStdIo && !fromStore.IsNamedStdIo)
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidOption, "Encryption is not supported from an unnamed standard input stream."));
        }

        if (!New<IFileVerify>().CanReadFromFile(fromStore))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, "Can't read from '{0}'.".Format(fromStore.Name)));
        }
        if (!fromStore.IsEncryptable)
        {
            return Task.FromResult(new Status(XfStatusCode.FileUnavailable, "Encryption of '{0}' is not supported, it may be a system file or hidden.".Format(parameters.CurrentOp.Arg1)));
        }

        if (parameters.ProgrammaticUse && FileLargerThanLicenseLimit(fromStore))
        {
            return Task.FromResult(new Status(XfStatusCode.Unlicensed, "'{0}' is too large for encryption. When using options for programmatic use, a valid maintenance subscription is required for files > 1 MB, or use a GPL build.".Format(parameters.CurrentOp.Arg1)));
        }

        parameters.TotalsTracker.AddWorkItem(fromStore.Length());
        return Task.FromResult(Status.Success);
    }

    private static bool FileLargerThanLicenseLimit(IStandardIoDataStore fromStore)
    {
        long length;
        if (fromStore.IsStdIo)
        {
            Stream stdin = fromStore.OpenRead(); // Don't think we should close the stdin stream
            length = stdin.CanSeek ? stdin.Length : 0;
        }
        else
        {
            length = fromStore.Length();
        }

        if (length <= 1024 * 1024)
        {
            return false;
        }
        LicenseStatus licenseStatus = New<ILicense>().Status();
        if (licenseStatus is LicenseStatus.Gpl or LicenseStatus.Valid)
        {
            return false;
        }
        return true;
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        IStandardIoDataStore toFreeStore = parameters.Arg2.FindFree(parameters);
        if (!toFreeStore.VerifyCanWrite(parameters, out Status status))
        {
            return Task.FromResult(status);
        }

        IStandardIoDataStore fromStore = New<IStandardIoDataStore>(parameters.Arg1);

        parameters.Progress.Display = parameters.Arg1;

        IEnumerable<UserPublicKey> userPublicKeys = parameters.PublicKeys.Where(pk => parameters.SharingEmails.Contains(pk.Email));
        using (var encryption = new Encryption(fromStore.OpenRead(), parameters.Identities.Where(id => id.Passphrase != Passphrase.Empty), userPublicKeys, parameters.Progress))
        {
            if (parameters.AsciiArmor)
            {
                toFreeStore = new AsciiArmorDataStore(toFreeStore);
            }
            encryption.EncryptTo(toFreeStore, fromStore.AliasName,
                parameters.Compress ? AxCryptOptions.EncryptWithCompression : AxCryptOptions.EncryptWithoutCompression);
        }

        string freeTo = Path.Combine(Path.GetDirectoryName(parameters.Arg2) ?? string.Empty, toFreeStore.Name);
        parameters.Logger.Log(new Status(parameters, "Encrypted '{0}' to '{1}'.".Format(parameters.Arg1, freeTo)));
        return Task.FromResult(Status.Success);
    }
}
