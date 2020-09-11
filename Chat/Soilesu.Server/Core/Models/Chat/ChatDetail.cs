using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soilesu.Server.Core.Models.Chat
{
    public class ChatDetail
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the message sent/received time.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the MessageType.
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// Gets or sets the message sent/received notification type.
        /// </summary>
        public string NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the recipient's available status.
        /// </summary>
        public string AvailableStatus { get; set; }

        #endregion
    }
}
