using ChatServer.Models;
using System;
using System.Collections.Generic;

namespace Vasilev.SimpleChat.ConsNetCore.Server.DataLayer
{
    internal class ConnectionData
    {


        #region ConnectedClients

        private ICollection<UserModel> _connectedClients = null;

        /// <summary>
        /// Connected Clients
        /// </summary>
        internal ICollection<UserModel> ConnectedClients => _connectedClients ??= new List<UserModel>();


        /// <summary>
        /// Clear all data
        /// </summary>
        internal void Clear()
        {
            ConnectedClients.Clear();
            _connectedClients = null;
        }

        #endregion

    }
}
