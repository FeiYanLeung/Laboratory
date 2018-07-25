using System;
using System.IO;

namespace Laboratory.Socket.FileClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key;
            Client client = new Client();
            string filePath = Environment.CurrentDirectory + "/" + "client01.png";

            if (File.Exists(filePath))
            {
                client.BeginSendFile(filePath);
            }

            Console.WriteLine("输入Q键退出");
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Q);
        }
    }
}
