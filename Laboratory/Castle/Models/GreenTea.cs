using Autofac.Extras.DynamicProxy;
using System;
using System.Threading;

namespace Laboratory.Castle
{
    [Intercept(typeof(TeaInterceptor))]
    public class GreenTea : ITea
    {
        /// <summary>
        /// 茶叶名称
        /// </summary>
        public string Name { get => "绿茶"; }

        /// <summary>
        /// 泡茶
        /// </summary>
        public void Make()
        {
            Console.WriteLine($"老板，给我一杯{this.Name}");
            Thread.Sleep(2000);
            Console.WriteLine("客官，您的茶");
        }
    }
}
