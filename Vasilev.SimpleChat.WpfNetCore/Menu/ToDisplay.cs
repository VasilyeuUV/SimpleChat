using System;
using System.Collections.Generic;
using System.Text;

namespace Vasilev.SimpleChat.ConsNetCore.Menu
{
    /// <summary>
    /// Class for Display string
    /// </summary>
    internal static class ToDisplay
    {

        /// <summary>
        /// View operations Title
        /// </summary>
        /// <param name="title">text to view</param>
        /// <param name="clear">true if Console clear</param>
        internal static void ViewTitle(string title, bool clear = false)
        {
            if (!clear)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{title}:");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{title}:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }


        /// <summary>
        /// View operation Title
        /// </summary>
        /// <param name="title"></param>
        internal static void ViewBody(string body)
        {
            Console.WriteLine(body);
            Console.WriteLine();
        }


        /// <summary>
        /// Wait push key 
        /// </summary>
        /// <param name="str"></param>
        internal static void Write(string str = "")
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(str);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        /// <summary>
        /// Wait push key 
        /// </summary>
        /// <param name="str"></param>
        internal static ConsoleKeyInfo WaitForContinue(string str = "")
        {
            Write(str);
            Console.WriteLine();
            Console.WriteLine("Press key to continue");
            return Console.ReadKey();
        }

    }
}
