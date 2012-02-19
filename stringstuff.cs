using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PriyomOTPcoder
{
    public static class Stringstuff
    {
        public static bool IsValid(String str)
        {
            return Regex.IsMatch(str, @"^[a-z]+$");
        }
        public static bool IsValidWithSpace(String str)
        {
            return Regex.IsMatch(str, @"^[a-z ]+$");
        }
        public static string GenerateRandomNumberCharacter(Random rollTheDice)
        {
            return Convert.ToString((char)rollTheDice.Next(97, 122));
        }
        public static string GenerateRandomNumberCharacterWithSpace(Random rollTheDice)
        {
            return Convert.ToString(OTPcypher.Backtickcheck((char)rollTheDice.Next(96, 122)));
        }
        public static string RemoveSpaces(string message)
        {
            return message.Where(t => IsValid(t.ToString())).Aggregate("", (current, t) => current + t);
        }
    }
}
