using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Vasilev.SimpleChat.ConsNetCore.Communication.Models;
using Vasilev.SimpleChat.WpfNetCore.Client.Models;
using Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels
{
    internal class ClientViewModel : ViewModelBase
    {
        private Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        private NetworkStream _stream = null;

        private bool _isActiveClient = true;
        private readonly string _disconnectPhrase = "пока";

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
            while (_isActiveClient)
            {
                UserName = "";
                Chat.Clear();
                await Task.Run(() => ListenServer());
            }
            Chat.Add(MessageModel.CreateModel(DateTime.Now, "Внимание!", "Сервер прервал соединение."));
            SelectedMessage = Chat.Last();
        }


        private void ListenServer()
        {
            Connection.TcpClient = new TcpClient();
            while (!Connection.IsConnected)
            {
                Task.Delay(1000);
                Connection.IsConnected = ConnectToServer();    // try to connect to server
            }

            _stream = Connection.TcpClient?.GetStream();
            while (Connection.IsConnected)
            {
                Task.Delay(10);
                try
                {
                    GetMessage(_stream);
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }                
            }
            Close();
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



        #region GET/SEND MESSAGE
        /// <summary>
        /// Get server message
        /// </summary>
        private void GetMessage(NetworkStream stream)
        {
            try
            {
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
                        MessageBox.Show(ex.Message);
                        Disconnect();
                        return;
                    }
                }

                string message = response.ToString();
                if (message.Length > 0)
                {
                    MessageModel msg = MessageModel.CreateModel(message);

                    if (msg == null) { return; }
                    if (msg.Message.ToLower() == _disconnectPhrase)
                    {
                        Connection.IsConnected = false;                        
                        return;
                    }

                    this._dispatcher.Invoke(new Action(() => 
                    { 
                        Chat.Add(msg);
                        SelectedMessage = Chat.Last();
                    }));
                }
            }
            catch (Exception)
            {
                Disconnect();
            }
        }


        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name = "userMessage" ></ param >
        internal void SendMessage(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage)) { return; }

            string msg = string.Format($"{UserName}\n{userMessage}");

            if (_stream.CanWrite)
            {
                try
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg);
                    _stream.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    Disconnect();
                    Environment.Exit(0);              
                }
            }
        } 
        #endregion



        #region CLOSE_CLIENT

        internal void Close()
        {
            _isActiveClient = false;
            Disconnect();
        }

        private void Disconnect()
        {            
            Connection.TcpClient?.Close();
            Connection.TcpClient = null;
            Connection.IsConnected = false;            
        } 
        #endregion




        #endregion
    }
}
