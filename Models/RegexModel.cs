using System;
using System.Text.RegularExpressions;

namespace Project2.Models
{
    public static class RegexModel
    {
        public static string CreateReg(string Description, int MinLength, bool chMinLength, int MaxLength, bool chMaxLength, int MinUppercase, bool chUppercase, int MinLowercase, bool chLowercase, int MinSpecialSigns, bool chSpecialSigns, bool chDigits, int MinDigits)
        {
            string length, uppercase, lowercase, specsigs, digits;
            int min, max;
            if (chMinLength == true)
                 min = MinLength;
            else
                min = 0;
            if (chMaxLength == true)
                max = MaxLength;
            else
                max = Int32.MaxValue;
            length = "(?=^.{" + min + "," + max + "}$)";
            if (chUppercase == true)
                uppercase = "(?=(.*[A-Z]){" + MinUppercase + ",})";
            else
                uppercase = null;
            if (chLowercase == true)
                lowercase = "(?=(.*[a-z]){" + MinLowercase + ",})";
            else
                lowercase = null;
            if (chDigits == true)
                digits = @"(?=(.*\d){" + MinDigits + ",})";
            else
                digits = null;
            if (chSpecialSigns == true)
                specsigs = @"(?=(.*[^\da-zA-Z]){" + MinSpecialSigns + ",})";
            else
                specsigs = null;
            string result = length + uppercase + lowercase + digits + specsigs;
            return result;
        }

        public static bool MatchPasswordWithRegex(string Pass, string Reg)
        {
            bool result = false;
            Regex regex= new Regex(Reg);
            Match match = regex.Match(Pass);
            if(match.Success)
                result = true;
            else
                result = false;
            
            return result;
        }
    }
}