using System;
using System.Collections.Generic;

namespace Vasilev.SimpleChat.ConsNetCore.Server.DataLayer
{
    public class QuestionAnswerData
    {
        public IDictionary<string[], string[]> QADictionary { get; private set; }


        public QuestionAnswerData()
        {
            this.QADictionary = CreateDictionary();
        }

        private IDictionary<string[], string[]> CreateDictionary()
        {
            var dict = new Dictionary<string[], string[]>();

            dict.Add(new[] { "привет", "здоров", "здравствуй", "доброго" }, new[] { "Привет", "Здоров", "Здравствуй", "Доброго" });
            dict.Add(new[] { "пока" }, new[] { "Пока", "Счастливо", "Удачи", "Всего доброго" });



            return dict;
        }
    }
}
