using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriyomOTPcoder
{
    public static class Keygen
    {
        public static string Keywithspace(int length, Random randomgen)
        {
            var result = "";
            for (var i = 0; i < length; i++)
            {
                result += Stringstuff.GenerateRandomNumberCharacterWithSpace(randomgen);
            }
            return result;
        }
        public static string Key(int length, Random randomgen)
        {
            var result = "";
            for (var i = 0; i < length; i++)
            {
                result += Stringstuff.GenerateRandomNumberCharacter(randomgen);
            }
            return result;
        }
    }
}
