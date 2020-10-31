using System.Collections.Generic;
using System.Linq;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    internal static class MenuManager
    {
        internal delegate void method();

        private static List<KeyValuePair<method, string>> methods = new List<KeyValuePair<method, string>>()
        {
            new KeyValuePair<method, string>(ServerControl, "Управление сервером"),
            new KeyValuePair<method, string>(Exit, "Выход"),
        };

        private static KeyValuePair<method, string> _selectedMethod = default;





        internal static void DisplayMainMenu()
        {
            //string operation = "МЕНЮ:";
            //string[] items = { "Управление сервером", "Выход" };
            //method[] methods = new MenuManager.method[] { ServerControl, Exit };
            //SelectMenuItem(operation, items, methods);

            ExecuteMenuItemMethod("МЕНЮ:", methods);
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




        private static void ServerControl()
        {
            ServerManager.DisplayMenu();
        }

        private static void Exit()
        {
            ToDisplay.WaitForContinue("Работа завершена.");
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
        //        menuResult = menu.Navigate(operation);
        //        Console.WriteLine();
        //        methods[menuResult]();
        //    } while (menuResult != items.Length - 1);
        //}

    }
}
