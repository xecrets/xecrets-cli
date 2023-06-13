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

using System.Security.Cryptography;

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using Xecrets.File.Licensing.Abstractions;

namespace Xecrets.File.Licensing.Implementation
{
    public class License : ILicense
    {
        private const string Issuer = "xecrets@axantum.com";

        private const string ProductClaimName = "xflic.axantum.com";

        private readonly IEnumerable<string> _validProducts;

        private readonly INewLocator _newLocator;

        private readonly IEnumerable<string> _publicKeys;

        private LicenseSubscription _subscription = LicenseSubscription.Empty;

        public License(INewLocator newLocator, IEnumerable<string> publicKeys, IEnumerable<string> validProducts)
        {
            _newLocator = newLocator;
            _publicKeys = publicKeys;
            _validProducts = validProducts;
        }

        public void LoadFrom(IEnumerable<string> licenseCandidates)
        {
            LicenseToken = GetBestValidLicenseToken(licenseCandidates);
            _subscription = Subscription(LicenseToken);
        }

        public string GetBestValidLicenseToken(IEnumerable<string> candidates)
        {
            candidates = candidates.Where(c => _newLocator.New<ILicenseCandidates>().IsCandidate(c));
            if (!candidates.Any())
            {
                return string.Empty;
            }

            List<string> validSignedLicenses = new List<string>();

            foreach (string publicKey in _publicKeys)
            {
                ValidSignedLicenses(publicKey, candidates, validSignedLicenses);
            }

            if (!validSignedLicenses.Any())
            {
                return string.Empty;
            }

            var handler = new JsonWebTokenHandler();
            return validSignedLicenses.OrderByDescending(l => handler.ReadToken(l).ValidTo).First();
        }

        public string LicenseToken { get; private set; } = string.Empty;

        private bool IsExpired(LicenseSubscription licenseSubscription)
        {
                return _newLocator.New<ILicenseExpiration>().IsExpired(licenseSubscription.ExpirationUtc);
        }

        public LicenseSubscription Subscription()
        {
            return _subscription;
        }

        public LicenseSubscription Subscription(string signedLicense)
        {
            signedLicense = signedLicense.Trim();
            if (signedLicense.Length == 0)
            {
                return LicenseSubscription.Empty;
            }

            var handler = new JsonWebTokenHandler();
            JsonWebToken token = (JsonWebToken)handler.ReadToken(signedLicense);

            return new LicenseSubscription(token.ValidTo, token.Audiences.First(), token.GetClaim(ProductClaimName).Value);
        }

        public LicenseStatus Status() => Status(_subscription);

        public LicenseStatus Status(LicenseSubscription subscription)
        {
            bool isGplBuild = _newLocator.New<IBuildUtc>().IsGplBuild;

            if (!isGplBuild && subscription == LicenseSubscription.Empty)
            {
                return LicenseStatus.Unlicensed;
            }

            if (!isGplBuild && IsExpired(subscription))
            {
                return LicenseStatus.Expired;
            }

            if (_validProducts.Contains(subscription.Product) && !IsExpired(subscription))
            {
                return LicenseStatus.Valid;
            }

            if (isGplBuild)
            {
                return LicenseStatus.Gpl;
            }

            return LicenseStatus.Invalid;
        }

        private static void ValidSignedLicenses(string keyPem, IEnumerable<string> candidates, List<string> validSignedLicenses)
        {
            JsonWebTokenHandler handler = new JsonWebTokenHandler();
            var testKey = ECDsa.Create();
            testKey.ImportFromPem(keyPem);

            foreach (string candidate in candidates)
            {
                string trimmedCandidate = candidate.Trim();
                TokenValidationResult result = handler.ValidateToken(trimmedCandidate, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidIssuer = Issuer,
                    IssuerSigningKey = new ECDsaSecurityKey(testKey),
                    ValidAlgorithms = new string[] { "ES256" },
                    ValidateLifetime = false,
                });

                if (result.IsValid)
                {
                    validSignedLicenses.Add(trimmedCandidate);
                }
            }
        }
    }
}
