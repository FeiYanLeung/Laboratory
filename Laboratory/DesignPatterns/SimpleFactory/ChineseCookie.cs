using System;

namespace Laboratory.DesignPatterns.SimpleFactory
{
    public class ChineseCookie : Cookie
    {
        public override void Print()
        {
            Console.WriteLine("东方甜点");
        }
    }
}
