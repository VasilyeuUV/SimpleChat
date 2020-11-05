using System.Linq;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Models
{
    internal class MessageModel
    {

        public string Author { get; internal set; }
        public string Message { get; private set; }

        internal MessageModel(string message)
        {
            ConvertToMessageModel(message);
        }

        private void ConvertToMessageModel(string message)
        {
            string[] msg = message.Split("\n");

            if (msg.Length >= 3)
            {
                Author = msg[1];
                Message = msg.Skip(2).Aggregate((rez, item) => $"{rez}\n{item}").Trim();
                return;
            }
            Author = "";
            Message = "";
        }
    }
}
