using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.OverrideTest
{
    public abstract class Person
    {
        public abstract void Speak();
        public abstract void Walk();

        public virtual void Sleep()
        {
            Console.WriteLine("Person");
        }
    }
}
