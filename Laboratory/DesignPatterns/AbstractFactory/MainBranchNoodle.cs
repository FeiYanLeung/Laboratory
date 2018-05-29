using System;

namespace Laboratory.DesignPatterns.AbstractFactory
{
    public class MainBranchNoodle : Noodle
    {
        public override void Print()
        {
            Console.WriteLine("老店里面煮碗面");
        }
    }
}
