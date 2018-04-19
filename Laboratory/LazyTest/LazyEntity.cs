using System;

namespace Laboratory.LazyTest
{
    public class LazyEntity
    {
        public LazyEntity()
        {
            Console.WriteLine(".ctor");
        }
        public int Id
        {
            get
            {
                return 1;
            }
        }
    }
}
