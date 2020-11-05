using System;
using System.Globalization;
using System.Linq;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class MessageModel
    {
        internal DateTime Dtg { get; set; }
        internal string Author { get; set; }
        internal string Message { get; set; }

        public override string ToString()
        {
            return string.Format($"{Dtg.ToString("G", DateTimeFormatInfo.InvariantInfo)}\n{Author}\n{Message}");
        }

        internal MessageModel ConvertToMessageModel(string message)
        {
            string[] msg = message.Split("\n");

            if (msg.Length >= 2)
            {
                Author = msg[0];
                Message = msg.Skip(1).Aggregate((rez, item) => $"{rez}\n{item}").Trim();
            }
            Author = "";
            Message = "";

            return this;
        }
    }
}
