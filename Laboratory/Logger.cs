using System;
using System.IO;

namespace Laboratory
{
    public class Logger
    {
        private static readonly object _locker = new object();
        private static Logger _instance;
        public static Logger Instance
        {
            get
            {
                lock (_locker)
                {
                    if (_instance == null) _instance = new Logger();
                }
                return _instance;
            }
        }

        public void Log(string message, string path = @".\consoleOut.log")
        {
            var tsw = Console.Out;
            using (var sw = new StreamWriter(path))
            {
                Console.SetOut(sw);
                Console.WriteLine(message);
                sw.Flush();
            }
            Console.SetOut(tsw);
        }
    }
}
