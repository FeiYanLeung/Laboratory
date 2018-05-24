using Autofac.Extras.DynamicProxy;
using System;
using System.Threading;

namespace Laboratory.Castle
{
    [Intercept("tea-standard-interceptor")]
    public class RedTea : ITea
    {
        public string Name { get => "红茶"; }

        public void Make()
        {
            Console.WriteLine($"老板，给我一杯{this.Name}");
            Thread.Sleep(2000);
            Console.WriteLine("客官，您的茶");
        }
    }
}
