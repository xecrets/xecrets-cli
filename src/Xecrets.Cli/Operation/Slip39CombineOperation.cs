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

using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

using AxCrypt.Abstractions;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;
using Xecrets.Slip39;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Operation;

internal class Slip39CombineOperation : IExecutionPhases
{
    [AllowNull]
    private IStandardIoDataStore _toStore;

    public Task<Status> DryAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => CombineOperationInternal(parameters));

    public Task<Status> RealAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => RealCombineOperationInternal(parameters));

    private Status RealCombineOperationInternal(Parameters parameters)
    {
        Status status = CombineOperationInternal(parameters);
        if (!status.IsSuccess)
        {
            return status;
        }

        IShamirsSecretSharing sss = New<IShamirsSecretSharing>();
        GroupedShares groupedShares = sss.CombineShares([.. parameters.Slip39.Shares.Select(Share.Parse)],
            parameters.Slip39.Password);

        if (parameters.ProgrammaticUse)
        {
            XfSlip39.ShareSet shareSet = new(
                Description: $"{groupedShares.ShareGroups.Length} groups, with threshold {groupedShares.ShareGroups[0][0].Prefix.GroupThreshold}",
                Threshold: groupedShares.ShareGroups[0][0].Prefix.GroupThreshold,
                Value: groupedShares.Secret.Length > 0
                    ? new(
                        Bip39: groupedShares.Secret.ToBip39(),
                        Hex: groupedShares.Secret.ToHex(),
                        Base64: groupedShares.Secret.ToHex(),
                        Text: groupedShares.Secret.ToSecretString())
                    : null,
                Groups: [.. groupedShares.ShareGroups.Select((g, gi) => new XfSlip39.Group(
                    Description: $"Group {gi + 1} of {groupedShares.ShareGroups.Length} with threshold {g[0].Prefix.MemberThreshold}.",
                    Threshold: g[0].Prefix.MemberThreshold,
                    Shares: [.. g.Select(s => new XfSlip39.Share(
                        Slip39: s.ToString("G"),
                        Hex: s.ToString("X"),
                        Base64: s.ToString("64")
                        ))]
                    ))]
                );

            string shareSetJson = JsonSerializer.Serialize(shareSet, typeof(XfSlip39.ShareSet),
                SourceGenerationContext.Indented);

            using Stream stream = _toStore.OpenWrite();
            byte[] bytes = Encoding.UTF8.GetBytes(shareSetJson);
            stream.Write(bytes, 0, bytes.Length);
        }
        else
        {
            byte[] masterSecret = groupedShares.Secret;
            string secretString = parameters.Slip39.CombineFormat switch
            {
                XfOptionKeys.Bip39 => masterSecret.ToBip39(),
                XfOptionKeys.Base64 => masterSecret.ToUrlSafeBase64(),
                XfOptionKeys.Text => masterSecret.ToSecretString(),
                XfOptionKeys.Hex => masterSecret.ToHex(),
                _ => throw new InvalidOperationException($"Unknown format '{parameters.Slip39.CombineFormat}'.")
            };

            using StreamWriter stream = new(_toStore.OpenWrite());

            stream.WriteLine(secretString);
        }
        return Status.Success;
    }

    private Status CombineOperationInternal(Parameters parameters)
    {
        string format = parameters.Arg1;

        if (format.Length == 0)
        {
            return new Status(XfStatusCode.InvalidOption, parameters, "Missing {format}.");
        }

        if (!Enum.TryParse(format, ignoreCase: true, out XfOptionKeys formatOption))
        {
            return new Status(XfStatusCode.InvalidOption,
                parameters, $"Unknown format '{format}'.");
        }

        switch (formatOption)
        {
            case XfOptionKeys.Bip39:
            case XfOptionKeys.Hex:
            case XfOptionKeys.Text:
            case XfOptionKeys.Base64:
                break;
            default:
                return new Status(XfStatusCode.InvalidOption,
                    parameters, $"Invalid format '{format}'.");
        }

        parameters.Slip39.CombineFormat = formatOption;

        string toStore = parameters.Arg2.Length > 0 ? parameters.Arg2 : "+";
        _toStore = New<IStandardIoDataStore>(toStore);
        if (!New<IFileVerify>().CanWriteToFile(_toStore))
        {
            return new Status(XfStatusCode.CannotWrite, parameters,
                "The file path '{0}' cannot be written to.".Format(_toStore.Name));
        }

        return Status.Success;
    }
}
