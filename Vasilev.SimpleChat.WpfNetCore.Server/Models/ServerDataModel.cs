using ChatServer.Models;
using System.Collections.Generic;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ServerDataModel
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


        #region QuestionAnswerData

        private IDictionary<string[], string[]> qaDictionary = default;

        internal IDictionary<string[], string[]> QaDictionary => qaDictionary ??= new Dictionary<string[], string[]>()
        {
            [new[] { "привет", "здоров", "здравствуй", "доброго" }] = new[] { "Привет", "Здоров", "Здравствуй", "Доброго" },
            [new[] { "как дела", }] = new[] { "Нормально", "Отлично", "Сносно", "Не очень" },
            [new[] { "что делаешь", }] = new[] { "Туплю", "Думаю", "Отдыхаю", "Работаю" },
            [new[] { "будешь", }] = new[] { "Нет", "Да", "Возможно", "Не исключено" },
            [new[] { "пока" }] = new[] { "Пока", "Счастливо", "Удачи", "Всего доброго" }
        };

        #endregion


    }
}
