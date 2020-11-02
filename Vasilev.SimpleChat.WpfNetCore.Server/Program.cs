using System;
using Vasilev.SimpleChat.ConsNetCore.Server.Logic;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverControl = new ServerControl();
            serverControl.StartServer();

            //Console.WriteLine($"Ip: {serverControl.Ip}");
            //Console.WriteLine($"Port: {serverControl.Port}");


            serverControl.StopServer();
            Console.ReadKey();
        }
    }
}
