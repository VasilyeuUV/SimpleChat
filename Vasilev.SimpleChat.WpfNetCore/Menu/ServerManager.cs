using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Vasilev.SimpleChat.ConsNetCore.Menu.Base;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    //internal delegate void method();

    internal class ServerManager : MenuBase, IDisposable
    {     
        private static ServerModel server = null;

        /// <summary>
        /// CTOR
        /// </summary>
        internal ServerManager()
        {
            _menuName = "СЕРВЕР:";
            _methods = new List<KeyValuePair<method, string>>()
            {
                new KeyValuePair<method, string>(StartServer, "Запустить сервер"),
                new KeyValuePair<method, string>(ViewQuestions, "Перечень возможных вопросов"),
                new KeyValuePair<method, string>(ViewClients, "Список присоединенных пользователей"),
                new KeyValuePair<method, string>(StopServer, "Остановить сервер"),
                new KeyValuePair<method, string>(Back, "Назад"),
            };
        }

        /// <summary>
        /// Possible questions
        /// </summary>
        private void ViewQuestions()
        {
            ToDisplay.ViewTitle(_selectedMethod.Value.ToUpper(), true);

            if (server == null)
            {
                ToDisplay.WaitForContinue("Сервер не запущен.");
                return;
            }

            ToDisplay.ViewBody("Здесь отобразится список вопросов");
            ToDisplay.WaitForContinue();
        }

        /// <summary>
        /// View active Clients
        /// </summary>
        private void ViewClients()
        {
            ToDisplay.ViewTitle(_selectedMethod.Value.ToUpper(), true); 

            if (server == null)
            {
                ToDisplay.WaitForContinue("Сервер не запущен. Список пуст.");
                return;
            }

            ToDisplay.ViewBody("Здесь отобразится список присоединенных клиентов");
            ToDisplay.WaitForContinue();
        }

        /// <summary>
        /// Return to the menu one level higher
        /// </summary>
        private static void Back()
        {
        }


        /// <summary>
        /// Start TCP server
        /// </summary>
        private void StartServer()
        {
            if (server != null)
            {
                ToDisplay.WaitForContinue("Сервер уже работает");
                return;
            }

            server = ServerModel.CreateServer();
            server.Listener.Start();
            ToDisplay.WaitForContinue("Сервер запущен");
        }


        /// <summary>
        /// Stop TCP server
        /// </summary>
        private void StopServer()
        {
            if (server == null)
            {
                ToDisplay.WaitForContinue("Сервер еще не запущен");
                return;
            }

            server?.Listener?.Stop();
            server = null;
            ToDisplay.WaitForContinue("Сервер остановлен");
        }



        #region IDISPOSABLE

        private bool _disposed;


        public void Dispose()
        {
            Dispose(true);
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed) { return; }

            _disposed = true;

            // Освобождение управляемых ресурсов
            if (server != null) { StopServer(); }
            _methods.Clear();
        }

        #endregion
    }
}
