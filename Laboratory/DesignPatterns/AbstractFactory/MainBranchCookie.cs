using System;

namespace Laboratory.DesignPatterns.AbstractFactory
{
    public class MainBranchCookie : Cookie
    {
        public override void Print()
        {
            Console.WriteLine("主店制作的Cookie");
        }
    }
}
