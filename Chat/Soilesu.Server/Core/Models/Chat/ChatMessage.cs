using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Core.Models.Chat
{
  public class ChatMessage
    {
        #region Fields

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public string ImagePath { get; set; }
        public bool IsReceived { get; set; }

        #endregion
    }
}
