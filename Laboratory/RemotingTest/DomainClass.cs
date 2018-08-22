using System;

namespace Laboratory.RemotingTest
{
    public class DomainClass
    {
        private int count = 0;

        public DomainClass()
        {
            Console.WriteLine("domain class: constructor.");
        }

        public void ShowCount(string name)
        {
            count++;
            Console.WriteLine($"{name}, the count is {count}.");
        }

        public void ShowAppDomain()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine(currentDomain.FriendlyName);
        }

    }
}
