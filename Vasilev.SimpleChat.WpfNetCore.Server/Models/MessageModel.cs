using System;
using System.Globalization;

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
    }
}
