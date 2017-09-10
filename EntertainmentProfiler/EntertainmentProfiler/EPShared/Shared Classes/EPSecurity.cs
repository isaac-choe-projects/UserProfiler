using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EPShared.Shared_Classes
{
    public static class EPSecurity
    {
        private const int SALT_SIZE = 8;
        private const int NUM_ITERATIONS = 1000;

        private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        /// &lt;summary>
        /// Creates a signature for a password.
        /// &lt;/summary>
        /// &lt;param name="password">The password to hash.&lt;/param>
        /// &lt;returns>the "salt:hash" for the password.&lt;/returns>
        public static string CreatePasswordSalt(string password)
        {
            byte[] buf = new byte[SALT_SIZE];
            rng.GetBytes(buf);
            string salt = Convert.ToBase64String(buf);

            Rfc2898DeriveBytes deriver2898 = new Rfc2898DeriveBytes(password.Trim(), buf, NUM_ITERATIONS);
            string hash = Convert.ToBase64String(deriver2898.GetBytes(16));
            return salt + ':' + hash;
        }

        /// &lt;summary>
        /// Validate if a password will generate the passed in salt:hash.
        /// &lt;/summary>
        /// &lt;param name="password">The password to validate.&lt;/param>
        /// &lt;param name="saltHash">The "salt:hash" this password should generate.&lt;/param>
        /// &lt;returns>true if we have a match.&lt;/returns>
        public static bool IsPasswordValid(string password, string saltHash)
        {
            string[] parts = saltHash.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)

                return false;
            byte[] buf = Convert.FromBase64String(parts[0]);
            Rfc2898DeriveBytes deriver2898 = new Rfc2898DeriveBytes(password.Trim(), buf, NUM_ITERATIONS);
            string computedHash = Convert.ToBase64String(deriver2898.GetBytes(16));
            return parts[1].Equals(computedHash);
        }
    }

}
