using System;
using System.Net;
using System.Net.Sockets;

namespace Vasilev.SimpleChat.WpfNetCore.Client.Models
{
    internal class ConnectionModel
    {

        internal TcpClient TcpClient { get; set; }

        public IPAddress Ip { get; internal set; }

        public int Port { get; internal set; }


        private bool _isConnected = false;
        public bool IsConnected
        {
            get => _isConnected;
            internal set
            {
                _isConnected = value;
                OnConnectChanged();
            }
        }


        #region CONNECT_CHANGED_EVENT

        internal event EventHandler ConnectChanged;
        protected virtual void OnConnectChanged(EventArgs e = null)
        {
            ConnectChanged?.Invoke(this, e);
        }

        #endregion

    }
}
