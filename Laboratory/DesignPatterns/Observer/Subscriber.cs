using System;

namespace Laboratory.DesignPatterns.Observer
{
    public class Subscriber
    {
        public void OnNumberChanged(int count)
        {
            Console.WriteLine($"subscriber notified: count = {count}");
        }
    }
}
