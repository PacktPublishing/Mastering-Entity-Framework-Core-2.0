using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MasteringEFCore.MultiTenancy.Starter.Helpers
{
    public class Cryptography
    {
        private static readonly Lazy<Cryptography> _instance = new Lazy<Cryptography>();
        public static Cryptography Instance => _instance.Value;

        public string HashPassword(string password)
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var salt = new byte[128 / 8];
                randomNumberGenerator.GetBytes(salt);
                return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 15000,
                    numBytesRequested: 256 / 8));
            }
        }
    }
}
