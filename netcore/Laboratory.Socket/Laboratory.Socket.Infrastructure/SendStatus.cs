using System;
using System.IO;

namespace Laboratory.Socket.Infrastructure
{
    /// <summary>
    /// 即时计算发送文件状态
    /// </summary>
    public class SendStatus
    {
        private FileInfo info;
        private long fileBytes;

        public SendStatus(string filePath)
        {
            info = new FileInfo(filePath);
            fileBytes = info.Length;
        }

        public void PrintStatus(int sent)
        {
            string percent = GetPercent(sent);
            Console.WriteLine($"Sending {sent} bytes, {percent}");
        }

        /// <summary>
        /// 获取文件发送百分比
        /// </summary>
        /// <param name="sent"></param>
        /// <returns></returns>
        public string GetPercent(int sent)
        {
            decimal allBytes = Convert.ToDecimal(fileBytes);
            decimal currentSent = Convert.ToDecimal(sent);

            decimal percent = (currentSent / allBytes) * 100;

            return percent.ToString("p");
        }
    }
}
