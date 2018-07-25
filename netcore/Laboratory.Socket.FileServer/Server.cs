using Laboratory.Socket.Infrastructure;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Laboratory.Socket.FileServer
{
    public class Server
    {
        private TcpClient client;
        private NetworkStream streamToClient;
        private const int bufferSize = 8192;
        private byte[] buffer;
        private ProtocolHandler handler;

        public Server(TcpClient client)
        {
            this.client = client;

            // 打印连接到的客户端信息
            Console.WriteLine($"Client Connected! Local:{client.Client.LocalEndPoint} <-- Client:{client.Client.RemoteEndPoint}");

            // 获得流
            streamToClient = client.GetStream();
            buffer = new byte[bufferSize];

            handler = new ProtocolHandler();
        }

        // 开始进行读取
        public void BeginRead()
        {
            AsyncCallback callback = new AsyncCallback(OnReadComplete);
            streamToClient.BeginRead(buffer, 0, bufferSize, callback, null);
        }

        /// <summary>
        /// 在读取完成时进行回调
        /// </summary>
        /// <param name="ar"></param>
        private void OnReadComplete(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                bytesRead = streamToClient.EndRead(ar);
                Console.WriteLine($"Reading data, {bytesRead} bytes.");

                if (bytesRead == 0)
                {
                    Console.WriteLine("Client offline.");
                    return;
                }

                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);

                // 清空缓存，避免脏读
                Array.Clear(buffer, 0, buffer.Length);

                // 获取protocol数组
                string[] protocolArray = handler.GetProtocol(msg);
                foreach (var pro in protocolArray)
                {
#if NETCOREAPP2_0
                    handlerProtocol(pro);
#else
                    // 异步读取，避免阻塞
                    ParameterizedThreadStart start = new ParameterizedThreadStart(handlerProtocol);
                    start.BeginInvoke(pro, null, null);
#endif
                }

                // 再次调用BeginRead(), 完成时调用自身，形成无限循环
                AsyncCallback callback = new AsyncCallback(OnReadComplete);
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

        // 处理protocol
        private void handlerProtocol(object obj)
        {
            string pro = obj as string;
            ProtocolHelper helper = new ProtocolHelper(pro);
            FileProtocol protocol = helper.GetProtocol();

            if (protocol.Mode == FileRequestMode.Send)
            {
                // 客户端发送文件，对于服务器来说是接收文件
                receiveFile(protocol);
            }
            else if (protocol.Mode == FileRequestMode.Receive)
            {
                // 客户端接收文件，对于服务器来说是发送文件
                //sendFile(protocol);
            }
        }

        // 接收文件
        private void receiveFile(FileProtocol protocol)
        {
            // 获取远程客户端位置
            IPEndPoint endPoint = client.Client.RemoteEndPoint as IPEndPoint;
            IPAddress ip = endPoint.Address;

            // 使用新端口号，用于获取远程传输文件
            endPoint = new IPEndPoint(ip, protocol.Port);

            // 连接到远程客户端
            TcpClient localClient;
            try
            {
                localClient = new TcpClient();
                localClient.Connect(endPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine($"无法连接到客户端{endPoint} \r\n[{e.Message}]");
                return;
            }

            // 获取发送文件的流
            NetworkStream streamToClient = localClient.GetStream();
            // 随机生成一个在当前目录下的文件名称
            string path = Environment.CurrentDirectory + "/" + generateFileName(protocol.FileName);
            byte[] fileBuffer = new byte[1024]; // 每次接收1kb
            FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);

            // 从缓存buffer中读取到文件流中
            int bytesRead = 0;
            int totalBytes = 0;

            do
            {
                bytesRead = streamToClient.Read(buffer, 0, bufferSize);

                fs.Write(buffer, 0, bytesRead);
                totalBytes += bytesRead;

                Console.WriteLine($"Receiving {totalBytes} bytes. ");
            } while (bytesRead > 0);

            Console.WriteLine($"Total {totalBytes} bytes received, Done!");

            streamToClient.Dispose();
            fs.Dispose();
            localClient.Close();
        }

        /// <summary>
        /// 随机获取一个文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string generateFileName(string fileName)
        {
            var now = DateTime.Now;
            return $"{now.Millisecond}_{now.Hour}_{now.Minute}_{fileName}";
        }
    }
}
