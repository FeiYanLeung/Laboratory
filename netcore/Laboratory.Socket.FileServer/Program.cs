using System;
using System.Net;
using System.Net.Sockets;

namespace Laboratory.Socket.FileServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Server is running……");
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ip, 8500);

            listener.Start();   //开启对控制端口8500的监听
            Console.WriteLine("Start listening……");

            while (true)
            {
                // 获取一个连接，同步方法，此处中断
                TcpClient client = listener.AcceptTcpClient();
                Server wapper = new Server(client);
                wapper.BeginRead();
            }
        }
    }
}
