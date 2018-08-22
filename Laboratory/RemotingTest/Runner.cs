using System;

namespace Laboratory.RemotingTest
{
    /// <summary>
    /// .net remoting
    /// </summary>
    public class Runner : IRunner
    {
        public string Name => ".NET Remoting";

        public void Run()
        {
            test1();
            Console.ReadLine();
        }

        void test1()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;  // 获取当前应用程序域
            Console.WriteLine(currentDomain.FriendlyName);  // 显示名称

            DomainClass obj;
            // obj = new DomainClass(); // 常规创建对象方式

            // 在默认应用程序中创建对象
            obj = (DomainClass)currentDomain.CreateInstanceAndUnwrap("Laboratory.RemotingTest", "Laboratory.RemotingTest.Runner");

            obj.ShowAppDomain();
            obj.ShowCount("leung");
            obj.ShowCount("liang");
        }
    }
}
