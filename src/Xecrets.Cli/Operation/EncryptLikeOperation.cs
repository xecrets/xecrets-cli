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
using Xecrets.Cli.Implementation;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class EncryptLikeOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Any(id => id.Passphrase.Length > 0))
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, "A password must be provided to encrypt like a file."));
        }

        IFile fromStore = parameters.DesktopServices.StandardIoFile(parameters.Arg1);
        if (fromStore.IsStdIo)
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidOption, parameters,
                "Encrypt like another file is not supported from a standard input stream."));
        }
        if (!parameters.DesktopServices.CanReadFromFile(fromStore, out string? reason))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, parameters, $"Can't read from '{fromStore.Name}'. [{reason}]"));
        }

        return Task.FromResult(Status.Success);
    }

    public async Task<Status> RealAsync(Parameters parameters)
    {
        IFile fromStore = parameters.DesktopServices.StandardIoFile(parameters.Arg1);

        await using Stream fromStream = fromStore.OpenRead();
        using Decryption decryption = await Decryption.CreateAsync(fromStream, parameters.Identities, new NoProgressContext(), parameters.CoreServices);

        parameters.EncryptedWithParameters = decryption.EncryptedWithParameters;

        return Status.Success;
    }
}
