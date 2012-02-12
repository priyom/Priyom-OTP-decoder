using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PriyomOTPcoder
{
    class Encryption
    {
        public static void Encode(Random randomgenerator)
        {
            var rollTheDice = randomgenerator;
            Console.WriteLine("Read key from file? Leave blank for No, file name for yes:");
            var file = Console.ReadLine();
            var fileflag = false;
            var fivefigflag = true;
            var keyfiletext = "";
            if (file != "")
            {
                fileflag = true;
                var keyfile = FileOperations.Readfile(file);
                keyfiletext = keyfile[1].Fileline;
                if (IsValid(keyfiletext.Substring(0, 1)))
                {
                    fivefigflag = false;
                }
            }
            Console.WriteLine("Type your message (a-z, spaces. Nothing else yet)");
            var message = Console.ReadLine();
            if (message != null)
                if (message.Length > 0)
                {
                    message = message.ToLower();
                    //remove non a-z chars
                    var cleanmessage = "";
                    for (var i = 0; i < message.Length; i++)
                    {
                        if (IsValid(message.Substring(i, 1)))
                        {
                            cleanmessage += message.Substring(i, 1);
                        }
                    }
                    if (cleanmessage.Length > 0)
                    {
                        var cypherText = "";
                        var keyText = "";
                        var fullkeytext = "";
                        var cypher5Fig = "";
                        var key5Fig = "";
                        var messageArray = new string[cleanmessage.Length, 6];
                        Console.WriteLine("Encoding...");
                        for (var i = 0; i < cleanmessage.Length; i++)
                        {
                            if (fileflag)
                            {
                                keyText = fivefigflag ? ConvertFromFiveFigs(keyfiletext.Substring(i, 1)) : keyfiletext.Substring(i, 1);

                            }
                            else
                            {
                                keyText = GenerateRandomNumberCharacter(rollTheDice);
                            }
                            messageArray[i, 0] = cleanmessage.Substring(i, 1);
                            messageArray[i, 1] = keyText;
                            messageArray[i, 2] = CypherIt(messageArray[i, 0], messageArray[i, 1]);
                            messageArray[i, 3] = Decode(messageArray[i, 2], messageArray[i, 1]);
                            messageArray[i, 4] = ConvertToFiveFigs(messageArray[i, 1]);
                            messageArray[i, 5] = ConvertToFiveFigs(messageArray[i, 2]);
                            cypherText += messageArray[i, 2];
                            fullkeytext += messageArray[i, 1];
                            cypher5Fig += messageArray[i, 5];
                            key5Fig += messageArray[i, 4];
                            Console.Write(".");
                            //Console.WriteLine("0 {0} 1 {1} 2 {2} 3 {3} 4 {4} 5 {5}", messageArray[i, 0], messageArray[i, 1], messageArray[i, 2], messageArray[i, 3], messageArray[i, 4], messageArray[i, 5]);
                        }
                        //now output to files
                        var linestowrite = new FileOperations.FileLine[2]
                                               {
                                                   new FileOperations.FileLine(fullkeytext),
                                                   new FileOperations.FileLine(key5Fig)
                                               };
                        var newstuff = new FileOperations.FileLines(linestowrite);
                        FileOperations.WriteFile("key", newstuff);
                        var linestowritecypher = new FileOperations.FileLine[2]
                                                     {
                                                         new FileOperations.FileLine(cypherText),
                                                         new FileOperations.FileLine(cypher5Fig)
                                                     };
                        var newstuffcypher = new FileOperations.FileLines(linestowritecypher);
                        FileOperations.WriteFile("cypher", newstuffcypher);
                        Console.WriteLine("");
                        Console.WriteLine("Cypher Text: {0}", cypherText);
                        Console.WriteLine("Cypher 5 fig: {0}",cypher5Fig);
                        Console.WriteLine("Key 5fig: {0}",key5Fig);
                        Console.WriteLine("Key Text: {0}", fullkeytext);
                    }
                }
        }
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
        private static bool IsValid(String str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }
        public static string GenerateRandomNumberCharacter(Random rollTheDice)
        {
            return Convert.ToString((char)rollTheDice.Next(97, 122));
        }
        public static string CypherIt(String clean, String pad)
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
        public static void DecodeIt()
        {
            Console.WriteLine("Specify key file? (Leave blank if key.txt exists and is correct)");
            var keyfilename = Console.ReadLine();
            if (keyfilename == "")
            {
                keyfilename = "key";
            }
            Console.WriteLine("Decoding...");
            var keyfile = FileOperations.Readfile(keyfilename);
            FileOperations.FileLine keyfile5Fig;
            FileOperations.FileLine keyfiletext;
            if (keyfilename == "key")
            {
                keyfiletext = keyfile[0];
                keyfile5Fig = keyfile[1];
            }
            else
            {
                keyfiletext = keyfile[1];
                keyfile5Fig = keyfile[2];
            }
            var cyphertextfile = FileOperations.Readfile("cypher");
            var cypherfiletext = cyphertextfile[0];
            var cypherfile5Fig = cyphertextfile[1];
            var messageArray = new string[cypherfiletext.Length(), 4];
            var messageText = "";
            var message5Figs = "";
            var figflag = false;

            if (!IsValid(cypherfile5Fig.Fileline.Substring(0, 1)))
            {
                figflag = true;
            }
            var d = 0;
            for (int i = 0; i < cypherfiletext.Fileline.Length; i++)
            {
                var keytext = "";
                var cyphertext = "";
                if (figflag && d < keyfile5Fig.Fileline.Length)
                {
                    keytext = ConvertFromFiveFigs(keyfile5Fig.Fileline.Substring(d, 2));
                    cyphertext = ConvertFromFiveFigs(cypherfile5Fig.Fileline.Substring(d, 2));
                    d = d + 2;
                }
                else
                {
                    keytext = keyfiletext.Fileline.Substring(i, 1);
                    cyphertext = cypherfiletext.Fileline.Substring(i, 1);
                    d++;
                }
                if (keyfile5Fig.Fileline.Length < d)
                {
                    break;
                }
                messageArray[i, 0] = cyphertext;
                messageArray[i, 1] = keytext;
                messageArray[i, 2] = Decode(messageArray[i, 0], messageArray[i, 1]);
                messageArray[i, 3] = ConvertToFiveFigs(messageArray[i, 2]);
                messageText += messageArray[i, 2];
                message5Figs += messageArray[i, 3];
                Console.Write(".");
            }
            var linestowrite = new FileOperations.FileLine[2]
                                   {
                                       new FileOperations.FileLine(messageText),
                                       new FileOperations.FileLine(message5Figs)
                                   };
            var newStuff = new FileOperations.FileLines(linestowrite);
            FileOperations.WriteFile("message", newStuff);
            Console.WriteLine();
            Console.WriteLine(messageText);
            Console.ReadLine();
        }
        private static string ConvertToFiveFigs(string todecode)
        {
            var cleanChar = Convert.ToChar(todecode);
            var cleanNum = (int)cleanChar;
            cleanNum = cleanNum - 97;
            if (cleanNum > 9)
            {
                return Convert.ToString(cleanNum);
            }
            else
            {
                return "0" + cleanNum;
            }
        }
        private static string ConvertFromFiveFigs(string todecode)
        {
            var newchar = Convert.ToInt16(todecode);
            newchar += 97;
            return Convert.ToString((char)newchar);
        }

        public static void GenKey(Random randomgenerator)
        {
            string docsfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dice = randomgenerator;
            Console.WriteLine("Key Name/ID?:");
            var keyid = Console.ReadLine();
            Console.WriteLine("Key length?(5000)");
            var length = Console.ReadLine();
            var keylength = 5000;
            if (length != "")
            {
                keylength = Convert.ToInt16(length);
            }
            var keyText = "";
            var keyFigs = "";
            for (int i = 0; i < keylength; i++)
            {
                var key = GenerateRandomNumberCharacter(dice);
                keyText += key;
                keyFigs += ConvertToFiveFigs(key);
            }
            var fileLineArray = new FileOperations.FileLine[3]
                                                          {
                                                              new FileOperations.FileLine(keyid),
                                                              new FileOperations.FileLine(keyText),
                                                              new FileOperations.FileLine(keyFigs)
                                                          };
            var newstuff = new FileOperations.FileLines(fileLineArray);

            FileOperations.WriteFile(keyid, newstuff);
            Console.WriteLine("Key Text: " + keyText);
            Console.WriteLine("Key Figs: " + keyFigs);
            Console.ReadLine();
        }
    }
}
