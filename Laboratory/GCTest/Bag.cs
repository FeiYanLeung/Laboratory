using System;

namespace Laboratory.GCTest
{
    public class Bag
    {
        public Bag()
        {
            Console.WriteLine("");
        }

        /// <summary>
        /// 析构函数，在对象被回收时调用
        /// </summary>
        ~Bag()
        {
            Console.WriteLine("析构函数：我要被回收了.");
        }
    }
}
