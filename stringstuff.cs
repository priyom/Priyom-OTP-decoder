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
        public static string ConvertAlphaNoSpaceToNumber(string incoming)
        {
            var result = "";
            foreach(var a in incoming)
            {
                var cleanChar = (Convert.ToChar(a));
                var cleanNum = (int)cleanChar;
                var newnum = cleanNum - 96;
                if (newnum < 10)
                {
                    result += "0" + newnum;
                }
                else
                {
                    result += newnum;
                }
            }
            return result;
        }
        public static string ConvertAlphaWithSpaceToNumber(string incoming)
        {
            var result = "";
            foreach (var a in incoming)
            {
                var numChar = OTPcypher.Spacecheck(Convert.ToChar(a));
                var cleanNum = (int)numChar;
                var newnum = cleanNum - 96;
                if (newnum== 0)
                {
                    newnum = 27;
                }
                if (newnum < 10)
                {
                    result += "0" + newnum;
                }
                else
                {
                    result += newnum;
                }
            }
            return result;
        }
        public static string ConvertNumberToAlpha(string incoming)
        {
            var result = "";
            var end = incoming.Length;
            for (var i = 0; i < end; i = i +2 )
            {
                var twoFigure = incoming.Substring(i, 2);
                var newFigure = Convert.ToInt16(twoFigure) + 96;
                result += Convert.ToChar(newFigure);
            }
            return result;
        }
        public static string ConvertNumberWith27ToAlphaWithSpace(string incoming)
        {
            var result = "";
            var end = incoming.Length;
            for (var i = 0; i < end; i = i + 2)
            {
                var twoFigure = incoming.Substring(i, 2);
                if (twoFigure=="27")
                {
                    twoFigure = "0";
                }
                var newFigure = Convert.ToInt16(twoFigure) + 96;
                result += OTPcypher.Backtickcheck(Convert.ToChar(newFigure));
            }
            return result;
        }

        public static string ConvertTo5FigGroups(string message)
        {
            var result = "";
            for (var i=0; i < message.Length; i = i+5)
            {
                if(i+5 >= message.Length)
                {
                    result += message.Substring(i, (message.Length - i));
                    break;
                }
                result += message.Substring(i, 5) + " ";
            }
            return result;
        }
    }
}
