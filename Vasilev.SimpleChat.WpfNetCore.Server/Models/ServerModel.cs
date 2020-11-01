using System.Net;
using System.Net.Sockets;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ServerModel
    {
        private readonly IPAddress _ip = IPAddress.Parse("127.0.0.1");
        private readonly int _port = 8888;
        private TcpListener _listener = null;


        internal IPAddress Ip => _ip;
        internal int Port => _port;
        internal TcpListener Listener
        {
            get => _listener ??= new TcpListener(_ip, _port);
            private set => _listener = value;
        }
    }
}
