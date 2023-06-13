#region Coypright and GPL License

/*
 * Xecrets File Licensing - Copyright 2023, Svante Seleborg, All Rights Reserved.
 *
 * This code file may be used by Xecrets File Cli, parts of which in turn are derived from AxCrypt as licensed under GPL v3 or later.
 * 
 * However, this code is not derived from AxCrypt and is separately copyrighted and only licensed as follows unless
 * explicitly licensed otherwise. If you use any part of this code in your software, please see https://www.gnu.org/licenses/
 * for details of what this means for you.
 *
 * Xecrets File Licensing is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 *
 * Xecrets File Licensing is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along with Xecrets File Cli.  If not, see <https://www.gnu.org/licenses/>.
 *
 * The source repository can be found at https://github.com/ please go there for more information, suggestions and
 * contributions. You may also visit https://www.axantum.com for more information about the author.
*/

#endregion Coypright and GPL License

using System.Globalization;
using System.Reflection;

using Xecrets.File.Licensing.Abstractions;

namespace Xecrets.File.Licensing.Implementation
{
    public class BuildUtc : IBuildUtc
    {
        private const string UseExecutableDateTime = "UseExecutableDateTime";

        private readonly Assembly _assembly;

        private readonly string _buildUtcText;

        private readonly string _assemblyBuildUtcText;

        private readonly bool _isGplBuild;

        public BuildUtc(Type assemblyType)
        {
            _assembly = assemblyType.Assembly;
            _assemblyBuildUtcText = GetAssemblyBuildUtcText();
            _isGplBuild = _assemblyBuildUtcText == UseExecutableDateTime;
            _buildUtcText = GetBuildUtcText();
        }

        public string BuildUtcText => _buildUtcText;

        public bool IsGplBuild => _isGplBuild;

        private string GetBuildUtcText()
        {
            string GetExecutableDateTime()
            {
                string name = Path.Combine(System.AppContext.BaseDirectory, _assembly.GetName().Name ?? throw new InvalidOperationException("Unexpected null assembly name."));
                name += System.IO.File.Exists(name + ".exe") ? ".exe" : (System.IO.File.Exists(name + ".dll") ? ".dll" : string.Empty);
                string dateTime = System.IO.File.GetLastWriteTimeUtc(name).ToString(CultureInfo.GetCultureInfo("en-US"));
                return dateTime;
            }

            string buildUtcText = _isGplBuild ? GetExecutableDateTime() : _assemblyBuildUtcText;

            return buildUtcText;
        }

        private string GetAssemblyBuildUtcText()
        {
            string assemblyBuildUtcText = _assembly.GetCustomAttributes<AssemblyMetadataAttribute>().Where(a => a.Key == "BuildUtc").Select(a => a.Value).First()
                ?? throw new InvalidOperationException("Internal error, missing AssemblyMetadataAttribute BuildUtc");

            if (assemblyBuildUtcText != UseExecutableDateTime)
            {
                return assemblyBuildUtcText;
            }

            string? buildUtcEnv = Environment.GetEnvironmentVariable("XF_BUILDUTC");
            if (string.IsNullOrEmpty(buildUtcEnv))
            {
                return assemblyBuildUtcText;
            }

            return buildUtcEnv;
        }
    }
}
