using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.OverrideTest
{
    public class Student : Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public static bool operator ==(Student student1, Student student2)
        {
            return student1.Age == student2.Age;
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return !(student1 == student2);
        }

        public override string ToString()
        {
            return string.Format("{0}今年{1}岁", this.Name, this.Age);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Speak()
        {
            Console.WriteLine("{0}:我能说啥呢？", this.Name);
        }

        public override void Walk()
        {
            Console.WriteLine("{0}：你几岁？", this.Name);
        }


        public override void Sleep()
        {
            base.Sleep();

            Console.WriteLine("Student");

        }
    }
}
