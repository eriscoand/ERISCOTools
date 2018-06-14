using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERISCOTools
{
    public static class WebConfigElement
    {
        public static string Read(string key)
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
        }

        public static String ReadURL(string key)
        {
            return System.Web.VirtualPathUtility.ToAbsolute(Read(key));
        }
    }
}
