using System;
using System.Security.Cryptography;
using System.Text;
using CMSys.Common.Cryptography;

namespace CMSys.Common.Helpers
{
    public static class PasswordHelper
    {
        /// <exception cref="System.ArgumentException"><paramref name="length" /> must be > 0.</exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException">
        ///     The cryptographic service provider (CSP) cannot be acquired.
        /// </exception>
        public static string GenerateSalt(int length)
        {
            Check.ArgumentSatisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

            using var cryptoService = new RNGCryptoServiceProvider();
            var saltBytes = new byte[length];
            cryptoService.GetNonZeroBytes(saltBytes);
            return Encoding.Unicode.GetString(saltBytes);
        }

        /// <exception cref="System.ArgumentNullException"></exception>
        public static string ComputeHash(string password, string salt)
        {
            Check.ArgumentNotNull(password, nameof(password));
            Check.ArgumentNotNull(salt, nameof(salt));

            var hashAlgorithm = new Sha512Hash();
            return hashAlgorithm.CalculateHash(password + salt);
        }

        /// <exception cref="System.ArgumentException"><paramref name="length" /> must be > 0.</exception>
        public static string Generate(int length)
        {
            Check.ArgumentSatisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

            var randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Trim();
            return length <= randomString.Length ? randomString[..length] : randomString;
        }
    }
}