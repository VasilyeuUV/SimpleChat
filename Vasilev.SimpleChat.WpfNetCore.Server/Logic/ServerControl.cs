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
            MessageModel message = MessageModel.CreateModel(DateTime.Now, _server.ServerName, _server.ServerData.ServerFirstPhrase);
            SendMessage(client, message);

            while (true)
            {
                Task.Delay(10);
                GetMessage(client);
            }

            //ClientChatProcess.Create(server, client);
        }

        //private void SendMessage(string msg, ClientModel client = null)
        //{
        //    if (string.IsNullOrWhiteSpace(msg)) { return; }

        //    string author = client?.NickName;
        //    if (client == null) { author = _server.ServerName; }

        //    MessageModel message = MessageModel.CreateModel(DateTime.Now, author, msg);

        //    SendMessage(message);
        //}


        private void SendMessage(ClientModel client, MessageModel msg)
        {
            if (msg == null) { return; }

            client.Communication.TransmitMessage(msg.ToString());
        }



        /// <summary>
        /// Get messages
        /// </summary>
        /// <param name="client"></param>
        private void GetMessage(ClientModel client)
        {
            NetworkStream stream = client.Communication.GetStream();
            StringBuilder response = new StringBuilder();
            if (stream.CanRead)
            {
                byte[] data = new byte[256]; // буфер для получаемых данных
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
                    throw new Exception(ex.Message);
                }
            }

            string message = response.ToString();
            if (message.Length > 0)
            {
                MessageModel msg = MessageModel.CreateModel(message);
                if (msg != null) 
                { 
                    client.ChatHistory.Add(msg);
                    SendMessage(client, msg);
                }

                if (client.ChatHistory.Count == 2)
                {
                    client.NickName = msg.Author;
                    msg = MessageModel.CreateModel(DateTime.Now, _server.ServerName, _server.ServerData.ServerSecondPhrase);                    
                }
                else
                {
                    msg = GetResponse(msg.Message);
                }
                client.ChatHistory.Add(msg);
                SendMessage(client, msg);                
            }


            //while (true)
            //{
            //    try
            //    {
            //        NetworkStream stream = Connection.Client.GetStream();
            //        byte[] data = new byte[256]; // буфер для получаемых данных
            //        StringBuilder builder = new StringBuilder();
            //        int bytes = 0;
            //        do
            //        {
            //            bytes = stream.Read(data, 0, data.Length);
            //            //data = Encoding.UTF8.GetBytes("Строка для конвертации");
            //            builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            //        }
            //        while (stream.DataAvailable);

            //        string message = builder.ToString();
            //        if (message.Length > 0)
            //        {
            //            MessageModel msg = MessageModel.CreateModel(message);
            //            if (msg != null) { this._dispatcher.Invoke(new Action(() => Chat.Add(msg))); }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("Соединение прервано " + ex.Message);
            //    }
            //}

            //try
            //{
            //    string response = Communication.ReceiveMessage();
            //    if (response.Length > 0)
            //    {
            //        MessageModel msg = MessageModel.CreateModel(response.ToString());
            //        if (msg != null) { this._dispatcher.Invoke(new Action(() => Chat.Add(msg))); }
            //    }
            //}
            //catch (SocketException ex) when (ex.ErrorCode == 10004)
            //{
            //    throw new SocketException();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

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


    }
}
