using System.Collections.Generic;
using System.Linq;

namespace Vasilev.SimpleChat.ConsNetCore.Menu.Base
{
    internal abstract class MenuBase
    {
        internal delegate void method();

        protected string _menuName = string.Empty;
        protected List<KeyValuePair<method, string>> _methods = default;
        protected KeyValuePair<method, string> _selectedMethod = default;


        /// <summary>
        /// Display menu
        /// </summary>
        internal void DisplayMenu()
        {
            ConsoleMenu menu = new ConsoleMenu(_methods);
            do
            {
                _selectedMethod = menu.Navigate(_menuName);
                if (_selectedMethod.Key == _methods.Last().Key) { break; }
                _selectedMethod.Key();
            } while (true);
        }


    }
}
