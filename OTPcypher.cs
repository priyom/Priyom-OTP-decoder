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
        public static string DecodeWithSpace(String clean, String pad)
        {
            var cleanChar = Spacecheck(Convert.ToChar(clean));
            var padChar = Spacecheck(Convert.ToChar(pad));
            var cleanNum = (int)cleanChar;
            var padNum = (int)padChar;
            cleanNum = cleanNum - 96;
            padNum = padNum - 96;
            var cypherValue = (cleanNum - padNum) % 27;
            if (cypherValue < 0)
            {
                cypherValue = cypherValue + 27;
            }
            cypherValue = cypherValue + 96;
            return Convert.ToString(Backtickcheck((char)cypherValue));
        }
        public static string CypherWithSpace(String clean, String pad)
        {
            var cleanChar = Spacecheck(Convert.ToChar(clean));
            var padChar = Spacecheck(Convert.ToChar(pad));
            var cleanNum = (int)cleanChar;
            var padNum = (int)padChar;
            cleanNum = cleanNum - 96;
            padNum = padNum - 96;
            var cypherValue = (padNum + cleanNum) % 27;
            cypherValue = cypherValue + 96;
            return Convert.ToString(Backtickcheck((char)cypherValue));
        }
        public static char Spacecheck(char incoming)
        {
            if (incoming == 32)
            {
                return (char)96;
            }
            return incoming;
        }

        public static char Backtickcheck(char incoming)
        {
            if (incoming == 96)
            {
                return (char)32;
            }
            return incoming;
        }
    }
}
