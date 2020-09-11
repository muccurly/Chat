using Newtonsoft.Json;
using SoilesuX.Models.Chat;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoilesuX.Services
{
   public class ApiSocketService
    {
        public event EventHandler<ChatMessage> DataRecieved;
        private ClientWebSocket client;
        private Uri address = new Uri("ws://192.168.1.71:16000/chat");
        public ApiSocketService()
        {
            client = new ClientWebSocket();
            ConnecToServrAsync();
        }
        async void ConnecToServrAsync()
        {
            await client.ConnectAsync(address, CancellationToken.None);
            await Task.Factory.StartNew(async () =>
            {
                while (client.State == WebSocketState.Open)
                {
                    await ReadMessage();
                }
            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        private async Task ReadMessage()
        {
            WebSocketReceiveResult result;
            var message = new ArraySegment<byte>(new Byte[4096]);
            do
            {
                result = await client.ReceiveAsync(message, CancellationToken.None);
                var str = Encoding.UTF8.GetString(message.Array, message.Offset, result.Count);
                ChatMessage chatMessage = JsonConvert.DeserializeObject<ChatMessage>(str);
                if (chatMessage != null)
                {
                    DataRecieved?.Invoke(this, chatMessage);
                }

            } while (!result.EndOfMessage);
        }
        public async void SendMessageAsync(ChatMessage message)
        {
            string serialisedMessage = JsonConvert.SerializeObject(message);
            var byteMessage = Encoding.UTF8.GetBytes(serialisedMessage);
            var segmnet = new ArraySegment<byte>(byteMessage);
            await client.SendAsync(segmnet, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
