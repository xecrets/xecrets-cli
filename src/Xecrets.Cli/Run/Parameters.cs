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

using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.Session;
using AxCrypt.Core.UI;

using Xecrets.Cli.Implementation;
using Xecrets.Cli.Log;
using Xecrets.Cli.Public;

namespace Xecrets.Cli.Run;

internal class Parameters(OptionsParser parser) : IDisposable
{
    public bool IsDryRun { get; set; } = true;

    public bool IsStdoutLog { get; set; } = false;

    public bool Overwrite { get; set; } = false;

    public bool Compress { get; set; } = true;

    public bool AsciiArmor { get; set; } = false;

    public string CrashLogFile { get; set; } = string.Empty;

    public TotalsTracker TotalsTracker { get; } = new TotalsTracker();

    public ILogger Logger => TotalsTracker.Logger;

    public IProgressContext Progress => Logger.Progress;

    public ParsedOp CurrentOp { get; set; } = new ParsedOp(XfOpCode.None);

    public IList<string> Arguments => CurrentOp.Arguments;

    public XfOpCode OpCode => CurrentOp.OpCode;

    public bool Flag => CurrentOp.Flag;

    public string Arg1 => CurrentOp.Arg1;

    public string Arg2 => CurrentOp.Arg2;

    public string Arg3 => CurrentOp.Arg3;

    public KnownPublicKeys LoadedPublicKeys { get; } = new KnownPublicKeys();

    public OptionsParser Parser { get; } = parser;

    public IList<LogOnIdentity> Identities { get; } = [];

    public IList<EmailAddress> SharingEmails { get; } = [];

    public IDictionary<string, object> JwtClaims { get; } = new Dictionary<string, object>();

    public string JwtIssuer { get; set; } = string.Empty;

    public string JwtAudience { get; set; } = string.Empty;

    public string JwtPrivateKeyPem { get; set; } = string.Empty;

    public int JwtDaysUntilExpiration { get; set; } = 0;

    public Slip39Parameters Slip39 { get; set; } = new();

    public IEnumerable<UserPublicKey> PublicKeys
    {
        get
        {
            return Identities.SelectMany(i => i.PublicKeys).Concat(LoadedPublicKeys.PublicKeys);
        }
    }

    public int HelpLevel { get; set; }

    public bool ProgrammaticUse { get; set; }

    public void StartReal()
    {
        IsDryRun = false;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }
    }
}
