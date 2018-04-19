using Laboratory.OverrideTest;
using System;

namespace Laboratory.ObjectTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get
            {
                return "对象比较";
            }
        }

        public void Run()
        {
            string a = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });
            string b = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });
            Console.WriteLine("(string)a == (string)b =>{0}", a == b);  //true
            Console.WriteLine("(string)a.Equals((string)b) =>{0}", a.Equals(b)); //true

            object g = a;
            object h = b;
            Console.WriteLine("(object[->string])g == (object->[string])h =>{0}", g == h);  //false
            Console.WriteLine("(object[->string])g.Equals((object[->string])h) =>{0}", g.Equals(h));    //true 因为a、b对象为string类型(特殊类型,相当于值类型)

            Student p1 = new Student();
            Student p2 = new Student();
            Console.WriteLine("(class)p1 == (class)p2 =>{0}", p1 == p2);    //true
            Console.WriteLine("(class)p1.Equals((class)p2) =>{0}", p1.Equals(p2));   //false

            Student p3 = new Student();
            Student p4 = p3;
            Console.WriteLine("(class)p3 == (class)p4 =>{0}", p3 == p4);    //true
            Console.WriteLine("(class)p3.Equals((class)p4) =>{0}", p3.Equals(p4));   //true
        }
    }
}
