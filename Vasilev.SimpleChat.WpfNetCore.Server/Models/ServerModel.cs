using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Schema;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    public class ServerModel
    {
        private IPAddress _localAddr = IPAddress.Parse("127.0.0.1");

        private int _port = 8888;

        private TcpListener _listener = null;


        public IPAddress LocalAddr => _localAddr;
        public int Port => _port;

        public TcpListener Listener
        {
            get => _listener ??= new TcpListener(_localAddr, _port);
            private set => _listener = value;
        }

        private ServerModel()
        {

        }

        public static ServerModel CreateServer(string ip, int port = 8888)
        {
            ServerModel server = new ServerModel();

            if (IPAddress.TryParse(ip, out server._localAddr))
            {
                server._listener = new TcpListener(server._localAddr, port);
                return server;
            }
            return null;
        }
    }
}
