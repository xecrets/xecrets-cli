#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright 2022, Svante Seleborg, All Rights Reserved.
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

using System.Text.Json;

using AxCrypt.Abstractions;
using AxCrypt.Core.Portable;

using Xecrets.File.Cli.Abstractions;
using Xecrets.File.Cli.Public;
using Xecrets.File.Cli.Run;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.File.Cli
{
    internal static class Extensions
    {
        public static bool VerifyCanWrite(this IStandardIoDataStore store, Parameters parameters, out Status status)
        {
            if (!New<IFileVerify>().CanWriteToFile(store))
            {
                status = new Status(XfStatusCode.CannotWrite, "Can't write to '{0}'.".Format(store.Name));
                return false;
            }
            store.Delete();

            if (store.IsNamedStdIo)
            {
                status = new Status(XfStatusCode.InvalidOption, "Writing to a named standard output stream is not supported.");
                return false;
            }

            if (parameters.IsStdoutLog && store.IsStdIo)
            {
                status = new Status(XfStatusCode.NotSupported, "It's not supported to both redirect logs to standard output and a destination.");
                return false;
            }

            status = Status.Success;
            return true;
        }

        public static IStandardIoDataStore FindAvailable(this string fullPath, Parameters parameters)
        {
            IStandardIoDataStore store = Available(fullPath, parameters);
            return store;

            static IStandardIoDataStore Available(string fullPath, Parameters parameters)
            {
                IStandardIoDataStore availableStore = New<IStandardIoDataStore>(fullPath);
                if (parameters.Overwrite || availableStore.IsStdout)
                {
                    return availableStore;
                }

                int i = 0;
                while (availableStore.IsAvailable)
                {
                    string extension = New<IPath>().GetExtension(fullPath);
                    string pathWithoutExtension = fullPath.Substring(0, fullPath.Length - extension.Length);
                    string alternativeAvailableFullPath = $"{pathWithoutExtension}({++i}){extension}";
                    availableStore = New<IStandardIoDataStore>(alternativeAvailableFullPath);
                }
                return availableStore;
            }
        }

        public static string ToDisplayName(this IStandardIoDataStore store)
        {
            string displayName = Path.GetRelativePath(Environment.CurrentDirectory, store.FullName);
            return displayName.StartsWith("..") ? store.FullName : displayName;
        }

        public static object ToObject(this JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString() ?? throw new InvalidOperationException("String value can't be null here."),
                JsonValueKind.Number => element.GetInt32(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                _ => throw new ArgumentException($"Deserializing {element.ValueKind} is not supported here."),
            };
        }
    }
}
