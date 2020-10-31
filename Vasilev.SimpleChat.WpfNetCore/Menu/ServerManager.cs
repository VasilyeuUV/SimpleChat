using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    internal static class ServerManager
    {
        internal delegate void method();

        private static ServerModel server = null;

        //private static string[] items = {
        //    "Запустить сервер",
        //    "Перечень возможных вопросов", 
        //    "Список присоединенных пользователей", 
        //    "Остановить сервер", 
        //    "Назад" 
        //};

        //private static Dictionary<method, string> methods = new Dictionary<method, string>()
        //{
        //    [StartServer] = "Запустить сервер",
        //    [ViewQuestions] = "Перечень возможных вопросов",
        //    [ViewClients] = "Список присоединенных пользователей",
        //    [StopServer] = "Остановить сервер",
        //    [Back] = "Назад",
        //};

        private static List<KeyValuePair<method, string>> methods = new List<KeyValuePair<method, string>>()
        {
            new KeyValuePair<method, string>(StartServer, "Запустить сервер"),
            new KeyValuePair<method, string>(ViewQuestions, "Перечень возможных вопросов"),
            new KeyValuePair<method, string>(ViewClients, "Список присоединенных пользователей"),
            new KeyValuePair<method, string>(StopServer, "Остановить сервер"),
            new KeyValuePair<method, string>(Back, "Назад"),
        };

        private static KeyValuePair<method, string> _selectedMethod = default;





        internal static void DisplayMenu()
        {
            ExecuteMenuItemMethod("ОПЕРАЦИИ:", methods);
        }

        private static void ExecuteMenuItemMethod(string operation, List<KeyValuePair<method, string>> methods)
        {
            ConsoleMenu menu = new ConsoleMenu(methods);
            do
            {
                _selectedMethod = menu.Navigate(operation);
                if (_selectedMethod.Key == methods.Last().Key) { break; }
                _selectedMethod.Key();
            } while (true);            
        }

        ///// <summary>
        ///// Select menu item
        ///// </summary>
        ///// <param name="operation"></param>
        ///// <param name="items"></param>
        ///// <param name="methods"></param>
        //internal static void SelectMenuItem(string operation, string[] items, method[] methods)
        //{
        //    ConsoleMenu menu = new ConsoleMenu(items);
        //    int menuResult;
        //    do
        //    {
        //        menuResult = menu.PrintMenu(operation);
        //        Console.WriteLine();
        //        methods[menuResult]();
        //    } while (menuResult != items.Length - 1);
        //}





        /// <summary>
        /// Possible questions
        /// </summary>
        private static void ViewQuestions()
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

        private static void ViewClients()
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
