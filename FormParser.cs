using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERISCOTools
{
    public class FormParser
    {
        public static int StringToInt(string input)
        {
            var result = 0;
            int.TryParse(input, out result);
            return result;
        }

        public static double StringToDouble(string input)
        {
            var result = 0.0;
            if (input == null) return result;
            input = input.Replace(".", ",");
            Double.TryParse(input, out result);
            return result;
        }

        public static Boolean StringToBool(string input)
        {
            if (input != null) return Boolean.Parse(input);
            return false;
        }

        public static DateTime StringToDateTime(string input)
        {
            var result = DateTime.Now;
            DateTime.TryParse(input, out result);
            return result;
        }

        public static TimeSpan StringToTime(string input)
        {
            var result = new TimeSpan();
            TimeSpan.TryParse(input, out result);
            return result;
        }

        public static double TwoDecimals(double input)
        {
            return Math.Round(input, 2);
        }

    }
}
