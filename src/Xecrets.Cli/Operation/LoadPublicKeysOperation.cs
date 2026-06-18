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

internal class LoadPublicKeysOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        foreach (string from in parameters.CurrentOp.Arguments)
        {
            IFile fromFile = parameters.DesktopServices.StandardIoFile(from);
            if (!parameters.DesktopServices.CanReadFromFile(fromFile, out string? reason))
            {
                return Task.FromResult(new Status(XfStatusCode.CannotRead, parameters, $"Can't read public keys from file '{fromFile.Name}'. [{reason}]"));
            }
        }
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        foreach (string from in parameters.CurrentOp.Arguments)
        {
            IFile fromFile = parameters.DesktopServices.File(from);
            string userPublicKeyJson;

            using (StreamReader reader = new(fromFile.OpenRead()))
            {
                userPublicKeyJson = reader.ReadToEnd();
            }

            PublicKey? userPublicKey = parameters.CoreServices.ImportPublicKey(userPublicKeyJson);
            if (userPublicKey == null)
            {
                return Task.FromResult(new Status(XfStatusCode.PublicKeyNotFound, parameters, "Can't find a public key in '{0}'.".Format(fromFile.Name)));
            }
            parameters.LoadedPublicKeys.AddOrReplace(userPublicKey);
            parameters.Logger.Log(new Status(parameters, "Loaded a public key for '{0}' from '{1}'.".Format(userPublicKey.Email, parameters.Arg1)));
        }

        return Task.FromResult(Status.Success);
    }
}
