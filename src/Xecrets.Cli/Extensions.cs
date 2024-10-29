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

using System.Text.Json;
using System.Text.RegularExpressions;

using AxCrypt.Abstractions;
using AxCrypt.Core.Portable;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;
using Xecrets.Slip39;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli;

internal static partial class Extensions
{
    public static bool VerifyCanWrite(this IStandardIoDataStore store, Parameters parameters, out Status status)
    {
        if (!New<IFileVerify>().CanWriteToFile(store))
        {
            status = new Status(XfStatusCode.CannotWrite, "Can't write to '{0}'.".Format(store.Name));
            return false;
        }

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

    public static IStandardIoDataStore FindFree(this string fullPath, Parameters parameters)
    {
        IStandardIoDataStore store = Free(fullPath, parameters);
        return store;

        static IStandardIoDataStore Free(string fullPath, Parameters parameters)
        {
            IStandardIoDataStore candidateStore = New<IStandardIoDataStore>(fullPath);
            if (parameters.Overwrite || candidateStore.IsStdout)
            {
                return candidateStore;
            }

            int i = 0;
            while (candidateStore.IsAvailable)
            {
                string path = Path.GetDirectoryName(fullPath) ?? string.Empty;
                string fileName = Path.GetFileName(fullPath);
                string extension = Path.GetExtension(fileName);
                string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - extension.Length);
                string fileNameWithoutExtensionAndNumber = TrailingNumberInParenthesis().Replace(fileNameWithoutExtension, string.Empty);
                string candidateFreeFullPath = Path.Combine(path, $"{fileNameWithoutExtensionAndNumber} ({++i}){extension}");
                candidateStore = New<IStandardIoDataStore>(candidateFreeFullPath);
            }
            return candidateStore;
        }
    }

    [GeneratedRegex(@" \([\d]+\)$")]
    private static partial Regex TrailingNumberInParenthesis();

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

    public static XfSubStatusCode ToXfSubStatusCode(this ErrorCode errorCode)
    {
        XfSubStatusCode subStatus = errorCode switch
        {
            ErrorCode.InsufficientShares => XfSubStatusCode.Slip39InsufficientShares,
            ErrorCode.InconsistentShares => XfSubStatusCode.Slip39InconsistentShares,
            ErrorCode.InvalidGroups => XfSubStatusCode.Slip39InvalidGroups,
            ErrorCode.InvalidFormat => XfSubStatusCode.Slip39InvalidFormat,
            ErrorCode.InvalidMnemonic => XfSubStatusCode.Slip39InvalidMnemonic,
            ErrorCode.InvalidChecksum => XfSubStatusCode.Slip39InvalidChecksum,
            ErrorCode.NoError => XfSubStatusCode.Success,
            _ => XfSubStatusCode.Unknown,
        };

        return subStatus;
    }

    public static Task<Status> Slip39Safe(Func<Status> operation)
    {
        try
        {
            return Task.FromResult(operation());
        }
        catch (Slip39Exception ex)
        {
            return Task.FromResult(new Status(XfStatusCode.Slip39Error, ex.ErrorCode.ToXfSubStatusCode(), ex.Message));
        }
    }
}
