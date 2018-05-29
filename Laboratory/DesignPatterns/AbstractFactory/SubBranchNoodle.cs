using System;

namespace Laboratory.DesignPatterns.AbstractFactory
{
    public class SubBranchNoodle : Noodle
    {
        public override void Print()
        {
            Console.WriteLine("怪了，分店的居然比老店的好吃");
        }
    }
}
