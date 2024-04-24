#region Coypright and GPL License

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

#endregion Coypright and GPL License

using System.Text.Json;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation
{
    internal class JwtClaimsOperation : IExecutionPhases
    {
        public Task<Status> DryAsync(Parameters parameters)
        {
            return Task.FromResult(Status.Success);
        }

        /// <summary>
        /// Arguments[0] => days until expiration, i.e. '365'
        /// Arguments[1] => claims JSON, i.e. "{\"xflic.axantum.com\":\"cli\"}" or "{\"xflic.axantum.com\":\"skd\"}" or "{\"xflic.axantum.com\":\"ez\"}"
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Task<Status> RealAsync(Parameters parameters)
        {
            string daysText = parameters.Arguments[0];
            string claimsJson = parameters.Arguments[1];

            if (!int.TryParse(daysText, out int days))
            {
                return Task.FromResult(new Status(XfStatusCode.InvalidDays, $"Can't interpret '{daysText}' as an integer number of days claims are valid."));
            }
            parameters.JwtDaysUntilExpiration = days;

            try
            {
                Dictionary<string, object> claims = JsonSerializer.Deserialize<Dictionary<string, object>>(claimsJson, SourceGenerationContext.Default.DictionaryStringObject) ?? throw new InvalidOperationException($"Can't deserialize '{claimsJson}' as a claims dictionary.");
                foreach (KeyValuePair<string, object> kvp in claims)
                {
                    parameters.JwtClaims.Add(kvp.Key, ((JsonElement)kvp.Value).ToObject() ?? throw new InvalidOperationException("Deserialized value ToString() can't be null here."));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Status(XfStatusCode.JwtDeserializeError, ex.Message + Environment.NewLine + ex.StackTrace));
            }

            return Task.FromResult(Status.Success);
        }
    }
}
