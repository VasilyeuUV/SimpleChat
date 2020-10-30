using System.IO;
using System.Net.Sockets;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Models
{
    internal class ConnectionModel
    {
        public TcpClient Tcp { get; internal set; }

        public StreamReader Reader { get; internal set; }

        public StreamWriter Writer { get; internal set; }

    }
}
