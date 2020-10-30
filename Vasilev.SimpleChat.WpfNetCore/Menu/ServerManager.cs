using System;
using System.Collections.Generic;
using System.Text;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    internal static class ServerManager
    {

        private static ServerModel server = null;
        private static string[] items = { 
            "Перечень возможных вопросов", 
            "Запустить сервер", 
            "Список присоединенных пользователей", 
            "Остановить сервер", 
            "Назад" 
        };



        internal delegate void method();

        internal static void DisplayMenu()
        {
            string operation = "ОПЕРАЦИИ:";
            method[] methods = new method[] {
                               ViewQuestions,               // "Перечень возможных вопросов"
                               StartServer,                 // "Стартануть сервер",
                               ViewClients,                 // "Список присоединенных пользователей"
                               StopServer,                  // "Остановить сервер"
                               Back };                      // "Назад"
            SelectMenuItem(operation, items, methods);
        }

        /// <summary>
        /// Possible questions
        /// </summary>
        private static void ViewQuestions()
        {
            ToDisplay.ViewTitle(items[0].ToUpper(), true);

            ToDisplay.ViewBody("Здесь отобразится список вопросов");
            ToDisplay.WaitForContinue();
        }

        private static void ViewClients()
        {
            ToDisplay.ViewTitle(items[2].ToUpper(), true);

            if (server == null)
            {
                ToDisplay.WaitForContinue("Сервер не запущен. Список пуст.");
                return;
            }

            ToDisplay.ViewBody("Здесь отобразится список присоединенных клиентов");
            ToDisplay.WaitForContinue();
        }

        /// <summary>
        /// Select menu item
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="items"></param>
        /// <param name="methods"></param>
        internal static void SelectMenuItem(string operation, string[] items, method[] methods)
        {
            ConsoleMenu menu = new ConsoleMenu(items);
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu(operation);
                Console.WriteLine();
                methods[menuResult]();
            } while (menuResult != items.Length - 1);
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
        private static void StartServer()
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
        private static void StopServer()
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
    }
}
