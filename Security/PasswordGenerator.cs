using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ERISCOTools.Security
{
    public class PasswordGenerator
    {
        private HashAlgorithm hashAlgorithm { get; set; }
        private int saltLength { get; set; }

        public PasswordGenerator()
        {
            this.saltLength = 64;
            this.hashAlgorithm = SHA512.Create();
        }

        public string RandomPassword(int length = 8)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public HashWithSaltResult HashWithSalt(string password)
        {
            SaltGenerator saltGenerator = new SaltGenerator();
            byte[] saltBytes = saltGenerator.GenerateRandomCryptographicBytes(saltLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgorithm.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }

        public String HashPassword(HashWithSaltResult result)
        {
            var combinedPassword = String.Concat(result.Digest, result.Salt);
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
