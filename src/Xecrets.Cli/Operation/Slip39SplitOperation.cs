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

internal class Slip39SplitOperation : IExecutionPhases
{
    private List<XfOptionKeys> _shareFormats = [];

    [AllowNull]
    private IStandardIoDataStore _toStore;

    public Task<Status> DryAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => SplitOperationInternal(parameters));

    public Task<Status> RealAsync(Parameters parameters) =>
        Extensions.Slip39Safe(() => RealSplitOperationInternal(parameters));

    private Status RealSplitOperationInternal(Parameters parameters)
    {
        Status status = SplitOperationInternal(parameters);
        if (!status.IsSuccess)
        {
            return status;
        }

        StringEncoding stringEncoding = StringEncoding.None;
        switch (parameters.Slip39.SecretFormat)
        {
            case XfOptionKeys.Bip39:
                stringEncoding = StringEncoding.Bip39;
                break;
            case XfOptionKeys.Hex:
                stringEncoding = StringEncoding.Hex;
                break;
            case XfOptionKeys.Slip39:
                break;
            case XfOptionKeys.Text:
                stringEncoding = StringEncoding.None;
                break;
            case XfOptionKeys.Base64:
                stringEncoding = StringEncoding.Base64;
                break;
            default:
                return new Status(XfStatusCode.InvalidOption,
                    parameters, $"Invalid secret format '{parameters.Slip39.SecretFormat}'.");
        }

        IShamirsSecretSharing sss = New<IShamirsSecretSharing>();
        byte[] masterSecretBytes = parameters.Slip39.Secret.ToSecretBytes(stringEncoding);
        Group[] groups = [.. parameters.Slip39.Groups.Select(g => new Group(g.Threshold, g.Shares))];

        Share[][] shares = sss.GenerateShares(true, parameters.Slip39.Exponent, parameters.Slip39.GroupThreshold,
            groups, parameters.Slip39.Password, masterSecretBytes);

        XfSlip39.ShareSet shareSet = new(
            Threshold: parameters.Slip39.GroupThreshold,
            Description: $"{parameters.Slip39.Groups.Count} group(s) with group threshold {parameters.Slip39.GroupThreshold}.",
            Value: null,
            Groups: [..shares.Select((g, gi) => new XfSlip39.Group(
                Description: $"Group {gi + 1} of {shares.Length} with share threshold {groups[gi].ShareThreshold}.",
                Threshold: groups[gi].ShareThreshold,
                Shares: [.. g.Select(s => new XfSlip39.Share(
                    Slip39: _shareFormats.Contains(XfOptionKeys.Slip39) ? s.ToString("G") : null,
                    Hex: _shareFormats.Contains(XfOptionKeys.Hex) ? s.ToString("X") : null,
                    Base64: _shareFormats.Contains(XfOptionKeys.Base64) ? s.ToString("64") : null
                ))]
            ))]
        );

        if (parameters.ProgrammaticUse)
        {
            string shareSetJson = JsonSerializer.Serialize(shareSet, typeof(XfSlip39.ShareSet),
                SourceGenerationContext.Indented);

            using Stream stream = _toStore.OpenWrite();
            byte[] bytes = Encoding.UTF8.GetBytes(shareSetJson);
            stream.Write(bytes, 0, bytes.Length);
        }
        else
        {
            using StreamWriter stream = new(_toStore.OpenWrite());
            if (shareSet.Groups.Length > 1)
            {
                stream.WriteLine(shareSet.Description);
            }
            for (int i = 0; i < shareSet.Groups.Length; ++i)
            {
                if (i > 0)
                {
                    stream.WriteLine();
                }
                stream.WriteLine(shareSet.Groups[i].Description);
                foreach (XfOptionKeys key in _shareFormats)
                {
                    foreach (var share in shareSet.Groups[i].Shares)
                    {
                        string value = key switch
                        {
                            XfOptionKeys.Slip39 => share.Slip39!,
                            XfOptionKeys.Hex => share.Hex!,
                            XfOptionKeys.Base64 => share.Base64!,
                            _ => throw new InvalidOperationException($"Unknown share format '{_shareFormats}'.")
                        };
                        stream.WriteLine(value);
                    }
                }
            }
        }
        return Status.Success;
    }

    private Status SplitOperationInternal(Parameters parameters)
    {
        if (parameters.Slip39.GroupThreshold < 1 || parameters.Slip39.GroupThreshold > parameters.Slip39.Groups.Count)
        {
            return new Status(XfStatusCode.InvalidOption, parameters,
                $"Group threshold '{parameters.Slip39.GroupThreshold}' must be >= 1 and <= number of groups.");
        }
        if (parameters.Slip39.Secret.Length == 0)
        {
            return new Status(XfStatusCode.InvalidOption, parameters, "No secret to split was specified.");
        }

        string arg = parameters.Arg1;

        if (arg.Length == 0)
        {
            arg = XfOptionKeys.Slip39.ToString();
        }

        _shareFormats.Clear();
        string[] formats = arg.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (string f in formats)
        {
            if (!Enum.TryParse(f, ignoreCase: true, out XfOptionKeys formatOption))
            {
                return new Status(XfStatusCode.InvalidOption, parameters, $"Unknown format '{f}'.");
            }
            _shareFormats.Add(formatOption);
        }

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
