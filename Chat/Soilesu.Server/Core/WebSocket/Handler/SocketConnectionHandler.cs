using Newtonsoft.Json;
using Soilesu.Server.Core.Attributes;
using Soilesu.Server.Core.Models.Chat;
using Soilesu.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Soilesu.Server.Core.WebSocket.Handler
{    
    class SocketConnectionHandler : WebSocketBehavior
    {
        private int ChatID;
        protected override void OnMessage(MessageEventArgs e)
        {        
           var bytes = UTF8Encoding.UTF8.GetBytes(e.Data);
            Sessions.SendTo(e.Data,ChatRepository._chat[ChatID].FirstOrDefault(p=> p!=ID));            
        }
        protected override void OnOpen()
        {
            ChatID = ChatRepository.AddConnection(ID);
        }      
       
    }
}
