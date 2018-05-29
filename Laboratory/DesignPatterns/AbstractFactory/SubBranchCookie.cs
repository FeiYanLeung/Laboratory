using System;

namespace Laboratory.DesignPatterns.AbstractFactory
{
    public class SubBranchCookie : Cookie
    {
        public override void Print()
        {
            Console.WriteLine("分店的Cookie不如主店好吃");
        }
    }
}
