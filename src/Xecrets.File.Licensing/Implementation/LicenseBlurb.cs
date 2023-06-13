using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
