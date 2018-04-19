using ServiceStack.Redis;
using System;

namespace Laboratory.RedisTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "Redis";
            }
        }

        public void Run()
        {
            var db = new RedisClient("127.0.0.1", 6379, "lottak2014", 0);

            Console.WriteLine(db.GetAll<string>());

            db.AddItemToList("listid", "1");

            Console.WriteLine("请输入设置的键：");
            var _key = Console.ReadLine();
            Console.WriteLine("请输入设置的值：");
            var _value = Console.ReadLine();

            db.Set<string>(_key, _value);
            Console.WriteLine("设置完成=>{0}:{1}", _key, db.Get<string>(_key));

            Console.WriteLine(db.Exists(_key));
        }
    }
}
