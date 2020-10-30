using System.Net;
using System.Net.Sockets;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    public class ServerModel
    {
        private IPAddress _ip = IPAddress.Parse("127.0.0.1");

        private int _port = 8888;

        private TcpListener _listener = null;


        public IPAddress Ip => _ip;
        public int Port => _port;

        public TcpListener Listener
        {
            get => _listener ??= new TcpListener(_ip, _port);
            private set => _listener = value;
        }

        private ServerModel()
        {

        }

        /// <summary>
        /// Create new Chat Server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static ServerModel CreateServer(string ip = "127.0.0.1", int port = 8888)
        {
            ServerModel server = new ServerModel();

            if (IPAddress.TryParse(ip, out server._ip))
            {
                server._listener = new TcpListener(server._ip, port);
                return server;
            }
            return null;
        }
    }
}
