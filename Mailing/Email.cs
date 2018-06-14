using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERISCOTools.Mailing
{
    public class Email
    {
        public string subject { get; set; }
        public string message { get; set; }
        public string receiver { get; set; }
        public string copyTo { get; set; }
        public string replyTo { get; set; }
        public string attachment_path { get; set; }
        public string attachment_name { get; set; }
    }
}
