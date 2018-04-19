
using System;
namespace Laboratory.DesignPatterns
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "Design Patterns"; }
        }

        public void Run()
        {
            Console.WriteLine("see: https://github.com/FYLeung/DesignPatterns-full");
        }
    }
}
