using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vasilev.SimpleChat.ConsNetCore.Server.DataLayer
{
    public class ConnectionData
    {


        #region ConnectedClients

        private List<UserModel> _connectedClients = null;

        /// <summary>
        /// Connected Clients
        /// </summary>
        public List<UserModel> ConnectedClients => _connectedClients ??= new List<UserModel>(); 

        #endregion

    }
}
