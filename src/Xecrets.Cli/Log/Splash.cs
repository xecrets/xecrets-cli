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

using System.Runtime.InteropServices;

using Xecrets.Core.Public;
using Xecrets.Licensing;
using Xecrets.Licensing.Abstractions;
using Xecrets.Licensing.Implementation;

namespace Xecrets.Cli.Log;

internal class Splash
{
    private readonly string _splash;

    private readonly string _buildUtcText;

    private readonly LicenseBlurb _licenseBlurb;

    private bool _written;

    public Splash(string splash, IBuildUtc buildUtc, LicenseBlurb licenseBlurb)
    {
        _buildUtcText = buildUtc.BuildUtcText;
        _licenseBlurb = licenseBlurb;

        string runtime;
        string archString = RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant();
        if (Platform.IsMacOS)
        {
            runtime = $"macos-{archString}";
        }
        else if (Platform.IsLinux)
        {
            runtime = $"linux-{archString}";
        }
        else if (Platform.IsWindows)
        {
            runtime = $"win-{archString}";
        }
        else
        {
            runtime = $"unknown-{archString}";
        }
        _splash = splash
            .Replace("{gpl} ", buildUtc.IsGplBuild ? "GPL " : string.Empty)
            .Replace("{version}", GetType().Assembly.GetName().Version?.ToString() ?? "0.0.0.0")
            .Replace("{runtime}", runtime);
    }

    public void Write(Action<string> splashWriter)
    {
        if (!_written)
        {
            _written = true;
            string splash = _splash
                .Replace("{buildutc}", _buildUtcText.FromUtc().ToLocal())
                .Replace("{blurb}", _licenseBlurb.ToString());
            splashWriter(splash);
        }
    }

    public void Clear()
    {
        _written = true;
    }
}
