using System;
using System.Collections.Generic;

namespace Laboratory.YieldTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "Yield函数";
            }
        }



        //申明属性，定义数据来源
        private static List<int> Data
        {
            get
            {
                return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            }
        }

        //申明属性，过滤器(不适用yield)
        private static IEnumerable<int> FilterWithoutYield
        {
            get
            {
                foreach (var i in Data)
                {
                    if (i > 4)
                        yield return i;
                }
            }
        }
        public void Run()
        {
            foreach (var item in Data)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("==================");

            foreach (var item in FilterWithoutYield)
            {
                Console.WriteLine(item);
            }
        }
    }
}
