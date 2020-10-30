using System;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    internal static class MenuManager
    {
        internal delegate void method();

        internal static void DisplayMainMenu()
        {
            string operation = "МЕНЮ:";
            string[] items = { "Управление сервером", "Выход" };
            method[] methods = new MenuManager.method[] { ServerControl, Exit };
            SelectMenuItem(operation, items, methods);
        }

        private static void ServerControl()
        {
            ServerManager.DisplayMenu();
        }

        private static void Exit()
        {
            ToDisplay.WaitForContinue("Работа завершена.");
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

    }
}
