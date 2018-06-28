using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERISCOTools.Security
{
    public class HashWithSaltResult
    {
        public string salt { get; }
        public string digest { get; set; }

        public HashWithSaltResult(string salt, string digest)
        {
            this.salt = salt;
            this.digest = digest;
        }
    }
}