using System;
using Vasilev.SimpleChat.ConsNetCore.Menu;

namespace SimpleChat
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuManager mainMenu = new MenuManager();
            mainMenu.DisplayMenu();
        }
    }
}
