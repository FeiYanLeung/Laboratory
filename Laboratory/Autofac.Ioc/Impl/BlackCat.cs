using System;

namespace Laboratory.Ioc
{
    public class BlackCat : ICat
    {
        public void Run()
        {
            Console.WriteLine("奔跑吧，黑猫");
        }
    }
}
