using System;
using System.Collections.Generic;

namespace Vasilev.SimpleChat.ConsNetCore.Server.DataLayer
{
    internal class QuestionAnswerData
    {
        internal IDictionary<string[], string[]> QADictionary { get; private set; }

        /// <summary>
        /// CTOR
        /// </summary>
        internal QuestionAnswerData()
        {
            this.QADictionary = CreateDictionary();
        }

        /// <summary>
        /// Create dictionary
        /// </summary>
        /// <returns></returns>
        private IDictionary<string[], string[]> CreateDictionary()
        {
            var dict = new Dictionary<string[], string[]>();

            dict.Add(new[] { "привет", "здоров", "здравствуй", "доброго" }, new[] { "Привет", "Здоров", "Здравствуй", "Доброго" });
            dict.Add(new[] { "как дела", }, new[] { "Нормально", "Отлично", "Сносно", "Не очень" });
            dict.Add(new[] { "что делаешь", }, new[] { "Туплю", "Думаю", "Отдыхаю", "Работаю" });
            dict.Add(new[] { "будешь", }, new[] { "Нет", "Да", "Возможно", "Не исключено" });
            dict.Add(new[] { "пока" }, new[] { "Пока", "Счастливо", "Удачи", "Всего доброго" });

            return dict;
        }
    }
}
