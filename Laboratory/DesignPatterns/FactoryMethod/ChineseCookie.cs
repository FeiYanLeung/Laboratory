using System;

namespace Laboratory.DesignPatterns.FactoryMethod
{
    public class ChineseCookie : Cookie
    {
        public override void Print()
        {
            Console.WriteLine("东方甜点");
        }
    }
}
