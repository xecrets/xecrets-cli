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

using Xecrets.Cli.Public;
using Xecrets.Cli.Run;

namespace Xecrets.Cli;

internal class Status(XfStatusCode code, string message)
{
    public static Status Success = new(XfStatusCode.Success, string.Empty);

    public XfStatusCode StatusCode { get; } = code;

    public XfSubStatusCode SubStatusCode { get; } = XfSubStatusCode.Success;

    public string Message { get; set; } = message ?? throw new ArgumentNullException(nameof(message));

    public XfOpCode OpCode { get; set; } = XfOpCode.None;

    public string Id { get; set; } = string.Empty;

    public string Arg1 { get; set; } = string.Empty;

    public string Arg2 { get; set; } = string.Empty;

    public string CliVersion { get; set; } = string.Empty;

    public string ProgramVersion { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty;

    public string OriginalFileName { get; set; } = string.Empty;

    public string Result { get; set; } = string.Empty;

    public DateTime Utc { get; set; }

    public bool IsSuccess { get { return StatusCode == XfStatusCode.Success; } }

    public Status(string message)
        : this(XfStatusCode.Success, message)
    {
    }

    public Status(Parameters parameters, string message)
        : this(XfStatusCode.Success, parameters, message)
    {
    }

    public Status(XfStatusCode code, XfSubStatusCode subCode, string message)
        : this(code, message)
    {
        SubStatusCode = subCode;
    }

    public Status(XfStatusCode code, XfSubStatusCode subCode, Parameters parameters, string message)
        :this(code, parameters, message)
    {
        SubStatusCode = subCode;
    }

    public Status(XfStatusCode code, Parameters parameters, string message)
        : this(code, message)
    {
        OpCode = parameters.OpCode;
        Id = parameters.TotalsTracker.Id;
        Arg1 = parameters.Arg1;
        Arg2 = parameters.Arg2;
    }

    public override string ToString()
    {
        return Message;
    }
}
