
using Laboratory.DesignPatterns.Strategy;
using System;
using System.Threading;

namespace Laboratory.DesignPatterns
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "Design Patterns"; }
        }

        /// <summary>
        /// <see cref="https://github.com/FYLeung/DesignPatterns-full"/>
        /// </summary>
        public void Run()
        {
            Console.WriteLine("see: https://github.com/FYLeung/DesignPatterns-full");
            this.abstract_factory();
            //this.factory_method();
            //this.simple_factory();
            //this.strategy();
            //this.proxy();
            //this.singleton();
            //this.multition();
        }

        /// <summary>
        /// 策略模式
        /// </summary>
        void strategy()
        {
            var context = new StrategyContext(new StrategyImpls.Strategy1Impl());
            context.Make();

            context = new StrategyContext(new StrategyImpls.Strategy2Impl());
            context.Make();
        }

        /// <summary>
        /// 代理模式
        /// </summary>
        void proxy()
        {
            var _proxy_event = new Proxy.ProxyImpls.ProxyEventImpl(new Proxy.ProxyImpls.EventImpl());
            _proxy_event.Invoke();
        }

        /// <summary>
        /// 单例
        /// </summary>
        void singleton()
        {
            new Thread(new ThreadStart(() =>
            {
                while (Singleton.CounterImpl.Instance.Count <= 20)
                {
                    Console.WriteLine("线程1：" + Singleton.CounterImpl.Instance.Count);
                    Singleton.CounterImpl.Instance.Count++;
                    Thread.Sleep(200);
                }
            }))
            .Start();

            new Thread(new ThreadStart(() =>
            {
                while (Singleton.CounterImpl.Instance.Count <= 20)
                {
                    Console.WriteLine("线程2：" + Singleton.CounterImpl.Instance.Count);
                    Singleton.CounterImpl.Instance.Count++;
                    Thread.Sleep(200);
                }
            }))
         .Start();

        }

        /// <summary>
        /// 多例
        /// </summary>
        void multition()
        {
            for (int i = 0; i < 10; i++)
            {
                var umpire = Multition.UmpireImpl.Instance();
                Console.WriteLine("当前裁判是：{0}", umpire.Name);
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 简单工厂
        /// </summary>
        void simple_factory()
        {
            Console.WriteLine("plase choice (c)hinese cookie or (w)est cookie?");
            var k = Console.ReadKey().KeyChar;
            Console.Clear();
            var cookie = SimpleFactory.CookieSimpleFactory.Make(k);
            cookie.Print();
        }

        /// <summary>
        /// 工厂方法
        /// </summary>
        void factory_method()
        {
            var chineseFactory = new FactoryMethod.ChineseCookieFactory();
            var westFactory = new FactoryMethod.WestCookieFactory();

            var chineseCookie = chineseFactory.CreateCookieFactory();
            var westCookie = westFactory.CreateCookieFactory();

            chineseCookie.Print();
            westCookie.Print();
        }

        /// <summary>
        /// 抽象工厂
        /// </summary>
        void abstract_factory()
        {
            var mainBranchFactory = new AbstractFactory.MainBranchFactory();
            var mainBranchCookie = mainBranchFactory.MakeCookie();
            var mainBranchNoodle = mainBranchFactory.MakeNoodles();
            mainBranchCookie.Print();
            mainBranchNoodle.Print();


            var subBranchFactory = new AbstractFactory.SubBranchFactory();
            var subBranchCookie = subBranchFactory.MakeCookie();
            var subBranchNoodle = subBranchFactory.MakeNoodles();
            subBranchCookie.Print();
            subBranchNoodle.Print();
        }
    }
}
