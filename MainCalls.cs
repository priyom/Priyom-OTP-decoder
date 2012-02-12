using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriyomOTPcoder
{
    public class MainCalls
    {
        public static void GenKey(Random randomgenerator)
        {
            var dice = randomgenerator;
            Console.WriteLine("Key Name/ID?:");
            var keyid = Console.ReadLine();
            Console.WriteLine("Key length?(5000)");
            var length = Console.ReadLine();
            var keylength = 5000;
            var keyText = "";
            if (length != "")
            {
                keylength = Convert.ToInt16(length);
            }
            Console.WriteLine("Use spaces?(y/n)");
            var spaces = Console.ReadLine();
            if (spaces == "y"||spaces == "Y")
            {
                keyText = Keygen.Keywithspace(keylength, dice);
            }
            else
            {
                keyText = Keygen.Key(keylength, dice);
            } 
            var file = new FileObject
                           {
                               Id = Guid.NewGuid(),
                               Name = keyid,
                               MessageText = keyText,
                               MessageFigure = "",
                               SpaceFlag = true
                           };
            file.WriteFile();
            Console.WriteLine("Key Text: " + keyText);
        }
        public static void Decode()
        {
            Console.WriteLine("Specify key file? (Leave blank if key.txt exists and is correct)");
            var keyfilename = Console.ReadLine();
            if (keyfilename == "")
            {
                keyfilename = "key";
            }
            Console.WriteLine("Specify message file? (Leave blank if cyoher.txt exists and is correct)");
            var cypherfilename = Console.ReadLine();
            if (cypherfilename == "")
            {
                cypherfilename = "cypher";
            }
            var cypherfile = new FileObject();
            cypherfile.ReadFile(cypherfilename);
            var keyfile = new FileObject();
            keyfile.ReadFile(keyfilename);
            var decryptor = new Encryptor
                                {
                                    Key = keyfile.MessageText,
                                    Message = cypherfile.MessageText,
                                    SpaceFlag = cypherfile.SpaceFlag  
                                    };
            var result = decryptor.DeCypherIt();
            Console.WriteLine(result);
            var messagefile = new FileObject
                                  {
                                      Id = Guid.NewGuid(), 
                                      Name ="message",
                                      MessageFigure = "", 
                                      MessageText = result,
                                      SpaceFlag = cypherfile.SpaceFlag
                                  };
            messagefile.WriteFile();
        }
        public static void Encode(Random randomgenerator)
        {
            var rollTheDice = randomgenerator;
            Console.WriteLine("Read key from file? Leave blank for No, file name for yes:");
            var keyfilename = Console.ReadLine();
            var key = "";
            Console.WriteLine("Encode with spaces?(y/n)");
            var spaces = Console.ReadLine();
            var spaceflag = false;
            if (spaces == "y"||spaces =="Y")
            {
                spaceflag = true;
            }
            Console.WriteLine("Type your message (a-z, spaces. Nothing else yet)");
            var message = Console.ReadLine(); 
            if (keyfilename!="")
            {
                var keyfile = new FileObject();
                keyfile.ReadFile(keyfilename);
                key = keyfile.MessageText;
            }
            else
            {
                key = Keygen.Key(message.Length, randomgenerator);
            }
            var encryptor = new Encryptor {Key = key, Message = message, SpaceFlag = spaceflag};
            var result = encryptor.CypherIt();
            Console.WriteLine(result);
            var messagefile = new FileObject
                                  {
                                      Id = Guid.NewGuid(),
                                      MessageFigure = "",
                                      MessageText = result,
                                      Name="cypher",
                                      SpaceFlag = spaceflag
                                  };
            messagefile.WriteFile();
            if (keyfilename=="")
            {
                var keyfile = new FileObject
                                  {
                                      Id = Guid.NewGuid(),
                                      MessageFigure = "",
                                      MessageText = key,
                                      Name = "key",
                                      SpaceFlag = spaceflag
                                  };
                keyfile.WriteFile();
            }
        }
        }
    }
