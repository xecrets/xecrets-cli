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

using System.Runtime.CompilerServices;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using Xecrets.Cli;
using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Implementation;
using Xecrets.Cli.Log;
using Xecrets.Cli.Properties;
using Xecrets.Cli.Public;
using Xecrets.Cli.Run;
using Xecrets.Core.Desktop;
using Xecrets.Core.Public;
using Xecrets.Licensing.Abstractions;
using Xecrets.Licensing.Implementation;
using Xecrets.Slip39;

[assembly: InternalsVisibleTo("Xecrets.Cli.Test")]

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

OptionsParser parser = new(Environment.CommandLine);

var workFolder = parser.WorkFolder.Length > 0
    ? parser.WorkFolder.NormalizeDirectorySeparator()
    : Path.Combine(Path.GetTempPath(), "Axantum/XecretsCli".NormalizeDirectorySeparator());

Directory.CreateDirectory(workFolder);

await using ServiceProvider serviceProvider = new ServiceCollection()
    .AddXecretsCore()
    .AddXecretsCoreDesktop(workFolder, "xecrets-cli-settings.json", "xecrets-cli-exceptions.txt",
        maxReportFileLength: 1024 * 1024)
    .AddLicensing()
    .AddSingleton(TimeProvider.System)
    .AddSingleton<CancelSignal>()
    .AddSingleton(_ => new ConsoleOut(Console.Error))
    .AddSingleton<IInUseBy>(_ => Platform.IsWindows ? new InUseByWindows() : new InUseByUnsupported())
    .AddSingleton<IShamirsSecretSharing>(_ => new ShamirsSecretSharing(new StrongRandom()))
    .AddSingleton<CliServices>()
    .BuildServiceProvider();

CliServices cliServices = serviceProvider.GetRequiredService<CliServices>();
await cliServices.License.LoadFromAsync(cliServices.LicenseCandidates.CandidatesFromFiles(
    cliServices.BuildUtc.IsGplBuild ? [] : Directory.GetFiles(AppContext.BaseDirectory, "*.txt")));

Parameters parameters = new(parser, cliServices);

Status status;
try
{
    using (Executor executor = new(parameters))
    {
        status = await executor.RunAsync();

        if (!status.IsSuccess)
        {
            parameters.Logger.Log(status);
        }
    }

    if (!status.IsSuccess || !parser.ParsedOps.Any())
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
                                               (status.Arg1.Length == 0
                                                   ? string.Empty
                                                   : $"{Environment.NewLine}Arg1 = '{status.Arg1}'") +
                                               (status.Arg2.Length == 0
                                                   ? string.Empty
                                                   : $"{Environment.NewLine}Arg2 = '{status.Arg2}'") +
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

static file class Extensions
{
    public static IServiceCollection AddLicensing(this IServiceCollection services)
    {
        services
            .AddSingleton<IBuildUtc>(_ => new BuildUtc(typeof(Program)))
            .AddSingleton<ILicenseCandidates, LicenseCandidates>()
            .AddSingleton<NewLocator>() // Needed for the ILicenseExpiration override, as it is used in the NewLocator constructor
            .AddSingleton<INewLocator>(sp => sp.GetRequiredService<NewLocator>())
            .AddSingleton<ILicenseExpiration>(sp => new LicenseExpirationByBuildTime(sp.GetRequiredService<INewLocator>()))
            .AddSingleton<ILicense>(sp => new License(sp.GetRequiredService<INewLocator>(), issuer: "xecrets@axantum.com",
                claim: "xflic.axantum.com", [Resource.LicensePublicKeyProduction, Resource.LicensePublicKeyTest],
                ["cli", "sdk"]))
            .AddSingleton(sp => new LicenseBlurb(sp.GetRequiredService<INewLocator>(), Resource.GplBlurb,
                Resource.UnlicensedBlurb, Resource.LicensedExpiredDownloadBlurb, Resource.LicensedDownloadBlurb,
                Resource.LicenseNotValidForProductBlurb))
            .AddSingleton(sp =>
                new Splash(Resource.splash, sp.GetRequiredService<IBuildUtc>(), sp.GetRequiredService<LicenseBlurb>()));

        return services;
    }
}
