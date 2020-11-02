using System.Collections.Generic;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Logic
{
    public class ServerControl
    {
        private ServerModel _serverModel = null;


        #region QADATA

        /// <summary>
        /// Get List of possible questions
        /// </summary>
        /// <returns></returns>
        public ICollection<string> GetQuestions() => _serverModel.GetQuestions();

        #endregion




        #region CLIENTS

        /// <summary>
        /// Returns the names of the connected clients
        /// </summary>
        /// <returns></returns>
        public ICollection<string> GetClients() => _serverModel.GetClient();   


        #endregion



        #region SERVER
        /// <summary>
        /// Start Server
        /// </summary>
        /// <returns></returns>
        public void StartServer()
        {
            _serverModel = new ServerModel();
            _serverModel.ServerStart();
        }

        /// <summary>
        /// Stop Server
        /// </summary>
        public void StopServer()
        {
            _serverModel?.ServerStop();
            _serverModel = null;
        } 
        #endregion
    }
}
