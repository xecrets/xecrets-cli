﻿#region Coypright and GPL License

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

using System.Globalization;

using Xecrets.File.Cli.Properties;
using Xecrets.File.Licensing.Abstractions;
using Xecrets.File.Licensing.Implementation;

using static AxCrypt.Abstractions.TypeResolve;

namespace Xecrets.File.Cli.Log
{
    internal class Splash
    {
        private readonly string _splash;

        private bool _written;

        public Splash(string splash)
        {
            string runtime = OperatingSystem.IsLinux()
                ? "linux-x64"
                : OperatingSystem.IsMacOS()
                    ? "osx-x64"
                    : OperatingSystem.IsWindows()
                        ? "win-x64"
                        : "unknown";
            string buildUtc = New<IBuildUtc>().BuildUtcText;
            _splash = splash
                .Replace("{gpl} ", New<IBuildUtc>().IsGplBuild ? "GPL " : string.Empty)
                .Replace("{version}", GetType().Assembly.GetName().Version?.ToString() ?? "0.0.0.0")
                .Replace("{buildutc}", buildUtc)
                .Replace("{runtime}", runtime)
                .Replace("{blurb}", New<LicenseBlurb>().ToString());
        }

        public void Write(Action<string> splashWriter)
        {
            if (!_written)
            {
                _written = true;
                splashWriter(_splash);
            }
        }

        public void Clear()
        {
            _written = true;
        }
    }
}
