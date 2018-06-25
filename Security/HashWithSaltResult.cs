using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERISCOTools.Security
{
    public class HashWithSaltResult
    {
        public string Salt { get; }
        public string Digest { get; set; }

        public HashWithSaltResult(string Salt, string Digest)
        {
            this.Salt = Salt;
            this.Digest = Digest;
        }
    }
}
