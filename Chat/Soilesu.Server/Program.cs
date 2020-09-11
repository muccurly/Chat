using Soilesu.Server.Core;
using Soilesu.Server.Core.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server
{
    class Program
    {
        private static HttpServerProvider _httpServerProvider;
        private static WebSocketServerProvider _socketServerProvider;
        static void Main(string[] args)
        {
            var address = new Uri("http://192.168.1.71:15000");            
            _httpServerProvider = new HttpServerProvider(address);
            _socketServerProvider = new WebSocketServerProvider(new Uri("ws://192.168.1.71:16000"));
            Task.Run(() =>
              {
                  _httpServerProvider.StartAsync();
              });
            Task.Run(() =>
            {
                _socketServerProvider.Start();
            });

            Console.ReadLine();
            Console.ReadLine();
        }
    }
}
