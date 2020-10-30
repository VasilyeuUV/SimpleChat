using System;
using Vasilev.SimpleChat.ConsNetCore.Server.Models;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = ServerModel.CreateServer();

            Console.WriteLine($"Ip: {server.Ip}");
            Console.WriteLine($"Port: {server.Port}");

            //server.Listener.Start();
            Console.ReadKey();
        }
    }
}
