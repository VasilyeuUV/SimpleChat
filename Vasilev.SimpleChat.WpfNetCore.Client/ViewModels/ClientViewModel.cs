using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands;
using Vasilev.SimpleChat.WpfNetCore.Client.Models;
using Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels
{
    internal class ClientViewModel : ViewModelBase
    {
        #region UserName

        private string _userName = null;

        /// <summary>
        /// User NickName
        /// </summary>
        public string UserName
        {
            get => _userName;
            set
            {
                Set(ref _userName, value);

                int maxLength = 50;
                _userName = _userName.Length > maxLength 
                    ? _userName.Substring(0, maxLength) 
                    : _userName;
            }
        }

        #endregion

        #region UserMessage

        private string _userMessage = string.Empty;

        /// <summary>
        /// User Message
        /// </summary>
        public string UserMessage
        {
            get => _userMessage;
            set => Set(ref _userMessage, value);
        }




        #endregion



        #region CONNECTION

        private ConnectionModel _connection = null;

        /// <summary>
        /// Client connection parameters and connected status
        /// </summary>
        public ConnectionModel Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new ConnectionModel()
                    {
                        IsConnected = false,
                        Port = 8888
                    };
                    string host = Dns.GetHostName();
                    _connection.Ip = Dns.GetHostEntry(host).AddressList[0];
                    DoWork();

                }
                return _connection;
            }
        }

        #endregion


        #region CHAT

        private MessageModel _selectedMessage = null;

        /// <summary>
        /// Selected Message
        /// </summary>
        public MessageModel SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }


        private ObservableCollection<MessageModel> _chat = null;

        /// <summary>
        /// Chat
        /// </summary>
        public ObservableCollection<MessageModel> Chat
        {
            get
            {
                if (_chat == null)
                {
                    _chat = new ObservableCollection<MessageModel>();
                    _chat.Add(new MessageModel() { Author = "Иванов", Message = string.Format("Привет\r\nHello!") });
                    _chat.Add(new MessageModel() { Author = "Петров", Message = "Здоров" });
                    _chat.Add(new MessageModel() { Author = "Иванов", Message = "Пока" });
                    _chat.Add(new MessageModel() { Author = "Петров", Message = "Hello, World!, однако" });
                    _chat.Add(new MessageModel() { Author = "Петров", Message = "Hello, World!, однако" });
                }
                return _chat;
            }
        }

        #endregion

        #region COMMANDS

        private ICommand _sendMessageCommand = null;
        public ICommand SendMessageCommand =>
            _sendMessageCommand ??= new LambdaCommand(
                obj => 
                {
                    Chat.Add( new MessageModel()
                    {
                        Author = UserName,
                        Message = UserMessage
                    });
                    UserMessage = string.Empty;
                    SelectedMessage = Chat.Last();
                },
                obj =>
                {
                    return (string.IsNullOrWhiteSpace(UserMessage)) ? false : true;
                }
                );


        #endregion



        #region METHODS

        /// <summary>
        /// Job
        /// </summary>
        private void DoWork()
        {
            TcpClient client = new TcpClient();
            Connection.IsConnected = ConnectToServer(ref client);

            while (Connection.IsConnected)
            {
                try
                {
                    byte[] data = new byte[256];
                    StringBuilder response = new StringBuilder();
                    NetworkStream stream = client.GetStream();

                    do
                    {
                        int bytes = stream.Read(data, 0, data.Length);
                        response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable); // пока данные есть в потоке

                    Chat.Add(new MessageModel() { Author = "Server", Message = response.ToString() });

                }
                catch (SocketException ex)
                {
                    MessageBox.Show("SocketException: {0}", ex.Message);
                    throw new SocketException();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: {0}", ex.Message);
                    throw new Exception();
                }

                Connection.IsConnected = false;
            }
            
        }

        /// <summary>
        /// Connection to Server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private bool ConnectToServer(ref TcpClient client)
        {
            try
            {
                client.Connect(Connection.Ip, Connection.Port);
                MessageBox.Show("Подключен к серверу");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        #endregion


    }
}
