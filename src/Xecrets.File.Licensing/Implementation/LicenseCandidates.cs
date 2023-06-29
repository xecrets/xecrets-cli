#region Coypright and GPL License

/*
 * Xecrets File Licensing - Copyright © 2022-2023, Svante Seleborg, All Rights Reserved.
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

using System.Text.RegularExpressions;

using Xecrets.File.Licensing.Abstractions;

namespace Xecrets.File.Licensing.Implementation
{
    public partial class LicenseCandidates : ILicenseCandidates
    {
        public IEnumerable<string> CandidatesFromFiles(IEnumerable<string> files)
        {
            List<string> candidateLicenses = new List<string>();

            foreach (string file in files)
            {
                if (TryCandidateFile(file, out string candidateLicense))
                {
                    candidateLicenses.Add(candidateLicense);
                }
            }

            return candidateLicenses;
        }

        public bool TryCandidateFile(string file, out string candidateLicense)
        {
            candidateLicense = string.Empty;
            FileInfo fileInfo = new FileInfo(file);
            if (fileInfo.Length > 1024)
            {
                return false;
            }

            string fileText = System.IO.File.ReadAllText(file);
            if (IsCandidate(fileText))
            {
                candidateLicense = fileText;
                return true;
            }

            return false;
        }

        public bool IsCandidate(string? candidate)
        {
            candidate = candidate?.Trim();
            return !string.IsNullOrEmpty(candidate) && candidate.Length < 1024 && _jwtRegex.IsMatch(candidate);
        }

        private static readonly Regex _jwtRegex = JwtRegex();

        [GeneratedRegex(@"^[-_a-zA-Z0-9]+\.[-_a-zA-Z0-9]+\.[-_a-zA-Z0-9]+$", RegexOptions.Compiled)]
        private static partial Regex JwtRegex();
    }
}
