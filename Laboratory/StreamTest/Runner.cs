using System;
using System.IO;
using System.Text;

namespace Laboratory.StreamTest
{
    public class Runner : IRunner
    {

        public string Name
        {
            get { return "Stream"; }
        }

        public void Run()
        {
            var buff = Encoding.UTF8.GetBytes("二进制");

            using (var stream = new FileStream("./StreamTest/Writter.bs", FileMode.Create))
            {
                using (var bw = new BinaryWriter(stream))
                {
                    bw.Write(buff, 0, buff.Length);
                }
            }

            using (var reader = new FileStream("./StreamTest/Writter.bs", FileMode.Open))
            {
                using (var bw = new BinaryReader(reader, Encoding.UTF8))
                {
                    Console.WriteLine(Encoding.UTF8.GetString(bw.ReadBytes(buff.Length)));
                }
            }

        }
    }
}
