using Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels
{
    internal class ClientViewModel : ViewModelBase
    {


        #region USER

        private string _userName = string.Empty;

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


    }
}
