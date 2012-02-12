using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriyomOTPcoder
{
    class Encryptionwithspace
    {
        public static void EncodeIt(Random randomgenerator)
        {
            var rollTheDice = randomgenerator;
            var fileflag = false;
            var keyfiletext = "";
            Console.WriteLine("Read key from file? Leave blank for No, file name for yes:");
            var file = Console.ReadLine();
            if (file != "")
            {
                fileflag = true;
                var keyfile = FileOperations.Readfile(file);
                keyfiletext = keyfile[1].Fileline;
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
                        if (Stringstuff.IsValidWithSpace(message.Substring(i, 1)))
                        {
                            cleanmessage += message.Substring(i, 1);
                        }
                    }
                    if (cleanmessage.Length > 0)
                    {
                        var cypherText = "";
                        var fullkeytext = "";
                        var messageArray = new string[cleanmessage.Length, 4];
                        Console.WriteLine("Encoding...");
                        for (var i = 0; i < cleanmessage.Length; i++)
                        {
                            string keyText = Stringstuff.GenerateRandomNumberCharacterWithBacktick(rollTheDice);
                            messageArray[i, 0] = cleanmessage.Substring(i, 1);
                            messageArray[i, 1] = keyText;
                            messageArray[i, 2] = OTPcypher.CypherWithSpace(messageArray[i, 0], messageArray[i, 1]);
                            messageArray[i, 3] = OTPcypher.DecodeWithSpace(messageArray[i, 2], messageArray[i, 1]);
                            cypherText += messageArray[i, 2];
                            fullkeytext += messageArray[i, 1];
                            Console.Write(".");
                            Console.WriteLine("0 {0} 1 {1} 2 {2} 3 {3}", messageArray[i, 0], messageArray[i, 1], messageArray[i, 2], messageArray[i, 3]);
                        }
                        //now output to files
                        var linestowrite = new[]
                                               {
                                                   new FileOperations.FileLine(fullkeytext),
                                               };
                        var newstuff = new FileOperations.FileLines(linestowrite);
                        FileOperations.WriteFile("key", newstuff);
                        var linestowritecypher = new[]
                                                     {
                                                         new FileOperations.FileLine(cypherText),
                                                     };
                        var newstuffcypher = new FileOperations.FileLines(linestowritecypher);
                        FileOperations.WriteFile("cypher", newstuffcypher);
                        Console.WriteLine("");
                        Console.WriteLine("Cypher Text: {0}", cypherText);
                        Console.WriteLine("Key Text: {0}", fullkeytext);
                    }
                }
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
            FileOperations.FileLine keyfiletext;
            if (keyfilename == "key")
            {
                keyfiletext = keyfile[0];
            }
            else
            {
                keyfiletext = keyfile[1];
            }
            var cyphertextfile = FileOperations.Readfile("cypher");
            var cypherfiletext = cyphertextfile[0];
            var messageArray = new string[cypherfiletext.Length(), 4];
            var messageText = "";

            var d = 0;
            for (int i = 0; i < cypherfiletext.Fileline.Length; i++)
            {
                string keytext;
                string cyphertext;

                keytext = keyfiletext.Fileline.Substring(i, 1);
                cyphertext = cypherfiletext.Fileline.Substring(i, 1);
  
                messageArray[i, 0] = cyphertext;
                messageArray[i, 1] = keytext;
                messageArray[i, 2] = OTPcypher.DecodeWithSpace(messageArray[i, 0], messageArray[i, 1]);
                messageText += messageArray[i, 2];
                Console.Write(".");
            }
            var linestowrite = new[]
                                   {
                                       new FileOperations.FileLine(messageText),
                                   };
            var newStuff = new FileOperations.FileLines(linestowrite);
            FileOperations.WriteFile("message", newStuff);
            Console.WriteLine();
            Console.WriteLine(messageText);
            Console.ReadLine();
        }


        public static void GenKey(Random randomgenerator)
        {
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
                var key = Stringstuff.GenerateRandomNumberCharacter(dice);
                keyText += key;
                keyFigs += Stringstuff.ConvertToFiveFigs(key);
            }
            var fileLineArray = new[]
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
