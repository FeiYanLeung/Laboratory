using Laboratory.Socket.Infrastructure;
using System;
using System.Net.Sockets;
using System.Text;

namespace Laboratory.Server
{
    public class Server
    {
        private TcpClient client;
        private NetworkStream streamToClient;
        private const int bufferSize = 8192;
        private byte[] buffer;
        private RequestHanlder hanlder;

        public Server(TcpClient client)
        {
            this.client = client;

            // 打印连接的客户端信息
            Console.WriteLine($"Client Connected! Local:{client.Client.LocalEndPoint} <-- Client:{client.Client.RemoteEndPoint}");

            // 获取流
            streamToClient = client.GetStream();
            buffer = new byte[bufferSize];

            // 设置RequestHanlder
            hanlder = new RequestHanlder();

            // 在构造函数中就准备开始读取
            var callback = new AsyncCallback(ReadComplete);
            streamToClient.BeginRead(buffer, 0, bufferSize, callback, null);
        }

        // 在读取完成时进行回调
        private void ReadComplete(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                bytesRead = streamToClient.EndRead(ar);
                if (bytesRead == 0)
                {
                    Console.WriteLine("Client offline.");
                    return;
                }
                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                Array.Clear(buffer, 0, buffer.Length);  // 清空缓存，避免脏读

                string[] msgArray = hanlder.GetActualString(msg);   // 获取实际内容

                // 遍历获取字符串
                foreach (string m in msgArray)
                {
                    Console.WriteLine($"Received: {m} [{bytesRead} bytes.]");
                    string back = m.ToUpper();

                    // 将得到的字符串改为大写并重新发送
                    byte[] temp = Encoding.Unicode.GetBytes(back);
                    streamToClient.Write(temp, 0, temp.Length);
                    streamToClient.Flush();
                    Console.WriteLine($"Sent:{back}");
                }

                // 再次调用BeginRead(),完成时调用自身，形成无限循环
                AsyncCallback callback = new AsyncCallback(ReadComplete);
                streamToClient.BeginRead(buffer, 0, bufferSize, callback, null);
            }
            catch (Exception e)
            {
                if (streamToClient != null)
                    streamToClient.Dispose();
                client.Close();

                Console.WriteLine(e.Message);
            }
        }
    }
}
