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

using System.Text.Json.Serialization;

namespace Xecrets.Cli.Public;

public sealed record XfSlip39
{
    public sealed record Prefixes([property: JsonPropertyName("prefixes")] Prefix[] Values);

    public sealed record Prefix(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("extendable")] bool Extendable,
        [property: JsonPropertyName("exponent")] int IterationExponent,
        [property: JsonPropertyName("groupIndex")] int GroupIndex,
        [property: JsonPropertyName("groupThreshold")] int GroupThreshold,
        [property: JsonPropertyName("groupCount")] int GroupCount,
        [property: JsonPropertyName("memberIndex")] int MemberIndex,
        [property: JsonPropertyName("memberThreshold")] int MemberThreshold);

    public sealed record ShareSet(
        [property: JsonPropertyName("threshold")] int Threshold,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("secret")] Secret? Value,
        [property: JsonPropertyName("groups")] Group[] Groups);

    public sealed record Secret(
        [property: JsonPropertyName("bip39")] string Bip39,
        [property: JsonPropertyName("hex")] string Hex,
        [property: JsonPropertyName("base64")] string Base64,
        [property: JsonPropertyName("text")] string Text);

    public sealed record Group(
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("threshold")] int Threshold,
        [property: JsonPropertyName("shares")] Share[] Shares);

    public sealed record Share(
        [property: JsonPropertyName("slip39")] string? Slip39,
        [property: JsonPropertyName("hex")] string? Hex,
        [property: JsonPropertyName("base64")] string? Base64);
}

