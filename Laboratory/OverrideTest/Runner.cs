using System;

namespace Laboratory.OverrideTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "对象重写"; }
        }

        public void Run()
        {
            Student stu1 = new Student()
            {
                Name = "张三",
                Age = 20
            };

            Student stu2 = new Student()
            {
                Name = "李四",
                Age = 18
            };

            Console.WriteLine(stu1.ToString());
            Console.WriteLine(stu2.ToString());

            stu1.Walk();
            stu2.Walk();

            stu1.Speak();
            stu2.Speak();

            stu1.Sleep();

            Console.WriteLine(stu1.GetHashCode());
            Console.WriteLine("{0}和{1}年龄相同={2}", stu1.Name, stu2.Name, stu1 == stu2);
        }
    }
}
