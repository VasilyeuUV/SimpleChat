using System.Windows;
using System.Windows.Input;
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


        #region COMMANDS
        private ICommand _closeApplicationCommand = null;
        public ICommand CloseApplicationCommand =>
            _closeApplicationCommand ??= new LambdaCommand(obj => { Application.Current.Shutdown(); });


        #endregion


    }
}
