using ChatServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vasilev.SimpleChat.ConsNetCore.Server.DataLayer;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Logic
{
    public class ServerControl
    {

        private ServerModel _serverModel = null;
        private ICollection<UserModel> _users = null;

        private ConnectionData _connectionData = null;

        private QuestionAnswerData _qaData = null;




        public ICollection<string> GetQuestions() => _qaData.QADictionary.SelectMany(x => x.Key).ToList();


        #region CLIENTS

        /// <summary>
        /// Returns the names of the connected clients
        /// </summary>
        /// <returns></returns>
        public ICollection<string> GetClients() => _connectionData.ConnectedClients.Select(x => x.NickName).ToList();


        #endregion



        #region SERVER
        /// <summary>
        /// Start Server
        /// </summary>
        /// <returns></returns>
        public void StartServer()
        {
            _connectionData = new ConnectionData();
            _qaData = new QuestionAnswerData();

            _serverModel = new ServerModel();
            _serverModel?.Listener?.Start();
        }

        /// <summary>
        /// Stop Server
        /// </summary>
        public void StopServer()
        {
            _serverModel?.Listener?.Stop();
            _serverModel = null;

            _connectionData.Clear();
            _connectionData = null;
        } 
        #endregion
    }
}
