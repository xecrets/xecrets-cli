﻿#region Copyright and GPL License

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

using System.Text;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation;

/// <summary>
/// Helper to produce Markdown command line documentation. Use as for example:
/// XecretsCli.exe --stdout --internal --argument-markdown
/// </summary>
internal class ArgumentMarkdownOperation : IExecutionPhases
{
    public Task<Status> DryAsync(Parameters parameters)
    {
        return Task.FromResult(Status.Success);
    }

    public Task<Status> RealAsync(Parameters parameters)
    {
        StringBuilder descriptions = new StringBuilder();
        StringBuilder synopsis = new StringBuilder();
        synopsis.Append("XecretsCli");
        foreach (string[] description in parameters.Parser.Descriptions)
        {
            string[] parts = description[0].Split(' ');
            string option = parts[0].Split('|').Last();
            string arguments = string.Join(' ', parts.Skip(1).ToArray());
            synopsis.Append(" [").Append(option).Append(arguments.Length > 0 ? " " : string.Empty).Append(arguments).Append(']');

            descriptions.AppendLine(description[0]);
            descriptions.AppendLine(":       " + description[1]);
            for (int i = 2; i < description.Length; ++i)
            {
                descriptions.AppendLine("        " + description[i]);
            }
            descriptions.AppendLine();
        }
        synopsis.Replace('{', '_').Replace('}', '_');
        descriptions.Replace('{', '_').Replace('}', '_');

        parameters.Logger.Log(new Status(parameters, synopsis.ToString()));
        parameters.Logger.Log(string.Empty);
        parameters.Logger.Log(new Status(parameters, descriptions.ToString()));
        return Task.FromResult(Status.Success);
    }
}
