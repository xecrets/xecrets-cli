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

using AxCrypt.Abstractions;
using AxCrypt.Core.Portable;

using Xecrets.File.Cli.Abstractions;
using Xecrets.File.Cli.Public;
using Xecrets.File.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.File.Cli.Operation
{
    internal class DecryptToFolderOperation : DecryptOperationBase
    {
        protected override (Status, IStandardIoDataStore) ToStore(Parameters parameters, string originalFileName)
        {
            Status status;
            string toFolder = ToFolder(parameters.From, parameters.To);
            if (toFolder.Length == 0)
            {
                status = new Status(XfStatusCode.NotAFolder, parameters, "Cannot determine a destination folder from '{0}' and '{1}'.".Format(parameters.From, parameters.To));
                return (status, null!);
            }

            IStandardIoDataStore toFolderStore = New<IStandardIoDataStore>(toFolder);
            if (toFolderStore.IsStdIo)
            {
                status = new Status(XfStatusCode.NotSupported, parameters, "Decryption to a stream is not supported when decrypting to a folder.".Format(toFolderStore.Name));
                return (status, null!);
            }

            string toPath = New<IPath>().Combine(toFolder, originalFileName);
            IStandardIoDataStore toAvailableStore = toPath.FindAvailable(parameters);
            if (!toAvailableStore.VerifyCanWrite(parameters, out status))
            {
                return (status, null!);
            }

            return (Status.Success, toAvailableStore);
        }

        private static string ToFolder(string from, string toFolder)
        {
            if (toFolder.Length == 0)
            {
                toFolder = New<IPath>().GetDirectoryName(from);
            }
            if (toFolder.Length == 0)
            {
                return ".";
            }

            return toFolder;
        }
    }
}
