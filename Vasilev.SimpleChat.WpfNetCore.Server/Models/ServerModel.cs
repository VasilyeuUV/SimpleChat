using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ServerModel : IDisposable
    {
        private readonly IPAddress _ip = null;
        private readonly int _port = 8888;
        private TcpListener _listener = null;
        private ServerDataModel _serverData = null;


        internal IPAddress Ip => _ip;
        internal int Port => _port;


        /// <summary>
        /// Start Server
        /// </summary>
        internal async Task ServerStartAsync()
        {
            _serverData = new ServerDataModel();
            _listener ??= new TcpListener(IPAddress.Any, _port);
            await DoWorkAsync();
        }

        private async Task DoWorkAsync()
        {
            _listener?.Start();
            while (_listener != null)
            {
                TcpClient tcpClient = _listener.AcceptTcpClient();

                using (StreamReader sr = new StreamReader(tcpClient.GetStream()) )
                {
                    while (tcpClient.Connected)
                    {
                        try
                        {
                            var line = sr.ReadLine();
                            Console.WriteLine(line);
                        }
                        catch (Exception)
                        {
                        }

                    }
                }

            }

        }


        /// <summary>
        /// Server stop
        /// </summary>
        internal void ServerStop()
        {
            _listener?.Stop();
            _listener = null;

            foreach (var client in _serverData.ConnectedClients)
            {
                //client.Close();
            }



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
