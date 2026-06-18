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

internal class UseKeyPairOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        if (!parameters.Identities.Any())
        {
            return Task.FromResult(new Status(XfStatusCode.NoPassword, "A password must be provided to access an encrypted key pair file."));
        }
        IFile fromFile = parameters.DesktopServices.StandardIoFile(parameters.Arg1);
        if (!parameters.DesktopServices.CanReadFromFile(fromFile, out string? reason))
        {
            return Task.FromResult(new Status(XfStatusCode.CannotRead, $"Can't read key pair from file '{fromFile.Name}'. [{reason}]"));
        }
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        byte[] keyPairFile = parameters.DesktopServices.File(parameters.Arg1).ReadAllBytes();
        
        if (!parameters.CoreServices.TryLoadKeyPair(keyPairFile, [.. parameters.Identities.Select(i => i.Passphrase)], out LoadedKeyPair? loaded))
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidPassword, parameters, "No valid password was provided to decrypt the key pair."));
        }

        Identity identity = parameters.Identities[loaded.Index];
        parameters.Identities[loaded.Index] = identity with { KeyPairs = [.. identity.KeyPairs.Concat([loaded.KeyPair])] };

        parameters.Logger.Log(new Status(parameters, "Loaded a key pair created {3} with tag '{2}' for '{1}' from '{0}'".Format(parameters.CurrentOp.Arg1, loaded.KeyPair.Email, loaded.KeyPair.PublicKey.Tag ?? string.Empty, loaded.KeyPair.CreatedUtc.ToLocalTime()))
        {
            Utc = loaded.KeyPair.CreatedUtc.UtcDateTime,
        });

        parameters.LoadedPublicKeys.AddOrReplace(loaded.KeyPair.PublicKey);
        parameters.SharingEmails.Add(loaded.KeyPair.PublicKey.Email);

        parameters.Logger.Log(new Status(parameters, "Loaded a public key for '{0}' from '{1}'.".Format(loaded.KeyPair.PublicKey.Email, parameters.Arg1)));

        return Task.FromResult(Status.Success);
    }
}
