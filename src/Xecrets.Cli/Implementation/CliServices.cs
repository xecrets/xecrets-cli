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

using Xecrets.Cli.Abstractions;
using Xecrets.Cli.Log;
using Xecrets.Licensing.Abstractions;
using Xecrets.Slip39;

namespace Xecrets.Cli.Implementation;

internal sealed class CliServices(
    ICoreServices coreServices,
    TimeProvider timeProvider,
    CancelSignal cancelSignal,
    ConsoleOut consoleOut,
    Splash splash,
    IBuildUtc buildUtc,
    ILicense license,
    ILicenseCandidates licenseCandidates,
    NewLocator licenseLocator,
    IDesktopServices desktopServices,
    IInUseBy inUseBy,
    IShamirsSecretSharing shamirsSecretSharing)
{
    public ICoreServices CoreServices { get; } = coreServices;

    public TimeProvider TimeProvider { get; } = timeProvider;

    public CancelSignal CancelSignal { get; } = cancelSignal;

    public ConsoleOut ConsoleOut { get; private set; } = consoleOut;

    public Splash Splash { get; } = splash;

    public IBuildUtc BuildUtc { get; } = buildUtc;

    public ILicense License { get; } = license;

    public ILicenseCandidates LicenseCandidates { get; } = licenseCandidates;

    public IDesktopServices DesktopServices { get; } = desktopServices;

    public IInUseBy InUseBy { get; } = inUseBy;

    public IShamirsSecretSharing ShamirsSecretSharing { get; } = shamirsSecretSharing;

    public void UseConsoleOut(TextWriter writer) => ConsoleOut = new ConsoleOut(writer);

    public void UseLicenseExpiration(ILicenseExpiration licenseExpiration) => licenseLocator.UseLicenseExpiration(licenseExpiration);
}
