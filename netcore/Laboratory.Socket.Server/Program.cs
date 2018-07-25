using System;
using System.Net;
using System.Net.Sockets;

namespace Laboratory.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server is running……");

            IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
            TcpListener listener = new TcpListener(ip, 8500);

            listener.Start();   // 开始监听
            Console.WriteLine("Start listening……");

            while (true)
            {
                // 获取一个连接，同步方法，在此处中端
                TcpClient client = listener.AcceptTcpClient();
                Server server = new Server(client);
            }
        }
    }
}
