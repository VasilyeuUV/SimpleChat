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

        internal string ServerFirstPhrase { get; } = "Приветствую в чате.\nКак Вас называть?";
        internal string ServerDisconnectPhrase { get; } = "пока";
        internal string ServerSecondPhrase { get; } = "Что интересует, ";

        internal string ServerErrorPhrase { get; } = "Не могу ответить.\nДанный вопрос не поддерживается.";

        private List<string> _helloAnswer = new List<string>() { "Привет", "Здоров", "Здравствуй", "Доброго"  };
        private List<string> _howAreYouAnswer = new List<string>() { "Нормально", "Отлично", "Сносно", "Не очень" };
        private List<string> _whatAreYouDoingAnswer = new List<string>() { "Туплю", "Думаю", "Отдыхаю", "Работаю" };
        private List<string> _willYouAnswer = new List<string>() { "Нет", "Да", "Возможно", "Не исключено" };
        private List<string> _goodBuyAnswer = new List<string>() { "Прощай", "Счастливо", "Удачи", "Всего доброго" };

        private IDictionary<string, List<string>> _qaDictionary = default;
        internal IDictionary<string, List<string>> QaDictionary => _qaDictionary ??= new Dictionary<string, List<string>>()
        {
            ["привет"] = _helloAnswer,
            ["как дела"] = _howAreYouAnswer,
            ["что делаешь"] = _whatAreYouDoingAnswer,
            ["будешь"] = _willYouAnswer,
            ["пока"] = _goodBuyAnswer
        };

        #endregion


        /// <summary>
        /// Clear all data
        /// </summary>
        internal void Clear()
        {
            if (ConnectedClients.Count > 0)
            {
                foreach (var client in ConnectedClients)
                {
                    client.Communication.CloseCommunication();
                }
            }

            ConnectedClients.Clear();
            _connectedClients = null;

            QaDictionary.Clear();
            _qaDictionary = null;
        }

    }
}
