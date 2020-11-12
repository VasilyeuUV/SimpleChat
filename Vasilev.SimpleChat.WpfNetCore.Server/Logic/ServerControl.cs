using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            Disconnect();
        }


        public ICollection<string> GetQuestions() =>
            _server?.ServerData.QaDictionary.Select(x => x.Key).ToList();

        public ICollection<string> GetClients() =>
            _server?.ServerData.ConnectedClients.Select(x => x.NickName).ToList();

        #endregion

        #region SERVER_LISTENER

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
            catch (Exception)
            {
                ServerStop();
            }
        }
        #endregion

        #region CURRENT CLIENT CHAT
        /// <summary>
        /// Async client listener
        /// </summary>
        /// <param name="tcpClient"></param>
        private async void ListenClientAsync(TcpClient tcpClient)
        {
            ClientModel client = new ClientModel()
            {
                Communication = new CommunicationModel(tcpClient),
            };
            _server.ServerData.ConnectedClients.Add(client);
            try
            {
                await Task.Run(() => ListenClient(_server, client));
                _server?.ServerData.ConnectedClients.Remove(client);
                client.Communication.CloseCommunication();
            }
            catch (Exception ex)
            {
                
                string err = ex.Message;
            }
            
        }

        /// <summary>
        /// client listen process
        /// </summary>
        /// <param name="server"></param>
        /// <param name="client"></param>
        private void ListenClient(ServerModel server, ClientModel client)
        {
            MessageModel message = MessageModel.CreateModel(DateTime.Now, _server.ServerName, _server.ServerData.ServerFirstPhrase);
            SendMessage(client, message);

            bool isActive = true;
            while (isActive)
            {
                Task.Delay(10);
                isActive = GetMessage(client);
            }
        }
        #endregion

        #region SEND_MESSAGE

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="client"></param>
        /// <param name="msg"></param>
        private void SendMessage(ClientModel client, MessageModel msg)
        {
            if (msg == null) { return; }

            client.ChatHistory.Add(msg);
            client.Communication.TransmitMessage(msg.ToString());
        }

        /// <summary>
        /// Send Message to all connected Clients
        /// </summary>
        /// <param name="message"></param>
        private void SendMessageAll(string message)
        {
            MessageModel msg = MessageModel.CreateModel(DateTime.Now, _server.ServerName, message);
            foreach (var client in _server.ServerData.ConnectedClients)
            {
                SendMessage(client, msg);
            }
        }

        #endregion

        /// <summary>
        /// Get messages
        /// </summary>
        /// <param name="client"></param>
        private bool GetMessage(ClientModel client)
        {
            NetworkStream stream = client.Communication.GetStream();
            StringBuilder response = new StringBuilder();
            if (stream.CanRead)
            {
                byte[] data = new byte[256]; 
                int bytesLength = 0;

                try
                {
                    do
                    {
                        bytesLength = stream.Read(data, 0, data.Length);
                        response.Append(Encoding.UTF8.GetString(data, 0, bytesLength));
                    }
                    while (stream.DataAvailable);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            string message = response.ToString();
            if (message.Length > 0)
            {
                MessageModel msg = MessageModel.CreateModel(DateTime.Now.ToString() + "\n" + message);                
                if (msg == null) { return true; }               
                SendMessage(client, msg);

                Task.Delay(1000);

                MessageModel answer = null;
                if (client.ChatHistory.Count == 2)
                {
                    client.NickName = msg.Author.Trim().Replace(Environment.NewLine, " ");
                    answer = MessageModel.CreateModel(
                        DateTime.Now,
                        _server.ServerName, 
                        _server.ServerData.ServerSecondPhrase + client.NickName + "?"
                        );                    
                }
                else
                {
                    answer = GetResponse(msg.Message);
                }
                SendMessage(client, answer);

                if (msg.Message.ToLower() == _server.ServerData.ServerDisconnectPhrase)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Make response
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private MessageModel GetResponse(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) { return null; }

            var keys = _server?.ServerData.QaDictionary.Select(x => x.Key).ToList();
            string answer = _server.ServerData.ServerErrorPhrase;

            foreach (var key in keys)
            {
                if (msg.IndexOf(key, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    var answers = _server.ServerData.QaDictionary[key];
                    answer = answers[new Random().Next(0, answers.Count - 1)];
                }
            }
            return MessageModel.CreateModel(DateTime.Now, _server.ServerName, answer);
        }


        private void Disconnect()
        {
            _server?.ServerData.Clear();
            _server?.TcpListener?.Stop();  
            _server = null;
        }

    }
}
