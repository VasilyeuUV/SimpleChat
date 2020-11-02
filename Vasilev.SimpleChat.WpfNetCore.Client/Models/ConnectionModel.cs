using System.Net;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Models
{
    internal class ConnectionModel
    {
        public IPAddress Ip { get; internal set; }

        public int Port { get; internal set; }

        public bool IsConnected { get; internal set; }
    }
}
