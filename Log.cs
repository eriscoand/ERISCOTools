using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERISCOTools
{
    public class Log
    {
        private string URL_SERVER_PATH { get; set; }
        public Log(string URL_SERVER_PATH = "~/Log/")
        {
            this.URL_SERVER_PATH = URL_SERVER_PATH;
        }

        public void SimpleLog(string message)
        {
            try
            {
                string fileLog = HttpContext.Current.Server.MapPath(URL_SERVER_PATH);
                fileLog = fileLog + string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMdd"));

                string text = "[" + DateTime.Now.ToString("HH:mm:ss") + "] Error: " + message.ToString() + "";

                if (!File.Exists(fileLog))
                {
                    using (StreamWriter sw = File.CreateText(fileLog))
                    {
                        sw.WriteLine("----------------START----------------");
                    }
                }

                using (StreamWriter sw = File.AppendText(fileLog))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message.ToString());
            }

        }
    }
}
