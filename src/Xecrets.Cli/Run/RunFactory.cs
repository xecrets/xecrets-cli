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

using AxCrypt.Abstractions;

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Operation;
using Xecrets.Cli.Public;

namespace Xecrets.Cli.Run
{
    internal abstract class RunFactory(Parameters parameters)
    {
        readonly Dictionary<XfOpCode, Func<IExecutionPhases>> _operationTable = new()
        {
            { XfOpCode.ArgumentMarkdown, () => new ArgumentMarkdownOperation() },
            { XfOpCode.AsciiArmor, () => new AsciiArmorOperation() },
            { XfOpCode.CliCrashLog, () => new CliCrashLogOperation() },
            { XfOpCode.OptionsCodeExport, () => new OptionsCodeExportOperation() },
            { XfOpCode.SdkCliVersion, () => new SdkCliVersionOperation() },
            { XfOpCode.Begin, () => new BeginOperation() },
            { XfOpCode.CreateKeyPair, () => new CreateKeyPairOperation() },
            { XfOpCode.CliDebugBreak, () => new CliDebugBreakOperation() },
            { XfOpCode.Compress, () => new CompressOperation() },
            { XfOpCode.DecryptTo, () => new DecryptToOperation() },
            { XfOpCode.DecryptToFolder, () => new DecryptToFolderOperation() },
            { XfOpCode.Echo, () => new EchoOperation() },
            { XfOpCode.EncryptTo, () => new EncryptToOperation() },
            { XfOpCode.End, () => new EndOperation() },
            { XfOpCode.EnvironmentOption, () => new EnvironmentOptionsOperation() },
            { XfOpCode.ExportPublicKey, () => new ExportPublicKeyOperation() },
            { XfOpCode.OptionsFromFile, () => new FileOptionsOperation() },
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
            { XfOpCode.NoAsciiArmor, () => new NoAsciiArmorOperation() },
            { XfOpCode.NoCompress, () => new NoCompressOperation() },
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

        public Parameters Parameters { get; } = parameters;

        private class SomeAction(RunFactory factory, IExecutionPhases methods, XfOpCode opCode) : IOperation
        {
            public async Task<Status> DoAsync()
            {
                Status status;
                try
                {
                    status = factory.Parameters.IsDryRun ? await methods.DryAsync(factory.Parameters) : await methods.RealAsync(factory.Parameters);
                }
                catch (XecretsCliException xfcex)
                {
                    status = new Status(xfcex.Status.StatusCode, xfcex.ToString());
                }
                catch (AxCryptException acex)
                {
                    status = new Status(XfStatusCode.AxCryptException, acex.ToString())
                    {
                        Id = factory.Parameters.TotalsTracker.Id,
                    };
                }
                catch (FileNotFoundException fnfex)
                {
                    status = new Status(XfStatusCode.FileUnavailable, fnfex.ToString())
                    {
                        Id = factory.Parameters.TotalsTracker.Id,
                        File = fnfex.FileName ?? (factory.Parameters.From),
                    };
                }
                catch (Exception ex)
                {
                    status = new Status(XfStatusCode.UnhandledOperationException, ex.ToString())
                    {
                        Id = factory.Parameters.TotalsTracker.Id,
                    };
                }

                status.OpCode = opCode;
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
