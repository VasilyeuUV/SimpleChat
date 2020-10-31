using System;
using System.Collections.Generic;
using System.Linq;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    /// <summary>
    /// Console menu class
    /// </summary>
    internal class ConsoleMenu
    {
        private delegate int action(int index);

        private List<KeyValuePair<ServerManager.method, string>> _menuItems;
        private Dictionary<ConsoleKey, action> keyActions = default;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="methods"></param>
        public ConsoleMenu(List<KeyValuePair<ServerManager.method, string>> methods)
        {
            this._menuItems = methods.ToList();
            this.keyActions = new Dictionary<ConsoleKey, action>()
            {
                [ConsoleKey.UpArrow] = SelectPrevMenuItem,
                [ConsoleKey.DownArrow] = SelectNextMenuItem,
                [ConsoleKey.Escape] = ReturnFromMenu,
                [ConsoleKey.Enter] = SelectMenuItem
            };
        }

        public ConsoleMenu(List<KeyValuePair<MenuManager.method, string>> methods)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public KeyValuePair<ServerManager.method, string> Navigate(string op)
        {
            KeyValuePair<ServerManager.method, string> _selectedMenuItem = this._menuItems.First();

            ConsoleKeyInfo key;
            do
            {
                PrintMenu(op, _selectedMenuItem.Key);
                key = Console.ReadKey();
                _selectedMenuItem = _menuItems[keyActions[key.Key](_menuItems.IndexOf(_selectedMenuItem))];
            }
            while (key.Key != ConsoleKey.Enter);

            return _selectedMenuItem;
        }


        /// <summary>
        /// Menu item to Display
        /// </summary>
        /// <param name="op"></param>
        private void PrintMenu(string op, ServerManager.method key)
        {
            Console.Clear();

            Console.WriteLine(op);
            Console.WriteLine();

            foreach (var menuItem in _menuItems)
            {
                if (key == menuItem.Key)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menuItem.Value);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine(menuItem.Value); }
            }
        }







        /// <summary>
        /// Select preview menu item
        /// </summary>
        private int SelectNextMenuItem(int index)
        {
            return (index + 1 >= _menuItems.Count) ? 0 : ++index;
        }

        /// <summary>
        ///  Select next menu item
        /// </summary>
        private int SelectPrevMenuItem(int index)
        {
            return (index - 1 < 0) ? _menuItems.Count - 1 : --index;
        }

        /// <summary>
        /// return last menu item 
        /// </summary>
        private int ReturnFromMenu(int index)
        {
            return _menuItems.Count - 1;
        }


        /// <summary>
        /// Select Menu Item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int SelectMenuItem(int index)
        {
            return index;
        }







        ///// <summary>
        ///// Display Menu
        ///// </summary>
        ///// <returns>selected Menu item</returns>
        //public int PrintMenu(string op)
        //{
        //    ConsoleKeyInfo key;
        //    do
        //    {
        //        Console.Clear();

        //        Console.WriteLine(op);
        //        Console.WriteLine();

        //        // MENU
        //        if (counter >= menuItems.Length) { counter = menuItems.Length - 1; }

        //        for (int i = 0; i < menuItems.Length; i++)
        //        {
        //            if (counter == i)
        //            {
        //                Console.BackgroundColor = ConsoleColor.Cyan;
        //                Console.ForegroundColor = ConsoleColor.Black;
        //                Console.WriteLine(menuItems[i]);
        //                Console.BackgroundColor = ConsoleColor.Black;
        //                Console.ForegroundColor = ConsoleColor.White;
        //            }
        //            else
        //                Console.WriteLine(menuItems[i]);
        //        }
        //        key = Console.ReadKey();
        //        if (key.Key == ConsoleKey.UpArrow)
        //        {
        //            if (--counter == -1) counter = menuItems.Length - 1;
        //        }
        //        if (key.Key == ConsoleKey.DownArrow)
        //        {
        //            if (++counter == menuItems.Length) counter = 0;
        //        }
        //        if (key.Key == ConsoleKey.Escape)
        //        {
        //            return menuItems.Length - 1;                // last menu must be Exit
        //        }
        //    }
        //    while (key.Key != ConsoleKey.Enter);
        //    return counter;
        //}
    }
}
