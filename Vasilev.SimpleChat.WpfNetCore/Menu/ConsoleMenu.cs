using System;
using System.Collections.Generic;
using System.Linq;
using Vasilev.SimpleChat.ConsNetCore.Menu.Base;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    /// <summary>
    /// Console menu class
    /// </summary>
    internal class ConsoleMenu
    {
        private delegate int action(int index);

        private Dictionary<ConsoleKey, action> _keyActions = default;
        private List<KeyValuePair<MenuBase.method, string>> _menuItems;

        public ConsoleMenu(List<KeyValuePair<MenuBase.method, string>> methods)
        {
            this._menuItems = methods;
            this._keyActions = new Dictionary<ConsoleKey, action>()
            {
                [ConsoleKey.UpArrow] = SelectPrevMenuItem,
                [ConsoleKey.DownArrow] = SelectNextMenuItem,
                [ConsoleKey.Escape] = ReturnFromMenu,
                [ConsoleKey.Enter] = SelectMenuItem
            };
        }

        /// <summary>
        /// Menu navigation
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        internal KeyValuePair<MenuBase.method, string> Navigate(string operationName)
        {
            KeyValuePair<MenuBase.method, string> _selectedMenuItem = this._menuItems.First();

            ConsoleKeyInfo key;
            do
            {
                PrintMenu(operationName, _selectedMenuItem.Key);
                key = Console.ReadKey();

                if (_keyActions.Any(x => x.Key == key.Key))
                {
                    _selectedMenuItem = _menuItems[_keyActions[key.Key](_menuItems.IndexOf(_selectedMenuItem))];
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return _selectedMenuItem;
        }


        /// <summary>
        /// Menu item to Display
        /// </summary>
        /// <param name="op"></param>
        private void PrintMenu(string op, MenuBase.method key)
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
            Console.WriteLine();
        }



        #region MENU NAVIGATION

        /// <summary>
        /// Select preview menu item
        /// </summary>
        private int SelectNextMenuItem(int index) => (index + 1 >= _menuItems.Count) ? 0 : ++index;


        /// <summary>
        ///  Select next menu item
        /// </summary>
        private int SelectPrevMenuItem(int index) => (index - 1 < 0) ? _menuItems.Count - 1 : --index;

        /// <summary>
        /// return last menu item 
        /// </summary>
        private int ReturnFromMenu(int index) => _menuItems.Count - 1;


        /// <summary>
        /// Select Menu Item
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int SelectMenuItem(int index) => index; 

        #endregion
    }
}
