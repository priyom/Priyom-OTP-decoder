using System;

namespace PriyomOTPcoder
{
    public static class OTPcypher
    {
        public static string Decode(String clean, String pad)
        {
            var cleanChar = Convert.ToChar(clean);
            var padChar = Convert.ToChar(pad);
            var cleanNum = (int)cleanChar;
            var padNum = (int)padChar;
            cleanNum = cleanNum - 97;
            padNum = padNum - 97;
            var cypherValue = (cleanNum - padNum) % 26;
            if (cypherValue < 0)
            {
                cypherValue = cypherValue + 26;
            }
            cypherValue = cypherValue + 97;
            return Convert.ToString((char)cypherValue);
        }
        public static string Cypher(String clean, String pad)
        {
            var cleanChar = Convert.ToChar(clean);
            var padChar = Convert.ToChar(pad);
            var cleanNum = (int)cleanChar;
            var padNum = (int)padChar;
            cleanNum = cleanNum - 97;
            padNum = padNum - 97;
            var cypherValue = (padNum + cleanNum) % 26;
            cypherValue = cypherValue + 97;
            return Convert.ToString((char)cypherValue);
        }
    }
}
