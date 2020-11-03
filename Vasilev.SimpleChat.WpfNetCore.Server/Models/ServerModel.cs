using System.Net;
using System.Net.Sockets;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ServerModel
    {
        internal string ServerName => "Server";
        internal IPAddress Ip => IPAddress.Any;
        internal int Port => 8888;

        private TcpListener _tcpListener = null;
        public TcpListener TcpListener => _tcpListener ??= new TcpListener(this.Ip, this.Port);

        internal ServerDataModel ServerData { get; private set; }
        

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="serverData"></param>
        internal ServerModel(ServerDataModel serverData)
        {
            this.ServerData = serverData;
        }
    }
}
