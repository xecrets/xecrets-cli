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

#endregion Copyright and GPL Licenseusing System.Text;

using System.Text;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli.Operation
{
    internal class OptionsCodeExportOperation : IExecutionPhases
    {
        public Task<Status> DryAsync(Parameters parameters)
        {
            return Task.FromResult(Status.Success);
        }

        public Task<Status> RealAsync(Parameters parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace Xecrets.Cli.Public");
            sb.AppendLine("{");

            sb.AppendLine($"    public enum {nameof(XfOpCode)}");
            sb.AppendLine("    {");
            foreach (XfOpCode opCode in Enum.GetValues(typeof(XfOpCode)))
            {
                sb.Append("        ").Append(Enum.GetName(opCode)).Append(" = ").Append((int)opCode).AppendLine(",");
            }
            sb.AppendLine("    }");
            sb.AppendLine();

            sb.AppendLine($"    public enum {nameof(XfStatusCode)}");
            sb.AppendLine("    {");
            foreach (XfStatusCode statusCode in Enum.GetValues(typeof(XfStatusCode)))
            {
                sb.Append("        ").Append(Enum.GetName(statusCode)).Append(" = ").Append((int)statusCode).AppendLine(",");
            }
            sb.AppendLine("    }");
            sb.AppendLine();

            sb.AppendLine("    public class XfCliApi");
            sb.AppendLine("    {");
            string version = $"{parameters.Parser.ExportableCliVersion.Major},{parameters.Parser.ExportableCliVersion.Minor}";
            sb.Append("        public Version XfCliVersion { get; } = new Version(").Append(version).AppendLine(");");
            sb.AppendLine();
            sb.AppendLine("        public List<(int, string, string)> XfCliOptions { get; } =");
            sb.AppendLine("        [");
            foreach (XfOptionDefinition def in parameters.Parser.ExportableCliDefinitions)
            {
                sb.AppendLine($"            ({def.OpCode}, \"{def.Prototype}\", \"{def.Description}\"),");
            }
            sb.AppendLine("        ];");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            parameters.Logger.Log(new Status(parameters, sb.ToString()));

            return Task.FromResult(Status.Success);
        }
    }
}
