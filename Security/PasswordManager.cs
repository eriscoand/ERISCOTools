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

        public HashWithSaltResult HashWithSalt(string password)
        {
            PasswordGenerator passwordGenerator = new PasswordGenerator(this.saltLength);
            var randomSalt = passwordGenerator.Generate();

            byte[] saltBytes = Encoding.UTF8.GetBytes(randomSalt);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);

            List<byte> prepend = new List<byte>();
            prepend.AddRange(passwordAsBytes);
            prepend.AddRange(saltBytes);

            byte[] digestBytes = hashAlgorithm.ComputeHash(prepend.ToArray());
            var digestBase64 = Convert.ToBase64String(digestBytes);

            return new HashWithSaltResult(randomSalt, digestBase64);
        }

        public bool Compare(string password, HashWithSaltResult hash)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(hash.salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            List<byte> prepend = new List<byte>();
            prepend.AddRange(passwordBytes);
            prepend.AddRange(saltBytes);

            var passwordCheckBase64 = Convert.ToBase64String(hashAlgorithm.ComputeHash(prepend.ToArray()));

            return passwordCheckBase64 == hash.digest;
        }
       
    }
}