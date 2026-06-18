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

internal class CreateKeyPairOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Any())
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, parameters, "A password must be provided to create a key pair."));
        }

        if (!parameters.CoreServices.TryParseEmail(parameters.Arg1, out string? _))
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidEmail, parameters, "'{0}' is not a valid email address.".Format(parameters.Arg1)));
        }

        IFile toFile = parameters.DesktopServices.StandardIoFile(parameters.Arg2);
        if (!parameters.DesktopServices.CanWriteToFile(toFile))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotWrite, parameters, "The file path '{0}' cannot be written to.".Format(toFile.Name)));
        }

        return Task.FromResult(Status.Success);
    }

    public async Task<Status> RealAsync(Parameters parameters)
    {
        parameters.Logger.Log(XfOpCode.Progressing, new Status(parameters, "Generating a key pair may take some time, please be patient."));

        Identity identity = parameters.Identities.First(i => i.Passphrase.Length > 0);
        KeyPair keyPair = await parameters.CoreServices.CreateKeyPairAsync(
            parameters.CurrentOp.Arg1,
            identity.Passphrase,
            parameters.CliServices.TimeProvider.GetUtcNow());
        IFile destination = parameters.DesktopServices.StandardIoFile(parameters.CurrentOp.Arg2);
        await using (Stream stream = destination.OpenWrite())
        {
            await stream.WriteAsync(keyPair.EncryptedBytes.AsMemory(0, keyPair.EncryptedBytes.Length));
        }

        int index = parameters.Identities.IndexOf(identity);
        parameters.Identities[index] = identity with { KeyPairs = [.. identity.KeyPairs.Concat([keyPair])] };

        parameters.Logger.Log(new Status(parameters, $"Created a key pair for '{parameters.CurrentOp.Arg1}' in '{parameters.CurrentOp.Arg2}'."));

        return Status.Success;
    }
}
