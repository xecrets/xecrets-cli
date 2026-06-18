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
using Xecrets.Licensing.Abstractions;
using Xecrets.Core.Public;

namespace Xecrets.Cli.Operation;

internal class EncryptToOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Any(id => id.Passphrase.Length > 0))
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, "A password must be provided to encrypt files."));
        }

        IFile toFreeStore = parameters.Arg2.FindFreeFile(parameters);
        if (!toFreeStore.VerifyCanWrite(parameters, out Status status))
        {
            return Task.FromResult(status);
        }

        IFile fromStore = parameters.DesktopServices.StandardIoFile(parameters.Arg1);
        if (fromStore is { IsStdIo: true, IsNamedStdIo: false })
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidOption,
                "Encryption is not supported from an unnamed standard input stream."));
        }

        if (!parameters.DesktopServices.CanReadFromFile(fromStore, out string? reason))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead,
                $"Can't read from '{fromStore.Name}'. [{reason}]"));
        }
        if (!fromStore.IsEncryptable)
        {
            return Task.FromResult(new Status(XfStatusCode.FileUnavailable,
                "Encryption of '{0}' is not supported, it may be a system file or hidden.".Format(parameters.CurrentOp.Arg1)));
        }
        if (fromStore.IsNamedStdIo && parameters.Arg3.Length > 0)
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidOption,
                $"Cannot specify both original name '{parameters.Arg3}' and stdin alias '{fromStore.AliasName}'."));
        }

        if (parameters.ProgrammaticUse && FileLargerThanLicenseLimit(fromStore, parameters.CliServices.License))
        {
            return Task.FromResult(new Status(XfStatusCode.Unlicensed,
                "'{0}' is too large for encryption. When using options for programmatic use, a valid maintenance " +
                "subscription is required for files > 1 MB, or use a GPL build.".Format(parameters.CurrentOp.Arg1)));
        }

        parameters.TotalsTracker.AddWorkItem(fromStore.Length);
        return Task.FromResult(Status.Success);
    }

    private static bool FileLargerThanLicenseLimit(IFile fromStore, ILicense license)
    {
        long length;
        if (fromStore.IsStdIo)
        {
            Stream stdin = fromStore.OpenRead(); // Don't think we should close the stdin stream
            length = stdin.CanSeek ? stdin.Length : 0;
        }
        else
        {
            length = fromStore.Length;
        }

        if (length <= 1024 * 1024)
        {
            return false;
        }
        LicenseStatus licenseStatus = license.Status();
        if (licenseStatus is LicenseStatus.Gpl or LicenseStatus.Valid)
        {
            return false;
        }
        return true;
    }

    public async Task<Status> RealAsync(Parameters parameters)
    {
        IFile toFreeStore = parameters.Arg2.FindFreeFile(parameters);
        if (!toFreeStore.VerifyCanWrite(parameters, out Status status))
        {
            return status;
        }

        IFile fromStore = parameters.DesktopServices.StandardIoFile(parameters.Arg1);

        parameters.Progress.Display = parameters.Arg1;
        parameters.Progress.Report(Progress.TotalAdded(fromStore.Length));

        try
        {
            using var encryption = await Encryption.CreateAsync(fromStore, parameters);
            
            string originalName = parameters.Arg3.Length > 0 ? parameters.Arg3 : fromStore.AliasName;
            await using Stream toStream = parameters.AsciiArmor
                ? new AsciiArmorStream(toFreeStore.OpenWrite())
                : toFreeStore.OpenWrite();
            await encryption.EncryptToAsync(toStream, originalName, parameters.Compress);
        }
        catch
        {
            if (toFreeStore is { IsStdIo: false, IsAvailable: true })
            {
                toFreeStore.DeleteIfAvailable();
            }
            throw;
        }

        string freeTo = Path.Combine(Path.GetDirectoryName(parameters.Arg2) ?? string.Empty, toFreeStore.Name);
        parameters.Logger.Log(new Status(parameters, "Encrypted '{0}' to '{1}'.".Format(parameters.Arg1, freeTo)));
        return Status.Success;
    }
}
