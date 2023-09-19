#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright © 2022-2023, Svante Seleborg, All Rights Reserved.
 *
 * This code file is part of Xecrets File Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 * 
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets File Cli is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets File Cli is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets File Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Coypright and GPL License

using System.Text;

using AxCrypt.Abstractions;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.IO;

using Xecrets.File.Cli.Abstractions;
using Xecrets.File.Cli.Public;
using Xecrets.File.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.File.Cli.Operation
{
    internal class LoadPublicKeyOperation : IExecutionPhases
    {
        public Task<Status> DryAsync(Parameters parameters)
        {
            foreach (string from in parameters.CurrentOp.Arguments)
            {
                var fromStore = New<IStandardIoDataStore>(from);
                if (!New<IFileVerify>().CanReadFromFile(fromStore))
                {
                    return Task.FromResult(new Status(XfStatusCode.CannotRead, parameters, "Can't read from file '{0}'.".Format(fromStore.Name)));
                }
            }
            return Task.FromResult(Status.Success);
        }

        public Task<Status> RealAsync(Parameters parameters)
        {
            foreach (string from in parameters.CurrentOp.Arguments)
            {
                var fromStore = New<IDataStore>(from);
                string userPublicKeyJson;

                using (var reader = new StreamReader(fromStore.OpenRead(), Encoding.UTF8))
                {
                    userPublicKeyJson = reader.ReadToEnd();
                }

                UserPublicKey? userPublicKey = New<IStringSerializer>().Deserialize<UserPublicKey>(userPublicKeyJson);
                if (userPublicKey == null)
                {
                    return Task.FromResult(new Status(XfStatusCode.PublicKeyNotFound, parameters, "Can't find a public key in '{0}'.".Format(fromStore.Name)));
                }
                parameters.LoadedPublicKeys.AddOrReplace(userPublicKey);
                parameters.Logger.Log(new Status(parameters, "Loaded a public key for '{0}' from '{1}'.".Format(userPublicKey.Email, parameters.From)));
            }

            return Task.FromResult(Status.Success);
        }
    }
}
