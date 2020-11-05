using System;
using System.Globalization;
using System.Linq;

namespace Vasilev.SimpleChat.ConsNetCore.Communication.Models
{
    public class MessageModel
    {
        public DateTime Dtg { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="msg"></param>
        private MessageModel()
        {
        }

        public static MessageModel CreateModel(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) { return null; }
            
            string[] msg = message.Split("\n");
            if (msg.Length >= 3)
            {
                MessageModel messageModel = new MessageModel();
                try
                {
                    messageModel.Dtg = Convert.ToDateTime(msg[0]);
                }
                catch (Exception)
                {
                    messageModel.Dtg = DateTime.Now;
                }
                messageModel.Author = msg[1];
                messageModel.Message = msg.Skip(2).Aggregate((rez, item) => $"{rez}\n{item}").Trim();
                return messageModel;
            }
            return null;
        }


        /// <summary>
        /// Convert MessageModel to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format($"{Dtg.ToString("G", DateTimeFormatInfo.InvariantInfo)}\n{Author}\n{Message}");
        }
    }
}
