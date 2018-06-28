using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ERISCOTools.Security
{
    public class PasswordManager
    {
        private HashAlgorithm hashAlgorithm { get; set; }
        private int saltLength { get; set; }

        public PasswordManager()
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

            List<byte> prepend = new List<byte>();
            prepend.AddRange(passwordAsBytes);
            prepend.AddRange(saltBytes);

            byte[] digestBytes = hashAlgorithm.ComputeHash(prepend.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }

        public bool Compare(string input, HashWithSaltResult hash)
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            byte[] digest = Encoding.UTF8.GetBytes(hash.Digest);
            byte[] salt = Encoding.UTF8.GetBytes(hash.Salt);

            List<byte> prepend = new List<byte>();
            prepend.AddRange(inputAsBytes);
            prepend.AddRange(salt);

            return hashAlgorithm.ComputeHash(prepend.ToArray()) == digest;
        }
    }
}