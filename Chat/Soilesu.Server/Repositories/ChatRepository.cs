using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Soilesu.Server.Repositories
{
   public class ChatRepository
    {
        public static Dictionary<int, List<string>> _chat = new Dictionary<int, List<string>>();
        private static int countChat = 1;
        private static List<string> vs = new List<string>();       
        public static int AddConnection(string connectionID)
        {
            if (vs.Count == 2)
            {
                vs = new List<string>();
                countChat++;
            }
            _chat[countChat] =vs;           
            vs.Add(connectionID);
            return countChat;
        }
    }
}
