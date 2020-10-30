using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands;
using Vasilev.SimpleChat.WpfNetCore.Client.Models;
using Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels
{
    internal class ClientViewModel : ViewModelBase
    {
        #region UserName

        private string _userName = "Anton";

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
                _userName = _userName.Length > maxLength ? _userName.Substring(0, maxLength) : _userName;
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


        private bool _connected = false;

        /// <summary>
        /// Connection status
        /// </summary>
        public bool Connected
        {
            get => _connected;
            set => Set(ref _connected, value);
        }


        private string _ip = string.Empty;

        /// <summary>
        /// Client IP
        /// </summary>
        public string Ip
        {
            get => _ip;
            set => Set(ref _ip, value);
        }



        private string _port = string.Empty;

        /// <summary>
        /// Connection Port
        /// </summary>
        public string Port
        {
            get => _port;
            set => Set(ref _port, value);
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
                    return (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(UserMessage)) ? false : true;
                }
                );


        #endregion

    }
}
