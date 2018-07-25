using System;

namespace Laboratory.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key;
            Client client = new Client();
            client.SendMessage("hello, thanks for reading.");

            Console.WriteLine("输入Q键退出");
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Q);

            client.Release();
        }
    }
}
