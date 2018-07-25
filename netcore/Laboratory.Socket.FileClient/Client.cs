using Laboratory.Socket.Infrastructure;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Laboratory.Socket.FileClient
{
    public class Client
    {
        private const int bufferSize = 8192;
        private byte[] buffer;
        private TcpClient client;
        private NetworkStream streamToServer;

        public Client()
        {
            try
            {
                client = new TcpClient();
                client.Connect("127.0.0.1", 8500);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            buffer = new byte[bufferSize];

            // 打印连接的服务器信息
            Console.WriteLine($"Server Connected! Local:{client.Client.LocalEndPoint} --> Server:{client.Client.RemoteEndPoint}");
            streamToServer = client.GetStream();
        }

        public void SendMessage(string msg)
        {
            byte[] temp = Encoding.Unicode.GetBytes(msg);
            try
            {
                streamToServer.Write(temp, 0, temp.Length);
                Console.WriteLine($"Sent:{msg}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void BeginSendFile(string filePath)
        {
#if NETCOREAPP2_0
            beginSendFile(filePath);
#else
            ParameterizedThreadStart start = new ParameterizedThreadStart(beginSendFile);
            start.BeginInvoke(filePath, null, null);
#endif            
        }

        private void beginSendFile(object obj)
        {
            string filePath = obj as string;
            SendFile(filePath);
        }

        public void SendFile(string filePath)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ip, 0);

            listener.Start();

            // 获取本地监听的端口号
            IPEndPoint endPoint = listener.LocalEndpoint as IPEndPoint;
            int listeningPort = endPoint.Port;

            // 获取发送的协议字符串
            string fileName = Path.Combine(filePath);
            FileProtocol protocol = new FileProtocol(FileRequestMode.Send, listeningPort, fileName);
            string pro = protocol.ToString();

            // 发送协议到服务器
            SendMessage(pro);

            // 中断，等待远程连接
            TcpClient localClient = listener.AcceptTcpClient();
            Console.WriteLine("Start sending file……");
            NetworkStream stream = localClient.GetStream();

            // 创建文件流
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] fileBuffer = new byte[1024]; //每次传递1kb
            int bytesRead = 0;
            int totalBytes = 0;

            // 创建获取文件发送状态的类
            SendStatus status = new SendStatus(filePath);

            // 将文件流写入网络流
            try
            {
                do
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(10));    // 模拟网络传输延迟
                    bytesRead = fs.Read(fileBuffer, 0, fileBuffer.Length);
                    stream.Write(fileBuffer, 0, bytesRead);
                    totalBytes += bytesRead;
                    status.PrintStatus(totalBytes);
                } while (bytesRead > 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                stream.Dispose();
                fs.Dispose();
                localClient.Close();
                listener.Stop();
            }
        }
    }
}
