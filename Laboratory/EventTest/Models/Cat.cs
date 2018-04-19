
using System;
namespace Laboratory.EventTest
{
    public sealed class Cat
    {
        public delegate void Crying();

        public event Crying cry;

        public void Cryed()
        {
            Console.WriteLine("喵喵喵，我发现了一只耗子");

            if (this.cry != null) this.cry.Invoke();
        }
    }
}
