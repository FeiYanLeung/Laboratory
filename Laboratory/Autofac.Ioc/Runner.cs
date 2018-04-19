using Autofac;
using System;

namespace Laboratory.Ioc
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "Autofac.Ioc";
            }
        }

        public void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<WhiteCat>().SingleInstance();  //注册成单例模式
            builder.RegisterType<BlackCat>();
            builder.RegisterType<WhiteCat>().As<ICat>();

            using (var container = builder.Build())
            {
                ICat wcat = container.Resolve<WhiteCat>();
                ICat bcat = container.Resolve<BlackCat>();

                if (wcat == bcat && Object.ReferenceEquals(wcat, bcat)) Console.WriteLine("两只一样的猫");
                else Console.WriteLine("两只不一样的猫");

                var myCat = container.Resolve<ICat>();
                myCat.Run();
            }
        }
    }
}
