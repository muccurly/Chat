using ManualHttpServer.Extensions;
using Soilesu.Server.Core.WebSocket.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Soilesu.Server.Core.WebSocket
{
   public class WebSocketServerProvider
    {      
        private WebSocketServer _webSocketServer;
        public WebSocketServerProvider(Uri EndPoint)
        {
            _webSocketServer = new WebSocketServer(EndPoint.ToString());          
            _webSocketServer.AddWebSocketService<SocketConnectionHandler>("/chat");
        }
        #region Temp
        //public void BroadcastMessageToClients(string message)
        //{
        //    _webSocketServer.WebSocketServices.TryGetServiceHost("/socket", out WebSocketServiceHost host);
        //   var sessions = host.Sessions.Sessions.Select(p => p.ID);
        //    foreach (var ses in sessions)
        //    {
        //        SendMessageToClient(ses, message);
        //    }
        //}
        //public void SendMessageToClient(string id,string message)
        //{
        //    var client = _activeConnection.GetConnectionById(id);
        //    if (client != null)
        //    {
        //        _webSocketServer.WebSocketServices.TryGetServiceHost("/socket", out WebSocketServiceHost host);
        //        host.Sessions.SendTo(message, id);
        //    }
        //}
        #endregion
        public void Start()
        {
            try {
                ConsoleExtension.WriteLine(
               $"Trying to start WebSocket-server at ", ConsoleColor.Blue);
                _webSocketServer.Start();                

                ConsoleExtension.WriteLine(
               $"WebSocket-server successfully started at ", ConsoleColor.Blue);
            }catch(Exception ex)
            {

            }

        }

    }
}
