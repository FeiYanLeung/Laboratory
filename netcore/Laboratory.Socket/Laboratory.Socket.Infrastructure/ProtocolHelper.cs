using System;
using System.Xml;

namespace Laboratory.Socket.Infrastructure
{
    public class ProtocolHelper
    {
        private XmlNode fileNode;
        private XmlNode root;

        public ProtocolHelper(string protocol)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(protocol);
            root = doc.DocumentElement;
            fileNode = root.SelectSingleNode("file");
        }

        // 此时的protocal一定为单条完整的protocal
        private FileRequestMode GetFileMode()
        {
            string mode = fileNode.Attributes["mode"].Value;
            mode = mode.ToLower();

            if (mode == "send")
                return FileRequestMode.Send;
            else
                return FileRequestMode.Receive;
        }

        // 获取单条协议包含的信息
        public FileProtocol GetProtocol()
        {
            FileRequestMode mode = GetFileMode();
            string fileName = "";
            int port = 0;

            fileName = fileNode.Attributes["name"].Value;
            port = Convert.ToInt32(fileNode.Attributes["port"].Value);

            return new FileProtocol(mode, port, fileName);
        }
    }
}
