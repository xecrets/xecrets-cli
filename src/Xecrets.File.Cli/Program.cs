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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

using AxCrypt.Abstractions;
using AxCrypt.Abstractions.Algorithm;
using AxCrypt.Api;
using AxCrypt.Core;
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Extensions;
using AxCrypt.Core.IO;
using AxCrypt.Core.Runtime;
using AxCrypt.Core.Service;
using AxCrypt.Core.UI;
using AxCrypt.Mono;
using AxCrypt.Mono.Portable;

using Xecrets.File.Cli;
using Xecrets.File.Cli.Abstractions;
using Xecrets.File.Cli.Implementation;
using Xecrets.File.Cli.Log;
using Xecrets.File.Cli.Properties;
using Xecrets.File.Cli.Public;
using Xecrets.File.Cli.Run;
using Xecrets.Licensing.Abstractions;
using Xecrets.Licensing.Implementation;

using static AxCrypt.Abstractions.TypeResolve;

[assembly: InternalsVisibleTo("Xecrets.File.Cli.Test")]

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var options = new OptionsParser(Environment.CommandLine);

RuntimeEnvironment.RegisterTypeFactories();

string workFolderPath = Path.Combine(Path.GetTempPath(), "Axantum/XecretsFileCli".Replace('/', Path.DirectorySeparatorChar));
Directory.CreateDirectory(workFolderPath); 
Resolve.RegisterTypeFactories(workFolderPath, Array.Empty<Assembly>());

TypeMap.Register.Singleton(() => new FileLocker());
TypeMap.Register.Singleton<IProtectedData>(() => new ProtectedDataImplementation("Xecrets.File.Cli"));
TypeMap.Register.New<ILauncher>(() => new Launcher());
TypeMap.Register.New(() => PortableFactory.AxCryptHMACSHA1());
TypeMap.Register.New<HMACSHA512>(() => new AxCrypt.Mono.Cryptography.HMACSHA512Wrapper(new AxCrypt.Desktop.Cryptography.HMACSHA512CryptoServiceProvider()));
TypeMap.Register.New<Aes>(() => new AxCrypt.Mono.Cryptography.AesWrapper(System.Security.Cryptography.Aes.Create()));
TypeMap.Register.New<Sha1>(() => PortableFactory.SHA1Managed());
TypeMap.Register.New<Sha256>(() => PortableFactory.SHA256Managed());
TypeMap.Register.New<CryptoStreamBase>(() => PortableFactory.CryptoStream());
TypeMap.Register.New(() => PortableFactory.RandomNumberGenerator());
TypeMap.Register.New<LogOnIdentity, IAccountService>((LogOnIdentity identity) => new CachingAccountService(new DeviceAccountService(new LocalAccountService(identity, Resolve.WorkFolder.FileInfo), new ApiAccountService(new AxCryptApiClient(identity.ToRestIdentity(), Resolve.UserSettings.RestApiBaseUrl, Resolve.UserSettings.ApiTimeout)))));
TypeMap.Register.New(() => new GlobalApiClient(Resolve.UserSettings.RestApiBaseUrl, Resolve.UserSettings.ApiTimeout));
TypeMap.Register.New(() => new AxCryptApiClient(Resolve.KnownIdentities.DefaultEncryptionIdentity.ToRestIdentity(), Resolve.UserSettings.RestApiBaseUrl, Resolve.UserSettings.ApiTimeout));
TypeMap.Register.New<ISystemCryptoPolicy>(() => new ProCryptoPolicy());
TypeMap.Register.New<ICryptoPolicy>(() => New<LicensePolicy>().Capabilities.CryptoPolicy);

TypeMap.Register.Singleton<IReport>(() => new Report(Resolve.WorkFolder.FileInfo.FullName, 1000000));
TypeMap.Register.Singleton<TimeProvider>(() => TimeProvider.System);
TypeMap.Register.Singleton<INow>(() => new TimeProviderNow());
TypeMap.Register.New<string, IFileWatcher>((path) => new FileWatcher());

// Avoid JSON deserialization errors when the user settings file is empty.
IDataStore settings = Resolve.WorkFolder.FileInfo.FileItemInfo("UserSettings.txt");
if (settings.IsAvailable && settings.Length() == 0)
{
    settings.Delete();
}
TypeMap.Register.Singleton<ISettingsStore>(() => new SettingsStore(settings));

TypeMap.Register.Singleton<IUIThread>(() => new UIThread());
TypeMap.Register.Singleton(() => new ConsoleOut(Console.Error));
TypeMap.Register.Singleton<IEmailParser>(() => new EmailParser());
TypeMap.Register.Singleton(() => new Splash(Resource.splash));
TypeMap.Register.Singleton(() => new RewindableStdinStream());
TypeMap.Register.New<string, IStandardIoDataStore>((path) => new StandardIoDataStore(path));
TypeMap.Register.Singleton<IFileVerify>(() => new FileVerify());
TypeMap.Register.Singleton<IBuildUtc>(() => new BuildUtc(typeof(Program)));
TypeMap.Register.Singleton<ILicense>(() => new License(new NewLocator(), issuer: "xecrets@axantum.com", claim: "xflic.axantum.com", new[] { Resource.LicensePublicKeyProduction, Resource.LicensePublicKeyTest }, [ "cli", "sdk" ]));
TypeMap.Register.Singleton<ILicenseCandidates>(() => new LicenseCandidates());
TypeMap.Register.Singleton<ILicenseExpiration>(() => new LicenseExpirationByBuildTime(new NewLocator()));
TypeMap.Register.Singleton(() => new LicenseBlurb(new NewLocator(), Resource.GplBlurb, Resource.UnlicensedBlurb, Resource.LicensedExpiredDownloadBlurb, Resource.LicensedDownloadBlurb, Resource.LicenseNotValidForProductBlurb));

await New<ILicense>().LoadFromAsync(New<ILicenseCandidates>().CandidatesFromFiles(New<IBuildUtc>().IsGplBuild ? [] : Directory.GetFiles(AppContext.BaseDirectory, "*.txt")));

var parameters = new Parameters(options);

Status status;
try
{
    using (var executor = new Executor(parameters))
    {
        status = await executor.RunAsync();

        if (!status.IsSuccess)
        {
            parameters.Logger.Log(status);
        }
    }

    if (!parameters.Parser.ParseStatus.IsSuccess || !parameters.Parser.ParsedOps.Any())
    {
        parameters.Logger.Log("Use --help to display valid options.");
    }

    parameters.Logger.Log(XfOpCode.CliProgramExit, status);
}
catch (Exception ex)
{
    status = new Status(XfStatusCode.UnhandledRunException, ex.ToString());
    parameters.Logger.Log(XfOpCode.SdkCliError, status);
}

if (status != Status.Success && parameters.CrashLogFile.Length > 0)
{
    File.WriteAllText(parameters.CrashLogFile, $"Cli status code: '{status.StatusCode}' ({(int)status.StatusCode}).{Environment.NewLine}{status.Message}");
}

await WaitForKeyPressedOrTimeoutWhenStartedWithoutArguments(args, status);

Environment.ExitCode = (int)status.StatusCode;
return Environment.ExitCode;

static async Task WaitForKeyPressedOrTimeoutWhenStartedWithoutArguments(string[] args, Status status)
{
    if (args.Length > 0 || status.StatusCode != XfStatusCode.Success)
    {
        return;
    }

    await Task.Run(async () =>
    {
        int totalMsWait = 0;
        while (!Console.IsInputRedirected && !Console.KeyAvailable && totalMsWait < 5000)
        {
            await Task.Delay(100);
            totalMsWait += 100;
        }
    });
}
