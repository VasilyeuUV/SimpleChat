using System.Linq;
using System.Windows;
using System.Windows.Input;
using Vasilev.SimpleChat.ConsNetCore.Communication.Models;
using Vasilev.SimpleChat.WpfNetCore.Client.Infrastructure.Commands;
using Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels
{

    /// <summary>
    /// Main Window View Model
    /// </summary>
    internal class MainWindowViewModel : ViewModelBase
    {

        #region ЗАГОЛОВОК ОКНА

        private string _title = "SimpleChat";       

        /// <summary>
        /// Main Window Title
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #endregion

        #region STATUSBAR
        private StatusBarViewModel _statusBarViewModel = null;

        public StatusBarViewModel StatusBarViewModel
        {
            get
            {
                if (_statusBarViewModel == null)
                {
                    _statusBarViewModel = new StatusBarViewModel();
                }
                return _statusBarViewModel;
            }
        }
        #endregion


        #region ClientViewModel

        private ClientViewModel _client = null;

        /// <summary>
        /// SimpleChat Client
        /// </summary>
        public ClientViewModel Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new ClientViewModel();
                }
                return _client;
            }
        }



        #endregion


        #region IsFocusedTbx
        private bool _isFocusedTbx = true;

        /// <summary>
        /// Focused TextBox
        /// </summary>
        public bool IsFocusedTbx
        {
            get => _isFocusedTbx;
            set => Set(ref _isFocusedTbx, value);
        }
        #endregion


        #region COMMANDS

        #region CloseApplicationCommand
        private ICommand _closeApplicationCommand = null;
        public ICommand CloseApplicationCommand =>
            _closeApplicationCommand ??= new LambdaCommand(
                obj =>
                {
                    Client.Close();
                    Application.Current.Shutdown();
                }
                ); 
        #endregion

        #region SendMessageCommand

        private ICommand _sendMessageCommand = null;
        public ICommand SendMessageCommand =>
            _sendMessageCommand ??= new LambdaCommand(
                obj =>
                {
                    if (string.IsNullOrWhiteSpace(Client.UserName))
                    {
                        Client.UserName = Client.UserMessage;
                    }
                    Client.SendMessage(Client.UserMessage);
                    Client.UserMessage = string.Empty;
                    //SelectedMessage = Client.Chat.Count > 0 ? Client.Chat.Last() : null;
                },
                obj =>
                {
                    return (!Client.Connection.IsConnected
                            || string.IsNullOrWhiteSpace(Client.UserMessage))
                            ? false : true;
                }
                );

        #endregion

        #endregion


    }
}
