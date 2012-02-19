
namespace PriyomOTPcoder
{
    public class Encryptor
    {
        public string Message;
        public string Key;
        public bool SpaceFlag;

        public string CypherIt()
        {
            return SpaceFlag ? CypherWithSpace() : CypherWithOutSpace();
        }

        public string DeCypherIt()
        {
            return SpaceFlag ? DeCypherWithSpace() : DeCypherWithOutSpace();
        }
        private string CypherWithSpace()
        {
            var result = "";
           for (var i =0; i < Message.Length; i++)
           {
               result += OTPcypher.CypherWithSpace(Message.Substring(i,1),Key.Substring(i,1));
           }
            return result;

        }
        private string CypherWithOutSpace()
        {
            var result = "";
            Message = Stringstuff.RemoveSpaces(Message);
            for (var i = 0; i < Message.Length; i++)
            {
                result += OTPcypher.Cypher(Message.Substring(i, 1), Key.Substring(i, 1));
            }
            return result;
        }
        private string DeCypherWithSpace()
        {
            var result = "";
            for (var i = 0; i < Message.Length; i++)
            {
                result += OTPcypher.DecodeWithSpace(Message.Substring(i, 1), Key.Substring(i, 1));
            }
            return result;

        }
        private string DeCypherWithOutSpace()
        {
            var result = "";
            for (var i = 0; i < Message.Length; i++)
            {
                result += OTPcypher.Decode(Message.Substring(i, 1), Key.Substring(i, 1));
            }
            return result;
        }
    }
}
