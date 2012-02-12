﻿using System;


namespace PriyomOTPcoder
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitflag = false;
            var randomgen = new Random();
            do
            {
                Console.WriteLine("Encode(E), Decode(D), GenKey(G) or Exit(X)?");
                var mode = Console.ReadLine();
                switch (mode)
                {
                    case "E":
                    case "e":
                        Encryption.EncodeIt(randomgen);
                        break;
                    case "D":
                    case "d":
                        Encryption.DecodeIt();
                        break;
                    case "X":
                    case "x":
                        exitflag = true;
                        break;
                    case "G":
                    case "g":
                        Encryption.GenKey(randomgen);
                        break;
                    default:
                        break;
                }
            } while (exitflag == false);
            Environment.Exit(0);
        }
    }
}
