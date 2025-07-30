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

using System.Globalization;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class DatePatternOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        string pattern = parameters.Arg1.Trim();
        if (string.IsNullOrEmpty(pattern))
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidOption, "There must be a date time pattern specified."));
        }

        // Not a full validation, just a quick check to see if it looks like a date time pattern.
        if (!pattern.Contains('y', StringComparison.OrdinalIgnoreCase) ||
            !pattern.Contains('m', StringComparison.OrdinalIgnoreCase) ||
            !pattern.Contains('d', StringComparison.OrdinalIgnoreCase) ||
            !pattern.Contains('h', StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(new Status(XfStatusCode.InvalidOption, "The date time pattern must include all components of a date and time."));
        }

        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        CultureInfo culture = new CultureInfo(string.Empty);

        string pattern = parameters.Arg1.Trim();

        // All known time patterns start with 'h' or 'H' and follow the date pattern.
        int timeIndex = pattern.IndexOf('H', StringComparison.OrdinalIgnoreCase);
        if (timeIndex < 0)
        {
            timeIndex = pattern.Length;
        }

        string shortDatePattern = pattern.Substring(0, timeIndex).TrimEnd();
        string shortTimePattern = pattern.Substring(timeIndex);

        culture.DateTimeFormat.ShortDatePattern = shortDatePattern;
        culture.DateTimeFormat.ShortTimePattern = shortTimePattern;

        CultureInfo.CurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;

        return Task.FromResult(Status.Success);
    }
}
