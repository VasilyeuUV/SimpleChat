using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ServerModel : IDisposable
    {
        private readonly IPAddress _ip = IPAddress.Parse("127.0.0.1");
        private readonly int _port = 8888;
        private TcpListener _listener = null;
        private ServerDataModel _serverData = null;


        internal IPAddress Ip => _ip;
        internal int Port => _port;


        /// <summary>
        /// Start Server
        /// </summary>
        internal async Task ServerStart()
        {
            _serverData = new ServerDataModel();
            _listener ??= new TcpListener(_ip, _port);
            _listener?.Start();

            //while (_listener != null)
            //{

            //}            
        }


        /// <summary>
        /// Server stop
        /// </summary>
        internal void ServerStop()
        {
            _listener?.Stop();
            _listener = null;

            _serverData.ConnectedClients.Clear();
            _serverData.QaDictionary.Clear();
            _serverData.Clear();
            _serverData = null;
        }


        /// <summary>
        /// Return List of connected users
        /// </summary>
        /// <returns></returns>
        internal ICollection<string> GetQuestions() =>
            _serverData.QaDictionary.SelectMany(x => x.Key).ToList();

        /// <summary>
        /// Returns the names of the connected clients
        /// </summary>
        /// <returns></returns>
        internal ICollection<string> GetClient() =>
            _serverData.ConnectedClients.Select(x => x.NickName).ToList();



        #region IDISPOSABLE

        private bool _disposed;


        public void Dispose()
        {
            Dispose(true);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed) { return; }

            _disposed = true;

            // Освобождение управляемых ресурсов
            ServerStop();
        }



        #endregion




    }
}
