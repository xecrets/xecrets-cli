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

using System.Security.Cryptography;
using System.Text;

using AxCrypt.Abstractions;
using AxCrypt.Core.Runtime;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.Cli.Implementation;

internal class XecretsCliReport(string reportFileName, long maxReportFileLength) : IReport
{
    public void Exception(Exception ex)
    {
        if (IgnoreException(ex))
        {
            return;
        }

        string fullName = Path.Combine(New<WorkFolder>().FileInfo.FullName, reportFileName);

        AxCryptException? ace = ex as AxCryptException;
        string displayContext = ace?.DisplayContext ?? string.Empty;

        StringBuilder sb = new();
        sb.AppendFormat("----------- Exception at {0} -----------", New<INow>().Utc.ToString("u")).AppendLine();
        sb.AppendLine(displayContext);
        sb.AppendLine(ex?.ToString() ?? "(null)");
        sb.AppendLine();

        AssertReportFileSize(fullName, sb.Length);

        if (!File.Exists(fullName))
        {
            StringBuilder preamble = new();
            preamble.AppendLine("This is a log of caught exceptions in Xecrets Cli.");
            preamble.AppendLine();

            preamble.Append(sb);
            sb = preamble;
        }
        File.AppendAllText(fullName, sb.ToString());
    }

    private static bool IgnoreException(Exception ex)
    {
        if (ex is CryptographicException ce && ce.HResult == unchecked((int)0xc100000d))
        {
            return true;
        }
        return false;
    }

    private void AssertReportFileSize(string fullName, int length)
    {
        if (!File.Exists(fullName) || new FileInfo(fullName).Length + length <= maxReportFileLength)
        {
            return;
        }
        string backupName = Path.ChangeExtension(fullName, ".1" + Path.GetExtension(fullName));
        File.Move(fullName, backupName, overwrite: true);
        return;
    }

    public void Open()
    {
        throw new NotImplementedException();
    }
}
