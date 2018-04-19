using System;

namespace Laboratory.LazyTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "懒加载";
            }
        }
        public void Run()
        {
            var entity = new LazyEntity();
            Console.WriteLine("entity对象已初始化");
            Console.WriteLine(entity.Id);

            Console.WriteLine("==================");

            var entity2 = new Lazy<LazyEntity>();
            Console.WriteLine("entity2对象已初始化");

            Console.WriteLine(entity2.Value.Id);
        }
    }
}
