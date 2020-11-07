using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Vasilev.SimpleChat.ConsNetCore.Communication.Models;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Logic
{
    public class ServerControl
    {
        private ServerModel _server = null;


        #region PUBLIC METHODS

        /// <summary>
        /// Start Server
        /// </summary>
        /// <returns></returns>
        public void ServerStart()
        {
            _server = new ServerModel(new ServerDataModel());
            StartListener();
        }


        /// <summary>
        /// Server stop
        /// </summary>
        public void ServerStop()
        {
            _server?.TcpListener?.Stop();
            _server?.ServerData.Clear();
            _server = null;
        }

        public ICollection<string> GetQuestions() =>
            _server?.ServerData.QaDictionary.Select(x => x.Key).ToList();

        public ICollection<string> GetClients() =>
            _server?.ServerData.ConnectedClients.Select(x => x.NickName).ToList();

        #endregion


        /// <summary>
        /// Start TCP Listener
        /// </summary>
        /// <param name="server"></param>
        private void StartListener()
        {
            try
            {
                _server?.TcpListener.Start();

                while (true)
                {
                    TcpClient tcpClient = _server?.TcpListener?.AcceptTcpClient();
                    ListenClientAsync(tcpClient);            
                }
            }
            catch (SocketException ex) when (ex.ErrorCode == 10004)
            {
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ServerStop();
            }            
        }


        private async void ListenClientAsync(TcpClient tcpClient)
        {
            ClientModel client = new ClientModel()
            {
                Communication = new CommunicationModel(tcpClient),
            };
            _server.ServerData.ConnectedClients.Add(client);

            await Task.Run(() => ListenClient(_server, client));
        }

        private void ListenClient(ServerModel server, ClientModel client)
        {
            try
            {
                NetworkStream stream = client. GetStream();
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        message = String.Format("{0}: {1}", userName, message);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                    }
                    catch
                    {
                        message = String.Format("{0}: покинул чат", userName);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // в случае выхода из цикла закрываем ресурсы
                server.RemoveConnection(this.Id);
                Close();
            }
        }
    }
}
