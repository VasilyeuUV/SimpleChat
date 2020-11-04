using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
            _server?.ServerData.QaDictionary.SelectMany(x => x.Key).ToList();

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

                while (_server.TcpListener != null)
                {
                    TcpClient tcpClient = _server?.TcpListener?.AcceptTcpClient();
                    if (tcpClient != null)
                    {
                        AddNewClient(tcpClient);
                    }
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

        /// <summary>
        /// Add new TCP client
        /// </summary>
        /// <param name="tcpClient"></param>
        private void AddNewClient(TcpClient tcpClient)
        {
            try
            {
                ClientModel client = new ClientModel();

                // получаем сетевой поток для чтения и записи
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    client.Stream = stream;

                    // сообщение для отправки клиенту
                    string response = "Приветствую Вас.\nНазовите Ваше имя.";
                    MessageModel message = new MessageModel()
                    {
                        Author = _server.ServerName,
                        Dtg = DateTime.Now,
                        Message = response
                    };

                    if (SendMessage(client, message))
                    {
                        client.ChatHistory.Add(message);
                    }
                    _server.ServerData.ConnectedClients.Add(client);


                    //Task doWork = new Task(async () => await ChatDoWorkAsync(client));
                    //doWork.Start();



                    // закрываем поток
                    //stream.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool SendMessage(ClientModel client, MessageModel message)
        {
            try
            {
                // преобразуем сообщение в массив байтов
                byte[] data = Encoding.UTF8.GetBytes(message.ToString());

                // отправка сообщения
                client.Stream.Write(data, 0, data.Length);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //private async Task ChatDoWorkAsync(ClientModel client)
        //{
        //    using (client.Stream)
        //    {
        //        byte[] data = Encoding.Unicode.GetBytes(message);
        //        clients[0].Stream.Write(data, 0, data.Length); //передача данных
        //    }

        //}

    }
}
