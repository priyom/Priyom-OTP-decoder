using System;
using System.Text.RegularExpressions;

namespace PriyomOTPcoder
{
    public static class Stringstuff
    {
        public static string ConvertToFiveFigs(string todecode)
        {
            var cleanChar = Convert.ToChar(todecode);
            var cleanNum = (int)cleanChar;
            cleanNum = cleanNum - 97;
            if (cleanNum > 9)
            {
                return Convert.ToString(cleanNum);
            }
            return "0" + cleanNum;
        }
        public static string ConvertFromFiveFigs(string todecode)
        {
            var newchar = Convert.ToInt16(todecode);
            newchar += 97;
            return Convert.ToString((char)newchar);
        }
        public static bool IsValid(String str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }
        public static string GenerateRandomNumberCharacter(Random rollTheDice)
        {
            return Convert.ToString((char)rollTheDice.Next(97, 122));
        }
    }
}
