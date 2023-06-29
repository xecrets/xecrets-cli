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

using Xecrets.File.Licensing.Abstractions;

namespace Xecrets.File.Licensing.Implementation
{
    public class LicenseBlurb
    {
        private readonly INewLocator _newLocator;
        private readonly string _unlicensedBlurb;
        private readonly string _expiredBlurb;
        private readonly string _licensedBlurb;
        private readonly string _invalidBlurb;

        public LicenseBlurb(INewLocator newLocator, string unlicensedBlurb, string expiredBlurb, string licensedBlurb, string invalidBlurb)
        {
            _newLocator = newLocator;
            _unlicensedBlurb = unlicensedBlurb;
            _expiredBlurb = expiredBlurb;
            _licensedBlurb = licensedBlurb;
            _invalidBlurb = invalidBlurb;
        }

        public override string ToString()
        {
            return GetBlurb();
        }

        private string GetBlurb()
        {
            LicenseStatus status = _newLocator.New<ILicense>().Status();

            return status switch
            {
                LicenseStatus.Gpl or LicenseStatus.Unlicensed => _unlicensedBlurb,

                LicenseStatus.Expired => FillLicenseInfo(_expiredBlurb),

                LicenseStatus.Valid => FillLicenseInfo(_licensedBlurb),

                LicenseStatus.Invalid => FillLicenseInfo(_invalidBlurb),

                _ => throw new InvalidOperationException($"Unexpected {nameof(LicenseStatus)} value '{status}'."),
            };
        }

        private string FillLicenseInfo(string text)
        {
            var subscription = _newLocator.New<ILicense>().Subscription();
            return text
                .Replace("{licensee}", subscription.Licensee)
                .Replace("{expiration}", subscription.ExpirationUtc.ToString(CultureInfo.GetCultureInfo("en-US")))
                .Replace("{product}", subscription.Product)
                .Replace(@"\n", Environment.NewLine);
        }
    }
}
