using Laboratory.Socket.Infrastructure;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Laboratory.Client
{
    public class Client
    {

        private const int bufferSize = 8192;
        private byte[] buffer;
        private TcpClient client;
        private NetworkStream streamToServer;
        private RequestHanlder hanlder;

        public Client()
        {
            try
            {
                client = new TcpClient();
                client.Connect(new IPAddress(new byte[] { 127, 0, 0, 1 }), 8500);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            buffer = new byte[bufferSize];

            // 设置RequestHanlder
            hanlder = new RequestHanlder();

            // 打印连接到的服务器信息
            Console.WriteLine($"Server connected! Local:{client.Client.LocalEndPoint} <-- Server:{client.Client.RemoteEndPoint}");
            streamToServer = client.GetStream();
        }

        public void SendMessage(string msg)
        {
            msg = $"[length={msg.Length}]{msg}";

            for (int i = 0; i <= 2; i++)
            {
                byte[] temp = Encoding.Unicode.GetBytes(msg);   // 获得缓存
                try
                {
                    streamToServer.Write(temp, 0, temp.Length);  // 发送信息到服务器
                    Console.WriteLine($"Sent:{msg}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }

            AsyncCallback callback = new AsyncCallback(ReadComplete);
            streamToServer.BeginRead(buffer, 0, bufferSize, callback, null);
        }

        private void ReadComplete(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                bytesRead = streamToServer.EndRead(ar);
                if (bytesRead == 0)
                {
                    Console.WriteLine("Server offline.");
                    return;
                }

                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                Array.Clear(buffer, 0, buffer.Length);  // 清空缓存，避免脏读

                string[] msgArray = hanlder.GetActualString(msg);   // 获取实际内容

                // 遍历获取字符串
                foreach (string m in msgArray)
                {
                    Console.WriteLine($"Received: {m} [{bytesRead} bytes.]");
                }

                // 再次调用BeginRead(),完成时调用自身，形成无限循环
                AsyncCallback callback = new AsyncCallback(ReadComplete);
                streamToServer.BeginRead(buffer, 0, bufferSize, callback, null);
            }
            catch (Exception e)
            {
                if (streamToServer != null)
                    streamToServer.Dispose();
                client.Close();

                Console.WriteLine(e.Message);
            }
        }

        public void Release()
        {
            try
            {
                if (streamToServer != null)
                    streamToServer.Dispose();

                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}