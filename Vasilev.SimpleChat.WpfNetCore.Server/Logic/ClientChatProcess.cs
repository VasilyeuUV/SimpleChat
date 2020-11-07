using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Vasilev.SimpleChat.ConsNetCore.Communication.Models;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Logic
{
    internal class ClientChatProcess
    {
        private ServerModel _server = null;
        private ClientModel _client = null;



        /// <summary>
        /// CTOR
        /// </summary>
        private ClientChatProcess(ServerModel server, ClientModel client)
        {
            this._server = server;
            this._client = client;

        }

        /// <summary>
        /// Create and strt process
        /// </summary>
        /// <param name="tcpClient"></param>
        /// <returns></returns>
        internal static void Create(ServerModel server, ClientModel client)
        {
            ClientChatProcess chat = new ClientChatProcess(server, client);
            chat.Start();
        }

        /// <summary>
        /// Start chatting
        /// </summary>
        private void Start()
        {
            SendMessage(_server.ServerData.ServerFirstPhrase);

            while (true)
            {
                Task.Delay(10);
                GetMessage();
            }
        }

        private void GetMessage()
        {
            try
            {
                string response = _client.Communication.ReceiveMessage();
                if (response.Length > 0)
                {
                    MessageModel msg = MessageModel.CreateModel(response.ToString());
                    if (msg != null) 
                    { 
                        _client?.ChatHistory.Add(msg);
                        SendMessage(response);
                        Task.Delay(1000);
                        SendResponce(msg);
                    }
                }
            }
            catch (SocketException ex) when (ex.ErrorCode == 10004)
            {
                throw new SocketException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SendMessage(string msg, string author = "")
        {
            if (string.IsNullOrWhiteSpace(msg)) { return; }
            if (string.IsNullOrWhiteSpace(author)) { author = _server.ServerName; }

            MessageModel firstMessage = MessageModel.CreateModel(DateTime.Now, author, msg);

            _client.Communication.TransmitMessage(firstMessage.ToString());
        }


        private void SendResponce(MessageModel msg)
        {
            var keys = _server?.ServerData.QaDictionary.Select(x => x.Key).ToList();
            string answer = _server.ServerData.ServerErrorPhrase;

            foreach (var key in keys)
            {
                if (msg.Message.IndexOf(key, StringComparison.OrdinalIgnoreCase) > -1 )
                {
                    var answers = _server.ServerData.QaDictionary[key];
                    answer = answers[new Random().Next(0, answers.Count - 1)];
                }
            }
            SendMessage(answer);
        }
    }
}
