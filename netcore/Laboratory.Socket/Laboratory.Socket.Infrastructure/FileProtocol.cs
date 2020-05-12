namespace Laboratory.Socket.Infrastructure
{
    public struct FileProtocol
    {
        public FileRequestMode Mode { get; set; }
        public int Port { get; set; }
        public string FileName { get; set; }

        public FileProtocol(FileRequestMode mode, int port, string fileName)
        {
            this.Mode = mode;
            this.Port = port;
            this.FileName = fileName;
        }

        public override string ToString()
        {
            return $"<protocol><file name=\"{FileName}\" mode=\"{Mode}\" port=\"{Port}\" /></protocol>";
        }
    }
}
