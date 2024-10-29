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

using System.Runtime.CompilerServices;
using System.Text;

using AxCrypt.Abstractions;
using AxCrypt.Abstractions.Algorithm;
using AxCrypt.Core;
using AxCrypt.Core.Crypto;
using AxCrypt.Core.Crypto.Asymmetric;
using AxCrypt.Core.IO;
using AxCrypt.Core.Portable;
using AxCrypt.Core.Runtime;
using AxCrypt.Core.UI;
using AxCrypt.Mono;
using AxCrypt.Mono.Portable;

using Xecrets.Cli;
using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Implementation;
using Xecrets.Cli.Log;
using Xecrets.Cli.Properties;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;
using Xecrets.Licensing.Abstractions;
using Xecrets.Licensing.Implementation;
using Xecrets.Net.Api.Implementation;
using Xecrets.Net.Core;
using Xecrets.Net.Core.Crypto.Asymmetric;
using Xecrets.Slip39;

using static AxCrypt.Abstractions.TypeResolve;

[assembly: InternalsVisibleTo("Xecrets.Cli.Test")]

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var options = new OptionsParser(Environment.CommandLine);

TypeMap.Register.Singleton(() => new CancelSignal());
TypeMap.Register.Singleton<IRuntimeEnvironment>(() => new AxCrypt.Mono.RuntimeEnvironment(".axx"));
TypeMap.Register.Singleton<IPortableFactory>(() => new PortableFactory());
TypeMap.Register.Singleton<ILogging>(() => new Logging());
TypeMap.Register.Singleton<IPlatform>(() => new MonoPlatform());
TypeMap.Register.Singleton<IPath>(() => new PortablePath());

TypeMap.Register.New<string, IDataStore>((path) => new DataStore(path));
TypeMap.Register.New<string, IDataContainer>((path) => new DataContainer(path));
TypeMap.Register.New<string, IDataItem>((path) => DataItem.Create(path));

string workFolderPath = Path.Combine(Path.GetTempPath(), "Axantum/XecretsCli".Replace('/', Path.DirectorySeparatorChar));
Directory.CreateDirectory(workFolderPath);

TypeMap.Register.Singleton(() => new UserSettingsVersion());
TypeMap.Register.Singleton(() => new UserSettings(New<ISettingsStore>(), New<IterationCalculator>()));
TypeMap.Register.Singleton<IRandomGenerator>(() => new RandomGenerator());
TypeMap.Register.Singleton<IAsymmetricFactory>(() => new NetAsymmetricFactory());
TypeMap.Register.Singleton(() => new CryptoFactory([]));

TypeMap.Register.New(() => new AxCryptFactory());
TypeMap.Register.New(() => new AxCryptFile());
TypeMap.Register.New<int, Salt>((size) => new Salt(size));
TypeMap.Register.New(() => new IterationCalculator());
TypeMap.Register.Singleton<IStringSerializer>(() => new SystemTextJsonStringSerializer(JsonSourceGenerationContext.CreateJsonSerializerContext(New<IAsymmetricFactory>().GetConverters())));

TypeMap.Register.Singleton(() => new WorkFolder(workFolderPath), () => { });

TypeMap.Register.Singleton(() => new FileLocker());
TypeMap.Register.New(() => PortableFactory.AxCryptHMACSHA1());
TypeMap.Register.New<HMACSHA512>(() => new AxCrypt.Mono.Cryptography.HMACSHA512Wrapper(new AxCrypt.Desktop.Cryptography.HMACSHA512CryptoServiceProvider()));
TypeMap.Register.New<Aes>(() => new AxCrypt.Mono.Cryptography.AesWrapper(System.Security.Cryptography.Aes.Create()));
TypeMap.Register.New<Sha1>(() => PortableFactory.SHA1Managed());
TypeMap.Register.New<Sha256>(() => PortableFactory.SHA256Managed());
TypeMap.Register.New<CryptoStreamBase>(() => PortableFactory.CryptoStream());
TypeMap.Register.New(() => PortableFactory.RandomNumberGenerator());
TypeMap.Register.New<ISystemCryptoPolicy>(() => new ProCryptoPolicy());

TypeMap.Register.Singleton<IReport>(() => new Report(Resolve.WorkFolder.FileInfo.FullName, 1000000));
TypeMap.Register.Singleton(() => TimeProvider.System);
TypeMap.Register.Singleton<INow>(() => new TimeProviderNow());

// Avoid JSON deserialization errors when the user settings file is empty.
IDataStore settings = Resolve.WorkFolder.FileInfo.FileItemInfo("UserSettings.txt");
if (settings.IsAvailable && settings.Length() == 0)
{
    settings.Delete();
}
TypeMap.Register.Singleton<ISettingsStore>(() => new SettingsStore(settings));

TypeMap.Register.Singleton(() => new ConsoleOut(Console.Error));
TypeMap.Register.Singleton<IEmailParser>(() => new EmailParser());
TypeMap.Register.Singleton(() => new Splash(Resource.splash));
TypeMap.Register.Singleton(() => new RewindableStdinStream());
TypeMap.Register.New<string, IStandardIoDataStore>((path) => new StandardIoDataStore(path));
TypeMap.Register.Singleton<IFileVerify>(() => new FileVerify());
TypeMap.Register.Singleton<IBuildUtc>(() => new BuildUtc(typeof(Program)));
TypeMap.Register.Singleton<ILicense>(() => new License(new NewLocator(), issuer: "xecrets@axantum.com", claim: "xflic.axantum.com", [Resource.LicensePublicKeyProduction, Resource.LicensePublicKeyTest], ["cli", "sdk"]));
TypeMap.Register.Singleton<ILicenseCandidates>(() => new LicenseCandidates());
TypeMap.Register.Singleton<ILicenseExpiration>(() => new LicenseExpirationByBuildTime(new NewLocator()));
TypeMap.Register.Singleton(() => new LicenseBlurb(new NewLocator(), Resource.GplBlurb, Resource.UnlicensedBlurb, Resource.LicensedExpiredDownloadBlurb, Resource.LicensedDownloadBlurb, Resource.LicenseNotValidForProductBlurb));

TypeMap.Register.Singleton<IShamirsSecretSharing>(() => new ShamirsSecretSharing(new StrongRandom()));

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
    File.WriteAllText(parameters.CrashLogFile, $"Cli status code: '{status.StatusCode}' ({(int)status.StatusCode})." +
        (status.Arg1.Length == 0 ? string.Empty : $"{Environment.NewLine}Arg1 = '{status.Arg1}'") +
        (status.Arg2.Length == 0 ? string.Empty : $"{Environment.NewLine}Arg2 = '{status.Arg2}'") +
        Environment.NewLine + Environment.NewLine + status.Message);
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
