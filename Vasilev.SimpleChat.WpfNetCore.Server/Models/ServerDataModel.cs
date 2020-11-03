using System.Collections.Generic;

namespace Vasilev.SimpleChat.ConsNetCore.Server.Models
{
    internal class ServerDataModel
    {

        #region ConnectedClients

        private ICollection<ClientModel> _connectedClients = null;

        /// <summary>
        /// Connected Clients
        /// </summary>
        internal ICollection<ClientModel> ConnectedClients => _connectedClients ??= new List<ClientModel>();

        #endregion


        #region QuestionAnswerData

        private IDictionary<string[], string[]> _qaDictionary = default;

        internal IDictionary<string[], string[]> QaDictionary => _qaDictionary ??= new Dictionary<string[], string[]>()
        {
            [new[] { "привет", "здоров", "здравствуй", "доброго" }] = new[] { "Привет", "Здоров", "Здравствуй", "Доброго" },
            [new[] { "как дела", }] = new[] { "Нормально", "Отлично", "Сносно", "Не очень" },
            [new[] { "что делаешь", }] = new[] { "Туплю", "Думаю", "Отдыхаю", "Работаю" },
            [new[] { "будешь", }] = new[] { "Нет", "Да", "Возможно", "Не исключено" },
            [new[] { "пока" }] = new[] { "Пока", "Счастливо", "Удачи", "Всего доброго" }
        };

        #endregion


        /// <summary>
        /// Clear all data
        /// </summary>
        internal void Clear()
        {
            ConnectedClients.Clear();
            _connectedClients = null;

            QaDictionary.Clear();
            _qaDictionary = null;
        }

    }
}
