#region Coypright and GPL License

/*
 * Xecrets File Cli - Copyright © 2022-2023, Svante Seleborg, All Rights Reserved.
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

using AxCrypt.Abstractions;

using Xecrets.File.Cli.Abstractions;
using Xecrets.File.Cli.Operation;
using Xecrets.File.Cli.Public;

namespace Xecrets.File.Cli.Run
{
    internal abstract class RunFactory
    {
        readonly Dictionary<XfOpCode, Func<IExecutionPhases>> _operationTable = new Dictionary<XfOpCode, Func<IExecutionPhases>>()
        {
            { XfOpCode.ArgumentMarkdown, () => new ArgumentMarkdownOperation() },
            { XfOpCode.CliCrashLog, () => new CliCrashLogOperation() },
            { XfOpCode.OptionsCodeExport, () => new OptionsCodeExportOperation() },
            { XfOpCode.SdkCliVersion, () => new SdkCliVersionOperation() },
            { XfOpCode.CreateKeyPair, () => new CreateKeyPairOperation() },
            { XfOpCode.CliDebugBreak, () => new CliDebugBreakOperation() },
            { XfOpCode.DecryptTo, () => new DecryptToOperation() },
            { XfOpCode.DecryptToFolder, () => new DecryptToFolderOperation() },
            { XfOpCode.Echo, () => new EchoOperation() },
            { XfOpCode.EncryptTo, () => new EncryptToOperation() },
            { XfOpCode.ExportPublicKey, () => new ExportPublicKeyOperation() },
            { XfOpCode.GplLicense, () => new GplLicenseOperation() },
            { XfOpCode.Help, () => new HelpOperation() },
            { XfOpCode.Id, () => new IdOperation() },
            { XfOpCode.SdkJsonLog, () => new SdkJsonLogOperation() },
            { XfOpCode.JwtAudience, () => new JwtAudienceOperation() },
            { XfOpCode.JwtClaims, () => new JwtClaimsOperation() },
            { XfOpCode.JwtCreateKeyPair , () => new JwtCreateKeyPairOperation() },
            { XfOpCode.JwtIssuer, () => new JwtIssuerOperation() },
            { XfOpCode.JwtPrivateKey, () => new JwtUseKeyPairOperation() },
            { XfOpCode.JwtSign, () => new JwtSignOperation() },
            { XfOpCode.JwtVerify, () => new JwtVerifyOperation() },
            { XfOpCode.CliLicense, () => new CliLicenseOperation() },
            { XfOpCode.LoadPublicKey, () => new LoadPublicKeyOperation() },
            { XfOpCode.NoLog, () => new NoLogOperation() },
            { XfOpCode.None, () => new NoOperation() },
            { XfOpCode.NoOverwrite, () => new NoOverwriteOperation() },
            { XfOpCode.NoProgress, () => new NoProgressOperation() },
            { XfOpCode.Overwrite, () => new OverwriteOperation() },
            { XfOpCode.Password, () => new PasswordOperation() },
            { XfOpCode.CliPlatform, () => new CliPlatformOperation() },
            { XfOpCode.ProgressLog, () => new ProgressLogOperation() },
            { XfOpCode.Stdout, () => new StdoutOperation() },
            { XfOpCode.TextLog, () => new TextLogOperation() },
            { XfOpCode.UseKeyPair, () => new UseKeyPairOperation() },
            { XfOpCode.UsePublicKey, () => new UsePublicKeyOperation() },
            { XfOpCode.Wipe, () => new WipeOperation() },
        };

        public Parameters Parameters { get; }

        public RunFactory(Parameters parameters)
        {
            Parameters = parameters;
        }

        private class SomeAction : IOperation
        {
            private readonly RunFactory _factory;

            private readonly IExecutionPhases _methods;

            private readonly XfOpCode _opCode;

            public SomeAction(RunFactory factory, IExecutionPhases methods, XfOpCode opCode)
            {
                _factory = factory;
                _methods = methods;
                _opCode = opCode;
            }

            public Status Do()
            {
                Status status;
                try
                {
                    status = _factory.Parameters.IsDryRun ? _methods.Dry(_factory.Parameters) : _methods.Real(_factory.Parameters);
                }
                catch (XecretsFileCliException xfcex)
                {
                    status = new Status(xfcex.Status.StatusCode, xfcex.ToString());
                }
                catch (AxCryptException acex)
                {
                    status = new Status(XfStatusCode.AxCryptException, acex.ToString())
                    {
                        Id = _factory.Parameters.TotalsTracker.Id,
                    };
                }
                catch (FileNotFoundException fnfex)
                {
                    status = new Status(XfStatusCode.FileUnavailable, fnfex.ToString())
                    {
                        Id = _factory.Parameters.TotalsTracker.Id,
                        File = fnfex.FileName ?? (_factory.Parameters.From),
                    };
                }
                catch (Exception ex)
                {
                    status = new Status(XfStatusCode.UnhandledOperationException, ex.ToString())
                    {
                        Id = _factory.Parameters.TotalsTracker.Id,
                    };
                }

                status.OpCode = _opCode;
                return status;
            }
        }

        public IOperation Create(XfOpCode opCode)
        {
            if (_operationTable.TryGetValue(opCode, out var actionFunc))
            {
                Parameters.TotalsTracker.ResetLogger();
                return new SomeAction(this, actionFunc(), opCode);
            }

            return new SomeAction(this, new ErrorOperation($"Can't create the appropriate action for {opCode}."), XfOpCode.SdkCliError);
        }
    }
}
