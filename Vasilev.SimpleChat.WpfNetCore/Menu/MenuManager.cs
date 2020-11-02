using System;
using System.Collections.Generic;
using System.Linq;
using Vasilev.SimpleChat.ConsNetCore.Menu.Base;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    //internal delegate void method();

    internal class MenuManager : MenuBase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public MenuManager()
        {
            _menuName = "МЕНЮ:";
            _methods = new List<KeyValuePair<method, string>>()
            {
                new KeyValuePair<method, string>(ServerControl, "Управление сервером"),
                new KeyValuePair<method, string>(Exit, "Выход"),
            };
        }


        /// <summary>
        /// Server Menu (2-nd level)
        /// </summary>
        private void ServerControl()
        {
            using (var serverMenu = new ServerManager())
            {
                serverMenu.DisplayMenu();
            }
        }

        /// <summary>
        /// Exit from Server Manager
        /// </summary>
        private void Exit()
        {
            ToDisplay.WaitForContinue("Работа завершена.");
        }
    }
}
