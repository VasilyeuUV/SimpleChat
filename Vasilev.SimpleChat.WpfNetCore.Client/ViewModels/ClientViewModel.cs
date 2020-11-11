using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Vasilev.SimpleChat.ConsNetCore.Communication.Models;
using Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands;
using Vasilev.SimpleChat.WpfNetCore.Client.Models;
using Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels
{
    internal class ClientViewModel : ViewModelBase
    {
        private Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;


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

        #region SelectedMessage
        private MessageModel _selectedMessage = null;

        /// <summary>
        /// Selected Message
        /// </summary>
        public MessageModel SelectedMessage
        {
            get => _selectedMessage;
            set => Set(ref _selectedMessage, value);
        }
        #endregion

        #region CHAT

        private ObservableCollection<MessageModel> _chat = null;

        /// <summary>
        /// Chat
        /// </summary>
        public ObservableCollection<MessageModel> Chat => _chat ??= new ObservableCollection<MessageModel>();

        #endregion

        #region Communication

        private CommunicationModel _communication = null;
        public CommunicationModel Communication
        { 
            get
            {
                if (_communication == null)
                {
                    _communication = new CommunicationModel(Connection.TcpClient);
                }
                return _communication;
            }
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
                    _connection.ConnectChanged += _connection_ConnectChanged;

                    string host = Dns.GetHostName();
                    _connection.Ip = Dns.GetHostEntry(host).AddressList[0];
                    _connection.TcpClient = new TcpClient();

                    ListenServerAsync();
                }
                return _connection;
            }
        }

        private void _connection_ConnectChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Connection");
        }


        private async void ListenServerAsync()
        {
            await Task.Run(() => ListenServer()); 
        }


        private void ListenServer()
        {
            while (true)
            {
                if (!Connection.IsConnected)
                {
                    Connection.IsConnected = ConnectToServer();    // try to connect to server
                    Task.Delay(1000);
                }
                else
                {
                    Task.Delay(10);
                    GetMessage();                  
                }
            }
        }



        /// <summary>
        /// Connection to Server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private bool ConnectToServer()
        {
            try
            {
                Connection.TcpClient.Connect(Connection.Ip, Connection.Port);    
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /// <summary>
        /// Get server message
        /// </summary>
        private void GetMessage()
        {
            try
            {
                NetworkStream stream = Connection.TcpClient.GetStream();

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
                        MessageBox.Show(ex.Message);
                        Connection.TcpClient.Close();
                        Connection.IsConnected = false;
                        return;
                    }
                }

                string message = response.ToString();
                if (message.Length > 0)
                {
                    MessageModel msg = MessageModel.CreateModel(message);

                    if (msg == null) { return; }
                    if (msg.Message == "0")
                    {
                        msg.Message = "Сервер завершил сеанс";
                        Disconnect();
                    }

                    this._dispatcher.Invoke(new Action(() => Chat.Add(msg)));

                }
            }
            catch (Exception)
            {
                Connection.TcpClient.Close();
                Connection.IsConnected = false;
            }
        }


        private void Disconnect()
        {
            Connection.IsConnected = false;
        }



        #endregion

        #region COMMANDS

        #region SendMessageCommand

        private ICommand _sendMessageCommand = null;
        public ICommand SendMessageCommand =>
            _sendMessageCommand ??= new LambdaCommand(
                obj =>
                {
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        UserName = UserMessage;
                    }
                    SendMessage(UserMessage);
                    UserMessage = string.Empty;
                    SelectedMessage = Chat.Count > 0 ? Chat.Last() : null;
                },
                obj =>
                {
                    return (/*!Connection.IsConnected */
                            /*||*/ string.IsNullOrWhiteSpace(UserMessage))
                            ? false : true;
                }
                );



        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name = "userMessage" ></ param >
        private void SendMessage(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage)) { return; }

            string msg = string.Format($"{UserName}\n{userMessage}");
            Communication.TransmitMessage(msg);
        } 

        #endregion



        #endregion

    }
}
