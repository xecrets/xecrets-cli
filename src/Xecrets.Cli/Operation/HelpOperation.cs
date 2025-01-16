﻿#region Copyright and GPL License

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

using System.Text;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Properties;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

internal class HelpOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        parameters.HelpLevel++;

        string m = string.Empty;

        switch (parameters.HelpLevel)
        {
            case 1:
                var descriptions = new List<Tuple<string, string>>();

                int maxOptionNameLength = 0;
                foreach (string[] description in parameters.Parser.Descriptions)
                {
                    maxOptionNameLength = Math.Max(maxOptionNameLength, description[0].Length);
                    descriptions.Add(new Tuple<string, string>(description[0], description[1]));
                    for (int i = 2; i < description.Length; ++i)
                    {
                        descriptions.Add(new Tuple<string, string>(string.Empty, description[i]));
                    }
                }

                descriptions.ForEach(t => m += t.Item1.PadRight(maxOptionNameLength + 1) + t.Item2 + Environment.NewLine);

                m += Environment.NewLine;
                m += "** Options are position dependent and interpreted strictly in order except for (global). **";
                m += Environment.NewLine;
                m += "Use additional --help options for more help, or visit https://www.axantum.com/ .";
                break;

            case 2:
                m += Environment.NewLine;
                m += Resource.help_details;
                break;

            case 3:
                m += Environment.NewLine;
                m += Encoding.UTF8.GetString(Resource.index);
                break;

            default:
                m += "Sorry, there is no more help to get here! See https://www.axantum.com/ .";
                break;
        }

        parameters.Logger.Log(new Status(parameters, m));
        parameters.Logger.Log(string.Empty);

        return Task.FromResult(Status.Success);
    }
}
