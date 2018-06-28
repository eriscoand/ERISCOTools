
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ERISCOTools.Security
{
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }

    public class PasswordAdvisor
    {
        public static PasswordScore CheckStrength(String password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;
            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if(password.Any(c => char.IsDigit(c)))
                score++;
            if (password.Any(c => char.IsUpper(c)))
                score++;
            if (password.Any(c => char.IsLower(c)))
                score++;
            if (password.Any(c => !char.IsLetterOrDigit(c)))
                score++;
            return (PasswordScore)score;
        }
    }
}
