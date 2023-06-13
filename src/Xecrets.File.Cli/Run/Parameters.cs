#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright 2022, Svante Seleborg, All Rights Reserved.
 *
 * This code file is part of Xecrets File Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 * 
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets File Cli is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets File Cli is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets File Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Coypright and GPL License

using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.Session;
using AxCrypt.Core.UI;

using Xecrets.File.Cli.Log;
using Xecrets.File.Cli.Public;

namespace Xecrets.File.Cli.Run
{
    internal class Parameters : IDisposable
    {
        public bool IsDryRun { get; set; } = true;

        public bool IsStdoutLog { get; set; } = false;

        public bool Overwrite { get; set; } = false;

        public TotalsTracker TotalsTracker { get; } = new TotalsTracker();

        public ILogger Logger => TotalsTracker.Logger;

        public IProgressContext Progress => Logger.Progress;

        public ParsedOp CurrentOp { get; set; } = new ParsedOp(XfOpCode.None);

        public IList<string> Arguments => CurrentOp.Arguments;

        public XfOpCode OpCode => CurrentOp.OpCode;

        public bool Flag => CurrentOp.Flag;

        public string Email => CurrentOp.Email;

        public string To => CurrentOp.To;

        public string From => CurrentOp.From;

        public string File => CurrentOp.File;

        public string Value => CurrentOp.Value;

        public KnownPublicKeys LoadedPublicKeys { get; } = new KnownPublicKeys();

        public OptionsParser Parser { get; }

        public IList<LogOnIdentity> Identities { get; } = new List<LogOnIdentity>();

        public IList<EmailAddress> SharingEmails { get; } = new List<EmailAddress>();

        public IDictionary<string, object> JwtClaims { get; } = new Dictionary<string, object>();

        public string JwtIssuer { get; set; } = string.Empty;

        public string JwtAudience { get; set; } = string.Empty;

        public string JwtPrivateKeyPem { get; set; } = string.Empty;

        public int JwtDaysUntilExpiration { get; set; } = 0;

        public IEnumerable<UserPublicKey> PublicKeys
        {
            get
            {
                return Identities.SelectMany(i => i.PublicKeys).Concat(LoadedPublicKeys.PublicKeys);
            }
        }

        public int HelpLevel { get; set; }

        public void StartReal()
        {
            IsDryRun = false;
        }

        public Parameters(OptionsParser parser)
        {
            Parser = parser;
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
}
